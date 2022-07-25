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
            [Description("New")]
            New = 0,
            [Description("Body Collection")]
            BodyCollection = 1,
            [Description("Mortuary")]
            Mortuary = 2,
            [Description("Physical Description")]
            PhysicalDesciption = 3,
            [Description("Funeral Arrangement")]
            FuneralArrangement = 4,
            [Description("Payment")]
            Payment = 5,
            [Description("Funeral Schedule")]
            FuneralSchedule = 6,
            [Description("Customer Feedback")]
            CustomerFeedback = 7,
            [Description("Completed")]
            Completed = 7
        }
    }
}
