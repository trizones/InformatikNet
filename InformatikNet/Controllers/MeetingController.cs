using InformatikNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace InformatikNet.Controllers
{
    public class MeetingController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //GET: Index
        public ActionResult Index()
        {

            return View();
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
            pendingMeeting.SuggestedDate1 = model.SuggestedDate1;
            pendingMeeting.SuggestedDate2 = model.SuggestedDate2;
            pendingMeeting.SuggestedDate3 = model.SuggestedDate3;
            
            db.PendingMeeting.Add(pendingMeeting);
            db.SaveChanges();

            var mail = new EmailFormModel
            {
                FromEmail = user.Email,
                FromName = user.Name,
                Message = "Du har blivit kallad till ett nytt möte! :), logga in på intranätet för att bekräfta!",
                Subject = model.Title,
                Recievers = list
            };

            HomeController.Contact(mail);


            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult YourPendingMeetings()
        {
            var model = new List<PendingMeetingViewModel>();
            var user = db.Users.Single(u => u.UserName == User.Identity.Name);
            var meetings = db.PendingMeeting.Where(x => x.Recievers.Any(u => u.Id == user.Id)).ToList();

            foreach(var meet in meetings)
            {
                foreach(var id in meet.Responders)
                {
                    if (id == User.Identity.Name)
                    {
                        meetings.Remove(meet);
                    }
                }
            }

             foreach(var meet in meetings)
            {
                PendingMeetingViewModel meeting = new PendingMeetingViewModel();
                meeting.Title = meet.Title;
                meeting.SuggestedDate1 = meet.SuggestedDate1;
                meeting.SuggestedDate2 = meet.SuggestedDate2;
                meeting.SuggestedDate3 = meet.SuggestedDate3;
                meeting.Id = meet.PendingMeetingId;
                meeting.Creator = meet.Creator;

                model.Add(meeting);
            }
            
            return View(model);
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