using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
  public class BankingDetailSending
    {

        public BankingDetailSending()
        {
            AccountHolder = string.Empty;
            Bankname = string.Empty;
            AccountNumber = string.Empty;
            Accounttype = string.Empty;
            Branch = string.Empty;
            Branchcode = string.Empty;
            LastModified = System.DateTime.Now;
            ModifiedUser = string.Empty;
            
        }

        [Required(ErrorMessage = "Please enter Account Holder")]
        public string AccountHolder { get; set; }

        [Required(ErrorMessage = "Please enter Bank name")]
        public string Bankname { get; set; }

        [Required(ErrorMessage = "Please enter Account Number")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Please enter Account type")]
        public string Accounttype { get; set; }

        [Required(ErrorMessage = "Please enter Branch")]
        public string Branch { get; set; }

        [Required(ErrorMessage = "Please enter Branch code")]
        public string Branchcode { get; set; }

        public Guid parlourid { get; set; }

        public DateTime LastModified { get; set; }

        public string ModifiedUser { get; set; }
    }
}
