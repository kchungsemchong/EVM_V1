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
    public class ArtistsController : Controller
    {
        private readonly IArtistRepo _repo;

        public ArtistsController(IArtistRepo repository)
        {
            _repo = repository;
        }

        // GET: Artists
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

        // GET: Artists/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (id > 0)
                    {
                        var record = _repo.Get(id);
                        if (record.ArtistId < 1)
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

        // GET: Artists/Create
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

        // POST: Artists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Artist item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        var newRecord = _repo.Create(item);
                        if (newRecord.ArtistId < 1)
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

        // GET: Artists/Edit/5
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

        // POST: Artists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Artist item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        if (item.ArtistId > 0)
                        {
                            var updatedRecord = _repo.Update(item);
                            if (updatedRecord.ArtistId < 1)
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