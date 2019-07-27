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
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string FailMessage { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        // Оценка [1..5]
        public int Mark { get; set; }
        public string MarkDescription { get; set; }

        // Компания, что реализует задачу...
        public Guid? CompanyId { get; set; }
        public Company Company { get; set; }

        // Регион (Пусть пока будет)
        public Guid? RegionId { get; set; }
        public Region Region { get; set; }

        public ICollection<ReportStatus> ReportStatuses { get; set; }

        public Report()
        {
            ReportStatuses = new List<ReportStatus>();
        }
    }
}