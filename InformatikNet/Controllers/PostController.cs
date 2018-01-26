using InformatikNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformatikNet.Controllers
{
    public class PostController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create(string category)
        {
            CreatePostModel createPostModel = new CreatePostModel();

            var taglists = db.Tag.Where(x => x.Category.CategoryName == category).ToList();
            var selectListItem = taglists.Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString()});
            createPostModel.Tag = selectListItem;
            return View(createPostModel);
        }

        public ActionResult Posts(string SelectedCategory)
        {
            var posts = db.Post.Where(p => p.Categories.CategoryName == SelectedCategory).ToList();
            var postModel = new PostViewModel();
            postModel.Posts = posts;
            postModel.Category = SelectedCategory;
            return View(postModel);
        }
        
        public string Title { get; set; }

        [HttpPost]
        public ActionResult Create(CreatePostModel model)
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
            db.Post.Add(post);
            db.SaveChanges();

            return RedirectToAction("Posts", new { SelectedCategory = post.Categories.CategoryName});
        }
    }
}