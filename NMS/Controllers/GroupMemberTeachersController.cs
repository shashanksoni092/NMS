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
    public class GroupMemberTeachersController : Controller
    {
        private NMSEntities db = new NMSEntities();

        // GET: GroupMemberTeachers
        public ActionResult Index()
        {
            var groupMemberTeachers = db.GroupMemberTeachers.Include(g => g.Group).Include(g => g.Teacher);
            return View(groupMemberTeachers.ToList());
        }

        // GET: GroupMemberTeachers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMemberTeacher groupMemberTeacher = db.GroupMemberTeachers.Find(id);
            if (groupMemberTeacher == null)
            {
                return HttpNotFound();
            }
            return View(groupMemberTeacher);
        }

        // GET: GroupMemberTeachers/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName");
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name");
            return View();
        }

        // POST: GroupMemberTeachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,TID,username")] GroupMemberTeacher groupMemberTeacher)
        {
            if (ModelState.IsValid)
            {
                db.GroupMemberTeachers.Add(groupMemberTeacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", groupMemberTeacher.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", groupMemberTeacher.TID);
            return View(groupMemberTeacher);
        }

        // GET: GroupMemberTeachers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMemberTeacher groupMemberTeacher = db.GroupMemberTeachers.Find(id);
            if (groupMemberTeacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", groupMemberTeacher.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", groupMemberTeacher.TID);
            return View(groupMemberTeacher);
        }

        // POST: GroupMemberTeachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,TID,username")] GroupMemberTeacher groupMemberTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupMemberTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", groupMemberTeacher.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", groupMemberTeacher.TID);
            return View(groupMemberTeacher);
        }

        // GET: GroupMemberTeachers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMemberTeacher groupMemberTeacher = db.GroupMemberTeachers.Find(id);
            if (groupMemberTeacher == null)
            {
                return HttpNotFound();
            }
            return View(groupMemberTeacher);
        }

        // POST: GroupMemberTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GroupMemberTeacher groupMemberTeacher = db.GroupMemberTeachers.Find(id);
            db.GroupMemberTeachers.Remove(groupMemberTeacher);
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
