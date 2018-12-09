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
    public class Teacher_Teaches_subjectController : Controller
    {
        private NMSEntities db = new NMSEntities();

        // GET: Teacher_Teaches_subject
        public ActionResult Index()
        {
            var teacher_Teaches_subject = db.Teacher_Teaches_subject.Include(t => t.Subject).Include(t => t.Teacher);
            return View(teacher_Teaches_subject.ToList());
        }

        // GET: Teacher_Teaches_subject/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Teaches_subject teacher_Teaches_subject = db.Teacher_Teaches_subject.Find(id);
            if (teacher_Teaches_subject == null)
            {
                return HttpNotFound();
            }
            return View(teacher_Teaches_subject);
        }

        // GET: Teacher_Teaches_subject/Create
        public ActionResult Create()
        {
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName");
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name");
            return View();
        }

        // POST: Teacher_Teaches_subject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TSRating,TID,SubID")] Teacher_Teaches_subject teacher_Teaches_subject)
        {
            if (ModelState.IsValid)
            {
                db.Teacher_Teaches_subject.Add(teacher_Teaches_subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName", teacher_Teaches_subject.SubID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", teacher_Teaches_subject.TID);
            return View(teacher_Teaches_subject);
        }

        // GET: Teacher_Teaches_subject/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Teaches_subject teacher_Teaches_subject = db.Teacher_Teaches_subject.Find(id);
            if (teacher_Teaches_subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName", teacher_Teaches_subject.SubID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", teacher_Teaches_subject.TID);
            return View(teacher_Teaches_subject);
        }

        // POST: Teacher_Teaches_subject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TSRating,TID,SubID")] Teacher_Teaches_subject teacher_Teaches_subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher_Teaches_subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName", teacher_Teaches_subject.SubID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", teacher_Teaches_subject.TID);
            return View(teacher_Teaches_subject);
        }

        // GET: Teacher_Teaches_subject/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher_Teaches_subject teacher_Teaches_subject = db.Teacher_Teaches_subject.Find(id);
            if (teacher_Teaches_subject == null)
            {
                return HttpNotFound();
            }
            return View(teacher_Teaches_subject);
        }

        // POST: Teacher_Teaches_subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Teacher_Teaches_subject teacher_Teaches_subject = db.Teacher_Teaches_subject.Find(id);
            db.Teacher_Teaches_subject.Remove(teacher_Teaches_subject);
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
