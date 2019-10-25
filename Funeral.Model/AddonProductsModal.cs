using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            LastModified = System.DateTime.Now;
            ModifiedUser = string.Empty;
            ProductName = string.Empty;
           
        }
        public Guid pkiProductID { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserID { get; set; }
        public string ProductDesc { get; set; }
        public decimal ProductCost { get; set; }
        public decimal ProductCover { get; set; }
        public int IsProductOngoing { get; set; }
       public int IsProductLaybye { get; set; }
        public Guid Parlourid { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public string ProductName { get; set; }
        
        
    }
}