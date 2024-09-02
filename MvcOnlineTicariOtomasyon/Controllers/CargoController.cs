using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,B")]
    public class CargoController : Controller
    {
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var values = from x in c.cargoDetails select x;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(y => y.TrackingCode.Contains(p));
            }
            return View(values.ToList());
        }
        [HttpGet]
        public ActionResult AddCargo()
        {
            //create random CargoTracking code.
            Random rnd = new Random();
            string[] characters = { "A", "B", "C", "D" };
            int c1, c2, c3;
            c1 = rnd.Next(0, 4);
            c2 = rnd.Next(0, 4);
            c3 = rnd.Next(0, 4);
            int s1, s2, s3;
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);
            string code = s1.ToString() + characters[c1] + s2 + characters[c2] + s3 + characters[c3];
            ViewBag.tcode = code;
            return View();
        }
        [HttpPost]
        public ActionResult AddCargo(CargoDetail p)
        {
            if (!ModelState.IsValid)
            {
                return View("Add");
            }
            c.cargoDetails.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //Cargo Tracking
        public ActionResult DetailCargo(string id)
        {
            var values = c.cargoTrackings.Where(x => x.TrackingCode == id).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult DescriptionCargo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DescriptionCargo(CargoTracking cargoTracking, string id)
        {
            cargoTracking.TrackingCode = id;
            c.cargoTrackings.Add(cargoTracking);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}