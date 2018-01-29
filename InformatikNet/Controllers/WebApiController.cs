using InformatikNet.Models;
using System.Linq;
using System.Web.Http;

namespace InformatikNet.Controllers
{
    public class WebApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();

            base.Dispose(disposing);
        }
        [HttpPost, ActionName("CreateTag")]
        public void NewTag(Tag tag)
        {
            if (tag.Name != "")
            {
                var category = db.Category.Single(c => c.CategoryName == tag.CategoryString);
                tag.Category = category;
                if(!(db.Tag.Any(t => t.Name == tag.Name && t.Category.CategoryName == category.CategoryName)))
                db.Tag.Add(tag);
                db.SaveChanges();
            }
        }
    }
}
