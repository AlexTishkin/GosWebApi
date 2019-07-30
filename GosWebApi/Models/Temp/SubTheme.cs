using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GosWebApi.Models
{
    public class SubTheme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Guid ThemeId { get; set; }
        public Theme Theme { get; set; }

        public ICollection<CompanySubTheme> CompanySubThemes { get; set; }
        public ICollection<Report> Reports { get; set; }

        public SubTheme()
        {
            CompanySubThemes = new List<CompanySubTheme>();
            Reports = new List<Report>();
        }

        public SubTheme(Guid id, string name, Theme theme)
        {
            Id = id;
            Name = name;
            Theme = theme;
            CompanySubThemes = new List<CompanySubTheme>();
            Reports = new List<Report>();
        }
    }
}