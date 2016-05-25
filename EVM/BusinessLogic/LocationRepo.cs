using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public class LocationRepo : ILocationRepo
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly Helper helper = new Helper();

        public IEnumerable<Location> Retrieve()
        {
            var records = (from location in db.Locations
                           where location.Status == "Active"
                           select location).ToList();

            return records;
        }
        public Location Get(int id)
        {
            var record = (from location in db.Locations
                          where location.LocationId == id
                          select location).FirstOrDefault();

            return record;
        }
        public Location Create(Location item)
        {
            bool duplicateExists = true;

            item.Name = helper.ConvertToTitleCase(item.Name);
            duplicateExists = checkForDuplicates(item.Name);
            if (duplicateExists == true)
                return item;

            db.Locations.Add(item);
            db.SaveChanges();

            return item;
        }
        public Location Update(Location item)
        {
            var recordToUpdate = Get(item.LocationId);
            recordToUpdate.GoogleMapsLink = item.GoogleMapsLink;
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
            var records = (from n in db.Locations
                           where n.Name == name
                           select n).ToList();
            if (records.Count > 0)
                return true;

            return false;
        }
    }
}