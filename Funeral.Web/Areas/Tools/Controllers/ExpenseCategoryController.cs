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
using static Funeral.Web.Areas.Admin.Controllers.MembersController;

namespace Funeral.Web.Areas.Tools.Controllers
{
    public class ExpenseCategoryController : BaseAdminController
    {
        public ExpenseCategoryController() : base(18)
        {
            this.dbPageId = 18;
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

        [PageRightsAttribute(CurrentPageId = 18, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            //ViewBag.HasAccess = HasAccess;
            return View("Index");
        }

        [PageRightsAttribute(CurrentPageId = 18)]
        public PartialViewResult List()
        {
            //ViewBag.HasEditRight = HasEditRight;
            //ViewBag.HasDeleteRight = HasDeleteRight;

            Model.Search.ExpenseCategorySearch search = new Model.Search.ExpenseCategorySearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;

            var searchResult = new SearchResult<Model.Search.ExpenseCategorySearch, ExpensesModel>(search, new List<ExpensesModel>(), o => o.Category.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Tools/Views/ExpenseCategory/_ExpenseCategoryList.cshtml", search);
        }

        public ActionResult SearchData(Model.Search.ExpenseCategorySearch search)
        {
            var searchResult = new SearchResult<Model.Search.ExpenseCategorySearch, ExpensesModel>(search, new List<ExpensesModel>(), o => o.Category.Contains(search.SarchText));

            try
            {
                var expenseCategories = ToolsSetingBAL.GetAllExpenseses(ParlourId);
                return Json(new SearchResult<Model.Search.ExpenseCategorySearch, ExpensesModel>(search, expenseCategories, o => o.Category.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.ExpenseCategorySearch, ExpensesModel>.Error(searchResult, ex));
            }
        }

        [PageRightsAttribute(CurrentPageId = 18, Right = new isPageRight[] { isPageRight.HasAdd})]
        public PartialViewResult Add(ExpensesModel expense)
        {
            expense.parlourid = ParlourId;
            ModelState.Clear();
            return PartialView("~/Areas/Tools/Views/ExpenseCategory/_ExpenseCategoryAddEdit.cshtml", expense);
        }

        [PageRightsAttribute(CurrentPageId = 18, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int expenseCategoryID)
        {
            var expenseCategory = ToolsSetingBAL.EditExpensesbyID(expenseCategoryID, ParlourId);
            return PartialView("~/Areas/Tools/Views/ExpenseCategory/_ExpenseCategoryAddEdit.cshtml", expenseCategory);
        }

        public ActionResult Update(int expenseCategoryID)
        {
            ViewBag.ExpenseCategoryID = expenseCategoryID;
            return View("Index");
        }

        public ActionResult Save(ExpensesModel expense)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    expense.LastModified = System.DateTime.Now;
                    expense.ModifiedUser = formIdentity.Name;

                    var expenseCategory = ToolsSetingBAL.SaveExpensesDetails(expense);

                    TempData["IsExpenseCategorySaved"] = true;
                    TempData.Keep("IsExpenseCategorySaved");

                    return RedirectToAction("Index", "ExpenseCategory", new { area = "Tools" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsExpenseCategorySaved"] = false;
            TempData.Keep("IsExpenseCategorySaved");

            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string, new { area = "Tools" });
        }

        [PageRightsAttribute(CurrentPageId = 18, Right = new isPageRight[] { isPageRight.HasDelete })]
        public JsonResult Delete(int expenseCategoryID)
        {
            int retID = ToolsSetingBAL.DeleteExpenses(expenseCategoryID);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}