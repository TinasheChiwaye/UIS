using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model.Search
{
    public class FuneralStatusSearch : BaseSearch
    {
        public int ClaimID { get; set; }
        public Guid CompanyId { get; set; }
    }
}
