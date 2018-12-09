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
    public class NGroupMemberTeachersController : Controller
    {
        private NMSEntities db = new NMSEntities();

        // GET: NGroupMemberTeachers
        public ActionResult Index()
        {
            var nGroupMemberTeachers = db.NGroupMemberTeachers.Include(n => n.Group).Include(n => n.Teacher);
            return View(nGroupMemberTeachers.ToList());
        }

        // GET: NGroupMemberTeachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroupMemberTeacher nGroupMemberTeacher = db.NGroupMemberTeachers.Find(id);
            if (nGroupMemberTeacher == null)
            {
                return HttpNotFound();
            }
            return View(nGroupMemberTeacher);
        }

        // GET: NGroupMemberTeachers/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName");
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name");
            return View();
        }

        // POST: NGroupMemberTeachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,TID,username")] NGroupMemberTeacher nGroupMemberTeacher)
        {
            if (ModelState.IsValid)
            {
                db.NGroupMemberTeachers.Add(nGroupMemberTeacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", nGroupMemberTeacher.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", nGroupMemberTeacher.TID);
            return View(nGroupMemberTeacher);
        }

        // GET: NGroupMemberTeachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroupMemberTeacher nGroupMemberTeacher = db.NGroupMemberTeachers.Find(id);
            if (nGroupMemberTeacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", nGroupMemberTeacher.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", nGroupMemberTeacher.TID);
            return View(nGroupMemberTeacher);
        }

        // POST: NGroupMemberTeachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,TID,username")] NGroupMemberTeacher nGroupMemberTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nGroupMemberTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", nGroupMemberTeacher.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", nGroupMemberTeacher.TID);
            return View(nGroupMemberTeacher);
        }

        // GET: NGroupMemberTeachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroupMemberTeacher nGroupMemberTeacher = db.NGroupMemberTeachers.Find(id);
            if (nGroupMemberTeacher == null)
            {
                return HttpNotFound();
            }
            return View(nGroupMemberTeacher);
        }

        // POST: NGroupMemberTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NGroupMemberTeacher nGroupMemberTeacher = db.NGroupMemberTeachers.Find(id);
            db.NGroupMemberTeachers.Remove(nGroupMemberTeacher);
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
