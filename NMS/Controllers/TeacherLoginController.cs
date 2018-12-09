using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NMS.Models;

namespace NMS.Controllers
{
    public class TeacherLoginController : Controller
    {
        // GET: TeacherLogin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Teacher objUser)
        {
            if (ModelState.IsValid)
            {
                using (NMSEntities db = new NMSEntities())
                {
                    var obj = db.Teachers.Where(a => a.TID.Equals(objUser.TID) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["TID"] = obj.TID.ToString();
                        Session["Name"] = obj.Name.ToString();
                        return RedirectToAction("Index","TeacherPortal");
                    }
                }
            }
            return View(objUser);
        }
    }
}