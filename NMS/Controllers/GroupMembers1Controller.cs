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
    public class GroupMembers1Controller : Controller
    {
        private NMSEntities db = new NMSEntities();

        // GET: GroupMembers1
        public ActionResult Index()
        {
            var groupMembers = db.GroupMembers.Include(g => g.Group).Include(g => g.Student);
            return View(groupMembers.ToList());
        }

        // GET: GroupMembers1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMember groupMember = db.GroupMembers.Find(id);
            if (groupMember == null)
            {
                return HttpNotFound();
            }
            return View(groupMember);
        }

        // GET: GroupMembers1/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName");
            ViewBag.USN = new SelectList(db.Students, "USN", "Name");
            return View();
        }

        // POST: GroupMembers1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,USN,username")] GroupMember groupMember)
        {
            if (ModelState.IsValid)
            {
                db.GroupMembers.Add(groupMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", groupMember.GroupID);
            ViewBag.USN = new SelectList(db.Students, "USN", "Name", groupMember.USN);
            return View(groupMember);
        }

        // GET: GroupMembers1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMember groupMember = db.GroupMembers.Find(id);
            if (groupMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", groupMember.GroupID);
            ViewBag.USN = new SelectList(db.Students, "USN", "Name", groupMember.USN);
            return View(groupMember);
        }

        // POST: GroupMembers1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,USN,username")] GroupMember groupMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName", groupMember.GroupID);
            ViewBag.USN = new SelectList(db.Students, "USN", "Name", groupMember.USN);
            return View(groupMember);
        }

        // GET: GroupMembers1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupMember groupMember = db.GroupMembers.Find(id);
            if (groupMember == null)
            {
                return HttpNotFound();
            }
            return View(groupMember);
        }

        // POST: GroupMembers1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GroupMember groupMember = db.GroupMembers.Find(id);
            db.GroupMembers.Remove(groupMember);
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
