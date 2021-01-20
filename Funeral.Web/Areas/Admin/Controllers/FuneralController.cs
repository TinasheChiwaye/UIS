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
using System.Web.Security;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class FuneralController : BaseAdminController
    {


        /// <summary>
        /// Base of Page which allow to access the page
        /// </summary>
        public FuneralController() : base(10)
        {
            this.dbPageId = 10;
        }

        /// <summary>
        /// Index method to Display List
        /// </summary>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 10, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            //ViewBag.HasCreate = HasCreateRight;
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
        [PageRightsAttribute(CurrentPageId = 10)]
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

            var searchResult = new SearchResult<Model.Search.BaseSearch, FuneralModel>(search, new List<FuneralModel>(), o => o.FullNames.Contains(search.SarchText) || o.Surname.Contains(search.SarchText) || o.IDNumber.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Admin/Views/Funeral/_FuneralList.cshtml", search);
        }

        /// <summary>
        /// method to search data as per user input
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult SearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, FuneralModel>(search, new List<FuneralModel>(), o => o.FullNames.Contains(search.SarchText) || o.Surname.Contains(search.SarchText) || o.IDNumber.Contains(search.SarchText));
            try
            {
                var funeralList = FuneralBAL.SelectAllFuneralByParlourId(ParlourId, search.PageSize, search.PageNum, "", search.SortBy, search.SortOrder);
                return Json(new SearchResult<Model.Search.BaseSearch, FuneralModel>(search, funeralList, o => o.FullNames.Contains(search.SarchText) || o.Surname.Contains(search.SarchText) || o.IDNumber.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, FuneralModel>.Error(searchResult, ex));
            }
        }

        /// <summary>
        /// Add method to display page
        /// </summary>
        /// <param name="funeralModel"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 10, Right = new isPageRight[] { isPageRight.HasAdd })]
        public PartialViewResult Add(FuneralModel funeralModel)
        {
            funeralModel.parlourid = ParlourId;
            ModelState.Clear();
            return PartialView("~/Areas/Admin/Views/Funeral/_FuneralAddEdit.cshtml", funeralModel);
        }

        /// <summary>
        /// method for get the data to edit
        /// </summary>
        /// <param name="pkiFuneralID"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 10, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int pkiFuneralID)
        {
            Index();
            var funeral = FuneralBAL.SelectFuneralBypkid(pkiFuneralID, ParlourId);
            return PartialView("~/Areas/Admin/Views/Funeral/_FuneralAddEdit.cshtml", funeral);
        }

        /// <summary>
        /// Update method to update the data after edit
        /// </summary>
        /// <param name="QuotationId"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 10)]
        public ActionResult Update(int QuotationId)
        {
            ViewBag.QuotationId = QuotationId;
            return View("Index");
        }

        /// <summary>
        /// Save Funeral
        /// </summary>
        /// <param name="funeralModel"></param>
        /// <returns></returns>
        [HttpPost]
        [PageRightsAttribute(CurrentPageId = 10)]
        public ActionResult Save(FuneralModel funeralModel)
        {
            var funeral = FuneralBAL.SelectFuneralBypkid(funeralModel.pkiFuneralID, ParlourId);
            try
            {
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    funeralModel.LastModified = System.DateTime.Now;
                    funeralModel.ModifiedUser = formIdentity.Name;
                    funeralModel.FkiClaimID = funeral.FkiClaimID;
                    funeralModel.MemberType = funeral.MemberType;
                    if (funeralModel.pkiFuneralID != 0)
                    {
                        if (funeralModel.TimeOfFuneral == null)
                        {
                            funeralModel.TimeOfFuneral = DateTime.Now;
                        }
                        FuneralBAL.UpdateFuneral(funeralModel);
                    }
                    else
                    {
                        FuneralBAL.SaveFuneral(funeralModel);
                    }


                    TempData["IsFuneralModelSaved"] = true;
                    TempData.Keep("IsFuneralModelSaved");

                    return RedirectToAction("Index", "Funeral");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsFuneralModelSaved"] = false;
            TempData.Keep("IsFuneralModelSaved");

            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string);
        }

        /// <summary>
        /// Delete Quatation Service
        /// </summary>
        /// <param name="QuotationID"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 10, Right = new isPageRight[] { isPageRight.HasDelete })]
        public JsonResult Delete(int ID)
        {
            int quotationID = FuneralBAL.FuneralDelete(ID, UserName);
            var result = new ResponseResult() { Error = null, Message = "Deleted Successfully.", StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString()) };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FuneralServiceSelect(int pkiFuneralID)
        {
            FuneralServiceVM objFuneral = new FuneralServiceVM();
            objFuneral.Currency = Currency;
            objFuneral.TaxSettings = TaxSettingBAL.GetAllTaxSettings().Select(f => new SelectListItem { Text = f.TaxText, Value = f.TaxValue.ToString() }).ToList();
            objFuneral.ApplicationSettings = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            objFuneral.ServiceType = FuneralBAL.GetAllFuneralServices(ParlourId).Select(f => new SelectListItem { Text = f.ServiceName, Value = f.pkiServiceID.ToString() }).ToList();
            objFuneral.objFuneralModel = FuneralBAL.SelectFuneralByParlAndPki(pkiFuneralID, ParlourId);
            objFuneral.ServiceList = FuneralBAL.SelectServiceByFuneralID(pkiFuneralID);
            objFuneral.GetAllPackage = FuneralPackageBAL.SelectAllPackage(ParlourId).Select(f => new SelectListItem { Text = f.PackageName, Value = f.pkiPackageID.ToString() }).ToList();
            objFuneral.ModelBankDetails = ToolsSetingBAL.GetBankingByID(ParlourId);
            objFuneral.ModelTermsAndCondition = ToolsSetingBAL.SelectApplicationTermsAndCondition(ParlourId);
            //objFuneral.FuneralPaymentModelList = TombStonesPaymentBAL.TombStonesPaymentSelectByTombstoneID(ParlourId, pkiFuneralID);
            objFuneral.FuneralPaymentModelList = null;
            var dueDate = objFuneral.objFuneralModel.CreatedDate;
            DateTime newDueDate = dueDate.AddHours(48);
            ViewBag.DueDate = newDueDate.ToString("dd/MMM/yyyy");
            ViewBag.CreatedDate = objFuneral.objFuneralModel.CreatedDate.ToString("dd/MMM/yyyy");
            return View(objFuneral);
        }

        /// <summary>
        /// Funeral Payment Method to bind All service and invoices to page
        /// </summary>
        /// <param name="funeralId"></param>
        /// <returns></returns>
        public ActionResult FuneralPayment(int funeralId)
        {
            decimal Amt = 0;
            decimal tax = 0;
            var processPayment = new FuneralPaymentVM();
            processPayment.FunerlaPaymentList = MemberPaymentBAL.ReturnFuneralPayments(ParlourId, Convert.ToString(funeralId));
            processPayment.FuneralServiceList = FuneralBAL.SelectServiceByFuneralID(funeralId);
            processPayment.objFuneralModel = FuneralBAL.SelectFuneralBypkid(funeralId, ParlourId);
            var TotalPayment = MemberPaymentBAL.ReturnFuneralPayments(ParlourId, Convert.ToString(funeralId)).ToList().Sum(x => x.AmountPaid);
            foreach (var item in processPayment.FuneralServiceList)
            {
                Amt = Amt + item.Amount;
            }
            var test = CalculateFinal(Amt, tax, processPayment.objFuneralModel.Discount, TotalPayment);
            processPayment.FuneralNumber = Convert.ToString(funeralId);
            processPayment.ReceivedBy = User.Identity.Name;
            processPayment.TotalAmount = test;
            //processPayment.MonthPaid = 
            return View(processPayment);
        }
        public string CalculateFinal(Decimal sub, decimal Tax, decimal Dis, decimal totalPaid)
        {

            Decimal DisAmt = 0;
            Decimal TaxAmt = 0;
            Decimal ttl = 0;
            Decimal a = 0;
            TaxAmt = (((sub * Tax) / 100));
            a = (sub + TaxAmt);
            DisAmt = (((a * Dis) / 100));
            ttl = (a - DisAmt);
            return (ttl - totalPaid).ToString("N2");

        }
        [HttpPost]
        public JsonResult FuneralPaymentAddPayment(FuneralPaymentsModel data)
        {
            //FuneralPaymentsModel funeralPayment = new FuneralPaymentsModel();
            ////funeralPayment.DatePaid = Convert.ToDateTime(txtNextPaymentDate.Text);
            //funeralPayment.FuneralID = FuneralId;
            //funeralPayment.AmountPaid = Convert.ToDecimal(txtAmount.Text.Replace(Currency.Trim() + " ", ""));
            //funeralPayment.RecievedBy = txtReceivedBy.Text.ToString();
            //funeralPayment.Notes = txtMohthPaid.Text.ToString();
            //funeralPayment.ParlourId = ParlourId;
            //funeralPayment.UserName = HttpContext.Current.User.Identity.Name;
            try
            {
                data.DatePaid = System.DateTime.Now;
                data.ParlourId = ParlourId;
                data.UserName = UserName;
                int FuneralID = MemberPaymentBAL.AddFuneralPayments(data);
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
        /// Save PAckage service for Funeral Select Service
        /// </summary>
        /// <param name="tssp"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveServiceForFuneralService(FuneralServicePackageVM tssp)
        {
            try
            {


                FuneralServiceSelectModel objSer = new FuneralServiceSelectModel();
                objSer.pkiFuneralServiceSelectionID = tssp.pkiFuneralSelectionID;
                objSer.fkiFuneralID = tssp.fkiFuneralID;
                objSer.fkiServiceID = tssp.fkiServiceID;
                objSer.Quantity = tssp.Quantity;
                objSer.lastModified = System.DateTime.Now;
                objSer.modifiedUser = UserName;
                objSer.ServiceRate = Convert.ToDecimal(tssp.ServiceRate);

                int a = FuneralBAL.SaveFuneralService(objSer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json("Service Successfully Saved.", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Save PAckage service for Funeral Select Service
        /// </summary>
        /// <param name="tssp"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateServiceForFuneralService(FuneralServicePackageVM tssp)
        {
            try
            {


                FuneralServiceSelectModel objSer = new FuneralServiceSelectModel();
                objSer.fkiFuneralID = tssp.fkiFuneralID;
                objSer.fkiServiceID = tssp.fkiServiceID;
                objSer.Quantity = tssp.Quantity;
                objSer.lastModified = System.DateTime.Now;
                objSer.modifiedUser = UserName;
                objSer.ServiceRate = Convert.ToDecimal(tssp.ServiceRate);

                int a = FuneralBAL.UpdateFuneralService(objSer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json("Service Successfully Saved.", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// On Edit of Tombstone Select Service 
        /// method for getting data of selected service
        /// </summary>
        /// <param name="TBID"></param>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public JsonResult GetDataForServiceSelect(int FID, int serviceID)
        {
            FuneralServiceSelectModel model = FuneralBAL.SelectServiceByFunAndID(FID, serviceID);
            if ((model == null) || (model.fkiFuneralID != FID))
            {
                return Json("Sorry!you are not authorized to perform edit on this Service.");
            }
            else
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// method for deleting selected service from Tombstone select service
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public JsonResult DeletePackageForServiceSelect(int serviceID)
        {
            var test = FuneralBAL.DeleteFuneralServiceByID(serviceID);
            return Json(test, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveFuneralService(FuneralService data)
        {
            //try
            //{
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
            int a = FuneralBAL.UpdateAllFuneralServiceData(data.pkiFuneralID, data.InvoiceNumber, Discount, Tax);

            string result = "Successfully Saved.";

            return Json(result, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return Json(ex, JsonRequestBehavior.AllowGet);
            //}

        }
        public ActionResult PrintForm(int funId)
        {
            FuneralServiceVM printObj = new FuneralServiceVM();
            printObj.Currency = Currency;
            printObj.TaxSettings = TaxSettingBAL.GetAllTaxSettings().Select(f => new SelectListItem { Text = f.TaxText, Value = f.TaxValue.ToString() }).ToList();
            printObj.ApplicationSettings = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            printObj.ServiceType = FuneralBAL.GetAllFuneralServices(ParlourId).Select(f => new SelectListItem { Text = f.ServiceName, Value = f.pkiServiceID.ToString() }).ToList();
            printObj.objFuneralModel = FuneralBAL.SelectFuneralByFuneralId(funId, ParlourId);
            printObj.ServiceList = FuneralBAL.SelectServiceByFuneralID(funId);
            printObj.GetAllPackage = FuneralPackageBAL.SelectPackage(ParlourId).Select(f => new SelectListItem { Text = f.PackageName, Value = f.pkiPackageID.ToString() }).ToList();
            printObj.ModelBankDetails = ToolsSetingBAL.GetBankingByID(ParlourId);
            printObj.ModelTermsAndCondition = ToolsSetingBAL.SelectApplicationTermsAndCondition(ParlourId);

            return View(printObj);
        }

        public ActionResult PaymentHistory(int FuneralId)
        {
            var model = new FuneralPaymentVM();
            model.FunerlaPaymentList = MemberPaymentBAL.ReturnFuneralPayments(ParlourId, Convert.ToString(FuneralId));
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveFuneralDocument()
        {
            //int docType = Convert.ToInt32(documentType);
            if (Request.Files.Count > 0)
            {
                try
                {
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
                        fname = Server.MapPath("~/Upload/FuneralDocument/" + ParlourId + "/") + path;
                        file.SaveAs(fname);
                        Stream fs = file.InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string dbPath = "~/Upload/FuneralDocument/" + ParlourId + "/" + path;
                        FuneralDocumentModel objFuneralDocument = new FuneralDocumentModel();
                        objFuneralDocument.DocContentType = file.ContentType;
                        objFuneralDocument.ImageName = file.FileName;
                        objFuneralDocument.ImageFile = bytes;
                        objFuneralDocument.fkiFuneralID = Convert.ToInt32(Request.Params["funeralID"]);
                        objFuneralDocument.CreateDate = DateTime.Now;
                        objFuneralDocument.Parlourid = ParlourId;
                        objFuneralDocument.LastModified = DateTime.Now;
                        objFuneralDocument.ModifiedUser = User.Identity.Name;
                        objFuneralDocument.DocType = Convert.ToInt32(Request["docType"]);
                        int b = FuneralBAL.SaveFuneralSupportedDocument(objFuneralDocument);
                        imagePath = string.Format("{0}://{1}{2}/{3}", this.HttpContext.Request.Url.Scheme, this.HttpContext.Request.Url.Authority, this.HttpContext.Request.ApplicationPath, "Upload/FuneralDocument/" + ParlourId + "/" + path);
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
    }
}