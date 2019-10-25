using Funeral.Model;

namespace Funeral.Web.Areas.Admin.Models.ViewModel
{
    public class FuneralPrintVM
    {
        public ApplicationSettingsModel ApplicationSettings { get; set; }
        public FuneralModel FuneralModel { get; set; }
        public BankingDetailSending ModelBankDetails { get; set; }
        public ApplicationTnCModel ModelTermsAndCondition { get; set; }
    }
}