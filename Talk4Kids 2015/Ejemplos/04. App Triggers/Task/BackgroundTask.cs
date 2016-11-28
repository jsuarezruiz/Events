namespace Task
{
    using System;
    using System.Diagnostics;
    using Windows.ApplicationModel.Background;
    using Windows.Storage;
    using Windows.System.Threading;
    using Helpers;

    public sealed class BackgroundTask : IBackgroundTask
    {
        BackgroundTaskCancellationReason _cancelReason = BackgroundTaskCancellationReason.Abort;
        volatile bool _cancelRequested;
        BackgroundTaskDeferral _deferral;
        ThreadPoolTimer _periodicTimer;
        uint _progress;
        IBackgroundTaskInstance _taskInstance;

        /// <summary>
        /// The Run method is the entry point of a background task.
        /// </summary>
        /// <param name="taskInstance"></param>
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Debug.WriteLine("Background " + taskInstance.Task.Name + " Starting...");

            // Query BackgroundWorkCost
            // Guidance: If BackgroundWorkCost is high, then perform only the minimum amount
            // of work in the background task and return immediately.
            var cost = BackgroundWorkCost.CurrentBackgroundWorkCost;
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values["BackgroundWorkCost"] = cost.ToString();

            // Associate a cancellation handler with the background task.
            taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);

            // Get the deferral object from the task instance, and take a reference to the taskInstance;
            _deferral = taskInstance.GetDeferral();
            _taskInstance = taskInstance;

            _periodicTimer = ThreadPoolTimer.CreatePeriodicTimer(new TimerElapsedHandler(PeriodicTimerCallback), TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Handles background task cancellation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            // Indicate that the background task is canceled.
            _cancelRequested = true;
            _cancelReason = reason;

            Debug.WriteLine("Background " + sender.Task.Name + " Cancel Requested...");
        }

        /// <summary>
        /// Simulate the background task activity.
        /// </summary>
        /// <param name="timer"></param>
        private void PeriodicTimerCallback(ThreadPoolTimer timer)
        {
            if ((_cancelRequested == false) && (_progress < 100))
            {
                _progress += 10;
                ToastHelper.ShowToast("Progress:" + _progress + "%");
                _taskInstance.Progress = _progress;
            }
            else
            {
                _periodicTimer.Cancel();

                var settings = ApplicationData.Current.LocalSettings;
                var key = _taskInstance.Task.Name;

                // Write to LocalSettings to indicate that this background task ran.
                settings.Values[key] = (_progress < 100) ? "Canceled with reason: " + _cancelReason : "Completed";
                Debug.WriteLine("Background " + _taskInstance.Task.Name + settings.Values[key]);

                // Indicate that the background task has completed.
                _deferral.Complete();
            }
        }
    }
}