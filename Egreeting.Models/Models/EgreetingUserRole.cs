using System;
using System.Collections.Generic;
using System.Text;

namespace Egreeting.Models.Models
{
    public class EgreetingUserRole
    {
        public int EgreetingUserId { get; set; }
        public int EgreetingRoleId { get; set; }
        public virtual EgreetingUser EgreetingUser { get; set; }
        public virtual EgreetingRole EgreetingRole { get; set; }


    }
}
