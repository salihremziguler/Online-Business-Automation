using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,B")]
    public class StatisticsController : Controller
    {
        // GET: Statistics
        Context c = new Context();
        public ActionResult Index()
        {
            var values1 = c.Currents.Count().ToString();
            ViewBag.d1 = values1;
            var values2 = c.Products.Count().ToString();
            ViewBag.d2 = values2;
            var values3 = c.Personels.Count().ToString();
            ViewBag.d3 = values3;
            var values4 = c.Categories.Count().ToString();
            ViewBag.d4 = values4;
            var values5 = c.Products.Sum(x => x.Stock).ToString();
            ViewBag.d5 = values5;

            //Count of brand
            var values6 = (from x in c.Products select x.Brand).Distinct().Count().ToString();
            ViewBag.d6 = values6;

            var values7 = c.Products.Count(x => x.Stock <= 20).ToString();
            ViewBag.d7 = values7;

            var values8 = (from x in c.Products orderby x.SalesPrice descending select x.ProductName).FirstOrDefault();
            ViewBag.d8 = values8;

            var values9 = (from x in c.Products orderby x.SalesPrice ascending select x.ProductName).FirstOrDefault();
            ViewBag.d9 = values9;

            //İsmi en çok geçen marka
            var values12 = c.Products.GroupBy(x => x.Brand).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.d12 = values12;

            var values10 = c.Products.Count(x => x.ProductName == "TV").ToString();
            ViewBag.d10 = values10;

            var values11 = c.Products.Count(x => x.ProductName == "Su Isitici").ToString();
            ViewBag.d11 = values11;

            var values13 = c.Products.Where(u => u.ProductID == c.SalesMovements.GroupBy(x => x.ProductID).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault()).Select(k => k.ProductName).FirstOrDefault();
            ViewBag.d13 = values13;

            var values14 = c.SalesMovements.Sum(x => x.TotalPrice).ToString();
            ViewBag.d14 = values14;

            DateTime today = DateTime.Today;
            var values15 = c.SalesMovements.Count(x => x.Date == today).ToString();
            ViewBag.d15 = values15;

            var values16 = c.SalesMovements.Where(x => x.Date == today).Sum(y => (decimal?)y.TotalPrice).ToString();
            ViewBag.d16 = values16;
            return View();
        }
        public ActionResult SimplesTable()
        {
            var result = from x in c.Currents
                         where x.Status == true
                         group x by x.CurrentCity
                       into g
                         select new GroupClass
                         {
                             City = g.Key,
                             Total = g.Count()

                         };
            var t = c.Currents.Count();
            
            ViewBag.CariCount = t;
            return View(result.ToList());
          
        }
        public PartialViewResult Partial1()
        {
            var result = from x in c.Personels
                         group x by x.Department.DepartmentName into g
                         select new GroupClass2
                         {
                             Department = g.Key,
                             Count = g.Count()

                         };

            ViewBag.DepartmentCount = c.Departments.Count();
            return PartialView(result.ToList());
          
        }

        public PartialViewResult PartialBrand()
        {
            var result = from x in c.Products
                         where x.Status == true
                         group x by x.Brand into g
                         select new GroupClass3
                         {
                             Brand = g.Key,
                             Count = g.Count()
                         };

            ViewBag.BrandCount = c.Products.Count();
            return PartialView(result.ToList()); ;
        }
        public PartialViewResult PartialProduct()
        {
            var result = c.Currents.ToList();
            return PartialView(result);
        }
        public PartialViewResult Partial3()
        {
            var result = c.Products.Where(x=>x.Status==true).ToList();
            return PartialView(result);
        }
        public PartialViewResult Partial5()
        {
            var values4 = from x in c.Products
                          where x.Status == true
                          group x by x.ProductName into g
                          select new ClassGroup4
                          {
                              ProductsName = g.Key,
                              Total = g.Count()
                          };
            return PartialView(values4.ToList());
        }
    }
}