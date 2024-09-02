using MvcOnlineTicariOtomasyon.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Register()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Register(Current current)
        {
            current.Status = true;
            c.Currents.Add(current);
            c.SaveChanges();

            ViewBag.RegisterSuccess = true;
            return PartialView();
        }
        [HttpGet]
        public ActionResult CurrentLogin()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult CurrentLogin(Current q)
        {
            var values = c.Currents.FirstOrDefault(x => x.CurrentMail == q.CurrentMail && x.CurrentPassword == q.CurrentPassword);
            if (values != null)
            {
                FormsAuthentication.SetAuthCookie(values.CurrentName, false);
                Session["CurrentMail"] = values.CurrentMail.ToString();
                Session.Timeout = 60;
                return RedirectToAction("Index", "CurrentPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
         public ActionResult Logout()
         {
             FormsAuthentication.SignOut();
             Session.Abandon();
             return RedirectToAction("Index", "Login");
         }


        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {
            var values = c.Admins.FirstOrDefault(x => x.Name == p.Name && x.Password == p.Password);
            if (values != null)
            {
                FormsAuthentication.SetAuthCookie(values.Name, false);
                Session["Name"] = values.Name.ToString();
                return RedirectToAction("Index", "ToDoList");
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

    }
}