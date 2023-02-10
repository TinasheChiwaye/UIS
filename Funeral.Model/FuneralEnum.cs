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
            //[Description("Physical Description")]
            //PhysicalDesciption = 3,
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

        public enum FuneralServiceType
        {
            [Description("Funeral Service")]
            FuneralService = 1,
            [Description("Tombstone Service")]
            TombstoneService = 2,
          
        }
    }
}
