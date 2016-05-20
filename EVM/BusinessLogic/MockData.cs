using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public class MockData
    {
        public MockData()
        {
            PopulateData();
        }

        public List<Artist> Artists = new List<Artist>();
        public List<Location> Locations = new List<Location>();

        public void PopulateData()
        {
            PopulateArtist();
            PopulateLocation();
        }

        public void PopulateArtist()
        {
            Artist artist1 = new Artist
            {
                ArtistId = 1,
                Name = "Martin Garrix",
                DtAdded = DateTime.UtcNow,
                FacebookUrl = "https://facebook.com/martin.garrix/",
                Status = "Active"
            };

            Artists.Add(artist1);

            Artist artist2 = new Artist
            {
                ArtistId = 2,
                Name = "Calvin Harris",
                DtAdded = DateTime.UtcNow,
                FacebookUrl = "https://facebook.com/calvinharris/",
                Status = "Active"
            };

            Artists.Add(artist2);
        }
        public void PopulateLocation()
        {
            Location location1 = new Location
            {
                LocationId = 1,
                Name = "L'aventure du sucre"
            };
            Locations.Add(location1);
            Location location2 = new Location
            {
                LocationId = 2,
                Name = "J & J Auditorium"
            };
            Locations.Add(location2);
        }
    }
}