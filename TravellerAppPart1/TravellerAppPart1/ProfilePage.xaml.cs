using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerAppPart1.Helpers;
using TravellerAppPart1.Model;
using TravellerAppPart1.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravellerAppPart1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            (Resources["vm"] as ProfileVM).GetPosts(); // ADDED --> poziv


            //           using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //           {
            //               var postTable = conn.Table<Post>().ToList();
            //            var postTable = await Firestore.Read();


            //           countriesListView.ItemsSource = countriesCount;
            //           postCountLabel.Text = postTable.Count.ToString(); 
        }
        // }
    }
}