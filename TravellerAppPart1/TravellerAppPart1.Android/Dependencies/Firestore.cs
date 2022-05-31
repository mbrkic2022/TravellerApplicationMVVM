using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Java.Interop;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerAppPart1.Helpers;
using TravellerAppPart1.Model;
using Xamarin.Forms;

[assembly: Dependency(typeof(TravellerAppPart1.Droid.Dependencies.Firestore))]
namespace TravellerAppPart1.Droid.Dependencies
{
    public class Firestore : Java.Lang.Object, IFirestore, IOnCompleteListener
    {
        List<Post> posts;
        bool hasReadPosts = false;
        public Firestore()
        {
            posts = new List<Post>();
        }

        public async Task<bool> Delete(Post post)
            {
                try
                {
                    var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                    collection.Document(post.Id).Delete();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }


        public bool Insert(Post post)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>()
            {
                {"experience", post.Experience },
                {"country", post.Country },
                {"municipality", post.Municipality },
                {"address", post.Address },
                {"latitude", post.Latitude },
                {"longitude", post.Longitude },
                {"userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid }
            };
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Add(new HashMap(postDocument));
                return true;
            }
            catch(Exception)
            { 
                return false; 
            }
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
           if (task.IsSuccessful)
            {
                // više je načina za ovo odraditi jer collection trebamo vratiti iz read metode, a ne ove
                var documents = (QuerySnapshot)task.Result; // to je isti tip kojeg smo primili i na iOS-u, tj. querysnapshot
                posts.Clear();
                foreach(var doc in documents.Documents)
                {
                    Post newPost = new Post()
                    {
                        Experience = doc.Get("experience").ToString(),
                        Country = doc.Get("country").ToString(),
                        Address = doc.Get("address").ToString(),
                        Municipality = doc.Get("municipality").ToString(),
                        Latitude = (double)doc.Get("latitude"),
                        Longitude = (double)doc.Get("longitude"),
                        UserId = doc.Get("userId").ToString(),
                        Id = doc.Id
                    };
                    posts.Add(newPost);
                }
            }
            else 
            { 
                posts.Clear(); 
            }
            hasReadPosts = true;
        }

        public async Task<List<Post>> Read()
        {
            try
            {
                hasReadPosts = false;
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                var query = collection.WhereEqualTo("userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid); // možemo kopirat iz iOS-a i popravit
                query.Get().AddOnCompleteListener(this);
                for (int i = 0; i < 50; i++)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    // čekat ćemo 5 sekundi...to možemo promijeniti
                    if (hasReadPosts) break;
                }
                return posts;
            }
            catch (Exception)
            {
                return posts;
            }
        }


        public async Task<bool> Update(Post post)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>()
            {
                {"experience", post.Experience },
                {"country", post.Country },
                {"municipality", post.Municipality },
                {"address", post.Address },
                {"latitude", post.Latitude },
                {"longitude", post.Longitude },
                {"userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid }
            };
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Document(post.Id).Update(postDocument);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}