using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Funeral.Model
{
	
	public class QuotationModel
	{		

		#region Constructors
		public QuotationModel()
		{
				InitVariables();
		}	
		
		#endregion

        #region Methods
     
        public void InitVariables()
        {
           
            this.ContactTitle = string.Empty;
            this.ContactFirstName = string.Empty;
            this.ContactLastName = string.Empty;
            this.TelNumber = string.Empty;
            this.CellNumber = string.Empty;
            this.AddressLine1 = string.Empty;
            this.AddressLine2 = string.Empty;
            this.AddressLine3 = string.Empty;
            this.AddressLine4 = string.Empty;
            this.Code = string.Empty;
            this.DateOfQuotation = DateTime.MinValue;
            this.parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            this.LastModified = DateTime.MinValue;
            this.ModifiedUser = string.Empty;
            this.QuotationNumber = string.Empty;
            this.QuotationNumber2 = string.Empty;
            this.Notes = string.Empty;
            this.Tax = 0;
            this.Discount = 0;
            this.QuotationStatus = string.Empty;
        }
        #endregion

        #region Properties

        public virtual int QuotationID
        {
            get;
            set;
        }

        public virtual string ContactTitle
        {
            get;
            set;
        }

        
        public virtual string ContactFirstName
        {
            get;
            set;
        }

        public virtual string ContactLastName
        {
            get;
            set;
        }

        public virtual string TelNumber
        {
            get;
            set;
        }

        public virtual string CellNumber
        {
            get;
            set;
        }

        public virtual string AddressLine1
        {
            get;
            set;
        }

        public virtual string AddressLine2
        {
            get;
            set;
        }

        public virtual string AddressLine3
        {
            get;
            set;
        }

        public virtual string AddressLine4
        {
            get;
            set;
        }

        public virtual string Code
        {
            get;
            set;
        }

        public virtual DateTime DateOfQuotation
        {
            get;
            set;
        }


        public virtual Guid parlourid
        {
            get;
            set;
        }


        public virtual DateTime LastModified
        {
            get;
            set;
        }


        public virtual string ModifiedUser
        {
            get;
            set;
        }

        public virtual string QuotationNumber { get; set; }

        public virtual string QuotationNumber2 { get; set; }

        public virtual string Notes { get; set; }
        public virtual Decimal Tax { get; set; }
        public virtual Decimal Discount { get; set; }
        public virtual string QuotationStatus { get; set; }

		#endregion
		
	}
}
