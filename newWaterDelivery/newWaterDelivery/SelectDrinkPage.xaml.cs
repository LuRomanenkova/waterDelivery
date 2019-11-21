using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace newWaterDelivery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectDrinkPage : ContentPage
    {
        public SelectDrinkPage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModelBase
            {
                Drinks = InitDrinkData()
            };
        }

        private List<Drink> InitDrinkData()
        {
            var Drinks = new List<Drink>();

            Drinks.Add(
                new Drink
                {
                    Name = "Non-carbonated water",
                    ImageUrl = "Noncorb.png"

                });

            Drinks.Add(
               new Drink
               {
                   Name = "Carbonated water",
                   ImageUrl = "Corb.png"

               });

            Drinks.Add(
               new Drink
               {
                   Name = "Grink",
                   ImageUrl = "Grink.jpg"

               });

            Drinks.Add(
              new Drink
              {
                  Name = "Cola",
                  ImageUrl = "Cola.jpg"

              });

            return Drinks;
        }

        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            
        
        
        }
    }
}