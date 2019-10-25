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
    public class CustomManagementController : BaseAdminController
    {
        public CustomManagementController() :base(28){
            this.dbPageId = 28;
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

        [PageRightsAttribute(CurrentPageId = 28, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            //ViewBag.HasAccess = HasAccess;
            return View();
        }

        [HttpPost]
        [PageRightsAttribute(CurrentPageId = 28)]
        public PartialViewResult List([System.Web.Http.FromUri]int customType,[System.Web.Http.FromBody] Model.Search.CustomManagementSearch search)
        {
            //ViewBag.HasEditRight = HasEditRight;
            //ViewBag.HasDeleteRight = HasDeleteRight;
            
            var searchResult = new SearchResult<Model.Search.CustomManagementSearch, CustomDetails>(search, new List<CustomDetails>(), o => o.Name.Contains(search.SarchText));

            ViewBag.CustomType = customType;

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Tools/Views/CustomManagement/_CustomManagementList.cshtml", search);
        }

        [HttpPost]
        public ActionResult SearchData([System.Web.Http.FromUri]int customType, Model.Search.CustomManagementSearch search)
        {
            var searchResult = new SearchResult<Model.Search.CustomManagementSearch, CustomDetails>(search, new List<CustomDetails>(), o => o.Name.Contains(search.SarchText));

            try
            {
                var customManagement = CustomDetailsBAL.GetAllCustomDetailsByParlourId(ParlourId, customType);
                return Json(new SearchResult<Model.Search.CustomManagementSearch, CustomDetails>(search, customManagement, o => o.Name.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.CustomManagementSearch, CustomDetails>.Error(searchResult, ex));
            }
        }

        [PageRightsAttribute(CurrentPageId = 28, Right = new isPageRight[] { isPageRight.HasAdd })]
        public PartialViewResult Add(CustomDetails customManagement)
        {   
            customManagement.ParlourId = ParlourId;
            ModelState.Clear();
            return PartialView("~/Areas/Tools/Views/CustomManagement/_CustomManagementAddEdit.cshtml", customManagement);
        }

        [PageRightsAttribute(CurrentPageId = 28, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int id, int customType = 0)
        {
            var customManagement = CustomDetailsBAL.GetCustomDetails(id, this.ParlourId, customType);
            return PartialView("~/Areas/Tools/Views/CustomManagement/_CustomManagementAddEdit.cshtml", customManagement);
        }

        public ActionResult Update(int Id)
        {
            ViewBag.Id = Id;
            return View("Index");
        }

        public ActionResult Save(CustomDetails customManagement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    customManagement.CreatedDate = System.DateTime.Now;
                    customManagement.CreatedBy = UserID;
                    customManagement.ParlourId = this.ParlourId;

                    if (customManagement.Id <= 0) {
                        CustomDetailsBAL.CustomDetailsSave(customManagement);
                    }
                    else
                    {
                        CustomDetailsBAL.CustomDetailsUpdate(customManagement);
                    }
                    
                    TempData["IsCustomManagementSaved"] = true;
                    TempData.Keep("IsCustomManagementSaved");

                    return RedirectToAction("Index", "CustomManagement", new { area = "Tools" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsCustomManagementSaved"] = false;
            TempData.Keep("IsCustomManagementSaved");

            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string, new { area = "Tools" });
        }

        [PageRightsAttribute(CurrentPageId = 28, Right = new isPageRight[] { isPageRight.HasDelete })]
        [HttpGet]
        public JsonResult Delete(int customTypeId, int customType)
        {
            CustomDetails companyDetails = new CustomDetails();
            companyDetails.Id = customTypeId;
            companyDetails.CustomType = (CustomDetailsEnums.CustomDetailsType)customType;
            companyDetails.ParlourId = ParlourId;
            CustomDetailsBAL.CustomDetailsDelete(companyDetails);

            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}