using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class tblPageModel
    {
        public int ID { get; set; }
        public string PageName { get; set; }
        public string PageTitle { get; set; }
        public string URL { get; set; }
        public string MVCURL { get; set; }
        public bool IsActive { get; set; }
        public bool IsExcludedRight { get; set; }
        public int ParentRoleId { get; set; }
        public string IconClass { get; set; }
        public int MenuOrder { get; set; }
        public int MenuLevel { get; set; }
        public bool IsMenu { get; set; }
        public bool HasAccess { get; set; }
        public bool IsRead { get; set; }
        public bool IsWrite { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsReversalPayment { get; set; }
        public int GroupId { get; set; }
    }
}
