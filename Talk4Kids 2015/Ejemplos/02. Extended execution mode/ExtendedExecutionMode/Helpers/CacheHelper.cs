namespace ExtendedExecutionMode.Helpers
{
    using System.Threading.Tasks;

    public static class CacheHelper
    {
        public static Task SaveLocalData()
        { 
            // Save local data
            return Task.Delay(500);
        }

        public static Task SaveCloudData()
        {
            // Save cloud data
            return Task.Delay(1000);
        }

        public static Task LoadLocalData()
        {
            // Load local data
            return Task.Delay(500);
        }
    }
}
