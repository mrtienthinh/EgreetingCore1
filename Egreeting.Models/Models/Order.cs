using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class Order : BaseModel
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int OrderID { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [DisplayName("Sender's Name")]
        public string SenderName { get; set; }

        [EmailAddress]
        [DisplayName("Recipient's Email")]
        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]

        public string RecipientEmail { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [DisplayName("Subject")]
        public string SendSubject { get; set; }

        [DisplayName("Message")]
        [StringLength(500, ErrorMessage = "The {0} must not more than {1} characters long!")]
        public string SendMessage { get; set; }

        [DisplayName("Schedule Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm}", ApplyFormatInEditMode = true)]
        public DateTime? ScheduleTime { get; set; }

        [DisplayName("Sending Status")]
        public bool SendStatus { get; set; }

        public double TotalPrice { get; set; }

        public virtual EgreetingUser User { get; set; }

        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
