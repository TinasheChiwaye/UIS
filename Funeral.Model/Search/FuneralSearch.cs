using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model.Search
{
    public class FuneralSearch : BaseSearch
    {
        public string ClaimID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Guid CompanyId { get; set; }
        public string StatusId { get; set; }
        public string SocietyID { get; set; }
        public string keyword { get; set; }
        public string SearchType { get; set; }
    }
}
