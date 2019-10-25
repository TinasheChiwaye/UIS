using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class UnderwriterModel
    {
        public int PkiUnderwriterId { get; set; }
        public string UnderwriterName { get; set; }
        public string PlanName { get; set; }
        public string Premium { get; set; }
        public string CoverAmount { get; set; }
        public int Spouse { get; set; }
        public int Children { get; set; }
        public int Adults { get; set; }
        public decimal Cover { get; set; }
        public decimal UnderwriterSplit { get; set; }
        public decimal ManagerSplit { get; set; }
        public decimal SpouseCover { get; set; }
        public decimal ChildCover { get; set; }
        public decimal AdultCover { get; set; }
        public Guid Parlourid { get; set; }
        public string PlanUnderwriter { get; set; }
        public DateTime? LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime? CreateddDate { get; set; }
        public string CreatedUser { get; set; }
        public decimal Cover0to5year { get; set; }
        public decimal Cover6to13year { get; set; }
        public decimal Cover14to21year { get; set; }
        public decimal Cover22to40year { get; set; }
        public decimal Cover41to59year { get; set; }
        public decimal Cover60to65year { get; set; }
        public decimal Cover66to75year { get; set; }
    }
}
