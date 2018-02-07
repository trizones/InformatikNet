using InformatikNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformatikNet.Controllers
{
    
    public class CalendarController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            
            var yourActivities = db.Calendar.ToList();
            return new JsonResult { Data = yourActivities, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public ActionResult CreateActivity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateActivity(Calendar model)
        {
            var user = db.Users.Single(u => u.Email == User.Identity.Name);
            model.aUser = user;
            db.Calendar.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}