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
            EgreetingUserRoles = new HashSet<EgreetingUserRole>();
        }

        [Key]
        public int EgreetingRoleID { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must not more than {1} characters long!")]
        [DisplayName("Role")]
        public string EgreetingRoleName { get; set; }

        public virtual ICollection<EgreetingUserRole> EgreetingUserRoles { get; set; }
    }
}
