namespace MicrosoftBand.ViewModels
{
    using Base;
    using Microsoft.Band.Portable;
    using Microsoft.Band.Portable.Tiles;
    using Microsoft.Band.Portable.Tiles.Pages;
    using Microsoft.Band.Portable.Tiles.Pages.Data;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Diagnostics;

    public class AddTileViewModel : ViewModelBase
    {
        // Variables
        private Guid _tileId;
        private string _tileName;
        private BandImage _tileIcon;
        private BandImage _tileBadge;
        private bool _disableScreenTimeout;    
        private BandTileManager _tileManager;

        // Commands
        private ICommand _createTileCommand;


        public override async void OnAppearing(object navigationContext)
        {
            // Init
            BandClient = navigationContext as BandClient;
            _tileManager = BandClient.TileManager;

            TileId = Guid.NewGuid();
            TileName = "New Tile";
            TileIcon = await ResourcesHelper.LoadBandImageFromResourceAsync("Resources/tile.png");
            TileBadge = await ResourcesHelper.LoadBandImageFromResourceAsync("Resources/badge.png");
            DisableScreenTimeout = true;

            base.OnAppearing(navigationContext);
        }

        public Guid TileId
        {
            get { return _tileId; }
            set
            {
                _tileId = value;
                RaisePropertyChanged();
            }
        }

        public string TileName
        {
            get { return _tileName; }
            set
            {
                _tileName = value;
                RaisePropertyChanged();
            }
        }

        public BandImage TileIcon
        {
            get { return _tileIcon; }
            set
            {
                _tileIcon = value;
                RaisePropertyChanged();
            }
        }

        public BandImage TileBadge
        {
            get { return _tileBadge; }
            set
            {
                _tileBadge = value;
                RaisePropertyChanged();
            }
        }

        public bool DisableScreenTimeout
        {
            get { return _disableScreenTimeout; }
            set
            {
                _disableScreenTimeout = value;
                RaisePropertyChanged();
            }
        }

        public ICommand CreateTileCommand
        {
            get { return _createTileCommand = _createTileCommand ?? new DelegateCommandAsync(CreateTileCommandExecute); }
        }

