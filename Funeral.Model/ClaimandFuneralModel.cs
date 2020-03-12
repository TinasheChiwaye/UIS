using System.Collections.Generic;

namespace Funeral.Model
{
    public class ClaimandFuneralModel
    {
        public ClaimsModel claimsModel { get; set; }
        public FuneralModel funeralModel { get; set; }
        public List<ClaimDocumentModel> ClaimDocumentList { get; set; }
        public List<MemberInvoiceModel> PaymentHistoryList { get; set; }
        public List<ClaimStatusHistory> claimStatusHistory { get; set; }
    }
}
