using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Models.ViewModel
{
    public class QuotationServiceVM : BaseViewModel
    {
        public List<SelectListItem> TaxSettings { get; set; }
        public ApplicationSettingsModel ApplicationSettings { get; set; }
        public List<SelectListItem> ServiceType { get; set; }
        public QuotationModel objQuotation { get; set; }
        public List<QuotationServicesModel> ServiceList { get; set; }
        public QuotationMessageModel QuotationMessageModel { get; set; }
        public List<SelectListItem> GetAllPackage { get; set; }
        public ApplicationTnCModel ModelTermsAndCondition { get; set; }
        public BankingDetailSending ModelBankDetails { get; set; }
    }

    public class QuotationServicePackageVM
    {
        public string PackageName { get; set; }
        public int pkiQuotationSelectionID { get; set; }
        public int QuotationID { get; set; }
        public int fkiServiceID { get; set; }
        public int Quantity { get; set; }
        //public DateTime lastModified { get; set; }
        //public string modifiedUser { get; set; }
        public decimal Amount { get; set; }
        public int pkiServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDesc { get; set; }
        public decimal ServiceCost { get; set; }
        public int QTY { get; set; }
        public Guid parlourid { get; set; }
        //public DateTime LastModified { get; set; }
        //public string ModifiedUser { get; set; }
        public decimal ServiceRate { get; set; }
    }

    public class QuatationServiceVM
    {
        public string Notes { get; set; }
        public string QuotationNumber { get; set; }
        public int QuotationID { get; set; }
        public int DiscountID { get; set; }
        public string Tax { get; set; }
        public string disc { get; set; }

    }
}