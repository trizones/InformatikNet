using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformatikNet.Models
{
    public class CreateMeetingModel
    {
        public virtual ICollection<ApplicationUser> Reciever { get; set; }
        public string Title { get; set; }
        public ICollection<DateTime> SuggestedDates { get; set; }   
    }

    public class ConfirmedMeeting
    {
        public int Id { get; set; }
        public ApplicationUser Creator { get; set; }
        public string Title { get; set; }
        public ICollection<ApplicationUser> Recievers { get; set; }
        public DateTime ConfirmedDate { get; set;
        }

        public class PendingMeeting
        {
            public int Id { get; set; }
            public ApplicationUser Creator { get; set; }
            public string Title { get; set; }
            public ICollection<ApplicationUser> Recievers { get; set; }
            public DateTime SuggestedDate1 { get; set; }
            public DateTime SuggestedDate2 { get; set; }
            public DateTime SuggestedDate3 { get; set; }
            public int SuggestedDateVotes1 { get; set; }
            public int SuggestedDateVotes2 { get; set; }
            public int SuggestedDateVotes3 { get; set; }
        }
    }