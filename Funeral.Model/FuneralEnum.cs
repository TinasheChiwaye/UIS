using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
