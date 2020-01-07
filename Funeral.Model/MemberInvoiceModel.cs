using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class MemberInvoiceModel
    {
        public MemberInvoiceModel()
        {
            DatePaid = string.Empty;
            AmountPaid = string.Empty;
            RecievedBy = string.Empty;
            InvNumber = string.Empty;
            PaymentBranch = string.Empty;
            PaidBy = string.Empty;
            Notes = string.Empty;
        }
        public int InvoiceID { get; set; }
        public int MemberID { get; set; }
        public string DatePaid { get; set; }
        public string AmountPaid { get; set; }
        public decimal AmountPaidNumeric { get; set; }
        public bool IsReversal { get; set; }
        public int ReversalInvoiceID { get; set; }
        public string RecievedBy { get; set; }
        public string InvNumber { get; set; }
        public string PaymentBranch { get; set; }
        public string PaidBy { get; set; }
        public string Notes { get; set; }
        public Guid parlourid { get; set; }
        public DateTime PaidUntil { get; set; }
        public string MethodOfPayment { get; set; }

    }

}
