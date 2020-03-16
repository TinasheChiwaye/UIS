using System;

namespace Funeral.Model
{
    public class ExternalUserLink
    {
        public long ExternalId { get; set; }
        public Guid ExternalToken { get; set; }
        public int ClaimId { get; set; }
        public string Email { get; set; }
        public Guid parlourId { get; set; }
        public bool TokenUsed { get; set; }
        public string SentBy { get; set; }
        public DateTime SentDate { get; set; }
    }
}
