using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class QuotationDiscountModel
    {
        public QuotationDiscountModel() 
        {
            InitVariables();
        }
        public void InitVariables()
        {
            this.DiscountID = 0;
            this.QuotationID = 0;
            this.DatePaid = DateTime.MinValue;
            this.Amount = 0;
            this.LastModified = DateTime.MinValue;
            this.ModifiedUser = string.Empty;
        }
        #region Properties
        public int DiscountID { get; set; }
        public int QuotationID { get; set; }
        public DateTime DatePaid { get; set; }
        public int Amount { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }

        #endregion
    }
}
