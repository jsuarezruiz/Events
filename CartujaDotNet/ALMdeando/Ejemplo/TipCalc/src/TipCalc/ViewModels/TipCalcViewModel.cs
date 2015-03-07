using System;
using TipCalc.ViewModels.Base;

namespace TipCalc.ViewModels
{
    public class TipCalcViewModel : ViewModelBase
    {
        double _subTotal, _postTaxTotal, _tipPercent, _tipAmount, _total;

        public double SubTotal
        {
            set
            {
                _subTotal = value;
                OnPropertyChanged("SubTotal");
                Recalculate();
            }
            get
            {
                return _subTotal;
            }
        }

        public double PostTaxTotal
        {
            set
            {
                _postTaxTotal = value;
                OnPropertyChanged("PostTaxTotal");
                Recalculate();
            }
            get
            {
                return _postTaxTotal;
            }
        }

        public double TipPercent
        {
            set
            {
                _tipPercent = value;
                OnPropertyChanged("TipPercent");
                Recalculate();
            }
            get
            {
                return _tipPercent;
            }
        }

        public double TipAmount
        {
            set
            {
                _tipAmount = value;
                OnPropertyChanged("TipAmount");
            }
            get
            {
                return _tipAmount;
            }
        }

        public double Total
        {
            set
            {
                _total = value;
                OnPropertyChanged("Total");
            }
            get
            {
                return _total;
            }
        }

        void Recalculate()
        {
            this.TipAmount = Math.Round(this.TipPercent * this.SubTotal / 100, 2);

            // Round total to nearest quarter.
            this.Total = Math.Round(4 * (this.PostTaxTotal + this.TipAmount)) / 4;
        }
    }
}