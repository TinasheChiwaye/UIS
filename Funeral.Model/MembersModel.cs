using System;
using System.ComponentModel.DataAnnotations;

namespace Funeral.Model
{
    public class MembersModel
    {
        public MembersModel()
        {

            CreateDate = System.DateTime.Now;
            MemberType = string.Empty;
            Title = string.Empty;
            FullNames = string.Empty;
            Surname = string.Empty;
            Gender = string.Empty;
            IDNumber = string.Empty;
            DateOfBirth = System.DateTime.Now;
            Telephone = string.Empty;
            Cellphone = string.Empty;
            Address1 = string.Empty;
            Address2 = string.Empty;
            Address3 = string.Empty;
            Address4 = string.Empty;
            Code = string.Empty;
            //New data feilds [Tab 2] - Postal Address
            Address1_Post = string.Empty;
            Address2_Post = string.Empty;
            Address3_Post = string.Empty;
            Address4_Post = string.Empty;
            Code_Post = string.Empty;
            //end
            MemeberNumber = string.Empty;
            MemberSociety = string.Empty;
            fkiPlanID = 1;
            Active = true;
            InceptionDate = System.DateTime.Now;
            Claimnumber = string.Empty;
            PolicyStatus = string.Empty;
            //   parlourid =Convert.tou string.Empty;
            Agent = string.Empty;
            AccountHolder = string.Empty;
            Bank = string.Empty;
            BranchCode = string.Empty;
            Branch = string.Empty;
            AccountNumber = string.Empty;
            AccountType = string.Empty;
            DebitDate = System.DateTime.Now;
            MemberBranch = string.Empty;
            CoverDate = System.DateTime.Now;
            LastModified = System.DateTime.Now;
            ModifiedUser = string.Empty;
            Email = string.Empty;
            Passport = string.Empty;
            Citizenship = string.Empty;
            EasyPayNo = string.Empty;
            RefNumber = string.Empty;
            PlanName = string.Empty;
            pkiAdditionalMemberInfo = new Guid("00000000-0000-0000-0000-000000000000");
            StartDate = System.DateTime.Now;
            Administratorparlourid = new Guid("00000000-0000-0000-0000-000000000000");

        }
        public int pkiMemberID { get; set; }
        public DateTime CreateDate { get; set; }
        public string MemberType { get; set; }
        public string Title { get; set; }
        public int Age { get; set; }

        [Required(ErrorMessage = "Please enter full name")]
        [RegularExpression(@"[a-zA-Z ]*$", ErrorMessage = "full name Enter Only characters")]
        public string FullNames { get; set; }
        public int SocietyId { get; set; }
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Surname")]
        [RegularExpression(@"[a-zA-Z ]*$", ErrorMessage = "Surname Enter Only characters")]
        public string Surname { get; set; }

        public string Gender { get; set; }

        //[Required(ErrorMessage = "Please enter id number")]
        //[ValidateId()]
        public string IDNumber { get; set; }

        public double TotalPremium { get; set; }

        public string Province { get; set; }

        [Required(ErrorMessage = "Please enter premium amount")]
        public string PolicyPremium { get; set; }
        public string CoverAmount { get; set; }
        public string PolicyNumber { get; set; }

        [Required(ErrorMessage = "Please select product name")]
        public int ProductId { get; set; }

        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Telephone Number Enter Only Number")]
        public string Telephone { get; set; }

        //[Required(ErrorMessage = "Enter Cellphone Number")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Cellphone Number Enter Only Number")]
        public string Cellphone { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Code { get; set; }
        //New Data - [Tab 2] Address
        public string Address1_Post { get; set; }
        public string Address2_Post { get; set; }
        public string Address3_Post { get; set; }
        public string Address4_Post { get; set; }
        public string Code_Post { get; set; }
        //end
        public string MemeberNumber { get; set; }

        public string MemberSociety { get; set; }
        public int fkiPlanID { get; set; }
        public bool Active { get; set; }

        [Required(ErrorMessage = "Please enter Inception date")]
        public DateTime InceptionDate { get; set; }
        public string Claimnumber { get; set; }
        public string PolicyStatus { get; set; }
        public Guid parlourid { get; set; }
        public Guid Administratorparlourid { get; set; }
        public string Agent { get; set; }
        public string AccountHolder { get; set; }
        public string Bank { get; set; }
        public string BranchCode { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public DateTime DebitDate { get; set; }

        [Required(ErrorMessage = "Select a Branch")]
        public string MemberBranch { get; set; }
        public DateTime? CoverDate { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }

        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }
        public string Citizenship { get; set; }

        //[Required(ErrorMessage = "Enter Passport Number")]
        public string Passport { get; set; }
        public string EasyPayNo { get; set; }
        public string RefNumber { get; set; }
        public Guid pkiAdditionalMemberInfo { get; set; }
        public string PlanName { get; set; }

        [Required(ErrorMessage = "Please enter Start date")]
        public DateTime StartDate { get; set; }
        public int CustomId1 { get; set; }
        public int CustomId2 { get; set; }
        public int CustomId3 { get; set; }
        public int CustomId4 { get; set; }
        public int FK_MemberId { get; set; }
        public int ClaimExistCount { get; set; }
        public string ApplicationName { get; set; }
        public Boolean AutogenerateEasyPay { get; set; }
        public bool AccountNumberVerified { get; set; }
        public string ServiceKey { get; set; }

    }
}
