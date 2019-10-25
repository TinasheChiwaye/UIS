using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class PackageServiceModel
    {
        public int pkiPackageID { get; set; }
        [Required(ErrorMessage = "Please Enter Package Name")]
        public string PackageName { get; set; }
        public int? fkiServiceID { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public Guid ParlourId { get; set; }
        public int fkiPackageID { get; set; }
        public decimal Total { get; set; }
        public List<FuneralServiceList> ServiceList { get; set; }
    }

    public class FuneralServiceList
    {
        public int pkiServiceID { get; set; }
        public string ServiceName { get; set; }
    }
    public class Service
    {
        public string PackageName { get; set; }
        public int fkiPackageID { get; set; }
        public int fkiServiceID { get; set; }
        public string ServiceName { get; set; }
        public decimal ServiceCost { get; set; }
        public string ServiceDesc { get; set; }
    }


}
