using System;

namespace Funeral.Model
{
    public class PlanStagingList
    {
        public int pkiPlanID { get; set; }
        public string PlanName { get; set; }
        public string PlanDesc { get; set; }
        public decimal PlanSubscription { get; set; }
        public decimal Cover { get; set; }
        public int WaitingPeriod { get; set; }
        public int PolicyLaps { get; set; }
        public Guid parlourid { get; set; }
        public string PlanUnderwriter { get; set; }
        public decimal JoiningFee { get; set; }
        public int UnderwriterId { get; set; }
        public string AgeBand { get; set; }
        public string RelationType { get; set; }
        public int Age { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public decimal CoverAmount { get; set; }
        public decimal PremiumAmount { get; set; }
        public decimal UnderwriterCover { get; set; }
        public decimal UnderwriterPremium { get; set; }
        public decimal ReinsurancePremium { get; set; }
        public decimal OfficePremium { get; set; }
        public Guid ImportedId { get; set; }
        public string CreatedBy { get; set; }

        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public int RelationTypeId { get; set; }
    }
}
