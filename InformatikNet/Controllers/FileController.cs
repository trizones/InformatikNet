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

        
        public ActionResult Files(FileViewModel aModel)
        {
            if(aModel == null)
            {
                var model = new FileViewModel();
                var cats = db.Category.ToList();

                var types = new List<SelectListItem>();


                foreach (var cat in cats)
                {
                    types.Add(new SelectListItem() { Text = cat.CategoryName, Value = cat.CategoryName });
                }
                
                model.Categories = types;

                return View(model);
            }
            var postsWithFiles = db.Post.Where(x => x.PublishDate >= aModel.startDate && x.PublishDate <= aModel.endDate && x.FileName != "").ToList();
            aModel.Posts = postsWithFiles;
           

            return View(aModel);
            
        }
    }
}