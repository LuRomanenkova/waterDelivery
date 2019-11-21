using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace newWaterDelivery
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        List<Drink> drinks;
        public List<Drink> Drinks
        {
            get { return drinks; }
            set
            {
                if (drinks != value)
                {
                    drinks = value;
                    OnPropertyChanged();
                }
            }
        }

        Drink selectedDrink;
        public Drink SelectedDrink
        {
            get { return selectedDrink; }
            set
            {
                if (selectedDrink != value)
                {
                    selectedDrink = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

