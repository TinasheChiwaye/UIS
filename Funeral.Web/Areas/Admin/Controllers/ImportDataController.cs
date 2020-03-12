using Funeral.BAL;
using Funeral.Model;
using Funeral.Model.Search;
using Funeral.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class ImportDataController : BaseAdminController
    {
        // GET: Admin/ImportData
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ImportedHistory()
        {
            LoadEntriesCount();
            BindCompanyList("Search");
            MemberSearch search = new MemberSearch();
            search.CompanyId = new Guid(CurrentParlourId.ToString());
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = "";
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.StatusId = "0";
            search.TotalRecord = 0;
            var searchResult = new Funeral.Model.SearchResult<Model.Search.MemberSearch, ImportedHistory>(search, new List<ImportedHistory>(), o => o.ImportedFilename.Contains(search.SarchText) || o.parlourId.Equals(search.CompanyId));
            return View(search);

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
        public ActionResult SearchData(MemberSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<MemberSearch, ImportedHistory>(search, new List<ImportedHistory>(), o => o.ImportedFilename.ToLower().Contains(search.SarchText.ToLower()));
            try
            {
                var members = ToolsSetingBAL.GetImportedHistory(search.CompanyId);
                return Json(new Funeral.Model.SearchResult<MemberSearch, ImportedHistory>(search, members, o => o.ImportedFilename.ToLower().Contains(search.SarchText.ToLower())));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<MemberSearch, ImportedHistory>.Error(searchResult, ex));
            }
        }
        [HttpGet]
        public ActionResult MappingColumnList(Guid ImportedId)
        {
            ImportedHistory getHistory = ToolsSetingBAL.GetImportedHistory_ByNewImportedId(ImportedId);
            List<MappedDependents> mappedDependents = ToolsSetingBAL.GetMappedDependent(ImportedId);
            var GetAttributesColumn = ToolsSetingBAL.GetXMLColumn(getHistory.MappingColumn);
            var MappedColumns = Tuple.Create(GetAttributesColumn, mappedDependents);
            return PartialView("MappedColumnList", MappedColumns);
        }
    }
}