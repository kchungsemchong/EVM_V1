using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EVM.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }
    }
}