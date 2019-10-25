using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class smsTempletModel
    {
        public smsTempletModel()
        {

            ID = 0;
            Name = string.Empty;
            smsText = string.Empty;
            LastModified = System.DateTime.Now;
            ModifiedUser = string.Empty;
            IsActive = true;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string smsText { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; }
    }
}
