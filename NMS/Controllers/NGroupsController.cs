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
    public class NGroupsController : Controller
    {
        private NMSEntities db = new NMSEntities();

        // GET: NGroups
        public ActionResult Index()
        {
            var nGroups = db.NGroups.Include(n => n.Class).Include(n => n.Subject);
            return View(nGroups.ToList());
        }

        // GET: NGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroup nGroup = db.NGroups.Find(id);
            if (nGroup == null)
            {
                return HttpNotFound();
            }
            return View(nGroup);
        }

        // GET: NGroups/Create
        public ActionResult Create()
        {
            ViewBag.classID = new SelectList(db.Classes, "classID", "ClassName");
            ViewBag.subID = new SelectList(db.Subjects, "SubID", "SubName");
            return View();
        }

        // POST: NGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupName,GroupIcon,classID,subID")] NGroup nGroup)
        {
            if (ModelState.IsValid)
            {
                db.NGroups.Add(nGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.classID = new SelectList(db.Classes, "classID", "ClassName", nGroup.classID);
            ViewBag.subID = new SelectList(db.Subjects, "SubID", "SubName", nGroup.subID);
            return View(nGroup);
        }

        // GET: NGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroup nGroup = db.NGroups.Find(id);
            if (nGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.classID = new SelectList(db.Classes, "classID", "ClassName", nGroup.classID);
            ViewBag.subID = new SelectList(db.Subjects, "SubID", "SubName", nGroup.subID);
            return View(nGroup);
        }

        // POST: NGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GroupName,GroupIcon,classID,subID")] NGroup nGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.classID = new SelectList(db.Classes, "classID", "ClassName", nGroup.classID);
            ViewBag.subID = new SelectList(db.Subjects, "SubID", "SubName", nGroup.subID);
            return View(nGroup);
        }

        // GET: NGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGroup nGroup = db.NGroups.Find(id);
            if (nGroup == null)
            {
                return HttpNotFound();
            }
            return View(nGroup);
        }

        // POST: NGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NGroup nGroup = db.NGroups.Find(id);
            db.NGroups.Remove(nGroup);
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
