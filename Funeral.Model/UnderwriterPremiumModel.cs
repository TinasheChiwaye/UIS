using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class UnderwriterPremiumModel
    {
        public UnderwriterPremiumModel()
        {
            pkiUnderwriterID = 0;
            PlanUnderwriter = string.Empty;
            PlanId = 0;
            PlanName = string.Empty;
            DependencyType = string.Empty;
            PremiumAmount = 0;
            CoverAmount = 0;
            CoverAgeFrom = 0;
            CoverAgeTo = 0;
            ApplysToDependents = true;
            Parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            LastModified = DateTime.Now;
            ModifiedUser = string.Empty;
            UnderwriterPremium = 0;
            UnderwriterCover = 0;
        }

        public int PkiUnderwriterPremiumId { get; set; }
        public int FkiUnderwriterId { get; set; }

        public int pkiUnderwriterID { get; set; }
        public int PlanId { get; set; }
        public string PlanUnderwriter { get; set; }
        public string PlanName { get; set; }
        public string DependencyType { get; set; }
        public decimal PremiumAmount { get; set; }
        public decimal CoverAmount { get; set; }
        public decimal UnderwriterPremium { get; set; }
        public decimal UnderwriterCover { get; set; }
        public int CoverAgeFrom { get; set; }
        public int CoverAgeTo { get; set; }
        public bool ApplysToDependents { get; set; }
        public Guid Parlourid { get; set; }
        public DateTime? LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public string RolePlayerType { get; set; }
        public string CreatedUser { get; set; }
        public string UnderwriterName { get; set; }
        public string CoverAgeFromType { get; set; }
        public string CoverAgeToType { get; set; }
        public DateTime? CreatedDate { get; set; }
        //public UnderwriterPremiumEnums.UnderwriterPlans UnderwriterPlanSelection { get; set; }
    }
}
