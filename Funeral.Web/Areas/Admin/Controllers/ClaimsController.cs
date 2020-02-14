using Funeral.BAL;
using Funeral.Model;
using Funeral.Model.Search;
using Funeral.Web.Areas.Admin.Models;
using Funeral.Web.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static Funeral.Model.FuneralEnum;
namespace Funeral.Web.Areas.Admin.Controllers
{
    public class ClaimsController : BaseAdminController
    {
        // GET: Admin/Claims
        #region Claim Main View
        public ActionResult Index(string Status)
        {
            ClaimSearch search = new ClaimSearch();
            search.CompanyId = new Guid(CurrentParlourId.ToString());
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = "";
            search.SortBy = "";
            search.SocietyID = "";
            search.SortOrder = "Asc";
            search.StatusId = Status == null ? "0" : Status;
            search.TotalRecord = 0;
            search.DateFrom = DateTime.Now.AddYears(-1);
            search.DateTo = DateTime.Now;

            List<StatusModel> statusList = new List<StatusModel>();
            var StatusId = Request.Params["StatusId"];
            if (StatusId == null)
            {
                statusList = Status == null ? ClaimsBAL.GetClaimsStatus() : ClaimsBAL.GetClaimsStatus().Where(x => x.Status == Status).ToList();
                search.SearchType = "Normal";
            }
            else
            {
                search.SearchType = StatusId;
                if (!ClaimStatusTypes.Where(x => x.Key.Equals(StatusId)).FirstOrDefault().Value.ToString().Equals("All"))
                {
                    var GetStatusId = ClaimStatusTypes.Where(x => x.Key.Equals(StatusId)).FirstOrDefault().Value.ToString().Split(',').ToList();
                    HashSet<int> StatusIdCollection = new HashSet<int>(GetStatusId.Select(s => Convert.ToInt32(s)));
                    statusList = ClaimsBAL.GetClaimsStatus().Where(m => StatusIdCollection.Contains(m.ID)).ToList();
                }
                else
                    statusList = ClaimsBAL.GetClaimsStatus();
            }
            ViewBag.SocietyLists = CommonBAL.GetSocietyByParlourId(CurrentParlourId);
            ViewBag.Statuses = statusList;
            LoadEntriesCount();
            BindCompanyList();
            var bankList = CommonBAL.GetBankList();
            ViewBag.Banks = bankList;
            ViewBag.Provinces = CommonBAL.GetProvinces();
            var AccountList = CommonBAL.GetAccountTypeList();
            ViewBag.Accounts = AccountList;
            var searchResult = new Funeral.Model.SearchResult<Model.Search.ClaimSearch, ClaimsModel>(search, new List<ClaimsModel>(),
                o => o.ClaimNotes.Contains(search.SarchText)
                || o.MemberNumber.Contains(search.SarchText)
                || o.CourseOfDearth.Contains(search.SarchText)
                || o.ClaimantTitle.Contains(search.SarchText)
                || o.ClaimantFullname.Contains(search.SarchText)
                || o.ClaimantSurname.Contains(search.SarchText)
                || o.ClaimantIDNumber.Contains(search.SarchText)
                || o.ClaimantAddressLine1.Contains(search.SarchText)
                || o.ClaimantAddressLine2.Contains(search.SarchText)
                || o.ClaimantAddressLine3.Contains(search.SarchText)
                || o.ClaimantAddressLine4.Contains(search.SarchText)
                || o.ClaimantCode.Contains(search.SarchText)
                || o.ClaimantContactNumber.Contains(search.SarchText));
            return View(search);
        }
        #endregion
        #region LoadEntriesCount
        public void LoadEntriesCount()
        {
            List<KeyValue> keyValues = new List<KeyValue>();
            List<SelectListItem> entriesItems = new List<SelectListItem>();
            keyValues.Add(new KeyValue { Key = "10", Value = "10" });
            keyValues.Add(new KeyValue { Key = "20", Value = "20" });
            keyValues.Add(new KeyValue { Key = "25", Value = "25" });
            keyValues.Add(new KeyValue { Key = "50", Value = "50" });
            keyValues.Add(new KeyValue { Key = "100", Value = "100" });
            keyValues.Add(new KeyValue { Key = "200", Value = "200" });
            keyValues.Add(new KeyValue { Key = "250", Value = "250" });
            keyValues.Add(new KeyValue { Key = "500", Value = "500" });
            ViewBag.EntriesCount = keyValues;
        }
        #endregion
        #region BindCompanyList
        public void BindCompanyList()
        {
            List<SelectListItem> companyListItems = new List<SelectListItem>();
            List<ApplicationSettingsModel> model = new List<ApplicationSettingsModel>();
            if (this.IsAdministrator)
            {
                model = ToolsSetingBAL.GetAllApplicationList(ParlourId, 1, 0).ToList();
                if (model == null)
                    model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
            }
            else
            {
                model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
            }

            ViewBag.Companies = model;
        }
        #endregion
        #region SearchData
        [HttpPost]
        public ActionResult SearchData(ClaimSearch search)
        {
            DateTime Datefrom = Convert.ToDateTime("01/01/1753");
            var dateAndTime = DateTime.Now;
            DateTime DateTo = dateAndTime.Date;
            if (search.DateFrom != null || search.DateTo != null)
            {
                Datefrom = search.DateFrom;
                dateAndTime = search.DateTo;

            }

            var getExpectedClaimStatus = ClaimsBAL.GetExpectedClaimStatusList(UserID);
            HashSet<string> expectedStatus = new HashSet<string>(getExpectedClaimStatus.Select(s => s.Status));
            var searchResult = new Funeral.Model.SearchResult<Model.Search.ClaimSearch, ClaimsModel>(search, new List<ClaimsModel>(),
               o => (o.ClaimNotes.Contains(search.SarchText)
               || o.Status.ToLower().Contains(search.StatusId.ToLower())
               || o.MemberNumber.Contains(search.SarchText)
               || o.CourseOfDearth.Contains(search.SarchText)
               || o.ClaimantTitle.Contains(search.SarchText)
               || o.ClaimantFullname.Contains(search.SarchText)
               || o.ClaimantSurname.Contains(search.SarchText)
               || o.ClaimantIDNumber.Contains(search.SarchText)
               || o.ClaimantAddressLine1.Contains(search.SarchText)
               || o.ClaimantAddressLine2.Contains(search.SarchText)
               || o.ClaimantAddressLine3.Contains(search.SarchText)
               || o.ClaimantAddressLine4.Contains(search.SarchText)
               || o.ClaimantCode.Contains(search.SarchText)
               || expectedStatus.Contains(o.Status)
               || o.ClaimantContactNumber.Contains(search.SarchText)) && (o.CreatedDate >= Datefrom && o.CreatedDate <= dateAndTime));
            try
            {
                List<ClaimsModel> members = new List<ClaimsModel>();
                members = ClaimsBAL.SelectAllClaims(search.CompanyId).Where(x => x.Status.Contains(search.StatusId == "0" ? "" : search.StatusId)).ToList();
                if (!string.IsNullOrEmpty(search.SocietyID) && search.SocietyID != "0")
                    members = members.Where(x => x.SocietyID.ToString() == search.SocietyID).ToList();
                members = IsAdministrator == true || IsSuperUser == true ? members : members = members.Where(m => (expectedStatus.Contains(m.Status))).ToList();

                if (search.SearchType != null && search.SearchType != "Normal")
                {
                    if (!ClaimStatusTypes.Where(x => x.Key.Equals(search.SearchType)).FirstOrDefault().Value.ToString().Equals("All"))
                    {
                        var GetStatusId = ClaimStatusTypes.Where(x => x.Key.Equals(search.SearchType)).FirstOrDefault().Value.ToString().Split(',').ToList();
                        HashSet<int> StatusIdCollection = new HashSet<int>(GetStatusId.Select(s => Convert.ToInt32(s)));
                        var statusList = ClaimsBAL.GetClaimsStatus().Where(m => StatusIdCollection.Contains(m.ID)).Select(x => x.Status).ToList();
                        members = members.Where(m => statusList.Contains(m.Status)).ToList();
                    }
                    else
                    {
                        members = members.Where(m => m.AssignedTo == 0).ToList();
                    }
                }

                var searchDatatable = new Funeral.Model.SearchResult<Model.Search.ClaimSearch, ClaimsModel>(search, members, o => (o.ClaimNotes.Contains(search.SarchText.ToLower())
                    || o.ClaimNotes.ToLower().Contains(search.SarchText.ToLower())
                    || o.CourseOfDearth.ToLower().Contains(search.SarchText.ToLower())
                    || o.ClaimantTitle.ToLower().Contains(search.SarchText.ToLower())
                    || o.ClaimantFullname.ToLower().Contains(search.SarchText.ToLower())
                    || o.ClaimantSurname.ToLower().Contains(search.SarchText.ToLower())
                    || o.ClaimantIDNumber.ToLower().Contains(search.SarchText.ToLower())
                    || o.ClaimantAddressLine1.ToLower().Contains(search.SarchText.ToLower())
                    || o.ClaimantAddressLine2.ToLower().Contains(search.SarchText.ToLower())
                    || o.ClaimantAddressLine3.ToLower().Contains(search.SarchText.ToLower())
                    || o.ClaimantAddressLine4.ToLower().Contains(search.SarchText.ToLower())
                    || o.ClaimantCode.ToLower().Contains(search.SarchText.ToLower())
                    || o.ClaimantContactNumber.ToLower().Contains(search.SarchText.ToLower())) && (o.CreatedDate >= Datefrom && o.CreatedDate <= dateAndTime));
                return Json(searchDatatable, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(WebApiResult<ClaimSearch, ClaimsModel>.Error(searchResult, ex));
            }
        }
        #endregion
        #region DownloadCliamDocument
        [HttpPost]
        public JsonResult DownloadCliamDocument(int pkiClaimPictureID)
        {
            Response.Redirect("../../Handler/DocumentHandler.ashx?DocClaimID=" + pkiClaimPictureID);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Delete
        public ActionResult Delete(int pkiClaimID)
        {
            int memberId = ClaimsBAL.DeleteClaimByID(pkiClaimID);
            #region ClaimHistory
            ClaimsBAL.SaveClaimHistory(IPAddress, pkiClaimID, StaticMessages.ClaimDeleted, UserName, CurrentParlourId);
            #endregion
            return RedirectToAction("Index", "Claims");
        }
        #endregion
        #region SearchDocumentData
        [HttpPost]
        public ActionResult SearchDocumentData(int claimID)
        {
            var claimDoc = ClaimsBAL.SelectClaimDocumentsByClaimId(claimID);
            return Json(claimDoc, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region GetMemberOrDependent
        [HttpGet]
        public ActionResult GetMemberOrDependentPartial(bool chkMember, string keyword, string CompanyId)
        {
            List<MembersModel> Mmodel = null;
            List<FamilyDependencyModel> Dmodel = null;
            Guid Company = new Guid();
            Company = CompanyId == null ? ParlourId : new Guid(CompanyId);
            try
            {
                if (keyword != "" || keyword != string.Empty || Company != Guid.Empty)
                {
                    if (chkMember == true)
                    {
                        Mmodel = ClaimsBAL.SelectMembersAndDependencies1(Company, true, keyword);
                        foreach (var search in Mmodel)
                        {
                            ViewBag.MemberId = search.pkiMemberID;
                        }
                    }
                    else
                    {
                        Dmodel = ClaimsBAL.SelectMembersAndDependencies2(Company, false, keyword);
                    }

                }
            }
            catch (Exception e)
            {

            }
            Tuple<List<MembersModel>, List<FamilyDependencyModel>> memberAndDependent = new Tuple<List<MembersModel>, List<FamilyDependencyModel>>(Mmodel, Dmodel);
            return PartialView("_MemberDependentSearchResult", memberAndDependent);
        }
        #endregion
        #region SendClaimMail
        [HttpPost]
        public JsonResult SendClaimMail(int ClaimId, int MemberID, string Email)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            bool IsSent = false;
            try
            {
                ReportViewer ReportViewerdata = new ReportViewer();

                LocalReport localReport = new LocalReport();

                MembersModel objmodel = ClaimsBAL.selectMemberByPkidAndParlor(ParlourId, MemberID);
                if (objmodel != null)
                {
                    var GetClaimMailReport = ClaimsBAL.GetClaimMailReport(objmodel, MemberID, ParlourId, ClaimId);
                    ReportViewerdata.Visible = true;
                    localReport.EnableExternalImages = true;
                    localReport.ReportPath = Server.MapPath("~/Admin/Reports/ReportLayouts/ReservationClaim.rdlc");//Server.MapPath("~/Admin/Reports/ReportLayouts/ReservationClaim.rdlc");
                    localReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
                    localReport.DataSources.Add(new ReportDataSource("dsGetPlanDetailsByPlanId", GetClaimMailReport.Item1));
                    localReport.DataSources.Add(new ReportDataSource("dsMemberSelectList", GetClaimMailReport.Item2));
                    localReport.DataSources.Add(new ReportDataSource("DsClaimReoprt", GetClaimMailReport.Item3));
                    localReport.DataSources.Add(new ReportDataSource("dsFuneralDataset", GetClaimMailReport.Item4));
                    localReport.DataSources.Add(new ReportDataSource("dsTermsAndConditionOfApplication", GetClaimMailReport.Item5));
                    localReport.EnableHyperlinks = true;
                    localReport.Refresh();


                    //ReportParameterCollection reportParameters = new ReportParameterCollection();
                    //reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
                    //ReportViewerdata.LocalReport.SetParameters(reportParameters);
                    byte[] bytes = localReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                    MemoryStream memoryStream = new MemoryStream(bytes);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    System.Net.Mail.Attachment claimdoc = new System.Net.Mail.Attachment(memoryStream, "ClaimDoc CLMNo" + ClaimId.ToString() + ".pdf");
                    List<ClaimDocumentModel> documentModel = ClaimsBAL.SelectClaimDocumentsByClaimId(Convert.ToInt32(ClaimId)).ToList();
                    IsSent = ClaimsBAL.SendClaimEmail(ClaimId, Email, documentModel, claimdoc);
                }
                return Json(IsSent, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(IsSent, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region ClaimStatusPartial
        [HttpGet]
        public ActionResult ClaimStatusPartial(int ClaimId, Guid ParlourId)
        {
            ClaimsModel data = ClaimsBAL.SelectClaimsBypkid(ClaimId, ParlourId);
            #region New Status Process
            var AllStatus = ClaimsBAL.GetClaimsStatus();
            var RightsWiseStatus = ClaimsBAL.GetClaimsStatus(UserID);
            var SequencePriority = (AllStatus.Where(m => m.Status == data.Status).Select(mm => mm.SequencePriority).FirstOrDefault() + 1);
            SequencePriority = SequencePriority > AllStatus.Max(x => x.SequencePriority) ? AllStatus.Max(x => x.SequencePriority) : SequencePriority;
            var NewStatus = IsAdministrator == true || IsSuperUser == true ? RightsWiseStatus.Where(x => x.SequencePriority == SequencePriority).Select(x => x.Status).FirstOrDefault() : AllStatus.Where(x => x.SequencePriority == SequencePriority).Select(x => x.Status).FirstOrDefault();

            int BackSequencePriority = (SequencePriority - 1) < 0 ? 1 : (SequencePriority - 1);
            var AllStatuses = AllStatus.Where(d => d.SequencePriority >= BackSequencePriority && d.SequencePriority <= SequencePriority).ToList();
            var RightsStatuses = RightsWiseStatus.Where(d => d.SequencePriority >= BackSequencePriority && d.SequencePriority <= SequencePriority).ToList();
            #endregion

            ChangeStatusReason changeStatus = new ChangeStatusReason();
            changeStatus.fkiMemberId = data.fkiMemberID;
            changeStatus.ClaimID = data.pkiClaimID;
            changeStatus.NewStatus = data.Status;
            changeStatus.CurrentStatus = data.Status;
            changeStatus.ClaimCreatedDate = data.CreatedDate;
            changeStatus.ParlourID = data.parlourid;
            changeStatus.ClaimNotes = data.ClaimNotes;
            //ViewBag.Statuses = IsSuperUser == true || IsAdministrator == true ? AllStatus.Where(x => x.SequencePriority == SequencePriority).ToList() : RightsWiseStatus.Where(x => x.SequencePriority == SequencePriority).ToList();
            ViewBag.Statuses = IsSuperUser == true || IsAdministrator == true ? AllStatuses : RightsStatuses;
            ViewBag.ClaimReasonList = ClaimsBAL.GetClaimReasonList(ParlourId);
            return PartialView("_ClaimStatusForm", changeStatus);
        }
        #endregion
        #region ChangeClaimStatus
        [HttpPost]
        public ActionResult ChangeClaimStatus(ChangeStatusReason changeStatus)
        {
            if (ModelState.IsValid)
            {
                changeStatus.ChangedBy = this.UserID;
                ClaimsBAL.ClaimStatusChangeReason(changeStatus);
                ClaimsBAL.UpdateClaimStatus(changeStatus.ClaimID, changeStatus.NewStatus);
                var applicationSettings = ToolsSetingBAL.GetApplictionByParlourID(changeStatus.ParlourID);
                var fromMail = ConfigurationManager.AppSettings["ReportEmailSenderId"].ToString().Trim();
                #region ClaimHistory
                ClaimsBAL.SaveClaimHistory(IPAddress, changeStatus.ClaimID, StaticMessages.ClaimStatusUpdated, UserName, changeStatus.ParlourID);
                #endregion
                if (applicationSettings != null && fromMail != null)
                {
                    string msg = "";
                    msg += "Please be aware of your claims status changed from <b>" + changeStatus.CurrentStatus + "</b> to <b>" + changeStatus.NewStatus + "</b><br/><br/>";
                    msg += "<b>Changed Reason : </b>" + changeStatus.ChangeReason + "<br />";
                    msg += "<b>Comment : </b>" + changeStatus.Comment + "<br />";
                    msg += "<b>Claim Assigned to  : </b>" + UserName + "<br/>";
                    msg += "<b>Claim Create Date : </b>" + changeStatus.ClaimCreatedDate.ToString("dd MMM yyyy") + "<br />";
                    msg += "<b>Claim No. : </b>" + changeStatus.ClaimID + "<br />";
                    msg += "<b>Claim Notes : </b>" + changeStatus.ClaimNotes + "<br />";
                    msg += "<br /><br />Regards,<br />ARL Claims Team <br/> claims @africanrainbowlife.co.za <br /> Service call centre: 010 880 5055";
                    string subject = "ARL Claim No " + changeStatus.ClaimID + "  - Claims Status Changed";
                    ClaimsBAL.SendMail_StatusChanged(applicationSettings.EmailForClaimNotification, fromMail, applicationSettings.ApplicationName, subject, msg);
                }
            }
            return RedirectToAction("Index", "Claims");
        }
        #endregion
        #region ClaimStatusHistory
        public ActionResult ClaimStatusHistory(int pkiClaimID, int MemberId, Guid parlourId)
        {
            ClaimStatusHistoryModal claimStatusHistoryModal = new ClaimStatusHistoryModal();
            claimStatusHistoryModal.claimStatusHistory = ClaimsBAL.GetClaimStatusHistories(pkiClaimID);
            claimStatusHistoryModal.claimsModel = ClaimsBAL.SelectClaimsBypkid(pkiClaimID, parlourId);
            claimStatusHistoryModal.funeralModel = FuneralBAL.GetFuneralByClaimId(pkiClaimID);
            claimStatusHistoryModal.memberInvoices = MembersBAL.GetInvoicesByMemberID(parlourId, MemberId);
            claimStatusHistoryModal.claimDocuments = ClaimsBAL.GetClaimDocumentsByClaimId(pkiClaimID, parlourId, claimStatusHistoryModal.funeralModel.MemberType);
            return View(claimStatusHistoryModal);
        }
        #endregion
        #region ClaimStatusHistoryPartial
        [HttpGet]
        public ActionResult ClaimStatusHistoryPartial(int ClaimId)
        {
            ClaimsModel data = ClaimsBAL.SelectClaimsBypkid(ClaimId, CurrentParlourId);
            return PartialView("_ClaimStatusHistoryDetails", data);
        }
        #endregion
        #region ClaimAddEdit
        [HttpGet]
        public ActionResult ClaimAddEdit(int pkiClaimID)
        {
            ClaimandFuneralModel claimandFuneral = new ClaimandFuneralModel();
            if (pkiClaimID == 0)
            {
                ClaimsModel claimsModel = new ClaimsModel();
                claimsModel.pkiClaimID = pkiClaimID;
                claimsModel.parlourid = CurrentParlourId;
                claimandFuneral.claimsModel = claimsModel;
                claimandFuneral.funeralModel = new FuneralModel();
            }
            else
            {
                claimandFuneral.PaymentHistoryList = MembersBAL.GetInvoicesByMemberID(CurrentParlourId, MemberId);
                claimandFuneral.claimStatusHistory = ClaimsBAL.GetClaimStatusHistories(pkiClaimID);
                claimandFuneral.claimsModel = ClaimsBAL.SelectClaimsBypkid(pkiClaimID, CurrentParlourId);
                claimandFuneral.funeralModel = FuneralBAL.GetFuneralByClaimId(pkiClaimID);
                if (claimandFuneral.funeralModel != null)
                    claimandFuneral.ClaimDocumentList = ClaimsBAL.GetClaimDocumentsByClaimId(pkiClaimID, CurrentParlourId, claimandFuneral.funeralModel.MemberType);
                else
                    claimandFuneral.ClaimDocumentList = ClaimsBAL.GetClaimDocumentsByClaimId(pkiClaimID, CurrentParlourId, "Main Member");
            }
            ViewBag.SocietyLists = CommonBAL.GetSocietyByParlourId(CurrentParlourId);
            ViewBag.Statuses = ClaimsBAL.GetClaimsStatus();
            ViewBag.Banks = CommonBAL.GetBankList();
            ViewBag.Provinces = CommonBAL.GetProvinces();
            ViewBag.Accounts = CommonBAL.GetAccountTypeList();
            LoadEntriesCount();
            BindCompanyList();
            return View(claimandFuneral);
        }
        [HttpPost]
        public ActionResult ClaimAddEdit(ClaimandFuneralModel claimandFuneral)
        {
            string errorMsg = "";
            if (ModelState.IsValid)
            {

                claimandFuneral.claimsModel.parlourid = claimandFuneral.claimsModel.parlourid == Guid.Empty ? CurrentParlourId : claimandFuneral.claimsModel.parlourid;
                claimandFuneral.funeralModel.parlourid = claimandFuneral.claimsModel.parlourid;
                claimandFuneral.claimsModel.LoggedBy = UserName;
                claimandFuneral.claimsModel.ModifiedUser = UserName;
                claimandFuneral.claimsModel.BeneficiaryBank = claimandFuneral.claimsModel.BeneficiaryBank == null ? "" : claimandFuneral.claimsModel.BeneficiaryBank;

                if (claimandFuneral.claimsModel.ClaimDate == null || claimandFuneral.claimsModel.ClaimDate == DateTime.MinValue)
                    claimandFuneral.claimsModel.ClaimDate = DateTime.MaxValue;

                claimandFuneral.claimsModel.CreatedBy = UserID;
                int ClaimID = ClaimsBAL.SaveClaims(claimandFuneral.claimsModel);
                Session["pkClaimId"] = ClaimID.ToString();

                //Decease
                claimandFuneral.funeralModel.LastModified = System.DateTime.Now;
                claimandFuneral.funeralModel.ModifiedUser = UserName;
                claimandFuneral.funeralModel.MemeberNumber = claimandFuneral.claimsModel.MemberNumber;
                claimandFuneral.funeralModel.FkiClaimID = ClaimID;
                int desId = FuneralBAL.SaveFuneral(claimandFuneral.funeralModel);
                errorMsg += "Record Inserted Successfully";
                #region ClaimHistory
                if (claimandFuneral.claimsModel.pkiClaimID > 0)
                    ClaimsBAL.SaveClaimHistory(IPAddress, claimandFuneral.claimsModel.pkiClaimID, StaticMessages.ClaimUpdated, UserName, claimandFuneral.claimsModel.parlourid);
                else
                    ClaimsBAL.SaveClaimHistory(IPAddress, ClaimID, StaticMessages.ClaimCreated, UserName, claimandFuneral.claimsModel.parlourid);
                #endregion
                return RedirectToAction("Index", "Claims");
            }
            else
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errorMsg += "<li>" + error.ErrorMessage + "</li>";
                    }
                }
                TempData["message"] = ShowMessage(MessageType.Success, errorMsg);
                return RedirectToAction("ClaimAddEdit", "Claims", new { pkiClaimID = claimandFuneral.claimsModel.pkiClaimID });
            }
        }
        #endregion
        #region Claim Dashboard
        public ActionResult ClaimDashboard()
        {
            dynamic model = new ExpandoObject();
            model.getStatusCount = ClaimsBAL.GetStatusCountList_Dashboard(ParlourId);
            return View(model);
        }
        #endregion
        #region Upload Claim Document
        [HttpGet]
        public ActionResult ClaimDocumentPartial(int ClaimId, Guid parlourId, int pkiClaimPictureID)
        {
            ClaimsModel data = ClaimsBAL.SelectClaimsBypkid(ClaimId, parlourId);
            ClaimDocument claimDocument = new ClaimDocument();
            claimDocument.ParlourId = data.parlourid;
            claimDocument.fkiMemberId = data.fkiMemberID;
            claimDocument.PkiClaimId = data.pkiClaimID;
            claimDocument.Status = ClaimDocumentStatusEnum.Pending.ToString();
            claimDocument.DocumentId = pkiClaimPictureID;
            return PartialView("_UploadClaimDocumentForm", claimDocument);
        }
        [HttpPost]
        public ActionResult SaveClaimDocument(ClaimDocument document, HttpPostedFileBase fuSupportDocument)
        {
            try
            {
                if (fuSupportDocument != null)
                {
                    string fileName = Path.GetFileName(fuSupportDocument.FileName);
                    var path = (dynamic)null;
                    string Str = System.DateTime.Now.Ticks.ToString();
                    path = 1012 + "_" + Str + "_" + fileName;
                    string fname = Server.MapPath("~/Upload/FuneralDocument/" + ParlourId + "/");
                    if (!Directory.Exists(fname))
                        Directory.CreateDirectory(fname);
                    fname = fname + path;
                    fuSupportDocument.SaveAs(fname);
                    string dbPath = "~/Upload/FuneralDocument/" + ParlourId + "/" + path;
                    ClaimDocumentModel claimDoc = new ClaimDocumentModel();
                    claimDoc.ImageName = fileName;
                    Byte[] docPath = Encoding.ASCII.GetBytes(dbPath);
                    claimDoc.ImageFile = docPath;
                    claimDoc.fkiClaimID = Convert.ToInt32(document.PkiClaimId);
                    claimDoc.Parlourid = document.ParlourId;
                    claimDoc.DocType = document.DocumentId;
                    claimDoc.Status = ClaimDocumentStatusEnum.Pending.ToString();
                    claimDoc.DocContentType = fuSupportDocument.ContentType;
                    claimDoc.CreateDate = System.DateTime.Now;
                    claimDoc.ModifiedUser = UserName;
                    claimDoc.LastModified = System.DateTime.Now;
                    int docID = ClaimsBAL.SaveClaimSupportedDocument(claimDoc);
                    #region ClaimHistory
                    ClaimsBAL.SaveClaimHistory(IPAddress, document.PkiClaimId, StaticMessages.ClaimDocumentUploaded, UserName, document.ParlourId);
                    #endregion
                }
                TempData["message"] = ShowMessage(MessageType.Success, "Claim Document file uploaded successfully");
            }
            catch (Exception e)
            {
                TempData["message"] = ShowMessage(MessageType.Danger, "Claim Document file Could not be uploaded");
            }
            return RedirectToAction("ClaimAddEdit", "Claims", new { pkiClaimID = document.PkiClaimId });
        }
        public ActionResult DeleteClaimDocument(int pkiClaimPictureID, int PkiClaimId)
        {
            try
            {
                ClaimsBAL.DeleteClaimsdocumentById(pkiClaimPictureID);
                #region ClaimHistory
                ClaimsBAL.SaveClaimHistory(IPAddress, PkiClaimId, StaticMessages.ClaimDocumentDeleted, UserName, CurrentParlourId);
                #endregion
                TempData["message"] = ShowMessage(MessageType.Success, "Claim Document deleted successfully");
            }
            catch (Exception e)
            {
                TempData["message"] = ShowMessage(MessageType.Danger, "Claim Document could not deleted");
            }
            return RedirectToAction("ClaimAddEdit", "Claims", new { pkiClaimID = PkiClaimId });
        }
        public ActionResult ClaimDocumentStatusChange(int DocumentId, int ClaimId)
        {
            try
            {
                ClaimsBAL.ClaimDocumentStatusUpdated(DocumentId, UserName, ClaimId);
                TempData["message"] = ShowMessage(MessageType.Success, "Claim Document Status has been updated successfully");
            }
            catch (Exception e)
            {
                TempData["message"] = ShowMessage(MessageType.Danger, "Claim Document Status could not updated");
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        #endregion
        #region ClaimDocumentStatusPartial
        [HttpGet]
        public ActionResult ClaimDocumentStatusPartial(int pkiClaimPictureID, Guid ParlourId)
        {
            var data = ClaimsBAL.GetClaimDocumentsByDocumentId(pkiClaimPictureID, ParlourId);
            var DocumentStatusList = Enum.GetValues(typeof(ClaimDocumentStatusEnum)).Cast<ClaimDocumentStatusEnum>().Select(v => new SelectListItem { Text = v.ToString(), Value = v.ToString() }).ToList();
            ChangeStatusReason changeStatus = new ChangeStatusReason();
            changeStatus.ID = data.pkiClaimPictureID;
            changeStatus.ClaimID = data.fkiClaimID;
            changeStatus.NewStatus = data.Status;
            changeStatus.CurrentStatus = data.Status;
            changeStatus.ParlourID = data.Parlourid;
            ViewBag.Statuses = DocumentStatusList;
            return PartialView("_DocumentStatusForm", changeStatus);
        }
        [HttpPost]
        public ActionResult UpdateClaimDocumentStatus(ChangeStatusReason model)
        {
            ClaimsBAL.UpdateClaimDocument(model);
            string Comment = "Old Status : " + model.CurrentStatus + "," + System.Environment.NewLine + "New Status : " + model.NewStatus + "," + System.Environment.NewLine + "Comment : " + model.Comment;
            #region Document FollowUp
            ClaimFollowUp followUp = new ClaimFollowUp();
            followUp.ClaimId = model.ClaimID;
            followUp.pkiClaimPictureID = model.ID;
            followUp.Comment = Comment;
            followUp.FollowUpType = FollowUpTypesEnum.Document.ToString();
            followUp.CreatedBy = UserName;
            followUp.ParlourId = model.ParlourID;
            ClaimsBAL.AddClaimFollowUp(followUp);
            #endregion

            #region ClaimHistory
            ClaimsBAL.SaveClaimHistory(IPAddress, model.ClaimID, StaticMessages.ClaimDocumentStatusChanged, UserName, model.ParlourID);
            #endregion
            return Redirect(Request.UrlReferrer.ToString());
        }

        #endregion
        #region Document Status History
        public ActionResult ClaimDocumentHistoryPartial(int pkiClaimPictureID, Guid ParlourId)
        {
            var data = ClaimsBAL.GetDocumentFollowUpHistory(pkiClaimPictureID, ParlourId);
            return PartialView("_DocumentFollowUpHistory", data);
        }
        #endregion
        #region ClaimAssignedToPartial
        [HttpGet]
        public ActionResult ClaimAssignedToPartial(int ClaimId, Guid ParlourId)
        {
            ClaimsModel data = ClaimsBAL.SelectClaimsBypkid(ClaimId, ParlourId);
            ClaimAssigned claimAssigned = new ClaimAssigned();
            claimAssigned.ClaimId = data.pkiClaimID;
            claimAssigned.CurrentAssigned = data.AssignedTo;
            claimAssigned.NewAssigned = data.AssignedTo;
            claimAssigned.ParlourId = ParlourId;
            ViewBag.AllUserList = CommonBAL.GetAllUser(ParlourId);
            return PartialView("_ChangeAssignedForm", claimAssigned);
        }
        [HttpPost]
        public ActionResult ChangeClaimAssigned(ClaimAssigned claimAssigned)
        {
            ClaimsBAL.ChangeClaimAssigned(claimAssigned);
            TempData["message"] = ShowMessage(MessageType.Success, "Claim Assigned successfully");
            string UserName = CommonBAL.GetAllUser(claimAssigned.ParlourId).FirstOrDefault(x => x.PkiUserID == claimAssigned.NewAssigned).EmployeeFullname;
            String message = String.Format(StaticMessages.ClaimAssigned, claimAssigned.ClaimId, UserName);
            ClaimsBAL.SaveClaimHistory(IPAddress, claimAssigned.ClaimId, message, UserName, claimAssigned.ParlourId);
            return Redirect(Request.UrlReferrer.ToString());
        }
        #endregion
        #region External Link Send
        public ActionResult SendExternalLink(int pkiClaimID, Guid parlourId)
        {
            var applicationSettings = ToolsSetingBAL.GetApplictionByParlourID(parlourId);
            ClaimsModel data = ClaimsBAL.SelectClaimsBypkid(pkiClaimID, ParlourId);
            if (applicationSettings != null && data.Email != null)
            {
                Guid NewTokenId = Guid.NewGuid();
                ExternalUserLink externalUserLink = new ExternalUserLink();
                externalUserLink.ClaimId = data.pkiClaimID;
                externalUserLink.Email = data.Email;
                externalUserLink.ExternalToken = NewTokenId;
                externalUserLink.parlourId = data.parlourid;
                externalUserLink.SentBy = UserName;
                ClaimsBAL.SaveExternalLink(externalUserLink);
                #region Send Email
                var ExternalLink = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery.ToString(), "/") + "ExternalUser/Index?ExternalToken=" + NewTokenId;
                var fromMail = ConfigurationManager.AppSettings["ReportEmailSenderId"].ToString().Trim();
                string msg = "";
                msg += "Please be aware of below your one time access claims external link generate<br/><br/>";
                msg += "<b>here External link  :</b>";
                msg += "<br/><a href=" + ExternalLink + " tital='External User link' class='btn btn-primary'>Click here external link</a><br /><br/>";
                msg += "<br /><br />Regards,<br />ARL Claims Team <br/> claims @africanrainbowlife.co.za <br /> Service call centre: 010 880 5055";
                string subject = "ARL Claim No " + data.pkiClaimID + "  - Claims External Link";
                #endregion
                ClaimsBAL.SendMail_StatusChanged(data.Email, fromMail, applicationSettings.ApplicationName, subject, msg);
                ClaimsBAL.SaveClaimHistory(IPAddress, pkiClaimID, String.Format(StaticMessages.ClaimExternalLink, pkiClaimID, data.Email), UserName, parlourId);
                TempData["message"] = ShowMessage(MessageType.Success, "External link has been generated and sent");
            }
            else
            {
                TempData["message"] = ShowMessage(MessageType.Danger, "External link has not been generated beacause claim email Could not be find");
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        #endregion
        #region BindDependentUpdate
        [HttpPost]
        public JsonResult BindDependentUpdate(int dependentId, string CompanyId)
        {
            Guid Company = new Guid(CompanyId);
            FamilyDependencyModel objmodel = MembersBAL.SelectFamilyDependencyById(dependentId);
            MembersModel obj = ClaimsBAL.selectMemberByPkidAndParlor(Company, objmodel.MemberId);
            MemberId = objmodel.MemberId;
            return Json(new { FamilyDependencyModel = objmodel, MembersModel = obj }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region BindMainMemberUpdate
        [HttpPost]
        public JsonResult BindMainMemberUpdate(int memberId, string CompanyId)
        {
            Guid Company = new Guid(CompanyId);
            MembersModel objmodel = ClaimsBAL.selectMemberByPkidAndParlor(Company, memberId);
            PlanModel objpan = ClaimsBAL.GetPlanDetailsByPlanId(objmodel.fkiPlanID);
            return Json(new { PlanModel = objpan, MembersModel = objmodel }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [HttpPost]
        public JsonResult BindGroupByCompanyId(Guid CompanyId)
        {
            var Company = CommonBAL.GetSocietyByParlourId(CompanyId).Select(x => new SelectListItem() { Text = x.SocietyName, Value = x.pkiSocietyID.ToString() });
            return Json(Company, JsonRequestBehavior.AllowGet);
        }
    }
}