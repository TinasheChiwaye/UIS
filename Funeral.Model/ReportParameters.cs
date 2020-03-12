using System;

namespace Funeral.Model
{
    public class ReportParameters
    {
        public string ReportName { get; set; }
        public Guid parlourId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string Email { get; set; }
    }
}
