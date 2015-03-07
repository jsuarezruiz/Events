using System;
using TipCalc.ViewModels.Base;

namespace TipCalc.ViewModels
{
    public class TipCalcViewModel : ViewModelBase
    {
        private double _subTotal, _postTaxTotal, _tipPercent, _tipAmount, _total;

        public double SubTotal
        {
            set
            {
                _subTotal = value;
                RaisePropertyChanged();
                Recalculate();
            }
            get { return _subTotal; }
        }

        public double PostTaxTotal
        {
            set
            {
                _postTaxTotal = value;
                RaisePropertyChanged();
                Recalculate();
            }
            get { return _postTaxTotal; }
        }

        public double TipPercent
        {
            set
            {
                _tipPercent = value;
                RaisePropertyChanged();
                Recalculate();
            }
            get { return _tipPercent; }
        }

        public double TipAmount
        {
            set
            {
                _tipAmount = value;
                RaisePropertyChanged();
            }
            get { return _tipAmount; }
        }

        public double Total
        {
            set
            {
                _total = value;
                RaisePropertyChanged();
            }
            get { return _total; }
        }

        private void Recalculate()
        {
            TipAmount = Math.Round(TipPercent * SubTotal / 100, 2);

            // Round total to nearest quarter.
            Total = Math.Round(4 * (PostTaxTotal + TipAmount)) / 4;
        }
    }
}