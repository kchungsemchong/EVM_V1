using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public class TariffRepo : ITariffRepo
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly Helper helper = new Helper();

        public IEnumerable<Tariff> Retrieve()
        {
            var records = (from tariff in db.Tariffs
                           where tariff.Status == "Active"
                           select tariff).ToList();

            return records;
        }
        public Tariff Get(int id)
        {
            var record = (from tariff in db.Tariffs
                          where tariff.TariffId == id
                          select tariff).FirstOrDefault();

            return record;
        }
        public Tariff Create(Tariff item)
        {
            bool duplicateExists = true;
            item.Name = helper.ConvertToTitleCase(item.Name);
            duplicateExists = checkForDuplicates(item.Name);
            if (duplicateExists == true)
                return item;

            db.Tariffs.Add(item);
            db.SaveChanges();

            return item;
        }
        public Tariff Update(Tariff item)
        {
            var recordToUpdate = Get(item.TariffId);
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
            var records = (from n in db.Tariffs
                           where n.Name == name
                           select n).ToList();
            if (records.Count > 0)
                return true;

            return false;
        }
    }
}