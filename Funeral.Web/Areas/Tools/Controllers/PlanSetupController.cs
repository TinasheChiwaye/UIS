using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.Areas.Admin.Controllers;
using Funeral.Web.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Funeral.Web.Areas.Tools.Controllers
{
    public class PlanSetupController : BaseAdminController
    {
        public PlanSetupController() : base(15)
        {
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
        public ActionResult Index()
        {
            ViewBag.HasAccess = HasAccess;
            return View("Index");
        }
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

            var searchResult = new SearchResult<Model.Search.BaseSearch, PlanModel>(search, new List<PlanModel>(), o => o.PlanName.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Tools/Views/PlanSetup/_PlanSetupList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, PlanModel>(search, new List<PlanModel>(), o => o.PlanName.Contains(search.SarchText));

            try
            {
                var PlanList = ToolsSetingBAL.GetAllPlans(ParlourId, search.PageSize, search.PageNum, "", search.SortBy, search.SortOrder);
                return Json(new SearchResult<Model.Search.BaseSearch, PlanModel>(search, PlanList, o => o.PlanName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, PlanModel>.Error(searchResult, ex));
            }
        }
        [FuneralAuth(PageId = 15, Right = new Rights[] { Rights.HasAdd })]
        public PartialViewResult Add()
        {
            PlanModel planModel = new PlanModel();
            List<PlanCreator> planCreators = new List<PlanCreator>();
            planCreators.Add(new PlanCreator { });
            planModel.planCreators = planCreators;
            

            FormsIdentity id = (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;
            string[] strData = ticket.UserData.Split('|');
            if (strData.Count() > 0)
                planModel.parlourid = new Guid(string.IsNullOrEmpty(strData[0]) ? "00000000-0000-0000-0000-000000000000" : strData[0]);
            else
                planModel.parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            planModel.UnderwriterList = CommonBAL.GetUnderwriterList(planModel.parlourid).ToList();
            return PartialView("~/Areas/Tools/Views/PlanSetup/_PlanSetupAddEdit.cshtml", planModel);
        }
        [FuneralAuth(PageId = 15, Right = new Rights[] { Rights.HasEdit })]
        public PartialViewResult Edit(int ID)
        {
            var planSetup = ToolsSetingBAL.EditPlanbyID(ID, ParlourId);
            planSetup.AdminSplit = Convert.ToDecimal(planSetup.AdminSplit.ToString("0.00"));
            planSetup.AdultCover = Convert.ToDecimal(planSetup.AdultCover.ToString("0.00"));
            planSetup.AgentSplit = Convert.ToDecimal(planSetup.AgentSplit.ToString("0.00"));
            planSetup.CashPayout = Convert.ToDecimal(planSetup.CashPayout.ToString("0.00"));
            planSetup.ChildCover = Convert.ToDecimal(planSetup.ChildCover.ToString("0.00"));
            planSetup.CompanySplit = Convert.ToDecimal(planSetup.CompanySplit.ToString("0.00"));
            planSetup.Cover = Convert.ToDecimal(planSetup.Cover.ToString("0.00"));
            planSetup.Cover0to5year = Convert.ToDecimal(planSetup.Cover0to5year.ToString("0.00"));
            planSetup.Cover14to21year = Convert.ToDecimal(planSetup.Cover14to21year.ToString("0.00"));
            planSetup.Cover22to40year = Convert.ToDecimal(planSetup.Cover22to40year.ToString("0.00"));
            planSetup.Cover41to59year = Convert.ToDecimal(planSetup.Cover41to59year.ToString("0.00"));
            planSetup.Cover60to65year = Convert.ToDecimal(planSetup.Cover60to65year.ToString("0.00"));
            planSetup.Cover66to75year = Convert.ToDecimal(planSetup.AdminSplit.ToString("0.00"));
            planSetup.Cover6to13year = Convert.ToDecimal(planSetup.Cover6to13year.ToString("0.00"));
            planSetup.HeadManagerSplit = Convert.ToDecimal(planSetup.HeadManagerSplit.ToString("0.00"));
            planSetup.JoiningFee = Convert.ToDecimal(planSetup.JoiningFee.ToString("0.00"));
            planSetup.PlanSubscription = Convert.ToDecimal(planSetup.PlanSubscription.ToString("0.00"));
            planSetup.ManagerSplit = Convert.ToDecimal(planSetup.ManagerSplit.ToString("0.00"));
            planSetup.SpouseCover = Convert.ToDecimal(planSetup.SpouseCover.ToString("0.00"));
            planSetup.UnderwriterSplit = Convert.ToDecimal(planSetup.UnderwriterSplit.ToString("0.00"));
            planSetup.OfficeSplit = Convert.ToDecimal(planSetup.OfficeSplit.ToString("0.00"));
            planSetup.planCreators = ToolsSetingBAL.EditPlanCreatorbyID(planSetup.pkiPlanID);
            planSetup.UnderwriterList = CommonBAL.GetUnderwriterList(ParlourId).ToList();
            return PartialView("~/Areas/Tools/Views/PlanSetup/_PlanSetupAddEdit.cshtml", planSetup);
        }
        public ActionResult Update(int ID)
        {
            ViewBag.PlanId = ID;
            return View("Index");
        }
        public ActionResult Save(PlanModel planSetup)
        {
            try
            {
                planSetup.planCreators = planSetup.planCreators.Where(x => x.TableRawStatus == false).ToList();
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    planSetup.LastModified = System.DateTime.Now;
                    planSetup.ModifiedUser = formIdentity.Name;
                    var agentInfoSetupData = ToolsSetingBAL.NewSavePlanDetails(planSetup);
                    if (planSetup.planCreators.Count > 0)
                    {
                        for (int i = 0; i < planSetup.planCreators.Count; i++)
                        {
                            var context = new ValidationContext(planSetup.planCreators[i], null, null);
                            var results = new List<ValidationResult>();
                            if (Validator.TryValidateObject(planSetup.planCreators[i], context, results, true))
                            {
                                planSetup.planCreators[i].CreatedBy = planSetup.ModifiedUser;
                                planSetup.planCreators[i].CreatedDate = planSetup.LastModified;
                                planSetup.planCreators[i].IsActive = true;
                                planSetup.planCreators[i].PlanID = agentInfoSetupData;
                                ToolsSetingBAL.SavePlanCreatorDetails(planSetup.planCreators[i]);
                            }
                        }
                    }
                    TempData["IsPlanSetupSaved"] = true;
                    TempData.Keep("IsPlanSetupSaved");
                    return RedirectToAction("Index", "PlanSetup", new { area = "Tools" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            TempData["IsPlanSetupSaved"] = false;
            TempData.Keep("IsPlanSetupSaved");
            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string, new { area = "Tools" });
        }
        [FuneralAuthAttribute(PageId = 15, Right = new Rights[] { Rights.HasDelete })]
        public JsonResult Delete(int ID)
        {
            int retID = ToolsSetingBAL.DeletePlanAndPlanCreator(ID);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getUserTypeList()
        {
            var result = CommonBAL.GetUserTypes();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}