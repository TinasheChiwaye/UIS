using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class UnderwriterSetupModel
    {

        public int PkiUnderWriterSetupId { get; set; }

        [Required(ErrorMessage ="UnderWriterName is required.")]
        public string UnderwriterName { get; set; }
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "AddressLine1 is required.")]
        public string AddressLine1 { get; set; }

        [Required(ErrorMessage = "AddressLine2 is required.")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Province is required.")]
        public string Province { get; set; }
        
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "ContactEmail is required.")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "FSPNNumber is required.")]
        public string FSPNNumber { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Guid Parlourid { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreateddDate { get; set; }
        public Byte[] UnderwriterLogo { get; set; }
        public string UnderwriterLogoPath { get; set; }




    }
}
