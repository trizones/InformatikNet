using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InformatikNet.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InformatikNet.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual Category Categories { get; set; }
        public virtual Tag Tag { get; set; }
    }

    public class CreatePostModel
    {
        public int TagId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public virtual IEnumerable<SelectListItem> Tag { get; set; }
        public Category Category;
    }
}