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
    public class NotesController : Controller
    {
        private NMSEntities db = new NMSEntities();

        // GET: Notes
        public ActionResult Index()
        {
            var notes = db.Notes.Include(n => n.Class).Include(n => n.Module).Include(n => n.Subject).Include(n => n.Teacher);
            return View(notes.ToList());
        }

        // GET: Notes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "classID", "ClassName");
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "ModuleName");
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName");
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TID,ModuleID,SubID,ClassID,NotesID,PDFFile")] Note note)
        {
            if (ModelState.IsValid)
            {
                db.Notes.Add(note);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "classID", "ClassName", note.ClassID);
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "ModuleName", note.ModuleID);
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName", note.SubID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", note.TID);
            return View(note);
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "classID", "ClassName", note.ClassID);
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "ModuleName", note.ModuleID);
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName", note.SubID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", note.TID);
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TID,ModuleID,SubID,ClassID,NotesID,PDFFile")] Note note)
        {
            if (ModelState.IsValid)
            {
                db.Entry(note).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "classID", "ClassName", note.ClassID);
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "ModuleName", note.ModuleID);
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName", note.SubID);
            ViewBag.TID = new SelectList(db.Teachers, "TID", "Name", note.TID);
            return View(note);
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = db.Notes.Find(id);
            db.Notes.Remove(note);
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
