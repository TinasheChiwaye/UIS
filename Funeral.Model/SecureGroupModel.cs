using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class SecureGroupModel
    {
        public SecureGroupModel()
        {
            sSecureGroupName = string.Empty;
        }
       public int pkiSecureGroupID { get; set; }
       public string sSecureGroupName { get; set; }
    }
}
