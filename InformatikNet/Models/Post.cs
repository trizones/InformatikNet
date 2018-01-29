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
        [Required(ErrorMessage ="Titelfältet får inte vara tomt")]
        [StringLength(50, ErrorMessage = "Titeln får inte vara längre än 50 tecken")]
        [Display(Name ="Rubrik")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Innehållet får inte vara tomt")]
        [Display(Name = "Meddelande")]
        public string Content { get; set; }
        [Required]
        public virtual IEnumerable<SelectListItem> Tag { get; set; }
        public Category Category;
    }
}