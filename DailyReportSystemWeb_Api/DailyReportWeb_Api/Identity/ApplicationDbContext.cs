using DailyReportWeb_Api.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.Identity
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        /// <summary>
        /// A DbSet of UserTask entities.
        /// </summary>
        public DbSet<UserTask> UserTask { get; set; }

        /// <summary>
        /// A DbSet of ApplicationRole entities.
        /// </summary>
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        /// <summary>
        /// A DbSet of Organization entities.
        /// </summary>
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserTask>()
                .HasOne(t => t.ApplicationUser)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId).IsRequired();
    }

}
}
