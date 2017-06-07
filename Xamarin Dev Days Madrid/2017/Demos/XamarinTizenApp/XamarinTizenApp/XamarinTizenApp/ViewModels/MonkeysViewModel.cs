using System.Collections.ObjectModel;
using XamarinTizenApp.Helpers;
using XamarinTizenApp.Models;

namespace XamarinTizenApp.ViewModels
{
    public class MonkeysViewModel
    {
        public ObservableCollection<Monkey> Monkeys { get; set; }
        public ObservableCollection<Grouping<string, Monkey>> MonkeysGrouped { get; set; }

        public MonkeysViewModel()
        {

            Monkeys = MonkeyHelper.Monkeys;
            MonkeysGrouped = MonkeyHelper.MonkeysGrouped;
        }

        public int MonkeyCount => Monkeys.Count;
    }
}