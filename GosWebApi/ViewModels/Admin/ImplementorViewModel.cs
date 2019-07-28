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
    public class ImplementorViewModel
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Message { get; set; }
        public string FailMessage { get; set; }
        // Подача заявки Первая дата
        public DateTime Datetime { get; set; }
        public IEnumerable<StatusViewModel> Statuses { get; set; }

        public ImplementorViewModel(Guid id, string lastName, string firstName, string middleName, string message, string failMessage, DateTime datetime, IEnumerable<StatusViewModel> statuses)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            Message = message;
            FailMessage = failMessage;
            Datetime = datetime;
            Statuses = statuses;
        }
    }
}