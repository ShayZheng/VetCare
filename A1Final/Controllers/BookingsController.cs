using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using A1Final.Models;
using Microsoft.AspNet.Identity;

namespace A1Final.Controllers
{
    public class BookingsController : Controller
    {
        private Entities db = new Entities();

        // GET: Bookings
        [Authorize]
        public ActionResult Index()
        {
            //Reference from studio 7
            var userId = User.Identity.GetUserId();
            var bookings = db.BookingSet.Where(b => b.AspNetUsersId == userId).ToList();
            return View(bookings);
            //var bookingSet = db.BookingSet.Include(b => b.AspNetUsers).Include(b => b.Vets);
            //return View(bookingSet.ToList());

        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.BookingSet.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.VetsId = new SelectList(db.VetsSet, "Id", "FirstName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //only authorized users can enter the Create page. 
        [Authorize]
        // Comment out ,AspNetUsersId
        public ActionResult Create([Bind(Include = "Id,Date,Descirption,VetsId")] Booking booking)
        {
            //each user can only view their own bookings by identify the userid
            booking.AspNetUsersId = User.Identity.GetUserId();
            ModelState.Clear();
            TryValidateModel(booking);

            if (ModelState.IsValid)
            {
                db.BookingSet.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", booking.AspNetUsersId);
            //ViewBag.VetsId = new SelectList(db.VetsSet, "Id", "FirstName", booking.VetsId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.BookingSet.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", booking.AspNetUsersId);
            ViewBag.VetsId = new SelectList(db.VetsSet, "Id", "FirstName", booking.VetsId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Descirption,AspNetUsersId,VetsId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", booking.AspNetUsersId);
            ViewBag.VetsId = new SelectList(db.VetsSet, "Id", "FirstName", booking.VetsId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.BookingSet.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.BookingSet.Find(id);
            db.BookingSet.Remove(booking);
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
