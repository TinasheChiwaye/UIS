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

namespace Funeral.Web.Areas.Tools.Controllers
{
    public class SocietySetupController : BaseToolController
    {
        public SocietySetupController() : base(15)
        {
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

        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasAccess})]
        public ActionResult Index()
        {
            //ViewBag.HasAccess = HasAccess;
            return View("Index");
        }

        [PageRightsAttribute(CurrentPageId = 18)]
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

            return PartialView("~/Areas/Tools/Views/SocietySetup/_SocietySetupList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.BaseSearch search)
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
        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasAdd })]
        public PartialViewResult Add(SocietyModel SocietySetup)
        {
            SocietySetup.parlourid = ParlourId;
            ModelState.Clear();
            return PartialView("~/Areas/Tools/Views/SocietySetup/_SocietySetupAddEdit.cshtml", SocietySetup);
        }

        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int ID)
        {
            var SocietySetup = ToolsSetingBAL.EditSocietybyID(ID, ParlourId);
            return PartialView("~/Areas/Tools/Views/SocietySetup/_SocietySetupAddEdit.cshtml", SocietySetup);
        }
        public ActionResult Update(int ID)
        {
            ViewBag.SocietyId = ID;
            return View("Index");
        }
        public ActionResult Save(SocietyModel SocietySetup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    SocietySetup.LastModified = System.DateTime.Now;
                    SocietySetup.ModifiedUser = formIdentity.Name;

                    var agentInfoSetupData = ToolsSetingBAL.SaveSocietyDetails(SocietySetup);

                    TempData["IsSocietySetupSaved"] = true;
                    TempData.Keep("IsSocietySetupSaved");

                    return RedirectToAction("Index", "SocietySetup", new { area = "Tools" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsSocietySetupSaved"] = false;
            TempData.Keep("IsSocietySetupSaved");

            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string, new { area = "Tools" });
        }

        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasDelete})]
        public JsonResult Delete(int ID)
        {
            int retID = ToolsSetingBAL.DeleteSociety(ID);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}