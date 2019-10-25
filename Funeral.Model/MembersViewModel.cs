using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class MembersViewModel
    {
        public List<MembersModel> MemberList { get; set; }
        public Int64 TotalRecord { get; set; }
    }

    public class MembersPaymentViewModel
    {
        public List<MembersPaymentModel> MemberList { get; set; }
        public Int64 TotalRecord { get; set; }
    }
}
