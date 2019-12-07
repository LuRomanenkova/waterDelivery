using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace newWaterDelivery
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public struct Member
        {
            public string Name;
            public int Count;
            public string ImageUrl;
        }

        List<Member> Bucket = new List<Member>();

        private void Add_new_to_busket(string count, string name, string img_path, int index)
        {
            Image img = new Image
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 30,
                Source = img_path,
                HeightRequest = 30
            };

            Label name_drink = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Text = name
                //FontFamily = Device.RuntimePlatform == Device.Android ? "Lobster-Regular.ttf#Lobster-Regular" : "Assets/Fonts/Lobster-Regular.ttf#Lobster"
                //HeightRequest = 45,
                //WidthRequest = 230
            };

            Label numbers = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Text = count
                //FontFamily = Device.RuntimePlatform == Device.Android ? "Lobster-Regular.ttf#Lobster-Regular" : "Assets/Fonts/Lobster-Regular.ttf#Lobster"
                //HeightRequest = 45,
                //WidthRequest = 45
            };

            Button plus_drinks = new Button
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 40,
                WidthRequest = 30,
                CornerRadius = 5,
                Text = "+",
                BackgroundColor = Color.FromHex("#5F53A3"),
                TextColor = Color.FromHex("#F8F7FF"),
                BorderColor = Color.Black,
                BorderWidth = 1
                //FontFamily = Device.RuntimePlatform == Device.Android ? "Lobster-Regular.ttf#Lobster-Regular" : "Assets/Fonts/Lobster-Regular.ttf#Lobster"
            };

            plus_drinks.Clicked += (s, w) =>
            {
                numbers.Text = Convert.ToString(Convert.ToInt32(numbers.Text) + 1);
                Member a = Bucket[index];
                a.Count = Convert.ToInt32(numbers.Text);
                Bucket[index] = a;
            };

            Button minus_drinks = new Button
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 40,
                WidthRequest = 30,
                CornerRadius = 5,
                Text = "-",
                BackgroundColor = Color.FromHex("#5F53A3"),
                TextColor = Color.FromHex("#F8F7FF"),
                BorderColor = Color.Black,
                BorderWidth = 1
                //FontFamily = Device.RuntimePlatform == Device.Android ? "Lobster-Regular.ttf#Lobster-Regular" : "Assets/Fonts/Lobster-Regular.ttf#Lobster"
            };

           

            Button delete_drinks = new Button
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 40,
                WidthRequest = 40,
                CornerRadius = 10,
                Text = "x",
                BackgroundColor = Color.FromHex("#5F53A3"),
                TextColor = Color.FromHex("#F8F7FF"),
                BorderColor = Color.Black,
                BorderWidth = 1
                //FontFamily = Device.RuntimePlatform == Device.Android ? "Lobster-Regular.ttf#Lobster-Regular" : "Assets/Fonts/Lobster-Regular.ttf#Lobster"
            };

            Frame frame = new Frame()
            {
                Padding = 6,
                BackgroundColor = Color.FromHex("#C1C1FF")
            };

            StackLayout stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.FromHex("#C1C1FF")
            };

            stack.Children.Add(img);
            stack.Children.Add(name_drink);
            stack.Children.Add(numbers);
            stack.Children.Add(minus_drinks);
            stack.Children.Add(plus_drinks);
            stack.Children.Add(delete_drinks);

            frame.Content = stack;

            delete_drinks.Clicked += (s, w) =>
            {
                Stack.Children.Remove(frame);
                Bucket.RemoveAll(x => x.Name == name_drink.Text);
            };

            minus_drinks.Clicked += (s, w) =>
            {
                if (Convert.ToInt32(numbers.Text) - 1 > 0)
                {
                    numbers.Text = Convert.ToString(Convert.ToInt32(numbers.Text) - 1);

                    Member a = Bucket[index];
                    a.Count = Convert.ToInt32(numbers.Text);
                    Bucket[index] = a;
                }
                //else
                //{
                //    Stack.Children.Remove(frame);
                //    Bucket.RemoveAll(x => x.Name == name_drink.Text);
                //}
            };

            Stack.Children.Add(frame);
        }

        private void Add_new_drink(object sender, EventArgs e)
        {
            var page_select_drink = new SelectDrinkPage();
            page_select_drink.Disappearing += (s, _e) =>
            {
                if (SelectDrinkPage.is_confirmed)
                {
                    if (SelectDrinkPage.count_drinks != 0)
                    {
                        bool is_in = false;
                        if (Bucket.Count() == 0)
                        {
                            Member member;
                            member.Count = SelectDrinkPage.count_drinks;
                            member.Name = SelectDrinkPage.global_name;
                            member.ImageUrl = SelectDrinkPage.global_img;
                            is_in = true;
                            Bucket.Add(member);
                            SelectDrinkPage.count_drinks = 0;
                        }
                        else
                        {
                            var ind = Bucket.FindIndex(x => x.Name == SelectDrinkPage.global_name);
                            if (ind != -1)
                            {
                                Member member;
                                member.Name = Bucket[ind].Name;
                                member.Count = Bucket[ind].Count + SelectDrinkPage.count_drinks;
                                member.ImageUrl = Bucket[ind].ImageUrl;
                                Bucket[ind] = member;
                                is_in = true;
                            }
                            else
                            {
                                Member member;
                                member.Count = SelectDrinkPage.count_drinks;
                                member.Name = SelectDrinkPage.global_name;
                                member.ImageUrl = SelectDrinkPage.global_img;
                                is_in = true;
                                Bucket.Add(member);
                            }

                            SelectDrinkPage.count_drinks = 0;

                        }

                        if (is_in)
                        {
                            Stack.Children.Clear();
                            for (int i = 0; i < Bucket.Count(); i++)
                                Add_new_to_busket(Convert.ToString(Bucket[i].Count), Convert.ToString(Bucket[i].Name), Convert.ToString(Bucket[i].ImageUrl), i);
                        }
                    }
                }
            };


            Navigation.PushAsync(page_select_drink);

        }

        private async void Confirm_drinks(object sender, EventArgs e)
        {
            if (Bucket.Count() == 0)
            {
                await DisplayAlert("0_0 Opps 0_0", "Your basket is empty :( \r\n You should select smth", "Ok");
            }
            else
            {
                string purchase = "Your order:\r\n";
                for (var i = 0; i != Bucket.Count(); i++)
                {
                    purchase += $"{Bucket[i].Count} {Bucket[i].Name}\r\n";
                }

                var accept = await DisplayAlert("Do you want to confirm?", $"{purchase}","Yes", "No");

                if (accept)
                {
                    await DisplayAlert("Successfully!", "Your order will be delivered soon!", "Cool");
                    Stack.Children.Clear();
                    Bucket.Clear();
                }
            }
        }
    }
}
