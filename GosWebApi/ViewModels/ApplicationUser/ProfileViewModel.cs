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
    public class ProfileViewModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        //public IEnumerable<Status> AllStatuses { get; set; }

        public IEnumerable<ProfileReportViewModel> reports { get; set; }

        public ProfileViewModel(string lastName, string firstName, string middleName,
            IEnumerable<ProfileReportViewModel> reports)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            this.reports = reports;
        }
    }

    public class ProfileReportViewModel
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime Datetime { get; set; }
        public StatusViewModel LastStatus { get; set; }

        public ProfileReportViewModel(Guid id, string message, DateTime datetime, StatusViewModel lastStatus)
        {
            Id = id;
            Message = message;
            Datetime = datetime;
            LastStatus = lastStatus;
        }
    }

    public class StatusViewModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public DateTime Datetime { get; set; }
    }
}