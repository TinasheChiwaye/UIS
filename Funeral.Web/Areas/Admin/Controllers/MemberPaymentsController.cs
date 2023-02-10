using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Areas.Admin.Models.ViewModel;
using Funeral.Web.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Data;
using System.Linq;
using Funeral.Model.Search;
	

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class MemberPaymentsController : BaseAdminController
    {
        // GET: Admin/MemberPayments
        private readonly ISiteSettings _siteConfig  = new SiteSettings();

        public MemberPaymentsController() : base(72)
        {
            this.dbPageId = 72;
        }


        [PageRightsAttribute(CurrentPageId = 72)]
        public ActionResult Index()
        {
            ViewBag.HasAccess = HasAccess;
            if (ViewBag.HasAccess == true)
            {
                var statusList = CommonBAL.GetStatus(FuneralEnum.StatusAssociatedTable.Members.ToString()).Select(x => new SelectListItem() { Text = x.ID.ToString(), Value = x.Status });
                ViewBag.StatusList = statusList;

                ViewBag.HasEditRight = HasEditRight;
                ViewBag.HasDeleteRight = HasDeleteRight;

                ViewBag.totalPremium = Currency;

                ViewBag.SocietyLists = CommonBAL.GetSocietyByParlourId(CurrentParlourId);

                LoadStatus();
                LoadEntriesCount();
                BindCompanyList("Search");

                Model.Search.MemberSearch search = new Model.Search.MemberSearch();
                search.CompanyId = new Guid(CurrentParlourId.ToString());
                search.PageNum = 1;
                search.PageSize = 10;
                search.SarchText = "";
                search.SortBy = "";
                search.SortOrder = "Asc";
                search.StatusId = "0";
                search.TotalRecord = 0;
                search.BookID = "";

                var searchResult = new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, new List<MembersModel>(),
                    o => o.IDNumber.Contains(search.SarchText)
                    || o.MemeberNumber.Contains(search.SarchText));
                return View(search);
            }
            else
            {
                return View();
            }
        }
        public void LoadEntriesCount()
        {
            List<KeyValue> keyValues = new List<KeyValue>();
            List<SelectListItem> entriesItems = new List<SelectListItem>();
            keyValues.Add(new KeyValue { Key = "10", Value = "10" });
            keyValues.Add(new KeyValue { Key = "20", Value = "20" });
            keyValues.Add(new KeyValue { Key = "25", Value = "25" });
            keyValues.Add(new KeyValue { Key = "50", Value = "50" });
            keyValues.Add(new KeyValue { Key = "100", Value = "100" });
             
            keyValues.Add(new KeyValue { Key = "200", Value = "200" });
            keyValues.Add(new KeyValue { Key = "250", Value = "250" });
            keyValues.Add(new KeyValue { Key = "500", Value = "500" });
            keyValues.Add(new KeyValue { Key = "1000", Value = "1000" });
            ViewBag.EntriesCount = keyValues;
        }
        public void LoadStatus()
        {
            var statusList = CommonBAL.GetStatus(FuneralEnum.StatusAssociatedTable.Members.ToString()).Select(status => new { status.Status, status.ID }).Distinct();
            ViewBag.Statuses = statusList;
        }
        [HttpPost]
        public ActionResult SearchData(Model.Search.MemberSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, new List<MembersModel>(), o => o.IDNumber.ToLower().Contains(search.SarchText.ToLower()));
            try
            {
                var members = MembersBAL.GetAllMembers(search.CompanyId, search.PageSize, search.PageNum, search.SarchText, search.SortBy, search.SortOrder, search.StatusId.ToString(), "");
                return Json(new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, members.MemberList, o => o.IDNumber.ToLower().Contains(search.SarchText.ToLower()) || o.MemeberNumber.ToLower().Contains(search.SarchText.ToLower()) || o.Surname.ToLower().Contains(search.SarchText.ToLower()) || o.FullNames.ToLower().Contains(search.SarchText.ToLower()) || o.Cellphone.ToLower().Contains(search.SarchText.ToLower()) || o.EasyPayNo.ToLower().Contains(search.SarchText.ToLower())));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.MemberSearch, MembersModel>.Error(searchResult, ex));
            }
        }
        [HttpPost]
        public ActionResult GetAllData(Model.Search.MemberSearch search)
        {
            return null;
        }


        [HttpGet]
        [PageRightsAttribute(CurrentPageId = 72)]
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

                if (model.IsJoiningFee == false)
                {
                    ViewBag.IsJoiningFee = false;
                }
                else if (model.IsJoiningFee == true)
                {
                    ViewBag.IsJoiningFee = true;
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
                var JoingingFee = model.JoiningFee;
                ViewBag.JoingingFee = JoingingFee;

                //ViewBag.MonthToPay = MembersBAL.GetMonthsToPay(id);
            }
            var info = CultureInfo.InvariantCulture.Clone() as CultureInfo;
            info.NumberFormat.NumberDecimalSeparator = ".";

            CultureInfo cInfo = new CultureInfo(info.ToString());
            cInfo.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = cInfo;
            return View(model);
        }

        //=============================TEST[AddPayments] - Charles.M =======================================
        [HttpPost]
        [PageRightsAttribute(CurrentPageId = 72, Right = new isPageRight[] { isPageRight.HasAdd })]
        public JsonResult AddPayments(MembersPaymentDetailsModel data)
        {
            //add Joining fee boolean in method
            MembersModel objmember = MembersBAL.GetMemberByID(data.pkiMemberID, ParlourId);
            //PaymentReminderModel outstandingPayment = MemberPaymentBAL.GetOustandingPaymentByMemberId(data.LatePaymentId);
            if (objmember.MemberBranch != "")
            {
                data.Branch = objmember.MemberBranch;
            }
            //data.Notes = outstandingPayment.MemberNotes;
            data.ParlourId = ParlourId;
            data.NextPaymentDate = Convert.ToDateTime(data.PaymentDate).AddMonths(Convert.ToInt32(data.MonthOwing));
            var paymentId = MemberPaymentBAL.AddPayments(data, true);
            if (paymentId > 0)
            {
                TempData["message"] = ShowMessage(MessageType.Success, "Payment added successfully");
                return Json("Payment added successfully.", JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["message"] = ShowMessage(MessageType.Danger, "Payment not added successfully");
                return Json("Payment not added successfully.", JsonRequestBehavior.AllowGet);
            }
        }

        //=============================TEST END=======================================

        //[HttpPost]
        //[PageRightsAttribute(CurrentPageId = 72, Right = new isPageRight[] { isPageRight.HasAdd })]
        //public JsonResult AddPayments(MembersPaymentDetailsModel data)
        //{
        //    //add Joining fee boolean in method
        //    MembersModel objmember = MembersBAL.GetMemberByID(data.pkiMemberID, ParlourId);
        //    if (objmember.MemberBranch != "")
        //    {
        //        data.Branch = objmember.MemberBranch;
        //    }
        //    data.ParlourId = ParlourId;
        //    data.NextPaymentDate = Convert.ToDateTime(data.PaymentDate).AddMonths(Convert.ToInt32(data.MonthOwing));
        //    var paymentId = MemberPaymentBAL.AddPayments(data, true);
        //    if (paymentId > 0)
        //    {
        //        TempData["message"] = ShowMessage(MessageType.Success, "Payment added successfully");
        //        return Json("Payment added successfully.", JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        TempData["message"] = ShowMessage(MessageType.Danger, "Payment not added successfully");
        //        return Json("Payment not added successfully.", JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult JoiningFee(MembersPaymentDetailsModel data)
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
            return Json("JoiningFee added successfully.", JsonRequestBehavior.AllowGet);
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
        //public JsonResult CalculateAmount(int noOfMonths, int TotalPremieum, int LatePanelty, int NextDate)
        //{
        //    var info = CultureInfo.InvariantCulture.Clone() as CultureInfo;
        //    info.NumberFormat.NumberDecimalSeparator = ".";

        //    CultureInfo cInfo = new CultureInfo(info.ToString());
        //    cInfo.NumberFormat.NumberDecimalSeparator = ".";

        //    Thread.CurrentThread.CurrentCulture = cInfo;

        //    decimal Amount = 0;
        //    DateTime NextDate1 = Convert.ToDateTime(NextDate);
        //    string monthPaid = string.Empty;
        //    string currency = Currency;

        //    Amount = (TotalPremieum * noOfMonths) + LatePanelty;

        //    if (noOfMonths > 1)
        //    {
        //        if (NextDate1.Year == NextDate1.AddMonths(noOfMonths - 1).Year)
        //            monthPaid = string.Format("{0}-{1} {2}", NextDate1.ToString("MMM"), NextDate1.AddMonths(noOfMonths - 1).ToString("MMM"), NextDate1.AddMonths(noOfMonths - 1).ToString("yyyy"));
        //        else
        //            monthPaid = string.Format("{0} {1}-{2} {3}", NextDate1.ToString("MMM"), NextDate1.ToString("yyyy"), NextDate1.AddMonths(noOfMonths - 1).ToString("MMM"), NextDate1.AddMonths(noOfMonths - 1).ToString("yyyy"));

        //    }
        //    else
        //        monthPaid = string.Format("{0}-{1}", NextDate1.AddMonths(noOfMonths - 1).ToString("MMM"), NextDate1.AddMonths(noOfMonths - 1).ToString("yyyy"));

        //    double TotalPremium = Convert.ToDouble(TotalPremieum * Convert.ToInt32(noOfMonths) + LatePanelty);



        //    return Json(Amount + "~" + monthPaid + "~" + TotalPremium + "~" + currency, JsonRequestBehavior.AllowGet);
        //}


        //=========================test===============================
        public JsonResult CalculateAmount(int noOfMonths, decimal TotalPremieum, int LatePanelty, string NextDate)
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
        //=========================test end===============================

        [PageRightsAttribute(CurrentPageId = 72, Right = new isPageRight[] { isPageRight.HasReversalPayment })]
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
            //try
            //{
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
            //}
            //catch (Exception exc)
            //{
            //ShowMessage(ref "lblMessage", MessageType.Danger, exc.Message);
            //}
        }

        [HttpPost]
        public JsonResult BindGroupByCompanyId(Guid CompanyId)
        {
            var Company = CommonBAL.GetSocietyByParlourId(CompanyId).Select(x => new SelectListItem() { Text = x.SocietyName, Value = x.pkiSocietyID.ToString() });
            return Json(Company, JsonRequestBehavior.AllowGet);
        }

        [PageRightsAttribute(CurrentPageId = 72)]
        public void UpdatePolicyStatus(string policyStatus, int memberId)

        {
            MembersBAL.UpdateMemberPolicyStatus(policyStatus, memberId, UserName);
            CommonBAL.SaveAudit(UserName, CurrentParlourId, "Policy Status Changed");
        }

    }
}