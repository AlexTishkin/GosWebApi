using Microsoft.AspNetCore.Identity;
using System;
using GosWebApi.Models.Entities;

namespace GosWebApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public Guid? CompanyId { get; set; }
        public Company Company { get; set; }
    }
}