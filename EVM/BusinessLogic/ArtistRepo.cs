using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public class ArtistRepo : IArtistRepo
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Artist> Retrieve()
        {
            var records = (from artist in db.Artists
                           where artist.Status == "Active"
                           select artist).ToList();

            return records;
        }
        public Artist Get(int id)
        {
            var record = (from artist in db.Artists
                          where artist.ArtistId == id
                          select artist).FirstOrDefault();

            return record;
        }
        public Artist Create(Artist item)
        {
            item.DtAdded = DateTime.UtcNow;
            db.Artists.Add(item);
            db.SaveChanges();

            return item;
        }
        public Artist Update(Artist item)
        {
            var recordToUpdate = Get(item.ArtistId);
            recordToUpdate.FacebookUrl = item.FacebookUrl;
            recordToUpdate.Name = item.Name;

            if (item.Status == "Archived")
            {
                recordToUpdate.Status = "Active";
                db.SaveChanges();

                return item;
            }
            if (item.Status == "Active")
            {
                recordToUpdate.Status = "Archived";
                db.SaveChanges();

                return item;
            }

            return item;
        }
    }
}