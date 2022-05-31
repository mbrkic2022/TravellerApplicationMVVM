using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerAppPart1.Helpers;
using TravellerAppPart1.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravellerAppPart1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        IGeolocator locator = CrossGeolocator.Current;
        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetLocation();
            GetPosts();
        }

        private async void GetPosts()
        {
            /*           using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                       {
                           conn.CreateTable<Post>();
                           var posts = conn.Table<Post>().ToList();
                           DisplayOnMap(posts);
                       }*/

            var posts = await Firestore.Read();
            DisplayOnMap(posts);
        }

        private void DisplayOnMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var pinCoordinates = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);
                    var pin = new Pin()
                    {
                        Position = pinCoordinates,
                        Label = post.Municipality,
                        Address = post.Address,
                        Type = PinType.SavedPin
                    };

                    locationsMap.Pins.Add(pin);
                }
                catch(NullReferenceException nre) { }
                catch (Exception ex) { }
        }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            locator.StopListeningAsync();
        }

        private async void GetLocation()
        {
            var status = await CheckAndRequestLocationPermission();

            if (status == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();

                locator.PositionChanged += Locator_PositionChanged;
                if (locator!=null && locator.IsListening!=true) await locator.StartListeningAsync(new TimeSpan(0,1,0), 10);
                locationsMap.IsShowingUser = true;

                CenterMap(location.Latitude, location.Longitude);
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            CenterMap(e.Position.Latitude, e.Position.Longitude);
        }

        private void CenterMap(double latitude, double longitude)
        {
            Xamarin.Forms.Maps.Position Center = new Xamarin.Forms.Maps.Position(latitude, longitude);
            MapSpan span = new MapSpan(Center, 1, 1);
            locationsMap.MoveToRegion(span);
        }

        private async Task<PermissionStatus>CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
                return status;
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }
    }
}