using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public class OutletRepo : IOutletRepo
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly Helper helper = new Helper();

        public IEnumerable<Outlet> Retrieve()
        {
            var records = (from outlet in db.Outlets
                           where outlet.Status == "Active"
                           select outlet).ToList();

            return records;
        }
        public Outlet Get(int id)
        {
            var record = (from outlet in db.Outlets
                          where outlet.OutletId == id
                          select outlet).FirstOrDefault();

            return record;
        }
        public Outlet Create(Outlet item)
        {
            bool duplicateExists = true;
            item.City = helper.ConvertToTitleCase(item.City);
            item.Name = helper.ConvertToTitleCase(item.Name);
            duplicateExists = checkForDuplicates(item.Name);
            if (duplicateExists == true)
                return item;

            db.Outlets.Add(item);
            db.SaveChanges();

            return item;
        }
        public Outlet Update(Outlet item)
        {
            var recordToUpdate = Get(item.OutletId);
            recordToUpdate.City = item.City;
            recordToUpdate.Street = item.Street;
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
            var records = (from n in db.Outlets
                           where n.Name == name
                           select n).ToList();
            if (records.Count > 0)
                return true;

            return false;
        }
    }
}