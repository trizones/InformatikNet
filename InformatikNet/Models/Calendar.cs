using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformatikNet.Models
{
    public class Calendar
    {
        public int id { get; set; }
        public string activity { get; set; }
        public ApplicationUser aUser { get; set; }
        public DateTime datetime { get; set; }
    }
}