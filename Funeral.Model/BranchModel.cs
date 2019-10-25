using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class BranchModel
    {
        public BranchModel()
        {
            Brancheid = new Guid("00000000-0000-0000-0000-000000000000");
            BranchName = string.Empty;
            Parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            LastModified = System.DateTime.Now;
            ModifiedUser = string.Empty;
            Address1 = string.Empty;
            Address2 = string.Empty;
            Address3 = string.Empty;
            Address4 = string.Empty;
            Code = string.Empty;
            TelNumber = string.Empty;
            CellNumber = string.Empty;
            BranchCode = string.Empty;
            Region = string.Empty;
        }

        public Guid Brancheid { get; set; }

        [Required(ErrorMessage = "Please enter Branch Name")]
        public string BranchName { get; set; }

        public Guid Parlourid { get; set; }

        public DateTime LastModified { get; set; }

        public string ModifiedUser { get; set; }

        [Required(ErrorMessage = "Please enter Physical Address")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Address4 { get; set; }

        [Required(ErrorMessage = "Please Enter Postal Code")]
        [RegularExpression(pattern: @"^[0-9]*$", ErrorMessage = "Postal Code Enter Only Number")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please enter Phone number")]
        [RegularExpression(pattern: @"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Phone Number Enter Only Number")]
        public string TelNumber { get; set; }     
        
        [RegularExpression(pattern: @"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Cell Number Enter Only Number")]
        public string CellNumber { get; set; }

        public string Region { get; set; }

        public string BranchCode { get; set; }
    }
}
