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
    public class NGroupMessageTeacher1Controller : Controller
    {
        private NMSEntities db = new NMSEntities();

        // GET: NGroupMessageTeacher1
        public ActionResult Index()
        {
            var nGroupMessageTeachers1 = db.NGroupMessageTeachers1.Include(n => n.NGroup).Include(n => n.Teacher);
            return View(nGroupMessageTeachers1.ToList());
        }

        // GET: NGroupMessageTeacher1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroupMessageTeacher1 nGroupMessageTeacher1 = db.NGroupMessageTeachers1.Find(id);
            if (nGroupMessageTeacher1 == null)
            {
                return HttpNotFound();
            }
            return View(nGroupMessageTeacher1);
        }

        // GET: NGroupMessageTeacher1/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.NGroups, "GroupID", "GroupName");
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name");
            return View();
        }

        // POST: NGroupMessageTeacher1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,TID,message,Time,Media")] NGroupMessageTeacher1 nGroupMessageTeacher1)
        {
            if (ModelState.IsValid)
            {
                db.NGroupMessageTeachers1.Add(nGroupMessageTeacher1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.NGroups, "GroupID", "GroupName", nGroupMessageTeacher1.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", nGroupMessageTeacher1.TID);
            return View(nGroupMessageTeacher1);
        }

        // GET: NGroupMessageTeacher1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroupMessageTeacher1 nGroupMessageTeacher1 = db.NGroupMessageTeachers1.Find(id);
            if (nGroupMessageTeacher1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.NGroups, "GroupID", "GroupName", nGroupMessageTeacher1.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", nGroupMessageTeacher1.TID);
            return View(nGroupMessageTeacher1);
        }

        // POST: NGroupMessageTeacher1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,TID,message,Time,Media")] NGroupMessageTeacher1 nGroupMessageTeacher1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nGroupMessageTeacher1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.NGroups, "GroupID", "GroupName", nGroupMessageTeacher1.GroupID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", nGroupMessageTeacher1.TID);
            return View(nGroupMessageTeacher1);
        }

        // GET: NGroupMessageTeacher1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroupMessageTeacher1 nGroupMessageTeacher1 = db.NGroupMessageTeachers1.Find(id);
            if (nGroupMessageTeacher1 == null)
            {
                return HttpNotFound();
            }
            return View(nGroupMessageTeacher1);
        }

        // POST: NGroupMessageTeacher1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NGroupMessageTeacher1 nGroupMessageTeacher1 = db.NGroupMessageTeachers1.Find(id);
            db.NGroupMessageTeachers1.Remove(nGroupMessageTeacher1);
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
