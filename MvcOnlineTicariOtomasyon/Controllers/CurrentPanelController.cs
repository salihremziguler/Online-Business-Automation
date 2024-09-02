using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Models.Classes
{
    
    public class CurrentPanelController : Controller
    {
        // GET: CurrentPanel
        Context c = new Context();
        
        public ActionResult Index()
        {
            var mail = (string)Session["CurrentMail"];
           // var values = c.Currents.FirstOrDefault(x => x.CurrentMail == mail);
            var values2 = c.Messages.Where(x => x.Receiver == mail && x.Status == true).ToList();
            ViewBag.m = mail;
            var mailid = c.Currents.Where(x => x.CurrentMail == mail).Select(y => y.CurrentID).FirstOrDefault();
            ViewBag.mid = mailid;
            var totalsales = c.SalesMovements.Where(x => x.CurrentID == mailid).Count();
            ViewBag.ts = totalsales;
            var totalpayment = c.SalesMovements.Where(y => y.CurrentID == mailid).Sum(x => (decimal?)x.TotalPrice).ToString();
            ViewBag.tp = totalpayment;
            var totalpiece = c.SalesMovements.Where(x => x.CurrentID == mailid).Sum(y => (int?)y.Piece).ToString();
            ViewBag.tpe = totalpiece;
            var namesurname = c.Currents.Where(x => x.CurrentMail == mail).Select(y => y.CurrentName + " " + y.CurrentSurname).FirstOrDefault();
            ViewBag.nmsrn = namesurname;
           var image = c.Currents.Where(x => x.CurrentMail == mail).Select(y => y.CurrentImage).FirstOrDefault();
            ViewBag.img = image;
            var city = c.Currents.Where(x => x.CurrentMail == mail).Select(y => y.CurrentCity).FirstOrDefault();
            ViewBag.cy = city;
            var mail2 = c.Currents.Where(x => x.CurrentMail == mail).Select(y => y.CurrentMail).FirstOrDefault();
            ViewBag.cm = mail2;

            return View(values2);
            
        }
          public ActionResult MyOrder()
          {
            //Sisteme giriş yapan mail adresini sessiona atama
              var values = (string)Session["CurrentMail"];
              var id = c.Currents.Where(x => x.CurrentMail == values.ToString()).Select(y => y.CurrentID).FirstOrDefault();
              var values2 = c.SalesMovements.Where(x => x.CurrentID == id).ToList();
              return View(values2);
          }
          public ActionResult IncomingMessages()
          {
              var mail = (string)Session["CurrentMail"];
              var values2 = c.Messages.Where(x => x.Receiver == mail).Where(x => x.Status == true).OrderByDescending(x => x.MessageId).ToList();
              //Count messages
              var values3 = c.Messages.Where(x => x.Status == true).Count(x => x.Receiver == mail).ToString();
              ViewBag.d1 = values3;
              var values4 = c.Messages.Where(x => x.Status == true).Count(x => x.Sender == mail).ToString();
              ViewBag.d2 = values4;
              var values5 = c.Messages.Where(x => x.Status == false).Count(x => x.Receiver == mail || x.Sender == mail).ToString();
              ViewBag.d3 = values5;
              var starry = c.Messages.Where(x => x.Status == true && x.Starry == true).Count(x => x.Receiver == mail || x.Sender == mail).ToString();
              ViewBag.starry = starry;
              return View(values2);
          }

        [HttpGet]
        public ActionResult NewMessage()
        {
            var values = (string)Session["CurrentMail"];
            var values3 = c.Messages.Where(x => x.Status == true).Count(x => x.Receiver == values).ToString();
            ViewBag.d1 = values3;
            var values4 = c.Messages.Where(x => x.Status == true).Count(x => x.Sender == values).ToString();
            ViewBag.d2 = values4;
            var values5 = c.Messages.Where(x => x.Status == false).Count(x => x.Receiver == values || x.Sender == values).ToString();
            ViewBag.d3 = values5;
            var starry = c.Messages.Where(x => x.Status == true && x.Starry == true).Count(x => x.Receiver == values || x.Sender == values).ToString();
            ViewBag.starry = starry;
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message m)
        {
            var values = (string)Session["CurrentMail"];
            var values3 = c.Messages.Where(x => x.Status == true).Count(x => x.Receiver == values).ToString();
            ViewBag.d1 = values3;
            var values4 = c.Messages.Where(x => x.Status == true).Count(x => x.Sender == values).ToString();
            ViewBag.d2 = values4;
            var values5 = c.Messages.Where(x => x.Status == false).Count(x => x.Receiver == values || x.Sender == values).ToString();
            ViewBag.d3 = values5;
            var starry = c.Messages.Where(x => x.Status == true && x.Starry == true).Count(x => x.Receiver == values || x.Sender == values).ToString();
            ViewBag.starry = starry;

            m.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Sender = values;
            m.Status = true;
            c.Messages.Add(m);
            c.SaveChanges();
            return RedirectToAction("IncomingMessages");
        }

         public ActionResult OutgoingMessages()
         {
             var values = (string)Session["CurrentMail"];
             var values2 = c.Messages.Where(x => x.Sender == values).Where(x => x.Status == true).OrderByDescending(x => x.MessageId).ToList();
             var values3 = c.Messages.Where(x => x.Status == true).Count(x => x.Receiver == values).ToString();
             ViewBag.d1 = values3;
             var values4 = c.Messages.Where(x => x.Status == true).Count(x => x.Sender == values).ToString();
             ViewBag.d2 = values4;
             var values5 = c.Messages.Where(x => x.Status == false).Count(x => x.Receiver == values || x.Sender == values).ToString();
             ViewBag.d3 = values5;
             var starry = c.Messages.Where(x => x.Status == true && x.Starry == true).Count(x => x.Receiver == values || x.Sender == values).ToString();
             ViewBag.starry = starry;
             return View(values2);
         }
         public ActionResult MessageDetails(int id)
         {
             var values = (string)Session["CurrentMail"];
             var values2 = c.Messages.Where(x => x.MessageId == id).ToList();
             var values3 = c.Messages.Where(x => x.Status == true).Count(x => x.Receiver == values).ToString();
             ViewBag.d1 = values3;
             var values4 = c.Messages.Where(x => x.Status == true).Count(x => x.Sender == values).ToString();
             ViewBag.d2 = values4;
             var values5 = c.Messages.Where(x => x.Status == false).Count(x => x.Receiver == values || x.Sender == values).ToString();
             ViewBag.d3 = values5;
             var starry = c.Messages.Where(x => x.Status == true && x.Starry == true).Count(x => x.Receiver == values || x.Sender == values).ToString();
             ViewBag.starry = starry;
             return View(values2);
         }

         public ActionResult Delete(int id)
         {
             var values = c.Messages.Find(id);
             values.Status = false;
             c.SaveChanges();
             return RedirectToAction("Trash");
         }
         public ActionResult TakeItBack(int id)
         {
             var values = c.Messages.Find(id);
             values.Status = true;
             c.SaveChanges();
             return RedirectToAction("Trash");
         }
         public ActionResult AddStarry(int id)
         {
             var values = c.Messages.Find(id);
             values.Starry = true;
             c.SaveChanges();
             return RedirectToAction("Starry");
         }
         public ActionResult DeleteStarry(int id)
         {
             var values = c.Messages.Find(id);
             values.Starry = false;
             c.SaveChanges();
             return RedirectToAction("Starry");
         }
         public ActionResult Delete2(int id)
         {
             var values = c.Messages.Find(id);
             values.Status = false;
             c.SaveChanges();
             return RedirectToAction("Trash");
         }
         public ActionResult Trash()
         {
             var values = (string)Session["CurrentMail"];
             var values2 = c.Messages.Where(x => x.Receiver == values || x.Sender == values).Where(x => x.Status == false).OrderByDescending(x => x.MessageId).ToList();
             var values3 = c.Messages.Where(x => x.Status == true).Count(x => x.Receiver == values).ToString();
             ViewBag.d1 = values3;
             var values4 = c.Messages.Where(x => x.Status == true).Count(x => x.Sender == values).ToString();
             ViewBag.d2 = values4;
             var values5 = c.Messages.Where(x => x.Status == false).Count(x => x.Receiver == values || x.Sender == values).ToString();
             var starry = c.Messages.Where(x => x.Status == true && x.Starry == true).Count(x => x.Receiver == values || x.Sender == values).ToString();
             ViewBag.starry = starry;
             ViewBag.d3 = values5;
             return View(values2);
         }
         public ActionResult Starry()
         {
             var values = (string)Session["CurrentMail"];
             var values2 = c.Messages.Where(x => x.Receiver == values || x.Sender == values).Where(x => x.Starry == true).OrderByDescending(x => x.MessageId).ToList();
             var values3 = c.Messages.Where(x => x.Status == true).Count(x => x.Receiver == values).ToString();
             ViewBag.d1 = values3;
             var values4 = c.Messages.Where(x => x.Status == true).Count(x => x.Sender == values).ToString();
             ViewBag.d2 = values4;
             var values5 = c.Messages.Where(x => x.Status == false).Count(x => x.Receiver == values || x.Sender == values).ToString();
             ViewBag.d3 = values5;
             var starry = c.Messages.Where(x => x.Status == true && x.Starry == true).Count(x => x.Receiver == values || x.Sender == values).ToString();
             ViewBag.starry = starry;
             return View(values2);
         }
         public ActionResult DeleteTrash(int id)
         {
             var values = c.Messages.Find(id);
             c.Messages.Remove(values);
             c.SaveChanges();
             return RedirectToAction("Trash");
         }
         public ActionResult CargoTracking(string p)
         {
             var values = from x in c.cargoDetails select x;
             values = values.Where(y => y.TrackingCode.Contains(p));
             return View(values.ToList());
         }
         public ActionResult Details(string id)
         {
             var values = c.cargoTrackings.Where(x => x.TrackingCode == id).ToList();
             return View(values);
         }

         [HttpPost]
         public ActionResult Update(Current p)
         {
             var values = (string)Session["CurrentMail"];
             var values2 = c.Currents.Where(x => x.CurrentMail == values.ToString()).Select(y => y.CurrentID).FirstOrDefault();
             var values3 = c.Currents.Find(values2);
             values3.CurrentName = p.CurrentName;
             values3.CurrentSurname = p.CurrentSurname;
             values3.CurrentCity = p.CurrentCity;
             values3.CurrentPassword = p.CurrentPassword;

             if (p.CurrentImage != null)
             {
                 if (Request.Files.Count > 0)
                 {
                     string filesname = Path.GetFileName(Request.Files[0].FileName);
                     string extension = Path.GetExtension(Request.Files[0].FileName);
                     string road = "~/Image/" + filesname + extension;
                     Request.Files[0].SaveAs(Server.MapPath(road));
                     p.CurrentImage = "/Image/" + filesname + extension;
                     values3.CurrentImage = p.CurrentImage;
                 }
             }

             c.SaveChanges();
             return RedirectToAction("Index");
         }
        [HttpGet]
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CurrentMail"];
            var id = c.Currents.Where(x => x.CurrentMail == mail).Select(y => y.CurrentID).FirstOrDefault();
            var findcurrent = c.Currents.Find(id);
            return PartialView("Partial1", findcurrent);
        }
        public PartialViewResult Partial2()
        {
            var values = c.Messages.Where(x => x.Sender == "admin").OrderByDescending(x => x.MessageId).ToList();
            return PartialView(values);
        }
        public ActionResult SearchMail(string q)
        {
            var mail = (string)Session["CurrentMail"];

            var word = HttpContext.Request.QueryString["q"].ToString();
            var values = c.Messages.Where(x => x.Receiver.ToLower().Contains(word.ToLower()) == word.ToLower().Contains(word.ToLower()) || x.Title.ToLower().Contains(word.ToLower()) == word.ToLower().Contains(word.ToLower()) || x.Sender.ToLower().Contains(word.ToLower()) == word.ToLower().Contains(word.ToLower()) || x.Content.ToLower().Contains(word.ToLower()) == word.ToLower().Contains(word.ToLower())).Where(x => x.Receiver == mail || x.Sender == mail).ToList();

            var values3 = c.Messages.Where(x => x.Status == true).Count(x => x.Receiver == mail).ToString();
            ViewBag.d1 = values3;
            var values4 = c.Messages.Where(x => x.Status == true).Count(x => x.Sender == mail).ToString();
            ViewBag.d2 = values4;
            var values5 = c.Messages.Where(x => x.Status == false).Count(x => x.Receiver == mail || x.Sender == mail).ToString();
            ViewBag.d3 = values5;
            var starry = c.Messages.Where(x => x.Status == true && x.Starry == true).Count(x => x.Receiver == mail || x.Sender == mail).ToString();
            ViewBag.starry = starry;

            return View(values);
        }
    }
}