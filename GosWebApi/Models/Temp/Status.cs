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
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public ICollection<ReportStatus> ReportStatuses { get; set; }

        public Status()
        {
            ReportStatuses = new List<ReportStatus>();
        }

        public Status(Guid id, int order, string name)
        {
            Id = id;
            Order = order;
            Name = name;
        }
    }
}