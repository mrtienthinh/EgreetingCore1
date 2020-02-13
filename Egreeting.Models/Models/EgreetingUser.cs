using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.Models
{
    public class EgreetingUser : BaseModel
    {
        public EgreetingUser()
        {
            Ecards = new HashSet<Ecard>();
            Feedbacks = new HashSet<Feedback>();
            ScheduleSenders = new HashSet<ScheduleSender>();
            EgreetingUserRoles = new HashSet<EgreetingUserRole>();
        }

        [Key]
        public int EgreetingUserID { get; set; }

        [DisplayName("Slug")]
        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        public string EgreetingUserSlug { get; set; }

        [DisplayName("First name")]
        [StringLength(50, ErrorMessage = "The {0} must not more than {1} characters long!")]
        public string FirstName { get; set; }

        [DisplayName("Email")]
        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [NotMapped]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [DisplayName("Last name")]
        [StringLength(100, ErrorMessage = "The {0} must not more than {1} characters long!")]
        public string LastName { get; set; }

        [DisplayName("Avatar")]
        public byte[] Avatar { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Birthday")]
        public DateTime? BirthDay { get; set; }

        [StringLength(12, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [MinLength(12, ErrorMessage = "The {0} must be at least {1} characters long!")]
        [DisplayName("Credit number")]
        public string CreditCardNumber { get; set; }

        [StringLength(3, ErrorMessage = "The {0} must be {1} characters long!")]
        [MinLength(3)]
        [MaxLength(3)]
        [DisplayName("Credit CVG")]
        public string CreditCardCVG { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Payment Due Date")]
        public DateTime? PaymentDueDate { get; set; }

        public virtual IEnumerable<EgreetingUserRole> EgreetingUserRoles { get; set; }
        public virtual IEnumerable<Feedback> Feedbacks { get; set; }
        public virtual IEnumerable<Ecard> Ecards { get; set; }
        public virtual Subcriber Subcriber { get; set; }
        public virtual IEnumerable<ScheduleSender> ScheduleSenders { get; set; }
    }
}
