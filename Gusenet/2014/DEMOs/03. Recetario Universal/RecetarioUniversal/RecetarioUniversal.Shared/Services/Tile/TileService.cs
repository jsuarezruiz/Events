namespace RecetarioUniversal.Services.Tile
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Windows.Foundation;
    using Windows.UI.Popups;
    using Windows.UI.StartScreen;
    using Windows.UI.Xaml.Media;

    public class TileService : ITileService
    {
        //Default logos for Tile.
        private readonly Uri _squareLogoUri = new Uri("ms-appx:///Assets/Logo.png");
        private readonly Uri _wideLogoUri = new Uri("ms-appx:///Assets/WideLogo.scale-100.png");

        /// <summary>
        /// Check if a secondary tile exists.
        /// </summary>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public bool SecondaryTileExists(string tileId)
        {
            return SecondaryTile.Exists(tileId);
        }

        /// <summary>
        /// Create and pin a secondary square Tile.
        /// </summary>
        /// <param name="tileId"></param>
        /// <param name="shortName"></param>
        /// <param name="image"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public async Task<bool> PinToStart(string tileId, string shortName, Uri image, string arguments)
        {
            // Create the unique tileId
            var id = Regex.Replace(tileId, @"[^\d\w\s]", "-").
                     Replace(" ", string.Empty) + ".LiveTile";

            //Check first if Tile exists
            if (!SecondaryTileExists(id))
            {
                //Create a secondary Tile
                var secondaryTile = new SecondaryTile(id, shortName, shortName, image, TileSize.Default);

#if WINDOWS_PHONE_APP     
                bool isPinned = await secondaryTile.RequestCreateAsync();
#else
                // Position of the modal dialog 
                GeneralTransform buttonTransform = App.Frame.TransformToVisual(null);
                Point point = buttonTransform.TransformPoint(new Point());
                var rect = new Rect(point, new Size(App.Frame.ActualWidth, App.Frame.ActualHeight));

                //Pin
                bool isPinned = await secondaryTile.RequestCreateForSelectionAsync(rect, Placement.Below);
#endif

                return isPinned;
            }

            return true;
        }

        /// <summary>
        /// UnPin a existed Tile.
        /// </summary>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public async Task<bool> UnpinTile(string tileId)
        {
            // Create the unique tileId
            var id = Regex.Replace(tileId, @"[^\d\w\s]", "-").
                     Replace(" ", string.Empty) + ".LiveTile";

            //Check first if tile exists
            if (SecondaryTileExists(id))
            {
                var secondaryTile = new SecondaryTile(id);
                bool isUnpinned = await secondaryTile.RequestDeleteAsync();

                return isUnpinned;
            }

            return true;
        }
    }
}
