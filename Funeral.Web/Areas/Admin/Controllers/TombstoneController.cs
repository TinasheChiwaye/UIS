using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Areas.Admin.Models.ViewModel;
using Funeral.Web.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class TombstoneController : BaseAdminController
    {
        public TombstoneController() : base(11)
        {
            this.dbPageId = 11;
        }

        /// <summary>
        /// Index method to Display List and add page
        /// </summary>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 11, Right = new isPageRight[] { isPageRight.HasAccess })]
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

        /// <summary>
        /// list method to display list of quatation
        /// </summary>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 11)]
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

            var searchResult = new SearchResult<Model.Search.BaseSearch, TombStoneModel>(search, new List<TombStoneModel>(), o => o.LastName.Contains(search.SarchText) || o.FirstName.Contains(search.SarchText) || o.IDNumber.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Admin/Views/Tombstone/_TombstoneList.cshtml", search);
        }
        /// <summary>
        /// method to search data as per user input
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult SearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, TombStoneModel>(search, new List<TombStoneModel>(), o => o.LastName.Contains(search.SarchText) || o.FirstName.Contains(search.SarchText) || o.IDNumber.Contains(search.SarchText));
            try
            {
                var tombStoneList = TombStoneBAL.SelectAllTombStoneByParlourId(ParlourId, search.PageSize, search.PageNum, "", search.SortBy, search.SortOrder);
                return Json(new SearchResult<Model.Search.BaseSearch, TombStoneModel>(search, tombStoneList, o => o.FirstName.Contains(search.SarchText) || o.LastName.Contains(search.SarchText) || o.IDNumber.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, TombStoneModel>.Error(searchResult, ex));
            }
        }
        /// <summary>
        /// Add method to display page
        /// </summary>
        /// <param name="tombStoneModel"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 11, Right = new isPageRight[] { isPageRight.HasAdd})]
        public PartialViewResult Add(TombStoneModel tombStoneModel)
        {
            tombStoneModel.parlourid = ParlourId;
            ModelState.Clear();
            return PartialView("~/Areas/Admin/Views/Tombstone/_TombstoneAddEdit.cshtml", tombStoneModel);
        }
        /// <summary>
        /// Edit Method for Edit Tombstone 
        /// </summary>
        /// <param name="pkiTombstoneID"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 11, Right = new isPageRight[] { isPageRight.HasEdit})]
        public PartialViewResult Edit(int pkiTombstoneID)
        {
            //Index();
            var funeral = TombStoneBAL.SelectTombStoneByParlAndPki(pkiTombstoneID, ParlourId);
            if (funeral != null)
            {
                Session["TombStoneID"] = funeral.pkiTombstoneID;
            }
            return PartialView("~/Areas/Admin/Views/Tombstone/_TombstoneAddEdit.cshtml", funeral);
        }
        /// <summary>
        /// Save method for saving tombstone 
        /// </summary>
        /// <param name="tombStoneModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(TombStoneModel tombStoneModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var TombstoneData = TombStoneBAL.SaveTombStone(tombStoneModel);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Tombstone payment method get data by tombstone id
        /// </summary>
        /// <param name="pkiTombstoneID"></param>
        /// <returns></returns>
        public ActionResult TombstonePayment(int pkiTombstoneID)
        {
            try
            {
                TombStonePaymentVM objTombStonePaymentVM = new TombStonePaymentVM();
                objTombStonePaymentVM.TombStonePaymentList = TombStonesPaymentBAL.TombStonesPaymentSelectByTombstoneID(ParlourId, pkiTombstoneID);
                objTombStonePaymentVM.TombStoneServiceList = TombStoneBAL.SelectServiceByTombStoneID(pkiTombstoneID);
                objTombStonePaymentVM.objTombStoneModel = TombStoneBAL.SelectTombStoneByParlAndPki(pkiTombstoneID, ParlourId);
                objTombStonePaymentVM.TombStoneNumber = objTombStonePaymentVM.objTombStoneModel.pkiTombstoneID.ToString();
                objTombStonePaymentVM.ReceivedBy = User.Identity.Name;
                return View(objTombStonePaymentVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Tombstone Service by tombstone id
        /// </summary>
        /// <param name="pkiTombstoneID"></param>
        /// <returns></returns>
        public ActionResult TombStoneServiceSelect(int pkiTombstoneID)
        {
            TombStoneServiceVM objTomstone = new TombStoneServiceVM();
            objTomstone.Currency = Currency;
            objTomstone.TaxSettings = TaxSettingBAL.GetAllTaxSettings().Select(f => new SelectListItem { Text = f.TaxText, Value = f.TaxValue.ToString() }).ToList();
            objTomstone.ApplicationSettings = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            objTomstone.ServiceType = QuotationBAL.GetAllQuotationServices(ParlourId).Select(f => new SelectListItem { Text = f.ServiceName, Value = f.pkiServiceID.ToString() }).ToList();
            objTomstone.objTombStoneModel = TombStoneBAL.SelectTombStoneByParlAndPki(pkiTombstoneID, ParlourId);
            objTomstone.ServiceList = TombStoneBAL.SelectServiceByTombStoneID(pkiTombstoneID);
            objTomstone.GetAllPackage = TombstonePackageBAL.SelectAllPackage(ParlourId).Select(f => new SelectListItem { Text = f.PackageName, Value = f.pkiPackageID.ToString() }).ToList();
            objTomstone.ModelBankDetails = ToolsSetingBAL.GetBankingByID(ParlourId);
            objTomstone.ModelTermsAndCondition = ToolsSetingBAL.SelectApplicationTermsAndCondition(ParlourId);
            objTomstone.TombStonesPaymentModelList = TombStonesPaymentBAL.TombStonesPaymentSelectByTombstoneID(ParlourId, pkiTombstoneID);
            var dueDate = objTomstone.objTombStoneModel.CreatedDate;
            DateTime newDueDate = dueDate.AddHours(48);
            ViewBag.DueDate = newDueDate.ToString("dd/MMM/yyyy");
            ViewBag.CreatedDate = objTomstone.objTombStoneModel.CreatedDate.ToString("dd/MMM/yyyy");
            return View(objTomstone);
        }
        /// <summary>
        /// Save Tombstone service 
        /// </summary>
        /// <param name="tombStoneServicePackageVM"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SavePackageForTombStoneService(TombStoneServicePackageVM tombStoneServicePackageVM)
        {
            var modelList = TombstonePackageBAL.SelectPackageServiceByPackgeId(ParlourId, tombStoneServicePackageVM.pkiServiceID);
            TombStoneServiceSelectModel model = new TombStoneServiceSelectModel();
            if (modelList != null || modelList.Count > 0)
            {
                foreach (var item in modelList)
                {
                    model.fkiTombstoneID = tombStoneServicePackageVM.fkiTombstoneID;
                    model.fkiServiceID = tombStoneServicePackageVM.fkiServiceID;
                    model.Quantity = 1;
                    model.lastModified = DateTime.Now;
                    model.modifiedUser = UserName;
                    model.ServiceRate = tombStoneServicePackageVM.ServiceRate;
                    int a = TombStoneBAL.SaveTombStoneService(model);
                }
            }
            return Json("Package successfully applied.", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get Package for 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPackageFor() { return View(); }
        /// <summary>
        /// Save PAckage service for Tombstone Select Service
        /// </summary>
        /// <param name="tssp"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveServiceForTombStoneService(TombStoneServicePackageVM tssp)
        {
            try
            {
                TombStoneServiceSelectModel objSer = new TombStoneServiceSelectModel();
                objSer.fkiTombstoneID = tssp.fkiTombstoneID;
                objSer.fkiServiceID = tssp.fkiServiceID;
                objSer.Quantity = tssp.Quantity;
                objSer.lastModified = System.DateTime.Now;
                objSer.modifiedUser = UserName;
                objSer.ServiceRate = Convert.ToDecimal(tssp.ServiceRate);

                int a = TombStoneBAL.SaveTombStoneService(objSer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json("Service Successfully Saved.", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Tombstone print form service
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintForm(int tombId)
        {
            TombStoneServiceVM printObj = new TombStoneServiceVM();
            printObj.Currency = Currency;
            printObj.TaxSettings = TaxSettingBAL.GetAllTaxSettings().Select(f => new SelectListItem { Text = f.TaxText, Value = f.TaxValue.ToString() }).ToList();
            printObj.ApplicationSettings = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            printObj.ServiceType = TombStoneBAL.GetAllTombStoneServices(ParlourId).Select(f => new SelectListItem { Text = f.ServiceName, Value = f.pkiServiceID.ToString() }).ToList();
            printObj.objTombStoneModel = TombStoneBAL.SelectTombStoneByTombStoneId(tombId, ParlourId);
            printObj.ServiceList = TombStoneBAL.SelectServiceByTombStoneID(tombId);
            printObj.GetAllPackage = TombstonePackageBAL.SelectAllPackage(ParlourId).Select(f => new SelectListItem { Text = f.PackageName, Value = f.pkiPackageID.ToString() }).ToList();
            printObj.ModelBankDetails = ToolsSetingBAL.GetBankingByID(ParlourId);
            printObj.ModelTermsAndCondition = ToolsSetingBAL.SelectApplicationTermsAndCondition(ParlourId);

            return View(printObj);
        }
        /// <summary>
        /// Tombstone Add payment method 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult TombStonePaymentAddPayment(TombStonesPaymentModel data)
        {
            try
            {
                data.InvoiceID = 0;
                data.MemberBranch = string.Empty;
                data.DatePaid = System.DateTime.Now;
                data.parlourid = ParlourId;
                data.ModifiedUser = UserName;
                data.PaidBy = data.RecievedBy;
                int FuneralID = TombStonesPaymentBAL.AddInvoice(data);
                if (FuneralID != 0)
                {
                    return Json("Payment added successfully.", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Payment failes to save.", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Tombstone Payment history method for Popup
        /// </summary>
        /// <param name="pkiTombstoneID"></param>
        /// <returns></returns>
        public ActionResult PaymentHistory(int pkiTombstoneID)
        {
            TombStonePaymentVM objTombStonePaymentVM = new TombStonePaymentVM();
            objTombStonePaymentVM.TombStonePaymentList = TombStonesPaymentBAL.TombStonesPaymentSelectByTombstoneID(ParlourId, pkiTombstoneID);
            return Json(objTombStonePaymentVM, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// File upload method when Editing Tombstone 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult fileUpload()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    var pkiTombStoneID = string.Empty;
                    if (Session["TombStoneID"] != null)
                    {
                        pkiTombStoneID = Session["TombStoneID"].ToString();
                    }
                    string fname = string.Empty;
                    string imagePath = string.Empty;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFileBase file = files[i];
                        string fileName = Path.GetFileName(file.FileName);
                        var path = (dynamic)null;
                        string Str = System.DateTime.Now.Ticks.ToString();
                        path = 1012 + "_" + Str + "_" + fileName;
                        fname = Server.MapPath("~/Upload/TombImage/" + ParlourId + "/") + path;
                        file.SaveAs(fname);
                        string dbPath = "~/Upload/TombImage/" + ParlourId + "/" + path;
                        int b = TombStoneBAL.UploadTombImage(fileName, dbPath, Convert.ToInt32(pkiTombStoneID), ParlourId);
                        //Image1.ImageUrl = "~/Files/" + Path.GetFileName(FileUpload1.FileName);
                        imagePath = string.Format("{0}://{1}{2}/{3}", this.HttpContext.Request.Url.Scheme, this.HttpContext.Request.Url.Authority, this.HttpContext.Request.ApplicationPath, "Upload/TombImage/" + ParlourId + "/" + path);
                    }

                    return Json(new { ResponseData = imagePath, ResponseStatus = "File Uploaded Successfully!" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        /// <summary>
        /// On Edit of Tombstone Select Service 
        /// method for getting data of selected service
        /// </summary>
        /// <param name="TBID"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public JsonResult GetDataForServiceSelect(int TBID, int serviceID)
        {
            TombStoneServiceSelectModel model = TombStoneBAL.SelectServiceByTombAndID(TBID, serviceID);
            if ((model == null) || (model.fkiTombstoneID != TBID))
            {
                return Json("Sorry!you are not authorized to perform edit on this Service.");
            }
            else
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Get Data from on change of Servce DropDown
        /// </summary>
        /// <param name="ServiceID"></param>
        /// <returns></returns>
        public JsonResult GetServiceDataOnChange(int ServiceID)
        {
            var result = TombStoneBAL.GetServiceByID(ServiceID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// method for deleting selected service from Tombstone select service
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public JsonResult DeletePackageForServiceSelect(int serviceID)
        {
            var test = TombStoneBAL.DeleteTombstoneServiceByID(serviceID);
            return Json(test, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveTombStoneService(TombStoneService data)
        {
            try
            {
                Decimal Tax = Convert.ToDecimal(data.Tax);
                Decimal Discount = Convert.ToDecimal(data.DisCount);
                if (data.DisCount == null || data.DisCount == "")
                {
                    Discount = 0;
                }
                else
                {
                    Discount = Convert.ToDecimal(data.DisCount);
                }
                int a = TombStoneBAL.UpdateAllTombStoneData(data.pkiTombstoneID, Discount, Tax, data.InvoiceNumber);
                return Json("Successfully Saved.", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// Delete Quatation Service
        /// </summary>
        /// <param name="TombStoneID"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 11, Right = new isPageRight[] { isPageRight.HasDelete})]
        public JsonResult Delete(int pkiTombstoneID)
        {
            int tombstoneID = TombStoneBAL.TombStoneDelete(pkiTombstoneID, UserName);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}