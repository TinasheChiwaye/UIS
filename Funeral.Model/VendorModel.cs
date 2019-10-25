using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class VendorModel
    {
        public VendorModel()
        {
            pkiVendorID = 0;
            VendorName = string.Empty;
            parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            LastModified = System.DateTime.Now;
            ModifiedUser = string.Empty;
        }
        public int pkiVendorID { get; set; }
        public string VendorName { get; set; }
        public Guid parlourid { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
    }
}
