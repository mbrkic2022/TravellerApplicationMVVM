
﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravellerAppPart1.Helpers;

namespace TravellerAppPart1.Model
{

        public class Summary
        {
            public int queryTime { get; set; }
            public int numResults { get; set; }
        }

        public class BoundingBox
        {
            public string northEast { get; set; }
            public string southWest { get; set; }
            public string entity { get; set; }
        }

        public class AddressDetails
        {
            public string buildingNumber { get; set; }
            public string streetNumber { get; set; }
            public IList<object> routeNumbers { get; set; }
            public string street { get; set; }
            public string streetName { get; set; }
            public string streetNameAndNumber { get; set; }
            public string countryCode { get; set; }
            public string countrySubdivision { get; set; }
            public string countrySecondarySubdivision { get; set; }
            public string municipality { get; set; }
            public string postalCode { get; set; }
            public string country { get; set; }
            public string countryCodeISO3 { get; set; }
            public string freeformAddress { get; set; }
            public BoundingBox boundingBox { get; set; }
            public string extendedPostalCode { get; set; }
            public string countrySubdivisionName { get; set; }
            public string localName { get; set; }
        }

        public class Address
        {
            public AddressDetails address { get; set; }
            public string position { get; set; }
        }

        public class Location
        {
            public Summary summary { get; set; }
            public List<Address> addresses { get; set; }

            public static string GenerateUrl(double lat, double lon)
            {
                var position = String.Format("{0},{1}", lat, lon);
                return string.Format(Constants.LOCATION_SEARCH, Constants.BASE_URL, Constants.VERSION_NUM, position, Constants.API_KEY);
            }

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