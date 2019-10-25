using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Funeral.Model
{
    public class GroupPayment
    {
        public GroupPayment()
        {
            GroupInvoiceID = 0;
            GroupID = 0;
            SocietyId = 0;
            DatePaid = DateTime.MinValue;
            AmountPaid = 0;
            RecievedBy = string.Empty;
            PaymentMethod = string.Empty;
            PaidBy = string.Empty;
            Notes = string.Empty;
            parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            LastModified = DateTime.MinValue;
            ModifiedUser = string.Empty;
        }
        public int GroupInvoiceID { get; set; }
        [Required(ErrorMessage = "Please select Group")]
        public int SocietyId { get; set; }
        public int GroupID { get; set; }
        [Required(ErrorMessage = "Please enter Payment Date")]
        public DateTime DatePaid { get; set; }
        [Required(ErrorMessage = "Please enter Amount")]
        public decimal AmountPaid { get; set; }
        public string RecievedBy { get; set; }
        [Required(ErrorMessage = "Please select Payment Method")]
        public string PaymentMethod { get; set; }
        public string PaidBy { get; set; }
        public string Notes { get; set; }
        public Guid parlourid { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public List<SocietyModel> SocietyDropdown { get; set; }

        public string SocietyName { get; set; }


    }
}
