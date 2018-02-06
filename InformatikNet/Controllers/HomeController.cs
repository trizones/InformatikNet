﻿using InformatikNet.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InformatikNet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public FileContentResult Photos(int? id)
        {

            if (id == null)
            {
                /*
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                */
                byte[] imageData = null;
                return File(imageData, "image/png");
                
            }
            // to get the user details to load user Image
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var image = db.Post.Where(x => x.Id == id).FirstOrDefault();

            return new FileContentResult(image.Photo, "image/jpeg");
        }

        public ActionResult Sent()
        {
            return View();
        }
        
        [ValidateAntiForgeryToken]
        public static void Contact(EmailFormModel model)
        {
                foreach (var reciever in model.Recievers)
                {
                    var message = new MailMessage();
                    message.To.Add(reciever.Email);
                    message.From = new MailAddress(model.FromEmail);
                    message.Subject = model.Subject;
                    message.Body = model.Message;
                    message.IsBodyHtml = true;
                
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                    }
                }
        }
    }
}