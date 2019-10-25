using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Areas.Admin.Controllers;
using Funeral.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static Funeral.Web.Areas.Admin.Controllers.MembersController;

namespace Funeral.Web.Areas.Tools.Controllers
{
    public class BranchSetupController : BaseAdminController
    {
        public BranchSetupController():base(15) {
            this.dbPageId = 15;
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

        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            //ViewBag.HasAccess = HasAccess;
            return View("Index");
        }

        [PageRightsAttribute(CurrentPageId = 15)]
        public PartialViewResult List()
        {
           
            //ViewBag.HasEditRight = HasEditRight;
            //ViewBag.HasDeleteRight = HasDeleteRight;

            Model.Search.BranchSearch search = new Model.Search.BranchSearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;

            var searchResult = new SearchResult<Model.Search.BranchSearch, BranchModel>(search, new List<BranchModel>(), o => o.BranchName.Contains(search.SarchText) || o.BranchCode.Contains(search.SarchText) || o.TelNumber.Contains(search.SarchText) || o.CellNumber.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Tools/Views/BranchSetup/_BranchSetupList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.BranchSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BranchSearch, BranchModel>(search, new List<BranchModel>(), o => o.BranchName.Contains(search.SarchText) || o.BranchCode.Contains(search.SarchText) || o.Address4.Contains(search.SarchText) || o.Address3.Contains(search.SarchText));

            try
            {
                var agentInfoSetups = ToolsSetingBAL.GetAllBranches(ParlourId);
                return Json(new SearchResult<Model.Search.BranchSearch, BranchModel>(search, agentInfoSetups, o => o.BranchName.Contains(search.SarchText) || o.BranchCode.Contains(search.SarchText) || o.Address4.Contains(search.SarchText) || o.Address3.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BranchSearch, BranchModel>.Error(searchResult, ex));
            }
        }
        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasAdd})]
        public PartialViewResult Add(BranchModel branchSetup)
        {
            //ViewBag.HasAddRight = HasCreateRight;
            branchSetup.Parlourid = ParlourId;
            ModelState.Clear();
            return PartialView("~/Areas/Tools/Views/BranchSetup/_BranchSetupAddEdit.cshtml", branchSetup);
        }

        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] {isPageRight.HasEdit })]
        public PartialViewResult Edit(Guid branchId)
        {
            var branchSetup = ToolsSetingBAL.EditBranchbyID(branchId, ParlourId);
            return PartialView("~/Areas/Tools/Views/BranchSetup/_BranchSetupAddEdit.cshtml", branchSetup);
        }

        public ActionResult Update(int branchId)
        {
            ViewBag.BranchId = branchId;
            return View("Index");
        }

        public ActionResult Save(BranchModel branchSetup)
        {
            try
             {  
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    branchSetup.LastModified = System.DateTime.Now;
                    branchSetup.ModifiedUser = formIdentity.Name;

                    var agentInfoSetupData = ToolsSetingBAL.SaveBranchDetails(branchSetup);

                    TempData["IsBranchSetupSaved"] = true;
                    TempData.Keep("IsBranchSetupSaved");

                    return RedirectToAction("Index", "BranchSetup", new { area = "Tools" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsBranchSetupSaved"] = false;
            TempData.Keep("IsBranchSetupSaved");

            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string, new { area = "Tools" });
        }
        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasDelete})]
        public JsonResult Delete(Guid branchId)
        {
            int retID = ToolsSetingBAL.DeleteBranch(branchId);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}