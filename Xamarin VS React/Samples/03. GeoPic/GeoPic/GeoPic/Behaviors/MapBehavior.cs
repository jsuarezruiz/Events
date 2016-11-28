using GeoPic.Helpers;
using GeoPic.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GeoPic.Behaviors
{
    public class MapBehavior : BindableBehavior<Map>
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<MapBehavior, IEnumerable<PhotoCommand>>(
            p => p.ItemsSource, null, BindingMode.Default, null, ItemsSourceChanged);

        public IEnumerable<PhotoCommand> ItemsSource
        {
            get { return (IEnumerable<PhotoCommand>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void ItemsSourceChanged(BindableObject bindable, IEnumerable oldValue, IEnumerable newValue)
        {
            var behavior = bindable as MapBehavior;
            if (behavior == null) return;
            behavior.AddPins();
        }

        private void AddPins()
        {
            var map = AssociatedObject;
            for (int i = map.Pins.Count - 1; i >= 0; i--)
            {
                map.Pins[i].Clicked -= PinOnClicked;
                map.Pins.RemoveAt(i);
            }

            var pins = ItemsSource.Select(x =>
            {
                var pin = new Pin
                {
                    Type = PinType.SearchResult,
                    Position = new Position(x.Latitude, x.Longitude),
                    Label = string.Format("{0}, {1}", x.Latitude, x.Longitude)
                };

                pin.Clicked += PinOnClicked;
                return pin;
            }).ToArray();

            foreach (var pin in pins)
                map.Pins.Add(pin);

            PositionMap();
        }

        private void PinOnClicked(object sender, EventArgs eventArgs)
        {
            var pin = sender as Pin;
            if (pin == null) return;
            var viewModel = ItemsSource
                .FirstOrDefault(x => string.Format("{0}, {1}", x.Latitude, x.Longitude) == pin.Label);

            if (viewModel == null)
                return;

            viewModel.Command.Execute(viewModel);
        }

        private void PositionMap()
        {
            if (ItemsSource == null || !ItemsSource.Any()) return;

            var centerPosition = new Position(ItemsSource.Average(x => x.Latitude), ItemsSource.Average(x => x.Longitude));

            var minLongitude = ItemsSource.Min(x => x.Longitude);
            var minLatitude = ItemsSource.Min(x => x.Latitude);

            var maxLongitude = ItemsSource.Max(x => x.Longitude);
            var maxLatitude = ItemsSource.Max(x => x.Latitude);

            var distance = MapHelper.CalculateDistance(minLatitude, minLongitude,
                maxLatitude, maxLongitude, 'M') / 2;

            AssociatedObject.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromMiles(distance)));

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                AssociatedObject.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromMiles(distance)));
                return false;
            });
        }
    }
}