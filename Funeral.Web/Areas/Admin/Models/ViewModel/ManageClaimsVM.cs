using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Models.ViewModel
{
    public class ManageClaimsVM : Model.Search.BaseSearch
    {
        public IEnumerable<SelectListItem> StatusList { get; set; }
        public IEnumerable<SelectListItem> BankList { get; set; }
        public IEnumerable<SelectListItem> AllAccountTypesList { get; set; }
        public ClaimsModel Claim { get; set; }
        public Guid CompanyId { get; set; }

        public string StatusId { get; set; }

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
        public string ClaimantIDNumber { get; set; }
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
        public string BeneficiaryBranchCode { get; set; }
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
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string keyword { get; set; }
        
    }
}