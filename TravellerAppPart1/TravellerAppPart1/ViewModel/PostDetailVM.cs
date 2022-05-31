using System;
using System.Collections.Generic;
using System.Text;
using TravellerAppPart1.Helpers;
using TravellerAppPart1.Model;
using Xamarin.Forms;

namespace TravellerAppPart1.ViewModel
{
    public class PostDetailVM
    {
        public Command DeleteCommand { get; set; }
        public Post SelectedPost { get; set; }

        public PostDetailVM()
        {
            DeleteCommand = new Command(Delete);
        }

        private async void Delete()
        {
            // Ctrl + K + C comment
            // Ctrl + K + U uncomment
            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Post>();
            //    int rows = conn.Delete(SelectedPost);
            //    if (rows > 0)
            //        DisplayAlert("Success", "Experience successfully deleted", "OK");
            //}
            bool result = await Firestore.Delete(SelectedPost);
            if (result) await App.Current.MainPage.Navigation.PopAsync();
            else App.Current.MainPage.DisplayAlert("Failure", "Try again", "ok");
        }
    }
}
