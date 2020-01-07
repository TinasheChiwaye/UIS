using System;

namespace Funeral.Model
{
    public class ClaimFollowUp
    {
        public long ClaimFollowUpId { get; set; }
        public string FollowUpType { get; set; }
        public int pkiClaimPictureID { get; set; }
        public int ClaimId { get; set; }
        public string Comment { get; set; }
        public Guid ParlourId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
