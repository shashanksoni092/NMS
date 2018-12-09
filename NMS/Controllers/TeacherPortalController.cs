using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NMS.Models;

namespace NotesManagementSystems.Controllers
{
    public class TeacherPortalController : Controller
    {
        // GET: TeacherPortal
        public ActionResult Index()
        {
            if(Session["TID"]==null)
            {
                return RedirectToAction("Index", "Main");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Main");
        }
        public ActionResult FileUpload()
        {

            if (Session["TID"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            NMSEntities db = new NMSEntities();
            ViewBag.ModuleID = new SelectList(db.Modules, "ModuleID", "ModuleName");
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName");
            ViewBag.ClassID = new SelectList(db.Classes, "classID", "ClassName");

            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(Note notes )
        {
            // object v = (TempData["TID"]);
            //string Tid = v.ToString();
            if (ModelState.IsValid)
            {
                using (NMSEntities db = new NMSEntities())
                {
                    String FileExt = Path.GetExtension(notes.files.FileName).ToUpper();
                    if(FileExt==".PDF")
                    {
                        Stream str = notes.files.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                        Note n = new NMS.Models.Note();


                        n.PDFFile = FileDet;
                        
                        //SaveFileDetails(n);
                        db.AddNotes(notes.TID, notes.ModuleID, notes.SubID, notes.ClassID, n.PDFFile);
                        return RedirectToAction("FileUpload");

                    }
                    else
                    {

                        ViewBag.FileStatus = "Invalid file format.";
                        return View();

                    }
                }
            }
            return View();
        }

        [HttpGet]
        public FileResult DownLoadFile(int id)
        {


            List<Note> ObjFiles = GetFileList();

            var FileById = (from FC in ObjFiles
                            where FC.NotesID.Equals(id)
                            select new { FC.TID, FC.ClassID,FC.ModuleID,FC.SubID,FC.PDFFile }).ToList().FirstOrDefault();

            string Filename = FileById.SubID+"Module"+FileById.ModuleID + ".pdf";
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
            string Tid = Session["TID"].ToString();
            // Note n = new Models.Note();
            DetList = db.GetAllTHEPDFFilesByOrder(Tid).ToList();
            
            //con.Close();

            return DetList;
        }
        public ActionResult main()
        {
            return View();
        }

        public ActionResult CreateGroup()
        {
            if (Session["TID"] == null)
            {
                return RedirectToAction("Index", "Main");
            }
            NMSEntities db = new NMSEntities();
            ViewBag.SubID = new SelectList(db.Subjects, "SubID", "SubName");
            ViewBag.ClassID = new SelectList(db.Classes, "classID", "ClassName");

            return View();
        }
        
        [HttpPost]
        public ActionResult CreateGroup(NGroup group)
        {
            if (ModelState.IsValid)
            {
                using (NMSEntities db = new NMSEntities())
                {
                    String FileExt = Path.GetExtension(group.files.FileName).ToUpper();
                    if (FileExt == ".PDF")
                    {
                        Stream str = group.files.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                        NGroup n = new NMS.Models.NGroup();


                       n.GroupIcon = FileDet;

                        //SaveFileDetails(n);
                       db.NMakeGroups(group.GroupName, n.GroupIcon, group.classID, group.subID);
                        
                       var a=db.NGetGroupIDsss(group.GroupName).FirstOrDefault();
                       int groupid = a.GroupID;
                        
                       db.NEInsertGroupMemberTeachers(groupid, Session["TID"].ToString(),Session["Name"].ToString());

                        return RedirectToAction("FileUpload");
                        
                    }
                    else
                    {

                        ViewBag.FileStatus = "Invalid file format.";
                        return View();

                    }
                }
            }
            return View();
        }

        /*    [HttpGet]
            public ActionResult GroupMessage()
            {
                List<GroupMessageTeacher> DetList = GetGroupMessage();

                return PartialView("GroupMessage", DetList);
            }
            private List<GroupMessageTeacher> GetGroupMessage()
            {
                NMSEntities db = new NMSEntities();
                List<GroupMessageTeacher> DetList = new List<GroupMessageTeacher>();

                string Tid = Session["TID"].ToString();

                DetList = db.CreateGroupMessageTeachers("1").ToList();


                return DetList;
            }
        */
        public ActionResult GroupMessage(int id)
        {
            Message message = new Message();
            List<NGroupMessageTeacher1> DetList = message.GetMessage(id);


            return View(DetList);
        }

        [HttpGet]
        public ActionResult JoinGroup()
        {
            JoinGroup join = new JoinGroup();
            List<NGroup> DetList = join.GetGroups();
            return View(DetList);
        }
        
        public ActionResult JoinGroupButton(int id)
        {
            NMSEntities db = new NMSEntities();
            db.NEInsertGroupMemberTeachers(id, Session["TID"].ToString(), Session["Name"].ToString());

            return RedirectToAction("GroupMessage",new {id=id });
        }

        [HttpGet]
        public ActionResult GetGroupChat()
        {
            string tid = Session["TID"].ToString();
            GroupChat chat=new GroupChat();
            List<NGroup> DetList = chat.GetGroupChat(tid);
            return View(DetList);
        }

        public ActionResult chat(int id)
        {
            Session["id"] = id;
            return RedirectToAction("GroupMessage", new { id = id });
        }

        public ActionResult WriteMessage(int id)
        {
           
            return View();
        }
        
        [HttpPost]
        public ActionResult WriteMessage(NGroupMessageTeacher1 nGroupMessageTeacher1)
        {
            if (ModelState.IsValid)
            {
                using (NMSEntities db = new NMSEntities())
                {
                    String FileExt = Path.GetExtension(nGroupMessageTeacher1.files.FileName).ToUpper();
                    if (FileExt == ".PDF")
                    {
                        Stream str = nGroupMessageTeacher1.files.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                        NGroupMessageTeacher1 n = new NMS.Models.NGroupMessageTeacher1();

                        //TimeSpan.FromTicks(DateTime.Now.Ticks)
                        n.Media = FileDet;
                        string id = Session["id"].ToString();
                        int ids = Convert.ToInt32(id);
                        //SaveFileDetails(n);
                        db.InsertMessageIntoGroup(ids,Session["TID"].ToString(),nGroupMessageTeacher1.message,DateTime.Now.TimeOfDay,n.Media);
                        //db.AddNotes(notes.TID, notes.ModuleID, notes.SubID, notes.ClassID, n.PDFFile);
                        return RedirectToAction("chat",new {id=ids});

                    }
                    else
                    {

                        ViewBag.FileStatus = "Invalid file format.";
                        return View();

                    }
                }
            }
            return View();
        }

    }
}