
using System;
using Windows.System.Display;

/// <summary>
/// ################################################################
///  EXPERIMENTAL HELPER IMPLEMENTATION
/// ################################################################
/// </summary>
namespace WindowsPhoneUWP.UpgradeHelpers
{

	public class PhoneApplicationServiceHelper
	{

		public static void UserIdleDetectionMode(IdleDetectionModeHelper argument)
		{
			DisplayRequest displayRequest = new DisplayRequest();
			if (argument == IdleDetectionModeHelper.Enabled)

				displayRequest.RequestRelease();
			else
				displayRequest.RequestActive();
		}

	}
}