using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GosWebApi.Models.Entities
{
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Report> Reports { get; set; }

        public Region()
        {
            Reports = new List<Report>();
        }

        public Region(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}