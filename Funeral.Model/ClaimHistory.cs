using System;

namespace Funeral.Model
{
    public class ClaimHistory
    {
        public int ClaimHistoryId { get; set; }
        public int FkiClaimId { get; set; }
        public string Note { get; set; }
        public Guid ParlourId { get; set; }
        public string IPAddress { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
