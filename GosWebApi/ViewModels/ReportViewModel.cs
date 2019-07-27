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
    public class ReportViewModel
    {
        public string Message { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}