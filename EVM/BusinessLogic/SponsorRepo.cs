using EVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVM.BusinessLogic
{
    public class SponsorRepo : ISponsorRepo
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly Helper helper = new Helper();

        public IEnumerable<Sponsor> Retrieve()
        {
            var records = (from sponsor in db.Sponsors
                           where sponsor.Status == "Active"
                           select sponsor).ToList();

            return records;
        }
        public Sponsor Get(int id)
        {
            var record = (from sponsor in db.Sponsors
                          where sponsor.SponsorId == id
                          select sponsor).FirstOrDefault();

            return record;
        }
        public Sponsor Create(Sponsor item, HttpPostedFileBase photo)
        {
            bool duplicateExists = true;
            item.Name = helper.ConvertToTitleCase(item.Name);
            duplicateExists = checkForDuplicates(item.Name);
            if (duplicateExists == true)
                return item;

            var newSponsor = new Sponsor()
            {
                Name = item.Name,
                DtAdded = DateTime.UtcNow,
                Status = "Active",
                ContentType = photo.ContentType,
                Content = new byte[photo.ContentLength]
            };
            photo.InputStream.Read(newSponsor.Content, 0, photo.ContentLength);

            db.Sponsors.Add(newSponsor);
            db.SaveChanges();

            return newSponsor;
        }
        public Sponsor Update(Sponsor item)
        {
            var recordToUpdate = Get(item.SponsorId);
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
            var records = (from n in db.Sponsors
                           where n.Name == name
                           select n).ToList();
            if (records.Count > 0)
                return true;

            return false;
        }
    }
}