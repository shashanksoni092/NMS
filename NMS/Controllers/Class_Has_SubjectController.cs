using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NMS.Models;

namespace NMS.Controllers
{
    public class Class_Has_SubjectController : Controller
    {
        private NMSEntities db = new NMSEntities();

        // GET: Class_Has_Subject
        public ActionResult Index()
        {
            var class_Has_Subject = db.Class_Has_Subject.Include(c => c.Class).Include(c => c.Subject);
            return View(class_Has_Subject.ToList());
        }

        // GET: Class_Has_Subject/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Has_Subject class_Has_Subject = db.Class_Has_Subject.Find(id);
            if (class_Has_Subject == null)
            {
                return HttpNotFound();
            }
            return View(class_Has_Subject);
        }

        // GET: Class_Has_Subject/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "classID", "ClassName");
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName");
            return View();
        }

        // POST: Class_Has_Subject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CSRating,ClassID,SubID")] Class_Has_Subject class_Has_Subject)
        {
            if (ModelState.IsValid)
            {
                db.Class_Has_Subject.Add(class_Has_Subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "classID", "ClassName", class_Has_Subject.ClassID);
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName", class_Has_Subject.SubID);
            return View(class_Has_Subject);
        }

        // GET: Class_Has_Subject/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Has_Subject class_Has_Subject = db.Class_Has_Subject.Find(id);
            if (class_Has_Subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "classID", "ClassName", class_Has_Subject.ClassID);
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName", class_Has_Subject.SubID);
            return View(class_Has_Subject);
        }

        // POST: Class_Has_Subject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CSRating,ClassID,SubID")] Class_Has_Subject class_Has_Subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(class_Has_Subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "classID", "ClassName", class_Has_Subject.ClassID);
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName", class_Has_Subject.SubID);
            return View(class_Has_Subject);
        }

        // GET: Class_Has_Subject/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class_Has_Subject class_Has_Subject = db.Class_Has_Subject.Find(id);
            if (class_Has_Subject == null)
            {
                return HttpNotFound();
            }
            return View(class_Has_Subject);
        }

        // POST: Class_Has_Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Class_Has_Subject class_Has_Subject = db.Class_Has_Subject.Find(id);
            db.Class_Has_Subject.Remove(class_Has_Subject);
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
