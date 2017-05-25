using System;
using Xamarin.Forms;

namespace TipCalc.ViewModel
{
    public class MainViewModel : BindableObject
    {
        private double _subTotal;
        private double _postTaxTotal;
        private double _tipPercent;
        private double _tipAmount;
        private double _total;

        public MainViewModel()
        {
            TipPercent = 5;
        }

        public double SubTotal
        {
            set
            {
                if (_subTotal != value)
                {
                    _subTotal = value;
                    OnPropertyChanged("SubTotal");
                    Recalculate();
                }
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
                if (_postTaxTotal != value)
                {
                    _postTaxTotal = value;
                    OnPropertyChanged("PostTaxTotal");
                    Recalculate();
                }
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
                if (_tipPercent != value)
                {
                    _tipPercent = value;
                    OnPropertyChanged("TipPercent");
                    Recalculate();
                }
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
                if (_tipAmount != value)
                {
                    _tipAmount = value;
                    OnPropertyChanged("TipAmount");
                }
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
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged("Total");
                }
            }
            get
            {
                return _total;
            }
        }

        void Recalculate()
        {
            TipAmount = Math.Round(TipPercent * SubTotal / 100, 2);

            Total = Math.Round(4 * (PostTaxTotal + TipAmount)) / 4;
        }
    }
}