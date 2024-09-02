using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,B")]
    public class PersonelController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.Personels.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddPersonel()
        {
            List<SelectListItem> value1 = (from x in c.Departments.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmentName,
                                               Value = x.DepartmentID.ToString()
                                           }).ToList();
            ViewBag.value2 = value1;
            return View();
        }
        [HttpPost]
        public ActionResult AddPersonel(Personel s)
        {
            if (s.PersonelImagePath != null)
            {
                if (Request.Files.Count > 0)
                {
                    string filesname = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string road = "~/Image/" + filesname + extension;
                    Request.Files[0].SaveAs(Server.MapPath(road));
                    s.PersonelImagePath = "/Image/" + filesname + extension;
                }
            }

            c.Personels.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BringPersonel(int id)
        {
            List<SelectListItem> value3 = (from x in c.Departments.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmentName,
                                               Value = x.DepartmentID.ToString()
                                           }).ToList();
            ViewBag.value = value3;
            var values = c.Personels.Find(id);
            return View("BringPersonel", values);
        }
        public ActionResult UpdatePersonel(Personel s)
        {
            var values = c.Personels.Find(s.PersonelID);
            values.PersonelName = s.PersonelName;
            values.PersonelSurname = s.PersonelSurname;
            values.DepartmentID = s.DepartmentID;
            values.PersonelImagePath = s.PersonelImagePath;
           // values. = s.Address;
           // values.Phone = s.Phone;

            if (s.PersonelImagePath != null)
            {
                if (Request.Files.Count > 0)
                {
                    string filesname = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string road = "~/Image/" + filesname + extension;
                    Request.Files[0].SaveAs(Server.MapPath(road));
                    s.PersonelImagePath = "/Image/" + filesname + extension;
                    values.PersonelImagePath = s.PersonelImagePath;
                }
            }

            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelList()
        {
            var values = c.Personels.ToList();
            return View(values);
        }

      
    }
}