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
    public class ToDoListController : Controller
    {// GET: ToDoList
        Context c = new Context();
        public ActionResult Index()
        {
            var values1 = c.Currents.Count().ToString();
            ViewBag.v1 = values1;
            var values2 = c.Products.Count().ToString();
            ViewBag.v2 = values2;
            var values3 = c.Categories.Count().ToString();
            ViewBag.v3 = values3;
            var values4 = (from x in c.Currents select x.CurrentCity).Distinct().Count().ToString();
            ViewBag.v4 = values4;
            var values = c.ToDoLists.OrderByDescending(x => x.ToDoListID).Take(12).ToList();
            return View(values);

        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ToDoList p)
        {
            p.Status = true;
            c.ToDoLists.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var values = c.ToDoLists.Find(id);
            c.ToDoLists.Remove(values);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult BringToDoList(int id)
        {
            var values = c.ToDoLists.Find(id);
            return View("BringToDoList", values);
        }
        [HttpPost]
        public ActionResult UpdateToDoList(ToDoList p)
        {
            var values = c.ToDoLists.Find(p.ToDoListID);
            values.Title = p.Title;
            values.Date = p.Date;
            values.Hour = p.Hour;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "A")]
        public ActionResult Admins()
        {
            var admin = c.Admins.Where(x => x.Status == true).ToList();
            return View(admin);
        }
        [Authorize(Roles = "A")]
        public ActionResult DeleteAdmin(int id)
        {
            var value = c.Admins.Find(id);
            value.Status = false;
            c.SaveChanges();
            return RedirectToAction("Admins");
        }
        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult AddAdmin()
        {
            ViewBag.Authority = new List<string>() { "A", "B" };
            return View();
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            ViewBag.Authority = new List<string>() { "A", "B" };
            c.Admins.Add(admin);
            admin.Status = true;
            c.SaveChanges();
            return RedirectToAction("Admins");
        }
        [Authorize(Roles = "A")]
        [HttpGet]
        public ActionResult UpdateAdmin(int id)
        {
            var value = c.Admins.Find(id);
            ViewBag.Authority = new List<string>() { "SuperAdmin", "Admin" };
            return View("UpdateAdmin", value);
        }
        [Authorize(Roles = "A")]
        [HttpPost]
        public ActionResult UpdateAdmin(Admin admin)
        {
            var values = c.Admins.Find(admin.AdminID);
            ViewBag.Authority = new List<string>() { "SuperAdmin", "Admin" };
            values.Authority = admin.Authority;
            c.SaveChanges();
            return RedirectToAction("Admins");
        }
       
      
        
      
        
    }
}