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
    public class MenuController : BaseAdminController
    {
        public MenuController() : base(10)
        {
            this.dbPageId = 10;
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
        public List<KeyName> GetApplications()
        {
            List<KeyName> keyNames = new List<KeyName>();
            keyNames.Add(new KeyName { Key = "1", Name = "UIS" });

            return keyNames;
        }
        public List<KeyName> GetMenuLevels()
        {
            List<KeyName> keyNames = new List<KeyName>();
            keyNames.Add(new KeyName { Key = "0", Name = "Level 0" });
            keyNames.Add(new KeyName { Key = "1", Name = "Level 1" });
            keyNames.Add(new KeyName { Key = "2", Name = "Level 2" });

            return keyNames;
        }
        public List<KeyName> GetParentForm()
        {
            List<KeyName> keyNames = new List<KeyName>();

            return keyNames;
        }

        [PageRightsAttribute(CurrentPageId = 10, Right = new isPageRight[] { isPageRight.HasAccess})]
        public ActionResult Index()
        {
            ViewBag.HasAccess = HasAccess;
            return View("Index");
        }

        [PageRightsAttribute(CurrentPageId = 10)]
        public PartialViewResult List()
        {
            ViewBag.HasEditRight = HasEditRight;
            ViewBag.HasDeleteRight = HasDeleteRight;

            Model.Search.MenuSearch search = new Model.Search.MenuSearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;

            var searchResult = new SearchResult<Model.Search.MenuSearch, RightsModel>(search, new List<RightsModel>(), o => o.MenuName.Contains(search.SarchText) || o.Description.Contains(search.SarchText) || o.FormName.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Tools/Views/Menu/_MenuList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.MenuSearch search)
        {
            var searchResult = new SearchResult<Model.Search.MenuSearch, RightsModel>(search, new List<RightsModel>(), o => o.MenuName.Contains(search.SarchText) || o.Description.Contains(search.SarchText) || o.FormName.Contains(search.SarchText));

            try
            {
                var menuRights = RightsBAL.tblRightGetAll();
                return Json(new SearchResult<Model.Search.MenuSearch, RightsModel>(search, menuRights, o => o.MenuName.Contains(search.SarchText) || o.Description.Contains(search.SarchText) || o.FormName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.MenuSearch, RightsModel>.Error(searchResult, ex));
            }
        }

        [PageRightsAttribute(CurrentPageId = 10, Right = new isPageRight[] { isPageRight.HasAdd})]
        public ActionResult Add()
        {
            ViewBag.Applications = GetApplications();
            ViewBag.MenuLevels = GetMenuLevels();
            ViewBag.ParentRoles = GetParentForm();

            var menuRight = new RightsModel();
            ModelState.Clear();
            return View("~/Areas/Tools/Views/Menu/MenuAddEdit.cshtml", menuRight);
        }

        [PageRightsAttribute(CurrentPageId = 10, Right = new isPageRight[] { isPageRight.HasEdit})]
        public PartialViewResult Edit(int menuId)
        {
            ViewBag.Applications = GetApplications();
            ViewBag.MenuLevels = GetMenuLevels();
            ViewBag.ParentRoles = GetParentForm();

            var menuRight = RightsBAL.SelecttblRightById(menuId);
            return PartialView("~/Areas/Tools/Views/Menu/MenuAddEdit.cshtml", menuRight);
        }
        public ActionResult Update(int menuId)
        {
            ViewBag.MenuId = menuId;
            return View("Index");
        }
        public ActionResult Save(RightsModel menuRight)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;

                    if (menuRight.ID == 0)
                    {
                        menuRight.CreateBy = UserName;
                        menuRight.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        menuRight.UpdateBy = UserName;
                        menuRight.UpdateDate = DateTime.Now;
                    }
                    
                    menuRight.Formkey=(menuRight.Formkey== null) ? "" : menuRight.Formkey;
                    menuRight.IconClassName = (menuRight.IconClassName == null) ? "" : menuRight.IconClassName;
                    menuRight.RoleId = menuRight.ParentRightId = (menuRight.ParentRightId == null) ? "0" : menuRight.ParentRightId;
                    var menuRightSaveResult = RightsBAL.SavetblRight(menuRight);

                    TempData["IsMenuRightSaved"] = true;
                    TempData.Keep("IsMenuRightSaved");

                    return RedirectToAction("Index", "Menu", new { area = "Tools" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsMenuRightSaved"] = false;
            TempData.Keep("IsMenuRightSaved");

            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string, new { area = "Tools" });
        }
        public ActionResult PreviousAction()
        {
            return Redirect(Request.UrlReferrer.ToString());
        }

        //[FuneralAuthAttribute(PageId = 10, Right = new Rights[] { Rights.HasDelete})]
        //public JsonResult Delete(int productId)
        //{
        //    int retID = ToolsSetingBAL.DeleteBranch(branchId);
        //    var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
    }
}