using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerAppPart1.Helpers;
using TravellerAppPart1.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravellerAppPart1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        Plugin.Geolocator.Abstractions.Position position;
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var locator = CrossGeolocator.Current;
            position = await locator.GetPositionAsync();
            var location = await Location.GetLocation(position.Latitude, position.Longitude);
            locationListView.ItemsSource = location;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedLocation = locationListView.SelectedItem as Address;
                var firstCategory = selectedLocation.address;
                Post post = new Post()
                {
                    Experience = experienceEntry.Text,
                    Address = firstCategory.freeformAddress,
                    Country = firstCategory.country,
                    Municipality = firstCategory.municipality,
                    Longitude = position.Longitude,
                    Latitude = position.Latitude
                };
                /*               using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                               {
                                   conn.CreateTable<Post>();
                                   int rows = conn.Insert(post);
                                   if (rows > 0)
                                       DisplayAlert("Success", "Experience successfully inserted", "OK");
                               }*/
                bool result = Firestore.Insert(post);
                if (result)
                {
                    experienceEntry.Text = "";
                    DisplayAlert("Success", "Experience successfully inserted", "OK");
                }
                else
                    DisplayAlert("Failure", "Experience not inserted, please try again", "OK");
            }
            catch (Exception ex) 
            {
            }
        }
    }
}