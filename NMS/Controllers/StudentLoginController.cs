using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NMS.Models;

namespace NMS.Controllers
{
    public class StudentLoginController : Controller
    {
        // GET: StudentLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Student objUser)
        {
            if (ModelState.IsValid)
            {
                using (NMSEntities db = new NMSEntities())
                {
                    var obj = db.Students.Where(a => a.USN.Equals(objUser.USN) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["USN"] = obj.USN.ToString();
                        Session["Name"] = obj.Name.ToString();
                        return RedirectToAction("Index", "StudentPortal");
                    }
                }
            }
            return View(objUser);
        }
    }
}