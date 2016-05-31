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
    public class ArtistEventsController : Controller
    {
        private readonly IArtistEventRepo _repo;

        public ArtistEventsController(IArtistEventRepo repository)
        {
            _repo = repository;
        }

        // GET: ArtistEvents
        public ActionResult Index()
        {
            return View();
        }

        // GET: ArtistEvents/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArtistEvents/Create
        public ActionResult Create()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        var items = Session[""] as List<Artist>;
                        var newEvent = Session[""] as Event;

                        if (newEvent.EventId < 1 || items.Count < 1)
                            return RedirectToAction("Error404", "Home");

                        var newRecords = _repo.SaveArtistInEvent(items, newEvent.EventId);
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

        //// POST: ArtistEvents/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: ArtistEvents/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArtistEvents/Edit/5
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

        // GET: ArtistEvents/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArtistEvents/Delete/5
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