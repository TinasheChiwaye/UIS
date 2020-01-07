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
    public class ClaimStatusCount
    {
        public int StatusCount { get; set; }
        public string Status { get; set; }
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
