using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class ClaimsModel
    {
        public ClaimsModel()
        { 
            InitVariables();
        }
        public void InitVariables()
        {
            this.pkiClaimID = 0;
            this.fkiMemberID =0;
            this.MemberNumber=string.Empty;
            //this.ClaimDate=DateTime.MinValue;
            this.ClaimNotes = string.Empty;
            this.CourseOfDearth = string.Empty;
            this.HostingFuneral = false;
            this.ClaimantTitle = string.Empty;
            this.ClaimantFullname = string.Empty;
            this.ClaimantSurname = string.Empty;
            this.ClaimantIDNumber=string.Empty;
            //this.ClaimantDateOfBirth= DateTime.MinValue;
            this.ClaimantGender = string.Empty;
            this.ClaimantAddressLine1= string.Empty;
            this.ClaimantAddressLine2 = string.Empty;
            this.ClaimantAddressLine3=string.Empty;
            this.ClaimantAddressLine4=string.Empty;
            this.ClaimantCode=string.Empty;
            this.ClaimantContactNumber=string.Empty;
            this.BeneficiaryBank=string.Empty;
            this.BeneficiaryAccountHolder = string.Empty;
            this.BeneficiaryAccountNumber = string.Empty;
            this.BeneficiaryBankBranch = string .Empty;
            this.BeneficiaryBranchCode = string.Empty;
            this.BeneficiaryAccountType=string.Empty;
            this.LoggedBy = string.Empty;
            this.Cover=0;
            this.BodyCollectedFrom = string.Empty;
            this.ClaimingFor = string.Empty;
           // this.CreatedDate=DateTime.MinValue;
            this.IsDeleted =false;
             this.DeletedBy = string.Empty;
             this.Status = string.Empty;
            //  this.DeletedDate = DateTime.MinValue;
             this.Payout = false;
             this.PayoutValue = 0;
        }
        public int pkiClaimID { get; set; }
        public int fkiMemberID { get; set; }
        public string MemberNumber { get; set; }
        public DateTime ClaimDate { get; set; }
        public string ClaimNotes { get; set; }
        public string CourseOfDearth { get; set; }
        public Boolean HostingFuneral { get; set; }
        public string ClaimantTitle { get; set; }
        public string ClaimantFullname { get; set; }
        public string ClaimantSurname { get; set; }
        public string  ClaimantIDNumber { get; set; }
        public DateTime ClaimantDateOfBirth { get; set; }
        public string ClaimantGender { get; set; }
        public string ClaimantAddressLine1 { get; set; }
        public string ClaimantAddressLine2 { get; set; }
        public string ClaimantAddressLine3 { get; set; }
        public string ClaimantAddressLine4 { get; set; }
        public string ClaimantCode { get; set; }
        public string ClaimantContactNumber { get; set; }
        public string BeneficiaryBank { get; set; }
        public string BeneficiaryAccountHolder { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryBankBranch { get; set; }
        public string  BeneficiaryBranchCode { get; set; }
        public string BeneficiaryAccountType { get; set; }
        public string LoggedBy { get; set; }
        public Decimal Cover { get; set; }
        public string BodyCollectedFrom { get; set; }
        public string ClaimingFor { get; set; }
        public Guid parlourid { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public string Status { get; set; }
        public bool Payout { get; set; }
        public decimal PayoutValue { get; set; }             
    }
}
