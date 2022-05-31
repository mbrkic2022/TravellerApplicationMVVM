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
    public partial class PostDetailPage : ContentPage
    {
        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();
            experienceEntry.Text = selectedPost.Experience;
            (Resources["vm"] as PostDetailVM).SelectedPost = selectedPost;
        }

    }
}