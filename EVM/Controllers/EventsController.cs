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
    public class EventsController : Controller
    {
        private readonly IEventRepo _repo;
        private readonly IArtistEventRepo _ArtistEventRepo;
        private readonly ISponsorEventRepo _SponsorEventRepo;
        private readonly ILocationRepo _LocationRepo;

        private ApplicationDbContext db = new ApplicationDbContext();

        public EventsController(IEventRepo EventRepository, IArtistEventRepo ArtistEventRepo, ISponsorEventRepo SponsorEventRepo, ILocationRepo LocationRepo)
        {
            _repo = EventRepository;
            _ArtistEventRepo = ArtistEventRepo;
            _SponsorEventRepo = SponsorEventRepo;
            _LocationRepo = LocationRepo;
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
                    //Event newEvent = null;
                    //newEvent.LocationId = RetrieveLocationId();
                    //if (newEvent.LocationId == 0)
                    //    return RedirectToAction("Create", "Locations");
                    //RedirectToAction("");

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
        public ActionResult Create(Event item, Nullable<DateTime> EventDate)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        //var newRecord = _repo.Create(item);
                        //if (newRecord.EventId < 1)
                        //    return RedirectToAction("Error404", "Home");

                        var NewEvent = new Event()
                        {
                            Name = item.Name,
                            Description = item.Description,
                            EventDate = EventDate,
                            DtAdded = DateTime.UtcNow,
                            Status = "Active"
                        };
                        Session["Event"] = NewEvent;

                        //var EventWallpaper = photo;

                        //var myPhoto = new Photo();
                        //myPhoto.ContentType = EventWallpaper.ContentType;
                        //myPhoto.Content = new byte[EventWallpaper.ContentLength];
                        //myPhoto.EventId = NewEvent.EventId;

                        //EventWallpaper.InputStream.Read(myPhoto.Content, 0, EventWallpaper.ContentLength);
                        //db.Photos.Add(myPhoto);
                        //db.SaveChanges();

                        //Session["EventWallpaper"] = photo;

                        return RedirectToAction("ArtistSelection", "Events");
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

                            return RedirectToAction("Index", "Events");
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
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public JsonResult GetArtist(string Name)
        {
            var record = db.Artists.Where(a => a.Name.StartsWith(Name) && a.Status == "Active").ToList();
            return Json(record, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ArtistList(int id)
        {
            var ArtistListForEvent = Session["ArtistListForEvent"] as List<Artist>;
            List<Artist> NewArtistList = new List<Artist>();
            bool msg = false;
            if (ArtistListForEvent == null)
            {
                var artist = new Artist()
                    {
                        ArtistId = id,
                        Name = db.Artists.Where(a => a.ArtistId == id).FirstOrDefault().Name
                    };
                NewArtistList.Add(artist);
                Session["ArtistListForEvent"] = NewArtistList;
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
                    ArtistListForEvent.Add(artist);
                    Session["ArtistListForEvent"] = ArtistListForEvent;
                    msg = true;
                }
                else
                {
                    msg = false;
                }
            }

            var FinalArtistListForEvent = Session["ArtistListForEvent"] as List<Artist>;

            return Json(new { FinalArtistListForEvent, msg }, JsonRequestBehavior.AllowGet);
        }

        public bool checkDuplicateArtist(int id)
        {
            var count = 0;
            var ArtistListForEvent = Session["ArtistListForEvent"] as List<Artist>;
            foreach (var item in ArtistListForEvent)
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
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public JsonResult GetLocation(string Name)
        {
            var record = db.Locations.Where(a => a.Name.StartsWith(Name) && a.Status == "Active").ToList();
            return Json(record, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LocationList(int id)
        {
            var eventLocation = Session["EventLocation"] as Location;
            Location NewLocation = new Location();
            bool msg = false;
            if (eventLocation == null)
            {
                var location = new Location()
                {
                    LocationId = id,
                    Name = db.Locations.Where(a => a.LocationId == id).FirstOrDefault().Name
                };

                //NewLocationList.Add(location);
                Session["EventLocation"] = location;
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

                    //eventLocation.Add(location);
                    Session["EventLocation"] = location;
                    msg = true;
                }
                else
                {
                    msg = false;
                }
            }

            var eventFinalLocation = Session["EventLocation"] as Location;

            return Json(new { eventFinalLocation, msg }, JsonRequestBehavior.AllowGet);
        }

        public bool checkDuplicateLocation(int id)
        {
            //var count = 0;
            var eventLocation = Session["EventLocation"] as Location;
            var record = db.Locations.Where(l => l.LocationId == id).ToList();

            //foreach (var item in eventLocation)
            //{
            //if (item.LocationId == id)
            //{
            //    count += 1;
            //}
            //}

            if (record.Count() > 0)
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

        public ActionResult SponsorSelection()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public JsonResult GetSponsor(string Name)
        {
            var record = db.Sponsors.Where(a => a.Name.StartsWith(Name) && a.Status == "Active").ToList();
            return Json(record, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SponsorList(int id)
        {
            var SponsorListForEvent = Session["SponsorListForEvent"] as List<Sponsor>;
            List<Sponsor> NewSponsorList = new List<Sponsor>();
            bool msg = false;
            if (SponsorListForEvent == null)
            {
                var sponsor = new Sponsor()
                {
                    SponsorId = id,
                    Name = db.Sponsors.Where(a => a.SponsorId == id).FirstOrDefault().Name
                };
                NewSponsorList.Add(sponsor);
                Session["SponsorListForEvent"] = NewSponsorList;
                msg = true;
            }
            else
            {
                bool isDuplicate = true;

                isDuplicate = checkDuplicateSponsor(id);
                if (isDuplicate == false)
                {
                    var sponsor = new Sponsor()
                    {
                        SponsorId = id,
                        Name = db.Sponsors.Where(a => a.SponsorId == id).FirstOrDefault().Name
                    };
                    SponsorListForEvent.Add(sponsor);
                    Session["SponsorListForEvent"] = SponsorListForEvent;
                    msg = true;
                }
                else
                {
                    msg = false;
                }
            }

            var FinalSponsorListForEvent = Session["SponsorListForEvent"] as List<Sponsor>;

            return Json(new { FinalSponsorListForEvent, msg }, JsonRequestBehavior.AllowGet);
        }

        public bool checkDuplicateSponsor(int id)
        {
            var count = 0;
            var SponsorList = Session["SponsorListForEvent"] as List<Sponsor>;
            foreach (var item in SponsorList)
            {
                if (item.SponsorId == id)
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

        public ActionResult Photo()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Photo(HttpPostedFileBase photo)
        {
            if (User.IsInRole("Admin"))
            {
                var EventDetails = Session["Event"] as Event;
                var ArtistListForEvent = Session["ArtistListForEvent"] as List<Artist>;
                var EventLocation = Session["EventLocation"] as Location;
                var SponsorListForEvent = Session["SponsorListForEvent"] as List<Sponsor>;

                //var EventWallpaper = Session["EventWallpaper"] as HttpPostedFileBase;

                if (EventDetails == null || EventLocation == null || SponsorListForEvent == null || ArtistListForEvent == null)
                {
                    return RedirectToAction("Error404", "Home");
                }

                var NewEvent = new Event()
                {
                    Description = EventDetails.Description,
                    EventDate = EventDetails.EventDate,
                    Name = EventDetails.Name,
                    LocationId = EventLocation.LocationId,
                    DtAdded = DateTime.UtcNow,
                    Status = "Active"
                };
                db.Events.Add(NewEvent);
                db.SaveChanges();

                var myPhoto = new Photo();
                myPhoto.ContentType = photo.ContentType;
                myPhoto.Content = new byte[photo.ContentLength];
                myPhoto.EventId = NewEvent.EventId;
                photo.InputStream.Read(myPhoto.Content, 0, photo.ContentLength);
                db.Photos.Add(myPhoto);
                db.SaveChanges();

                var newArtistList = _ArtistEventRepo.SaveArtistInEvent(ArtistListForEvent, NewEvent.EventId);
                if (newArtistList.Count < 0)
                {
                    return RedirectToAction("Error404", "Home");
                }

                var newSponsorList = _SponsorEventRepo.SaveSponsorInEvent(SponsorListForEvent, NewEvent.EventId);
                if (newSponsorList.Count() < 0)
                {
                    return RedirectToAction("Error404", "Home");
                }

                return RedirectToAction("Details", "Events", new { id = NewEvent.EventId });
            }
            return RedirectToAction("Login", "Account");
        }
    }
}