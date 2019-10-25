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
    public class UnderwriterPremiumsController : BaseAdminController
    {
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
        public UnderwriterPremiumsController() : base(42)
        {
            ViewBag.CoverAge = BindNumbers();
            ViewBag.UnderwriterName = BindUndrewriterSetupName();
            ViewBag.PlanName = BindPlanList();
            ViewBag.RolePlayerType = BindRolePlayerType();
            this.dbPageId = 42;
        }
        public class DropdownComanClass
        {
            public int Text { get; set; }
            public string Value { get; set; }
        }
        public List<ListItem> BindNumbers()
        {
            int Numbers = 100;
            List<ListItem> liList = new List<ListItem>();


            for (int i = 0; i <= Numbers; i++)
            {
                ListItem li = new ListItem();
                li.Text = i.ToString();
                li.Value = i.ToString();
                liList.Add(li);

            }

            return liList;
        }
        public List<ListItem> BindUndrewriterSetupName()
        {
            UnderwriterSetupModel[] objUndrewriterSetupNameModel = client.GetUnderwriterSetupNameByParlourId(ParlourId);

            List<ListItem> liList = new List<ListItem>();
            foreach (UnderwriterSetupModel UnderwriterName in objUndrewriterSetupNameModel)
            {
                ListItem li = new ListItem();
                li.Text = UnderwriterName.UnderwriterName;
                li.Value = UnderwriterName.PkiUnderWriterSetupId.ToString();
                liList.Add(li);
            }

            return liList;
        }
        public List<ListItem> BindPlanList()
        {
            PlanModel[] model = client.GetAllPlansList(ParlourId);

            List<ListItem> liList = new List<ListItem>();
            foreach (PlanModel Planlist in model)
            {
                ListItem li = new ListItem();
                li.Text = Planlist.PlanName;
                li.Value = Planlist.pkiPlanID.ToString();
                liList.Add(li);

            }           

            return liList;
        }
        public List<ListItem> BindRolePlayerType()
        {
            RolePlayerModel[] model = client.GetAllRolePlayer(ParlourId);

            List<ListItem> liList = new List<ListItem>();
            foreach (RolePlayerModel AllRolePlayer in model)
            {
                ListItem li = new ListItem();
                li.Text = AllRolePlayer.Name;
                li.Value = AllRolePlayer.Name;
                liList.Add(li);

            }
            return liList;
        }
        /// <summary>
        /// Index method to Display List
        /// </summary>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 42, Right = new isPageRight[] { isPageRight.HasDelete })]
        public ActionResult Index()
        {
            //ViewBag.HasAccess = HasAccess;
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

        public ActionResult Save(UnderwriterPremiumModel underwriterPremiumModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    underwriterPremiumModel.LastModified = System.DateTime.Now;
                    underwriterPremiumModel.ModifiedUser = formIdentity.Name;


                    var agentInfoSetupData = client.SaveUnderwriterPremium(underwriterPremiumModel);

                    TempData["IsUnderwriterPremiumSaved"] = true;
                    TempData.Keep("IsUnderwriterPremiumSaved");

                    return RedirectToAction("Index", "UnderwriterPremiums", new { area = "Tools" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsUnderwriterPremiumSaved"] = false;
            TempData.Keep("IsUnderwriterPremiumSaved");

            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string, new { area = "Tools" });
        }

        /// <summary>
        /// list method to display list of Underwriter
        /// </summary>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 42)]
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

            var searchResult = new SearchResult<Model.Search.BaseSearch, UnderwriterPremiumModel>(search, new List<UnderwriterPremiumModel>(), o => o.UnderwriterName.Contains(search.SarchText)); // || o.ContactPerson.Contains(search.SarchText) || o.ContactEmail.ToString().Contains(search.SarchText)
            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;
            return PartialView("~/Areas/Tools/Views/UnderwriterPremiums/_UnderwriterPremiumList.cshtml", search);
        }
        /// <summary>
        /// method to search data as per user input
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult SearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, UnderwriterPremiumModel>(search, new List<UnderwriterPremiumModel>(), o => o.UnderwriterName.Contains(search.SarchText));// || o.ContactPerson.Contains(search.SarchText) || o.ContactEmail.ToString().Contains(search.SarchText)
            try
            {
                var underwriterPremiumModelList = UnderwriterPremiumBAL.SelectAllUnderwriterPremiumByParlourId(ParlourId, search.PageSize, search.PageNum, "", search.SortBy, search.SortOrder);
                return Json(new SearchResult<Model.Search.BaseSearch, UnderwriterPremiumModel>(search, underwriterPremiumModelList, o => o.UnderwriterName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, UnderwriterPremiumModel>.Error(searchResult, ex));
            }
        }

        /// <summary>
        /// Add method to display page
        /// </summary>
        /// <param name="quotationModel"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 42, Right = new isPageRight[] { isPageRight.HasAdd})]
        public PartialViewResult Add()
        {   
            return PartialView("~/Areas/Tools/Views/UnderwriterPremiums/_UnderwriterPremiumAddEdit.cshtml");
        }

        [PageRightsAttribute(CurrentPageId = 42, Right = new isPageRight[] { isPageRight.HasEdit})]
        public PartialViewResult Edit(int ID)
        {
            var SocietySetup = UnderwriterPremiumBAL.EditUnderwriterPremiumbyID(ID, ParlourId);
            return PartialView("~/Areas/Tools/Views/UnderwriterPremiums/_UnderwriterPremiumAddEdit.cshtml", SocietySetup);
        }

        [PageRightsAttribute(CurrentPageId = 42, Right = new isPageRight[] { isPageRight.HasDelete})]
        public JsonResult Delete(int ID)
        {
            int retID = UnderwriterPremiumBAL.DeleteUnderwriterPremium(ID, UserName);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}