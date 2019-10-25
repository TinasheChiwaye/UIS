using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class PackageServicesSelectionModel
    {
        public int pkiPackageID { get; set; }
        public string PackageName { get; set; }
        public int fkiServiceID { get; set; }
        public DateTime LastModified { get; set; }
        public string ServiceName { get; set; }
        public decimal ServiceCost { get; set; }
        public string ServiceDesc { get; set; }
    
    }
}
