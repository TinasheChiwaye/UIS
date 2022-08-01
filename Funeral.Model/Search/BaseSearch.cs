using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model.Search
{
    public class BaseSearch : BaseViewModel
    {
        
        public string SortOrder { get; set; }
        public string SortBy { get; set; }
        public int TotalRecord { get; set; }
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public string SarchText { get; set; }
        public Guid CompanyId { get; set; }

        public string ClaimID { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string StatusId { get; set; }
        public string SocietyID { get; set; }
        public string keyword { get; set; }
        public string SearchType { get; set; }
    }
}
