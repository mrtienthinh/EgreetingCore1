using Egreeting.Models.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Models.AppContext
{
    public class EgreetingContext : IdentityDbContext<ApplicationUser>
    {
        public EgreetingContext()
            : base("EgreetingConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Ecard> Ecards { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<EgreetingUser> EgreetingUsers { get; set; }
        public virtual DbSet<EgreetingRole> EgreetingRoles { get; set; }
        public virtual DbSet<Subcriber> Subcribers { get; set; }
        public virtual DbSet<ScheduleSender> ScheduleSenders { get; set; }



        public static EgreetingContext Create()
        {
            return new EgreetingContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
