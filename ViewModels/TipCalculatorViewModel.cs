using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace TipCalculatorApp.ViewModels
{
    public class TipCalculatorViewModel : INotifyPropertyChanged
    {
        public decimal totalBill;
        private int tipPercent;
        private int peopleCount;
        private decimal sliderTipPercent;
        private bool _sliderHasPriority; // para evitar sincronización inicial

        public event PropertyChangedEventHandler PropertyChanged;

        public TipCalculatorViewModel()
        {
            TotalBill = 0;
            TipPercent = 10;
            SliderTipPercent = 0;
            PeopleCount = 1;
            _sliderHasPriority = false; // para evitar sincronización inicial
        }

        public decimal TotalBill
        {
            get => totalBill;
            set
            {
                if (totalBill != value)
                {
                    totalBill = value;
                    OnPropertyChanged();
                    Recalculate();
                }
            }
        }

        public int TipPercent
        {
            get => tipPercent;
            set
            {
                if (tipPercent != value)
                {
                    tipPercent = value;
                    if (!_sliderHasPriority)
                        SliderTipPercent = value; // solo sincroniza si no viene del slider
                    OnPropertyChanged();
                    Recalculate();
                }
            }
        }

        public decimal SliderTipPercent
        {
            get => sliderTipPercent;
            set
            {
                if (sliderTipPercent != value)
                {
                    _sliderHasPriority = true;
                    sliderTipPercent = value;
                    tipPercent = (int)sliderTipPercent;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TipPercent));
                    Recalculate();
                    _sliderHasPriority = false;
                }
            }
        }


        public int PeopleCount
        {
            get => peopleCount;
            set
            {
                if (value < 1) value = 1; // no menos de 1 persona
                if (peopleCount != value)
                {
                    peopleCount = value;
                    OnPropertyChanged();
                    Recalculate();
                }
            }
        }

        private decimal totalWithTip;
        public decimal TotalWithTip
        {
            get => totalWithTip;
            private set
            {
                if (totalWithTip != value)
                {
                    totalWithTip = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal tipPerPerson;
        public decimal TipPerPerson
        {
            get => tipPerPerson;
            private set
            {
                if (tipPerPerson != value)
                {
                    tipPerPerson = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal subtotalPerPerson;
        public decimal SubtotalPerPerson
        {
            get => subtotalPerPerson;
            private set
            {
                if (subtotalPerPerson != value)
                {
                    subtotalPerPerson = value;
                    OnPropertyChanged();
                }
            }
        }


        private decimal totalPerPerson;
        public decimal TotalPerPerson
        {
            get => totalPerPerson;
            private set
            {
                if (totalPerPerson != value)
                {
                    totalPerPerson = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Recalculate()
        {
            decimal tipAmount = TotalBill * tipPercent / 100m;
            TotalWithTip = TotalBill + tipAmount;

            if (PeopleCount > 0)
            {
                TipPerPerson = tipAmount / PeopleCount;
                SubtotalPerPerson = TotalBill / PeopleCount;
                TotalPerPerson = TotalWithTip / PeopleCount;
            }
            else
            {
                TipPerPerson = 0;
                SubtotalPerPerson = 0;
                TotalPerPerson = 0;
            }
        }



        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ICommand SetTipCommand => new Command<string>(param =>
        {
            if (int.TryParse(param, out int val))
            {
                TipPercent = val;
            }
        });

        public ICommand IncreasePeopleCommand => new Command(() => PeopleCount++);
        public ICommand DecreasePeopleCommand => new Command(() =>
        {
            if (PeopleCount > 1)
                PeopleCount--;
        });

    }
}



