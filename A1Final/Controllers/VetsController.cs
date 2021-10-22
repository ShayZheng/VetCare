﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using A1Final.Models;
using A1Final.ViewModel;

namespace A1Final.Controllers
{
    public class VetsController : Controller
    {
        private Entities db;

        public VetsController()
        {
            db = new Entities();
        }

        // GET: Vets
        public ActionResult Index()
        {
            
            IEnumerable<VetViewModel> listOfVetViewModel = (from obtVet in db.VetsSet
                                                            select new VetViewModel()
                                                            {
                                                                Id = obtVet.Id,
                                                                FirstName = obtVet.FirstName,
                                                                LastName = obtVet.LastName,
                                                                Location = obtVet.Location,
                                                                Latitude = obtVet.Latitude,
                                                                Longitude = obtVet.Longitude
                                                            }).ToList();
            return View(listOfVetViewModel);
            //return View(db.VetsSet.ToList());
        }

        // GET: Vets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vets vets = db.VetsSet.Find(id);
            if (vets == null)
            {
                return HttpNotFound();
            }
            return View(vets);
        }

        // GET: Vets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Location,Latitude,Longitude")] Vets vets)
        {
            if (ModelState.IsValid)
            {
                db.VetsSet.Add(vets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vets);
        }

        // GET: Vets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vets vets = db.VetsSet.Find(id);
            if (vets == null)
            {
                return HttpNotFound();
            }
            return View(vets);
        }

        // POST: Vets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Location,Latitude,Longitude")] Vets vets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vets);
        }

        // GET: Vets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vets vets = db.VetsSet.Find(id);
            if (vets == null)
            {
                return HttpNotFound();
            }
            return View(vets);
        }

        // POST: Vets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vets vets = db.VetsSet.Find(id);
            db.VetsSet.Remove(vets);
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

        //Reference: https://www.youtube.com/watch?v=1oGxPoTGl0U



        public ActionResult ShowFeedback(int vetsid)
        {
            IEnumerable<FeedbackViewModel> listofFeedback = (from objFeedback in db.FeedbackSet
                                                    where objFeedback.VetsId == vetsid
                                                    select new FeedbackViewModel()
                                                    {
                                                        VetsId = objFeedback.VetsId,
                                                        Id = objFeedback.Id,
                                                        Rating = objFeedback.Rating,
                                                        Comments = objFeedback.Comments,

                                                    }).ToList();

            return View(listofFeedback);
        }
    }
}
