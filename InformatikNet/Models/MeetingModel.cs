﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformatikNet.Models
{
    public class CreateMeetingModel
    {
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual List<ApplicationUser> Recievers { get; set; }
        public List<string> ReciverIds { get; set; }
        [Required]
        [StringLength(32, ErrorMessage = "Titel måste vara mellan 3 och 32 tecken", MinimumLength = 3)]
        public string Title { get; set; }
        public DateTime SuggestedDate1 { get; set; }
        public DateTime SuggestedDate2 { get; set; }
        public DateTime SuggestedDate3 { get; set; }
    }

    public class ConfirmedMeeting
    {
        public int ConfirmedMeetingId { get; set; }
        public ApplicationUser Creator { get; set; }
        public string Title { get; set; }
        public ICollection<ApplicationUser> Recievers { get; set; }
        public DateTime ConfirmedDate { get; set; }
    }
    public class PendingMeeting
    {
        public int PendingMeetingId { get; set; }
        public ApplicationUser Creator { get; set; }
        public string Title { get; set; }
        public virtual ICollection<ApplicationUser> Recievers { get; set; }
        public DateTime SuggestedDate1 { get; set; }
        public DateTime SuggestedDate2 { get; set; }
        public DateTime SuggestedDate3 { get; set; }
        public int SuggestedDateVotes1 { get; set; }
        public int SuggestedDateVotes2 { get; set; }
        public int SuggestedDateVotes3 { get; set; }
        public List<string> Responders { get; set; }
    }
    public class PendingMeetingViewModel
    {
        public int Id { get; set; }
        public ApplicationUser Creator { get; set; }
        public DateTime SuggestedDate1 { get; set; }
        public DateTime SuggestedDate2 { get; set; }
        public DateTime SuggestedDate3 { get; set; }
        public string Title { get; set; }
        public Boolean vote1 { get; set; }
        public Boolean vote2 { get; set; }
        public Boolean vote3 { get; set; }
    }
    public class IndexMeetingModel
    {
        public int Id { get; set; }
        public List<PendingMeeting> PendingsMeetings { get; set; }
        public List<PendingMeeting> CreatedPendingsMeetings { get; set; }
        public List<ConfirmedMeeting> ConfirmedMeetings { get; set; }
        public string SelectedMeetingTitle { get; set; }
        public int SelectedCreatedMeetingId { get; set; }

    }

    public class ConfirmedMeetingViewModel
    {
        public int PendingMeetingId { get; set; }
        public ApplicationUser Creator { get; set; }
        public string Title { get; set; }
        public virtual ICollection<ApplicationUser> Recievers { get; set; }
        public DateTime SuggestedDate1 { get; set; }
        public DateTime SuggestedDate2 { get; set; }
        public DateTime SuggestedDate3 { get; set; }
        public int SuggestedDateVotes1 { get; set; }
        public int SuggestedDateVotes2 { get; set; }
        public int SuggestedDateVotes3 { get; set; }
        public Boolean Select1 { get; set; }
        public Boolean Select2 { get; set; }
        public Boolean Select3 { get; set; }
        public DateTime ConfirmedDate { get; set; }
    }

}