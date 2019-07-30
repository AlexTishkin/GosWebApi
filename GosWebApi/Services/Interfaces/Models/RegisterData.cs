using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GosWebApi.Services.Interfaces.Models
{
    public class RegisterData
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public RegisterData(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}