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
        public byte[] Photo { get; set; }
        public byte[] FileContent { get; set; }
        public String FileName { get; set; }
        public bool isHidden { get; set; }
        public string PublishDate { get; set; }
        public string Coords { get; set; }
     



    }

    public class CreatePostModel
    {
        public int Id { get; set; }
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
        [Display(Name = "Lägg upp bild")]
        public byte[] Photo { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Select File")]
        public HttpPostedFileBase files { get; set; }
        [Display(Name = "Uploaded File")]
        public String FileName { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime PublishDate { get; set; }
        public string Coords { get; set; }
    }
    

    public class PostViewModel
    {
        public ICollection<Post> Posts { get; set; }
        public string Category { get; set; }
        [Display(Name = "Infoga bild")]
        public byte[] Picture { get; set; }
        [Display(Name = "Infoga fil")]
        public String FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}