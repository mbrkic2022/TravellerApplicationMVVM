using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerAppPart1.Helpers;
using TravellerAppPart1.Model;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(TravellerAppPart1.iOS.Dependencies.Firestore))]
namespace TravellerAppPart1.iOS.Dependencies
{
    public class Firestore : IFirestore
    {
        public async Task<bool> Delete(Post post)
        {
            try
            {
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
                await collection.GetDocument(post.Id).DeleteDocumentAsync();
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
                var keys = new NSString[]
                {
                new NSString("id"),
                new NSString("experience"),
                new NSString("country"),
                new NSString("municipality"),
                new NSString("address"),
                new NSString("latitude"),
                new NSString("longitude")
                };
                var values = new NSObject[]
                {
                    new NSString(post.Id),
                    new NSString(post.Experience),
                    new NSString(post.Country),
                    new NSString(post.Municipality),
                    new NSString(post.Address),
                    new NSNumber(post.Latitude),
                    new NSNumber(post.Longitude)
                };
                var document = new NSDictionary<NSString, NSObject>(keys, values);
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
                collection.AddDocument(document);
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<List<Post>> Read()
        {
            try
            {
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
                var query = collection.WhereEqualsTo("userId", Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid);
                var documents = await query.GetDocumentsAsync();
                List<Post> posts = new List<Post>();
                foreach (var doc in documents.Documents)
                {
                    var dictionary = doc.Data;
                    var newPost = new Post()
                    {
                        Experience = dictionary.ValueForKey(new NSString("experience")) as NSString,
                        Country = dictionary.ValueForKey(new NSString("country")) as NSString,
                        Municipality = dictionary.ValueForKey(new NSString("municipality")) as NSString,
                        Latitude = (double)(dictionary.ValueForKey(new NSString("latitude")) as NSNumber),
                        Longitude = (double)(dictionary.ValueForKey(new NSString("longitude")) as NSNumber),
                        Address = dictionary.ValueForKey(new NSString("address")) as NSString,
                        Id = doc.Id
                    };
                    posts.Add(newPost);
                }
                return posts;

            }
            catch (Exception)
            {
                return new List<Post>();
            }
        }

        public async Task<bool> Update(Post post)
        {
            try
            {
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
                var keys = new NSString[]
                {
                new NSString("id"),
                new NSString("experience"),
                new NSString("country"),
                new NSString("municipality"),
                new NSString("address"),
                new NSString("latitude"),
                new NSString("longitude")
                };
                var values = new NSObject[]
                {
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid),
                    new NSString(post.Experience),
                    new NSString(post.Country),
                    new NSString(post.Municipality),
                    new NSString(post.Address),
                    new NSNumber(post.Latitude),
                    new NSNumber(post.Longitude)
                };
                var document = new NSDictionary<NSObject, NSObject>(keys, values);
                await collection.GetDocument("post.Id").UpdateDataAsync(document);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}