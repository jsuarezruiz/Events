namespace Task
{
    using System.Diagnostics;
    using Windows.ApplicationModel.Background;
    using Helper;

    public sealed class BackgroundTask : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Debug.WriteLine("Background " + taskInstance.Task.Name + " Starting...");

            // Grab the deferral to make sure that the background task won't be called again until
            // you are finished processing this set of changes
            _deferral = taskInstance.GetDeferral();

            ToastHelper.ShowToast("Timer Trigger!");
            
            taskInstance.Canceled += TaskInstance_Canceled;
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            Debug.WriteLine("Background " + sender.Task.Name + " Cancel Requested...");

            _deferral?.Complete();
        }
    }
}
