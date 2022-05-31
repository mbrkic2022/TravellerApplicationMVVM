using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TravellerAppPart1.Helpers;
using System.Linq;
using System.ComponentModel;

namespace TravellerAppPart1.ViewModel
{
    public class ProfileVM : INotifyPropertyChanged
    {
        public ObservableCollection<CategoryCount> Categories { get; set; }
        private int postCount;
        public int PostCount { get
            {
                return postCount;
            }
            set
            {
                postCount = value;
                OnPropertyChanged("PostCount");
            }
                }
        public event PropertyChangedEventHandler PropertyChanged;
        public ProfileVM()
        {
            Categories = new ObservableCollection<CategoryCount>();
        }

        private void OnPropertyChanged (string propertyName)
        {
            PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
        }

        public async void GetPosts()
        {
            Categories.Clear();
            var posts = await Firestore.Read();
            var countries = (from p in posts
                             orderby p.Country
                             select p.Country).Distinct().ToList();
            foreach (var country in countries)
            {
                var count = (from post in posts
                             where post.Country == country
                             select post).ToList().Count();

                Categories.Add(new CategoryCount
                {
                    Name = country,
                    Count = count
                });
            }
            PostCount = posts.Count();
        }
    }

    public class CategoryCount
    {
        public object Name { get; set; }
        public object Count { get; set; }
    }
}
