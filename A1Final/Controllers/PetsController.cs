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
    public class PetsController : Controller
    {
        private Entities db = new Entities();

        // GET: Pets
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var pets = db.PetsSet.Where(p => p.AspNetUsersId == userId).ToList();
            return View(pets);
            //var petsSet = db.PetsSet.Include(p => p.AspNetUsers);
            //return View(petsSet.ToList());
        }

        // GET: Pets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pets pets = db.PetsSet.Find(id);
            if (pets == null)
            {
                return HttpNotFound();
            }
            return View(pets);
        }

        // GET: Pets/Create
        public ActionResult Create()
        {
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        //                                         Comment out ,AspNetUsersId 
        public ActionResult Create([Bind(Include = "Id,Name,Type,Age,Gender")] Pets pets)
        {
            pets.AspNetUsersId = User.Identity.GetUserId();
            ModelState.Clear();
            TryValidateModel(pets);

            if (ModelState.IsValid)
            {
                db.PetsSet.Add(pets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", pets.AspNetUsersId);
            return View(pets);
        }

        // GET: Pets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pets pets = db.PetsSet.Find(id);
            if (pets == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", pets.AspNetUsersId);
            return View(pets);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Type,Age,Gender,AspNetUsersId")] Pets pets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", pets.AspNetUsersId);
            return View(pets);
        }

        // GET: Pets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pets pets = db.PetsSet.Find(id);
            if (pets == null)
            {
                return HttpNotFound();
            }
            return View(pets);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pets pets = db.PetsSet.Find(id);
            db.PetsSet.Remove(pets);
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
