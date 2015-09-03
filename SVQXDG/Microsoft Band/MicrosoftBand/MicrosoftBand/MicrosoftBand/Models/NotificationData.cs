namespace MicrosoftBand.Models
{
    using Microsoft.Band.Portable;
    using Microsoft.Band.Portable.Tiles;

    public class NotificationData
    {
        public BandClient BandClient { get; set; }
        public BandTile Tile { get; set; }
    }
}
