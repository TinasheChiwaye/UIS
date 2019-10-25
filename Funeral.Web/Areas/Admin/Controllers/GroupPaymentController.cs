using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using System;
using System.Collections.Generic;
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


        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult GroupPaymentView(int id)
        {
            ViewBag.GroupId = id;
            TempData["GroupId"] = id;
            return View();
        }
        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasAdd })]
        public PartialViewResult Add(GroupPayment GroupPayment)
        {
            GroupPayment.parlourid = ParlourId;
            object value = TempData.Peek("GroupId");
            TempData.Keep("GroupId");
            GroupPayment.SocietyId = Convert.ToInt32(value);
            GroupPayment.DatePaid = DateTime.Now;
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
                    return RedirectToAction("GroupPaymentView", "GroupPayment", new { id = groupPayment.SocietyId, Area = "Admin" });
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
            var searchResult = new SearchResult<Model.Search.BaseSearch, SocietyModel>(search, new List<SocietyModel>(), o => o.SocietyName.Contains(search.SarchText));
            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;
            return PartialView("~/Areas/Admin/Views/GroupPayment/_GroupPaymentList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, SocietyModel>(search, new List<SocietyModel>(), o => o.SocietyName.Contains(search.SarchText));

            try
            {
                object value = TempData.Peek("GroupId");
                TempData.Keep("GroupId");
                int GroupId = Convert.ToInt32(value);
                var SocietyList = OtherPaymentBAL.GetAllGroupPaymentList(ParlourId, GroupId);
                return Json(new SearchResult<Model.Search.BaseSearch, GroupPayment>(search, SocietyList, o => o.SocietyName.ToLower().Contains(search.SarchText.ToLower())));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, SocietyModel>.Error(searchResult, ex));
            }
        }
        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int ID)
        {
            var groupPayment = OtherPaymentBAL.EditGroupPaymentByID(ID, ParlourId);
            groupPayment.SocietyDropdown = CommonBAL.GetAllSocietyesList(ParlourId);
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
                var SocietyList = ToolsSetingBAL.GetAllSocietyes(ParlourId);
                return Json(new SearchResult<Model.Search.BaseSearch, SocietyModel>(search, SocietyList, o => o.SocietyName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, SocietyModel>.Error(searchResult, ex));
            }
        }
    }
}