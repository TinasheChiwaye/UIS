using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.Common;
using PayFast;
using PayFast.AspNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Client.Controllers
{
    public class MyPolicyController : BaseClientController
    {
        // GET: Client/MyPolicy
        private readonly PayFastSettings payFastSettings;
        public MyPolicyController()
        {
            this.payFastSettings = new PayFastSettings();
            this.payFastSettings.MerchantId = ConfigurationManager.AppSettings["MerchantId"];
            this.payFastSettings.MerchantKey = ConfigurationManager.AppSettings["MerchantKey"];
            this.payFastSettings.PassPhrase = ConfigurationManager.AppSettings["PassPhrase"];
            this.payFastSettings.ProcessUrl = ConfigurationManager.AppSettings["ProcessUrl"];
            this.payFastSettings.ValidateUrl = ConfigurationManager.AppSettings["ValidateUrl"];
            this.payFastSettings.ReturnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            this.payFastSettings.CancelUrl = ConfigurationManager.AppSettings["CancelUrl"];
            this.payFastSettings.NotifyUrl = ConfigurationManager.AppSettings["NotifyUrl"];
        }

        public ActionResult Index()
        {
            LoadStatus();
            LoadEntriesCount();
            Model.Search.MemberSearch search = new Model.Search.MemberSearch();
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
        [HttpPost]
        public ActionResult SearchData(Model.Search.MemberSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, new List<MembersModel>(), o => o.IDNumber.ToLower().Contains(search.SarchText.ToLower()));
            try
            {
                var members = ClientPortalBAL.GetAllMembers(IdNumber, search.PageSize, search.PageNum, search.SarchText, search.SortBy, search.SortOrder, search.StatusId.ToString());
                return Json(new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, members.MemberList, o => o.IDNumber.ToLower().Contains(search.SarchText.ToLower()) || o.MemeberNumber.ToLower().Contains(search.SarchText.ToLower()) || o.Surname.ToLower().Contains(search.SarchText.ToLower()) || o.FullNames.ToLower().Contains(search.SarchText.ToLower()) || o.Cellphone.ToLower().Contains(search.SarchText.ToLower()) || o.EasyPayNo.ToLower().Contains(search.SarchText.ToLower())));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.MemberSearch, MembersModel>.Error(searchResult, ex));
            }
        }
        public void LoadStatus()
        {
            var statusList = CommonBAL.GetStatus(FuneralEnum.StatusAssociatedTable.Members.ToString()).Select(status => new { status.Status, status.ID }).Distinct();
            ViewBag.Statuses = statusList;
        }
        [HttpGet]
        public ActionResult MakePayment(int id, Guid ParlourID)
        {
            MembersPaymentDetailsModel model = ClientPortalBAL.ReturnMemberPlanDetailsWithBalance(Convert.ToString(id), ParlourID);
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
        public ActionResult AddPayments(MembersPaymentDetailsModel data)
        {

            MembersModel objmember = MembersBAL.GetMemberByID(data.pkiMemberID, data.ParlourId);
            if (objmember.MemberBranch != "")
            {
                data.Branch = objmember.MemberBranch;
            }
            data.ParlourId = data.ParlourId;
            data.NextPaymentDate = Convert.ToDateTime(data.PaymentDate).AddMonths(Convert.ToInt32(data.MonthOwing));
            var paymentId = MemberPaymentBAL.AddPayments(data, true);
            if (paymentId > 0)
            {

                var onceOffRequest = new PayFastRequest(this.payFastSettings.PassPhrase);
                // Merchant Details
                onceOffRequest.merchant_id = this.payFastSettings.MerchantId;
                onceOffRequest.merchant_key = this.payFastSettings.MerchantKey;
                onceOffRequest.return_url = this.payFastSettings.ReturnUrl;
                onceOffRequest.cancel_url = this.payFastSettings.CancelUrl;
                onceOffRequest.notify_url = this.payFastSettings.NotifyUrl;

                onceOffRequest.custom_int1 = data.pkiMemberID;
                onceOffRequest.custom_str1 = data.ParlourId.ToString();



                // Buyer Details
                onceOffRequest.email_address = "hemant@truecodemasters.com";

                // Transaction Details
                onceOffRequest.m_payment_id = paymentId.ToString();
                onceOffRequest.amount = Convert.ToDouble(data.Amount);
                onceOffRequest.item_name = "Member policy Payment";
                onceOffRequest.item_description = "Member policy Payment";

                // Transaction Options
                onceOffRequest.email_confirmation = true;
                onceOffRequest.confirmation_address = "contact@truecodemasters.com";

                var redirectUrl = $"{this.payFastSettings.ProcessUrl}{onceOffRequest.ToString()}";

                return Json(redirectUrl, JsonRequestBehavior.AllowGet);
                //return Redirect(redirectUrl);
            }
            else
            {
                TempData["message"] = ShowMessage(MessageType.Danger, "Payment not added successfully");
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Return()
        {
            return RedirectToAction("Index", "MyPolice");
        }

        public ActionResult Cancel()
        {
            TempData["message"] = ShowMessage(MessageType.Success, "Payment has been cancelled");
            return RedirectToAction("Index", "MyPolicy");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Notify([ModelBinder(typeof(PayFastNotifyModelBinder))]PayFastNotify payFastNotifyViewModel)
        {
            try
            {
                PayfastRequestModel payFastNotify = ConvertToModel(payFastNotifyViewModel);
                int MemberId = Convert.ToInt32(payFastNotify.custom_int1);
                Guid parlourId = Guid.Parse(payFastNotify.custom_str1);
                ClientPortalBAL.SaveClientPayment(payFastNotify);
                TempData["message"] = ShowMessage(MessageType.Success, "Payment added successfully");
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public ActionResult Error()
        {
            return View();
        }
        private PayfastRequestModel ConvertToModel(PayFastNotify payFastNotifyViewModel)
        {
            try
            {
                return new PayfastRequestModel()
                {
                    amount_fee = payFastNotifyViewModel.amount_fee,
                    token = payFastNotifyViewModel.token,
                    signature = payFastNotifyViewModel.signature,
                    pf_payment_id = payFastNotifyViewModel.pf_payment_id,
                    payment_status = payFastNotifyViewModel.payment_status,
                    name_last = payFastNotifyViewModel.name_last,
                    amount_gross = payFastNotifyViewModel.amount_gross,
                    amount_net = payFastNotifyViewModel.amount_net,
                    billing_date = payFastNotifyViewModel.billing_date,
                    custom_int1 = payFastNotifyViewModel.custom_int1,
                    custom_int2 = payFastNotifyViewModel.custom_int2,
                    custom_int3 = payFastNotifyViewModel.custom_int3,
                    custom_int4 = payFastNotifyViewModel.custom_int4,
                    custom_int5 = payFastNotifyViewModel.custom_int5,
                    custom_str1 = payFastNotifyViewModel.custom_str1,
                    custom_str2 = payFastNotifyViewModel.custom_str2,
                    custom_str3 = payFastNotifyViewModel.custom_str3,
                    custom_str4 = payFastNotifyViewModel.custom_str4,
                    custom_str5 = payFastNotifyViewModel.custom_str5,
                    email_address = payFastNotifyViewModel.email_address,
                    item_description = payFastNotifyViewModel.item_description,
                    item_name = payFastNotifyViewModel.item_name,
                    merchant_id = payFastNotifyViewModel.merchant_id,
                    m_payment_id = payFastNotifyViewModel.m_payment_id,
                    name_first = payFastNotifyViewModel.name_first
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public ActionResult PaymentReversal(int id)
        //{
        //    int PaymentID = MemberPaymentBAL.AddReversalPayments(id, UserName, ParlourId);
        //    string Message = "";
        //    if (PaymentID != 0)
        //    {
        //        bindInvoices(ParlourId, MemberId);
        //        Message = "Reversal added successfully.";
        //    }
        //    else
        //    {
        //        Message = "Reversal not added successfully.";
        //    }
        //    return Json(Message, JsonRequestBehavior.AllowGet);
        //}
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
        public ActionResult CancelPayment(int id, Guid ParlourID)
        {
            TempData["message"] = ShowMessage(MessageType.Success, "Payment has been cancelled");
            return RedirectToAction("MakePayment", "MyPolicy", new { id = id, ParlourID = ParlourID });
        }
    }
}