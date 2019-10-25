using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class TombStonesPaymentModel
    {
        public int InvoiceID { get; set; }
        public int TombstoneID { get; set; }
        public DateTime DatePaid { get; set; }
        public decimal AmountPaid { get; set; }
        public string RecievedBy { get; set; }
        public string PaidBy { get; set; }
        public string Notes { get; set; }
        public Guid parlourid { get; set; }
        public string MemberBranch { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
  
    }
}
