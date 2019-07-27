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
    public class ReportStatus
    {
        public Guid ReportId { get; set; }
        public Report Report { get; set; }

        public Guid StatusId { get; set; }
        public Status Status { get; set; }
    }
}