using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class Subcriber : BaseModel
    {
        [Key]
        public int SubcriberID { get; set; }

        [EmailAddress]
        [Display(Name ="Subcriber's email")]
        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        public string Email { get; set; }

        public int EgreetingUserID { get; set; }

        [ForeignKey("EgreetingUserID")]
        public virtual EgreetingUser EgreetingUser { get; set; }
    }
}
