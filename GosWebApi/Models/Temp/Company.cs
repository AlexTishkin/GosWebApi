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
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<CompanySubTheme> CompanySubThemes { get; set; }

        public Company()
        {
            Users = new List<ApplicationUser>();
            CompanySubThemes = new List<CompanySubTheme>();
        }
    }
}