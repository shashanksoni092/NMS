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
    public class NGroupMemberTeacher1Controller : Controller
    {
        private NMSEntities db = new NMSEntities();

        // GET: NGroupMemberTeacher1
        public ActionResult Index()
        {
            var nGroupMemberTeachers1 = db.NGroupMemberTeachers1.Include(n => n.NGroup).Include(n => n.Teacher);
            return View(nGroupMemberTeachers1.ToList());
        }

        // GET: NGroupMemberTeacher1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroupMemberTeacher1 nGroupMemberTeacher1 = db.NGroupMemberTeachers1.Find(id);
            if (nGroupMemberTeacher1 == null)
            {
                return HttpNotFound();
            }
            return View(nGroupMemberTeacher1);
        }

        // GET: NGroupMemberTeacher1/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.NGroups, "GroupID", "GroupName");
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name");
            return View();
        }

        // POST: NGroupMemberTeacher1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,TID,username")] NGroupMemberTeacher1 nGroupMemberTeacher1)
        {
            if (ModelState.IsValid)
            {
                db.NGroupMemberTeachers1.Add(nGroupMemberTeacher1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.NGroups, "GroupID", "GroupName", nGroupMemberTeacher1.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", nGroupMemberTeacher1.TID);
            return View(nGroupMemberTeacher1);
        }

        // GET: NGroupMemberTeacher1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroupMemberTeacher1 nGroupMemberTeacher1 = db.NGroupMemberTeachers1.Find(id);
            if (nGroupMemberTeacher1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.NGroups, "GroupID", "GroupName", nGroupMemberTeacher1.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", nGroupMemberTeacher1.TID);
            return View(nGroupMemberTeacher1);
        }

        // POST: NGroupMemberTeacher1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,TID,username")] NGroupMemberTeacher1 nGroupMemberTeacher1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nGroupMemberTeacher1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.NGroups, "GroupID", "GroupName", nGroupMemberTeacher1.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", nGroupMemberTeacher1.TID);
            return View(nGroupMemberTeacher1);
        }

        // GET: NGroupMemberTeacher1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroupMemberTeacher1 nGroupMemberTeacher1 = db.NGroupMemberTeachers1.Find(id);
            if (nGroupMemberTeacher1 == null)
            {
                return HttpNotFound();
            }
            return View(nGroupMemberTeacher1);
        }

        // POST: NGroupMemberTeacher1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NGroupMemberTeacher1 nGroupMemberTeacher1 = db.NGroupMemberTeachers1.Find(id);
            db.NGroupMemberTeachers1.Remove(nGroupMemberTeacher1);
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
