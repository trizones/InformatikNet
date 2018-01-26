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

        public ActionResult Index()
        {
            return View();
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
        public ActionResult Create(Post post, int catId)
        {
            var category = db.Category.Single(x => x.Id == catId);

            var user = User.Identity.Name;

            var apUser = db.Users.Single(x => x.UserName == user);

            post.Author = apUser;
            post.Categories = category;

            db.Post.Add(post);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}