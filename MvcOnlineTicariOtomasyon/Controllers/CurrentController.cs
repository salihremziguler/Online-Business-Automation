using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,B")]
    public class CurrentController : Controller
    {
        // GET: Current
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.Currents.Where(x=>x.Status==true).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddCurrent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCurrent(Current cr)
        {
            cr.Status = true;
            c.Currents.Add(cr);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DeleteCurrent(int id)
        {
            var values = c.Currents.Find(id);
            values.Status = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DetailsCurrent(int id)
        {
            var values = c.SalesMovements.Where(x => x.CurrentID == id).ToList();
            var history = c.Currents.Where(x => x.CurrentID == id).Select(y => y.CurrentName + " " + y.CurrentSurname).FirstOrDefault();
            ViewBag.current = history;
            return View(values);
        }

        public ActionResult BringCurrent(int id)
        {
            var values = c.Currents.Find(id);
            return View("BringCurrent", values);
        }

        public ActionResult UpdateCurrent(Current cr)
        {
            if(!ModelState.IsValid)
            {
                return View("BringCurrent");
            }
            var current = c.Currents.Find(cr.CurrentID);
            current.CurrentName = cr.CurrentName;
            current.CurrentSurname = cr.CurrentSurname;
            current.CurrentCity = cr.CurrentCity;
            current.CurrentMail = cr.CurrentMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}