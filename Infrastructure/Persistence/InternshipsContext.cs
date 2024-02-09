using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class InternshipsContext : DbContext
    {
        public DbSet<DbAdminUser> AdminUsers { get; set; }
        public DbSet<DbRegularUser> RegularUsers { get; set; }
        public DbSet<DbInternship> Internships { get; set; }
        public DbSet<DbInternshipApplication> InternshipsApplications { get; set;}

        public InternshipsContext(DbContextOptions<InternshipsContext> op) : base(op) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbAdminUser>()
                .HasIndex(a => a.Username)
                .IsUnique();
            modelBuilder.Entity<DbRegularUser>()
                .HasIndex(a => a.Username)
                .IsUnique();
            modelBuilder.Entity<DbInternshipApplication>()
                .HasKey(app => new { app.InternshipId, app.RegularUserId });
        }
    }
}
