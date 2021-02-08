/* Author:    Dan Ruley
  Partner:   None
  Date:      September, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

  Data context class for the Research Opportunity Database.
 */

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using URC.Models;

namespace URC.Data
{
    public class ResearchOpportunityContext : DbContext
    {
        /// <summary>
        /// Entity provides us with the DB Context
        /// </summary>
        public ResearchOpportunityContext(DbContextOptions<ResearchOpportunityContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// Opportunities DB table
        /// </summary>
        public DbSet<ResearchOpportunity> ResearchOpportunities { get; set; }

        /// <summary>
        /// Student Applications DB table
        /// </summary>
        public DbSet<StudentApplication> StudentApplications { get; set; }

        /// <summary>
        /// Notifications for Students
        /// </summary>
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<StudentOpportunityApplied> StudentOpportunityApplied { get; set; }

        /// <summary>
        /// Associate the models with the correct table and configure dynamic value generation for some Date fields.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ResearchOpportunity>().ToTable("Opportunities");
            modelBuilder.Entity<Notification>().ToTable("Notifications");
            modelBuilder.Entity<StudentOpportunityApplied>().ToTable("StudentOpportunityApplied");
            modelBuilder.Entity<StudentApplication>().ToTable("Applications").Property(p => p.AppDate).ValueGeneratedOnAdd();
            modelBuilder.Entity<StudentApplication>().ToTable("Applications").Property(p => p.LastModified).ValueGeneratedOnAdd();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DateTime now = DateTime.Now;

            foreach(var item in ChangeTracker.Entries<StudentApplication>().Where(e => e.State == EntityState.Added))
                item.Property("AppDate").CurrentValue = now;

            foreach (var item in ChangeTracker.Entries<StudentApplication>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
                item.Property("LastModified").CurrentValue = now;

            return base.SaveChangesAsync();
        }
    }
}
