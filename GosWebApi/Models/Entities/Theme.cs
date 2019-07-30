using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GosWebApi.Models.Entities
{
    public class Theme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<SubTheme> SubThemes { get; set; }

        public Theme()
        {
            SubThemes = new List<SubTheme>();
        }

        public Theme(string name)
        {
            Name = name;
        }
    }
}