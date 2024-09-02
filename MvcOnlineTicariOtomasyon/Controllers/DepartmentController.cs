using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,B")]
    public class DepartmentController : Controller
    {
       // GET: Department
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.Departments.Where(x=>x.Status==true).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDepartment(Department q)
        {
            q.Status = true;
            /*if (!ModelState.IsValid)
            {
                return View("Add");
            }*/
            c.Departments.Add(q);
           // q.Status = true;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteDepartment(int id)
        {
            var values = c.Departments.Find(id);
            values.Status = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BringDepartment(int id)
        {
            var values = c.Departments.Find(id);
            return View("BringDepartment", values);
        }
        public ActionResult UpdateDepartment(Department d)
        {
           /* if (!ModelState.IsValid)
            {
                return View("BringDepartment");
            }*/
            var values = c.Departments.Find(d.DepartmentID);
            values.DepartmentName = d.DepartmentName;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DetailsDepartment(int id)
        {
            var values = c.Personels.Where(x => x.DepartmentID == id).ToList();
            var values2 = c.Departments.Where(x => x.DepartmentID == id).Select(y => y.DepartmentName).FirstOrDefault();
            ViewBag.dp = values2;
            return View(values);
        
        }
        public ActionResult DepartmentPersonelSales(int id)
        {
            var values = c.SalesMovements.Where(x => x.PersonelID == id).ToList();
            var values2 = c.Personels.Where(x => x.DepartmentID == id).Select(y => y.PersonelName + " " + y.PersonelSurname).FirstOrDefault();
            ViewBag.dpers = values2;
            return View(values);
           
        }
        




    }
}