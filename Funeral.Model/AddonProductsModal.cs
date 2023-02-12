using System;

namespace Funeral.Model
{
    public class AddonProductsModal
    {
        public AddonProductsModal()
        {
            pkiProductID = new Guid("00000000-0000-0000-0000-000000000000");
            DateCreated = System.DateTime.Now;
            UserID = string.Empty;
            ProductDesc = string.Empty;
            ProductCost = 0;
            ProductCover = 0;
            IsProductOngoing = 0;
            IsProductLaybye = 0;
            parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            LastModified = System.DateTime.Now;
            ModifiedUser = string.Empty;
            ProductName = string.Empty;
            InceptionDate = System.DateTime.Now;
            StartDate = System.DateTime.Now;
            CoverDate = System.DateTime.Now;
            WaitingPeriod = 0;
            LapsePeriod = 0;
        }
        public Guid pkiProductID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime InceptionDate { get; set; }

        //public int WaitingPeriod { get; set; }
        public int WaitingPeriod { get;  set; }

        public int LapsePeriod { get; set; }



        public DateTime StartDate{ get; set; }
        public DateTime CoverDate { get; set; }



        public string UserID { get; set; }
        public string ProductDesc { get; set; }
        public decimal ProductCost { get; set; }
        public decimal ProductCover { get; set; }
        public decimal UnderwriterPremium { get; set; }
        public int IsProductOngoing { get; set; }
        public int IsProductLaybye { get; set; }
        //public Guid Parlourid { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public string ProductName { get; set; }
        public Guid parlourid { get; set; }
        public Guid SchemeID { get; set; }



    }
}