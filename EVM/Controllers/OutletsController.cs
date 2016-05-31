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
    public class OutletsController : Controller
    {
        private readonly IOutletRepo _repo;

        public OutletsController(IOutletRepo repository)
        {
            _repo = repository;
        }

        // GET: Outlets
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

        // GET: Outlets/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (id > 0)
                    {
                        var record = _repo.Get(id);
                        if (record.OutletId < 1)
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

        // GET: Outlets/Create
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

        // POST: Outlets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Outlet item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        var newRecord = _repo.Create(item);
                        if (newRecord.OutletId < 1)
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

        // GET: Outlets/Edit/5
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

        // POST: Outlets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Outlet item)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    if (ModelState.IsValid)
                    {
                        if (item.OutletId > 0)
                        {
                            var updatedRecord = _repo.Update(item);
                            if (updatedRecord.OutletId < 1)
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