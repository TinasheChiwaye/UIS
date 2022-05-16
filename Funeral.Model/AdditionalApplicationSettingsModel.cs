using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class AdditionalApplicationSettingsModel
    {
        public AdditionalApplicationSettingsModel()
        {
            pkiParlourid = new Guid("00000000-0000-0000-0000-000000000000");
        }
        public Guid pkiParlourid { get; set; }
        public string spUPuser { get; set; }
        public string spUPpass { get; set; }
        public string spUPpinpad { get; set; }
        public string spValuser { get; set; }
        public string spValpass { get; set; }
        public string spValpinpad { get; set; }
        public string spCCuser { get; set; }
        public string spCCpass { get; set; }
        public string spCCpinpad { get; set; }
        public string GenerateMember { get; set; }
        public Boolean GenerateEasyPay { get; set; }
        public string Currency { get; set; }


        public string spNetcashAccountNumber { get; set; }
        public string spAccountServiceKey { get; set; }
        public string spDebitOrderServicekey { get; set; }
        public string spPaynowServiveKey { get; set; }
        public string spSoftwareVendorKey { get; set; }
        public string spAccountServiceKeyMessage { get; set; }
        public string spDebitOrderServikeyMessage { get; set; }
        public string spPaynowServiveKeyMessage { get; set; }
        public string spNetcashAccountNumberMessage { get; set; }
        public string spSoftwareVendorKeyMessage { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }

    }
}
