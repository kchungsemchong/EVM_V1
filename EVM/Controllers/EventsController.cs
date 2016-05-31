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
    public class EventsController : Controller
    {
        private readonly IEventRepo _repo;
        private ApplicationDbContext db = new ApplicationDbContext();

        public EventsController(IEventRepo repository)
        {
            _repo = repository;
        }

        // GET: Events
        public ActionResult Index()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var records = _repo.Retrieve();
                    if (records == null)
                        return RedirectToAction("Error404", "Home");

                    return View(records);
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (id > 0)
                    {
                        var record = _repo.Get(id);
                        if (record.EventId < 1)
                            return RedirectToAction("Error404", "Home");

                        return View(record);
                    }

                    return RedirectToAction("Error404", "Home");
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    Event newEvent = null;
                    newEvent.LocationId = RetrieveLocationId();
                    if (newEvent.LocationId == 0)
                        return RedirectToAction("Create", "Locations");
                    RedirectToAction("");

                    return View();
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        var newRecord = _repo.Create(item);
                        if (newRecord.EventId < 1)
                            return RedirectToAction("Error404", "Home");

                        return RedirectToAction("Index", "Locations");
                    }
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (id > 0)
                    {
                        var record = _repo.Get(id);

                        return View(record);
                    }
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        if (item.EventId > 0)
                        {
                            var updatedRecord = _repo.Update(item);
                            if (updatedRecord.EventId < 1)
                                return RedirectToAction("Error404", "Home");

                            return RedirectToAction("Index", "Locations");
                        }
                    }
                }

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public ActionResult ArtistSelection()
        {
            return View();
        }

        public JsonResult GetArtist(string Name)
        {
            var record = db.Artists.Where(a => a.Name.StartsWith(Name) && a.Status == "Active").ToList();
            return Json(record, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ArtistList(int id)
        {
            var eventArtist = Session["Event"] as List<Artist>;
            List<Artist> artistList = new List<Artist>();
            bool msg = false;
            if (eventArtist == null)
            {
                var artist = new Artist()
                    {
                        ArtistId = id,
                        Name = db.Artists.Where(a => a.ArtistId == id).FirstOrDefault().Name
                    };
                artistList.Add(artist);
                Session["Event"] = artistList;
                msg = true;
            }
            else
            {
                bool isDuplicate = true;

                isDuplicate = checkDuplicateArtist(id);
                if (isDuplicate == false)
                {
                    var artist = new Artist()
                    {
                        ArtistId = id,
                        Name = db.Artists.Where(a => a.ArtistId == id).FirstOrDefault().Name
                    };
                    eventArtist.Add(artist);
                    Session["Event"] = eventArtist;
                    msg = true;
                }
                else
                {
                    msg = false;
                }
            }

            var eventFinalList = Session["Event"] as List<Artist>;

            return Json(new { eventFinalList, msg }, JsonRequestBehavior.AllowGet);
        }

        public bool checkDuplicateArtist(int id)
        {
            var count = 0;
            var eventArtist = Session["Event"] as List<Artist>;
            foreach (var item in eventArtist)
            {
                if (item.ArtistId == id)
                {
                    count += 1;
                }
            }

            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public ActionResult LocationSelection()
        {
            return View();
        }

        public JsonResult GetLocation(string Name)
        {
            var record = db.Locations.Where(a => a.Name.StartsWith(Name) && a.Status == "Active").ToList();
            return Json(record, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LocationList(int id)
        {
            var eventLocation = Session["Location"] as List<Location>;
            List<Location> LocationList = new List<Location>();
            bool msg = false;
            if (eventLocation == null)
            {
                var location = new Location()
                {
                    LocationId = id,
                    Name = db.Locations.Where(a => a.LocationId == id).FirstOrDefault().Name
                };
                LocationList.Add(location);
                Session["Location"] = LocationList;
                msg = true;
            }
            else
            {
                bool isDuplicate = true;

                isDuplicate = checkDuplicateLocation(id);
                if (isDuplicate == false)
                {
                    var location = new Location()
                    {
                        LocationId = id,
                        Name = db.Locations.Where(a => a.LocationId == id).FirstOrDefault().Name
                    };
                    eventLocation.Add(location);
                    Session["Location"] = eventLocation;
                    msg = true;
                }
                else
                {
                    msg = false;
                }
            }

            var eventFinalList = Session["Location"] as List<Location>;

            return Json(new { eventFinalList, msg }, JsonRequestBehavior.AllowGet);
        }

        public bool checkDuplicateLocation(int id)
        {
            var count = 0;
            var eventLocation = Session["Location"] as List<Location>;
            foreach (var item in eventLocation)
            {
                if (item.LocationId == id)
                {
                    count += 1;
                }
            }

            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public int RetrieveLocationId()
        {
            var location = Session["LocationEvent"] as Location;
            if (location.LocationId < 1)
                return 0;

            return location.LocationId;
        }
    }
}