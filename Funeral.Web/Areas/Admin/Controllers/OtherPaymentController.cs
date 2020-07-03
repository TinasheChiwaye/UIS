using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.Areas.Admin.Models.ViewModel;
using Funeral.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class OtherPaymentController : BaseAdminController
    {
        public OtherPaymentController() : base(30)
        {
            this.dbPageId = 30;
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

        // GET: Admin/OtherPayment
        public ActionResult Index()
        {
            ViewBag.HasAccess = HasAccess;
            return View("Index");
        }

        public PartialViewResult List()
        {
            ViewBag.HasEditRight = HasEditRight;
            ViewBag.HasDeleteRight = HasDeleteRight;
            Model.Search.OthrePaymentSearch search = new Model.Search.OthrePaymentSearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SearchPolicyId = string.Empty;
            search.SearchId = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;
            var searchResult = new OtherPaymentsSearchResult<Model.Search.OthrePaymentSearch, MembersPaymentModel>(search, new List<MembersPaymentModel>(), o => o.IDNumber.Contains(search.SearchPolicyId) || o.MemeberNumber.Contains(search.SearchId));
            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;
            return PartialView("~/Areas/Admin/Views/OtherPayment/_OtherPaymentList.cshtml", search);
        }

        public ActionResult SearchData(Model.Search.OthrePaymentSearch search)
        {
            var searchResult = new OtherPaymentsSearchResult<Model.Search.OthrePaymentSearch, MembersPaymentModel>(search, new List<MembersPaymentModel>(), o =>  o.IDNumber.Contains(search.SearchPolicyId) || o.MemeberNumber.Contains(search.SearchId));
            try
            {
                var otherPayment = MemberPaymentBAL.GetAllPayentMembers(ParlourId, search.SearchPolicyId, search.SearchId, search.PageSize, search.PageNum, search.SortBy, search.SortOrder, "","");
                if(search.SearchId == null )
                {
                    return Json(new OtherPaymentsSearchResult<Model.Search.OthrePaymentSearch, MembersPaymentModel>(search, otherPayment.MemberList, o => o.MemeberNumber.Contains(search.SearchPolicyId)));
                }

                return Json(new OtherPaymentsSearchResult<Model.Search.OthrePaymentSearch, MembersPaymentModel>(search, otherPayment.MemberList, o => o.IDNumber.Contains(search.SearchId) || o.MemeberNumber.Contains(search.SearchPolicyId)));
            }
            catch (Exception ex)
            {
                return Json(OtherPaymentsWebApiResult<Model.Search.OthrePaymentSearch, MembersPaymentModel>.Error(searchResult, ex));
            }
        }

        [FuneralAuth(PageId = 30, Right = new Rights[] { Rights.HasAdd })]
        public ActionResult Add(MembersOtherPaymentDetailsVM memberPaymentDetail)
        {
            OtherPaymentModel model = new OtherPaymentModel();
            model.MemberID = memberPaymentDetail.MembersModel.pkiMemberID;
            model.RecievedBy = memberPaymentDetail.ReceivedBy;
            model.AmountPaid = memberPaymentDetail.amount;
            model.MethodOfPayment = memberPaymentDetail.methodOfPayment;
            model.DatePaid = Convert.ToDateTime(memberPaymentDetail.date);
            model.PaymentTypeId = 1;
            model.Notes = memberPaymentDetail.Notes;
            model.Parlourid = ParlourId;
            model.ModifiedUser = UserName;
            int InvoiceID = OtherPaymentBAL.OtherPaymentDetailsSave(model);
            if (InvoiceID > 0)
            {
                model.Notes = "Update SuccessFully";
            }
            else
            {
                model.Notes = "Insert Succssfully";

            }
            ViewBag.Number = 10;
            return RedirectToAction("EditOtherPayment",new { invoiceId = 0 , MemeberNumber = memberPaymentDetail.MembersModel.pkiMemberID , messgae = model.Notes });
        }

        [FuneralAuth(PageId = 30, Right = new Rights[] { Rights.HasEdit })]
        public ActionResult EditOtherPayment(int invoiceId = 0, int MemeberNumber = 0 , string messgae = "")
        {
            try
            {

            MembersOtherPaymentDetailsVM vmModel = new MembersOtherPaymentDetailsVM();
            vmModel.MembersModel = MembersBAL.GetMemberByID(MemeberNumber, ParlourId);
            vmModel.OtherPaymentModel = OtherPaymentBAL.OtherPaymentSelectByMemberId(MemeberNumber, ParlourId);
            if (vmModel.OtherPaymentModel.Count > 0)
            {
                vmModel.ReceivedBy = vmModel.OtherPaymentModel[0].RecievedBy;
                vmModel.date = vmModel.OtherPaymentModel[0].DatePaid.ToString("dd-MMM-yyyy");
                vmModel.amount = vmModel.OtherPaymentModel[0].AmountPaid;
                vmModel.methodOfPayment = vmModel.OtherPaymentModel[0].MethodOfPayment;
                vmModel.Notes = vmModel.OtherPaymentModel[0].Notes;
            }
                ViewBag.message = messgae;
                return View(vmModel);
            }
            catch (Exception)
            {
                ViewBag.message = "Error !";
                return View();
            }
        }
        public ActionResult Update(int branchId)
        {
            ViewBag.BranchId = branchId;
            return View("Index");
        }

        public ActionResult OtherPaymentsSearch()
        {
            MembersPaymentViewModel model = new MembersPaymentViewModel();
            model = MemberPaymentBAL.GetAllPayentMembers(ParlourId, "", "", 10, 1, "pkiMemberID", "ASC", "","");
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult EditOtherPayment(OtherPaymentModel model)
        {

            return View();
        }
    }
}