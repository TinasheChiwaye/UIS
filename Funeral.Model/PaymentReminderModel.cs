using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class PaymentReminderModel
    {
        public int pkiMemberId { get; set; }
        public int fkiMemberId { get; set; }
        public string Notes { get; set; }
        public string DateLastPaid { get; set; }
        public decimal Premium { get; set; }
        public string ReferenceNumber { get; set; }
        public string MemberNotes { get; set; }
    }
}
