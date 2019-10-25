using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Funeral.Model
{
    public class PlanModel
    {
        public PlanModel()
        {
            pkiPlanID = 0;
            PlanName = string.Empty;
            PlanDesc = string.Empty;
            PlanSubscription = 0;
            Spouse = 0;
            Children = 0;
            Adults = 0;
            Cover = 0;
            WaitingPeriod = 0;
            UnderwriterSplit = 0;
            ManagerSplit = 0;
            AgentSplit = 0;
            PolicyLaps = 0;
            //SpouseCover = 0;
            //ChildCover = 0;
            //AdultCover = 0;
            parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            PlanUnderwriter = string.Empty;
            JoiningFee = 0;
            LastModified = System.DateTime.MinValue;
            ModifiedUser = string.Empty;
            HeadManagerSplit = 0;
            Manager2Split = 0;
            OfficeSplit = 0;
            AdminSplit = 0;
            MainMember = 0;
            CompanySplit = 0;
            UnderwriterId = 0;
            //Cover0to5year = 0;
            //Cover6to13year = 0;
            //Cover14to21year = 0;
            //CashPayout = 0;
        }
        public int pkiPlanID { get; set; }
        [Required]
        public string PlanName { get; set; }
        public string PlanDesc { get; set; }
        [Required]
        public decimal PlanSubscription { get; set; }
        public int Spouse { get; set; }
        public int Children { get; set; }
        public int Adults { get; set; }
        [Required]
        public decimal Cover { get; set; }
        public int WaitingPeriod { get; set; }
        public decimal UnderwriterSplit { get; set; }
        public decimal ManagerSplit { get; set; }
        public decimal AgentSplit { get; set; }
        public int PolicyLaps { get; set; }
        public decimal SpouseCover { get; set; }
        public decimal ChildCover { get; set; }
        public decimal AdultCover { get; set; }
        public Guid parlourid { get; set; }
        public string PlanUnderwriter { get; set; }
        public decimal JoiningFee { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        [Required]
        public decimal HeadManagerSplit { get; set; }
        public decimal Manager2Split { get; set; }
        public decimal OfficeSplit { get; set; }
        [Required]
        public decimal AdminSplit { get; set; }
        [Required]
        public decimal CompanySplit { get; set; }
        public int MainMember { get; set; }
        public decimal Cover0to5year { get; set; }
        public decimal Cover6to13year { get; set; }
        public decimal Cover14to21year { get; set; }
        public decimal Cover22to40year { get; set; }
        public decimal Cover41to59year { get; set; }
        public decimal Cover60to65year { get; set; }
        public decimal Cover66to75year { get; set; }
        public int? UnderwriterId { get; set; }
        public decimal CashPayout { get; set; }

        public List<UserType> GetUserTypes { get; set; }
        public List<PlanCreator> planCreators { get; set; }
        public List<UnderwriterModel> UnderwriterList { get; set; }
    }
    public class UserType
    {
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
    }
    public class PlanCreator
    {
        public long PlanCreatorId { get; set; }
        public int PlanID { get; set; }
        [Required(ErrorMessage = "Please select user type")]
        public int UserTypeId { get; set; }
        [Required(ErrorMessage = "Please select From age")]
        public int FromAge { get; set; }
        [Required(ErrorMessage = "Please select to age")]
        public int ToAge { get; set; }

        [Required(ErrorMessage = "Please enter premium")]
        public decimal Premium { get; set; }
        [Required(ErrorMessage = "Please enter cover")]
        public decimal Cover { get; set; }

        [Required(ErrorMessage = "Please enter Underwriter premium")]
        public decimal UnderwriterPremium { get; set; }
        [Required(ErrorMessage = "Please enter Underwriter cover")]
        public decimal UnderwriterCover { get; set; }

        public bool TableRawStatus { get; set; }
        public bool IsActive { get; set; }
        public DateTime DeletedDate { get; set; }
        public string DeletedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}

