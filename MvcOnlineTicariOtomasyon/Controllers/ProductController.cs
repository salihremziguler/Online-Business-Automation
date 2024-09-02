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
    public class ProductController : Controller
    {

        Context c = new Context();

        // GET: Product
        public ActionResult Index(string p)
        {
            var result = from product in c.Products select product;
            if (!string.IsNullOrEmpty(p))
            {
                result = result.Where(y => y.ProductName.Contains(p) );
            }
            return View(result.Where(product => product.Status == true).ToList());
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            //DropDown
            List<SelectListItem> value1 = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.value = value1;

            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            if (p.ProductImagePath != null)
            {
                if (Request.Files.Count > 0)
                {
                    string filesname = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string road = "~/Image/" + filesname + extension;
                    Request.Files[0].SaveAs(Server.MapPath(road));
                    p.ProductImagePath = "/Image/" + filesname + extension;
                }
            }

            p.Status = true;
           
            c.Products.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProduct(int id)
        {
            var value = c.Products.Find(id);
            value.Status = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BringProduct(int id)
        {
            List<SelectListItem> value1 = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.value= value1;
            var value_ = c.Products.Find(id);
            return View("BringProduct", value_);
        }

        public ActionResult UpdateProduct(Product q)
        {
            var values = c.Products.Find(q.ProductID);
            values.ProductName = q.ProductName;
            values.Brand = q.Brand;
            values.Stock = q.Stock;
            values.PurchasePrice = q.PurchasePrice;
            values.SalesPrice = q.SalesPrice;
            values.CategoryId = q.CategoryId;

            values.Status = true;



            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                    string filesname = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string road = "~/Image2/" + filesname + extension;
                    Request.Files[0].SaveAs(Server.MapPath(road));
                    q.ProductImagePath = "/Image2/" + filesname + extension;
                    values.ProductImagePath = q.ProductImagePath;
                
            }

            c.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult ListProduct()
        {
            var values = c.Products.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult SellProduct(int id)
        {
            List<SelectListItem> value1 = (from x in c.Personels.ToList() select new SelectListItem { Text = x.PersonelName + " " + x.PersonelSurname, Value = x.PersonelID.ToString() }).ToList();
            ViewBag.valuee1 = value1;

            List<SelectListItem> current = (from x in c.Currents.ToList() select new SelectListItem { Text = x.CurrentName + " " + x.CurrentSurname, Value = x.CurrentID.ToString() }).ToList();
            ViewBag.current = current;

            var value2 = c.Products.Find(id);
            ViewBag.valuee2 = value2.ProductID;
            ViewBag.valuee3 = value2.SalesPrice;
            return View();
        }
        [HttpPost]
        public ActionResult SellProduct(SalesMovement p)
        {
            p.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SalesMovements.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index", "Sales");
        }
        public ActionResult Gallery()
        {
            var values = c.Products.ToList();
            return View(values);
        }
    }
}