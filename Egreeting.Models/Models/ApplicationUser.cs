using Microsoft.AspNetCore.Identity;
using System;

namespace Egreeting.Models.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public virtual EgreetingUser EgreetingUser { get; set; }
    }
}
