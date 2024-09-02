using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Graphic
        Context c = new Context();
        public ActionResult Index()
        {
            var values = c.Products.ToList();
            return View(values);
        }
       /* public ActionResult VisualizeProductResult2()
        {
            return Json(Productlist2(), JsonRequestBehavior.AllowGet);
        }
        public List<Class3> Productlist2()
        {
            List<Class3> cls = new List<Class3>();
            using (var c = new Context())
            {
                cls = c.Products.Select(x => new Class3
                {
                    prd = x.ProductsName,
                    stk = x.Stock

                }).ToList();
            }
            return cls;
        }
        public ActionResult Index6()
        {
            return View();
        }
        public ActionResult VisualizeProductResult3()
        {
            return Json(Productlist3(), JsonRequestBehavior.AllowGet);
        }
        public List<Class4> Productlist3()
        {
            List<Class4> cls = new List<Class4>();
            using (var c = new Context())
            {
                cls = c.Products.Select(x => new Class4
                {
                    bnd = x.Brand,
                    stk = x.Stock

                }).ToList();
            }
            return cls;
        }
        public ActionResult Index7()
        {
            return View();
        }
        public ActionResult VisualizeProductResult4()
        {
            return Json(Productlist4(), JsonRequestBehavior.AllowGet);
        }
        public List<Class5> Productlist4()
        {
            List<Class5> cls = new List<Class5>();
            using (var c = new Context())
            {
                cls = c.Products.Select(x => new Class5
                {
                    prd = x.ProductsName,
                    sp = x.Stock

                }).ToList();
            }
            return cls;
        }*/
    }
}