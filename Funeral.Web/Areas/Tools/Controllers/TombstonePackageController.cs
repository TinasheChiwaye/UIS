using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Areas.Admin.Controllers;
using Funeral.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Funeral.Web.Areas.Tools.Controllers
{

    public class TombstonePackageController : BaseToolController
    {
        //public int pkiPackageID
        //{
        //    get
        //    {
        //        if (TempData["PackageId"] != null)
        //        {
        //            return Convert.ToInt32(TempData["PackageId"]);
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    set
        //    {
        //        TempData["PackageId"] = value;
        //    }
        //}

        public TombstonePackageController() : base(15)
        {
            ViewBag.ServiceName = BindFuneralServiceList();
            dbPageId = 15;
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
        public List<ListItem> BindFuneralServiceList()
        {
            List<ListItem> liList = new List<ListItem>();
            List<QuotationServicesModel> objQuoSer = QuotationBAL.GetAllQuotationServices(ParlourId);
            foreach (QuotationServicesModel FuneralService in objQuoSer)
            {
                ListItem li = new ListItem();
                li.Text = FuneralService.ServiceName;
                li.Value = FuneralService.pkiServiceID.ToString();
                liList.Add(li);
            }
            return liList;
        }

        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            //ViewBag.HasAccess = HasAccess;
            return View();
        }

        [PageRightsAttribute(CurrentPageId = 15)]
        public PartialViewResult ListPackage()
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

            List<TombstonePackageModel> TombstonePackageList = TombstonePackageBAL.SelectAllPackage(ParlourId);

            var searchResult = new SearchResult<Model.Search.BaseSearch, TombstonePackageModel>(search, TombstonePackageList, o => o.ServiceName.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Tools/Views/TombstonePackage/_TombstonePackageList.cshtml", search);
        }
        [PageRightsAttribute(CurrentPageId = 15)]
        public ActionResult ListServices(int pkiPackageID)
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
            //const int testId = pkiPackageID;
            List<TombstonePackageModel> TombstonePackageList = TombstonePackageBAL.SelectPackageServiceByPackgeId(ParlourId, pkiPackageID);

            var searchResult = new SearchResult<Model.Search.BaseSearch, TombstonePackageModel>(search, TombstonePackageList, o => o.ServiceName.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;
            //ViewBag.pkiPackageID = pkiPackageID;
            Session["pkiPackageID"] = pkiPackageID;
            //(int)Session["pkiPackageID"] = pkiPackageID;

            // return Json(new SearchResult<Model.Search.BaseSearch, TombstonePackageModel>(search, TombstonePackageList, o => o.ServiceName.Contains(search.SarchText)));
            return PartialView("~/Areas/Tools/Views/TombstonePackage/_TombstonePackageList.cshtml", search);

        }

        //public ActionResult SearchData1(Model.Search.BaseSearch search, int pkiPackageId)
        //{
        //    var searchResult = new SearchResult<Model.Search.BaseSearch, TombstonePackageModel>(search, new List<TombstonePackageModel>(), o => o.ServiceName.Contains(search.SarchText));

        //    try
        //    {
        //        List<TombstonePackageModel> TombstonePackageList = client.GetTombstonePackageServiceByPackgeId(ParlourId, pkiPackageId).ToList();

        //        var searchResult1 = new SearchResult<Model.Search.BaseSearch, TombstonePackageModel>(search, TombstonePackageList, o => o.ServiceName.Contains(search.SarchText));

        //        return Json(new SearchResult<Model.Search.BaseSearch, TombstonePackageModel>(search, TombstonePackageList, o => o.PackageName.Contains(search.SarchText)));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(WebApiResult<Model.Search.BaseSearch, TombstonePackageModel>.Error(searchResult, ex));
        //    }
        //}        
        public ActionResult SearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, TombstonePackageModel>(search, new List<TombstonePackageModel>(), o => o.ServiceName.Contains(search.SarchText));

            //int a = 1;

            try
            {
                if (Convert.ToInt32(Session["pkiPackageID"]) == 0)
                {
                    List<TombstonePackageModel> TombstonePackageList = TombstonePackageBAL.SelectAllPackage(ParlourId).ToList();

                    searchResult = new SearchResult<Model.Search.BaseSearch, TombstonePackageModel>(search, TombstonePackageList, o => o.ServiceName.Contains(search.SarchText));

                    return Json(new SearchResult<Model.Search.BaseSearch, TombstonePackageModel>(search, TombstonePackageList, o => o.ServiceName.Contains(search.SarchText)));
                }
                else
                {
                    //var pkiPackageID = ViewBag.pkiPackageID;
                    //var pkiPackageID  = Session["pkiPackageID"] as Int32?; ;
                    int pkiPackageID = Convert.ToInt32(Session["pkiPackageID"]);

                    List<TombstonePackageModel> TombstonePackageList = TombstonePackageBAL.SelectPackageServiceByPackgeId(ParlourId, pkiPackageID).ToList();

                    var searchResult1 = new SearchResult<Model.Search.BaseSearch, TombstonePackageModel>(search, TombstonePackageList, o => o.ServiceName.Contains(search.SarchText));
                    Session.Clear();
                    return Json(new SearchResult<Model.Search.BaseSearch, TombstonePackageModel>(search, TombstonePackageList, o => o.ServiceName.Contains(search.SarchText)));
                }
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, TombstonePackageModel>.Error(searchResult, ex));
            }
        }

        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasAdd })]
        public PartialViewResult Add(TombstonePackageModel TombstonePackage)
        {
            TombstonePackage.ParlourId = ParlourId;
            ModelState.Clear();
            return PartialView("~/Areas/Tools/Views/TombstonePackage/_TombstonePackageAddEdit.cshtml", TombstonePackage);
        }

        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int pkiPackageID)
        {
            TombstonePackageModel model = new TombstonePackageModel();

            var SocietySetup = TombstonePackageBAL.SelectPackageServiceByPackgeId(ParlourId, pkiPackageID);
            model.pkiPackageID = SocietySetup[0].pkiPackageID;
            model.PackageName = SocietySetup[0].PackageName;
            model.IsActive = SocietySetup[0].IsActive;
            model.ModifiedUser = SocietySetup[0].ModifiedUser;
            model.ServiceCost = SocietySetup[0].ServiceCost;
            model.ServiceDesc = SocietySetup[0].ServiceDesc;
            model.ServiceName = SocietySetup[0].ServiceName;
            model.fkiServiceID = SocietySetup[0].fkiServiceID;
            model.pkiPackageServiceID = SocietySetup[0].pkiPackageServiceID;

            //ListServices(pkiPackageID);
            //SearchData1(pkiPackageID);

            return PartialView("~/Areas/Tools/Views/TombstonePackage/_TombstonePackageAddEdit.cshtml", model);
        }
        public ActionResult Update(int ID)
        {
            ViewBag.SocietyId = ID;
            return View("Index");
        }
        public ActionResult Save(TombstonePackageModel SavePackage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;

                    SavePackage.PackageName = SavePackage.PackageName;
                    SavePackage.LastModified = System.DateTime.Now;
                    SavePackage.ModifiedUser = formIdentity.Name;
                    SavePackage.ParlourId = SavePackage.ParlourId;
                    SavePackage.IsActive = true;
                    TombstonePackageBAL.SavePackage(SavePackage);
                    TempData["IsTombstonePackageSaved"] = true;
                    TempData.Keep("IsTombstonePackageSaved");

                    //SaveServices(SavePackage);
                    return RedirectToAction("Index", "TombstonePackage", new { area = "Tools" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsTombstonePackageSaved"] = false;
            TempData.Keep("IsTombstonePackageSaved");

            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string, new { area = "Tools" });

        }


        //public ActionResult SaveServices(TombstonePackageModel SaveService)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            FormsIdentity formIdentity = (FormsIdentity)User.Identity;
        //            SaveService.LastModified = System.DateTime.Now;
        //            SaveService.ModifiedUser = formIdentity.Name;
        //            SaveService.IsActive = true;

        //            SaveService.PackageName = SaveService.PackageName;
        //            SaveService.ParlourId = SaveService.ParlourId;
        //            SaveService.fkiPackageID = SaveService.fkiPackageID;
        //            SaveService.fkiServiceID = SaveService.fkiServiceID;
        //            client.SaveTombstonePackageService(SaveService);

        //            TempData["IsTombstoneServiceSaved"] = true;
        //            TempData.Keep("IsTombstoneServiceSaved");

        //            return RedirectToAction("Index", "TombstoneService", new { area = "Tools" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    TempData["IsTombstoneServiceSaved"] = false;
        //    TempData.Keep("IsTombstoneServiceSaved");

        //    return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string, new { area = "Tools" });
        //}

        [PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasDelete })]
        public JsonResult Delete(int pkiPackageID)
        {
            TombstonePackageBAL.DeleteTombstonePackage(pkiPackageID);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}