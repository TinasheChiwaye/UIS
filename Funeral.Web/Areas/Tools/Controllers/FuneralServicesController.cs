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
using System.Web.UI.WebControls;

namespace Funeral.Web.Areas.Tools.Controllers
{
    public class FuneralServicesController : BaseAdminController
    {

        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();

        public FuneralServicesController() : base(15)
        {
            ViewBag.VendorName = BindVenderName();
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

        public class DropdownComanClass
        {
            public int Text { get; set; }
            public string Value { get; set; }
        }
        public List<ListItem> BindVenderName()
        {

            List<ListItem> liList = new List<ListItem>();

            VendorModel[] obVendorNameModel = client.GetVendorNameByParlourId(ParlourId);

            foreach (VendorModel FuneralService in obVendorNameModel)
            {
                ListItem li = new ListItem();
                li.Text = FuneralService.VendorName;
                li.Value = FuneralService.pkiVendorID.ToString();
                liList.Add(li);
            }
            return liList;
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

            Model.Search.BaseSearch search = new Model.Search.BaseSearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;

            var searchResult = new SearchResult<Model.Search.BaseSearch, FuneralServiceManageModel>(search, new List<FuneralServiceManageModel>(), o => o.ServiceName.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Tools/Views/FuneralServices/_FuneralServicesList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, FuneralServiceManageModel>(search, new List<FuneralServiceManageModel>(), o => o.ServiceName.Contains(search.SarchText));

            try
            {
                List<FuneralServiceManageModel> FuneralServicesList = client.SelectFuneralManageServiceByParlID(ParlourId, search.PageSize, search.PageNum, search.SarchText, search.SortBy, search.SortOrder).ToList();

                return Json(new SearchResult<Model.Search.BaseSearch, FuneralServiceManageModel>(search, FuneralServicesList, o => o.ServiceName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, FuneralServiceManageModel>.Error(searchResult, ex));
            }
        }

        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasAdd })]
        public PartialViewResult Add(FuneralServiceManageModel FuneralServices)
        {
            FuneralServices.parlourid = ParlourId;
            ModelState.Clear();
            return PartialView("~/Areas/Tools/Views/FuneralServices/_FuneralServicesAddEdit.cshtml", FuneralServices);
        }


        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int ID)
        {
            var FuneralServices = client.SelectFuneralManageServiceByParlANdID(ID, ParlourId);
            FuneralServices.CostOfSale = Convert.ToDecimal(FuneralServices.CostOfSale.ToString("0.00"));
            FuneralServices.ServiceCost = Convert.ToDecimal(FuneralServices.ServiceCost.ToString("0.00"));
            return PartialView("~/Areas/Tools/Views/FuneralServices/_FuneralServicesAddEdit.cshtml", FuneralServices);
        }
        public ActionResult Update(int ID)
        {
            ViewBag.FuneralServicesId = ID;
            return View("Index");
        }
        public ActionResult Save(FuneralServiceManageModel FuneralServices)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    FuneralServices.LastModified = System.DateTime.Now;
                    FuneralServices.ModifiedUser = formIdentity.Name;

                    var agentInfoSetupData = client.SaveFuneralManageService(FuneralServices);

                    TempData["IsFuneralServicesSaved"] = true;
                    TempData.Keep("IsFuneralServicesSaved");

                    return RedirectToAction("Index", "FuneralServices", new { area = "Tools" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsFuneralServicesSaved"] = false;
            TempData.Keep("IsFuneralServicesSaved");

            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string, new { area = "Tools" });
        }

        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasDelete })]
        public JsonResult Delete(int ID)
        {
            int retID = ToolsSetingBAL.DeleteFuneralManageServiceById(ID);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}