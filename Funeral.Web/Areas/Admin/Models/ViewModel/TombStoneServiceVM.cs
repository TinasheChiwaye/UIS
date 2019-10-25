using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Models.ViewModel
{
    public class TombStoneServiceVM : BaseViewModel
    {
        public List<SelectListItem> TaxSettings { get; set; }
        public ApplicationSettingsModel ApplicationSettings { get; set; }
        public List<SelectListItem> ServiceType { get; set; }
        public TombStoneModel objTombStoneModel { get; set; }
        public List<TombStoneServiceSelectModel> ServiceList { get; set; }
        //public QuotationMessageModel QuotationMessageModel { get; set; }
        public List<SelectListItem> GetAllPackage { get; set; }
        public ApplicationTnCModel ModelTermsAndCondition { get; set; }
        public List<TombStonesPaymentModel> TombStonesPaymentModelList { get; set; }
        public BankingDetailSending ModelBankDetails { get; set; }
    }

    public class TombStonePaymentVM
    {
        public List<TombStonesPaymentModel> TombStonePaymentList { get; set; }
        public List<TombStoneServiceSelectModel> TombStoneServiceList { get; set; }
        public TombStoneModel objTombStoneModel { get; set; }
        public string TombStoneNumber { get; set; }
        public string ReceivedBy { get; set; }
        public string TotalAmount { get; set; }
        public int MonthPaid { get; set; }
        public string Notes { get; set; }

    }

    public class TombStoneServicePackageVM
    {
        public string PackageName { get; set; }
        public string PackageID { get; set; }
        public int pkiTombStoneSelectionID { get; set; }
        public int fkiTombstoneID { get; set; }
        public int fkiServiceID { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int pkiServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDesc { get; set; }
        public decimal ServiceCost { get; set; }
        public int QTY { get; set; }
        public Guid parlourid { get; set; }
        public decimal ServiceRate { get; set; }
    }

    public class TombStoneService
    {
        public int pkiTombstoneID { get; set; }
        public string Tax { get; set; }
        public string DisCount { get; set; }
        public string InvoiceNumber { get; set; }

    }
}