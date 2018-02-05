using InformatikNet.Models;
using System;
using System.Collections.Generic;
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