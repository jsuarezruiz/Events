using System;
using Windows.System.Profile;
using XboxTvSafeArea.Models;

namespace XboxTvSafeArea.Helpers
{
    public static class PlatformHelper
    {
        public static DeviceFamily GetDeviceFamily()
        {
            var deviceFamily = AnalyticsInfo.VersionInfo.DeviceFamily;

            switch (deviceFamily)
            {
                case "Windows.Mobile":
                    return DeviceFamily.Mobile;
                case "Windows.Desktop":
                    return DeviceFamily.Desktop;
                case "Windows.Team":
                    return DeviceFamily.Team;
                case "Windows.IoT":
                    return DeviceFamily.IoT;
                case "Windows.Xbox":
                    return DeviceFamily.Xbox;
                default:
                    return DeviceFamily.Unknown;
            }
        }

        public static bool IsPhone()
        {
                return AnalyticsInfo.VersionInfo.DeviceFamily.Equals("Windows.Mobile",
                    StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsDesktop()
        {
                return AnalyticsInfo.VersionInfo.DeviceFamily.Equals("Windows.Desktop",
                StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsXbox()
        {
                return AnalyticsInfo.VersionInfo.DeviceFamily.Equals("Windows.Xbox",
                StringComparison.CurrentCultureIgnoreCase);
        }
    }
}