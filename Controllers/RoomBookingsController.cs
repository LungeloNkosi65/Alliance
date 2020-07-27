using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Accommodation.Models;
using Accommodation.Services.Interface;
using Microsoft.AspNet.Identity;

namespace Accommodation.Controllers
{
    public class RoomBookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IRoomService _roomService;
        private int _buildingId;

        public RoomBookingsController(IRoomService roomService)
        {
            _roomService = roomService;
            _buildingId = 0;
        }

        // GET: RoomBookings
        public ActionResult Index()
        {
            var userName = User.Identity.GetUserName();
            var roomBookings = db.RoomBookings.Include(r => r.Room);
            if (User.IsInRole("Tenant"))
            {
                return View(roomBookings.ToList().Where(x=>x.TenantEmail==userName));

            }
            else
            {
                return View(roomBookings.ToList());

            }
        }

        // GET: RoomBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            return View(roomBooking);
        }

        public ActionResult Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            return View(roomBooking);
        }

        [Authorize]
        // GET: RoomBookings/Create
        public ActionResult Create(int id)
        {
            ViewBag.Id = id;
            _buildingId = id;
            ViewBag.BuildingId = new SelectList(db.buildings, "BuildingId", "BuildingName");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber");
            return View();
        }

        // POST: RoomBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,BuildingId,RoomId,TenantEmail,RoomPrice,Status")] RoomBooking roomBooking)
        {
            var userName = User.Identity.GetUserName();
            var roomPrice = _roomService.GetRooms().ToList().Where(p => p.RoomId == roomBooking.RoomId).Select(p => p.RoomPrice).FirstOrDefault();
            if (ModelState.IsValid)
            {

                roomBooking.TenantEmail = userName;
                roomBooking.BuildingId = roomBooking.GetBuildingName();
                roomBooking.DateOFBooking = DateTime.Now.Date;
                roomBooking.RoomId = roomBooking.RoomId;
                roomBooking.RoomPrice = roomPrice;
                roomBooking.Status="Room Reserved";
                roomBooking.BuildingAddress = roomBooking.GetBuildingAddress();
                db.RoomBookings.Add(roomBooking);
                db.SaveChanges();
                return RedirectToAction("Confirm", new { id =roomBooking.BookingId});
            }

            ViewBag.BuildingId = new SelectList(db.buildings, "BuildingId", "BuildingName", roomBooking.BuildingId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", roomBooking.RoomId);
            return View(roomBooking);
        }

        // GET: RoomBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingId = new SelectList(db.buildings, "BuildingId", "BuildingName", roomBooking.BuildingId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", roomBooking.RoomId);
            return View(roomBooking);
        }

        // POST: RoomBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,BuildingId,RoomId,TenantEmail,RoomPrice,Status")] RoomBooking roomBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildingId = new SelectList(db.buildings, "BuildingId", "BuildingName", roomBooking.BuildingId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", roomBooking.RoomId);
            return View(roomBooking);
        }

        // GET: RoomBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            if (roomBooking == null)
            {
                return HttpNotFound();
            }
            return View(roomBooking);
        }

        // POST: RoomBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomBooking roomBooking = db.RoomBookings.Find(id);
            db.RoomBookings.Remove(roomBooking);
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
