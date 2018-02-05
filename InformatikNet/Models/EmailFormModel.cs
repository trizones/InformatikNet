using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InformatikNet.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Your Name")]
        public string FromName { get; set; }
        [Required, Display(Name = "Your Email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public ICollection<ApplicationUser> Recievers { get; set; }
    }
}