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

namespace Funeral.Web.Areas.Tools.Controllers
{
    public class UnderwriterSetupController : BaseToolController
    {
        public UnderwriterSetupController() : base(43)
        {
            this.dbPageId = 43;
        }

        /// <summary>
        /// Index method to Display List
        /// </summary>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 43, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// this will display the count of list to display
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// list method to display list of Underwriter
        /// </summary>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 43)]
        public PartialViewResult List()
        {
            //ViewBag.HasEditRight = HasEditRight;
            //ViewBag.HasDeleteRight = HasDeleteRight;

            Model.Search.BaseSearch search = new Model.Search.BaseSearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;

            var searchResult = new SearchResult<Model.Search.BaseSearch, UnderwriterSetupModel>(search, new List<UnderwriterSetupModel>(), o => o.UnderwriterName.Contains(search.SarchText) || o.ContactPerson.Contains(search.SarchText) || o.ContactEmail.ToString().Contains(search.SarchText));
            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;
            return PartialView("~/Areas/Tools/Views/UnderwriterSetup/_UnderWriterList.cshtml", search);
        }
        /// <summary>
        /// method to search data as per user input
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult SearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, UnderwriterSetupModel>(search, new List<UnderwriterSetupModel>(), o => o.UnderwriterName.Contains(search.SarchText) || o.ContactPerson.Contains(search.SarchText) || o.ContactEmail.ToString().Contains(search.SarchText));
            try
            {
                var underwriterSetupModelList = UnderwriterSetupBAL.SelectAllUnderwriterSetupByParlourId(ParlourId, search.PageSize, search.PageNum, "", search.SortBy, search.SortOrder);
                return Json(new SearchResult<Model.Search.BaseSearch, UnderwriterSetupModel>(search, underwriterSetupModelList, o => o.UnderwriterName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, UnderwriterSetupModel>.Error(searchResult, ex));
            }
        }

        /// <summary>
        /// Add method to display page
        /// </summary>
        /// <param name="quotationModel"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 43, Right = new isPageRight[] { isPageRight.HasAdd})]
        public PartialViewResult Add(UnderwriterSetupModel underwriterSetupModel)
        {
            underwriterSetupModel.Parlourid = ParlourId;
            ModelState.Clear();
            return PartialView("~/Areas/Tools/Views/UnderwriterSetup/_UnderwriterSetupAddEdit.cshtml", underwriterSetupModel);

        }

        [HttpPost]
        public ActionResult Save(UnderwriterSetupModel underwriterSetupModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    underwriterSetupModel.CreatedBy = UserName;
                    underwriterSetupModel.CreateddDate = System.DateTime.Now;
                    underwriterSetupModel.ModifiedDate = System.DateTime.Now;
                    underwriterSetupModel.ModifiedBy = UserName;
                    underwriterSetupModel.Parlourid = ParlourId;
                    var underwriterModelData = UnderwriterSetupBAL.SaveUnderwriterSetup(underwriterSetupModel);

                    TempData["IsSocietySetupSaved"] = true;
                    TempData.Keep("IsSocietySetupSaved");

                    return RedirectToAction("Index", "UnderwriterSetup", new { area = "Tools" });
                }
                else {
                    //return RedirectToAction("Add", "UnderwriterSetup", new { area = "Tools" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsSocietySetupSaved"] = false;
            TempData.Keep("IsSocietySetupSaved");

            return RedirectToAction("Add", "UnderwriterSetup", new { area = "Tools" });
        }

        [PageRightsAttribute(CurrentPageId = 43, Right = new isPageRight[] { isPageRight.HasEdit})]

        public PartialViewResult Edit(int PkiUnderWriterSetupId)
        {
            var underwriterSetupModel = UnderwriterSetupBAL.EditUnderwriterSetupbyID(PkiUnderWriterSetupId, ParlourId);
            return PartialView("~/Areas/Tools/Views/UnderwriterSetup/_UnderwriterSetupAddEdit.cshtml", underwriterSetupModel);
        }

        public ActionResult Update(Guid PkiUnderWriterSetupId)
        {
            ViewBag.PkiUnderWriterSetupId = PkiUnderWriterSetupId;
            return View("Index");
        }

        [PageRightsAttribute(CurrentPageId = 43, Right = new isPageRight[] { isPageRight.HasDelete})]
        public JsonResult Delete(int PkiUnderWriterSetupId)
        {
            int returnID = UnderwriterSetupBAL.DeleteUnderwriterSetupByID(PkiUnderWriterSetupId,UserName);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}