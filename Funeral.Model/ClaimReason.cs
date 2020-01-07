using System;

namespace Funeral.Model
{
    public class ClaimReasonModel
    {
        public int ClaimReasonId { get; set; }
        public string ClaimReason { get; set; }
        public Guid ParlourId { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
