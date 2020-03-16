using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model.Search
{
    public class MemberSearch : BaseSearch
    {
        public string StatusId { get; set; }
        public Guid CompanyId { get; set; }

        public string BookID { get; set; }



    }
}
