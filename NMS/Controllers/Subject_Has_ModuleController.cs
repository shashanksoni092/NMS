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
    public class Subject_Has_ModuleController : Controller
    {
        private NMSEntities db = new NMSEntities();

        // GET: Subject_Has_Module
        public ActionResult Index()
        {
            var subject_Has_Module = db.Subject_Has_Module.Include(s => s.Module).Include(s => s.Subject);
            return View(subject_Has_Module.ToList());
        }

        // GET: Subject_Has_Module/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject_Has_Module subject_Has_Module = db.Subject_Has_Module.Find(id);
            if (subject_Has_Module == null)
            {
                return HttpNotFound();
            }
            return View(subject_Has_Module);
        }

        // GET: Subject_Has_Module/Create
        public ActionResult Create()
        {
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "ModuleName");
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName");
            return View();
        }

        // POST: Subject_Has_Module/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SMRating,ModuleID,SubID")] Subject_Has_Module subject_Has_Module)
        {
            if (ModelState.IsValid)
            {
                db.Subject_Has_Module.Add(subject_Has_Module);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "ModuleName", subject_Has_Module.ModuleID);
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName", subject_Has_Module.SubID);
            return View(subject_Has_Module);
        }

        // GET: Subject_Has_Module/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject_Has_Module subject_Has_Module = db.Subject_Has_Module.Find(id);
            if (subject_Has_Module == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "ModuleName", subject_Has_Module.ModuleID);
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName", subject_Has_Module.SubID);
            return View(subject_Has_Module);
        }

        // POST: Subject_Has_Module/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SMRating,ModuleID,SubID")] Subject_Has_Module subject_Has_Module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject_Has_Module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "ModuleName", subject_Has_Module.ModuleID);
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName", subject_Has_Module.SubID);
            return View(subject_Has_Module);
        }

        // GET: Subject_Has_Module/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject_Has_Module subject_Has_Module = db.Subject_Has_Module.Find(id);
            if (subject_Has_Module == null)
            {
                return HttpNotFound();
            }
            return View(subject_Has_Module);
        }

        // POST: Subject_Has_Module/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Subject_Has_Module subject_Has_Module = db.Subject_Has_Module.Find(id);
            db.Subject_Has_Module.Remove(subject_Has_Module);
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
