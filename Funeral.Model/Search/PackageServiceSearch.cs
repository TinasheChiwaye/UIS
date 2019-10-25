using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model.Search
{
    public class PackageServiceSearch : BaseSearch
    {
        public string ServiceName { get; set; }
        public int PackageId { get; set; }
    }
}
