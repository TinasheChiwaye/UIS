using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class ChangeStatusReason
    {
        public int ID { get; set; }
        public string ChangeReason { get; set; }
        public int ClaimID { get; set; }
        public Guid ParlourID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ChangedBy { get; set; }
        public string CurrentStatus { get; set; }
        public string NewStatus { get; set; }
        public int fkiMemberId { get; set; }

    }
}