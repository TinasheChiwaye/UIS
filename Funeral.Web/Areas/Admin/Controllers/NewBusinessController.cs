using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class NewBusinessController : BaseAdminController
    {
        // GET: Admin/NewBusiness
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public NewBusinessController() : base(74)
        {
            this.dbPageId = 74;
        }

        [PageRightsAttribute(CurrentPageId = 74)]
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

                var searchResult = new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, new List<MembersModel>(), o => o.IDNumber.Contains(search.SarchText)
                     || o.MemeberNumber.Contains(search.SarchText));
                return View(search);
            }
            else
            {
                return View();
            }

        }
        public void LoadStatus()
        {
            var statusList = CommonBAL.GetStatus(FuneralEnum.StatusAssociatedTable.Members.ToString()).Select(status => new { status.Status, status.ID }).Distinct();
            ViewBag.Statuses = statusList;
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
            ViewBag.EntriesCount = keyValues;
        }
    }
}