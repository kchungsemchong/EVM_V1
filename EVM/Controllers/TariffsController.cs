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
    public class TariffsController : Controller
    {
        private readonly ITariffRepo _repo;

        public TariffsController(ITariffRepo repository)
        {
            _repo = repository;
        }

        // GET: Tariffs
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

        // GET: Tariffs/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (id > 0)
                    {
                        var record = _repo.Get(id);
                        if (record.TariffId < 1)
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

        // GET: Tariffs/Create
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

        // POST: Tariffs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tariff item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        var newRecord = _repo.Create(item);
                        if (newRecord.TariffId < 1)
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

        // GET: Tariffs/Edit/5
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

        // POST: Tariffs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tariff item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        if (item.TariffId > 0)
                        {
                            var updatedRecord = _repo.Update(item);
                            if (updatedRecord.TariffId < 1)
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