using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GosWebApi.Models.Entities
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsCommercial { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<CompanySubTheme> CompanySubThemes { get; set; }

        public ICollection<Report> Reports { get; set; }

        public Company()
        {
            Users = new List<ApplicationUser>();
            CompanySubThemes = new List<CompanySubTheme>();
            Reports = new List<Report>();
        }

        public Company(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSubThemes(params SubTheme[] subThemes)
        {
            if (subThemes is null || subThemes.Length == 0) return;
            if (CompanySubThemes is null) CompanySubThemes = new List<CompanySubTheme>();

            foreach (var subTheme in subThemes)
            {
                CompanySubThemes.Add(new CompanySubTheme(Id, subTheme.Id));
            }
        }

    }
}