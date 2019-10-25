using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Funeral.Web.Areas.Tools.Controllers
{
    public class PackageController : Admin.Controllers.BaseAdminController
    {
        public PackageController() : base(26)
        {
            this.dbPageId = 26;
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

        [PageRightsAttribute(CurrentPageId = 26, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            //ViewBag.HasAccess = HasAccess;
            if (ViewBag.HasAccess == true)
            {
                if (TempData["Package"] != null)
                    ViewBag.Package = TempData["Package"];
            }
            return View("Index");
        }

        [PageRightsAttribute(CurrentPageId = 26)]
        public PartialViewResult List()
        {
            //ViewBag.HasEditRight = HasEditRight;
            //ViewBag.HasDeleteRight = HasDeleteRight;

            Model.Search.PackageSearch search = new Model.Search.PackageSearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;

            var searchResult = new SearchResult<Model.Search.PackageSearch, PackageServiceModel>(search, new List<PackageServiceModel>(), o => o.PackageName.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Tools/Views/Package/_PackageList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.PackageSearch search)
        {
            var searchResult = new SearchResult<Model.Search.PackageSearch, PackageServiceModel>(search, new List<PackageServiceModel>(), o => o.PackageName.Contains(search.SarchText));

            try
            {
                var packages = FuneralPackageBAL.SelectPackage(ParlourId);
                return Json(new SearchResult<Model.Search.PackageSearch, PackageServiceModel>(search, packages, o => o.PackageName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.PackageSearch, PackageServiceModel>.Error(searchResult, ex));
            }
        }

        [PageRightsAttribute(CurrentPageId = 26, Right = new isPageRight[] { isPageRight.HasAdd })]
        [HttpPost]
        public PartialViewResult Add([System.Web.Http.FromBody]PackageServiceModel packageService)
        {
            packageService.ParlourId = ParlourId;
            packageService.ModifiedUser = User.Identity.Name;
            packageService.ServiceList = FuneralPackageBAL.GetAllQuotationServicesdt(ParlourId);
            ModelState.Clear();
            return PartialView("~/Areas/Tools/Views/Package/_PackageAddEdit.cshtml", packageService);
        }

        [PageRightsAttribute(CurrentPageId = 26, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int packageID)
        {
            PackageServiceModel package = new PackageServiceModel();

            var packageServices = FuneralPackageBAL.SelectPackageServiceByPackgeId(ParlourId, packageID);
            package.ServiceList = FuneralPackageBAL.GetAllQuotationServicesdt(ParlourId);
            package.ParlourId = ParlourId;
            package.pkiPackageID = packageID;
            package.fkiPackageID = packageServices.ElementAt(0).pkiPackageID;
            package.PackageName = packageServices.ElementAt(0).PackageName;
            package.ModifiedUser = User.Identity.Name;
            package.Total = Convert.ToDecimal(0);
            package.LastModified = DateTime.Now;

            return PartialView("~/Areas/Tools/Views/Package/_PackageAddEdit.cshtml", package);
        }
        public ActionResult Update(int pkiPackageID)
        {
            ViewBag.PackageID = pkiPackageID;
            return View("Index");
        }
        public ActionResult Save(PackageServiceModel package)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    package.LastModified = System.DateTime.Now;

                    var agentInfoSetupData = FuneralPackageBAL.SavePackageById(package);
                    if (agentInfoSetupData > 0)
                    {
                        if (package.fkiServiceID > 0)
                        {
                            PackageServiceModel model = new PackageServiceModel();
                            model.PackageName = package.PackageName;
                            model.ModifiedUser = this.UserName;
                            model.ParlourId = this.ParlourId;
                            model.fkiPackageID = agentInfoSetupData;
                            model.fkiServiceID = package.fkiServiceID;
                            var NewId = FuneralPackageBAL.SavePackageService(model);
                        }
                        TempData["IsPackageSaved"] = true;
                        TempData.Keep("IsPackageSaved");
                        TempData.Remove("PackageSaveError");
                        return RedirectToAction("Index", "Package", new { area = "Tools" });
                    }
                    else
                    {
                        TempData["PackageSaveError"] = "PackageName already exists";
                        TempData.Keep("PackageSaveError");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsPackageSaved"] = false;
            TempData.Keep("IsPackageSaved");

            TempData["Package"] = package;
            TempData.Keep("Package");

            return RedirectToAction("Index", "Package", new { Area = "Tools" });
        }


        [PageRightsAttribute(CurrentPageId = 26, Right = new isPageRight[] { isPageRight.HasDelete })]
        public JsonResult Delete(int packageId)
        {
            FuneralPackageBAL.DeletePackage(packageId, ParlourId);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}