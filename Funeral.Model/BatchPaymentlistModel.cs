using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class BatchPaymentlistModel
    {
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
