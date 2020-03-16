using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Areas.Admin.Models.ViewModel;
using Funeral.Web.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class PaymentsController : BaseAdminController
    {
        private readonly ISiteSettings _siteConfig = new SiteSettings();
        public PaymentsController() : base(34)
        {
            this.dbPageId = 34;
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
        [PageRightsAttribute(CurrentPageId = 34, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            //ViewBag.HasAccess = HasAccess;
            if (ViewBag.HasAccess == true)
            {
                var info = CultureInfo.InvariantCulture.Clone() as CultureInfo;
                info.NumberFormat.NumberDecimalSeparator = ".";

                CultureInfo cInfo = new CultureInfo(info.ToString());
                cInfo.NumberFormat.NumberDecimalSeparator = ".";

                Thread.CurrentThread.CurrentCulture = cInfo;

            }
            return View("Index");

        }

        [PageRightsAttribute(CurrentPageId = 34)]
        public PartialViewResult List()
        {
            ViewBag.SocietyLists = CommonBAL.GetSocietyByParlourId(CurrentParlourId);
            ViewBag.HasEditRight = HasEditRight;
            ViewBag.HasDeleteRight = HasDeleteRight;
            Model.Search.PaymentSearchNew search = new Model.Search.PaymentSearchNew();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;
            var searchResult = new SearchResult<Model.Search.PaymentSearchNew, MembersPaymentModel>(search, new List<MembersPaymentModel>(), o => o.FullNames.Contains(search.SarchText));
            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;
            var info = CultureInfo.InvariantCulture.Clone() as CultureInfo;
            info.NumberFormat.NumberDecimalSeparator = ".";

            ViewBag.CompanyDropDisplay = false;
            if (IsAdministrator)
            {
                ViewBag.CompanyDropDisplay = true;
                BindCompanyList("Search");
            }

            CultureInfo cInfo = new CultureInfo(info.ToString());
            cInfo.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = cInfo;

            return PartialView("~/Areas/Admin/Views/Payments/_PaymentList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.PaymentSearchNew search)
        {
            var searchResult = new SearchResult<Model.Search.PaymentSearchNew, MembersPaymentModel>(search, new List<MembersPaymentModel>(), o => o.FullNames.Contains(search.SarchText) || o.IDNumber.Contains(search.SarchText) || o.MemeberNumber.Contains(search.SarchText) || o.PolicyStatus.Contains(search.SarchText));
            try
            {
                var otherPayment = MemberPaymentBAL.GetAllPayentMembers(search.StatusId, "", "", search.PageSize, search.PageNum, search.SortBy, search.SortOrder, "", search.BookName);
                return Json(new SearchResult<Model.Search.PaymentSearchNew, MembersPaymentModel>(search, otherPayment.MemberList, o => o.FullNames.Contains(search.SarchText) || o.IDNumber.Contains(search.SarchText) || o.MemeberNumber.Contains(search.SarchText) || o.PolicyStatus.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.PaymentSearchNew, MembersPaymentModel>.Error(searchResult, ex));
            }
        }
        [HttpGet]
        [PageRightsAttribute(CurrentPageId = 34)]
        public ActionResult ManageMembersPayment(int id, Guid ParlourID)
        {
            MembersPaymentDetailsModel model = MemberPaymentBAL.ReturnMemberPlanDetailsWithBalance(Convert.ToString(id), ParlourID);
            if (model != null)
            {
                model.Currency = Currency;
                if (model.PlanSubscription != null)
                {
                    ViewBag.totalPremium = model.PlanSubscription;
                }
                if (model.PaymentDate != null)
                {
                    ViewBag.Date = model.PaymentDate.ToString("dd-MMM-yyyy");
                }
                if (model.NextPaymentDate != null)
                {
                    ViewBag.PaymentDate = model.NextPaymentDate.ToString("MMM-yyyy");
                }
                if (model.PolicyStatus != null || model.PolicyStatus != "")
                {
                    ViewBag.PolicyStatus = model.PolicyStatus;
                }

                var policyBalance = model.Currency + " " + Convert.ToDouble(model.Balance);
                ViewBag.policyBalance = policyBalance;
                var latePanelty = model.Currency + " " + Convert.ToDouble(model.LatePaymentPenalty);
                ViewBag.LatePanelty = latePanelty;
                var totalPremium = model.Currency + " " + Convert.ToDouble(MembersBAL.SumOfPremium(id, ParlourID));
                ViewBag.TotalPremium = totalPremium;
                ViewBag.MemberInvoiceList = MembersBAL.GetInvoicesByMemberID(ParlourID, id);
                ViewBag.MemberID = model.MemeberNumber;
                ViewBag.ParlourID = ParlourID;
            }
            var info = CultureInfo.InvariantCulture.Clone() as CultureInfo;
            info.NumberFormat.NumberDecimalSeparator = ".";

            CultureInfo cInfo = new CultureInfo(info.ToString());
            cInfo.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = cInfo;
            return View(model);
        }
        [HttpPost]
        [PageRightsAttribute(CurrentPageId = 34, Right = new isPageRight[] { isPageRight.HasAdd })]
        public JsonResult AddPayments(MembersPaymentDetailsModel data)
        {
            //add Joining fee boolean in method
            MembersModel objmember = MembersBAL.GetMemberByID(data.pkiMemberID, ParlourId);
            if (objmember.MemberBranch != "")
            {
                data.Branch = objmember.MemberBranch;
            }
            data.ParlourId = ParlourId;
            data.NextPaymentDate = Convert.ToDateTime(data.PaymentDate).AddMonths(Convert.ToInt32(data.MonthOwing));
            var paymentId = MemberPaymentBAL.AddPayments(data, true);
            return Json("Payment added successfully.", JsonRequestBehavior.AllowGet);
        }
        public ActionResult PrintPaymentReceipt(int id, int Type, string PolicyNumber, string DatePaid, string AmountPaid, string PaidBy, string ReceivedBy, string MonthPaid, int memberId, Guid parlourId)
        {

            PrintViewModel printObj = new PrintViewModel();
            MembersModel memberModel = MembersBAL.GetMemberByID(memberId, parlourId);
            if (memberModel == null)
            {
                printObj.SurName = string.Empty;
                printObj.FullName = string.Empty;
            }
            else
            {
                printObj.SurName = memberModel.Surname;
                printObj.FullName = memberModel.FullNames;
            }

            if (Type == 1)
            {
                //NewMemberInvoiceModel getmodel = MembersBAL.GetInvoiceByid(InvoiceId); 

                ApplicationSettingsModel model = new ApplicationSettingsModel();
                model = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
                printObj.BusinessAddressLine1 = model.BusinessAddressLine1;
                printObj.BusinessAddressLine2 = model.BusinessAddressLine2;
                printObj.BusinessAddressLine3 = model.BusinessAddressLine3;
                printObj.BusinessPostalCode = model.BusinessPostalCode;
                printObj.FSBNumber = "FSB Number: " + model.FSBNumber;
                printObj.telephoneNumber = model.ManageTelNumber + " | " + model.ManageCellNumber;
                printObj.Id = id;
                printObj.Type = Type;
                printObj.PolicyNumber = PolicyNumber;
                printObj.DatePaid = DatePaid;
                printObj.AmountPaid = AmountPaid;
                printObj.PaidBy = PaidBy;
                printObj.ReceivedBy = ReceivedBy;
                printObj.MonthPaid = MonthPaid;
                printObj.ParlourName = ApplicationName;
                printObj.TimePrinted = Convert.ToString(System.DateTime.Now);

            }
            else if (Type == 2)
            {
                ApplicationSettingsModel model = new ApplicationSettingsModel();
                model = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
                printObj.BusinessAddressLine1 = model.BusinessAddressLine1;
                printObj.BusinessAddressLine2 = model.BusinessAddressLine2;
                printObj.BusinessAddressLine3 = model.BusinessAddressLine3;
                printObj.BusinessPostalCode = model.BusinessPostalCode;
                printObj.FSBNumber = "FPS Number: " + model.FSBNumber;
                printObj.telephoneNumber = model.ManageTelNumber + " | " + model.ManageCellNumber;
                printObj.Id = id;
                printObj.Type = Type;
                printObj.PolicyNumber = PolicyNumber;
                printObj.DatePaid = DatePaid;
                printObj.AmountPaid = AmountPaid;
                printObj.PaidBy = PaidBy;
                printObj.ReceivedBy = ReceivedBy;
                printObj.MonthPaid = MonthPaid;
                printObj.ParlourName = ApplicationName;
                printObj.TimePrinted = Convert.ToString(System.DateTime.Now);
            }

            return View(printObj);
        }
        public void bindInvoices(Guid ParlourId, int MemberId)
        {
            List<MemberInvoiceModel> objMemberInvoiceModel = MembersBAL.GetInvoicesByMemberID(ParlourId, MemberId);
        }
        public JsonResult CalculateAmount(int noOfMonths, int TotalPremieum, int LatePanelty, string NextDate)
        {
            var info = CultureInfo.InvariantCulture.Clone() as CultureInfo;
            info.NumberFormat.NumberDecimalSeparator = ".";

            CultureInfo cInfo = new CultureInfo(info.ToString());
            cInfo.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = cInfo;

            decimal Amount = 0;
            DateTime NextDate1 = Convert.ToDateTime(NextDate);
            string monthPaid = string.Empty;
            string currency = Currency;

            Amount = (TotalPremieum * noOfMonths) + LatePanelty;

            if (noOfMonths > 1)
            {
                if (NextDate1.Year == NextDate1.AddMonths(noOfMonths - 1).Year)
                    monthPaid = string.Format("{0}-{1} {2}", NextDate1.ToString("MMM"), NextDate1.AddMonths(noOfMonths - 1).ToString("MMM"), NextDate1.AddMonths(noOfMonths - 1).ToString("yyyy"));
                else
                    monthPaid = string.Format("{0} {1}-{2} {3}", NextDate1.ToString("MMM"), NextDate1.ToString("yyyy"), NextDate1.AddMonths(noOfMonths - 1).ToString("MMM"), NextDate1.AddMonths(noOfMonths - 1).ToString("yyyy"));

            }
            else
                monthPaid = string.Format("{0}-{1}", NextDate1.AddMonths(noOfMonths - 1).ToString("MMM"), NextDate1.AddMonths(noOfMonths - 1).ToString("yyyy"));

            double TotalPremium = Convert.ToDouble(TotalPremieum * Convert.ToInt32(noOfMonths) + LatePanelty);



            return Json(Amount + "~" + monthPaid + "~" + TotalPremium + "~" + currency, JsonRequestBehavior.AllowGet);
        }
        [PageRightsAttribute(CurrentPageId = 34, Right = new isPageRight[] { isPageRight.HasReversalPayment })]
        public ActionResult PaymentReversal(int id)
        {
            int PaymentID = MemberPaymentBAL.AddReversalPayments(id, UserName, ParlourId);
            string Message = "";
            if (PaymentID != 0)
            {
                bindInvoices(ParlourId, MemberId);
                Message = "Reversal added successfully.";
            }
            else
            {
                Message = "Reversal not added successfully.";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public void ReinstatePolicy()
        {
            try
            {
                //string txtMonthFrom = null;
                //string strMonths = null;
                //txtAmount.Enabled = false;
                //ddlNoOfMonths.Enabled = false;
                //rfvNoOfMonth.Enabled = false;
                //txtMohthPaid.Enabled = false;
                //txtAmount.Text = txtPolicyBalance.Text;
                //hdnAmount.Value = txtPolicyBalance.Text;
                //txtMonthFrom = ViewState["NextPaymentDate"].ToString();
                //for (int iCnt = 1; iCnt <= Convert.ToInt32(txtMonthOwing.Text) - 1; iCnt++)
                //{ strMonths = DateTime.Now.AddMonths(iCnt).ToString("MMM-yyyy"); }

                //ddlNoOfMonths.SelectedValue = txtMonthOwing.Text.Replace("-", "");
                //txtMohthPaid.Text = "Reinstate Policy and PAYMENT OF balance owing from: " + txtMonthFrom + " to " + strMonths;
            }
            catch (Exception ex)
            {
                //Interaction.MsgBox("Error:- " + ex.Message, MsgBoxStyle.Information);
            }
        }
        public void RejoinPolicy()
        {
            try
            {
                //string txtMonthFrom = null;
                //string strMonths = null;
                //string strRejoin = null;
                //txtMohthPaid.Enabled = false;
                //txtMonthFrom = ViewState["NextPaymentDate"].ToString();

                //for (int iCnt = 1; iCnt <= Convert.ToInt32(txtMonthOwing.Text) - 1; iCnt++)
                //{
                //    strMonths = DateTime.Now.AddMonths(iCnt).ToString("MMM-yyyy");

                //}
                //txtMohthPaid.Text = "Rejoin Policy and start trial period because policy Lapsed: " + txtMonthFrom + " to " + strMonths;
                //ddlNoOfMonths.SelectedIndex = 0;
            }
            catch
            {

            }
        }
        public void btnStatement(string memberId, Guid parlourid)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            string filename;
            try
            {
                ReportViewer rpw = new ReportViewer();
                rpw.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new MyReportServerCredentials();
                rpw.ServerReport.ReportServerCredentials = irsc;
                rpw.ProcessingMode = ProcessingMode.Remote;
                rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/UIS_Customer Payments Statement";
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("MemberID", memberId));//txtMemberNo.Text
                reportParameters.Add(new ReportParameter("Parlourid", parlourid.ToString()));
                rpw.ServerReport.SetParameters(reportParameters);
                string ExportTypeExtensions = "pdf";
                byte[] bytes = rpw.ServerReport.Render(ExportTypeExtensions, null, out mimeType, out encoding, out ExportTypeExtensions, out streamids, out warnings);
                filename = string.Format("{0}.{1}", "UIS_Customer Payments Statement", ExportTypeExtensions);
                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (Exception exc)
            {
                //ShowMessage(ref "lblMessage", MessageType.Danger, exc.Message);
            }
        }
    }
}
