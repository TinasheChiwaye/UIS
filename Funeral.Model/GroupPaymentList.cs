using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class GroupPaymentList
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int IDNumbersIssues { get; set; }
        public int MissingIDNumbers { get; set; }
        public int PrincipalMembers { get; set; }
        public int WaitingPeriod { get; set; }
        public int OnTrial { get; set; }
        public int Active { get; set; }
        public decimal Premium { get; set; }
        public decimal Balance { get; set; }
        public Guid parlourid { get; set; }
        public DateTime InceptionDate { get; set; }
        public decimal AmountAtRisk { get; set; }
        public decimal TotalRiskCovered { get; set; }
    }
}
