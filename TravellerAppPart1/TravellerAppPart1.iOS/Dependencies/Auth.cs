using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerAppPart1.Helpers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(TravellerAppPart1.iOS.Dependencies.Auth))]
namespace TravellerAppPart1.iOS.Dependencies
{
    public class Auth : IAuth
    {
        public string GetCurrentUserId()
        {
            return Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid;
        }

        public bool IsAuthenticated()
        {
            return Firebase.Auth.Auth.DefaultInstance.CurrentUser != null;
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                await Firebase.Auth.Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return true;
            }
            catch (NSErrorException error)
            {
                throw new Exception(error.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an unknown error!");
            }
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                await Firebase.Auth.Auth.DefaultInstance.CreateUserAsync(email, password);
                return true;
            }
            catch (NSErrorException error)
            {
                string message = error.Message.Substring(error.Message.IndexOf("NSLocalizedDescription=", StringComparison.CurrentCulture));
                message = message.Replace("NSLocalizedDescription=", "").Split('.')[0];
                throw new Exception(error.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an unknown error!");
            }
        }
    }
}