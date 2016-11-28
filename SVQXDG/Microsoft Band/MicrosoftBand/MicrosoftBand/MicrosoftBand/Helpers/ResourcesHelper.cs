namespace MicrosoftBand.Helpers
{
    using Microsoft.Band.Portable;
    using ViewModels;
    using System.Threading.Tasks;
    using System.Reflection;

    public static class ResourcesHelper
    {
        public static async Task<BandImage> LoadBandImageFromResourceAsync(string resourcePath)
        {
            var stream = await Task.Run(() =>
            {
                var assembly = typeof(MainViewModel).GetTypeInfo().Assembly;
                resourcePath = "MicrosoftBand." + resourcePath.Replace("/", ".").Replace("\\", ".");
                return assembly.GetManifestResourceStream(resourcePath);
            });
   
            return await BandImage.FromStreamAsync(stream);
        }
    }
}
