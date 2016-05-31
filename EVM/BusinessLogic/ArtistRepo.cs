using EVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVM.BusinessLogic
{
    public class ArtistRepo : IArtistRepo
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly Helper helper = new Helper();

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
            bool duplicateExists = true;

            item.Name = helper.ConvertToTitleCase(item.Name);
            duplicateExists = checkForDuplicates(item.Name);
            if (duplicateExists == true)
                return item;

            item.DtAdded = DateTime.UtcNow;
            item.Status = "Active";
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
                recordToUpdate.Status = "Archived";
                db.SaveChanges();

                return item;
            }
            if (item.Status == "Active")
            {
                recordToUpdate.Status = "Active";
                db.SaveChanges();

                return item;
            }

            return item;
        }

        public bool checkForDuplicates(string name)
        {
            var records = (from n in db.Artists
                           where n.Name == name
                           select n).ToList();
            if (records.Count > 0)
                return true;

            return false;
        }
    }
}