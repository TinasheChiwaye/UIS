using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class ApplicationTnCModel
    {
        public ApplicationTnCModel()
        {
            InitVariables();
        }
        public void InitVariables()
        {
            this.pkiAppTC = 0;
            this.fkiApplicationID = 0;
            this.TermsAndCondition = string.Empty;
            this.parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            this.LastModified = DateTime.MinValue;
            this.ModifiedUser = string.Empty;
            this.TermsAndConditionFuneral = string.Empty;
            this.TermsAndConditionTombstone = string.Empty;
            this.QuotationTermsAndCondition = string.Empty;
            this.Declaration = string.Empty;
        }
        #region Properties
        public int pkiAppTC { get; set; }
        public int fkiApplicationID { get; set; }
        public string TermsAndCondition { get; set; }
      
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public Guid parlourid { get; set; }
        public string TermsAndConditionFuneral { get; set; }
        public string TermsAndConditionTombstone { get; set; }
        public string QuotationTermsAndCondition { get; set; }
        public string  Declaration { get; set; }
        #endregion

    }
}
