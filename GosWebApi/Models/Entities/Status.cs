using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GosWebApi.Models.Entities
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public ICollection<ReportStatus> ReportStatuses { get; set; }

        public Status()
        {
            ReportStatuses = new List<ReportStatus>();
        }

        public Status(Guid id, string name, int order)
        {
            Id = id;
            Name = name;
            Order = order;
        }
    }
}