using Funeral.Model;
using Funeral.Model.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Models.ViewModel
{
    public class ManageMembersVM : BaseViewModel
    {
        public IEnumerable<SelectListItem> countryList { get; set; }
        public IEnumerable<SelectListItem> PolicyList { get; set; }
        public IEnumerable<SelectListItem> BankList { get; set; }
        public IEnumerable<SelectListItem> AllAccountTypesList { get; set; }
        public IEnumerable<SelectListItem> AgentList { get; set; }
        public IEnumerable<SelectListItem> BankMemberNumber { get; set; }
        public IEnumerable<SelectListItem> BranchList { get; set; }
        public IEnumerable<SelectListItem> SocietyList { get; set; }
        public IEnumerable<SelectListItem> CustomPaymentMethod { get; set; }
        public IEnumerable<SelectListItem> CustomGrouping2 { get; set; }
        public IEnumerable<SelectListItem> CustomGrouping3 { get; set; }
        public IEnumerable<SelectListItem> ProductAddOnList { get; set; }
        public IEnumerable<SelectListItem> DependencyTypeList { get; set; }
        public IEnumerable<SelectListItem> ExtendedFamily { get; set; }
        public MembersModel Member { get; set; }
        public string UnderWriter { get; set; }
        public float TotalPremium { get; set; }
        public FamilyDependencyModel DependencyType { get; set; }
        public BaseSearch ProductSearch { get; set; }
        public BaseSearch InvoiceSearch { get; set; }
        public BaseSearch NoteSearch { get; set; }
        public BaseSearch DocumentSearch { get; set; }
        public BaseSearch DependancySearch { get; set; }
        public BaseSearch PolicySearch { get; set; }


        
    }


    public class ManageMemberInsertVM
    {
        public int pkiMemberID { get; set; }
        public DateTime CreateDate { get; set; }
        public string MemberType { get; set; }
        public string Title { get; set; }
        public string FullNames { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string IDNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string Cellphone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Code { get; set; }
        public string MemeberNumber { get; set; }
        public string MemberSociety { get; set; }
        public int fkiPlanID { get; set; }
        public bool Active { get; set; }
        public DateTime InceptionDate { get; set; }
        public string Claimnumber { get; set; }
        public string PolicyStatus { get; set; }
        public Guid parlourid { get; set; }
        public string Agent { get; set; }
        public string AccountHolder { get; set; }
        public string Bank { get; set; }
        public string BranchCode { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public DateTime DebitDate { get; set; }
        public string MemberBranch { get; set; }
        public DateTime CoverDate { get; set; }
        public string Email { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public string Citizenship { get; set; }
        public string Passport { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public int CustomId1 { get; set; }
        public int CustomId2 { get; set; }
        public int CustomId3 { get; set; }
        public int FK_MemberId { get; set; }
    }
}