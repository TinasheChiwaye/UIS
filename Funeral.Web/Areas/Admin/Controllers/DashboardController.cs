
using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class DashboardController : BaseAdminController
    {
        // GET: Admin/Dashboard
        private readonly ISiteSettings _siteConfig = new SiteSettings();
        public ActionResult Index()
        {
            Dashboard ds = new Dashboard();
            ds = CommonBAL.GetDashboardLableDetails(this.ParlourId, this.IsAdministrator, this.IsSuperUser, this.UserName, this.Currency);
            ds.dashboardChart = CommonBAL.GetDashboardChart(ParlourId, IsAdministrator, UserName, IsSuperUser);
            ds.dashboardChart.Currency = this.Currency;
            if (IsAdministrator == true || IsSuperUser == true)
            {
                ds.IsDisplayDashboarTable = true;
            }
            return View(ds);
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
        public PartialViewResult List()
        {
            Model.Search.BaseSearch search = new Model.Search.BaseSearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;

            var searchResult = new SearchResult<Model.Search.BaseSearch, PolicyPremiumDashboardModel>(search, new List<PolicyPremiumDashboardModel>(), o => o.RecievedBy.Contains(search.SarchText) || o.PaymentBranch.Contains(search.SarchText) || o.AmountPaid.Equals(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Admin/Views/Dashboard/_PolicyPremiumList.cshtml", search);
        }

        public ActionResult SearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, PolicyPremiumDashboardModel>(search, new List<PolicyPremiumDashboardModel>(), o => o.RecievedBy.Contains(search.SarchText) || o.PaymentBranch.Contains(search.SarchText) || o.AmountPaid.Equals(search.SarchText));
            try
            {
                var funeralList = MembersBAL.SelectPolicyPremiumByParlourId(ParlourId, search.PageSize, search.PageNum, "", search.SortBy, search.SortOrder, IsAdministrator, IsSuperUser, UserName);
                return Json(new SearchResult<Model.Search.BaseSearch, PolicyPremiumDashboardModel>(search, funeralList, o => o.RecievedBy.Contains(search.SarchText) || o.PaymentBranch.Contains(search.SarchText) || o.AmountPaid.Equals(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, PolicyPremiumDashboardModel>.Error(searchResult, ex));
            }
        }
        public JsonResult AddsmsTopUp(string Textvalue)
        {
            string msg = "";
            try
            {

                ConsumableOrders model1 = new ConsumableOrders();
                model1.DateCreated = DateTime.Now;
                model1.SMSQyt = Textvalue;
                model1.Parlourid = this.ParlourId;
                model1.LastModified = DateTime.Now;
                model1.ModifiedUser = this.UserName;
                CommonBAL.SMSTopup_save(model1);
                msg = "SMS Qty. Inserted successfully";
            }
            catch (Exception ee)
            {
                msg = ee.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendMessagesoutstaning()
        {
            string msg = "";
            try
            {
                CommonBAL.SendOutstandingMessagesToAll(this.ParlourId);
                msg = "Outstanding SMS Sent successfully";
            }
            catch (Exception ee)
            {
                msg = ee.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GenerateCashupReport()
        //{
        //    byte[] fileBytes = GenerateCashup_Report();
        //    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.Filename);

        //}
        public void GenerateCashup_Report()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filename;
            string result;

            try
            {
                ReportViewer rpw = new ReportViewer();
                rpw.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new MyReportServerCredentials();
                rpw.ServerReport.ReportServerCredentials = irsc;

                rpw.ProcessingMode = ProcessingMode.Remote;
                rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/UIS_Daily Cash Up Summary Dashboard";
                ReportParameterCollection reportParameters = new ReportParameterCollection();

                DateTime d1 = DateTime.Now;

                var IsSuperUserStatus = IsSuperUser == true ? "1" : "0";

                reportParameters.Add(new ReportParameter("DateFrom", d1.AddDays(-1).ToShortDateString()));
                reportParameters.Add(new ReportParameter("DateTo", d1.ToShortDateString()));
                reportParameters.Add(new ReportParameter("Parlourid", ParlourId.ToString()));
                reportParameters.Add(new ReportParameter("UserID", this.UserID.ToString()));
                reportParameters.Add(new ReportParameter("IsSuperUser", IsSuperUserStatus));


                rpw.ServerReport.SetParameters(reportParameters);
                string ExportTypeExtensions = "pdf";
                byte[] bytes = rpw.ServerReport.Render(ExportTypeExtensions, null, out mimeType, out encoding, out ExportTypeExtensions, out streamids, out warnings);
                filename = string.Format("{0}.{1}", "Daily Cash Up Summary", ExportTypeExtensions);

                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
                result = "true";

            }
            catch (Exception ex)
            {
                result = ex.Message;
                throw ex;
            }
        }

    }
}