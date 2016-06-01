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

        public ActionResult Events()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }

            var records = _repo.Retrieve();

            var eventList = new List<EventViewModel>();
            foreach (var item in records)
            {
                var newEvent = new EventViewModel()
                {
                    EventId = item.EventId,
                    Description = item.Description,
                    EventDate = item.EventDate,
                    Name = item.Name,
                    LocationName = db.Locations.Where(l => l.LocationId == item.LocationId).FirstOrDefault().Name,
                    WallpaperContent = db.Photos.Where(p => p.EventId == item.EventId).FirstOrDefault().Content
                };
                eventList.Add(newEvent);
            }

            return View(eventList);
        }

        public ActionResult EventDetails(int id)
        {
            if (id > 0)
            {
                var record = _repo.Get(id);
                if (record.EventId < 1)
                    return RedirectToAction("Error404", "Home");

                var newEvent = new EventDetailsViewModel()
                {
                    EventId = record.EventId,
                    Description = record.Description,
                    EventDate = record.EventDate,
                    Name = record.Name,
                    LocationName = db.Locations.Where(l => l.LocationId == record.LocationId).FirstOrDefault().Name,
                    WallpaperContent = db.Photos.Where(p => p.EventId == record.EventId).FirstOrDefault().Content,
                    DtAdded = record.DtAdded,
                    Status = record.Status
                };

                var ArtistEventRecord = (from a in db.Artists
                                         join ae in db.ArtistEvents on a.ArtistId equals ae.ArtistId
                                         where ae.EventId == id
                                         select a).ToList();

                var ArtistListForEvent = new List<EventArtistViewModel>();
                foreach (var item in ArtistEventRecord)
                {
                    var newArtist = new EventArtistViewModel()
                    {
                        ArtistName = item.Name,
                        FacebookUrl = item.FacebookUrl
                    };
                    ArtistListForEvent.Add(newArtist);
                }
                ViewBag.Artist = ArtistListForEvent;

                var SponsorEventRecord = (from s in db.Sponsors
                                          join se in db.SponsorEvents on s.SponsorId equals se.SponsorId
                                          where se.EventId == id
                                          select s).ToList();

                var SponsorListForEvent = new List<EventSponsorViewModel>();
                foreach (var item in SponsorEventRecord)
                {
                    var newSponsor = new EventSponsorViewModel()
                    {
                        SponsorName = item.Name,
                        SponsorImage = item.Content
                    };
                    SponsorListForEvent.Add(newSponsor);
                }
                ViewBag.Sponsor = SponsorListForEvent;

                //var photo = db.Photos.Where(p => p.EventId == id).FirstOrDefault();
                //ViewBag.Photo = photo;

                return View(newEvent);
            }

            return RedirectToAction("Error404", "Home");
        }
    }
}