using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Classes;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,B")]
    public class InvoiceController : Controller
    {
        // GET: Invoices
        Context c = new Context();
        public ActionResult Index()
        {
            //InvoiceOperation ınvoiceOperation = new InvoiceOperation();
            //ınvoiceOperation.Value = context.Invoices.ToList();
            //ınvoiceOperation.Value2 = context.InvoicesItems.ToList();
            var values = c.Invoices.ToList();
            return View(values);
        }
       /* public ActionResult Delete(int id)
        {
            var result = context.Invoices.Find(id);
            context.Invoices.Remove(result);
            context.SaveChanges();
            return RedirectToAction("Index");
        }*/
        [HttpGet]
        public PartialViewResult AddInvoice()
        {
            List<SelectListItem> Personel = (from personel in c.Personels.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = personel.PersonelName,
                                                 Value = personel.PersonelID.ToString()
                                             }).ToList();
            ViewBag.personel = Personel;
            List<SelectListItem> Cari = (from current in c.Currents.ToList()
                                         select new SelectListItem
                                         {
                                             Text = current.CurrentName,
                                             Value = current.CurrentSurname.ToString()
                                         }).ToList();
            ViewBag.cari = Cari;
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddInvoice(Invoice ınvoice)
        {
            c.Invoices.Add(ınvoice);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BringInvoice(int id)
        {
           /* List<SelectListItem> Personel = (from personel in context.Personels.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = personel.Name,
                                                 Value = personel.Id.ToString()
                                             }).ToList();
            ViewBag.personel = Personel;
            List<SelectListItem> Cari = (from cari in context.Caris.ToList()
                                         select new SelectListItem
                                         {
                                             Text = cari.Name,
                                             Value = cari.Id.ToString()
                                         }).ToList();
            ViewBag.cari = Cari;*/

            var result = c.Invoices.Find(id);
            return View("BringInvoice", result);
        }
        public ActionResult UpdateInvoice(Invoice invoice)
        {
            var result = c.Invoices.Find(invoice.InvoiceID);
            result.InvoiceSerialNo = invoice.InvoiceSerialNo;
            result.InvoiceSequenceNo = invoice.InvoiceSequenceNo;
            result.Date = invoice.Date;
            result.Hour = invoice.Hour;
            result.TaxAdministration = invoice.TaxAdministration;
            result.DeliveringPerson = invoice.DeliveringPerson;
            result.ReceiverPerson = invoice.ReceiverPerson;
        
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        /*public PartialViewResult InvoicesDetail()
        {
            List<SelectListItem> Personel = (from personel in context.Personels.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = personel.Name,
                                                 Value = personel.Id.ToString()
                                             }).ToList();
            ViewBag.personel = Personel;
            List<SelectListItem> Cari = (from cari in context.Caris.ToList()
                                         select new SelectListItem
                                         {
                                             Text = cari.Name,
                                             Value = cari.Id.ToString()
                                         }).ToList();
            ViewBag.cari = Cari;
            return PartialView();
        }*/


        public ActionResult DetailsInvoice(int id)
        {
            var values = c.ınvoiceItems.Where(x => x.InvoiceID == id).ToList();
            
            return View(values);

        }
        [HttpGet]
        public ActionResult AddInvoiceItem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddInvoiceItem(InvoiceItem p)
        {
            c.ınvoiceItems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Dynamic()
        {
            Dynamic dync = new Dynamic();
            dync.value1 = c.Invoices.ToList();
            dync.value2 = c.ınvoiceItems.ToList();
            return View(dync);
        }
        public ActionResult SaveInvoice(string InvoiceSerialNo, string InvoiceSequenceNo, DateTime Date, string TaxAdministration, string Hour, string DeliveringPerson, string ReceiverPerson, string Total, InvoiceItem[] contents)
        {
            Invoice f = new Invoice();
            f.InvoiceSerialNo = InvoiceSerialNo;
            f.InvoiceSequenceNo = InvoiceSequenceNo;
            f.Date = Date;
            f.TaxAdministration = TaxAdministration;
            f.Hour = Hour;
            f.DeliveringPerson = DeliveringPerson;
            f.ReceiverPerson = ReceiverPerson;
            f.TotalPrice = decimal.Parse(Total);

            c.Invoices.Add(f);

            foreach (var x in contents)
            {
                InvoiceItem ic = new InvoiceItem();
                ic.Description = x.Description;
                ic.Price = x.Price;
                ic.Quantity = x.Quantity;
                ic.InvoiceID = x.InvoiceItemID;
                ic.TotalPrice = x.TotalPrice;
                c.ınvoiceItems.Add(ic);
            }

            c.SaveChanges();
            return Json("İşlem Tamamlandı", JsonRequestBehavior.AllowGet);
        }

    }
}