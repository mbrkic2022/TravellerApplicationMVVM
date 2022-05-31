using System;
using System.Collections.Generic;
using System.Text;

namespace TravellerAppPart1.Model
{
    public class Post
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Experience { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public string Municipality { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
