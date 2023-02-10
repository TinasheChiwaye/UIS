using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class FuneralServiceManageModel
    {
        public FuneralServiceManageModel()
        {
            InitVariables();
        }
        public void InitVariables()
        { 
            this.pkiServiceID =0;
            this.ServiceName=string.Empty;
            this.ServiceDesc=string.Empty;
            this.QTY=0;
            this.ServiceCost = 0;
            this.FuneralServiceType = 0;
        }
        public int  pkiServiceID { get; set; }
        public int FuneralServiceType { get; set; }
        public string FuneralServiceTypeText { get; set; }
        public string ServiceName { get; set; } 
        public string ServiceDesc { get; set; }
        public Decimal ServiceCost { get; set; }
        public int QTY { get; set; }
        public Guid parlourid { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public int VendorId { get; set; }
        public Decimal CostOfSale { get; set; }
        public string VendorName { get; set; }
    }
}
