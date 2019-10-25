using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class TombStoneModel
    {
        public TombStoneModel()
        {
            InitVariables();
        }
        public void InitVariables()
        {
            this.pkiTombstoneID=0;
            this.IDNumber=string.Empty;
            this.Title=string.Empty;
            this.LastName=string.Empty;
            this.FirstName=string.Empty;
           // this.DateOfApplication=DateTime.MinValue;
            this.Address1=string.Empty;
            this.Address2=string.Empty;
            this.Address3=string.Empty;
            this.Address4=string.Empty;
            this.Code=string.Empty;
            this.TelNumber=string.Empty;
            this.CellNumber=string.Empty;
            this.DeceasedLastName=string.Empty;
            this.DeceasedFirstName=string.Empty;
            this.DeceasedIDNumber=string.Empty;
            this.Deceased=string.Empty;
           // this.DateOFMemorial=DateTime.MinValue;
            this.CemeterOrCrematorium=string.Empty;
            this.GraveNo=string.Empty;
            this.Erect=string.Empty;
            this.TombStoneMessage=string.Empty;
            this.Notes=string.Empty;
            this.parlourid = new Guid("00000000-0000-0000-0000-000000000000");
           // this.CreatedDate=DateTime.MinValue;
           // this.LastModified=DateTime.MinValue;
            this.ModifiedUser = string.Empty;
            this.ImageName = string.Empty;
            this.ImagePath = string.Empty;
            this.InvoiceNumber = string.Empty;
            this.InvoiceNumber2 = string.Empty;
            this.DisCount = 0;
            this.Tax = 0;
            this.ContactPerson = string.Empty;
            this.ContactPersonNumber = string.Empty;
        }
        public int pkiTombstoneID { get; set; }
        public string Title { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string IDNumber { get; set; }
        public DateTime? DateOfApplication { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Code { get; set; }
        public string TelNumber { get; set; }
        public string CellNumber { get; set; }
        public string DeceasedLastName { get; set; }
        public string DeceasedFirstName { get; set; }
        public string DeceasedIDNumber { get; set; }
        public string Deceased { get; set; }
        public DateTime? DateOFMemorial { get; set; }
        public string CemeterOrCrematorium { get; set; }
        public string GraveNo { get; set; }
        public string Erect { get; set; }
        public string TombStoneMessage { get; set; }
        public Byte[] TombStone { get; set; }
        public string Notes { get; set; }
        public Guid parlourid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceNumber2 { get; set; }
        public Decimal DisCount { get; set; }
        public Decimal Tax { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonNumber { get; set; }


    }

}
