using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Areas.Admin.Models.ViewModel;
using Funeral.Web.Common;
using Funeral.Web.DayPilot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

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
            ViewBag.AllUserList = CommonBAL.GetAllUser(ParlourId);
            ViewBag.StatusId = Request.Params["StatusId"];
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
        public PartialViewResult List(string StatusId)
        {
            //ViewBag.HasEditRight = HasEditRight;
            //ViewBag.HasDeleteRight = HasDeleteRight;
            ViewBag.StatusId = StatusId;

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

            search.DateFrom = null;
            search.DateTo = null;

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
            string status = Request.Params["StatusId"];

            if (status == null)
                status = search.StatusId;

            try
            {
                var funeralList = FuneralBAL.SelectAllFuneralByParlourId(ParlourId, search.PageSize, search.PageNum, "", search.SortBy, search.SortOrder, search.DateFrom, search.DateTo, status);

                return Json(new SearchResult<Model.Search.BaseSearch, FuneralModel>(search, funeralList, o => o.FullNames.Contains(search.SarchText) || o.Surname.Contains(search.SarchText) || o.IDNumber.Contains(search.SarchText)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, FuneralModel>.Error(searchResult, ex), JsonRequestBehavior.AllowGet);
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
            var funeral = new FuneralModel();
            funeralModel.parlourid = ParlourId;
            ModelState.Clear();
            funeralModel.CustomGrouping5 = CustomDetailsBAL.GetAllCustomDetailsByParlourId(CurrentParlourId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.DentalCondition)).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });

            //ViewBag.Provinces = CommonBAL.GetProvinces();
            //ViewBag.Status = CommonBAL.GetFuneralStatus();
            //ViewBag.Status = GetFuneralStatus();
            //ViewBag.DentalCondition = CommonBAL.GetDentalCondition();
            //ViewBag.BodyCondition = CommonBAL.GetBodyCondition();
            //ViewBag.AgeGroup = CommonBAL.GetAgeGroup();
            //ViewBag.HairType = CommonBAL.GetHairType();
            return PartialView("~/Areas/Admin/Views/Funeral/_FuneralAddEdit.cshtml", funeralModel);
        }

        //public List<ListItem> GetFuneralStatus()
        //{

        //    List<ListItem> liList = new List<ListItem>();

        //    List<FuneralStatus> objStatusModel = CommonBAL.GetFuneralStatus();

        //    foreach (FuneralStatus status in objStatusModel)
        //    {
        //        ListItem li = new ListItem();
        //        li.Text = status.Status;
        //        li.Value = status.FuneralStatusID.ToString();
        //        liList.Add(li);
        //    }
        //    return liList;
        //}

        /// <summary>
        /// method for get the data to edit
        /// </summary>
        /// <param name="pkiFuneralID"></param>
        /// <returns></returns>
        [PageRightsAttribute(CurrentPageId = 10, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int pkiFuneralID)
        {
            // Index();
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
            try
            {
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    funeralModel.LastModified = System.DateTime.Now;
                    funeralModel.ModifiedUser = formIdentity.Name;
                    if (funeralModel.pkiFuneralID != 0)
                    {
                        if (funeralModel.TimeOfFuneral == null)
                        {
                            funeralModel.TimeOfFuneral = DateTime.Now;
                        }
                        var updateFuneral = FuneralBAL.SaveFuneral(funeralModel);
                    }
                    else
                    {
                        var saveFuneral = FuneralBAL.SaveFuneral(funeralModel);
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
            int a = FuneralBAL.UpdateAllFuneralServiceData(data.pkiFuneralID, data.InvoiceNumber, Discount, Tax, data.Notes);

            string result = "Successfully Saved.";

            return Json(result, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return Json(ex, JsonRequestBehavior.AllowGet);
            //}

        }
        //public ActionResult PrintForm(int funId)
        //{

        //    FuneralListVM printObj = new FuneralListVM();

        //    FuneralPaymentsModel funeralPayments = new FuneralPaymentsModel();
        //    funeralPayments = MemberPaymentBAL.FuneralPaymentList(ParlourId, funId);

        //    ApplicationSettingsModel model = new ApplicationSettingsModel();
        //    model = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);

        //    printObj.BusinessAddressLine1 = model.BusinessAddressLine1;
        //    printObj.BusinessAddressLine2 = model.BusinessAddressLine2;
        //    printObj.BusinessAddressLine3 = model.BusinessAddressLine3;
        //    printObj.BusinessPostalCode = model.BusinessPostalCode;
        //    //printObj.FSBNumber = "FSB Number: " + model.FSBNumber;
        //    printObj.telephoneNumber = model.ManageTelNumber + " | " + model.ManageCellNumber;
        //    printObj.InvoiceID = funeralPayments.InvoiceID;
        //    //printObj.PolicyNumber = funeralPayments.;
        //    printObj.MonthPaid = funeralPayments.DatePaid.ToString("MMMM");
        //    printObj.DatePaid = funeralPayments.DatePaid;
        //    printObj.AmountPaid = Math.Round(funeralPayments.AmountPaid, 2);
        //    printObj.PaidBy = funeralPayments.PaidBy;
        //    printObj.RecievedBy = funeralPayments.RecievedBy;
        //    printObj.ParlourName = ApplicationName;
        //    printObj.TimePrinted = Convert.ToString(System.DateTime.Now);
        //    //FuneralBAL.SelectServiceByFuneralID

        //    return View(printObj);
        //}
        public ActionResult PrintForm(int funId)
        {
            FuneralServiceVM quotationServiceVM = new FuneralServiceVM();
            quotationServiceVM.Currency = Currency;
            quotationServiceVM.TaxSettings = TaxSettingBAL.GetAllTaxSettings().Select(f => new SelectListItem { Text = f.TaxText, Value = f.TaxValue.ToString() }).ToList();
            quotationServiceVM.ApplicationSettings = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            quotationServiceVM.ServiceType = QuotationBAL.GetAllQuotationServices(ParlourId).Select(f => new SelectListItem { Text = f.ServiceName, Value = f.pkiServiceID.ToString() }).ToList();
            quotationServiceVM.objFuneralModel = FuneralBAL.SelectFuneralBypkid(funId, ParlourId);
            quotationServiceVM.ServiceList = FuneralBAL.SelectServiceByFuneralID(funId);
            //quotationServiceVM.QuotationMessageModel = QuotationBAL.SelectQuotationMessageByID(qutId);
            quotationServiceVM.GetAllPackage = FuneralPackageBAL.SelectPackage(ParlourId).Select(f => new SelectListItem { Text = f.PackageName, Value = f.pkiPackageID.ToString() }).ToList();
            quotationServiceVM.ModelBankDetails = ToolsSetingBAL.GetBankingByID(ParlourId);
            quotationServiceVM.ModelTermsAndCondition = ToolsSetingBAL.SelectApplicationTermsAndCondition(ParlourId);

            return View(quotationServiceVM);
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

        public ActionResult BodyCollection()
        {
            return View();
        }

        public ActionResult Mortuary()
        {
            return View();
        }

        public ActionResult FuneralServices(int? funeralId)
        {
            var funeralModel = new FuneralModel() { parlourid = ParlourId, Status = "New" };
            if (funeralId.GetValueOrDefault(0) == 0)
                return View(funeralModel);

            funeralModel = FuneralBAL.SelectFuneralByFuneralId(funeralId.GetValueOrDefault(0), this.ParlourId);
            funeralModel.FuneralDocuments = FuneralBAL.SelectFuneralDocumentsByMemberId(funeralModel.pkiFuneralID);
            return View(funeralModel);
        }

        [HttpPost]
        public ActionResult FuneralServices(FuneralModel model, string submitForm)
        {
            string savedTabConfirmationMsg = model.Status == "New" ? "BodyCollection " : model.Status + " saved successfully";
            if (ModelState.IsValid)
            {
                model.parlourid = this.ParlourId;
                model.ModifiedUser = this.UserName;
                if (model.Status == "funeralschedule")
                    model.Status = "funeralschedule";
                else
                    model.Status = GetStatus(model.Status, submitForm);
                model.pkiFuneralID = FuneralBAL.SaveFuneral(model);
            }
            if (!string.IsNullOrEmpty(savedTabConfirmationMsg))
                TempData["savedTabConfirmationMsg"] = savedTabConfirmationMsg;
            model.FuneralDocuments = FuneralBAL.SelectFuneralDocumentsByMemberId(model.pkiFuneralID);
            return View(model);
        }
        public ActionResult FuneralSearch()
        {
            return View();
        }

        private string GetStatus(string currentStatus, string submittedFormName)
        {
            int statusNumber = 0;
            int formSubmitNumber = 0;
            switch (currentStatus)
            {
                case "BodyCollection":
                    statusNumber = 1;
                    break;
                case "Mortuary":
                    statusNumber = 2;
                    break;
                case "FuneralArrangement":
                    statusNumber = 3;
                    break;
                case "Payment":
                    statusNumber = 4;
                    break;
                case "FuneralSchedule":
                    statusNumber = 5;
                    break;
                case "CustomerFeedback":
                    statusNumber = 6;
                    break;
                case "Completed":
                    statusNumber = 7;
                    break;
                default:
                    statusNumber = 0;
                    break;
            }

            switch (submittedFormName)
            {
                case "bodyCollection":
                    formSubmitNumber = 1;
                    break;
                case "mortuary":
                    formSubmitNumber = 2;
                    break;
                case "funeralarrangement":
                    formSubmitNumber = 3;
                    break;
                case "payment":
                    formSubmitNumber = 4;
                    break;
                case "funeralschedule":
                    formSubmitNumber = 5;
                    break;
                default:
                    formSubmitNumber = 0;
                    break;
            }

            if (statusNumber >= formSubmitNumber)
                return currentStatus;
            else
                statusNumber = statusNumber + 1;

            return ((FuneralEnum.FuneralStatusEnum)statusNumber).ToString();
        }
        public JsonResult GetIdNumberAutocompleteData(string idNumber)
        {
            var data = FuneralBAL.GetIdNumberAutocompleteData(idNumber, this.ParlourId);

            return Json(JsonConvert.SerializeObject(data), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveClaimSupportedDocument(int funeralID, int docType)
        {
            string filename = Path.GetFileName(Request.Files[0].FileName);
            string contentType = Request.Files[0].ContentType;
            using (Stream fs = Request.Files[0].InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    FuneralDocumentModel ObjSupportedDocumentModel = new FuneralDocumentModel();
                    ObjSupportedDocumentModel.DocContentType = contentType;
                    ObjSupportedDocumentModel.ImageName = filename;
                    ObjSupportedDocumentModel.ImageFile = bytes;
                    ObjSupportedDocumentModel.fkiFuneralID = funeralID;
                    ObjSupportedDocumentModel.CreateDate = System.DateTime.Now;
                    ObjSupportedDocumentModel.Parlourid = this.ParlourId;
                    ObjSupportedDocumentModel.LastModified = DateTime.Now;
                    ObjSupportedDocumentModel.ModifiedUser = this.User.Identity.Name;
                    ObjSupportedDocumentModel.DocType = docType;

                    int documentId = FuneralBAL.SaveFuneralSupportedDocument(ObjSupportedDocumentModel);
                    if (documentId > 0)
                        return Json(new { success = true, message = "Supporting document uploaded successfully" });
                    else
                        return Json(new { success = false, message = "Supporting document upload failed" });
                }
            }
        }

        [Obsolete]
        public ActionResult FuneralServicesPage(int funeralID)
        {
            string id = funeralID.ToString();
            var plaintextBytes = Encoding.UTF8.GetBytes(id);
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);

            return Redirect("../FuneralServicesSelect.aspx?ID=" + encryptedValue);
        }
        public ActionResult FuneralService(int funeralId)
        {
            FuneralServiceViewModel model = new FuneralServiceViewModel();
            model.Funeral = FuneralBAL.SelectFuneralBypkid(funeralId, ParlourId);
            model.ApplicationSettings = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            model.FuneralServiceSelect = FuneralBAL.SelectServiceByFuneralID(funeralId);
            model.FuneralPayments = MemberPaymentBAL.ReturnFuneralPayments(ParlourId, funeralId.ToString()).ToList();
            model.ApplicationTnCModel = ToolsSetingBAL.SelectApplicationTermsAndCondition(ParlourId);

            ViewBag.ddlPackages = FuneralPackageBAL.GetAllPackage(this.ParlourId).Select(x => new SelectListItem() { Text = x.PackageName, Value = x.PackageName });
            ViewBag.ddlServices = QuotationBAL.GetAllQuotationServices(ParlourId).Select(x => new SelectListItem() { Text = x.ServiceName, Value = x.pkiServiceID.ToString() });

            return View(model);

        }
        public JsonResult ViewPaymentHistory(int funeralID, string parlourId)
        {
            List<FuneralPaymentsModel> modelList = MemberPaymentBAL.ReturnFuneralPayments(Guid.Parse(parlourId), funeralID.ToString()).ToList();
            return Json(new { data = modelList }, JsonRequestBehavior.AllowGet);
        }

        [Obsolete]
        public ActionResult PrintQuotation(int funeralID)
        {
            var plaintextBytes = Encoding.UTF8.GetBytes(funeralID.ToString());
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);
            return Redirect("../PrintForm.aspx?ID=" + encryptedValue);
        }
        [Obsolete]
        public ActionResult PrintFuneralQuotation(int funeralID)
        {
            var plaintextBytes = Encoding.UTF8.GetBytes(funeralID.ToString());
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All); 
            return Redirect("../PrintForm.aspx?FID=" + encryptedValue);
        }
        public ActionResult FuneralAssignedToUser(int? AssignedTo, int? PkiFuneralID, string funeralStatus, string ddlFuneralStatus)
        {
            FuneralBAL.FuneralAssignedToUser(AssignedTo, (PkiFuneralID.HasValue ? PkiFuneralID.Value : Convert.ToInt32(Request.Params["hdnPkiFuneralID"])), ddlFuneralStatus);
            return RedirectToAction("Index", new { StatusId = !string.IsNullOrEmpty(funeralStatus) ? funeralStatus : Request.Params["hdnfuneralStatus"] });
        }
        public ActionResult FuneralQuotation(int? funeralId)
        {
            var funeralModel = new FuneralModel() { parlourid = ParlourId, Status = "New" };
            if (funeralId.GetValueOrDefault(0) == 0)
                return View(funeralModel);

            funeralModel = FuneralBAL.SelectFuneralByFuneralId(funeralId.GetValueOrDefault(0), this.ParlourId);
            funeralModel.FuneralDocuments = FuneralBAL.SelectFuneralDocumentsByMemberId(funeralModel.pkiFuneralID);
            return View(funeralModel);
        }
        public ActionResult Backend(int? funeralId)
        {
            return new Dpc(funeralId, UserID).CallBack(this);
        }
        public ActionResult AllFuneralSchedules()
        {
            return View();
        }
        public ActionResult ServiceChange(int serviceId)
        {
            int Description = Convert.ToInt32(serviceId);
            QuotationServicesModel objQuotation = QuotationBAL.GetServiceByID(Description);
            decimal money = objQuotation.ServiceCost;

            string rate = string.Format("{0:#.00}", money);
            return Json(new
            {
                Description = objQuotation.ServiceDesc,
                Rate = rate
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddPackage(string packageId, int funeralId)
        {
            List<PackageServicesSelectionModel> modelList = FuneralPackageBAL.GetPackageService(this.ParlourId, packageId);
            FuneralServiceSelectModel objSer = null;

            foreach (var item in modelList)
            {
                objSer = new FuneralServiceSelectModel();
                objSer.fkiFuneralID = funeralId;
                objSer.fkiServiceID = item.fkiServiceID;
                objSer.Quantity = 1;
                objSer.lastModified = System.DateTime.Now;
                objSer.modifiedUser = UserName;
                objSer.ServiceRate = item.ServiceCost;

                FuneralBAL.SaveFuneralService(objSer);
            }
            return Json(new { message = "Package Successfully Added." }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddService(int funeralId, int serviceId, int quantity, int rate,int? pkiFuneralServiceSelectionID)
        {
            FuneralServiceSelectModel objSer = new FuneralServiceSelectModel();
            if (pkiFuneralServiceSelectionID.HasValue)
                objSer.pkiFuneralServiceSelectionID = pkiFuneralServiceSelectionID.Value;

            objSer.fkiFuneralID = funeralId;
            objSer.fkiServiceID = serviceId;
            objSer.Quantity = quantity;
            objSer.lastModified = System.DateTime.Now;
            objSer.modifiedUser = UserName;
            objSer.ServiceRate = rate;

            int a = FuneralBAL.SaveFuneralService(objSer);

            return Json(new { message = "Service Successfully Saved." }, JsonRequestBehavior.AllowGet);  
        }
        public ActionResult DeleteFuneralService(int pkiFuneralServiceSelectionID)
        {
            FuneralBAL.DeleteFuneralServiceByID(pkiFuneralServiceSelectionID);

            return Json(new { message = "Service Successfully Deleted." }, JsonRequestBehavior.AllowGet); 
        }
    }
}