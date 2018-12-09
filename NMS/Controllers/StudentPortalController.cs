using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NMS.Models;
namespace NotesManagementSystems.Controllers
{
    public class StudentPortalController : Controller
    {
        // GET: StudentPortal
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public FileResult DownLoadFile(int id)
        {


            List<Note> ObjFiles = GetFileList();

            var FileById = (from FC in ObjFiles
                            where FC.NotesID.Equals(id)
                            select new { FC.TID, FC.ClassID, FC.ModuleID, FC.SubID, FC.PDFFile }).ToList().FirstOrDefault();

            string Filename = FileById.SubID + "Module" + FileById.ModuleID + ".pdf";
            return File(FileById.PDFFile, "application/pdf", Filename);

        }

        [HttpGet]
        public PartialViewResult FileDetails()
        {
            List<Note> DetList = GetFileList();

            return PartialView("FileDetails", DetList);


        }
        private List<Note> GetFileList()
        {
            NMSEntities db = new NMSEntities();
            List<Note> DetList = new List<Note>();

            // DbConnection();
            //con.Open();
            //object v = (TempData["TID"]) as string;
            //string Tid = v.ToString();
            //string Tid= (TempData["TID"]) as string;
            //string Tid = Session["TID"].ToString();
            // Note n = new Models.Note();
            DetList = db.GetTeachersPdfNotes().ToList();
            //con.Close();

            return DetList;
        }
    }
}
