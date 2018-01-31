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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if(ModelState.IsValid)
            {
                var body = "<p>Hejsan här kommer ett mail</p><p>Message: </p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("martinsarling97@gmail.com"));
                message.From = new MailAddress("informatiknet101@gmail.com");
                message.Subject = ("TestMeddelande");
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

    }
}