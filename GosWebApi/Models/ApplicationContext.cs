using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GosWebApi.Models
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        // Entities
        public DbSet<Company> Companies { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<SubTheme> SubThemes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Company>()
            //    .HasOne(sc => sc.Id)
            // ТУТ!Ё!!!!!!!!!!!!!
            modelBuilder.Entity<ApplicationUser>()
                .HasOne<Company>(s => s.Company)
                .WithMany(g => g.Users)
                .HasForeignKey(s => s.CompanyId);


            modelBuilder.Entity<CompanySubTheme>()
                .HasKey(t => new { t.CompanyId, t.SubThemeId });

            modelBuilder.Entity<CompanySubTheme>()
                .HasOne(sc => sc.Company)
                .WithMany(s => s.CompanySubThemes)
                .HasForeignKey(sc => sc.CompanyId);

            modelBuilder.Entity<CompanySubTheme>()
                .HasOne(sc => sc.SubTheme)
                .WithMany(c => c.CompanySubThemes)
                .HasForeignKey(sc => sc.SubThemeId);


        }
    }

}