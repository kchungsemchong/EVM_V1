using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EVM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EVM.App_Start
{
    public class InitializeData
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public void InitializeApplicationData()
        {
            //InitializeEventStatus();
            //InitializeRoles();
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
        public void InitializeRoles()
        {
            var roleList = new List<string>()
            {
                "Admin","SuperAdmin"
            };
            if (roleList != null)
            {
                var helper = new Helper();
                foreach (var item in roleList)
                {
                    string titleCaseRole = helper.ConvertToTitleCase(item.ToLowerInvariant());

                    var searchResult = (from role in db.Roles
                                        where role.Name == titleCaseRole
                                        select role).FirstOrDefault();

                    if (searchResult == null)
                    {
                        db.Roles.Add(new IdentityRole()
                        {
                            Name = titleCaseRole
                        });
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}