using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Touch.Views;
using Foundation;
using MVVMCross.Montaditos.ViewModels;
using ObjCRuntime;
using UIKit;

namespace MVVMCross.Montaditos.Iphone.Views
{
    [Register("FirstView")]
    public class FirstView : MvxTableViewController
    {
        public override void ViewDidLoad()
        {
            View = new UIView { BackgroundColor = UIColor.White };
            base.ViewDidLoad();

            // ios7 layout
            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
            {
                EdgesForExtendedLayout = UIRectEdge.None;
            }

            var source = new MvxStandardTableViewSource(TableView, "TitleText Name;ImageUrl Image");
            TableView.Source = source;

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            set.Bind(source).To(vm => vm.Montaditos);
            set.Apply();

            TableView.ReloadData();
        }
    }
}