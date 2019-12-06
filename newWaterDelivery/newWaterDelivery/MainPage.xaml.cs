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

        private void Add_new_to_busket(string count, string name, string img_path)
        {
            Image img = new Image
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 40,
                Source = img_path,
                HeightRequest = 40
            };

            Label name_drink = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Text = name,
                //HeightRequest = 45,
                //WidthRequest = 230
            };

            Label numbers = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Text = count,
                //HeightRequest = 45,
                //WidthRequest = 45
            };


            Button delete_drinks = new Button
            {
                //Margin = 10,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 40,
                WidthRequest = 40,
                CornerRadius = 10,
                Text = "x",
                BackgroundColor = Color.CadetBlue,
                BorderColor = Color.Black,
                BorderWidth = 1
            };

            Frame frame = new Frame()
            {
                Padding = 6
            };

            StackLayout stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            stack.Children.Add(img);
            stack.Children.Add(name_drink);
            stack.Children.Add(numbers);
            stack.Children.Add(delete_drinks);

            frame.Content = stack;

            //Grid grid = new Grid();
            //grid.Children.Add(img, 0, 0);
            //grid.Children.Add(name_drink, 1, 0);
            //grid.Children.Add(numbers, 2, 0);
            //grid.Children.Add(delete_drinks, 3, 0);

            delete_drinks.Clicked += (s, w) =>
            {
                Stack.Children.Remove(frame);
                Bucket.RemoveAll(x => x.Name == name_drink.Text);
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
                                Add_new_to_busket(Convert.ToString(Bucket[i].Count), Convert.ToString(Bucket[i].Name), Convert.ToString(Bucket[i].ImageUrl));
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
                string purchase = "Your order:\r\n ";
                for (var i = 0; i != Bucket.Count(); i++)
                {
                    purchase += $"{Bucket[i].Count} {Bucket[i].Name};\r\n";
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
