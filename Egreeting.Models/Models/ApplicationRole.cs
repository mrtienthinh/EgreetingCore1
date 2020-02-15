using Microsoft.AspNetCore.Identity;
using System;

namespace Egreeting.Models.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public virtual EgreetingRole EgreetingRole { get; set; }
    }
}
