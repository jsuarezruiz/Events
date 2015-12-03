namespace ExtendedExecutionMode.Helpers
{
    using Windows.UI.Notifications;

    public static class ToastHelper
    {
        public static void ShowToast(string toastMessage)
        {
            var toastTemplate = ToastTemplateType.ToastText01;
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(toastMessage));
            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier("App").Show(toast);
        }
    }
}
