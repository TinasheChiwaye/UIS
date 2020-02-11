using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class RegenerateBillingModal
    {
        public Guid parlourId { get; set; }
        public DateTime PremiumDueDate { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
