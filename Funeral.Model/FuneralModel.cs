using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;



namespace Funeral.Model
{
    public class FuneralModel
    {
        public FuneralModel()
        {
            InitVariables();
        }
        public void InitVariables()
        {
            this.pkiFuneralID = 0;
            this.Title = string.Empty;
            this.FullNames = string.Empty;
            this.Surname = string.Empty;
            this.Gender = string.Empty;
            this.IDNumber = string.Empty;
             this.DateOfBirth=DateTime.Now;
            this.DateOfDeath = DateTime.Now;
            this.DateOfFuneral = DateTime.Now;
            this.TimeOfFuneral = DateTime.Now;
            this.FuneralCemetery = string.Empty;
            this.Address1 = string.Empty;
            this.Address2 = string.Empty;
            this.Address3 = string.Empty;
            this.Address4 = string.Empty;
            this.Code = string.Empty;
            this.MemeberNumber = string.Empty;
            this.ContactPerson = string.Empty;
            this.ContactPersonNumber = string.Empty;
            this.BodyCollectedFrom = string.Empty;
            this.CourseOfDearth = string.Empty;
            this.Claimnumber = string.Empty;
            this.BI1663 = string.Empty;
            this.DriverAndCars = string.Empty;
            this.GraveNo = string.Empty;
            this.parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            this.LastModified = DateTime.Now;
            this.ModifiedUser = string.Empty;
            this.Notes = string.Empty;
            this.CreatedDate = DateTime.Now;
            this.InvoiceNumber = string.Empty;
            this.InvoiceNumber2 = string.Empty;
            this.Discount = 0;
            this.Tax = 0;
            this.FkiClaimID = 0;

        }
        public int pkiFuneralID { get; set; }
        public int FkiClaimID { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "The full name is required")]
        public string FullNames { get; set; }
        [Required(ErrorMessage = "The Surname is required")]
        public string Surname { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage ="The Id Number is required")]
        public string IDNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public DateTime DateOfFuneral { get; set; }

        [DataType(DataType.Time)]
        public DateTime TimeOfFuneral { get; set; }
        public string FuneralCemetery { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Code { get; set; }
        public string MemeberNumber { get; set; }
        public string ContactPerson { get; set; }
        [StringLength(10)]
        public string ContactPersonNumber { get; set; }
        public string BodyCollectedFrom { get; set; }
        public string CourseOfDearth { get; set; }
        public string Claimnumber { get; set; }
        public string BI1663 { get; set; }
        public string BurialOrderNo { get; set; }
        public string DriverAndCars { get; set; }
        public string GraveNo { get; set; }
        public Guid parlourid { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceNumber2 { get; set; }
        public string Notes { get; set; }
        public Decimal Discount { get; set; }
        public Decimal Tax { get; set; }

        public string MemberType { get; set; }

        public string OfficialStatus { get; set; }
        public string PlaceOfCollection { get; set; }
        public string PolicyNumber { get; set; }
        public string GeneralCondition { get; set; }
        public string Sex { get; set; }
        public string AgeGroup { get; set; }
        public string HeadHair { get; set; }
        public string SkinMarks { get; set; }
        public string DentalCondition { get; set; }
        public string PersonalItem { get; set; }
        public string IdentityDocument { get; set; }
        public string CauseOfDeath { get; set; }
        public string Status { get; set; }

        public string CollectionType { get; set; }
        public string Village { get; set; }
        public string District { get; set; }
        public string Branch { get; set; }
        public string FridgeNumber { get; set; }
        public string ShelfNumber { get; set; }
        public string CarReg { get; set; }
        public string Embalming { get; set; }
        public string CoffineSize { get; set; }
        public string BodyAndCar { get; set; }
        public IEnumerable<SelectListItem> CustomGrouping5 { get; set; }

    }
}
