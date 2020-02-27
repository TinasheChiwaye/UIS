using System;

namespace Funeral.Model
{
    public class BankDetails
    {
        public string AccountHolder { get; set; }
        public string Bank { get; set; }
        public string BranchCode { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public Guid parlourid { get; set; }
    }
}