        private async Task CreateTileCommandExecute()
        {
            try
            {
                // Create Tile
                var tile = new BandTile(TileId)
                {
                    Icon = TileIcon,
                    Name = TileName,
                    SmallIcon = TileBadge,
                    IsScreenTimeoutDisabled = DisableScreenTimeout
                };

                // Tile Custom Layouts
                var layouts = CreatePageLayouts();
                tile.PageLayouts.AddRange(layouts);

                // Add Tile
                await _tileManager.AddTileAsync(tile);

                // Update with page data
                var datas = CreatePageDatas();
                await _tileManager.SetTilePageDataAsync(tile.Id, datas);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private static PageLayout[] CreatePageLayouts()
        {
            return new[] {
                // Page index layout index 0 - BARCODES
                new PageLayout {
                    Root = new ScrollFlowPanel {
                        Rect = new PageRect(0, 0, 245, 105),
                        Orientation = FlowPanelOrientation.Vertical,
                        Elements = {

                            new TextBlock
                            {
                                ElementId = 11,
                                Rect = new PageRect(0, 0, 230, 30),
                                Color = new BandColor(255, 0, 0),
                                AutoWidth = false,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Bottom
                            },
                            new Barcode {
                                ElementId = 12,
                                Rect = new PageRect(0, 0, 230, 61),
                                BarcodeType = BarcodeType.Code39,
                            },
                            new TextBlock
                            {
                                ElementId = 13,
                                Rect = new PageRect(0, 0, 230, 30),
                                Color = new BandColor(255, 0, 0),
                                AutoWidth = false,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Bottom
                            },
                            new Barcode {
                                ElementId = 14,
                                Rect = new PageRect(0, 0, 230, 61),
                                BarcodeType = BarcodeType.Pdf417,
                            }
                        }
                    }
                },
                // Page layout index 1 - IMAGES
                new PageLayout {
                    Root = new FlowPanel {
                        Rect = new PageRect(15, 0, 245, 105),
                        Orientation = FlowPanelOrientation.Vertical,
                        Elements = {
                            new FlowPanel {
                                Rect = new PageRect(0, 0, 230, 105),
                                Orientation = FlowPanelOrientation.Horizontal,
                                Elements = {
                                    new Icon {
                                        ElementId = 21,
                                        Rect = new PageRect(0, 0, 100, 70),
                                        Color = new BandColor(127, 127, 0),
                                        VerticalAlignment = VerticalAlignment.Center,
                                        HorizontalAlignment = HorizontalAlignment.Center
                                    },
                                    new Icon {
                                        ElementId = 22,
                                        Rect = new PageRect(0, 0, 100, 70),
                                        Color = new BandColor(127, 0, 127),
                                        VerticalAlignment = VerticalAlignment.Center,
                                        HorizontalAlignment = HorizontalAlignment.Center
                                    }
                                }
                            }
                        }
                    }
                },
                // Page layout index 2 - BUTTONS
                new PageLayout {
                    Root = new ScrollFlowPanel {
                        Rect = new PageRect(0, 0, 245, 105),
                        Orientation = FlowPanelOrientation.Vertical,
                        Elements = {

                            new TextBlock
                            {
                                ElementId = 31,
                                Rect = new PageRect(0, 0, 230, 30),
                                Color = new BandColor(255, 0, 0),
                                AutoWidth = false,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Bottom
                            },
                            new TextButton {
                                ElementId = 32,
                                Rect = new PageRect(0, 0, 229, 43),
                                PressedColor = new BandColor(0, 127, 0)
                            },
                            new TextBlock
                            {
                                ElementId = 33,
                                Rect = new PageRect(0, 0, 230, 30),
                                Color = new BandColor(255, 0, 0),
                                AutoWidth = false,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Bottom
                            },
                            new FilledButton {
                                ElementId = 34,
                                Rect = new PageRect(0, 0, 229, 43),
                                BackgroundColor = new BandColor(0, 0, 127)
                            }
                        }
                    }
                }
            };
        }

        private static IEnumerable<PageData> CreatePageDatas()
        {
            return new[] {
                // Page data index 0 - BARCODES
                new PageData {
                    PageId = Guid.NewGuid(),
                    PageLayoutIndex = 0,
                    Data = {
                        new TextBlockData
                        {
                            ElementId = 11,
                            Text = "Code 39"
                        },
                        new BarcodeData {
                            ElementId = 12,
                            BarcodeType = BarcodeType.Code39,
                            BarcodeValue = "HELLO"
                        },
                        new TextBlockData
                        {
                            ElementId = 13,
                            Text = "Pdf 417"
                        },
                        new BarcodeData {
                            ElementId = 14,
                            BarcodeType = BarcodeType.Pdf417,
                            BarcodeValue = "0246810"
                        }
                    }
                },
                // Page data index 1 - IMAGES
                new PageData {
                    PageId = Guid.NewGuid(),
                    PageLayoutIndex = 1,
                    Data = {
                        new ImageData {
                            ElementId = 21,
                            ImageIndex = 0
                        },
                        new ImageData {
                            ElementId = 22,
                            ImageIndex = 1
                        }
                    }
                },
                // Page data index 2 - BUTTONS
                new PageData {
                    PageId = Guid.NewGuid(),
                    PageLayoutIndex = 2,
                    Data = {
                        new TextBlockData
                        {
                            ElementId = 31,
                            Text = "Button"
                        },
                        new TextButtonData {
                            ElementId = 32,
                            Text = "Press!"
                        },
                        new TextBlockData
                        {
                            ElementId = 33,
                            Text = "FilledButton"
                        },
                        new FilledButtonData {
                            ElementId = 34,
                            PressedColor = new BandColor(0, 127, 127),
                        }
                    }
                }
            };
        }

    }
}
