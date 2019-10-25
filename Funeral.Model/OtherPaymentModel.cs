using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{   
   
    public class OtherPaymentModel
    {
        public OtherPaymentModel()
        {
            DatePaid = System.DateTime.Today;
            RecievedBy = string.Empty;
            PaymentBranch = string.Empty;
            PaidBy = string.Empty;
            Notes = string.Empty;
        }
        public int pkiInvoiceID { get; set; }
        public int MemberID { get; set; }
        public DateTime DatePaid { get; set; }
        public Decimal AmountPaid { get; set; }
        public string RecievedBy { get; set; }
        public string PaidBy { get; set; }
        public string Notes { get; set; }
        public Guid Parlourid { get; set; }
        public string PaymentBranch { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime PaidUntil { get; set; }
        public int InvNumber { get; set; }
        public Guid DeviceCollectionID { get; set; }
        public string MethodOfPayment { get; set; }
        public string Discount { get; set; }
        public int PaymentTypeId { get; set; }
   
        
    }
}
