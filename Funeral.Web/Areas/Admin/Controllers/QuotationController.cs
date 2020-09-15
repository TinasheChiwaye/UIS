using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Areas.Admin.Models.ViewModel;
using Funeral.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Funeral.Web.Areas.Admin.Controllers
{

    public class QuotationController : BaseAdminController
    {
        /// <summary>
        /// Base of Page which allow to access the page
        /// </summary>
        public QuotationController() : base(9)
        {
            this.dbPageId = 9;
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

        /// <summary>
        /// Index method to Display List
        /// </summary>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 9, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            //ViewBag.HasAccess = HasAccess;
            return View("Index");
        }

        /// <summary>
        /// list method to display list of quatation
        /// </summary>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 9)]
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

            var searchResult = new SearchResult<Model.Search.BaseSearch, QuotationModel>(search, new List<QuotationModel>(), o => o.ContactFirstName.Contains(search.SarchText) || o.CellNumber.Contains(search.SarchText) || o.QuotationStatus.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Admin/Views/Quotation/_QuotationList.cshtml", search);
        }

        /// <summary>
        /// method to search data as per user input
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult SearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, QuotationModel>(search, new List<QuotationModel>(), o => o.ContactFirstName.Contains(search.SarchText) || o.ContactLastName.Contains(search.SarchText) || o.CellNumber.Contains(search.SarchText) || o.QuotationStatus.Contains(search.SarchText));
            try
            {
                var quotationList = QuotationBAL.SelectQuotationByQuotationId(ParlourId, search.PageSize, search.PageNum, "", search.SortBy, search.SortOrder);
                return Json(new SearchResult<Model.Search.BaseSearch, QuotationModel>(search, quotationList, o => o.ContactFirstName.Contains(search.SarchText) || o.ContactLastName.Contains(search.SarchText) || o.CellNumber.Contains(search.SarchText) || o.QuotationStatus.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, QuotationModel>.Error(searchResult, ex));
            }
        }

        /// <summary>
        /// Add method to display page
        /// </summary>
        /// <param name="quotationModel"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 9, Right = new isPageRight[] { isPageRight.HasAdd })]
        public PartialViewResult Add(QuotationModel quotationModel)
        {
            ViewBag.Provinces = CommonBAL.GetProvinces();
            quotationModel.parlourid = ParlourId;
            ModelState.Clear();
            return PartialView("~/Areas/Admin/Views/Quotation/_QuotationAddEdit.cshtml", quotationModel);
        }

        /// <summary>
        /// method for get the data to edit
        /// </summary>
        /// <param name="QuotationId"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 9, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int QuotationId)
        {
            ViewBag.Provinces = CommonBAL.GetProvinces();
            var quotation = QuotationBAL.SelectQuotationByQuotationId(QuotationId, ParlourId);
            return PartialView("~/Areas/Admin/Views/Quotation/_QuotationAddEdit.cshtml", quotation);
        }

        /// <summary>
        /// Update method to update the data after edit
        /// </summary>
        /// <param name="QuotationId"></param>
        /// <returns></returns>
        public ActionResult Update(int QuotationId)
        {
            ViewBag.QuotationId = QuotationId;
            return View("Index");
        }

        /// <summary>
        /// Save Quatation
        /// </summary>
        /// <param name="quotationModel"></param>
        /// <returns></returns>
        [HttpPost]
        //public ActionResult Save(QuotationModel quotationModel)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            FormsIdentity formIdentity = (FormsIdentity)User.Identity;
        //            quotationModel.LastModified = System.DateTime.Now;
        //            quotationModel.ModifiedUser = formIdentity.Name;

        //            var agentInfoSetupData = QuotationBAL.SaveQuotation(quotationModel);

        //            TempData["IsQuotationModelSaved"] = true;
        //            TempData.Keep("IsQuotationModelSaved");

        //            return RedirectToAction("Index", "Quotation");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    TempData["IsQuotationModelSaved"] = false;
        //    TempData.Keep("IsQuotationModelSaved");

        //    return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string);
        //}
        public ActionResult Save(QuotationModel quotationModel)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => "<li>" + x.ErrorMessage + "</li>").ToList() }, JsonRequestBehavior.AllowGet);
                }
                else if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    quotationModel.LastModified = System.DateTime.Now;
                    quotationModel.ModifiedUser = formIdentity.Name;

                    var agentInfoSetupData = QuotationBAL.SaveQuotation(quotationModel);

                    TempData["IsQuotationModelSaved"] = true;
                    TempData.Keep("IsQuotationModelSaved");

                    return RedirectToAction("Index", "Quotation");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsQuotationModelSaved"] = false;
            TempData.Keep("IsQuotationModelSaved");

            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string);
        }

        /// <summary>
        /// Delete Quatation Service
        /// </summary>
        /// <param name="QuotationID"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 9, Right = new isPageRight[] { isPageRight.HasDelete })]
        public JsonResult Delete(int QuotationID)
        {
            //ViewBag.HasDeleteRight = HasDeleteRight;

            int quotationID = QuotationBAL.QuotationDelete(QuotationID, UserName);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region Quotation Serivce

        /// <summary>
        /// Print method for Getting Quatation Service
        /// </summary>
        /// <returns></returns>
        public ActionResult QuotationService(int qutId)
        {
            //ViewBag.Provinces = CommonBAL.GetProvinces();
            QuotationServiceVM quotationServiceVM = new QuotationServiceVM();
            quotationServiceVM.Currency = Currency;
            quotationServiceVM.TaxSettings = TaxSettingBAL.GetAllTaxSettings().Select(f => new SelectListItem { Text = f.TaxText, Value = f.TaxValue.ToString() }).ToList();
            quotationServiceVM.ApplicationSettings = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            quotationServiceVM.ServiceType = QuotationBAL.GetAllQuotationServices(ParlourId).Select(f => new SelectListItem { Text = f.ServiceName, Value = f.pkiServiceID.ToString() }).ToList();
            quotationServiceVM.objQuotation = QuotationBAL.SelectQuotationByQuotationId(qutId, ParlourId);
            quotationServiceVM.ServiceList = QuotationBAL.SelectServiceByQoutationID(qutId);
            quotationServiceVM.QuotationMessageModel = QuotationBAL.SelectQuotationMessageByID(qutId);
            quotationServiceVM.GetAllPackage = FuneralPackageBAL.SelectPackage(ParlourId).Select(f => new SelectListItem { Text = f.PackageName, Value = f.pkiPackageID.ToString() }).ToList();
            quotationServiceVM.ModelBankDetails = ToolsSetingBAL.GetBankingByID(ParlourId);
            quotationServiceVM.ModelTermsAndCondition = ToolsSetingBAL.SelectApplicationTermsAndCondition(ParlourId);
            var discID = QuotationBAL.GetAllQuotationDiscounts(qutId);
            if (discID != null)
            {
                ViewBag.DiscId = discID.DiscountID;
            }
            else
            {
                ViewBag.DiscId = 0;
            }
            QuotationMessageModel objmsg = QuotationBAL.SelectQuotationMessageByID(qutId);
            if (objmsg != null)
            {
                ViewBag.textMsgReject = objmsg.Message.ToString();
                ViewBag.pkidQuotationMsg = objmsg.pkidQuotationMsg;
            }
            return View(quotationServiceVM);
        }

        /// <summary>
        /// Get Data from on change of Servce DropDown
        /// </summary>
        /// <param name="ServiceID"></param>
        /// <returns></returns>
        public JsonResult GetServiceDataOnChange(int ServiceID)
        {
            var result = QuotationBAL.GetServiceByID(ServiceID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get quatation service Data by service id and quotation id
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <param name="QuotationId"></param>
        /// <returns></returns>
        public JsonResult GetServiceDataById(int ServiceId, int QuotationId)
        {
            string Message = string.Empty;
            QuotationServicesModel model = QuotationBAL.SelectServiceByQouAndID(QuotationId, ServiceId);
            if ((model == null) || (model.QuotationID != QuotationId))
            {
                Message = "Sorry!you are not authorized to perform edit on this Quotation.";
                return Json(Message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Delete Quotation Service by service id
        /// </summary>
        /// <param name="ServiceID"></param>
        /// <returns></returns>
        public JsonResult DeleteQuatationServiceById(int ServiceID)
        {
            int i = QuotationBAL.DeleteServiceByID(ServiceID);
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add Package for quatation Service
        /// </summary>
        /// <param name="objServiceVm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SavePackageForQuatationService(QuotationServicePackageVM objServiceVm)
        {
            var modelList = FuneralPackageBAL.SelectPackageService(ParlourId, objServiceVm.PackageName);
            var objService = new QuotationServicesModel();
            if (modelList != null)
            {
                foreach (var item in modelList)
                {
                    //if (ViewState["ID"] != null)
                    //{
                    //    objSer.pkiQuotationSelectionID = Convert.ToInt32(ViewState["ID"]);
                    //    ViewState["ID"] = null;
                    //}
                    //objService.pkiQuotationSelectionID = objServiceVm.pkiQuotationSelectionID;
                    objService.QuotationID = objServiceVm.QuotationID;
                    objService.fkiServiceID = item.fkiServiceID;
                    objService.Quantity = 1;
                    objService.lastModified = System.DateTime.Now;
                    objService.modifiedUser = UserName;
                    objService.ServiceRate = item.ServiceCost;
                    QuotationBAL.SaveService(objService);
                }
            }
            return Json("Package Successfully Added.", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add service for Quatation Service
        /// </summary>
        /// <param name="objServiceVm"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveServiceForQuotationService(QuotationServicePackageVM objServiceVm)
        {
            try
            {
                QuotationServicesModel objServ = new QuotationServicesModel();
                //if (ViewState["ID"] != null)
                //{
                //    objSer.pkiQuotationSelectionID = Convert.ToInt32(ViewState["ID"]);
                //    ViewState["ID"] = null;
                //}
                objServ.QuotationID = objServiceVm.QuotationID;
                objServ.fkiServiceID = objServiceVm.fkiServiceID;
                objServ.Quantity = objServiceVm.Quantity;
                objServ.lastModified = System.DateTime.Now;
                objServ.modifiedUser = UserName;
                objServ.ServiceRate = objServiceVm.ServiceCost;

                int a = QuotationBAL.SaveService(objServ);
                //Session["QuotationID"] = a;
                return Json(a, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// Print method for Printing Quotation
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintForm(int qutId)
        {
            QuotationServiceVM quotationServiceVM = new QuotationServiceVM();
            quotationServiceVM.Currency = Currency;
            quotationServiceVM.TaxSettings = TaxSettingBAL.GetAllTaxSettings().Select(f => new SelectListItem { Text = f.TaxText, Value = f.TaxValue.ToString() }).ToList();
            quotationServiceVM.ApplicationSettings = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            quotationServiceVM.ServiceType = QuotationBAL.GetAllQuotationServices(ParlourId).Select(f => new SelectListItem { Text = f.ServiceName, Value = f.pkiServiceID.ToString() }).ToList();
            quotationServiceVM.objQuotation = QuotationBAL.SelectQuotationByQuotationId(qutId, ParlourId);
            quotationServiceVM.ServiceList = QuotationBAL.SelectServiceByQoutationID(qutId);
            quotationServiceVM.QuotationMessageModel = QuotationBAL.SelectQuotationMessageByID(qutId);
            quotationServiceVM.GetAllPackage = FuneralPackageBAL.SelectPackage(ParlourId).Select(f => new SelectListItem { Text = f.PackageName, Value = f.pkiPackageID.ToString() }).ToList();
            quotationServiceVM.ModelBankDetails = ToolsSetingBAL.GetBankingByID(ParlourId);
            quotationServiceVM.ModelTermsAndCondition = ToolsSetingBAL.SelectApplicationTermsAndCondition(ParlourId);

            return View(quotationServiceVM);
        }

        /// <summary>
        /// Method for Save Quatation Service 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult SaveQuatationService(QuatationServiceVM data)
        {
            try
            {
                int a = QuotationBAL.UpdateAllData(data.QuotationID, data.Notes, data.QuotationNumber);
                //Discount Insert
                QuotationDiscountModel objQDM = new QuotationDiscountModel();
                objQDM.DiscountID = data.DiscountID;
                objQDM.QuotationID = data.QuotationID;

                objQDM.LastModified = DateTime.Now;
                objQDM.ModifiedUser = UserName;
                int b = QuotationBAL.SaveDiscountAndAmount(objQDM);
                Decimal Tax = Convert.ToDecimal(data.Tax);
                Decimal dis = 0;
                if (data.disc != null || data.disc == "")
                {
                    dis = Convert.ToDecimal(data.disc);
                }
                else { dis = 0; }

                int save = QuotationBAL.SaveQuotationTaxAndDiscount(data.QuotationID, Tax, dis);

                //ShowMessage(ref lblMessage, MessageType.Success, " Successfully Saved.");
                return Json("Successfully Saved.", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// Accept Button mehtod for quatation service
        /// </summary>
        /// <param name="qutId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AcceptButtonClick(string qutId)
        {
            try
            {
                string status = "Accept";
                int accept = QuotationBAL.QuotationStatus(Convert.ToInt32(qutId), ParlourId, status);
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Reject button click method for rejectin Quatation service
        /// </summary>
        /// <param name="qutId"></param>
        /// <param name="rejectMsg"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RejectButtonClick(int qutId, string rejectMsg)
        {
            try
            {
                QuotationMessageModel objmsg = new QuotationMessageModel();

                //if (ViewState["pkidQuotationMsg"] != null)
                //{
                //    objmsg.pkidQuotationMsg = Convert.ToInt32(ViewState["pkidQuotationMsg"]);
                //}
                objmsg.QuotationID = qutId;
                objmsg.Message = rejectMsg;
                objmsg.CreatedDate = System.DateTime.Now;
                objmsg.LastModified = System.DateTime.Now;
                objmsg.ModifiedUser = UserName;
                int a = QuotationBAL.SaveQuotationMessage(objmsg);

                string status = "Reject";
                int accept = QuotationBAL.QuotationStatus(qutId, ParlourId, status);
                return Json("True", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }
    }
}