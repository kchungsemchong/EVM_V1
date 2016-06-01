using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EVM.BusinessLogic;
using EVM.Models;

namespace EVM.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventRepo _repo;

        private ApplicationDbContext db = new ApplicationDbContext();

        public HomeController(IEventRepo repository)
        {
            _repo = repository;
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public ActionResult Splash()
        {
            return View();
        }

        public ActionResult About()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }

        //public ActionResult Events()
        //{
        //    if (User.IsInRole("Admin"))
        //    {
        //        return RedirectToAction("Index", "Admin");
        //    }

        //    var records = _repo.Retrieve();

        //    var eventList = new List<EventViewModel>();
        //    foreach (var item in records)
        //    {
        //        var newEvent = new EventViewModel()
        //        {
        //            EventId = item.EventId,
        //            Description = item.Description,
        //            EventDate = item.EventDate,
        //            Name = item.Name,
        //            LocationName = db.Locations.Where(l => l.LocationId == item.LocationId).FirstOrDefault().Name,
        //            WallpaperContent = db.Photos.Where(p => p.EventId == item.EventId).FirstOrDefault().Content
        //        };
        //        eventList.Add(newEvent);
        //    }

        //    return View(eventList);
        //}
    }
}