using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Accommodation.Models;

namespace Accommodation.Controllers
{
    public class ManagerBuildingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ManagerBuildings
        public ActionResult Index()
        {
            var managerBuildings = db.ManagerBuildings.Include(m => m.Building).Include(m => m.Manager);
            return View(managerBuildings.ToList());
        }

        // GET: ManagerBuildings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerBuilding managerBuilding = db.ManagerBuildings.Find(id);
            if (managerBuilding == null)
            {
                return HttpNotFound();
            }
            return View(managerBuilding);
        }

        // GET: ManagerBuildings/Create
        public ActionResult Create()
        {
            ViewBag.BuildingId = new SelectList(db.buildings, "BuildingId", "BuildingName");
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FullName");
            return View();
        }

        // POST: ManagerBuildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManagerBuildingId,ManagerId,BuildingId")] ManagerBuilding managerBuilding)
        {
            if (ModelState.IsValid)
            {
                db.ManagerBuildings.Add(managerBuilding);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuildingId = new SelectList(db.buildings, "BuildingId", "BuildingName", managerBuilding.BuildingId);
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FullName", managerBuilding.ManagerId);
            return View(managerBuilding);
        }

        // GET: ManagerBuildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerBuilding managerBuilding = db.ManagerBuildings.Find(id);
            if (managerBuilding == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingId = new SelectList(db.buildings, "BuildingId", "BuildingName", managerBuilding.BuildingId);
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FullName", managerBuilding.ManagerId);
            return View(managerBuilding);
        }

        // POST: ManagerBuildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManagerBuildingId,ManagerId,BuildingId")] ManagerBuilding managerBuilding)
        {
            if (ModelState.IsValid)
            {
                db.Entry(managerBuilding).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildingId = new SelectList(db.buildings, "BuildingId", "BuildingName", managerBuilding.BuildingId);
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FullName", managerBuilding.ManagerId);
            return View(managerBuilding);
        }

        // GET: ManagerBuildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerBuilding managerBuilding = db.ManagerBuildings.Find(id);
            if (managerBuilding == null)
            {
                return HttpNotFound();
            }
            return View(managerBuilding);
        }

        // POST: ManagerBuildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ManagerBuilding managerBuilding = db.ManagerBuildings.Find(id);
            db.ManagerBuildings.Remove(managerBuilding);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
