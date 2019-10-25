using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class smsSendingGroupModel
    {
        public smsSendingGroupModel()
        {
            smstempletName = string.Empty;
            LastModified = System.DateTime.Now;
            ModifiedUser = string.Empty;
        }
        public int ID { get; set; }
        public int fkiCompanyID { get; set; }
        public int fkismstempletID { get; set; }
        public string smstempletName { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }        
    }
}
