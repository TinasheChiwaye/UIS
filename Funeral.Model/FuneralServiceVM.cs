using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Funeral.Model
{
    public class FuneralServiceVM
    {
        public string SubTotal { get; set; }
        public string Total { get; set; }
        public string Currency { get; set; }
        public List<SelectListItem> TaxSettings { get; set; }
        public ApplicationSettingsModel ApplicationSettings { get; set; }
        public List<SelectListItem> ServiceType { get; set; }
        public FuneralModel objFuneralModel { get; set; }
        public List<FuneralServiceSelectModel> ServiceList { get; set; }
        public List<SelectListItem> GetAllPackage { get; set; }
        public ApplicationTnCModel ModelTermsAndCondition { get; set; }
        public List<FuneralPaymentsModel> FuneralPaymentModelList { get; set; }
        public BankingDetailSending ModelBankDetails { get; set; }
    }
}
