using System;

namespace Funeral.Model.Search
{
    public class PaymentSearchNew : BaseSearch
    {
        public Guid StatusId { get; set; }
        public string BookName { get; set; }
    }
}
