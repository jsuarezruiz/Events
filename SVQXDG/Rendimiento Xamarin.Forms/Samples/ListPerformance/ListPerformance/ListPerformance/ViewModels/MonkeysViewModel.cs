using ListPerformance.Models;
using ListPerformance.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ListPerformance.ViewModels
{
    public class MonkeysViewModel : BindableObject
    {
        public MonkeysViewModel()
        {
            Monkeys = MonkeyService.Instance.GetMonkeys();
        }

        public ObservableCollection<Monkey> Monkeys { get; set; }
    }
}
