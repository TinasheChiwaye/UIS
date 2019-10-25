using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model.Search
{
    public class PaymentSearch : BaseSearch
    {
        public int StatusId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
