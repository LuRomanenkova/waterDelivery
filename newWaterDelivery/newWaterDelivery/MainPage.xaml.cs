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
                Source = img_path
            };

            Label name_drink = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Text = name
            };

            Label numbers = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Text = count
            };


            Button delete_drinks = new Button
            {
                //Margin = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 40,
                WidthRequest = 40,
                CornerRadius = 10,
                Text = "-",
                BackgroundColor = Color.CadetBlue
            };

            Grid grid = new Grid();
            grid.Children.Add(img, 0, 0);
            grid.Children.Add(name_drink, 1, 0);
            grid.Children.Add(numbers, 2, 0);
            grid.Children.Add(delete_drinks, 3, 0);

            delete_drinks.Clicked += (s, w) =>
            {
                Stack.Children.Remove(grid);
                Bucket.RemoveAll(x => x.Name == name_drink.Text);
            };

            Stack.Children.Add(grid);

        }

        private void Add_new_drink(object sender, EventArgs e)
        {
            var page_select_drink = new SelectDrinkPage();
            page_select_drink.Disappearing += (s, _e) =>
            {
                if (SelectDrinkPage.is_confirmed)
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
                        }
                        
                    }

                    if (is_in)
                    {
                        Stack.Children.Clear();
                        for (int i = 0; i < Bucket.Count(); i++)
                            Add_new_to_busket(Convert.ToString(Bucket[i].Count), Convert.ToString(Bucket[i].Name), Convert.ToString(Bucket[i].ImageUrl));
                    }
                    
                }
            };


            Navigation.PushAsync(page_select_drink);

        }
    }
}
