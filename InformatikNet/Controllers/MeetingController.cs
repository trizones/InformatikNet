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
            IndexMeetingModel indexMeetingModel = new IndexMeetingModel();
            var confirmedMeetings = db.ConfirmedMeeting.ToList();
            var user = db.Users.Single(u => u.UserName == User.Identity.Name);
            var meetings = db.PendingMeeting.Where(x => x.Recievers.Any(u => u.Id == user.Id)).ToList();
            var yourCreatedMeetings = db.PendingMeeting.Where(x => x.Creator.Id == user.Id).ToList();

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
            indexMeetingModel.CreatedPendingsMeetings = yourCreatedMeetings;
            indexMeetingModel.ConfirmedMeetings = confirmedMeetings;
            indexMeetingModel.PendingsMeetings = meetings;

            
            return View(indexMeetingModel);
        }

        public JsonResult GetEvents()
        {
            //Behöver få med deltagare på möten här någonstans.
            var confirmedMeetings = db.ConfirmedMeeting.ToList();
            var ListOfRecievers = "";

            foreach(var item in confirmedMeetings)
            {
                var recievers = item.Recievers;

                foreach(var user in recievers)
                {
                    ListOfRecievers += user.Name + " ";
                }
            }

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

            foreach (var item in model.ReciverIds)
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


            return RedirectToAction("Index");

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
            if(theMeeting.Responders == null)
            {
                aList.Add(User.Identity.Name);
                theMeeting.Responders = aList;
            }
            
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ConfirmedMeeting(int PId)
        {
            var model = new ConfirmedMeetingViewModel();
            var thisMeeting = db.PendingMeeting.Where(m => m.PendingMeetingId == PId).Single();
            model.Creator = thisMeeting.Creator;
            model.PendingMeetingId = thisMeeting.PendingMeetingId;
            model.Recievers = thisMeeting.Recievers;
            model.SuggestedDate1 = thisMeeting.SuggestedDate1;
            model.SuggestedDate2 = thisMeeting.SuggestedDate2;
            model.SuggestedDate3 = thisMeeting.SuggestedDate3;
            model.SuggestedDateVotes1 = thisMeeting.SuggestedDateVotes1;
            model.SuggestedDateVotes2 = thisMeeting.SuggestedDateVotes2;
            model.SuggestedDateVotes3 = thisMeeting.SuggestedDateVotes3;
            model.Title = thisMeeting.Title;
            return View(model);

        }

        [HttpPost]
        public ActionResult SelectMeetingDate(ConfirmedMeetingViewModel model)
        {
            var thePendingMeeting = db.PendingMeeting.Include(X => X.Recievers).Single(x => x.PendingMeetingId == model.PendingMeetingId);
            var confirmedMeeting = new ConfirmedMeeting();
            confirmedMeeting.Creator = db.Users.Single(x => x.UserName == User.Identity.Name);
            confirmedMeeting.Title = thePendingMeeting.Title;

            string list2 = "";
            var list = new List<ApplicationUser>();

            foreach (var item in thePendingMeeting.Recievers)
            {
                var bock = db.Users.Where(u => u.Id == item.Id).Single();
                list2 = list2 + " " + bock.Name;
                list.Add(bock);
            }

            confirmedMeeting.Recievers = list;
            confirmedMeeting.UserNames = list2;

                if (model.Select1 == true)
            {
                confirmedMeeting.ConfirmedDate = thePendingMeeting.SuggestedDate1;
            }
            if (model.Select2 == true)
            {
                confirmedMeeting.ConfirmedDate = thePendingMeeting.SuggestedDate2;
            }
            if (model.Select3 == true)
            {
                confirmedMeeting.ConfirmedDate = thePendingMeeting.SuggestedDate3;
            }

            var mail = new EmailFormModel
            {
                FromEmail = confirmedMeeting.Creator.Email,
                FromName = confirmedMeeting.Creator.Name,
                Message = String.Format("Ett möte du är inbjuden till har blivit bekräftat, den bekräftade tiden är {0}", confirmedMeeting.ConfirmedDate),
                Subject = String.Format("{0} har blivit bekräftad", confirmedMeeting.Title),
                Recievers = list
            };

            HomeController.Contact(mail);

            db.ConfirmedMeeting.Add(confirmedMeeting);
            db.PendingMeeting.Remove(thePendingMeeting);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



    }
}