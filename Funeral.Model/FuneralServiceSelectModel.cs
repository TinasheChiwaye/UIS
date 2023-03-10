using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class FuneralServiceSelectModel
    {
        public FuneralServiceSelectModel()
        {
            InitVariables();
        }

        public void InitVariables()
        {

            this.pkiFuneralServiceSelectionID = 0;
            this.fkiFuneralID = 0;
            this.fkiServiceID = 0;
            this.Quantity = 0;
            this.lastModified = DateTime.MinValue;
            this.modifiedUser = string.Empty;
            this.Amount = Decimal.MaxValue;
            this.ServiceRate = Decimal.MaxValue;


            this.pkiServiceID = 0;
            this.ServiceName = string.Empty;
            this.ServiceDesc = string.Empty;
            this.ServiceCost = Decimal.MaxValue;
            this.QTY = 0;
            this.parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            this.LastModified = DateTime.MinValue;
            this.ModifiedUser = string.Empty;
        }
        public virtual int pkiFuneralServiceSelectionID { get; set; }
        public virtual int fkiFuneralID { get; set; }
        public virtual int fkiServiceID { get; set; }
        public virtual int Quantity { get; set; }
        public virtual DateTime lastModified { get; set; }
        public virtual string modifiedUser { get; set; }
        public virtual Decimal Amount { get; set; }



        public virtual int pkiServiceID { get; set; }
        public virtual string ServiceName { get; set; }
        public virtual string ServiceDesc { get; set; }
        public virtual Decimal ServiceCost { get; set; }
        public virtual int QTY { get; set; }
        public virtual Guid parlourid { get; set; }
        public virtual DateTime LastModified { get; set; }
        public virtual string ModifiedUser { get; set; }
        public virtual Decimal ServiceRate { get; set; }

        public FuneralEnum.FuneralServiceType FuneralServiceType { get; set; }
    }
}
