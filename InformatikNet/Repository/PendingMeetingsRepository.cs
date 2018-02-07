using InformatikNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace InformatikNet.Repository
{
    public class PendingMeetingsRepository
    {
        public ICollection<PendingMeeting> yourMeetings { get; set; }

        public PendingMeetingsRepository(string userName)
        {
            using (var db = new ApplicationDbContext())
            {
                var pendingMeetings = new List<PendingMeeting>();
                var pendingMeetingsAll = db.PendingMeeting.Include(X => X.Recievers).ToList();
                foreach(var meet in pendingMeetingsAll)
                {
                    foreach(var user in meet.Recievers)
                    {
                        if (user.UserName == userName)
                        {
                            pendingMeetings.Add(meet);
                        }
                    }
                    
                }
                this.yourMeetings = pendingMeetings;
            }
        }
    }
    }