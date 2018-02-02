using InformatikNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformatikNet.Controllers
{
    public class PostController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private static string tempCat = "";

        public ActionResult Create(string category)
        {
            CreatePostModel createPostModel = new CreatePostModel();

            var taglists = db.Tag.Where(x => x.Category.CategoryName == category).ToList();
            var selectListItem = taglists.Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() });
            createPostModel.Tag = selectListItem;

            ViewBag.Category = category;

            return View(createPostModel);
        }
      
        public ActionResult Posts(string tag, string category)
        {
            if (db.Category.Any(c => c.CategoryName == category))
            {
                tempCat = category;
                var posts = db.Post.Where(p => p.Categories.CategoryName == tempCat).ToList();
                var postModel = new PostViewModel();
                postModel.Posts = posts;
                postModel.Category = tempCat;
                

            
            return View(postModel);
            }

            else
            {
                var posts = db.Post.Where(p => p.Categories.CategoryName == tempCat && p.Tag.Name.Contains(tag)).ToList();
                var postModel = new PostViewModel();
                postModel.Posts = posts;
                postModel.Category = tempCat;
                return View(postModel);
            }

        }

        public string Title { get; set; }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Photo, FileContent")]CreatePostModel model)
        {

            Post post = new Post();

            var author = db.Users.Single(u => u.UserName == User.Identity.Name);
            post.Author = author;
            post.Content = model.Content;
            post.Title = model.Title;

            var tag = db.Tag.Single(t => t.Id == model.TagId);
            post.Tag = tag;

            var aCategory = db.Category.Single(c => c.CategoryName == tag.Category.CategoryName);
            post.Categories = aCategory;
            byte[] imageData = null;
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase poImgFile = Request.Files["Photo"];

                using (var binary = new BinaryReader(poImgFile.InputStream))
                {
                    imageData = binary.ReadBytes(poImgFile.ContentLength);
                }

            }
            byte[] docData = null;
            var fileName = "";
            if (Request.Files.Count > 0)
            {

                HttpPostedFileBase docFile = Request.Files["FileContent"];

                using (var binary = new BinaryReader(docFile.InputStream))
                {
                    docData = binary.ReadBytes(docFile.ContentLength);
                    fileName = docFile.FileName;

                }
            }

            post.FileName = fileName;
            post.FileContent = docData;
            post.Photo = imageData;
            db.Post.Add(post);

            db.SaveChanges();

            return RedirectToAction("Posts", new { category = post.Categories.CategoryName });
        }
        public ActionResult Search(string searchvalue)
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTag(string tagName, string category)
        {
            var categoryObj = db.Category.Single(c => c.CategoryName == category);

            Tag tag = new Tag { Name = tagName, Category = categoryObj };

            db.Tag.Add(tag);
            db.SaveChanges();
            return new EmptyResult();
        }

        [HttpGet]
        public FileResult Downloadfile (int id)
        {
            var postbyid = db.Post.Single(x => x.Id == id);
            byte[] filecontent = postbyid.FileContent;

            return File(filecontent, "application/pdf/docx/doc");
        }
    }
}