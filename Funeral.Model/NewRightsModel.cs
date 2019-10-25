using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class NewRightsModel
    {
        public int ID { get; set; }
        public int PageId { get; set; }
        public int GroupId { get; set; }
        public bool HasAccess { get; set; }
        public bool IsRead { get; set; }
        public bool IsWrite { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsReversalPayment { get; set; }
        public Guid ParlourId { get; set; }
        public string PageName { get; set; }
    }
}
