using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public class ArtistEventRepo : IArtistEventRepo
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public List<Artist> StoreArtistForEvent(Artist item)
        {
            List<Artist> artistList = null;
            artistList.Add(item);

            return artistList;
        }
        public List<Artist> SaveArtistInEvent(List<Artist> items, int eventId)
        {
            if (items.Count < 1)
                return null;

            foreach (var item in items)
            {
                var newRecord = new ArtistEvent
                {
                    ArtistId = item.ArtistId,
                    EventId = eventId,
                };
                db.ArtistEvents.Add(newRecord);
                db.SaveChanges();
            }

            return items;
        }
    }
}