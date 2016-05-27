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
    }
}