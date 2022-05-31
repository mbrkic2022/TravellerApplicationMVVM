using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravellerAppPart1.Helpers;
using TravellerAppPart1.Model;

namespace TravellerAppPart1.Logic
{
    public class LocationLogic
    {
        public async static Task<List<Address>> GetLocation(double latitiude, double longitude)
        {
            List<Address> addresses = new List<Address>();

            var url = Location.GenerateUrl(latitiude, longitude);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var address = JsonConvert.DeserializeObject<Location>(json);
                addresses = address.addresses;
            }
            return addresses;

        }

    }
}
