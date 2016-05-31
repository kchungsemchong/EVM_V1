using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EVM.Models;

namespace EVM.BusinessLogic
{
    public class SponsorEventsController : Controller
    {
        private readonly ISponsorEventRepo _repo;

        public SponsorEventsController(ISponsorEventRepo repository)
        {
            _repo = repository;
        }

        // GET: SponsorEvents
        public ActionResult Index()
        {
            return View();
        }

        // GET: SponsorEvents/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SponsorEvents/Create
        public ActionResult Create()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        var items = Session[""] as List<Sponsor>;
                        var newEvent = Session[""] as Event;

                        if (newEvent.EventId < 1 || items.Count < 1)
                            return RedirectToAction("Error404", "Home");

                        var newRecords = _repo.SaveSponsorInEvent(items, newEvent.EventId);
                        if (newRecords == null)
                            return RedirectToAction("Error404", "Home");

                        //Change the Redirect to Action
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

        // POST: SponsorEvents/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SponsorEvents/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SponsorEvents/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SponsorEvents/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SponsorEvents/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}