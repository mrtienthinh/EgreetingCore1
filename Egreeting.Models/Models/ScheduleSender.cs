using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class ScheduleSender : BaseModel
    {

        [Key]
        public int ScheduleSenderID { get; set; }

        [DisplayName("Schedule time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:R}", ApplyFormatInEditMode = true)]
        public DateTime? ScheduleTime { get; set; }

        // Enum ScheduleType 0: Once, 1: Daily, 2: Monthly, 3: Annual
        [DisplayName("Schedule type")]
        [Range(0,3, ErrorMessage = "The {0} not exist!")]
        public int ScheduleType { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [DisplayName("Sender name")]
        public string SenderName { get; set; }

        [EmailAddress]
        [DisplayName("Recipient's email")]
        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        public string RecipientEmail { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [DisplayName("Subject")]
        public string SendSubject { get; set; }

        [DisplayName("Message")]
        [StringLength(500, ErrorMessage = "The {0} must not more than {1} characters long!")]
        public string SendMessage { get; set; }

        public virtual Ecard Ecard { get; set; }

        public virtual EgreetingUser EgreetingUser { get; set; }
    }
}
