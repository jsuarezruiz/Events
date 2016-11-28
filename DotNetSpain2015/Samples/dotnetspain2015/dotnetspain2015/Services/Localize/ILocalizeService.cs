namespace dotnetspain2015.Services.Localize
{
    using System.Globalization;

    public interface ILocalizeService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        CultureInfo GetCurrentCultureInfo();

        /// <summary>
        /// 
        /// </summary>
        void SetLocale();
    }
}
