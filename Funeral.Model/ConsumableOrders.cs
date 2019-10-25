using System;

namespace Funeral.Model
{
    public class ConsumableOrders
    {
        public Guid OrderID { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserID { get; set; }
        public string SMSQyt { get; set; }
        public string LabelQty { get; set; }
        public string OrderDesc { get; set; }
        public string OrderStatus { get; set; }
        public Guid Parlourid { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
    }
}
