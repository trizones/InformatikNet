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
        public void Contact(EmailFormModel model)
        {
            if(ModelState.IsValid)
            {
                var body = "<p>Hejsan här kommer ett mail</p>";
                foreach (var reciever in model.Recievers)
                {
                    var message = new MailMessage();
                    message.To.Add(reciever);
                    message.From = new MailAddress(model.FromEmail);
                    message.Subject = model.Subject;
                    message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                    message.IsBodyHtml = true;
                
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                    }
                }
            }
        }
    }
}