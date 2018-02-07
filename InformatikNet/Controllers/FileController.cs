using InformatikNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformatikNet.Controllers
{
    public class FileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Files(DateTime startDate, DateTime endDate)
        {
            var model = new FileViewModel();
            var cats = db.Category.ToList();
            var postsWithFiles = db.Post.Where(x => x.FileName != "").ToList();
            model.Posts = postsWithFiles;
            var types = new List<SelectListItem>();


            foreach(var cat in cats)
            {
                types.Add(new SelectListItem() { Text = cat.CategoryName, Value = cat.CategoryName });
            }
            var d = db.Post.Where(x => x.PublishDate >= startDate && x.PublishDate <= endDate && x.FileName != "").ToList();
            model.Categories = types;

            return View(model);
        }
    }
}