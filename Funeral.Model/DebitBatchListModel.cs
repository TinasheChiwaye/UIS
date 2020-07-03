using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class DebitBatchListModel
    {
        public DebitBatchListModel()
        {
            InitVariables();
        }

        public void InitVariables()
        {
            this.pkiDebitBatch = 0;
            this.ActionDate = DateTime.MinValue;
            this.ServiceType = string.Empty;
            this.BatchName = string.Empty;
            this.Volume = 0;
            this.Amount = 0;
            this.UnpaidValue = 0;
            this.UnpaidVolumes = 0;
            this.Parlourid = new Guid("00000000-0000-0000-0000-000000000000");
        }


        public int pkiDebitBatch { get; set; }
        public DateTime? ActionDate { get; set; }
        public string ServiceType { get; set; }
        public string BatchName { get; set; }
        public int Volume { get; set; }
        public Decimal Amount { get; set; }
        public Decimal UnpaidValue { get; set; }
        public int UnpaidVolumes { get; set; }
        public Guid Parlourid { get; set; }


    }
}
