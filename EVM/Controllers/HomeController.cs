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
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Splash()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Events()
        {
            var record = db.Events.Where(e => e.Status == "Active").ToList();
            var photo = db.Photos.Where(p => p.PhotoId == 1).FirstOrDefault();
            ViewBag.Photo = photo;
            return View(record);
        }
    }
}