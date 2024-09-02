﻿using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A,B")]
    public class QRController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string code)
        {
            //create QR code...
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator newcode = new QRCodeGenerator();
                QRCodeGenerator.QRCode squarecode = newcode.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                using (Bitmap picture = squarecode.GetGraphic(10))
                {
                    picture.Save(ms, ImageFormat.Png);
                    ViewBag.squarecodeimage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return View();
        }
    }
}