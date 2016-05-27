using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVM.Models;

namespace EVM.App_Start
{
    public class InitializeData
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public void InitializeApplicationData()
        {
            InitializeEventStatus();
        }

        public void InitializeEventStatus()
        {
            var records = (from ev in db.Events
                           where ev.Status == "Active"
                           select ev).ToList();

            foreach (var item in records)
            {
                int differenceBetweenTwoDates = DateTime.Compare(Convert.ToDateTime(DateTime.UtcNow),
                    Convert.ToDateTime(item.EventDate));
                if (differenceBetweenTwoDates > 0)
                {
                    item.Status = "Archived";
                    db.SaveChanges();
                }
            }
        }
    }
}