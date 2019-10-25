using Funeral.BAL;
using Funeral.Model;
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
    public class PackageServiceController : Controller
    {
        public Guid ParlourId
        {
            get
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string[] strData = ticket.UserData.Split('|');
                    if (strData.Count() > 0)
                        return new Guid(string.IsNullOrEmpty(strData[0]) ? "00000000-0000-0000-0000-000000000000" : strData[0]);
                    else
                        return new Guid("00000000-0000-0000-0000-000000000000");
                }
                else
                {
                    return new Guid("00000000-0000-0000-0000-000000000000");
                }
            }

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

        public PartialViewResult List(int packageId)
        {
            Model.Search.PackageServiceSearch search = new Model.Search.PackageServiceSearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;
            search.PackageId = packageId;

            var searchResult = new SearchResult<Model.Search.PackageServiceSearch, Service>(search, new List<Service>(), o => o.ServiceName.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Tools/Views/PackageService/_PackageServiceList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.PackageServiceSearch search)
        {
            var searchResult = new SearchResult<Model.Search.PackageServiceSearch, Service>(search, new List<Service>(), o => o.ServiceName.Contains(search.SarchText));

            try
            {
                var packageServices = FuneralPackageBAL.SelectPackageServiceByPackgeAndService(ParlourId, search.PackageId);
                return Json(new SearchResult<Model.Search.PackageServiceSearch, Service>(search, packageServices, o => o.ServiceName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.PackageServiceSearch, Service>.Error(searchResult, ex));
            }
        }
        public PartialViewResult Add(BranchModel branchSetup)
        {
            branchSetup.Parlourid = ParlourId;
            ModelState.Clear();
            return PartialView("~/Areas/Tools/Views/BranchSetup/_BranchSetupAddEdit.cshtml", branchSetup);
        }
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
        public JsonResult Delete(int packageId, int serviceId)
        {
            FuneralPackageBAL.DeletePackageServiceByPackageAndService(packageId, serviceId);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}