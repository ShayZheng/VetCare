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
    public class FeedbacksController : Controller
    {
        private Entities db = new Entities();

        // GET: Feedbacks
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var feedbacks = db.FeedbackSet.Where(f => f.AspNetUsersId == userId).ToList();
            return View(feedbacks);
            //var feedbackSet = db.FeedbackSet.Include(f => f.Booking).Include(f => f.AspNetUsers);
            //return View(feedbackSet.ToList());
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.FeedbackSet.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            ViewBag.BookingId = new SelectList(db.BookingSet, "Id", "Descirption");
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Rating,Comments,BookingId")] Feedback feedback)
        {
            feedback.AspNetUsersId = User.Identity.GetUserId();
            ModelState.Clear();
            TryValidateModel(feedback);
            if (ModelState.IsValid)
            {
                db.FeedbackSet.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookingId = new SelectList(db.BookingSet, "Id", "Descirption", feedback.BookingId);
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", feedback.AspNetUsersId);
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.FeedbackSet.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingId = new SelectList(db.BookingSet, "Id", "Descirption", feedback.BookingId);
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", feedback.AspNetUsersId);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Rating,Comments,BookingId,AspNetUsersId")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookingId = new SelectList(db.BookingSet, "Id", "Descirption", feedback.BookingId);
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", feedback.AspNetUsersId);
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.FeedbackSet.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.FeedbackSet.Find(id);
            db.FeedbackSet.Remove(feedback);
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
