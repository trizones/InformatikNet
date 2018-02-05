﻿using InformatikNet.Models;
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
            IndexMeetingModel indexMeetingModel = new IndexMeetingModel();
            var confirmedMeetings = db.ConfirmedMeeting.ToList();
            var user = db.Users.Single(u => u.UserName == User.Identity.Name);
            var meetings = db.PendingMeeting.Where(x => x.Recievers.Any(u => u.Id == user.Id)).ToList();

            foreach (var meet in meetings)
            {
                if(meet.Responders != null)
                {
                    foreach (var id in meet.Responders)
                    {
                        if (id == User.Identity.Name)
                        {
                            meetings.Remove(meet);
                        }
                    }
                }
                
            }
           
            indexMeetingModel.ConfirmedMeetings = confirmedMeetings;
            indexMeetingModel.PendingsMeetings = meetings;

            
            return View(indexMeetingModel);
        }

        public JsonResult GetEvents()
        {
            
            var confirmedMeetings = db.ConfirmedMeeting.ToList();
            return new JsonResult { Data = confirmedMeetings, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            
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
        public ActionResult YourPendingMeetings(string title)
        {
            var model = new List<PendingMeetingViewModel>();
            var user = db.Users.Single(u => u.UserName == User.Identity.Name);
            var meeting = db.PendingMeeting.Where(x => x.Title == title).Single();


            PendingMeetingViewModel pending = new PendingMeetingViewModel();
            pending.Title = meeting.Title;
            pending.SuggestedDate1 = meeting.SuggestedDate1;
            pending.SuggestedDate2 = meeting.SuggestedDate2;
            pending.SuggestedDate3 = meeting.SuggestedDate3;
            pending.Id = meeting.PendingMeetingId;
            pending.Creator = meeting.Creator;

           
            return View(pending);
        }
        [HttpPost]
        public ActionResult VotePedningMeeting(PendingMeetingViewModel model)
        {
            var theMeeting = db.PendingMeeting.Single(x => x.PendingMeetingId == model.Id);
            
            if (model.vote1 == true)
            {
                theMeeting.SuggestedDateVotes1++;
            
            }
            if (model.vote2 == true)
            {
                theMeeting.SuggestedDateVotes2++;
             
            }
            if (model.vote3 == true)
            {
                theMeeting.SuggestedDateVotes3++;
            
            }
            var aList = new List<string>();
            if(theMeeting.Responders != null)
            {
                foreach (var item in theMeeting.Responders)
                {
                    aList.Add(item);
                }
                aList.Add(User.Identity.Name);
                theMeeting.Responders = aList;
            }
            
            db.SaveChanges();

            return RedirectToAction("Index");
        }




    }
}