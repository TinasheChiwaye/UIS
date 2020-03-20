using System;
using System.Collections.Generic;

namespace Funeral.Model
{
    public class ClaimStatusHistory
    {
        public int ID { get; set; }
        public int ClaimID { get; set; }
        public Guid ParlourID { get; set; }
        public string ChangeReason { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ChangedBy { get; set; }
        public string ChangedByName { get; set; }
        public string CurrentStatus { get; set; }
        public string NewStatus { get; set; }
        public ClaimsModel ClaimsModel { get; set; }
    }
    public class ClaimDashboard
    {
        public List<ClaimStatusCount> claimStatuses { get; set; }
        public ClaimDashboardLabel claimDashboardLabel { get; set; }
        public List<ClaimCostGraph> claimCostGraphs { get; set; }
        public List<ClaimPolicyGraph> claimPolicyGraph { get; set; }

    }
    public class ClaimStatusCount
    {
        public int StatusCount { get; set; }
        public string Status { get; set; }
    }
    public class ClaimPolicyGraph
    {
        public int ClaimCount { get; set; }
        public string ApplicationName { get; set; }
        public decimal ClaimAmount { get; set; }
    }
    public class ClaimCostGraph
    {
        public int ClaimCount { get; set; }
        public int UnixCreatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal ClaimAmount { get; set; }
    }
    public class ClaimDashboardLabel
    {
        public int ClaimsCount { get; set; }
        public decimal PayoutAmount { get; set; }
        public decimal CoverAmount { get; set; }
    }
    public class ClaimStatusHistoryModal
    {
        public List<ClaimStatusHistory> claimStatusHistory { get; set; }
        public ClaimsModel claimsModel { get; set; }
        public FuneralModel funeralModel { get; set; }
        public List<MemberInvoiceModel> memberInvoices { get; set; }
        public List<ClaimDocumentModel> claimDocuments { get; set; }
    }
}
