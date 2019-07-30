using System;

namespace GosWebApi.Models.Entities
{
    public class ReportStatus
    {
        public Guid ReportId { get; set; }
        public Report Report { get; set; }

        public Guid StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime Datetime { get; set; }
    }
}