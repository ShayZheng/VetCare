using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using A1Final.Models;

namespace A1Final.Controllers
{
    public class FeedbacksController : Controller
    {
        private Entities db = new Entities();

        // GET: Feedbacks
        public ActionResult Index()
        {
            var feedbackSet = db.FeedbackSet.Include(f => f.AspNetUsers).Include(f => f.Vets);
            return View(feedbackSet.ToList());
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
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.VetsId = new SelectList(db.VetsSet, "Id", "FirstName");
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Rating,Comments,AspNetUsersId,VetsId")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.FeedbackSet.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", feedback.AspNetUsersId);
            ViewBag.VetsId = new SelectList(db.VetsSet, "Id", "FirstName", feedback.VetsId);
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
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", feedback.AspNetUsersId);
            ViewBag.VetsId = new SelectList(db.VetsSet, "Id", "FirstName", feedback.VetsId);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Rating,Comments,AspNetUsersId,VetsId")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", feedback.AspNetUsersId);
            ViewBag.VetsId = new SelectList(db.VetsSet, "Id", "FirstName", feedback.VetsId);
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
