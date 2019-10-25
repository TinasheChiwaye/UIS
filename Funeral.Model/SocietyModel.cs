using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public  class SocietyModel
    {
        public SocietyModel()
        {

            pkiSocietyID = 0;
            SocietyName = string.Empty;
            parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            LastModified = System.DateTime.Now;
            ModifiedUser = string.Empty;
        }
        public int pkiSocietyID { get; set; }
        public string SocietyName { get; set; }
        public Guid parlourid { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
    }
}
