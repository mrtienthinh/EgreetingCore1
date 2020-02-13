using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class Payment : BaseModel
    {
        [Key]
        public int PaymentID { get; set; }

        [Range(1,12, ErrorMessage = "Month must be number from 1 to 12!")]
        [DisplayName("Month")]
        public int Month { get; set; }

        [DisplayName("Year")]
        public int Year { get; set; }

        [DisplayName("Payment Status")]
        public bool PaymentStatus { get; set; }

        public virtual EgreetingUser EgreetingUser { get; set; }
    }
}
