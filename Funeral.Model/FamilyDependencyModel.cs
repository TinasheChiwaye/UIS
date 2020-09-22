using Funeral.Web.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Funeral.Model
{
    public class FamilyDependencyModel : BaseViewModel
    {
        public FamilyDependencyModel()
        {
            FullName = string.Empty;
            Surname = string.Empty;
            IDNumber = string.Empty;
            pkiDependentID = 0;
            DependencyType = string.Empty;
            //parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            Age = 0;
            MemberId = 0;
            RelationshipType = string.Empty;
            Gender = string.Empty;
            DependentStatus = string.Empty;
        }

        //public string DTCode
        //{
        //    get;
        //    set;
        //}
        public string Cellphone { get; set; }

        public int Age
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter full name")]
        [RegularExpression(@"[a-zA-Z ]*$", ErrorMessage = "full name Enter Only characters")]
        public string FullName
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter Surname")]
        [RegularExpression(@"[a-zA-Z ]*$", ErrorMessage = "Surname Enter Only characters")]
        public string Surname
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter id number OR birthdate")]
        //[ValidateId()]
        public string IDNumber
        {
            get;
            set;
        }
        public int pkiDependentID
        {
            get;
            set;
        }
        public string DependencyType
        {
            get;
            set;
        }
        public Guid parlourid
        {
            get;
            set;
        }
        public int MemberId
        {
            get;
            set;
        }

        [ValidateDate("Please enter valid birth Date")]
        [Column("Date Of Birth")]
        public DateTime DateOfBirth
        {
            get;
            set;
        }

        public int Relationship
        {
            get;
            set;
        }
        public string RelationshipType
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter Inception date")]
        [ValidateDate("Please enter valid inception date")]
        public DateTime InceptionDate
        {
            get;
            set;
        }
        [ValidateDate("Please enter valid cover Date")]
        public DateTime CoverDate
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please enter premium amount")]
        public decimal Premium
        {
            get;
            set;
        }

        public string Gender { get; set; }
        public string DependentStatus { get; set; }


        [Required(ErrorMessage = "Please enter Start date")]
        [ValidateDate("Please enter valid start Date")]
        public DateTime? StartDate { get; set; }

        public decimal Cover { get; set; }
        public string MemeberNumber { get; set; }

        public int ClaimExistCount { get; set; }
    }



}
