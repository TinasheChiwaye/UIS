using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class AgentInfoSetupModel
    {
        public AgentInfoSetupModel()
        {
            Surname = string.Empty;
            Fullname = string.Empty;
            ContactNumber = string.Empty;
            Address1 = string.Empty;
            Address2 = string.Empty;
            Address3 = string.Empty;
            Address4 = string.Empty;
            Code = string.Empty;
            Email = string.Empty;
            ModifiedUser = string.Empty;
        }


        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter fullname")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Please enter percentage")]
        public Decimal percentage { get; set; }

        public Guid parlourid { get; set; }

        [Required(ErrorMessage = "Please enter contact number")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Please enter address1")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Address4 { get; set; }

        [Required(ErrorMessage = "Please enter code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; }

        public DateTime LastModified { get; set; }

        public string ModifiedUser { get; set; }


    }

}
