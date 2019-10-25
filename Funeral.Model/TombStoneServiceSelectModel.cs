using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class TombStoneServiceSelectModel
    {
        public TombStoneServiceSelectModel()
        {
            InitVariables();
        }

        public void InitVariables()
		{
            this.pkiTombStoneSelectionID = 0;
			this.fkiTombstoneID = 0;
			this.fkiServiceID = 0;
            this.Quantity = 0;
			this.lastModified = DateTime.MinValue;
			this.modifiedUser = string.Empty;
            this.Amount = Decimal.MaxValue;
            this.ServiceRate = Decimal.MaxValue;
           

            this.pkiServiceID = 0;
            this.ServiceName = string.Empty;
            this.ServiceDesc = string.Empty;
            this.ServiceCost =  Decimal.MaxValue;
            this.QTY = 0;
            this.parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            this.LastModified = DateTime.MinValue;
            this.ModifiedUser = string.Empty;
		}
        public virtual int pkiTombStoneSelectionID { get; set; }
        public virtual int fkiTombstoneID {get; set;}
        public virtual int fkiServiceID { get; set; }
        public virtual int Quantity { get; set; }
        public virtual DateTime lastModified { get; set; }
        public virtual  string modifiedUser { get; set; }
        public virtual  Decimal Amount { get; set; }

       

        public virtual int pkiServiceID { get; set; }
        public virtual string ServiceName { get; set; }
        public virtual string ServiceDesc { get; set; }
        public virtual Decimal ServiceCost { get; set; }
        public virtual int QTY { get; set; }
        public virtual Guid parlourid { get; set; }
        public virtual DateTime LastModified { get; set; }
        public virtual string ModifiedUser { get; set; }
        public virtual Decimal ServiceRate { get; set; }
    }
}
