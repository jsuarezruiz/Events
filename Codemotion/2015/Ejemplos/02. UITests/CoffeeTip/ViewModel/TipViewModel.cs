using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoffeeTip.ViewModel
{
    public class TipViewModel : BaseViewModel
    {
        public TipViewModel()
        {
            UpdateTip();
        }

        int drinkType;
        public int DrinkType
        {
            get { return drinkType; }
            set
            {
                if(SetProperty(ref drinkType, value))
                    UpdateTip();
            }
        }

        bool tamperedEspresso;
        public bool Tampered
        {
            get { return tamperedEspresso; }
            set
            {
                if(SetProperty(ref tamperedEspresso, value))
                    UpdateTip();
            }
        }

        bool atStarbucks;
        public bool AtStarbucks
        {
            get { return atStarbucks; }
            set
            {
                if(SetProperty(ref atStarbucks, value))
                    UpdateTip();
            }
        }

        decimal subTotal = 2.50M;
        public decimal SubTotal 
        {
            get { return subTotal; }
            set 
            {
                
                if(SetProperty(ref subTotal, value))
                    UpdateTip();

                value = Math.Round(value, 2);
                if(SetProperty(ref subTotal, value))
                    UpdateTip();
                
            }
        }

        decimal tipAmount;
        public decimal TipAmount 
        {
            get { return tipAmount; }
            private set 
            {
                if(SetProperty(ref tipAmount, value))
                    UpdateTip();
            }
        }

        decimal total;
        public decimal Total 
        {
            get { return total; }
            private set 
            {
                if(SetProperty(ref total, value))
                    UpdateTip();
            }
        }



        void UpdateTip()
        {
            if (AtStarbucks)
            {
                TipAmount = 0;
            }
            else
            {
                switch (DrinkType)
                {
                    case 0:
                        TipAmount = .5M;
                        break;
                    case 1: 
                        TipAmount = 1.0M;
                        break;
                    case 2:
                        if (Tampered)
                            TipAmount = .5M;
                        else
                            TipAmount = 0M;
                        break;
                    case 3:
                        if (Tampered)
                            TipAmount = 1.0M;
                        else
                            TipAmount = .5M;
                        break;
                }
            }

            Total = SubTotal + TipAmount;
        }


        ICommand resetCommand;
        public ICommand ResetCommand
        {
            get { 
                return resetCommand ?? (resetCommand = new Command(()=>
                    {
                        SubTotal = 2.5M;
                        AtStarbucks = false;
                        DrinkType = 0;
                        Tampered = false;
                    }));
                }
        }
    }
}

