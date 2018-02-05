using InformatikNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformatikNet.Controllers
{
    public class MeetingController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //GET: Index
        public ActionResult Index()
        {

            return ();
        }


        // GET: Meeting
        public ActionResult CreateMeeting()
        {
            CreateMeetingModel model = new CreateMeetingModel();
            var users = db.Users.Where(u => u.Email != User.Identity.Name).ToList();
            model.Users = users;
            var reciver = db.Users.Where(u => u.Email == User.Identity.Name).ToList();
   
            model.Recievers = reciver;
            return View(model);
        }

        //POST: Meeting
        [HttpPost]
        public ActionResult CreateMeeting(CreateMeetingModel model)
        {
            PendingMeeting pendingMeeting = new PendingMeeting();
            var user = db.Users.Single(u => u.Email == User.Identity.Name);
            pendingMeeting.Creator = user;
            pendingMeeting.Title = model.Title;
            var list = new List<ApplicationUser>();

            foreach(var item in model.ReciverIds)
            {
                var bock = db.Users.Where(u => u.Id == item).Single();
                list.Add(bock);
                
            }
            pendingMeeting.Recievers = list;
            pendingMeeting.SuggestedDate1 = DateTime.Today;
            pendingMeeting.SuggestedDate2 = DateTime.Today;
            pendingMeeting.SuggestedDate3 = DateTime.Today;
            
            db.PendingMeeting.Add(pendingMeeting);
            db.SaveChanges();


            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult YourPendingMeetings()
        {
            PendingMeetingViewModel model = new PendingMeetingViewModel();
            var user = db.Users.Single(u => u.Id == User.Identity.Name);
            var meetings = db.PendingMeeting.Where(x => x.Recievers == user).ToList();

            return View(meetings);
        }
        [HttpPost]
        public ActionResult VotePedningMeeting(PendingMeetingViewModel model)
        {
            var theMeeting = db.PendingMeeting.Single(x => x.PendingMeetingId == model.Id);
            if (model.vote1 == true)
            {
                theMeeting.SuggestedDateVotes1++;
                db.SaveChanges();
            }
            if (model.vote2 == true)
            {
                theMeeting.SuggestedDateVotes2++;
                db.SaveChanges();
            }
            if (model.vote3 == true)
            {
                theMeeting.SuggestedDateVotes3++;
                db.SaveChanges();
            }

            return RedirectToAction("YourPendingMeetings");
        }




    }
}