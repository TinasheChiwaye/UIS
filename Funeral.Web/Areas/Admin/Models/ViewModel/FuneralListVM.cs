using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funeral.Web.Areas.Admin.Models.ViewModel
{
    public class FuneralListVM
    {
        public int FuneralID { get; set; }
        public int MemberID { get; set; }
        public int InvoiceID { get; set; }
        public DateTime DatePaid { get; set; }
        public decimal AmountPaid { get; set; }
        public string TimePrinted { get; set; }
        public string RecievedBy { get; set; }
        public string PaidBy { get; set; }
        public string Notes { get; set; }
        public string UserName { get; set; }
        public Guid ParlourId { get; set; }
        public string BusinessAddressLine1 { get; set; }
        public string BusinessAddressLine2 { get; set; }
        public string BusinessAddressLine3 { get; set; }
        public string BusinessPostalCode { get; set; }
        public string FSBNumber { get; set; }
        public string MonthPaid { get; set; }
        public string telephoneNumber { get; set; }
        public string ParlourName { get; set; }
        public FuneralListVM()
        {
            FuneralID = 0;
            MemberID = 0;
            InvoiceID = 0;
            DatePaid = DateTime.Now;
            AmountPaid = 0;
            RecievedBy = string.Empty;
            Notes = string.Empty;
            PaidBy = string.Empty;
            UserName = string.Empty;
            ParlourId = System.Guid.NewGuid();
        }
    }
}