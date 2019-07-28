using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GosWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GosWebApi.ViewModels
{
    public class SubThemeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public SubThemeViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static ICollection<SubThemeViewModel> From(ICollection<SubTheme> subThemes)
        {
            return subThemes.Select(subTheme => new SubThemeViewModel(subTheme.Id, subTheme.Name)).ToList();
        }
    }
}