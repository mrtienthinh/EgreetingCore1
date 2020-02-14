using Egreeting.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Egreeting.Models.AppContext
{
    public class EgreetingContext : IdentityDbContext
    {
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CategoryEcard>()
                .HasKey(ce => new { ce.CategoryId, ce.EcardId });
            builder.Entity<CategoryEcard>()
                .HasOne(ce => ce.Category)
                .WithMany(c => c.CategoryEcards)
                .HasForeignKey(ce =>ce.CategoryId);
            builder.Entity<CategoryEcard>()
                .HasOne(ce => ce.Ecard)
                .WithMany(c => c.CategoryEcards)
                .HasForeignKey(ce => ce.EcardId);
            
            builder.Entity<Category>()
                .HasIndex(u => u.CategorySlug)
                .IsUnique();
            
            builder.Entity<Ecard>()
                .HasIndex(u => u.EcardSlug)
                .IsUnique();

            builder.Entity<EgreetingUserRole>()
                .HasKey(eur => new { eur.EgreetingUserId, eur.EgreetingRoleId });
            
            builder.Entity<EgreetingRole>()
                .HasIndex(u => u.EgreetingRoleName)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../Egreeting.Web/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<EgreetingContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseNpgsql(connectionString);
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EgreetingContext>
    {
        public EgreetingContext CreateDbContext(string[] args)
        {
            return new EgreetingContext();
        }
    }
}
