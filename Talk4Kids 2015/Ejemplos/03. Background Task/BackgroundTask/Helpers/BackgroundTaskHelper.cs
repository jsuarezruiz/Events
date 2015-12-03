namespace BackgroundTask.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Background;
    using Windows.Foundation.Collections;

    public static class BackgroundTaskHelper
    {
        public static async Task<BackgroundTaskRegistration> RegisterBackgroundTasksAsync(Type backgroundTaskType, IBackgroundTrigger trigger)
        {
            try
            {
                string taskEntryPoint = backgroundTaskType.ToString();

                if (!IsBackgroundTaskAlreadyRegistered(backgroundTaskType.Name))
                {
                    // Already registered  
                    Debug.WriteLine("Already registered", "Debug");
                    return null;
                }

                // Request access
                var access = await BackgroundExecutionManager.RequestAccessAsync();

                // If denied
                if (access == BackgroundAccessStatus.Denied)
                {
                    Debug.WriteLine("Access Denied, check your capabilities", "Debug");
                    return null;
                }

                // Construct the background task
                var builder = new BackgroundTaskBuilder
                {
                    Name = backgroundTaskType.Name,
                    TaskEntryPoint = taskEntryPoint
                };

                // Set trigger
                builder.SetTrigger(trigger);

                // And register the background task
                BackgroundTaskRegistration task = builder.Register();

                return task;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }

        public static async Task RegisterApplicationTrigger(Type backgroundTaskType)
        {
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();

            if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
            {
                var appTrigger = new ApplicationTrigger();
                string taskEntryPoint = backgroundTaskType.ToString();
                var backgroundTaskBuilder = new BackgroundTaskBuilder
                {
                    Name = backgroundTaskType.Name,
                    TaskEntryPoint = taskEntryPoint
                };

                backgroundTaskBuilder.SetTrigger(appTrigger);

                var task = BackgroundTaskRegistration.AllTasks.Values.FirstOrDefault(i => i.Name.Equals(backgroundTaskType.Name));
                task?.Unregister(true);
                backgroundTaskBuilder.Register();

                ValueSet message = new ValueSet { { "Command", "Init" } };

                var result = await appTrigger.RequestAsync(message);

                if (result == ApplicationTriggerResult.Allowed)
                {
                    Debug.WriteLine(string.Format("{0} called!", backgroundTaskType.Name));
                }
            }
        }

        public static void UnregisterBackgroundTask(Type backgroundTaskType)
        {
            var task = BackgroundTaskRegistration.AllTasks.Values.FirstOrDefault(i => i.Name.Equals(backgroundTaskType.Name));

            task?.Unregister(true);
        }

        private static bool IsBackgroundTaskAlreadyRegistered(string taskName)
        {
            bool retVal = true;

            lock (taskName)
            {
                if (BackgroundTaskRegistration.AllTasks.Any(cur => cur.Value.Name == taskName))
                {
                    retVal = false;
                }
            }

            return retVal;
        }
    }
}
