using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,B")]
    public class SalesController : Controller
    {
        // GET: Sales
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.SalesMovements.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddSales()
        {
            List<SelectListItem> value1 = (from x in c.Products.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.ProductName,
                                               Value = x.ProductID.ToString()
                                           }).ToList();
            ViewBag.value = value1;

            List<SelectListItem> value2 = (from x in c.Currents.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CurrentName + " " + x.CurrentSurname,
                                               Value = x.CurrentID.ToString()
                                           }).ToList();
            ViewBag.value_1 = value2;

            List<SelectListItem> value3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelName + " " + x.PersonelSurname,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.value_2 = value3;

            return View();
        }
        [HttpPost]
        public ActionResult AddSales(SalesMovement s)
        {
            s.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SalesMovements.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult BringSales(int id)
        {
            List<SelectListItem> value1 = (from x in c.Products.ToList() select new SelectListItem { Text = x.ProductName, Value = x.ProductID.ToString() }).ToList();
            List<SelectListItem> value2 = (from x in c.Currents.ToList() select new SelectListItem { Text = x.CurrentName + " " + x.CurrentSurname, Value = x.CurrentID.ToString() }).ToList();
            List<SelectListItem> value3 = (from x in c.Personels.ToList() select new SelectListItem { Text = x.PersonelName + " " + x.PersonelSurname, Value = x.PersonelID.ToString() }).ToList();
            ViewBag.value = value1;
            ViewBag.value_1 = value2;
            ViewBag.value_2 = value3;
            var values = c.SalesMovements.Find(id);
            return View("BringSales", values);
        }
        public ActionResult UpdateSales(SalesMovement q)
        {
            var values = c.SalesMovements.Find(q.SalesID);
            values.ProductID = q.ProductID;
            values.CurrentID = q.CurrentID;
            values.PersonelID = q.PersonelID;
            values.Piece = q.Piece;
            values.Price = q.Price;
            values.TotalPrice = q.TotalPrice;
            values.Date = q.Date;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SalesDetails(int id)
        {
            var values = c.SalesMovements.Where(x => x.SalesID == id).ToList();
            return View(values);
        }
        public ActionResult SaleList()
        {
            var values = c.SalesMovements.ToList();
            return View(values);
        }
    }
}