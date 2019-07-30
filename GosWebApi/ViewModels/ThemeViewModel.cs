using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GosWebApi.Models;
using GosWebApi.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GosWebApi.ViewModels
{
    public class ThemeViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<SubThemeViewModel> SubThemes { get; set; }

        public ThemeViewModel()
        {
            SubThemes = new List<SubThemeViewModel>();
        }

        public ThemeViewModel(Guid id, string name, ICollection<SubTheme> subThemes)
        {
            Id = id;
            Name = name;
            SubThemes = SubThemeViewModel.From(subThemes);
        }
    }
}