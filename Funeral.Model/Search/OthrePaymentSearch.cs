using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model.Search
{
    public class OthrePaymentSearch : BaseViewModel
    {
        
        public string SortOrder { get; set; }
        public string SortBy { get; set; }
        public int TotalRecord { get; set; }
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public string SearchPolicyId { get; set; }
        public string SearchId { get; set; }

    }
}
