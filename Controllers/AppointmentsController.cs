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
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET: Appointments
        public ActionResult Index()
        {
            var userName = User.Identity.GetUserName();
            if (User.IsInRole("Tenant"))
            {
                var appointments = _appointmentService.GetAppointments().Where(x=>x.email==userName);
                return View(appointments.ToList());
            }
            else if (User.IsInRole("Manager"))
            {
                var appointments = _appointmentService.GetAppointments().Where(x=>x.Managers.Email==userName);
                return View(appointments.ToList());
            }
            else
            {
                var appointments = _appointmentService.GetAppointments();
                return View(appointments.ToList());
            }
        
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }
        [Authorize]
        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FullName");
            ViewBag.TimeSlotID = new SelectList(db.timeslots, "TimeSlotID", "TimeS");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,ManagerId,email,ADate,TimeSlotID,Status")] Appointment appointment)
        {
            var userName = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                if (_appointmentService.CheckAppoinment(appointment)==false)
                {
                    appointment.email = userName;
                    appointment.DateBooked = DateTime.Now.Date;
                    appointment.Status = "Pending";
                    db.Appointments.Add(appointment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Slot is currently un available for the date chosen");
                    ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FullName", appointment.ManagerId);
                    ViewBag.TimeSlotID = new SelectList(db.timeslots, "TimeSlotID", "TimeS", appointment.TimeSlotID);
                    return View(appointment);
                }
              
            }

            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FullName", appointment.ManagerId);
            ViewBag.TimeSlotID = new SelectList(db.timeslots, "TimeSlotID", "TimeS", appointment.TimeSlotID);
            return View(appointment);
        }


         public ActionResult ConfirmAppointment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dbRecord = db.Appointments.Find(id);
            dbRecord.Status = "Confirmed";
            db.Entry(dbRecord).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FullName", appointment.ManagerId);
            ViewBag.TimeSlotID = new SelectList(db.timeslots, "TimeSlotID", "TimeS", appointment.TimeSlotID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentId,ManagerId,email,ADate,TimeSlotID,Status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FullName", appointment.ManagerId);
            ViewBag.TimeSlotID = new SelectList(db.timeslots, "TimeSlotID", "TimeS", appointment.TimeSlotID);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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
