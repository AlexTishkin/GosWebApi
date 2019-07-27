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
    public class CompanySubTheme
    {
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public Guid SubThemeId { get; set; }
        public SubTheme SubTheme { get; set; }
    }
}