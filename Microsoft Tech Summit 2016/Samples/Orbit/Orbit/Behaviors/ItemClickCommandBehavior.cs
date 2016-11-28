using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Orbit.Behaviors
{
    public static class ItemClickCommandBehavior
    {
        public static readonly DependencyProperty ItemClickCommandCommandProperty =
            DependencyProperty.RegisterAttached("ItemClickCommand", typeof(ICommand),
            typeof(ItemClickCommandBehavior), new PropertyMetadata(null, OnItemClickCommandPropertyChanged));

        public static void SetItemClickCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(ItemClickCommandCommandProperty, value);
        }

        public static ICommand GetItemClickCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(ItemClickCommandCommandProperty);
        }

        private static void OnItemClickCommandPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = d as ListViewBase;
            if (control != null)
                control.ItemClick += OnItemClick;
        }

        private static void OnItemClick(object sender, ItemClickEventArgs e)
        {
            var control = sender as ListViewBase;
            var itemClickCommand = GetItemClickCommand(control);

            if (itemClickCommand != null && itemClickCommand.CanExecute(e.ClickedItem))
                itemClickCommand.Execute(e.ClickedItem);
        }
    }
}