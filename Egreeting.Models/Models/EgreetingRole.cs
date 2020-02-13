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
    public class EgreetingRole : BaseModel
    {
        public EgreetingRole()
        {
            EgreetingUsers = new HashSet<EgreetingUser>();
        }

        [Key]
        public int EgreetingRoleID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(50, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [DisplayName("Role")]
        public string EgreetingRoleName { get; set; }

        public virtual ICollection<EgreetingUser> EgreetingUsers { get; set; }
    }
}
