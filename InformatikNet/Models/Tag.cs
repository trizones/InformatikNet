using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformatikNet.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }
        public string CategoryString { get; set; }
    }
}