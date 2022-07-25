using System.ComponentModel;

namespace Funeral.Model
{
    public class FuneralEnum
    {
        public enum StatusAssociatedTable
        {
            Claims,
            Members,
            Quotations,
            Invoices,
            Funerals,
            Expenses
        }
        public enum FollowUpTypesEnum
        {
            Document,
            Normal,
            ClaimStatus,
            AddUpdate,
        }
        public enum ClaimDocumentStatusEnum
        {
            Approved,
            Pending,
            Rejected
        }

        public enum FuneralStatusEnum
        {
            [Description("Body Collection")]
            BodyCollection = 1,
            [Description("Mortuary")]
            Mortuary = 2,
            [Description("Funeral Arrangement")]
            FuneralArrangement = 3,
            [Description("Payment")]
            Payment = 4,
            [Description("Funeral Schedule")]
            FuneralSchedule = 5,
            [Description("Customer Feedback")]
            CustomerFeedback = 6,
            [Description("Completed")]
            Completed = 7
        }
    }
}
