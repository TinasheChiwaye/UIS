using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class QuotationMessageModel
    {
        public QuotationMessageModel()
        {
            InitVariables();
        }

        public void InitVariables()
        {
            this.pkidQuotationMsg = 0;
            this.QuotationID = 0;
            this.Message = string.Empty;
            this.CreatedDate = DateTime.MinValue;
            this.LastModified = DateTime.MinValue;
            this.ModifiedUser = string.Empty;
        }

        public int pkidQuotationMsg { get; set; }
        public int QuotationID { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }


    }
}
