using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TravellerAppPart1.Helpers;
using TravellerAppPart1.Model;

namespace TravellerAppPart1.ViewModel
{
    public class HistoryVM
    {
        public ObservableCollection<Post> Posts{ get; set; }

        private Post selectedPost;
        public Post SelectedPost { 
            get
            {
                return selectedPost;
            }
            set
            {
                selectedPost = value;
                if (selectedPost != null) 
                    App.Current.MainPage.Navigation.PushAsync(new PostDetailPage(selectedPost));
            } 
        }

        public HistoryVM()
        {
            Posts = new ObservableCollection<Post>();
        }

        public async void GetPosts()
        {
            Posts.Clear();
            var posts = await Firestore.Read();
            foreach (var post in posts)
            {
                Posts.Add(post);
            }
        }
    }
}
