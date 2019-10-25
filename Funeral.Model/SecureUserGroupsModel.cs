using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class SecureUserGroupsModel
    {
        public SecureUserGroupsModel()
        {
            sSecureUserGroupDesc = string.Empty;
            LastModified = System.DateTime.Now;
            ModifiedUser = string.Empty;
        }
        public int pkiSecureUserGroups { get; set; }
        public int fkiSecureUserID { get; set; }
        public int fkiSecureGroupID { get; set; }
        public string sSecureUserGroupDesc { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
    }
}
