using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class SendReminderModel
    {
        public Guid parlourid { get; set; }
        public string MemberData { get; set; }
        public Int64 MemeberToNumber { get; set; }
        public string MemeberID { get; set; }
        public int PkId { get; set; }
    }
}
