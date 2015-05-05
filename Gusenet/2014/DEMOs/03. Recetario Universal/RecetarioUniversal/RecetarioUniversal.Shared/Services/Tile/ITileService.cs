namespace RecetarioUniversal.Services.Tile
{
    using System;
    using System.Threading.Tasks;

    public interface ITileService
    {
        bool SecondaryTileExists(string tileId);

        Task<bool> UnpinTile(string tileId);

        Task<bool> PinToStart(string tileId, string shortName, Uri image, string arguments);
    }
}
