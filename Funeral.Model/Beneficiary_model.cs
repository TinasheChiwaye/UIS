using System;
using System.ComponentModel.DataAnnotations;

namespace Funeral.Model
{
    public class Beneficiary_model
    {
        public int pkiBeneficiaryID { get; set; }
        [Required(ErrorMessage = "Please enter Relationship Type")]
        public string RelationshipType_Beneficiary { get; set; }
        [Required(ErrorMessage = "Please enter Full name")]
        public string FullName_Beneficiary { get; set; }
        [Required(ErrorMessage = "Please enter Surname")]
        public string Surname_Beneficiary { get; set; }
        [Required(ErrorMessage = "Please enter Date of birth")]
        public DateTime DateOfBirth_Beneficiary { get; set; }
        [Required(ErrorMessage = "Please enter ID Number")]
        public string IDNumber_Beneficiary { get; set; }
        public string Code_Beneficiary { get; set; }
        [Required(ErrorMessage = "Please enter Dependency Type")]
        public int DependencyType_Beneficiary { get; set; }
        public string DependencyName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public Guid parlourid { get; set; }
        public int pkiMemberID { get; set; }
        [Required(ErrorMessage = "Please Enter Cellphone Number")]
        public string Cellphone_Beneficiary { get; set; }

        [Required(ErrorMessage = "Please Enter Date of birth")]
        public string DateOfBirth_string { get; set; }

        [Required(ErrorMessage = "Please Enter Percentage")]
        public decimal Percentages { get; set; }


    }
}
