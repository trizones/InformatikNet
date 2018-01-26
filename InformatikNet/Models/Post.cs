using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InformatikNet.Models;

namespace InformatikNet.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual Category Categories { get; set; }
        public virtual Tag Tags { get; set; }
    }
}