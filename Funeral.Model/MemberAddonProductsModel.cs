using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class MemberAddonProductsModel
    {
        public MemberAddonProductsModel()
        {
            ProductName = string.Empty;
        }
        public Guid pkiMemberProductID  {get;set;}
        public DateTime DateCreated { get; set; }
        public string UserID { get; set; }
        [Required(ErrorMessage = "ProductName is required.")]
        public Guid fkiProductID { get; set; }
        public int fkiMemberid { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime CoverDate { get; set; }
        public DateTime InceptionDate { get; set; }





        public Guid parlourid { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }

        [Required(ErrorMessage = "Premium is required.")]
        public Decimal? ProductCost { get; set; }
        public int? Deleted { get; set; }

        [Required(ErrorMessage ="ProductName is required.")]
        public string ProductName { get; set; }        
        public string ProductPrice { get; set; }
        public int DependencyType { get; set; }
        public string DependencyName { get; set; }
    }
}
