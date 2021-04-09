using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Funeral.Web.Areas.Admin.Models.ViewModel;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using System.Data;


namespace Funeral.Web.Areas.Admin.Controllers
{
    public class GroupPaymentController : BaseAdminController
    {
        // GET: Admin/GroupPayment
        private readonly ISiteSettings _siteConfig = new SiteSettings();
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
        public GroupPaymentController() : base(7)
        {
            this.dbPageId = 7;
        }
        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            return View("Index");
        }
        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult GroupPaymentView(Guid id, string RefNo)
        {
            ViewBag.GroupId = id;
            TempData["GroupId"] = id;
            TempData["ReferenceNumber"] = RefNo;
            return View();
        }
        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasAdd })]
        public PartialViewResult Add(GroupPayment GroupPayment)
        {

            object value = TempData.Peek("GroupId");
            TempData.Keep("GroupId");
            string ReferenceNumber = TempData.Peek("ReferenceNumber").ToString();
            TempData.Keep("ReferenceNumber");
            Guid getParlourId = Guid.Parse(value.ToString());
            var GetDetails = ToolsSetingBAL.GetGroupPayment_ByParlourId(getParlourId, ReferenceNumber);
            if (GetDetails != null)
            {
                GroupPayment.SocietyId = GetDetails.GroupId;
                GroupPayment.AmountPaid = GetDetails.Premium;
                GroupPayment.CompanyGroupId = GetDetails.parlourid;
                GroupPayment.TotalRiskCovered = GetDetails.TotalRiskCovered;
                GroupPayment.Balance = GetDetails.Balance;
                GroupPayment.AmountAtRisk = GetDetails.AmountAtRisk;
                GroupPayment.InceptionDate = GetDetails.InceptionDate;
                GroupPayment.ReferenceNumber = GetDetails.ReferenceNumber;
            }
            GroupPayment.DatePaid = DateTime.Now.ToString();
            GroupPayment.parlourid = getParlourId;
            GroupPayment.SocietyDropdown = CommonBAL.GetAllSocietyesList(getParlourId);
            ModelState.Clear();
            return PartialView("~/Areas/Admin/Views/GroupPayment/_AddGroupPayment.cshtml", GroupPayment);
        }
        public ActionResult Save(GroupPayment groupPayment, FormCollection formCollection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FormsIdentity formIdentity = (FormsIdentity)User.Identity;
                    groupPayment.LastModified = System.DateTime.Now;
                    groupPayment.PaidBy = formIdentity.Name;
                    groupPayment.RecievedBy = formIdentity.Name;
                    int invoiceGroupId = OtherPaymentBAL.AddEditGroupPayment(groupPayment);
                    TempData["IsSocietySetupSaved"] = true;
                    TempData.Keep("IsSocietySetupSaved");

                    if (groupPayment.AutoAllocatePremiumToMember)
                    {
                        OtherPaymentBAL.AutoallocateMemberPayments(UserName, groupPayment.parlourid, groupPayment.ReferenceNumber);
                    }
                    else
                    {
                        var groupPaymentList = new List<GroupPayment>();
                        HttpPostedFileBase file = Request.Files["fnSelectedFile"];
                        if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                        {
                            string fileName = file.FileName;
                            string fileContentType = file.ContentType;
                            byte[] fileBytes = new byte[file.ContentLength];
                            var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                            using (var package = new ExcelPackage(file.InputStream))
                            {
                                var currentSheet = package.Workbook.Worksheets;
                                var workSheet = currentSheet.First();
                                var noOfCol = workSheet.Dimension.End.Column;
                                var noOfRow = workSheet.Dimension.End.Row;
                                if (Convert.ToString(workSheet.Cells[1, 1].Value).Replace(" ", "") == "ReferenceNumber")
                                {
                                    for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                    {
                                        var excelData = new GroupPayment();
                                        excelData.parlourid = groupPayment.parlourid;
                                        excelData.LastModified = DateTime.Now;
                                        excelData.DatePaid = groupPayment.DatePaid;
                                        excelData.RecievedBy = UserName;
                                        excelData.PaidBy = UserName;
                                        if (Convert.ToString(workSheet.Cells[rowIterator, 1].Value) != "")
                                            excelData.ReferenceNumber = Convert.ToString(workSheet.Cells[rowIterator, 1].Value);
                                        if (Convert.ToString(workSheet.Cells[rowIterator, 2].Value) != "")
                                            excelData.AmountPaid = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                                        if (Convert.ToString(workSheet.Cells[rowIterator, 3].Value) != "")
                                            excelData.Notes = Convert.ToString(workSheet.Cells[rowIterator, 3].Value);
                                        if (excelData.ReferenceNumber != null || excelData.AmountPaid != 0 || excelData.Notes != "")
                                            groupPaymentList.Add(excelData);
                                    }
                                }
                                else
                                {
                                    TempData["message"] = ShowMessage(MessageType.Danger, "ExcelSheet is not Proper Format. First Columns must be in sequence Reference Number,Amount,PolicyNumber");
                                }
                            }
                        }
                        var sheetData = OtherPaymentBAL.AddExcelSheetData(groupPaymentList);
                    }
                    return RedirectToAction("GroupPaymentView", "GroupPayment", new { id = groupPayment.CompanyGroupId, RefNo = groupPayment.ReferenceNumber, Area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            TempData["IsSocietySetupSaved"] = false;
            TempData.Keep("IsSocietySetupSaved");
            return RedirectToAction("Index", "GroupPayment", new { area = "Admin" });
        }
        [PageRightsAttribute(CurrentPageId = 7)]
        public PartialViewResult List()
        {
            var statusList = CommonBAL.GetStatus(FuneralEnum.StatusAssociatedTable.Members.ToString()).Select(x => new SelectListItem() { Text = x.ID.ToString(), Value = x.Status });
            ViewBag.StatusList = statusList;
            ViewBag.HasEditRight = HasEditRight;
            ViewBag.HasDeleteRight = HasDeleteRight;
            ViewBag.SocietyLists = CommonBAL.GetSocietyByParlourId(CurrentParlourId);
            ViewBag.GroupInvoiceList = OtherPaymentBAL.GetSchemePaymentList(CurrentParlourId);


            Model.Search.PaymentSearchNew search = new Model.Search.PaymentSearchNew();
            search.PageNum = 1;
            search.StatusId = new Guid(CurrentParlourId.ToString());
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;
            var searchResult = new SearchResult<Model.Search.BaseSearch, GroupPayment>(search, new List<GroupPayment>(), o => o.SocietyName.Contains(search.SarchText));
            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;
            return PartialView("~/Areas/Admin/Views/GroupPayment/_GroupPaymentList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.BaseSearch search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, GroupPayment>(search, new List<GroupPayment>(), o => o.SocietyName.Contains(search.SarchText));

            try
            {
                object value = TempData.Peek("GroupId");
                TempData.Keep("GroupId");

                var SocietyList = new List<GroupPayment>();
                var groupPayment = ToolsSetingBAL.GetGroupPayment_ByScheme(ParlourId);
                //var SocietyList = new List<GroupPayment>();
                if (groupPayment != null)
                {
                    SocietyList = OtherPaymentBAL.GetSchemePaymentList(CurrentParlourId);
                }
                return Json(new SearchResult<Model.Search.BaseSearch, GroupPayment>(search, SocietyList, o => o.SocietyName.ToLower().Contains(search.SarchText.ToLower())));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, GroupPayment>.Error(searchResult, ex));
            }
        }
        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int ID)
        {
            var groupPayment = OtherPaymentBAL.EditGroupPaymentByID(ID, ParlourId);
            var GetDetails = ToolsSetingBAL.GetGroupPayment_ByParlourId(groupPayment.parlourid, groupPayment.ReferenceNumber);
            groupPayment.SocietyDropdown = CommonBAL.GetAllSocietyesList(ParlourId);
            if (GetDetails != null)
            {
                groupPayment.AmountAtRisk = GetDetails.AmountAtRisk;
                groupPayment.Balance = GetDetails.Balance;
                groupPayment.TotalRiskCovered = GetDetails.TotalRiskCovered;
                groupPayment.InceptionDate = GetDetails.InceptionDate;
            }
            return PartialView("~/Areas/Admin/Views/GroupPayment/_AddGroupPayment.cshtml", groupPayment);
        }
        [PageRightsAttribute(CurrentPageId = 7, Right = new isPageRight[] { isPageRight.HasDelete })]
        public JsonResult GroupPaymentReversal(int ID)
        {
            int PaymentID = MemberPaymentBAL.AddGroupReversalPayments(ID, UserName, ParlourId, TempData.Peek("ReferenceNumber").ToString());
            string Message = "";
            if (PaymentID != 0)
            {
                bindInvoices(CurrentParlourId);
                Message = "Reversal added successfully.";
            }
            else
            {
                Message = "Reversal not added successfully.";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public void bindInvoices(Guid ParlourId)
        {

            List<GroupPayment> objGroupInvoiceModel = OtherPaymentBAL.GetSchemePaymentList(CurrentParlourId);
        }

        public ActionResult PrintForm(int GroupInvoiceID)
        {
            GroupPaymentVM printObj = new GroupPaymentVM();

            GroupPayment groupPayment = new GroupPayment();
            groupPayment = OtherPaymentBAL.GetGroupPaymentByID(GroupInvoiceID, ParlourId);

            ApplicationSettingsModel model = new ApplicationSettingsModel();
            model = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);

            printObj.BusinessAddressLine1 = model.BusinessAddressLine1;
            printObj.BusinessAddressLine2 = model.BusinessAddressLine2;
            printObj.BusinessAddressLine3 = model.BusinessAddressLine3;
            printObj.BusinessPostalCode = model.BusinessPostalCode;
            printObj.FSBNumber = "FSB Number: " + model.FSBNumber;
            printObj.telephoneNumber = model.ManageTelNumber + " | " + model.ManageCellNumber;
            printObj.Id = groupPayment.GroupInvoiceID;
            printObj.PolicyNumber = groupPayment.SocietyName;
            printObj.MonthPaid = Convert.ToDateTime(groupPayment.DatePaid).ToString("MMMM");
            printObj.DatePaid = groupPayment.DatePaid;
            printObj.AmountPaid = Math.Round(groupPayment.AmountPaid, 2);
            printObj.PaidBy = groupPayment.PaidBy;
            printObj.ReceivedBy = groupPayment.RecievedBy;
            printObj.ParlourName = model.ApplicationName;
            printObj.ParlourName = ApplicationName;
            printObj.TimePrinted = Convert.ToString(System.DateTime.Now);
            printObj.Notes = groupPayment.Notes;
            printObj.RefernceNumer = groupPayment.ReferenceNumber;

            return View(printObj);
        }

        [PageRightsAttribute(CurrentPageId = 7)]
        public PartialViewResult GroupList()
        {
            BindCompanyList("Search");
            ViewBag.HasEditRight = HasEditRight;
            ViewBag.HasDeleteRight = HasDeleteRight;

            Model.Search.PaymentSearchNew search = new Model.Search.PaymentSearchNew();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;
            //search.StatusId = CurrentParlourId;

            var searchResult = new SearchResult<Model.Search.BaseSearch, SocietyModel>(search, new List<SocietyModel>(), o => o.SocietyName.Contains(search.SarchText));

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            return PartialView("~/Areas/Admin/Views/GroupPayment/_PaymentList.cshtml", search);
        }
        public ActionResult GroupSearchData(Model.Search.PaymentSearchNew search)
        {
            var searchResult = new SearchResult<Model.Search.BaseSearch, SocietyModel>(search, new List<SocietyModel>(), o => o.SocietyName.Contains(search.SarchText));

            try
            {
                var SocietyList = ToolsSetingBAL.GetAllSocietyes_PaymentList(CurrentParlourId);
                SocietyList = search.StatusId != Guid.Empty ? SocietyList.Where(x => x.parlourid.Equals(search.StatusId)).ToList() : SocietyList;
                return Json(new SearchResult<Model.Search.BaseSearch, GroupPaymentList>(search, SocietyList, o => o.GroupName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, SocietyModel>.Error(searchResult, ex));
            }
        }

    }
}