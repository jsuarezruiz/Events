namespace ApplicationTrigger.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Background;

    public static class BackgroundTaskHelper
    {
        public static async Task<BackgroundTaskRegistration> RegisterBackgroundTasksAsync(Type backgroundTaskType, IBackgroundTrigger trigger)
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

        public static async Task<BackgroundTaskRegistration> RegisterApplicationTrigger(Type backgroundTaskType,
            ApplicationTrigger trigger)
        {
            try
            {
                var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();

                if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                    backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
                {
                    string taskEntryPoint = backgroundTaskType.ToString();
                    var backgroundTaskBuilder = new BackgroundTaskBuilder
                    {
                        Name = backgroundTaskType.Name,
                        TaskEntryPoint = taskEntryPoint
                    };

                    backgroundTaskBuilder.SetTrigger(trigger);

                    var task =
                        BackgroundTaskRegistration.AllTasks.Values.FirstOrDefault(
                            i => i.Name.Equals(backgroundTaskType.Name));
                    task?.Unregister(true);
                    var appTriggerTask = backgroundTaskBuilder.Register();

                    return appTriggerTask;
                }

                return null;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
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