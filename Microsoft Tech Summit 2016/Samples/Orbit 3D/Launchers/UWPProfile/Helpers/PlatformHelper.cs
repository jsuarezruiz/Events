namespace Orbit.Helpers
{
    public class PlatformHelper
    {
        private static string _deviceFamily;

        public static bool IsXbox()
        {
            if (_deviceFamily == null)
                _deviceFamily = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

            if(XboxSimulator.IsActivate)
                _deviceFamily = "Windows.Xbox";

            return _deviceFamily == "Windows.Xbox";
        }
    }
}
