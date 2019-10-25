using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    class QuotationDiscountsModel
    {
        #region Properties


        public virtual int DiscountID
        {
            get;
            set;
        }

        public virtual int QuotationID
        {
            get;
            set;
        }


        public virtual DateTime DatePaid
        {
            get;
            set;
        }


        public virtual int Amount
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
        #endregion
    }
}
