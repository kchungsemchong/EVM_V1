using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public class EventRepo : IEventRepo
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly Helper helper = new Helper();

        public IEnumerable<Event> Retrieve()
        {
            var records = (from ev in db.Events
                           where ev.Status == "Active"
                           select ev).ToList();

            return records;
        }

        public Event Get(int id)
        {
            var record = (from ev in db.Events
                          where ev.EventId == id
                          select ev).FirstOrDefault();

            return record;
        }

        public Event Create(Event item)
        {
            bool duplicateExists = true;

            item.Name = helper.ConvertToTitleCase(item.Name);
            duplicateExists = checkForDuplicates(item.Name);
            if (duplicateExists == true)
                return item;

            item.DtAdded = DateTime.UtcNow;
            db.Events.Add(item);
            db.SaveChanges();

            return item;
        }
        public Event Update(Event item)
        {
            var recordToUpdate = Get(item.EventId);
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
            var records = (from n in db.Events
                           where n.Name == name
                           select n).ToList();
            if (records.Count > 0)
                return true;

            return false;
        }
    }
}