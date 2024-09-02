using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,B")]
    public class CategoryController : Controller
    {
        Context c = new Context();
        // GET: Category
        public ActionResult Index()
        {
            var values = c.Categories.ToList();
            return View(values);
        }
        [HttpGet]  //form load
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]  
        public ActionResult AddCategory(Category category)
        {
            c.Categories.Add(category);
            c.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int id)
        {
            var ctg = c.Categories.Find(id);
            c.Categories.Remove(ctg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BringCategory(int id)
        {
            var values = c.Categories.Find(id);
            return View("BringCategory", values);

        }

        public ActionResult UpdateCategory(Category q)
        {
            if (!ModelState.IsValid)
            {
                return View("BringCategory");
            }

            var values = c.Categories.Find(q.CategoryID);
            values.CategoryName = q.CategoryName;

            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult BringProduct(int p)
        {
            var productlist = (from x in c.Products
                               join y in c.Categories
                               on x.Category.CategoryID equals y.CategoryID
                               where x.Category.CategoryID == p
                               select new
                               {
                                   Text = x.ProductName,
                                   Value = x.ProductID.ToString()
                               });
            return Json(productlist, JsonRequestBehavior.AllowGet);
        }

    }
}