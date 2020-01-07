using System;
using System.Collections.Generic;

namespace Funeral.Model
{
    public class ClaimDocument
    {
        public int PkiClaimId { get; set; }
        public int fkiMemberId { get; set; }
        public int DocumentId { get; set; }
        public Guid ParlourId { get; set; }
        public List<FuneralDocument> DocumentList { get; set; }

        public string Status { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
    }
}
