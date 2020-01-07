using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class PolicyModel
    {
        public PolicyModel()
        {
            pkiPlanID = 0;
            PlanName = string.Empty;
            PlanSubscription = "R 0.00";
            totalPremium = "0.00";
        }
        public int pkiPlanID
        {
            get;
            set;
        }
        public string PlanName
        {
            get;
            set;
        }
        public string PlanSubscription
        {
            get;
            set;
        }
        public string totalPremium
        {
            get;
            set;
        }
        public string CoverAmount
        {
            get;
            set;
        }
        public int? WaitingPeriod { get; set; }
       
    }
}
