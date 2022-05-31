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
    public partial class HistoryPage : ContentPage
    {
        private HistoryVM vm;
        public HistoryPage()
        {
            InitializeComponent();
            vm = Resources["vm"] as HistoryVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            /*           using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                       {
                           conn.CreateTable<Post>();
                           var posts = conn.Table<Post>().ToList();
                           postListView.ItemsSource = posts;
                       }*/
            vm.GetPosts();
        }

    }
}