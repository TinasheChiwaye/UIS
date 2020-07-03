using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class DebitOrderTransactionModel
    {
        public DebitOrderTransactionModel()
        {
            InitVariables();
        }

        public void InitVariables()
        {
            this.pkiTransactionID = 0;
            this.SequenceNo = 0;
            this.BranchCode = 0;
            this.AccountType = string.Empty;
            this.AccountNo = 0;
            this.DebitAmount = 0;
            this.DebitDate = DateTime.MinValue;
            this.BatchAmount = 0;
            this.Reference = 0;
            this.AccountHolder = string.Empty;
            this.CellNotification = string.Empty;
            this.EmailNotification = string.Empty;
            this.TransactionRefNo = 0;
            this.Parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            this.Status = string.Empty;
            this.Tel = 0;
            this.PolicyNo = string.Empty;

        }
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



