﻿using InformatikNet.Models;
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

        // GET: Meeting
        public ActionResult CreateMeeting()
        {
            CreateMeetingModel model = new CreateMeetingModel();
            var users = db.Users.Where(u => u.Email != User.Identity.Name).ToList();
            model.Users = users;
            model.Recievers = db.Users.Where(u => u.Email == User.Identity.Name).ToList();
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
            pendingMeeting.Recievers = model.Recievers;
            pendingMeeting.SuggestedDate1 = model.SuggestedDate1;
            pendingMeeting.SuggestedDate2 = model.SuggestedDate2;
            pendingMeeting.SuggestedDate3 = model.SuggestedDate3;

            db.PendingMeeting.Add(pendingMeeting);
            db.SaveChanges();

            return View();
        }
    }
}