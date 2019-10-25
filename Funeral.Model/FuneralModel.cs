using System;
using System.ComponentModel.DataAnnotations;

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
            // this.DateOfBirth=DateTime.MinValue;
            //this.DateOfDeath = DateTime.MinValue;
            //this.DateOfFuneral = DateTime.MinValue;
            //this.TimeOfFuneral = DateTime.MinValue;
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
            this.LastModified = DateTime.MinValue;
            this.ModifiedUser = string.Empty;

            this.Notes = string.Empty;
            this.CreatedDate = DateTime.MinValue;
            this.InvoiceNumber = string.Empty;
            this.InvoiceNumber2 = string.Empty;
            this.Discount = 0;
            this.Tax = 0;

        }
        public int pkiFuneralID { get; set; }
        public string Title { get; set; }
        public string FullNames { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
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


    }
}
