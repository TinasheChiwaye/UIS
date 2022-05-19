using Funeral.BAL;
using Funeral.Model;
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
    public class AddOnProductSetupController : BaseToolController
    {
        public AddOnProductSetupController():base(14) {
            this.dbPageId = 14;
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
        public ActionResult Index()
        {
            ViewBag.HasAccess = HasAccess;
            return View("Index");
        }
        public PartialViewResult List()
        {
            ViewBag.HasEditRight = HasEditRight;
            ViewBag.HasDeleteRight = HasDeleteRight;
            Model.Search.AddOnProductSearch search = new Model.Search.AddOnProductSearch();
            BindCompanyList("Search");
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;

            var searchResult = new SearchResult<Model.Search.AddOnProductSearch, AddonProductsModal>(search, new List<AddonProductsModal>(), o => o.ProductName.Contains(search.SarchText) || o.ProductDesc.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Tools/Views/AddOnProductSetup/_AddOnProductSetupList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.AddOnProductSearch search)
        {
            var searchResult = new SearchResult<Model.Search.AddOnProductSearch, AddonProductsModal>(search, new List<AddonProductsModal>(), o => o.ProductName.Contains(search.SarchText) || o.ProductDesc.Contains(search.SarchText));

            try
            {
                var addOnProductSetups = ToolsSetingBAL.GetAllAddonProductes(search.CompanyId);
                return Json(new SearchResult<Model.Search.AddOnProductSearch, AddonProductsModal>(search, addOnProductSetups, o => o.ProductName.Contains(search.SarchText) || o.ProductDesc.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.AddOnProductSearch, AddonProductsModal>.Error(searchResult, ex));
            }
        }
        [FuneralAuth(PageId = 14, Right = new Rights[] { Rights.HasAdd})]
        public PartialViewResult Add(AddonProductsModal addOnProductSetup)
        {
            addOnProductSetup.parlourid = ParlourId;
            BindCompanyList();
                       
            ModelState.Clear();
            return PartialView("~/Areas/Tools/Views/AddOnProductSetup/_AddOnProductSetupAddEdit.cshtml", addOnProductSetup);
        }
        [FuneralAuth(PageId = 14, Right = new Rights[] {Rights.HasEdit })]
        public PartialViewResult Edit(Guid productId)
        {
            var addOnProductSetup = ToolsSetingBAL.EditAddonProductbyID(productId);            
            addOnProductSetup.ProductCost = Convert.ToDecimal(addOnProductSetup.ProductCost.ToString("0.00"));
            addOnProductSetup.ProductCover = Convert.ToDecimal(addOnProductSetup.ProductCover.ToString("0.00"));
            BindCompanyList();
            return PartialView("~/Areas/Tools/Views/AddOnProductSetup/_AddOnProductSetupAddEdit.cshtml", addOnProductSetup);
        }
        public ActionResult Update(Guid productId)
        {
            ViewBag.ProductId = productId;
            return View("Index");
        }
        public ActionResult Save(AddonProductsModal addOnProductSetup)
        {
            ModelState["IsProductOngoing"].Errors.Clear();
            ModelState["IsProductLaybye"].Errors.Clear();

            try
             {  
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    addOnProductSetup.LastModified = System.DateTime.Now;
                    addOnProductSetup.ModifiedUser = formIdentity.Name;
                    addOnProductSetup.DateCreated = System.DateTime.Now;
                    addOnProductSetup.UserID = UserID.ToString();
                    //addOnProductSetup.Parlourid = ParlourId;
                    addOnProductSetup.LastModified = System.DateTime.Now;
                    addOnProductSetup.ModifiedUser = UserName;

                    Guid retID = ToolsSetingBAL.SaveAddonProductDetails(addOnProductSetup);                    

                    TempData["IsAddOnProductSetupSaved"] = true;
                    TempData.Keep("IsAddOnProductSetupSaved");

                    return RedirectToAction("Index", "AddOnProductSetup", new { area = "Tools" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsAddOnProductSetupSaved"] = false;
            TempData.Keep("IsAddOnProductSetupSaved");

            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string, new { area = "Tools" });
        }

        [FuneralAuthAttribute(PageId = 14, Right = new Rights[] { Rights.HasDelete})]
        public JsonResult Delete(Guid productId)
        {
            int retID = ToolsSetingBAL.DeleteAddonProduct(productId);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
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
    }
}