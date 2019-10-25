using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class NewMemberInvoiceModel
    {
        public int InvoiceID { get; set; }
        public int MemberID { get; set; }
        public DateTime DatePaid { get; set; }
        public decimal AmountPaid { get; set; }
        public string RecievedBy { get; set; }
        public int InvNumber { get; set; }
        public string PaymentBranch { get; set; }
        public string PaidBy { get; set; }
        public string Notes { get; set; }
        public Guid parlourid { get; set; }
        public DateTime PaidUntil { get; set; }
        public string MethodOfPayment { get; set; }

    }

    public class PolicyPremiumDashboardModel
    {
       
        public string DatePaid { get; set; }
        public decimal AmountPaid { get; set; }
        public string RecievedBy { get; set; }       
        public string PaymentBranch { get; set; }

    }
}
