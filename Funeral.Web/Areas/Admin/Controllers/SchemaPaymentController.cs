using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
        public PartialViewResult SchemaList()
        {
            BindCompanyList("Search");
            ViewBag.HasEditRight = HasEditRight;
            ViewBag.HasDeleteRight = HasDeleteRight;
            Model.Search.PaymentSearchNew search = new Model.Search.PaymentSearchNew();
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
        public ActionResult GroupSearchData(Model.Search.PaymentSearchNew search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, SocietyModel>(search, new List<SocietyModel>(), o => o.SocietyName.Contains(search.SarchText));

            try
            {
                var SocietyList = ToolsSetingBAL.GetAllSocietyes_PaymentList(Guid.Empty);
                SocietyList = search.StatusId != Guid.Empty ? SocietyList.Where(x => x.parlourid.Equals(search.StatusId)).ToList() : SocietyList;
                return Json(new SearchResult<Model.Search.BaseSearch, GroupPaymentList>(search, SocietyList, o => o.GroupName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, SocietyModel>.Error(searchResult, ex));
            }
        }
        #region Open Regenrated Billing PopUp
        [HttpGet]
        public ActionResult RegenerateBillingPartial(Guid ParlourId, string ReferenceNumber)
        {

            RegenerateBillingModal modal = new RegenerateBillingModal();
            modal.parlourId = ParlourId;
            modal.PremiumDueDate = DateTime.Now;
            modal.ReferenceNumber = ReferenceNumber;
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
        public ActionResult DownloadReportPartial(Guid ParlourId, string ReferenceNumber)
        {
            string ExportTypeExtensions = "xls";
            string ReportName = "ARL_Scheme_Billing Report";
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
            rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/" + ReportName;
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("DateFrom", DateTime.Now.ToString("yyyy/MM/dd")));
            reportParameters.Add(new ReportParameter("DateTo", DateTime.Now.ToString("yyyy/MM/dd")));
            reportParameters.Add(new ReportParameter("Parlourid", ParlourId.ToString()));
            reportParameters.Add(new ReportParameter("RefNo", ReferenceNumber));
            rpw.ServerReport.SetParameters(reportParameters);
            byte[] bytes = rpw.ServerReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            filename = string.Format("{0}.{1}", ReportName, ExportTypeExtensions);

            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = mimeType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
        [HttpGet]
        public ActionResult SendEmailReportPartial(Guid ParlourId, string ReferenceNumber)
        {
            ReportParameters modal = new ReportParameters();
            modal.parlourId = ParlourId;
            modal.fromDate = DateTime.Now;
            modal.toDate = DateTime.Now;
            modal.ReferenceNumber = ReferenceNumber;
            return PartialView("_ReportEmailSendModal", modal);
        }
        [HttpPost]
        public ActionResult SendEmailReport(ReportParameters modal)
        {
            if (ModelState.IsValid)
            {
                string ExportTypeExtensions = "xls";
                modal.ReportName = "ARL_Scheme_Billing Report";
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
                reportParameters.Add(new ReportParameter("DateFrom", modal.fromDate.ToString("yyyy/MM/dd")));
                reportParameters.Add(new ReportParameter("DateTo", modal.toDate.ToString("yyyy/MM/dd")));
                reportParameters.Add(new ReportParameter("Parlourid", modal.parlourId.ToString()));
                reportParameters.Add(new ReportParameter("RefNo", modal.ReferenceNumber.ToString()));
                rpw.ServerReport.SetParameters(reportParameters);
                byte[] bytes = rpw.ServerReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                filename = string.Format("{0}.{1}", modal.ReportName, ExportTypeExtensions);

                if (!string.IsNullOrEmpty(modal.Email))
                {
                    MemoryStream s = new MemoryStream(bytes);
                    s.Seek(0, SeekOrigin.Begin);
                    Attachment a = new Attachment(s, filename);
                    MailMessage message = new MailMessage(ConfigurationManager.AppSettings["ReportEmailSenderId"].ToString().Trim(), modal.Email.Trim(), modal.ReportName, "");
                    message.Attachments.Add(a);
                    SmtpClient client = new SmtpClient();
                    client.Send(message);
                    TempData["message"] = ShowMessage(MessageType.Success, "Email Sent Successfully");
                }
                else
                {
                    TempData["message"] = ShowMessage(MessageType.Success, "Please Enter Email");
                }
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult DownloadReconBillingReportPartial(Guid ParlourId, string ReferenceNumber)
        {
            string ExportTypeExtensions = "xls";
            string ReportName = "ARL_Scheme_Billing Recon Report";
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
            rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/" + ReportName;
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("DateFrom", DateTime.Now.ToString("yyyy/MM/dd")));
            reportParameters.Add(new ReportParameter("DateTo", DateTime.Now.ToString("yyyy/MM/dd")));
            reportParameters.Add(new ReportParameter("Parlourid", ParlourId.ToString()));
            reportParameters.Add(new ReportParameter("RefNo", ReferenceNumber.ToString()));
            rpw.ServerReport.SetParameters(reportParameters);
            byte[] bytes = rpw.ServerReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            filename = string.Format("{0}.{1}", ReportName, ExportTypeExtensions);

            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = mimeType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
    }
}