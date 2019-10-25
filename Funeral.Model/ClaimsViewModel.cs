using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class ClaimsViewModel
    {
        public List<ClaimsModel> ClaimsList { get; set; }
        public Int64 TotalRecord { get; set; }
    }
}
