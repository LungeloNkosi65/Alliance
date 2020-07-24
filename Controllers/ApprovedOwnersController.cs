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
    public class ApprovedOwnersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApprovedOwners
        public ActionResult Index()
        {
            return View(db.ApprovedOwners.ToList());
        }
        public ActionResult Index1()
        {
            return View(db.ApprovedOwners.ToList());
        }

        // GET: ApprovedOwners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApprovedOwnerss approvedOwners = db.ApprovedOwners.Find(id);
            if (approvedOwners == null)
            {
                return HttpNotFound();
            }
            return View(approvedOwners);
        }

        // GET: ApprovedOwners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApprovedOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ownerID,FullName,LastName,IDNumber,Type,Status,Email,Phone,Nationality,Gender,AltContactNumber")] ApprovedOwnerss approvedOwners)
        {
            if (ModelState.IsValid)
            {
                db.ApprovedOwners.Add(approvedOwners);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(approvedOwners);
        }

        // GET: ApprovedOwners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApprovedOwnerss approvedOwners = db.ApprovedOwners.Find(id);
            if (approvedOwners == null)
            {
                return HttpNotFound();
            }
            return View(approvedOwners);
        }

        // POST: ApprovedOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ownerID,FullName,LastName,IDNumber,Type,Status,Email,Phone,Nationality,Gender,AltContactNumber")] ApprovedOwnerss approvedOwners)
        {
            if (ModelState.IsValid)
            {
                db.Entry(approvedOwners).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(approvedOwners);
        }

        // GET: ApprovedOwners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApprovedOwnerss approvedOwners = db.ApprovedOwners.Find(id);
            if (approvedOwners == null)
            {
                return HttpNotFound();
            }
            return View(approvedOwners);
        }

        // POST: ApprovedOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApprovedOwnerss approvedOwners = db.ApprovedOwners.Find(id);
            db.ApprovedOwners.Remove(approvedOwners);
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
