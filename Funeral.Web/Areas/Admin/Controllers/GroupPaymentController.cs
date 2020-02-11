using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class GroupPaymentController : BaseAdminController
    {
        // GET: Admin/GroupPayment
        private readonly ISiteSettings _siteConfig = new SiteSettings();
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
        public GroupPaymentController() : base(7)
        {
            this.dbPageId = 7;
        }
        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            return View("Index");
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

        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult GroupPaymentView(Guid id)
        {
            ViewBag.GroupId = id;
            TempData["GroupId"] = id;
            return View();
        }
        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasAdd })]
        public PartialViewResult Add(GroupPayment GroupPayment)
        {

            object value = TempData.Peek("GroupId");
            TempData.Keep("GroupId");
            var GetDetails = ToolsSetingBAL.GetGroupPayment_ByParlourId(Guid.Parse(value.ToString()));
            if (GetDetails != null)
            {
                GroupPayment.SocietyId = GetDetails.GroupId;
                GroupPayment.AmountPaid = GetDetails.Premium;
                GroupPayment.CompanyGroupId = GetDetails.parlourid;
                GroupPayment.TotalRiskCovered = GetDetails.TotalRiskCovered;
                GroupPayment.Balance = GetDetails.Balance;
                GroupPayment.AmountAtRisk = GetDetails.AmountAtRisk;
                GroupPayment.InceptionDate = GetDetails.InceptionDate;
            }
            GroupPayment.DatePaid = DateTime.Now;
            GroupPayment.parlourid = ParlourId;
            GroupPayment.SocietyDropdown = CommonBAL.GetAllSocietyesList(ParlourId);
            ModelState.Clear();
            return PartialView("~/Areas/Admin/Views/GroupPayment/_AddGroupPayment.cshtml", GroupPayment);
        }
        public ActionResult Save(GroupPayment groupPayment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    groupPayment.LastModified = System.DateTime.Now;
                    groupPayment.parlourid = ParlourId;
                    groupPayment.PaidBy = formIdentity.Name;
                    groupPayment.RecievedBy = formIdentity.Name;
                    var agentInfoSetupData = OtherPaymentBAL.AddEditGroupPayment(groupPayment);
                    TempData["IsSocietySetupSaved"] = true;
                    TempData.Keep("IsSocietySetupSaved");
                    return RedirectToAction("GroupPaymentView", "GroupPayment", new { id = groupPayment.CompanyGroupId, Area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            TempData["IsSocietySetupSaved"] = false;
            TempData.Keep("IsSocietySetupSaved");
            return RedirectToAction("Index", "GroupPayment", new { area = "Admin" });
        }
        [PageRightsAttribute(CurrentPageId = 7)]
        public PartialViewResult List()
        {
            ViewBag.HasEditRight = HasEditRight;
            ViewBag.HasDeleteRight = HasDeleteRight;

            Model.Search.BaseSearch search = new Model.Search.BaseSearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;
            var searchResult = new SearchResult<Model.Search.BaseSearch, GroupPayment>(search, new List<GroupPayment>(), o => o.SocietyName.Contains(search.SarchText));
            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;
            return PartialView("~/Areas/Admin/Views/GroupPayment/_GroupPaymentList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, GroupPayment>(search, new List<GroupPayment>(), o => o.SocietyName.Contains(search.SarchText));

            try
            {
                object value = TempData.Peek("GroupId");
                TempData.Keep("GroupId");

                var groupPayment = ToolsSetingBAL.GetGroupPayment_ByParlourId(Guid.Parse(value.ToString()));
                var SocietyList = new List<GroupPayment>();
                if (groupPayment != null)
                {
                    SocietyList = OtherPaymentBAL.GetAllGroupPaymentList(ParlourId, groupPayment.GroupId);
                }
                return Json(new SearchResult<Model.Search.BaseSearch, GroupPayment>(search, SocietyList, o => o.SocietyName.ToLower().Contains(search.SarchText.ToLower())));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, GroupPayment>.Error(searchResult, ex));
            }
        }
        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int ID)
        {
            var groupPayment = OtherPaymentBAL.EditGroupPaymentByID(ID, ParlourId);
            var GetDetails = ToolsSetingBAL.GetGroupPayment_ByParlourId(groupPayment.parlourid);
            groupPayment.SocietyDropdown = CommonBAL.GetAllSocietyesList(ParlourId);
            if (GetDetails != null)
            {
                groupPayment.AmountAtRisk = GetDetails.AmountAtRisk;
                groupPayment.Balance = GetDetails.Balance;
                groupPayment.TotalRiskCovered = GetDetails.TotalRiskCovered;
                groupPayment.InceptionDate = GetDetails.InceptionDate;
            }
            return PartialView("~/Areas/Admin/Views/GroupPayment/_AddGroupPayment.cshtml", groupPayment);
        }
        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasDelete })]
        public JsonResult Delete(int ID)
        {
            int retID = OtherPaymentBAL.DeleteGroupPayment(ID);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [PageRightsAttribute(CurrentPageId = 7)]
        public PartialViewResult GroupList()
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

            return PartialView("~/Areas/Admin/Views/GroupPayment/_PaymentList.cshtml", search);
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
       
    }
}