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
    public class ReportArrayItemViewModel
    {
        public string CompanyName { get; set; }
        public string ThemeName { get; set; }
        public string SubThemeName { get; set; }
        public DateTime Datetime { get; set; }
        public IEnumerable<StatusViewModel> Statuses { get; set; }

        public ReportArrayItemViewModel(string companyName, string themeName, string subThemeName, DateTime datetime, IEnumerable<StatusViewModel> statuses)
        {
            CompanyName = companyName;
            ThemeName = themeName;
            SubThemeName = subThemeName;
            Datetime = datetime;
            Statuses = statuses;
        }
    }
}