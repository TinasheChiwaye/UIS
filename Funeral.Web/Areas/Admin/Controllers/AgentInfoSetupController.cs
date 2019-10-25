using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using Funeral.Web.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using static Funeral.Web.Areas.Admin.Controllers.MembersController;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class AgentInfoSetupController : BaseAdminController
    {
        public AgentInfoSetupController() : base(17)
        {
            this.dbPageId = 17;
        }

        // GET: Admin/AgentInfoSetup
        [PageRightsAttribute(CurrentPageId = 17, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            //ViewBag.HasAccess = HasAccess;
            return View("Index");
        }

        [PageRightsAttribute(CurrentPageId = 17)]
        public ActionResult Update(int agentInfoSetupId)
        {
            ViewBag.AgentInfoSetupId = agentInfoSetupId;
            return View("Index");
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

        [PageRightsAttribute(CurrentPageId = 17)]
        public PartialViewResult List()
        {
            //ViewBag.HasEditRight = HasEditRight;
            //ViewBag.HasDeleteRight = HasDeleteRight;

            Model.Search.AgentSearch search = new Model.Search.AgentSearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;

            var searchResult = new SearchResult<Model.Search.AgentSearch, AgentInfoSetupModel>(search, new List<AgentInfoSetupModel>(), o => o.Fullname.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Admin/Views/AgentInfoSetup/_AgentInfoSetupList.cshtml", search);
        }

        [PageRightsAttribute(CurrentPageId = 17, Right = new isPageRight[] { isPageRight.HasAdd })]
        public PartialViewResult Add(AgentInfoSetupModel agentInfoSetup)
        {
            agentInfoSetup.parlourid = ParlourId;
            ModelState.Clear();
            return PartialView("~/Areas/Admin/Views/AgentInfoSetup/_AgentInfoSetupAddEdit.cshtml", agentInfoSetup);
        }


        [PageRightsAttribute(CurrentPageId = 17, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int agentId)
        {
            var agentInfoSetup = ToolsSetingBAL.GetAgentByID(agentId);
            return PartialView("~/Areas/Admin/Views/AgentInfoSetup/_AgentInfoSetupAddEdit.cshtml", agentInfoSetup);
        }

        public ActionResult SearchData(Model.Search.AgentSearch search)
        {
            var searchResult = new SearchResult<Model.Search.AgentSearch, AgentInfoSetupModel>(search, new List<AgentInfoSetupModel>(), o => o.Fullname.Contains(search.SarchText));

            try
            {
                var agentInfoSetups = ToolsSetingBAL.GetAllAgentInfo(ParlourId, search.PageSize, search.PageNum, search.SarchText, search.SortBy, search.SortOrder);
                return Json(new SearchResult<Model.Search.AgentSearch, AgentInfoSetupModel>(search, agentInfoSetups, o => o.Fullname.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.AgentSearch, AgentInfoSetupModel>.Error(searchResult, ex));
            }
        }

        public ActionResult Save(AgentInfoSetupModel agentInfoSetup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    agentInfoSetup.LastModified = System.DateTime.Now;
                    var agentInfoSetupData = ToolsSetingBAL.SaveAgentInfo(agentInfoSetup);

                    TempData["IsAgentInfoSetupSaved"] = true;
                    TempData.Keep("IsAgentInfoSetupSaved");

                    return RedirectToAction("Index", "AgentInfoSetup", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsAgentInfoSetupSaved"] = false;
            TempData.Keep("IsAgentInfoSetupSaved");

            return RedirectToAction("Update", "AgentInfoSetup", new { area = "Admin", agentInfoSetupId = agentInfoSetup.ID });
        }

        public PartialViewResult AgentInfoSetupAddEdit(AgentInfoSetupModel agentInfoSetup)
        {
            return PartialView("_AgentInfoSetupAddEdit", agentInfoSetup);
        }


        [PageRightsAttribute(CurrentPageId = 17, Right = new isPageRight[] { isPageRight.HasDelete })]
        public JsonResult Delete(int agentId)
        {
            int retID = ToolsSetingBAL.DeleteAgent(agentId);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}