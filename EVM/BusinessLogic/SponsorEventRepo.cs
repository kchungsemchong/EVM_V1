using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public class SponsorEventRepo : ISponsorEventRepo
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public List<Sponsor> StoreSponsorForEvent(Sponsor item)
        {
            List<Sponsor> SponsorList = null;
            SponsorList.Add(item);

            return SponsorList;
        }
        public List<Sponsor> SaveSponsorInEvent(List<Sponsor> items, int eventId)
        {
            if (items.Count < 1)
                return null;

            foreach (var item in items)
            {
                var newRecord = new SponsorEvent
                {
                    SponsorId = item.SponsorId,
                    EventId = eventId,
                };
                db.SponsorEvents.Add(newRecord);
                db.SaveChanges();
            }

            return items;
        }
    }
}