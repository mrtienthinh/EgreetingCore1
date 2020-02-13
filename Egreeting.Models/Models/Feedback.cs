using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class Feedback : BaseModel
    {
        [Key]
        public int FeedbackID { get; set; }

        [DisplayName("Subject")]
        [StringLength(200, ErrorMessage = "The {0} must not more than {1} characters long!")]
        public string Subject { get; set; }

        [DisplayName("Message")]
        [StringLength(500, ErrorMessage = "The {0} must not more than {1} characters long!")]
        public string Message { get; set; }

        public virtual Ecard Ecard { get; set; }

        public virtual EgreetingUser EgreetingUser { get; set; }
    }
}
