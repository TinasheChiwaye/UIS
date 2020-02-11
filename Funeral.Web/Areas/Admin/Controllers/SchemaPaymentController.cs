using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class SchemaPaymentController : BaseAdminController
    {
        // GET: Admin/SchemaPayment
        private readonly ISiteSettings _siteConfig = new SiteSettings();
        public ActionResult Index()
        {
            return View();
        }
        public List<KeyValue> GetEntriesCount()
        {
            List<KeyValue> keyValues = new List<KeyValue>();
            keyValues.Add(new KeyValue { Key = "10", Value = "10" });
            keyValues.Add(new KeyValue { Key = "20", Value = "20" });
            keyValues.Add(new KeyValue { Key = "25", Value = "25" });
            keyValues.Add(new KeyValue { Key = "50", Value = "50" });
            keyValues.Add(new KeyValue { Key = "100", Value = "100" });
            keyValues.Add(new KeyValue { Key = "200", Value = "200" });
            keyValues.Add(new KeyValue { Key = "250", Value = "250" });
            keyValues.Add(new KeyValue { Key = "500", Value = "500" });
            return keyValues;
        }
        public void BindCompanyList()
        {
            List<SelectListItem> companyListItems = new List<SelectListItem>();
            List<ApplicationSettingsModel> model = new List<ApplicationSettingsModel>();

            if (this.IsAdministrator)
            {
                model = ToolsSetingBAL.GetAllApplicationList(ParlourId, 1, 0).ToList();

                if (model == null)
                {
                    model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
                }
            }
            else
            {
                model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
            }

            ViewBag.Companies = model;
        }

        public PartialViewResult SchemaList()
        {
            BindCompanyList();
            ViewBag.HasEditRight = HasEditRight;
            ViewBag.HasDeleteRight = HasDeleteRight;
            Model.Search.BaseSearch search = new Model.Search.BaseSearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;
            var searchResult = new SearchResult<Model.Search.BaseSearch, SocietyModel>(search, new List<SocietyModel>(), o => o.SocietyName.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Admin/Views/SchemaPayment/_SchemaList.cshtml", search);
        }
        public ActionResult GroupSearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, SocietyModel>(search, new List<SocietyModel>(), o => o.SocietyName.Contains(search.SarchText));

            try
            {
                var SocietyList = ToolsSetingBAL.GetAllSocietyes_PaymentList(ParlourId);
                return Json(new SearchResult<Model.Search.BaseSearch, GroupPaymentList>(search, SocietyList, o => o.GroupName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, SocietyModel>.Error(searchResult, ex));
            }
        }
        #region Open Regenrated Billing PopUp
        [HttpGet]
        public ActionResult RegenerateBillingPartial(Guid ParlourId)
        {
            RegenerateBillingModal modal = new RegenerateBillingModal();
            modal.parlourId = ParlourId;
            modal.PremiumDueDate = DateTime.Now;
            return PartialView("_RegenerateBillingModal", modal);
        }
        [HttpPost]
        public ActionResult SaveRegenerateBilling(RegenerateBillingModal modal)
        {
            if (ModelState.IsValid)
            {
                int value = MemberPaymentBAL.RecreateBillingMemberPayments(modal);
                if (value > 0)
                    TempData["message"] = ShowMessage(MessageType.Success, "Billing Regenerate Successfully");
                else
                    TempData["message"] = ShowMessage(MessageType.Danger, "Billing could not regenerate");
            }
            else
            {
                TempData["message"] = ShowMessage(MessageType.Danger, "Billing could not regenerate");
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        #endregion
        [HttpGet]
        public ActionResult DownloadReportPartial(Guid ParlourId)
        {
            ReportParameters modal = new ReportParameters();
            modal.parlourId = ParlourId;
            modal.toDate = DateTime.Now;
            modal.fromDate = DateTime.Now;
            return PartialView("_ReportParametersModal", modal);
        }
        [HttpPost]
        public ActionResult DownloadReport(ReportParameters modal)
        {
            if (ModelState.IsValid)
            {
                string ExportTypeExtensions = "xls";
                modal.ReportName = "UIS All Members Per Group Report";
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                string filename;

                ReportViewer rpw = new ReportViewer();
                rpw.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new MyReportServerCredentials();
                rpw.ServerReport.ReportServerCredentials = irsc;


                rpw.ProcessingMode = ProcessingMode.Remote;
                rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/" + modal.ReportName;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("DateFrom", modal.fromDate.ToString()));
                reportParameters.Add(new ReportParameter("DateTo", modal.toDate.ToString()));
                reportParameters.Add(new ReportParameter("Parlourid", modal.parlourId.ToString()));
                rpw.ServerReport.SetParameters(reportParameters);
                byte[] bytes = rpw.ServerReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                filename = string.Format("{0}.{1}", modal.ReportName, ExportTypeExtensions);

                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

            }
            TempData["message"] = ShowMessage(MessageType.Success, "Report Download");
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}