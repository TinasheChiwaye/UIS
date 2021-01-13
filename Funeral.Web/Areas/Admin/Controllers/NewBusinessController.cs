using Funeral.BAL;
using Funeral.Model;
using Funeral.Model.Search;
using Funeral.Web.App_Start;
using Funeral.Web.Areas.Admin.Models.ViewModel;
using Funeral.Web.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class NewBusinessController : BaseAdminController
    {

        private readonly ISiteSettings _siteConfig = new SiteSettings();
        public static string GetdataPremium;

        public NewBusinessController() : base(76)
        {
            this.dbPageId = 76;
        }

        [PageRightsAttribute(CurrentPageId = 76)]
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
        [HttpPost]
        public ActionResult SearchData(Model.Search.MemberSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, new List<MembersModel>(), o => o.IDNumber.ToLower().Contains(search.SarchText.ToLower()));
            try
            {
                var members = MembersBAL.GetAllMembers(search.CompanyId, search.PageSize, search.PageNum, search.SarchText, search.SortBy, search.SortOrder, search.StatusId.ToString(), search.BookID);
                return Json(new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, members.MemberList, o => o.IDNumber.ToLower().Contains(search.SarchText.ToLower()) || o.MemeberNumber.ToLower().Contains(search.SarchText.ToLower()) || o.Surname.ToLower().Contains(search.SarchText.ToLower()) || o.FullNames.ToLower().Contains(search.SarchText.ToLower()) || o.Cellphone.ToLower().Contains(search.SarchText.ToLower()) || o.EasyPayNo.ToLower().Contains(search.SarchText.ToLower())));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.MemberSearch, MembersModel>.Error(searchResult, ex));
            }
        }
    }
}