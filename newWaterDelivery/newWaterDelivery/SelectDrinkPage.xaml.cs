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
        Label global_label = new Label();
        Stepper global_stepper = new Stepper();
        Button global_button = new Button();
        public int count_drinks = 0;
        
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

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StackLayoutMap.Children.Contains(global_label) &&
                StackLayoutMap.Children.Contains(global_stepper) && 
                StackLayoutMap.Children.Contains(global_button))
            {
                StackLayoutMap.Children.Remove(global_stepper);
                StackLayoutMap.Children.Remove(global_label);
                StackLayoutMap.Children.Remove(global_button);
                count_drinks = 0;
            }

            Stepper stepper = new Stepper
            {
                Value = 0,
                Minimum = 0,
                Maximum = 10,
                Increment = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            stepper.ValueChanged += OnStepperValueChanged;


            Binding binding = new Binding { Source = stepper, Path = "Value" };
            // установка привязки для свойства TextProperty
            Label label = new Label
            {
                Text = "",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            label.SetBinding(Label.TextProperty, binding);
            global_label = label;
            global_stepper = stepper;

            StackLayoutMap.Children.Add(stepper);
            StackLayoutMap.Children.Add(label);

            Button button = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Margin = 10,
                HeightRequest = 40,
                WidthRequest = 100,
                CornerRadius = 20,
                Text = "Confirm",
                BackgroundColor = Color.CadetBlue
            };

            button.Clicked += OnButtonClicked;
            StackLayoutMap.Children.Add(button);
            global_button = button;
        }

        void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            global_label.Text = String.Format("{0}", e.NewValue);
            count_drinks = Convert.ToInt32(e.NewValue);
        }
        void OnButtonClicked(object sender, System.EventArgs e)
        {
            if (count_drinks > 0)
            {
                Navigation.PopAsync();
            }
        }

    }
       
}