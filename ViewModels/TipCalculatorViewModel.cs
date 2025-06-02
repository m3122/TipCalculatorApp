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

        public event PropertyChangedEventHandler PropertyChanged;

        public TipCalculatorViewModel()
        {
            TotalBill = 0;
            TipPercent = 10;
            SliderTipPercent = 0;
            PeopleCount = 1;
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
                    OnPropertyChanged();
                    SliderTipPercent = value; // sincroniza slider con botón
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
                    sliderTipPercent = value;
                    OnPropertyChanged();
                    tipPercent = (int)sliderTipPercent; // sincroniza botón con slider
                    OnPropertyChanged(nameof(TipPercent));
                    Recalculate();
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
            TipPerPerson = PeopleCount == 0 ? 0 : (TotalBill * TipPercent / 100) / PeopleCount;
            TotalPerPerson = PeopleCount == 0 ? 0 : TotalBill / PeopleCount;
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



