using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funeral.Web.Areas.Admin.Models.ViewModel
{
    public class GroupPaymentVM
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string PolicyNumber { get; set; }
        public string DatePaid { get; set; }
        public Decimal AmountPaid { get; set; }
        public string PaidBy { get; set; }
        public string ReceivedBy { get; set; }
        public string MonthPaid { get; set; }
        public string ParlourName { get; set; }
        public string TimePrinted { get; set; }
        public string Underwriter { get; set; }
        public string BusinessAddressLine1 { get; set; }
        public string BusinessAddressLine2 { get; set; }
        public string BusinessAddressLine3 { get; set; }
        public string BusinessPostalCode { get; set; }
        public string FSBNumber { get; set; }
        public string telephoneNumber { get; set; }

        public string GroupName { get; set; }

        public string RefernceNumer { get; set; }

        public string Notes { get; set; }
    }
}