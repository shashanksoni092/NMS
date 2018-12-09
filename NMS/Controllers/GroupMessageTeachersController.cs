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
    public class GroupMessageTeachersController : Controller
    {
        private NMSEntities db = new NMSEntities();

        // GET: GroupMessageTeachers
        public ActionResult Index()
        {
            var groupMessageTeachers = db.GroupMessageTeachers.Include(g => g.Group).Include(g => g.Teacher);
            return View(groupMessageTeachers.ToList());
        }

        // GET: GroupMessageTeachers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMessageTeacher groupMessageTeacher = db.GroupMessageTeachers.Find(id);
            if (groupMessageTeacher == null)
            {
                return HttpNotFound();
            }
            return View(groupMessageTeacher);
        }

        // GET: GroupMessageTeachers/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName");
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name");
            return View();
        }

        // POST: GroupMessageTeachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,TID,message,Time,Media")] GroupMessageTeacher groupMessageTeacher)
        {
            if (ModelState.IsValid)
            {
                db.GroupMessageTeachers.Add(groupMessageTeacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", groupMessageTeacher.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", groupMessageTeacher.TID);
            return View(groupMessageTeacher);
        }

        // GET: GroupMessageTeachers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMessageTeacher groupMessageTeacher = db.GroupMessageTeachers.Find(id);
            if (groupMessageTeacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", groupMessageTeacher.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", groupMessageTeacher.TID);
            return View(groupMessageTeacher);
        }

        // POST: GroupMessageTeachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,TID,message,Time,Media")] GroupMessageTeacher groupMessageTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupMessageTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", groupMessageTeacher.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", groupMessageTeacher.TID);
            return View(groupMessageTeacher);
        }

        // GET: GroupMessageTeachers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMessageTeacher groupMessageTeacher = db.GroupMessageTeachers.Find(id);
            if (groupMessageTeacher == null)
            {
                return HttpNotFound();
            }
            return View(groupMessageTeacher);
        }

        // POST: GroupMessageTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GroupMessageTeacher groupMessageTeacher = db.GroupMessageTeachers.Find(id);
            db.GroupMessageTeachers.Remove(groupMessageTeacher);
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
