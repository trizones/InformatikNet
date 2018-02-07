using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformatikNet.Models
{
    public class FileViewModel
    {
        public ICollection<Post> Posts { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}