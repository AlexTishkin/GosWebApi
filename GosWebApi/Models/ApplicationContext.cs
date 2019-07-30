using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GosWebApi.Models
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Theme> Themes { get; set; }

        public DbSet<SubTheme> SubThemes { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne<Company>(s => s.Company)
                .WithMany(g => g.Users)
                .HasForeignKey(s => s.CompanyId);

            // Company * - * SubTheme
            modelBuilder.Entity<CompanySubTheme>()
                .HasKey(t => new {t.CompanyId, t.SubThemeId});

            modelBuilder.Entity<CompanySubTheme>()
                .HasOne(sc => sc.Company)
                .WithMany(s => s.CompanySubThemes)
                .HasForeignKey(sc => sc.CompanyId);

            modelBuilder.Entity<CompanySubTheme>()
                .HasOne(sc => sc.SubTheme)
                .WithMany(c => c.CompanySubThemes)
                .HasForeignKey(sc => sc.SubThemeId);

            // Report * - * Status
            modelBuilder.Entity<ReportStatus>()
                .HasKey(t => new {t.ReportId, t.StatusId});

            modelBuilder.Entity<ReportStatus>()
                .HasOne(sc => sc.Report)
                .WithMany(s => s.ReportStatuses)
                .HasForeignKey(sc => sc.ReportId);

            modelBuilder.Entity<ReportStatus>()
                .HasOne(sc => sc.Status)
                .WithMany(c => c.ReportStatuses)
                .HasForeignKey(sc => sc.StatusId);
        }
    }
}