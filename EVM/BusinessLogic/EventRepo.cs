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
            return null;
        }
        public Event Update(Event item)
        {
            return null;
        }
    }
}