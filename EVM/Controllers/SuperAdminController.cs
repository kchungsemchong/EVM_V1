using EVM.BusinessLogic;
using EVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EVM.Controllers
{
    public class SuperAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SuperAdmin
        public ActionResult Index()
        {
            if (User.IsInRole("Super"))
            {
                var records = db.Users.ToList();
                return View(records);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}