using InformatikNet.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var posts = db.Post.OrderByDescending(p => p.PublishDate).ThenBy(a => a.Id).Take(3).ToList();

            PostViewModel postViewModel = new PostViewModel
            {
                Posts = posts
            };
            return View(postViewModel);
        }

        public FileContentResult Photos(int? id)
        {

            if (id == null)
            {

                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);

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
        
        public ActionResult DailyMail()
        {
            var db = new ApplicationDbContext();

            var AllUsers = db.Users.ToList();
            
            var Today = DateTime.Now;

            var Yesterday = Today.AddDays(-1);

            var Poster = db.Post.Where(i => i.PublishDate >= Yesterday && i.PublishDate <= Today).ToList();

            var Meetings = db.ConfirmedMeeting.Where(i => i.ConfirmedDate >= Yesterday && i.ConfirmedDate <= Today).ToList();

            var body = "Sammanfattning för " + DateTime.Today.ToString("yyyy-MM-dd");

            if (Poster != null)
            {
                body += "<br /> Dagens poster: <br />";
                foreach (var post in Poster)
                {
                    body += post.Title + " | Skriven av "+ post.Author.Name + "<br />";
                }
            }

            if (Meetings != null)
            {
                body += "<br /> Dagens möten: <br />";
                foreach (var meeting in Meetings)
                {
                    body += meeting.Title + " | " + meeting.ConfirmedDate +"<br />";
                }
            }

            var mail = new EmailFormModel()
            {
                FromName = "Admin",
                FromEmail = "informatiknet101@gmail.com",
                Subject = "Daglig sammanfattning från Informatiknet",
                Message = body,
                Recievers = AllUsers
            };

            Contact(mail);

            return RedirectToAction("Index", "Meeting");
        }
    }
}