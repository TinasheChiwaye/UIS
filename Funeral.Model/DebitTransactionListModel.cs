using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class DebitTransactionListModel
    {
        public int pkiTransactionID { get; set; }
        public int SequenceNo { get; set; }
        public int BranchCode { get; set; }
        public string AccountType { get; set; }
        public int AccountNo { get; set; }
        public Decimal DebitAmount { get; set; }
        public DateTime DebitDate { get; set; }
        public Decimal BatchAmount { get; set; }
        public int Reference { get; set; }
        public string AccountHolder { get; set; }
        public string CellNotification { get; set; }
        public string EmailNotification { get; set; }
        public int TransactionRefNo { get; set; }
        public Guid Parlourid { get; set; }
        public string Status { get; set; }
        public int Tel { get; set; }
        public string PolicyNo { get; set; }
    }
}
