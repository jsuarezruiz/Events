using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Shell;

namespace Recetario.Services.Tile
{
    public class LiveTileServiceWP8 : ILiveTileServiceWP8
    {
        // Título
        public string Title { get; set; }
        public string BackTitle { get; set; }

        // Contador
        public int Count { get; set; }

        // Texto
        public string BackContent { get; set; }
        public string WideBackContent { get; set; }

        // Imágenes
        public string BackgroundImagePath { get; set; }
        public string BackBackgroundImagePath { get; set; }
        public string SmallBackgroundImagePath { get; set; }
        public string WideBackgroundImagePath { get; set; }
        public string WideBackBackgroundImagePath { get; set; }

        public string Url { get; set; }

        public void UpdateTile()
        {
            var tileData = new FlipTileData
            {
                Title = Title ?? "",
                Count = Count,
                BackTitle = BackTitle ?? "",
                BackContent = BackContent ?? "",
                BackgroundImage = new Uri(BackgroundImagePath ?? "", UriKind.Relative),
                BackBackgroundImage = new Uri(BackBackgroundImagePath ?? "", UriKind.Relative)
            };

            // Actualiza el live tile con la plantilla
            ShellTile.ActiveTiles.FirstOrDefault().Update(tileData);
        }

        public bool TileExists(string navSource)
        {
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault(o => o.NavigationUri.ToString().Contains(navSource));
            return tile == null ? false : true;
        }

        public void CreateTile(string navSource)
        {
            var cycleTile = new CycleTileData();
            cycleTile.Title = Title ?? "";
            cycleTile.Count = Count;

            cycleTile.SmallBackgroundImage = new Uri(SmallBackgroundImagePath ?? "", UriKind.Relative);
            var images = new List<Uri>();
            images.Add(new Uri(BackgroundImagePath ?? "", UriKind.Relative));
            images.Add(new Uri(BackBackgroundImagePath ?? "", UriKind.Relative));
            cycleTile.CycleImages = images;

            ShellTile.Create(new Uri(string.Format("{0}&type=cycle", navSource), UriKind.Relative), cycleTile, true);
        }

        public void DeleteTile(string navSource)
        {
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault(o => o.NavigationUri.ToString().Contains(navSource));
            if (tile == null) return;

            tile.Delete();
        }
    }
}