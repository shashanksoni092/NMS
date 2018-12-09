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
    public class NGroupMessageTeachersController : Controller
    {
        private NMSEntities db = new NMSEntities();

        // GET: NGroupMessageTeachers
        public ActionResult Index()
        {
            var nGroupMessageTeachers = db.NGroupMessageTeachers.Include(n => n.Group).Include(n => n.Teacher);
            return View(nGroupMessageTeachers.ToList());
        }

        // GET: NGroupMessageTeachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroupMessageTeacher nGroupMessageTeacher = db.NGroupMessageTeachers.Find(id);
            if (nGroupMessageTeacher == null)
            {
                return HttpNotFound();
            }
            return View(nGroupMessageTeacher);
        }

        // GET: NGroupMessageTeachers/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName");
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name");
            return View();
        }

        // POST: NGroupMessageTeachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,TID,message,Time,Media")] NGroupMessageTeacher nGroupMessageTeacher)
        {
            if (ModelState.IsValid)
            {
                db.NGroupMessageTeachers.Add(nGroupMessageTeacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", nGroupMessageTeacher.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", nGroupMessageTeacher.TID);
            return View(nGroupMessageTeacher);
        }

        // GET: NGroupMessageTeachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroupMessageTeacher nGroupMessageTeacher = db.NGroupMessageTeachers.Find(id);
            if (nGroupMessageTeacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", nGroupMessageTeacher.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", nGroupMessageTeacher.TID);
            return View(nGroupMessageTeacher);
        }

        // POST: NGroupMessageTeachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,TID,message,Time,Media")] NGroupMessageTeacher nGroupMessageTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nGroupMessageTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", nGroupMessageTeacher.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", nGroupMessageTeacher.TID);
            return View(nGroupMessageTeacher);
        }

        // GET: NGroupMessageTeachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroupMessageTeacher nGroupMessageTeacher = db.NGroupMessageTeachers.Find(id);
            if (nGroupMessageTeacher == null)
            {
                return HttpNotFound();
            }
            return View(nGroupMessageTeacher);
        }

        // POST: NGroupMessageTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NGroupMessageTeacher nGroupMessageTeacher = db.NGroupMessageTeachers.Find(id);
            db.NGroupMessageTeachers.Remove(nGroupMessageTeacher);
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
