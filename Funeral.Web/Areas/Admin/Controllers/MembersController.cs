using Funeral.BAL;
using Funeral.Model;
using Funeral.Model.Search;
using Funeral.Web.App_Start;
using Funeral.Web.Areas.Admin.Models.ViewModel;
using Funeral.Web.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Funeral.Web.niws_validation;
using System.Net.Mail;

//using Funeral.Model.Search;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public partial class MembersController : BaseAdminController
    {
        public MembersController() : base(4)
        {
            this.dbPageId = 4;
        }

        private readonly ISiteSettings _siteConfig = new SiteSettings();
        public static string GetdataPremium;

        [PageRightsAttribute(CurrentPageId = 4)]
        public ActionResult Index()
        {
            ViewBag.HasAccess = HasAccess;
            if (ViewBag.HasAccess == true)
            {
                var statusList = CommonBAL.GetStatus(FuneralEnum.StatusAssociatedTable.Members.ToString()).Select(x => new SelectListItem() { Text = x.ID.ToString(), Value = x.Status });
                ViewBag.StatusList = statusList;
                ViewBag.HasEditRight = HasEditRight;
                ViewBag.HasDeleteRight = HasDeleteRight;
                ViewBag.totalPremium = Currency;
                //ViewBag.SocietyLists = CommonBAL.GetBranchByParlourId(CurrentParlourId);
                ViewBag.SocietyLists = CommonBAL.GetSocietyByParlourId(CurrentParlourId);
                LoadStatus();
                LoadEntriesCount();
                BindCompanyList("Search");
                Model.Search.MemberSearch search = new Model.Search.MemberSearch();
                search.CompanyId = new Guid(CurrentParlourId.ToString());
                search.PageNum = 1;
                search.PageSize = 10;
                search.SarchText = "";
                search.SortBy = "";
                search.SortOrder = "Asc";
                search.StatusId = "0";
                search.TotalRecord = 0;
                search.BookID = "";

                var searchResult = new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, new List<MembersModel>(),o => o.IDNumber.Contains(search.SarchText)
                    || o.MemeberNumber.Contains(search.SarchText));
                return View(search);
            }
            else
            {
                return View();
            }

        }
        public void LoadStatus()
        {
            var statusList = CommonBAL.GetStatus(FuneralEnum.StatusAssociatedTable.Members.ToString()).Select(status => new { status.Status, status.ID }).Distinct();
            ViewBag.Statuses = statusList;
        }
        public void LoadEntriesCount()
        {
            List<KeyValue> keyValues = new List<KeyValue>();
            List<SelectListItem> entriesItems = new List<SelectListItem>();
            keyValues.Add(new KeyValue { Key = "10", Value = "10" });
            keyValues.Add(new KeyValue { Key = "20", Value = "20" });
            keyValues.Add(new KeyValue { Key = "50", Value = "50" });
            keyValues.Add(new KeyValue { Key = "25", Value = "25" });
            keyValues.Add(new KeyValue { Key = "100", Value = "100" });
            keyValues.Add(new KeyValue { Key = "200", Value = "200" });
            keyValues.Add(new KeyValue { Key = "250", Value = "250" });
            keyValues.Add(new KeyValue { Key = "500", Value = "500" });
            ViewBag.EntriesCount = keyValues;
        }
        [HttpPost]
        public ActionResult SearchData(Model.Search.MemberSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, new List<MembersModel>(), o => o.IDNumber.ToLower().Contains(search.SarchText.ToLower()));
            try
            {
                var members = MembersBAL.GetAllMembers(search.CompanyId, search.PageSize, search.PageNum, search.SarchText, search.SortBy, search.SortOrder, search.StatusId.ToString(), search.BookID);
                return Json(new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, members.MemberList, o => o.IDNumber.ToLower().Contains(search.SarchText.ToLower()) || o.MemeberNumber.ToLower().Contains(search.SarchText.ToLower()) || o.Surname.ToLower().Contains(search.SarchText.ToLower()) || o.FullNames.ToLower().Contains(search.SarchText.ToLower()) || o.Cellphone.ToLower().Contains(search.SarchText.ToLower()) || o.EasyPayNo.ToLower().Contains(search.SarchText.ToLower())));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.MemberSearch, MembersModel>.Error(searchResult, ex));
            }
        }

        //public ActionResult BindAuditTrail(m)
        //{

        //        var memberAudits = MembersBAL.GetMemberAudits();
        //        return Json(new Funeral.Model.AuditTrail<Model.AuditTrail;

        //}
        //[HttpPost]
        public ActionResult GetAllData(Model.Search.MemberSearch search)
        {
            return null;
        }
        [PageRightsAttribute(CurrentPageId = 4, Right = new isPageRight[] { isPageRight.HasDelete })]
        public ActionResult Delete(int pkiMemberID)
        {
            try
            {
                int memberId = MembersBAL.DeleteMember(pkiMemberID);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [PageRightsAttribute(CurrentPageId = 4)]
        public ActionResult ManageMembers(int pkiMemberID = 0, Guid? parlourId = null)
        {

            var guidIsEmpty = parlourId == Guid.Empty;
            if (parlourId != null)
            {
                CurrentParlourId = new Guid(parlourId.ToString());
            }

            BindCompanyList();
            if (pkiMemberID == 0)
            {
                if (!HasCreateRight)
                {
                    RedirectToErrorPage();
                }
            }
            if (pkiMemberID != 0)
            {
                if (!HasEditRight)
                {
                    RedirectToErrorPage();
                }
            }
            TempData["IsPolicyNoEnabled"] = true;

            ApplicationSettingsModel app;
            app = ToolsSetingBAL.GetApplictionByParlourID(CurrentParlourId);
            if (app != null)
            {
                if (app.IsAutoGeneratedPolicyNo == true)
                {
                    TempData["IsPolicyNoEnabled"] = false;
                }
            }



            MemberId = pkiMemberID;
            MembersModel member = new MembersModel();
            if (pkiMemberID != 0)
                member = MembersBAL.GetMemberByID(pkiMemberID, CurrentParlourId);
            else
                member.parlourid = CurrentParlourId;
            var Managemembers = new ManageMembersVM();

            ViewBag.AuditList = MembersBAL.GetAuditList(pkiMemberID, CurrentParlourId);

            string message = "";

            if (member.AccountNumberVerified == true)
            {
                message = "Valid,Bank Account";
                @ViewBag.BankValidation = message;
            }
            else
            {
                message = "Bank Account Invalid Or Empty";
                @ViewBag.BankValidation = message;
            }


            var statusList = CommonBAL.GetStatus(FuneralEnum.StatusAssociatedTable.Members.ToString()).Select(status => new { status.Status, status.ID }).Distinct();
            ViewBag.Statuses = statusList;

            BindBankMemberNumber();
            ValidateInception(ref member, pkiMemberID);
            ValidatePolicyNumber();
            ValidatePolicyDoc(pkiMemberID);
            ValidateCreateRights();
            //MemburNumber = CommonBAL.GetMemberNumber(ParlourId);

            if (member == null)
            {
                MemburNumber = string.Empty;
            }
            else
            {
                MemburNumber = member.MemeberNumber;
            }

            Managemembers.Currency = Currency;

            Managemembers.IsSuperUser = this.IsSuperUser;
            Managemembers.IsAdministrator = this.IsAdministrator;
            Managemembers.BankList = BanksBAL.SelectAll().Select(x => new SelectListItem() { Text = x.BankName, Value = x.BranchCode });
            Managemembers.AllAccountTypesList = BanksBAL.AccountTypeSelectAll().Select(x => new SelectListItem() { Text = x.AccountType, Value = x.AccountTypeID.ToString() });
            Managemembers.AgentList = MembersBAL.SelectAllAgent(CurrentParlourId).Select(x => new SelectListItem() { Text = x.Agent, Value = x.AgentID.ToString() });
            //Managemembers.UnderwritterList = MembersBAL.SelectAllUnderwritters(CurrentParlourId).Select(x => new SelectListItem() { Text = x.UnderwriterName, Value = x.pkiUnderWriterSetupId.ToString() });
            Managemembers.PolicyList = CommonBAL.GetPolicyByParlourId(CurrentParlourId).Select(x => new SelectListItem() { Text = x.PlanName, Value = x.pkiPlanID.ToString() });
            Managemembers.countryList = MembersBAL.GetCountry().Select(x => new SelectListItem() { Text = x.Name, Value = x.CountryCode });
            Managemembers.BranchList = CommonBAL.GetBranchByParlourId(CurrentParlourId).Select(x => new SelectListItem() { Text = x.BranchName, Value = x.Brancheid.ToString() });
            Managemembers.ProductAddOnList = MembersBAL.SelectProductName(CurrentParlourId).Select(x => new SelectListItem() { Text = x.ProductName, Value = x.pkiProductID.ToString() });
            Managemembers.SocietyList = CommonBAL.GetSocietyByParlourId(CurrentParlourId).Select(x => new SelectListItem() { Text = x.SocietyName, Value = x.pkiSocietyID.ToString() });
            Managemembers.CustomPaymentMethod = CustomDetailsBAL.GetAllCustomDetailsByParlourId(CurrentParlourId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.EmploymentType)).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
            Managemembers.CustomGrouping2 = CustomDetailsBAL.GetAllCustomDetailsByParlourId(CurrentParlourId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.PaymentType)).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
            Managemembers.CustomGrouping3 = CustomDetailsBAL.GetAllCustomDetailsByParlourId(CurrentParlourId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.Source)).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
            Managemembers.CustomGrouping4 = CustomDetailsBAL.GetAllCustomDetailsByParlourId(CurrentParlourId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.MaritalStatus)).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
            Managemembers.Member = member;
            //Managemembers.DependencyTypeList = CommonBAL.GetUserTypes().Select(x => new SelectListItem() { Text = x.UserTypeName, Value = x.UserTypeId.ToString() });
            Managemembers.DependencyTypeList = CommonBAL.GetUserTypesByMemberID(MemberId, member.fkiPlanID, CurrentParlourId).Select(x => new SelectListItem() { Text = x.UserTypeName, Value = x.CreatorID.ToString() });
            ViewBag.Provinces = CommonBAL.GetProvinces();
            Managemembers.ExtendedFamily = MembersBAL.GetExtendedFamilyList(CurrentParlourId, MemberId).Select(x => new SelectListItem() { Text = x.FullName, Value = x.pkiDependentID.ToString() });



            PolicyModel objPolicyModel = CommonBAL.GetPlanSubscriptionByPlanId(member.fkiPlanID, CurrentParlourId).ToList().FirstOrDefault();
            Managemembers.Member.PolicyPremium = objPolicyModel != null ? objPolicyModel.PlanSubscription : string.Empty;
            if (pkiMemberID != 0)
            {
                string totalPremium = BindTotalPremium(pkiMemberID);
                Managemembers.TotalPremium = float.Parse(totalPremium, CultureInfo.InvariantCulture.NumberFormat);
            }
            Session["Id"] = pkiMemberID;
            Managemembers.ProductSearch = new BaseSearch()
            {
                PageNum = 0,
                PageSize = 10,
                SarchText = "",
                SortBy = "",
                SortOrder = "Asc",
                TotalRecord = 0
            };
            Managemembers.InvoiceSearch = new BaseSearch()
            {
                PageNum = 0,
                PageSize = 10,
                SarchText = "",
                SortBy = "",
                SortOrder = "Asc",
                TotalRecord = 0
            };
            Managemembers.NoteSearch = new BaseSearch()
            {
                PageNum = 0,
                PageSize = 10,
                SarchText = "",
                SortBy = "",
                SortOrder = "Asc",
                TotalRecord = 0
            };
            Managemembers.DocumentSearch = new BaseSearch()
            {
                PageNum = 0,
                PageSize = 10,
                SarchText = "",
                SortBy = "",
                SortOrder = "Asc",
                TotalRecord = 0
            };
            Managemembers.DependancySearch = new BaseSearch()
            {
                PageNum = 0,
                PageSize = 10,
                SarchText = "",
                SortBy = "",
                SortOrder = "Asc",
                TotalRecord = 0
            };
            Managemembers.PolicySearch = new BaseSearch()
            {
                PageNum = 0,
                PageSize = 10,
                SarchText = "",
                SortBy = "",
                SortOrder = "Asc",
                TotalRecord = 0
            };
            return View(Managemembers);
        }
        private void BindBankMemberNumber()
        {
            //Guid ParlourId1 = new Guid("6dcba090-f363-47e6-93f5-6def8f80702e");
            MemburNumber = CommonBAL.GetMemberNumber(CurrentParlourId);
            ViewBag.IsMemberNumberEmpty = false;
            if (MemburNumber == string.Empty || MemburNumber == "")
            {
                ViewBag.IsMemberNumberEmpty = true;
            }

            //txtEasyToPay.Enabled = false;
        }
        private void ValidateInception(ref MembersModel member, int pkiMemberId)
        {

            SecureUserGroupsModel model = ToolsSetingBAL.GetUserAccessByID(UserID, CurrentParlourId);

            if (model != null)
            {
                ViewBag.IsInceptionEnabled = true;
            }
            else
            {
                ViewBag.IsInceptionEnabled = false;
            }

            if (pkiMemberId == 0)
            {
                member.InceptionDate = System.DateTime.Now;
            }
        }
        private void ValidatePolicyNumber()
        {
            ApplicationSettingsModel app = ToolsSetingBAL.GetApplictionByParlourID(CurrentParlourId);
            if (app != null)
            {
                if (app.IsAutoGeneratedPolicyNo == true)
                    ViewBag.IsPolicyNoEnabled = false;
                else
                    ViewBag.IsPolicyNoEnabled = true;
            }
        }
        private void ValidatePolicyDoc(int pkiMemberId)
        {
            if (pkiMemberId == 0)
                ViewBag.IsPolicyDocEnabled = false;
            else
                ViewBag.IsPolicyDocEnabled = true;
        }

        [PageRightsAttribute(CurrentPageId = 4)]
        private void ValidateCreateRights()
        {
            ViewBag.HasCreateRight = HasCreateRight;
        }
        public string BindTotalPremium(int pkiMemberId)
        {
            if (pkiMemberId != 0)
            {
                return MembersBAL.SumOfPremium(pkiMemberId, CurrentParlourId).ToString();
            }
            return "0.0";
        }
        public ActionResult BindInvoices(Model.Search.BaseSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.BaseSearch, MemberInvoiceModel>(search, new List<MemberInvoiceModel>(), o => o.InvNumber.Contains(search.SarchText));

            try
            {
                var invoices = MembersBAL.GetInvoicesByMemberID(CurrentParlourId, MemberId).ToList();
                search.TotalRecord = invoices.Count;
                return Json(new Funeral.Model.SearchResult<Model.Search.BaseSearch, MemberInvoiceModel>(search, invoices, o => o.InvNumber.Contains(search.SarchText)));
                //return Json(new Funeral.Model.SearchResult<Model.Search.BaseSearch, MemberAddonProductsModel>(search, products, o => o.ProductName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, MemberInvoiceModel>.Error(searchResult, ex));
            }
        }
        public ActionResult GetMember(int pkiMemberId = 0)
        {
            MembersModel member = new MembersModel();
            member.parlourid = CurrentParlourId;
            if (pkiMemberId != 0)
            {
                member = MembersBAL.GetMemberByID(pkiMemberId, CurrentParlourId);
                member.Age = GetDifferenceInYears(member.DateOfBirth, DateTime.Now);
                PolicyModel objPolicyModel = CommonBAL.GetPolicyDetailsBetweenAge(member.fkiPlanID, member.Age, 1).FirstOrDefault();
                member.PolicyPremium = objPolicyModel != null ? objPolicyModel.PlanSubscription : string.Empty;
                member.CoverAmount = objPolicyModel != null ? (objPolicyModel.CoverAmount == "" || objPolicyModel.CoverAmount == null ? "0" : objPolicyModel.CoverAmount) : "0";
                member.TotalPremium = float.Parse(BindTotalPremium(pkiMemberId), CultureInfo.InvariantCulture.NumberFormat);
            }
            //var BankDetails = CommonBAL.GetBankDetails_ByParlourId(CurrentParlourId);
            //if (BankDetails != null)
            //{
            //    member.AccountHolder = BankDetails.AccountHolder;
            //    member.Bank = BankDetails.Bank;
            //    member.Branch = BankDetails.Branch;
            //    member.BranchCode = BankDetails.BranchCode;
            //    member.AccountNumber = BankDetails.AccountNumber;
            //    member.AccountType = BankDetails.AccountType;
            //}
            return Json(new { success = true, Member = member }, JsonRequestBehavior.AllowGet);
        }
        public int GetDifferenceInYears(DateTime startDate, DateTime endDate)
        {
            //Excel documentation says "COMPLETE calendar years in between dates"
            int years = endDate.Year - startDate.Year;

            if (startDate.Month == endDate.Month &&// if the start month and the end month are the same
                endDate.Day < startDate.Day// AND the end day is less than the start day
                || endDate.Month < startDate.Month)// OR if the end month is less than the start month
            {
                years--;
            }
            return years;
        }
        public ActionResult SaveAddOnProduct(int pkiMemberId, MemberAddonProductsModel addOnProduct)
        {
            ModelState["fkiMemberid"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            }

            MemberId = pkiMemberId;
            addOnProduct.fkiMemberid = pkiMemberId;
            addOnProduct.LastModified = DateTime.Now;
            addOnProduct.UserID = Request.LogonUserIdentity.User.ToString();
            addOnProduct.ModifiedUser = this.User.Identity.Name;
            addOnProduct.Deleted = 0;
            addOnProduct.parlourid = this.CurrentParlourId;
            addOnProduct.pkiMemberProductID = Guid.NewGuid();
            if (addOnProduct.fkiMemberid != 0)
            { var AddonProductID = MembersBAL.NewSaveAddonProducts(addOnProduct); }
            if (addOnProduct.fkiMemberid == 0)
            { Session["Product"] = addOnProduct; }
            return Json(new { success = true, Product = addOnProduct }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateAddOnProduct(int pkiMemberId, MemberAddonProductsModel addOnProduct)
        {
            ModelState["pkiMemberProductID"].Errors.Clear();
            ModelState["fkiMemberid"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            }
            
            MemberId = pkiMemberId;
            addOnProduct.fkiMemberid = Convert.ToInt32(MemberId);
            addOnProduct.LastModified = DateTime.Now;
            addOnProduct.UserID = Request.LogonUserIdentity.User.ToString();
            addOnProduct.ModifiedUser = this.User.Identity.Name;
            addOnProduct.Deleted = 0;
            addOnProduct.parlourid = this.CurrentParlourId;
            addOnProduct.pkiMemberProductID = addOnProduct.pkiMemberProductID;
            var AddonProductID = MembersBAL.NewAddonProductUpdateMember(addOnProduct);
            return Json(new { success = true, Product = addOnProduct, msg = "Product Updated Successfully" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteAddOnProduct(Guid pkiProductId)
        {
            AddonProductsModal ADDON = new AddonProductsModal();
            ADDON.ModifiedUser = UserID.ToString();
            string ModifiedUser = ADDON.ModifiedUser;
            MembersBAL.MemberAddonProductsRemove(pkiProductId, ModifiedUser);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchProductData(Model.Search.BaseSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.BaseSearch, MemberAddonProductsModel>(search, new List<MemberAddonProductsModel>(), o => o.ProductName.Contains(search.SarchText));
            try
            {
                var products = MembersBAL.GetAddonProductsList(MemberId).ToList();
                search.TotalRecord = products.Count;
                products.ForEach(x => x.ProductPrice = Currency.Trim() + " " + x.ProductCost);
                return Json(new Funeral.Model.SearchResult<Model.Search.BaseSearch, MemberAddonProductsModel>(search, products, o => o.ProductName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, MemberAddonProductsModel>.Error(searchResult, ex));
            }
        }
        public ActionResult SaveNote(MemberNotesModel memberNote)
        {
            ModelState["pkiNoteId"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            }

            memberNote.Notes = memberNote.Notes.Trim();
            memberNote.fkiMemberID = MemberId;
            memberNote.NoteDate = DateTime.Now;
            memberNote.ModifiedUser = this.User.Identity.Name;
            MembersBAL.NotesSaveMember(memberNote);
            return Json(new { success = true, Note = memberNote }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateNote(MemberNotesModel memberNote)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            }

            memberNote.Notes = memberNote.Notes.Trim();
            memberNote.fkiMemberID = MemberId;
            memberNote.NoteDate = DateTime.Now;
            memberNote.ModifiedUser = this.User.Identity.Name;
            memberNote.LastModified = DateTime.Now;
            MembersBAL.UpdateNotesMember(memberNote);
            return Json(new { success = true, Note = memberNote }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteNote(int pkiNoteId)
        {
            MembersBAL.DeleteMemberNote(pkiNoteId);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindNotes(Model.Search.BaseSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.BaseSearch, MemberNotesModel>(search, new List<MemberNotesModel>(), o => o.Notes.Contains(search.SarchText));

            try
            {
                var notes = MembersBAL.GetMemberNoteByMemberID(MemberId);
                search.TotalRecord = notes.Count;
                return Json(new Funeral.Model.SearchResult<Model.Search.BaseSearch, MemberNotesModel>(search, notes, o => o.Notes.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, MemberNotesModel>.Error(searchResult, ex));
            }
        }
        public ActionResult GetProductPrice(Guid productId)
        {
            var product = MembersBAL.MemberListBindAddonProduct(productId);
            string Product = string.Empty;
            if (product.Count != 0)
                //Product = product.FirstOrDefault().ProductCost.ToString();
                Product = Currency + (Decimal.Round(Convert.ToDecimal(product.FirstOrDefault().ProductCost), 2)).ToString();
            //Product += " "+product.FirstOrDefault().ProductCost;
            return Json(new { Product, ProductCost = product.FirstOrDefault().ProductCost, ProductCover = product.FirstOrDefault().ProductCover }, JsonRequestBehavior.AllowGet); //new { ProductPrice = Currency.Trim() + " " + product.FirstOrDefault().ProductCost, ProductCost = product.FirstOrDefault().ProductCost }
        }
        public ActionResult SubmitDocuments(int documentType)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        if (file != null)
                        {
                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }
                            string contentType = file.ContentType;
                            using (Stream fs = file.InputStream)
                            {
                                using (BinaryReader br = new BinaryReader(fs))
                                {
                                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                    SupportedDocumentModel ObjSupportedDocumentModel = new SupportedDocumentModel();
                                    ObjSupportedDocumentModel.DocContentType = contentType;
                                    ObjSupportedDocumentModel.ImageName = fname;
                                    ObjSupportedDocumentModel.ImageFile = bytes;
                                    ObjSupportedDocumentModel.fkiMemberID = MemberId;
                                    ObjSupportedDocumentModel.CreateDate = System.DateTime.Now;
                                    ObjSupportedDocumentModel.parlourid = this.CurrentParlourId;
                                    ObjSupportedDocumentModel.LastModified = DateTime.Now;
                                    ObjSupportedDocumentModel.ModifiedUser = this.User.Identity.Name;
                                    ObjSupportedDocumentModel.DocType = documentType;

                                    int documentId = MembersBAL.SaveSupportDocument(ObjSupportedDocumentModel);
                                }
                            }
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
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
        public ActionResult BindDocuments(Model.Search.BaseSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.BaseSearch, SupportedDocumentModel>(search, new List<SupportedDocumentModel>(), o => o.ImageName.Contains(search.SarchText));

            try
            {
                var documents = MembersBAL.SelectSupportDocumentsByMemberId(MemberId);
                search.TotalRecord = documents.Count;
                search.Currency = Currency;
                var jsonResult = Json(new Funeral.Model.SearchResult<Model.Search.BaseSearch, SupportedDocumentModel>(search, documents, o => o.ImageName.Contains(search.SarchText)));
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, SupportedDocumentModel>.Error(searchResult, ex));
            }
        }
        public ActionResult BindDependantFamily(BaseSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.BaseSearch, FamilyDependencyModel>(search, new List<FamilyDependencyModel>(), o => o.FullName.Contains(search.SarchText));
            try
            {
                var familyDependancies = MembersBAL.GetFamilyDependencyByMemberID(CurrentParlourId, MemberId);
                search.Currency = Currency;
                search.TotalRecord = familyDependancies.Count;
                
                return Json(new Funeral.Model.SearchResult<Model.Search.BaseSearch, FamilyDependencyModel>(search, familyDependancies, o => o.FullName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, FamilyDependencyModel>.Error(searchResult, ex));
            }
        }

        [HttpGet]
        public FileResult DownloadFile(int pkiPictureID)
        {
            var document = MembersBAL.SelectSupportDocumentsById(pkiPictureID);
            return File(document.ImageFile, document.DocContentType, document.ImageName);

            //byte[] myByteArray = Convert.FromBase64String(fileData);
            ////Response.AppendHeader("content-disposition", "attachment;filename="+ fileName);
            //return new FileContentResult(myByteArray, "application/octet-stream");
        }

        private static byte[] ConvertFromBase64String(string input)
        {
            if (String.IsNullOrWhiteSpace(input)) return null;
            try
            {
                string working = input.Replace('-', '+').Replace('_', '/'); ;
                while (working.Length % 4 != 0)
                {
                    working += '=';
                }
                return Convert.FromBase64String(working);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ActionResult DeleteDocument(int pkiPictureID)
        {
            SupportedDocumentModel supportedDocument = new SupportedDocumentModel();
            supportedDocument.ModifiedUser = UserID.ToString();
            string ModifiedUser = supportedDocument.ModifiedUser;

            bool isdocumentDeleted = MembersBAL.DeleteSUpportdocumentById(pkiPictureID, ModifiedUser);
            if (isdocumentDeleted)
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPremiumForPolicy(string policyId)
        {
            var data = new List<PolicyModel>();
            if (!string.IsNullOrEmpty(policyId))
            {
                data = CommonBAL.GetPlanSubscriptionByPlanIdNewMember(Convert.ToInt32(policyId));
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWaitingPeriodByPlanId(int policyId, string Date)
        {
            DateTime PolicyDate = Convert.ToDateTime(Date);


            var data = CommonBAL.GetWaitingPeriodByPlanId(policyId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [PageRightsAttribute(CurrentPageId = 4, Right = new isPageRight[] { isPageRight.HasAdd })]
        public JsonResult SaveManageMembers(MembersModel Member, int fkiMemberid, string ProductName, string ProductCost, Guid fkiProductID)
        {
            if (ModelState["pkiMemberID"] != null && ModelState["pkiMemberID"].Errors.Count > 0)
                ModelState["pkiMemberID"].Errors.Clear();
            if (ModelState["Passport"] != null && ModelState["Passport"].Errors.Count > 0)
                ModelState["Passport"].Errors.Clear();
            if (ModelState["MemberBranch"] != null && ModelState["MemberBranch"].Errors.Count > 0)
                ModelState["MemberBranch"].Errors.Clear();
            if (ModelState["parlourid"] != null && ModelState["parlourid"].Errors.Count > 0)
                ModelState["parlourid"].Errors.Clear();
            if (ModelState["FK_MemberId"] != null && ModelState["FK_MemberId"].Errors.Count > 0)
                ModelState["FK_MemberId"].Errors.Clear();

            //PlanModel Plan = new PlanModel();
            PlanModel objPlans = MembersBAL.GetPlanByPlanID(Member.fkiPlanID, CurrentParlourId);

            if (MembersBAL.GetMemberByIDNumber(Member.IDNumber, this.ParlourId, Member.fkiPlanID) != null && Member.pkiMemberID == 0 && Member.IDNumber != "0")
            {
                //return Json(new { success = false, errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Member Already Exists" + "</li>").ToList() }, JsonRequestBehavior.AllowGet);
                return Json(new { success = "Duplicate", errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Member ID Number already exists on this Plan." + "</li>").First() }, JsonRequestBehavior.AllowGet);
            }

            //var inceptionAge = Member.InceptionDate - Member.DateOfBirth

            int Years(DateTime start, DateTime end)
            {
                return (end.Year - start.Year - 1) +
                    (((end.Month > start.Month) ||
                    ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
            }
            int ageFromInception = Years(Member.DateOfBirth, Member.InceptionDate);


            if (Member.Age < objPlans.AgeFrom || Member.Age > objPlans.AgeTo)
            {
                return Json(new { success = "WrongAge", errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Memeber age is not supported under this Plan." + "</li>").First() }, JsonRequestBehavior.AllowGet);

            }
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => "<li>" + x.ErrorMessage + "</li>").ToList() }, JsonRequestBehavior.AllowGet);
            }
            Member.pkiMemberID = MemberId;
            Member.PolicyStatus = Member.PolicyStatus;
            Member.CreateDate = System.DateTime.Now;
            if (Member.Passport == null)
                Member.Passport = "";
            if (Member.Title == null)
                Member.Title = "";
            if (Member.DateOfBirth == null || Member.DateOfBirth == DateTime.MinValue)
                Member.DateOfBirth = DateTime.MaxValue;
            if (Member.DebitDate == null || Member.DebitDate == DateTime.MinValue)
                Member.DebitDate = DateTime.MaxValue;

            if (Member.parlourid != Guid.Empty && ParlourId != Member.parlourid && IsAdministrator)
                Member.parlourid = Member.parlourid;
            else
                Member.parlourid = CurrentParlourId;
            Member.MemeberNumber = Member.PolicyNumber;

            Member.pkiAdditionalMemberInfo = Guid.NewGuid();

            if (Member.StartDate == null || Member.StartDate == DateTime.MinValue)
            {
                Member.StartDate = DateTime.Now;
            }

            if (Member.CoverDate == null || Member.CoverDate == DateTime.MinValue)
            {
                Member.CoverDate = DateTime.Now;
            }

            //SocietyModel society = new SocietyModel();
            //Member.MemberSociety = society.pkiSocietyID.ToString();
            AdditionalApplicationSettingsModel additionalApplicationSettingsModel;
            additionalApplicationSettingsModel = ToolsSetingBAL.GetAdditionalApplicationSettingsByParlourID(ParlourId);
            Member.ModifiedUser = UserID.ToString();
            Member.Active = false;
            Member.ServiceKey = additionalApplicationSettingsModel.spAccountServiceKey;
            Member.AutogenerateEasyPay = additionalApplicationSettingsModel.GenerateEasyPay;
            Member.AccountNumberVerified = ValidateBankAccount(Member.ServiceKey, Member.AccountNumber, Member.BranchCode, Member.AccountType); //"d2fbc8dc-b9e3-4722-8555-c1ec8505be16", "1577844775", "470010","2");

            int retId = MembersBAL.SaveMembers(Member);
            Member.pkiMemberID = retId;


            //string error = "id number already exists";
            //var dtidnum = MembersBAL.GetMemberByIDNum(Member.IDNumber, this.ParlourId).ToString();
            //if (Member.IDNumber == dtidnum)
            //{
            //    return Json(new { success = "false", responsetext = error });
            //}


            if (Request.QueryString["ID"] == null)
            { saveAddproduct(retId); }
            //Add On Product
            //var addOnProduct = new MemberAddonProductsModel();
            //if (ProductName != "Select" && ProductCost != "0")
            //{
            //    addOnProduct.fkiMemberid = retId;
            //    addOnProduct.LastModified = DateTime.Now;
            //    addOnProduct.UserID = Request.LogonUserIdentity.User.ToString();
            //    addOnProduct.ModifiedUser = this.User.Identity.Name;
            //    addOnProduct.Deleted = 0;
            //    addOnProduct.parlourid = this.ParlourId;
            //    addOnProduct.pkiMemberProductID = Guid.NewGuid();
            //    addOnProduct.ProductName = ProductName;
            //    addOnProduct.ProductPrice = ProductCost;
            //    addOnProduct.ProductCost = Convert.ToDecimal(ProductCost);
            //    addOnProduct.fkiProductID = fkiProductID;
            //    if (addOnProduct.fkiMemberid != 0)
            //    { var AddonProductID = MembersBAL.SaveAddonProducts(addOnProduct); }
            //}
            return Json(Member, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveManageMembersDuplicate(MembersModel Member, int fkiMemberid, string ProductName, string ProductCost, Guid fkiProductID)
        {
            if (ModelState["pkiMemberID"] != null && ModelState["pkiMemberID"].Errors.Count > 0)
                ModelState["pkiMemberID"].Errors.Clear();
            if (ModelState["Passport"] != null && ModelState["Passport"].Errors.Count > 0)
                ModelState["Passport"].Errors.Clear();
            if (ModelState["MemberBranch"] != null && ModelState["MemberBranch"].Errors.Count > 0)
                ModelState["MemberBranch"].Errors.Clear();
            if (ModelState["parlourid"] != null && ModelState["parlourid"].Errors.Count > 0)
                ModelState["parlourid"].Errors.Clear();
            if (ModelState["FK_MemberId"] != null && ModelState["FK_MemberId"].Errors.Count > 0)
                ModelState["FK_MemberId"].Errors.Clear();

            //PlanModel Plan = new PlanModel();
            PlanModel objPlans = MembersBAL.GetPlanByPlanID(Member.fkiPlanID, CurrentParlourId);

            //if (MembersBAL.GetMemberByIDNumber(Member.IDNumber, this.ParlourId, Member.fkiPlanID) != null && Member.pkiMemberID == 0)
            //{
            //    //return Json(new { success = false, errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Member Already Exists" + "</li>").ToList() }, JsonRequestBehavior.AllowGet);
            //    return Json(new { success = false, errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Member ID Number already exists on this Plan." + "</li>").First() }, JsonRequestBehavior.AllowGet);
            //}

            //var inceptionAge = Member.InceptionDate - Member.DateOfBirth

            int Years(DateTime start, DateTime end)
            {
                return (end.Year - start.Year - 1) +
                    (((end.Month > start.Month) ||
                    ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
            }
            int ageFromInception = Years(Member.DateOfBirth, Member.InceptionDate);


            //if (Member.Age < objPlans.AgeFrom || Member.Age > objPlans.AgeTo)
            //{
            //    return Json(new { success = false, errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Memeber age is not supported under this Plan." + "</li>").First() }, JsonRequestBehavior.AllowGet);

            //}
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => "<li>" + x.ErrorMessage + "</li>").ToList() }, JsonRequestBehavior.AllowGet);
            }
            Member.pkiMemberID = MemberId;
            Member.PolicyStatus = Member.PolicyStatus;
            Member.CreateDate = System.DateTime.Now;
            if (Member.Passport == null)
                Member.Passport = "";
            if (Member.Title == null)
                Member.Title = "";
            if (Member.DateOfBirth == null || Member.DateOfBirth == DateTime.MinValue)
                Member.DateOfBirth = DateTime.MaxValue;
            if (Member.DebitDate == null || Member.DebitDate == DateTime.MinValue)
                Member.DebitDate = DateTime.MaxValue;

            if (Member.parlourid != Guid.Empty && ParlourId != Member.parlourid && IsAdministrator)
                Member.parlourid = Member.parlourid;
            else
                Member.parlourid = CurrentParlourId;
            Member.MemeberNumber = Member.PolicyNumber;

            Member.pkiAdditionalMemberInfo = Guid.NewGuid();

            if (Member.StartDate == null || Member.StartDate == DateTime.MinValue)
            {
                Member.StartDate = DateTime.Now;
            }

            if (Member.CoverDate == null || Member.CoverDate == DateTime.MinValue)
            {
                Member.CoverDate = DateTime.Now;
            }

            //SocietyModel society = new SocietyModel();
            //Member.MemberSociety = society.pkiSocietyID.ToString();
            AdditionalApplicationSettingsModel additionalApplicationSettingsModel;
            additionalApplicationSettingsModel = ToolsSetingBAL.GetAdditionalApplicationSettingsByParlourID(ParlourId);
            Member.ModifiedUser = UserID.ToString();
            Member.Active = false;
            Member.ServiceKey = additionalApplicationSettingsModel.spAccountServiceKey;
            Member.AutogenerateEasyPay = additionalApplicationSettingsModel.GenerateEasyPay;
            Member.AccountNumberVerified = ValidateBankAccount(Member.ServiceKey, Member.AccountNumber, Member.BranchCode, Member.AccountType); //"d2fbc8dc-b9e3-4722-8555-c1ec8505be16", "1577844775", "470010","2");

            int retId = MembersBAL.SaveMembers(Member);
            Member.pkiMemberID = retId;


            //string error = "id number already exists";
            //var dtidnum = MembersBAL.GetMemberByIDNum(Member.IDNumber, this.ParlourId).ToString();
            //if (Member.IDNumber == dtidnum)
            //{
            //    return Json(new { success = "false", responsetext = error });
            //}


            if (Request.QueryString["ID"] == null)
            { saveAddproduct(retId); }
            //Add On Product
            //var addOnProduct = new MemberAddonProductsModel();
            //if (ProductName != "Select" && ProductCost != "0")
            //{
            //    addOnProduct.fkiMemberid = retId;
            //    addOnProduct.LastModified = DateTime.Now;
            //    addOnProduct.UserID = Request.LogonUserIdentity.User.ToString();
            //    addOnProduct.ModifiedUser = this.User.Identity.Name;
            //    addOnProduct.Deleted = 0;
            //    addOnProduct.parlourid = this.ParlourId;
            //    addOnProduct.pkiMemberProductID = Guid.NewGuid();
            //    addOnProduct.ProductName = ProductName;
            //    addOnProduct.ProductPrice = ProductCost;
            //    addOnProduct.ProductCost = Convert.ToDecimal(ProductCost);
            //    addOnProduct.fkiProductID = fkiProductID;
            //    if (addOnProduct.fkiMemberid != 0)
            //    { var AddonProductID = MembersBAL.SaveAddonProducts(addOnProduct); }
            //}
            return Json(Member, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveManageMembersWrongAge(MembersModel Member, int fkiMemberid, string ProductName, string ProductCost, Guid fkiProductID)
        {
            if (ModelState["pkiMemberID"] != null && ModelState["pkiMemberID"].Errors.Count > 0)
                ModelState["pkiMemberID"].Errors.Clear();
            if (ModelState["Passport"] != null && ModelState["Passport"].Errors.Count > 0)
                ModelState["Passport"].Errors.Clear();
            if (ModelState["MemberBranch"] != null && ModelState["MemberBranch"].Errors.Count > 0)
                ModelState["MemberBranch"].Errors.Clear();
            if (ModelState["parlourid"] != null && ModelState["parlourid"].Errors.Count > 0)
                ModelState["parlourid"].Errors.Clear();
            if (ModelState["FK_MemberId"] != null && ModelState["FK_MemberId"].Errors.Count > 0)
                ModelState["FK_MemberId"].Errors.Clear();

            //PlanModel Plan = new PlanModel();
            PlanModel objPlans = MembersBAL.GetPlanByPlanID(Member.fkiPlanID, CurrentParlourId);

            //if (MembersBAL.GetMemberByIDNumber(Member.IDNumber, this.ParlourId, Member.fkiPlanID) != null && Member.pkiMemberID == 0)
            //{
            //    //return Json(new { success = false, errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Member Already Exists" + "</li>").ToList() }, JsonRequestBehavior.AllowGet);
            //    return Json(new { success = false, errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Member ID Number already exists on this Plan." + "</li>").First() }, JsonRequestBehavior.AllowGet);
            //}

            //var inceptionAge = Member.InceptionDate - Member.DateOfBirth

            int Years(DateTime start, DateTime end)
            {
                return (end.Year - start.Year - 1) +
                    (((end.Month > start.Month) ||
                    ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
            }
            int ageFromInception = Years(Member.DateOfBirth, Member.InceptionDate);


            //if (Member.Age < objPlans.AgeFrom || Member.Age > objPlans.AgeTo)
            //{
            //    return Json(new { success = false, errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Memeber age is not supported under this Plan." + "</li>").First() }, JsonRequestBehavior.AllowGet);

            //}
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => "<li>" + x.ErrorMessage + "</li>").ToList() }, JsonRequestBehavior.AllowGet);
            }
            Member.pkiMemberID = MemberId;
            Member.PolicyStatus = Member.PolicyStatus;
            Member.CreateDate = System.DateTime.Now;
            if (Member.Passport == null)
                Member.Passport = "";
            if (Member.Title == null)
                Member.Title = "";
            if (Member.DateOfBirth == null || Member.DateOfBirth == DateTime.MinValue)
                Member.DateOfBirth = DateTime.MaxValue;
            if (Member.DebitDate == null || Member.DebitDate == DateTime.MinValue)
                Member.DebitDate = DateTime.MaxValue;

            if (Member.parlourid != Guid.Empty && ParlourId != Member.parlourid && IsAdministrator)
                Member.parlourid = Member.parlourid;
            else
                Member.parlourid = CurrentParlourId;
            Member.MemeberNumber = Member.PolicyNumber;

            Member.pkiAdditionalMemberInfo = Guid.NewGuid();

            if (Member.StartDate == null || Member.StartDate == DateTime.MinValue)
            {
                Member.StartDate = DateTime.Now;
            }

            if (Member.CoverDate == null || Member.CoverDate == DateTime.MinValue)
            {
                Member.CoverDate = DateTime.Now;
            }

            //SocietyModel society = new SocietyModel();
            //Member.MemberSociety = society.pkiSocietyID.ToString();
            AdditionalApplicationSettingsModel additionalApplicationSettingsModel;
            additionalApplicationSettingsModel = ToolsSetingBAL.GetAdditionalApplicationSettingsByParlourID(ParlourId);
            Member.ModifiedUser = UserID.ToString();
            Member.Active = false;
            Member.ServiceKey = additionalApplicationSettingsModel.spAccountServiceKey;
            Member.AutogenerateEasyPay = additionalApplicationSettingsModel.GenerateEasyPay;
            Member.AccountNumberVerified = ValidateBankAccount(Member.ServiceKey, Member.AccountNumber, Member.BranchCode, Member.AccountType); //"d2fbc8dc-b9e3-4722-8555-c1ec8505be16", "1577844775", "470010","2");

            int retId = MembersBAL.SaveMembers(Member);
            Member.pkiMemberID = retId;


            //string error = "id number already exists";
            //var dtidnum = MembersBAL.GetMemberByIDNum(Member.IDNumber, this.ParlourId).ToString();
            //if (Member.IDNumber == dtidnum)
            //{
            //    return Json(new { success = "false", responsetext = error });
            //}


            if (Request.QueryString["ID"] == null)
            { saveAddproduct(retId); }
            //Add On Product
            //var addOnProduct = new MemberAddonProductsModel();
            //if (ProductName != "Select" && ProductCost != "0")
            //{
            //    addOnProduct.fkiMemberid = retId;
            //    addOnProduct.LastModified = DateTime.Now;
            //    addOnProduct.UserID = Request.LogonUserIdentity.User.ToString();
            //    addOnProduct.ModifiedUser = this.User.Identity.Name;
            //    addOnProduct.Deleted = 0;
            //    addOnProduct.parlourid = this.ParlourId;
            //    addOnProduct.pkiMemberProductID = Guid.NewGuid();
            //    addOnProduct.ProductName = ProductName;
            //    addOnProduct.ProductPrice = ProductCost;
            //    addOnProduct.ProductCost = Convert.ToDecimal(ProductCost);
            //    addOnProduct.fkiProductID = fkiProductID;
            //    if (addOnProduct.fkiMemberid != 0)
            //    { var AddonProductID = MembersBAL.SaveAddonProducts(addOnProduct); }
            //}
            return Json(Member, JsonRequestBehavior.AllowGet);

        }


        //public JsonResult BindPolicyCoverDate(int id, DateTime date)
        //{
        //    PolicyModel objPolicyModel = CommonBAL.GetPlanSubscriptionByPlanIdNewMember(id).ToList().FirstOrDefault();
        //    List<string> Response = new List<string>();
        //    try
        //    {
        //        GetdataPremium = Currency + " " + objPolicyModel.PlanSubscription;

        //        if (objPolicyModel != null)
        //            Response.Add(Currency + " " + objPolicyModel.PlanSubscription);
        //        else
        //            Response.Add(string.Empty);
        //        Response.Add(string.IsNullOrEmpty(CommonBAL.GetPlanUnderwriterByPlanId(id)) ? string.Empty : CommonBAL.GetPlanUnderwriterByPlanId(id));
        //        int WaitingPeriod = CommonBAL.GetWaitingPeriodByPlanId(id);

        //        if (objPolicyModel.WaitingPeriod == 0 || objPolicyModel.WaitingPeriod == null)
        //        {
        //            Response.Add(DateTime.Now.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
        //        }
        //        else if (date != null)
        //        {
        //            //DateTime PolicystartDate = Convert.ToDateTime(date);
        //            Response.Add(date.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
        //            //date = PolicystartDate;
        //        }
        //        if (objPolicyModel != null)
        //            Response.Add(objPolicyModel.totalPremium);
        //        else
        //            Response.Add(string.Empty);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return Json(Response, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult BindPolicyCoverDate(int id, DateTime date)
        {
            PolicyModel objPolicyModel = CommonBAL.GetPlanSubscriptionByPlanIdNewMember(id).ToList().FirstOrDefault();
            List<string> Response = new List<string>();
            try
            {
                GetdataPremium = Currency + " " + objPolicyModel.PlanSubscription;

                if (objPolicyModel != null)
                    Response.Add(Currency + " " + objPolicyModel.PlanSubscription);
                else
                    Response.Add(string.Empty);
                Response.Add(string.IsNullOrEmpty(CommonBAL.GetPlanUnderwriterByPlanId(id)) ? string.Empty : CommonBAL.GetPlanUnderwriterByPlanId(id));
                int WaitingPeriod = CommonBAL.GetWaitingPeriodByPlanId(id);

                //

                if (objPolicyModel.WaitingPeriod != 0 && objPolicyModel.WaitingPeriod == null)
                {
                    Response.Add(DateTime.Now.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
                }
                else if (date != null)
                {
                    DateTime PolicystartDate = Convert.ToDateTime(date);
                    Response.Add(date.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
                    //date = PolicystartDate;
                }
                //

                if (objPolicyModel != null)
                    Response.Add(objPolicyModel.totalPremium);
                else
                    Response.Add(string.Empty);

                if (objPolicyModel != null)
                {
                    Response.Add(Currency + " " + objPolicyModel.CoverAmount);
                }
                else
                {
                    Response.Add(string.Empty);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindDependentCoverDate(int id, DateTime date)
        {
            PolicyModel objPolicyModel = CommonBAL.GetPlanSubscriptionByPlanIdNewMember(id).ToList().FirstOrDefault();
            List<string> Response = new List<string>();
            try
            {
                int WaitingPeriod = CommonBAL.GetWaitingPeriodByPlanId(id);

                if (objPolicyModel.WaitingPeriod == 0 || objPolicyModel.WaitingPeriod == null)
                {
                    Response.Add(DateTime.Now.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
                }
                else if (date != null)
                {
                    //DateTime PolicystartDate = Convert.ToDateTime(date);
                    Response.Add(date.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
                    //date = PolicystartDate;
                }
                if (objPolicyModel != null)
                    Response.Add(objPolicyModel.totalPremium);
                else
                    Response.Add(string.Empty);
            }
            catch (Exception e)
            {
                throw e;
            }
            return Json(Response, JsonRequestBehavior.AllowGet);
        }
        [PageRightsAttribute(CurrentPageId = 4)]
        public JsonResult GetMemberById(int MemberId)
        {
            var memberDetails = MembersBAL.GetMemberByID(MemberId, CurrentParlourId);
            return Json(memberDetails, JsonRequestBehavior.AllowGet);
        }
        [PageRightsAttribute(CurrentPageId = 4)]
        public JsonResult UpdateManageMembers(MembersModel Member)
        {
            return Json(Member, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult SaveDependancy([System.Web.Http.FromBody]FamilyDependencyModel dependency)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
        //    //}

        //    //if (dependency.IDNumber == "" || dependency.IDNumber == string.Empty)
        //    //    dependency.IDNumber = "0";

        //    //FamilyDependencyModel ObjFamilyDependencyModel;
        //    //ObjFamilyDependencyModel = MembersBAL.GetDependencByIDNum(dependency.IDNumber, CurrentParlourId, MemberId);

        //    //if (ObjFamilyDependencyModel == null)
        //    //{
        //    //    ObjFamilyDependencyModel = new FamilyDependencyModel();
        //    //    ObjFamilyDependencyModel.Age = AgeFromDOB(Convert.ToDateTime(dependency.DateOfBirth));
        //    //    ObjFamilyDependencyModel.parlourid = CurrentParlourId;
        //    //    ObjFamilyDependencyModel.DependentStatus = dependency.DependentStatus;
        //    //    ObjFamilyDependencyModel.MemberId = this.MemberId;
        //    //    if (dependency.StartDate == null || dependency.StartDate == DateTime.MinValue)
        //    //    {
        //    //        ObjFamilyDependencyModel.StartDate = DateTime.Now;
        //    //    }
        //    //    else
        //    //    {
        //    //        ObjFamilyDependencyModel.StartDate = dependency.StartDate;
        //    //    }
        //    //    if (dependency.CoverDate == null || dependency.CoverDate == DateTime.MinValue)
        //    //    {
        //    //        ObjFamilyDependencyModel.CoverDate = DateTime.Now;
        //    //    }
        //    //    else
        //    //    {
        //    //        ObjFamilyDependencyModel.CoverDate = dependency.CoverDate;
        //    //    }
        //    //    if (dependency.InceptionDate == null || dependency.InceptionDate == DateTime.MinValue)
        //    //    {
        //    //        ObjFamilyDependencyModel.InceptionDate = DateTime.Now;
        //    //    }
        //    //    else
        //    //    {
        //    //        ObjFamilyDependencyModel.InceptionDate = dependency.InceptionDate;
        //    //    }
        //    //    if (dependency.DateOfBirth == null || dependency.DateOfBirth == DateTime.MinValue)
        //    //    {
        //    //        ObjFamilyDependencyModel.DateOfBirth = DateTime.Now;
        //    //    }
        //    //    else
        //    //    {
        //    //        ObjFamilyDependencyModel.DateOfBirth = dependency.DateOfBirth;
        //    //    }
        //    //    ObjFamilyDependencyModel.FullName = dependency.FullName;
        //    //    ObjFamilyDependencyModel.Surname = dependency.Surname;
        //    //    ObjFamilyDependencyModel.IDNumber = dependency.IDNumber;
        //    //    ObjFamilyDependencyModel.Gender = dependency.Gender == null ? "male" : dependency.Gender;
        //    //    ObjFamilyDependencyModel.Relationship = Convert.ToInt32(dependency.Relationship);
        //    //    ObjFamilyDependencyModel.DependencyType = dependency.DependencyType;
        //    //    ObjFamilyDependencyModel.Premium = dependency.Premium;
        //    //    ObjFamilyDependencyModel.Cover = dependency.Cover;
        //    //    ObjFamilyDependencyModel.Passport = dependency.Passport;

        //    //}
        //    //else
        //    //{
        //    //    return Json(new { success = false, errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Dependent already exists." + "</li>").First() }, JsonRequestBehavior.AllowGet);
        //    //}


        //    //if (MembersBAL.CheckDependencyCount(ObjFamilyDependencyModel) < 100)
        //    //{
        //    //    int documentId = MembersBAL.SaveFamilyDependency(ObjFamilyDependencyModel);
        //    //    return Json(new { success = true, maxLenghDependancy = true, Dependency = dependency }, JsonRequestBehavior.AllowGet);
        //    //}
        //    //else
        //    //{
        //    //    return Json(new { success = true, maxLenghDependancy = false, Dependency = dependency }, JsonRequestBehavior.AllowGet);
        //    //}
        //    if (MembersBAL.GetDependencByIDNum(dependency.IDNumber, CurrentParlourId, MemberId) != null && dependency.pkiDependentID == 0)
        //    {
        //        return Json(new { success = false, errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Dependent already exists." + "</li>").First() }, JsonRequestBehavior.AllowGet);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
        //    }

        //    if (dependency.IDNumber == "" || dependency.IDNumber == string.Empty)
        //        dependency.IDNumber = "0";

        //    FamilyDependencyModel ObjFamilyDependencyModel;
        //    ObjFamilyDependencyModel = MembersBAL.GetDependencByIDNum(dependency.IDNumber, CurrentParlourId, MemberId);
        //    PlanCreator ObjPlanModel = MembersBAL.GetPlanCreatorByID(MemberId, CurrentParlourId, dependency.Relationship);
        //    PlanModel ObjMemberPlanModel = MembersBAL.GetPlanByID(MemberId, CurrentParlourId);

        //    try
        //    {
        //        if (ObjFamilyDependencyModel != null)
        //        {
        //            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw new System.Exception(ex.Message.ToString());
        //    }


        //    if (ObjFamilyDependencyModel == null)
        //    {
        //        ObjFamilyDependencyModel = new FamilyDependencyModel();
        //        ObjFamilyDependencyModel.Age = AgeFromDOB(Convert.ToDateTime(dependency.DateOfBirth));
        //        ObjFamilyDependencyModel.parlourid = CurrentParlourId;
        //        ObjFamilyDependencyModel.DependentStatus = dependency.DependentStatus;
        //        ObjFamilyDependencyModel.MemberId = this.MemberId;
        //        if (dependency.StartDate == null || dependency.StartDate == DateTime.MinValue)
        //        {
        //            ObjFamilyDependencyModel.StartDate = DateTime.Now;
        //        }
        //        else
        //        {
        //            ObjFamilyDependencyModel.StartDate = dependency.StartDate;
        //        }
        //        if (dependency.CoverDate == null || dependency.CoverDate == DateTime.MinValue)
        //        {
        //            ObjFamilyDependencyModel.CoverDate = DateTime.Now;
        //        }
        //        else
        //        {
        //            ObjFamilyDependencyModel.CoverDate = dependency.CoverDate;
        //        }
        //        if (dependency.InceptionDate == null || dependency.InceptionDate == DateTime.MinValue)
        //        {
        //            ObjFamilyDependencyModel.InceptionDate = DateTime.Now;
        //        }
        //        else 
        //        {
        //            ObjFamilyDependencyModel.InceptionDate = dependency.InceptionDate;
        //        }
        //        if (dependency.DateOfBirth == null || dependency.DateOfBirth == DateTime.MinValue)
        //        {
        //            ObjFamilyDependencyModel.DateOfBirth = DateTime.Now;
        //        }
        //        else
        //        {
        //            ObjFamilyDependencyModel.DateOfBirth = dependency.DateOfBirth;
        //        }
        //        ObjFamilyDependencyModel.FullName = dependency.FullName;
        //        ObjFamilyDependencyModel.Surname = dependency.Surname;
        //        ObjFamilyDependencyModel.IDNumber = dependency.IDNumber;
        //        ObjFamilyDependencyModel.Gender = dependency.Gender == null ? "male" : dependency.Gender;
        //        ObjFamilyDependencyModel.Relationship = Convert.ToInt32(dependency.Relationship);
        //        ObjFamilyDependencyModel.DependencyType = dependency.DependencyType;
        //        ObjFamilyDependencyModel.Premium = dependency.Premium;
        //        ObjFamilyDependencyModel.Cover = dependency.Cover;
        //        ObjFamilyDependencyModel.Passport = dependency.Passport;

        //    }




        //    //MembersModel Member = new MembersModel();



        //    //---------------charles Edit-----------------------------------------
        //    //if (MembersBAL.CheckDependencyCount(ObjFamilyDependencyModel) < 100)
        //    //{
        //    //    int documentId = MembersBAL.SaveFamilyDependency(ObjFamilyDependencyModel);
        //    //    return Json(new { success = true, maxLenghDependancy = true, Dependency = dependency }, JsonRequestBehavior.AllowGet);
        //    //}
        //    //else
        //    //{
        //    //    return Json(new { success = true, maxLenghDependancy = false, Dependency = dependency }, JsonRequestBehavior.AllowGet);
        //    //}

        //    if (dependency.Age < ObjPlanModel.AgeFrom || dependency.Age > ObjPlanModel.AgeTo)
        //    {
        //        return Json(new { success = false, ageLimit = false, Dependency = dependency }, JsonRequestBehavior.AllowGet);
        //    }
        //    else if (MembersBAL.CheckDependencyCount(ObjFamilyDependencyModel) < ObjMemberPlanModel.NumberOfDependents)
        //    {
        //        int documentId = MembersBAL.SaveFamilyDependency(ObjFamilyDependencyModel);
        //        return Json(new { success = true, maxLenghDependancy = true, Dependency = dependency }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, maxLenghDependancy = false, Dependency = dependency }, JsonRequestBehavior.AllowGet);
        //    }
        //    //--------------------END-----------------------------------------------
        //}



        [HttpPost]
        public JsonResult SaveDependancy([System.Web.Http.FromBody]FamilyDependencyModel dependency)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            }

            if (dependency.IDNumber == "" || dependency.IDNumber == string.Empty)
                dependency.IDNumber = "0";

            FamilyDependencyModel ObjFamilyDependencyModel;
            ObjFamilyDependencyModel = MembersBAL.GetDependencByIDNum(dependency.IDNumber, CurrentParlourId, MemberId);
            PlanCreator ObjPlanModel = MembersBAL.GetPlanCreatorByID(MemberId, CurrentParlourId, dependency.Relationship);
            PlanModel ObjMemberPlanModel = MembersBAL.GetPlanByID(MemberId, CurrentParlourId);

            try
            {
                if (ObjFamilyDependencyModel != null)
                {
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
                    //return Json(new { success = false, errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Dependent ID Number Already Exists." + "</li>").FirstOrDefault() }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message.ToString());
            }


            if (ObjFamilyDependencyModel == null)
            {
                ObjFamilyDependencyModel = new FamilyDependencyModel();
                ObjFamilyDependencyModel.Age = AgeFromDOB(Convert.ToDateTime(dependency.DateOfBirth));
                ObjFamilyDependencyModel.parlourid = CurrentParlourId;
                ObjFamilyDependencyModel.DependentStatus = dependency.DependentStatus;
                ObjFamilyDependencyModel.MemberId = this.MemberId;
                if (dependency.StartDate == null || dependency.StartDate == DateTime.MinValue)
                {
                    ObjFamilyDependencyModel.StartDate = DateTime.Now;
                }
                else
                {
                    ObjFamilyDependencyModel.StartDate = dependency.StartDate;
                }
                if (dependency.CoverDate == null || dependency.CoverDate == DateTime.MinValue)
                {
                    ObjFamilyDependencyModel.CoverDate = DateTime.Now;
                }
                else
                {
                    ObjFamilyDependencyModel.CoverDate = dependency.CoverDate;
                }
                if (dependency.InceptionDate == null || dependency.InceptionDate == DateTime.MinValue)
                {
                    ObjFamilyDependencyModel.InceptionDate = DateTime.Now;
                }
                else
                {
                    ObjFamilyDependencyModel.InceptionDate = dependency.InceptionDate;
                }
                if (dependency.DateOfBirth == null || dependency.DateOfBirth == DateTime.MinValue)
                {
                    ObjFamilyDependencyModel.DateOfBirth = DateTime.Now;
                }
                else
                {
                    ObjFamilyDependencyModel.DateOfBirth = dependency.DateOfBirth;
                }
                ObjFamilyDependencyModel.FullName = dependency.FullName;
                ObjFamilyDependencyModel.Surname = dependency.Surname;
                ObjFamilyDependencyModel.IDNumber = dependency.IDNumber;
                ObjFamilyDependencyModel.Gender = dependency.Gender == null ? "male" : dependency.Gender;
                ObjFamilyDependencyModel.Relationship = Convert.ToInt32(dependency.Relationship);
                ObjFamilyDependencyModel.DependencyType = dependency.DependencyType;
                ObjFamilyDependencyModel.Premium = dependency.Premium;
                ObjFamilyDependencyModel.Cover = dependency.Cover;
                ObjFamilyDependencyModel.Passport = dependency.Passport;
                ObjFamilyDependencyModel.ModifiedUser = UserID.ToString();
                //ObjFamilyDependencyModel.ModifiedUser = UserName;
                ObjFamilyDependencyModel.CreatedBy = UserName;

            }
            else
            {
                return Json(new { success = false, errors = ModelState.Select(x => x.Value).Select(x => "<li>" + "Dependent already exists." + "</li>").First() }, JsonRequestBehavior.AllowGet);
            }

            MembersModel Member = new MembersModel();




            //if (MembersBAL.CheckDependencyCount(ObjFamilyDependencyModel) < ObjPlanModel.NumberOfDependents)
            //if (MembersBAL.CheckDependencyCount(ObjFamilyDependencyModel) < ObjPlanModel.NumberOfDependents)
            //    {
            //    int documentId = MembersBAL.SaveFamilyDependency(ObjFamilyDependencyModel);
            //    return Json(new { success = true, maxLenghDependancy = true, Dependency = dependency }, JsonRequestBehavior.AllowGet);
            //}
            //else if (dependency.Age > ObjPlanModel.AgeFrom || dependency.Age < ObjPlanModel.AgeTo)
            //{
            //    return Json(new { success = true, ageLimit = false, Dependency = dependency }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(new { success = true, maxLenghDependancy = false, Dependency = dependency }, JsonRequestBehavior.AllowGet);
            //}


            if (dependency.Age<ObjPlanModel.AgeFrom || dependency.Age> ObjPlanModel.AgeTo)
            {
                return Json(new { success = false, ageLimit = false, Dependency = dependency }, JsonRequestBehavior.AllowGet);
            }
            else if (MembersBAL.CheckDependencyCount(ObjFamilyDependencyModel) < ObjMemberPlanModel.NumberOfDependents)
            {
                int documentId = MembersBAL.SaveFamilyDependency(ObjFamilyDependencyModel);
                return Json(new { success = true, maxLenghDependancy = true, Dependency = dependency }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, maxLenghDependancy = false, Dependency = dependency }, JsonRequestBehavior.AllowGet);
            }
        }

        [PageRightsAttribute(CurrentPageId = 4)]
        public JsonResult UpdateDependancy(int pkiDependantId, FamilyDependencyModel dependency)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            }

            dependency.Age = AgeFromDOB(Convert.ToDateTime(dependency.DateOfBirth));
            dependency.parlourid = this.CurrentParlourId;
            dependency.MemberId = this.MemberId;
            dependency.pkiDependentID = pkiDependantId;
            dependency.Gender = dependency.Gender == null ? "male" : dependency.Gender;
            if (dependency.StartDate == null || dependency.StartDate == DateTime.MinValue)
            {
                dependency.StartDate = DateTime.Now;
            }

            if (dependency.CoverDate == null || dependency.CoverDate == DateTime.MinValue)
            {
                dependency.CoverDate = DateTime.Now;
            }

            if (dependency.InceptionDate == null || dependency.InceptionDate == DateTime.MinValue)
            {
                dependency.InceptionDate = DateTime.Now;
            }

            if (dependency.DateOfBirth == null || dependency.DateOfBirth == DateTime.MinValue)
            {
                dependency.DateOfBirth = DateTime.Now;
            }

            dependency.ModifiedUser = UserID.ToString();

            dependency.Relationship = Convert.ToInt32("1");


            int documentId = MembersBAL.UpdateFamilyDependency(dependency);

            return Json(dependency, JsonRequestBehavior.AllowGet);

        }
        public ActionResult BindDependency(Model.Search.BaseSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.BaseSearch, FamilyDependencyModel>(search, new List<FamilyDependencyModel>(), o => o.FullName.Contains(search.SarchText));

            try
            {
                var dependencies = MembersBAL.GetFamilyDependencyByMemberID(CurrentParlourId, MemberId);
                search.TotalRecord = dependencies.Count;

                return Json(new Funeral.Model.SearchResult<Model.Search.BaseSearch, FamilyDependencyModel>(search, dependencies, o => o.FullName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, FamilyDependencyModel>.Error(searchResult, ex));
            }
        }

        [PageRightsAttribute(CurrentPageId = 4)]
        public ActionResult DeleteDependency(int pkiDependantId)
        {
            FamilyDependencyModel dependency = new FamilyDependencyModel();
            dependency.ModifiedUser = UserID.ToString();
            string ModifiedUser = dependency.ModifiedUser;
            bool isDependencyDeleted = MembersBAL.GetFamilyDependencyTypes(pkiDependantId, ModifiedUser);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        private int AgeFromDOB(DateTime bday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - bday.Year;
            if (bday > today.AddYears(-age))
                age--;
            return age;
        }
        [PageRightsAttribute(CurrentPageId = 4, Right = new isPageRight[] { isPageRight.HasDelete })]
        public ActionResult DeleteMemberPolicy(int pkiMemberID)
        {
            try
            {
                MembersBAL.DeleteMember(pkiMemberID);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]

        [PageRightsAttribute(CurrentPageId = 4)]
        public JsonResult GetPolicyDetails(int id, DateTime date, int UserType)

        {
            PolicyModel objPolicyModel = CommonBAL.NewGetPlanSubscriptionByPlanIdNewMember(id, UserType).ToList().FirstOrDefault();
            List<string> Response = new List<string>();
            try
            {
                if (objPolicyModel != null)
                {
                    GetdataPremium = objPolicyModel.PlanSubscription;
                    Response.Add(objPolicyModel.PlanSubscription);
                }
                else
                    Response.Add(string.Empty);
                Response.Add(string.IsNullOrEmpty(CommonBAL.GetPlanUnderwriterByPlanId(id)) ? string.Empty : CommonBAL.GetPlanUnderwriterByPlanId(id));
                int WaitingPeriod = CommonBAL.GetWaitingPeriodByPlanId(id);

                if (objPolicyModel != null)
                {
                    if (objPolicyModel.WaitingPeriod != 0 && objPolicyModel.WaitingPeriod == null)
                    {
                        Response.Add(DateTime.Now.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
                    }
                    else if (date.ToLongDateString() != "" || date.ToLongDateString() != string.Empty)
                    {
                        DateTime PolicystartDate = Convert.ToDateTime(date);
                        Response.Add(PolicystartDate.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
                    }
                }
                if (objPolicyModel != null)
                    Response.Add(objPolicyModel.totalPremium);
                else
                    Response.Add(string.Empty);
            }
            catch (Exception e)
            {
                throw e;
            }

            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        [PageRightsAttribute(CurrentPageId = 4)]
        public JsonResult GetPolicyDetailsBetweenAge(int id, DateTime date, int UserType, int Age)
        {
            PolicyModel objPolicyModel = CommonBAL.GetPolicyDetailsBetweenAge(id, Age, UserType).ToList().FirstOrDefault();
            List<string> Response = new List<string>();
            try
            {
                if (objPolicyModel != null)
                {
                    GetdataPremium = objPolicyModel.PlanSubscription;
                    Response.Add(objPolicyModel.PlanSubscription);

                }
                else
                    Response.Add(string.Empty);
                Response.Add(string.IsNullOrEmpty(CommonBAL.GetPlanUnderwriterByPlanId(id)) ? string.Empty : CommonBAL.GetPlanUnderwriterByPlanId(id));
                int WaitingPeriod = CommonBAL.GetWaitingPeriodByPlanId(id);

                if (objPolicyModel != null)
                {
                    if (objPolicyModel.WaitingPeriod != 0 && objPolicyModel.WaitingPeriod == null)
                    {
                        Response.Add(DateTime.Now.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
                    }
                    else if (date.ToLongDateString() != "" || date.ToLongDateString() != string.Empty)
                    {
                        DateTime PolicystartDate = Convert.ToDateTime(date);
                        Response.Add(PolicystartDate.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
                    }
                }
                if (objPolicyModel != null)
                {
                    Response.Add(objPolicyModel.totalPremium == null || objPolicyModel.totalPremium == "" ? "0" : objPolicyModel.totalPremium);
                    Response.Add(objPolicyModel.CoverAmount == null || objPolicyModel.CoverAmount == "" ? "0" : objPolicyModel.CoverAmount);
                }
                else
                {
                    Response.Add("0");
                    Response.Add("0");
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return Json(Response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [PageRightsAttribute(CurrentPageId = 4)]
        public ActionResult BindMembers([System.Web.Http.FromUri]string IdNumber, [System.Web.Http.FromUri]string PassportNumber, [System.Web.Http.FromBody] Model.Search.BaseSearch search)
        {

            var searchResult = new Funeral.Model.SearchResult<Model.Search.BaseSearch, MembersModel>(search, new List<MembersModel>(), o => o.Surname.Contains(search.SarchText));

            try
            {

                List<MembersModel> policies = new List<MembersModel>();
                if (IdNumber == "" || IdNumber == null || IdNumber == "0")
                    policies = MembersBAL.GetPolicyByMemberPassportNumber(PassportNumber.ToString(), this.CurrentParlourId).ToList();
                else
                    policies = MembersBAL.GetPolicyByMemberIDNumber(IdNumber.ToString(), this.CurrentParlourId).ToList();

                PolicyModel objPolicyModel = new PolicyModel();
                objPolicyModel = CommonBAL.GetPlanSubscriptionByPlanId(policies[0].fkiPlanID, CurrentParlourId).FirstOrDefault();

                var totalPremium = MembersBAL.SumOfPremium(policies[0].fkiPlanID, policies[0].FK_MemberId, CurrentParlourId);

                MembersModel model = MembersBAL.GetMemberByID(Convert.ToInt32(MemberId), CurrentParlourId);

                foreach (var item in policies)
                {
                    item.PolicyPremium = objPolicyModel.PlanSubscription;
                    item.TotalPremium = totalPremium;
                    item.EasyPayNo = model.EasyPayNo;
                    item.RefNumber = model.RefNumber;
                }
                search.TotalRecord = policies.Count;
                return Json(new Funeral.Model.SearchResult<Model.Search.BaseSearch, MembersModel>(search, policies, o => o.Surname.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, MembersModel>.Error(searchResult, ex));
            }
        }
        [PageRightsAttribute(CurrentPageId = 4)]
        public ActionResult UpdatePolicyPopup(MembersModel policy)
        {
            MembersModel model = MembersBAL.GetMemberByID(MemberId, CurrentParlourId);
            model.pkiMemberID = MemberId;
            model.MemberBranch = policy.MemberBranch;
            model.Agent = policy.Agent;
            model.MemberSociety = policy.MemberSociety;
            model.InceptionDate = policy.InceptionDate;
            model.fkiPlanID = policy.fkiPlanID;
            model.MemeberNumber = policy.MemeberNumber;
            model.EasyPayNo = policy.EasyPayNo;
            model.RefNumber = policy.RefNumber;
            model.Email = policy.Email;
            model.MemberBranch = policy.MemberBranch;
            model.ModifiedUser = UserName;
            //model.

            if (model.StartDate == null || model.StartDate == DateTime.MinValue)
            {
                model.StartDate = DateTime.Now;
            }

            if (model.CoverDate == null || model.CoverDate == DateTime.MinValue)
            {
                model.CoverDate = DateTime.Now;
            }

            model.CustomId1 = policy.CustomId1;
            model.CustomId2 = policy.CustomId2;
            model.CustomId3 = policy.CustomId3;

            //================================================================ 
            int retID = MembersBAL.SaveMembers(model);
            MemberId = retID;
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        //public void bindDependencyType()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    FamilyDependencyTypeModel[] objFamilyDependencyModel = client.GetAllFamilyDependencyTypes();
        //    ddlDependencyType.DataTextField = "DepStatus_Code";
        //    ddlDependencyType.DataValueField = "DepStatus_ID";
        //    ddlDependencyType.DataSource = objFamilyDependencyModel;
        //    ddlDependencyType.DataBind();
        //    ddlDependencyType.Items.Insert(0, new ListItem("Select", "0"));

        //    ddlCopiedPolicyDependencyType.DataTextField = "DepStatus_Code";
        //    ddlCopiedPolicyDependencyType.DataValueField = "DepStatus_ID";
        //    ddlCopiedPolicyDependencyType.DataSource = objFamilyDependencyModel;
        //    ddlCopiedPolicyDependencyType.DataBind();
        //    ddlCopiedPolicyDependencyType.Items.Insert(0, new ListItem("Select", "0"));


        //}

        public void BindReportCopyData(int pkId)
        {
            //PkID = Convert.ToInt32(ViewState["ID"]);
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.Connection.Open();
            com.CommandText = "CopyPolicyOfMember";
            com.Parameters.Add(new SqlParameter("@id", pkId));
            com.Parameters.Add(new SqlParameter("@parlourid", CurrentParlourId));

            int SendOpration = Convert.ToInt32(com.ExecuteScalar());
            com.Connection.Close();
            if (SendOpration > 0)
            {
                //ShowMessage(ref lblMessage, MessageType.Success, "Record Copy successfully");

            }
            else
            {
                // ShowMessage(ref lblMessage, MessageType.Success, "Record Copy UnSuccessfully");
            }

        }

        public JsonResult btnCopyPolicy(int PKiD)
        {
            //BindReportCopyData(PKiD);
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.Connection.Open();
            com.CommandText = "CopyPolicyOfMember";
            com.Parameters.Add(new SqlParameter("@id", PKiD));
            com.Parameters.Add(new SqlParameter("@parlourid", CurrentParlourId));

            int SendOpration = Convert.ToInt32(com.ExecuteScalar());
            com.Connection.Close();
            //var Message = "";
            //if (SendOpration > 0)
            //{
            //    Message = "Record Copy successfully";
            //}
            //else
            //{
            //    Message = "Record Copy UnSuccessfully";
            //}
            return Json(SendOpration, JsonRequestBehavior.AllowGet);
        }

        public void btnPolicyDoc()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            //string filenameExtension;
            string filename;
            string result;

            try
            {
                ReportViewer rpw = new ReportViewer();
                rpw.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new MyReportServerCredentials();
                rpw.ServerReport.ReportServerCredentials = irsc;

                rpw.ProcessingMode = ProcessingMode.Remote;
                rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/Policy Doc";
                ReportParameterCollection reportParameters = new ReportParameterCollection();

                reportParameters.Add(new ReportParameter("MemberID", MemburNumber.ToString()));
                reportParameters.Add(new ReportParameter("Parlourid", CurrentParlourId.ToString()));
                rpw.ServerReport.SetParameters(reportParameters);
                string ExportTypeExtensions = "pdf";
                byte[] bytes = rpw.ServerReport.Render(ExportTypeExtensions, null, out mimeType, out encoding, out ExportTypeExtensions, out streamids, out warnings);
                filename = string.Format("{0}.{1}", "Policy Doc", ExportTypeExtensions);

                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
                result = "true";

            }
            catch (Exception ex)
            {
                result = ex.Message;
                //result = "The attempt to connect to the report server failed.  Check your connection information and that the report server is a compatible version.    ";
                //ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
            }
            //return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void btnParticipationCertificate()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            //string filenameExtension;
            string filename;
            string result;

            try
            {
                ReportViewer rpw = new ReportViewer();
                rpw.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new MyReportServerCredentials();
                rpw.ServerReport.ReportServerCredentials = irsc;

                rpw.ProcessingMode = ProcessingMode.Remote;
                rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/Participation Certificate";
                ReportParameterCollection reportParameters = new ReportParameterCollection();

                reportParameters.Add(new ReportParameter("MemberID", MemburNumber.ToString()));
                reportParameters.Add(new ReportParameter("Parlourid", CurrentParlourId.ToString()));
                rpw.ServerReport.SetParameters(reportParameters);
                string ExportTypeExtensions = "pdf";
                byte[] bytes = rpw.ServerReport.Render(ExportTypeExtensions, null, out mimeType, out encoding, out ExportTypeExtensions, out streamids, out warnings);
                filename = string.Format("{0}.{1}", "Policy Doc", ExportTypeExtensions);

                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
                result = "true";

            }
            catch (Exception ex)
            {
                result = ex.Message;
                //result = "The attempt to connect to the report server failed.  Check your connection information and that the report server is a compatible version.    ";
                //ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
            }
            //return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PageRightsAttribute(CurrentPageId = 4)]
        public JsonResult DependentForDuplicatePolicy(FamilyDependencyModel dependency)
        {
            if (dependency.IDNumber == "" || dependency.IDNumber == string.Empty)
                dependency.IDNumber = "0";

            FamilyDependencyModel ObjFamilyDependencyModel;
            ObjFamilyDependencyModel = MembersBAL.GetDependencByIDNum(dependency.IDNumber, CurrentParlourId, MemberId);

            if (ObjFamilyDependencyModel == null)
            {
                ObjFamilyDependencyModel = new FamilyDependencyModel();
                ObjFamilyDependencyModel.Age = AgeFromDOB(Convert.ToDateTime(dependency.DateOfBirth));
                ObjFamilyDependencyModel.parlourid = CurrentParlourId;
                ObjFamilyDependencyModel.MemberId = this.MemberId;
                if (dependency.StartDate == null || dependency.StartDate == DateTime.MinValue)
                {
                    ObjFamilyDependencyModel.StartDate = DateTime.Now;
                }
                else
                {
                    ObjFamilyDependencyModel.StartDate = dependency.StartDate;
                }
                if (dependency.CoverDate == null || dependency.CoverDate == DateTime.MinValue)
                {
                    ObjFamilyDependencyModel.CoverDate = DateTime.Now;
                }
                else
                {
                    ObjFamilyDependencyModel.CoverDate = dependency.CoverDate;
                }
                if (dependency.InceptionDate == null || dependency.InceptionDate == DateTime.MinValue)
                {
                    ObjFamilyDependencyModel.InceptionDate = DateTime.Now;
                }
                else
                {
                    ObjFamilyDependencyModel.InceptionDate = dependency.InceptionDate;
                }
                if (dependency.DateOfBirth == null || dependency.DateOfBirth == DateTime.MinValue)
                {
                    ObjFamilyDependencyModel.DateOfBirth = DateTime.Now;
                }
                else
                {
                    ObjFamilyDependencyModel.DateOfBirth = dependency.DateOfBirth;
                }
                ObjFamilyDependencyModel.FullName = dependency.FullName;
                ObjFamilyDependencyModel.Surname = dependency.Surname;
                ObjFamilyDependencyModel.IDNumber = dependency.IDNumber;
                ObjFamilyDependencyModel.Gender = dependency.Gender;
                ObjFamilyDependencyModel.Relationship = Convert.ToInt32("1");
                ObjFamilyDependencyModel.DependencyType = dependency.DependencyType;
                ObjFamilyDependencyModel.Premium = dependency.Premium;
            }
            ObjFamilyDependencyModel.MemberId = dependency.MemberId;
            int documentId = MembersBAL.SaveFamilyDependency(ObjFamilyDependencyModel);

            return Json(dependency, JsonRequestBehavior.AllowGet);
            //return Json("", JsonRequestBehavior.AllowGet);
        }


        public JsonResult DependencyStartdateChange(int id, string date)
        {
            //List<string> Response = new List<string>();
            var Response = string.Empty;
            if (date != "" || date != string.Empty)
            {
                DateTime PolicystartDate = Convert.ToDateTime(date);
                Response = PolicystartDate.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy");
            }
            else
            {
                Response = DateTime.Now.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy");
            }
            //return Response;
            return Json(Response, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [PageRightsAttribute(CurrentPageId = 4)]
        public JsonResult SaveAddonProduct(MemberAddonProductsModel objProductModel)
        {
            var Message = string.Empty;
            try
            {
                objProductModel.DateCreated = DateTime.Now;
                objProductModel.LastModified = DateTime.Now;
                objProductModel.UserID = UserID.ToString();
                objProductModel.ModifiedUser = User.Identity.Name;
                objProductModel.parlourid = CurrentParlourId;
                objProductModel.Deleted = 0;
                objProductModel.pkiMemberProductID = Guid.NewGuid();
                if (objProductModel.fkiMemberid != 0)
                {
                    var AddOnProductID = MembersBAL.NewSaveAddonProducts(objProductModel);
                }
                if (objProductModel.fkiMemberid == 0)
                { Session["Product"] = objProductModel; }
                Message = "Addon product add successfully.";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }


        public void saveAddproduct(int id)
        {
            MemberAddonProductsModel profile = (MemberAddonProductsModel)Session["Product"];
            if (profile != null)
            {
                profile.fkiMemberid = id;
                int AddonProductID = MembersBAL.SaveAddonProducts(profile);
                Session["Product"] = null;
            }
        }

        [HttpPost]
        //public void UpdatePolicyStatus(string policyStatus, int memberId)

        [PageRightsAttribute(CurrentPageId = 4)]
        public void UpdatePolicyStatus(string policyStatus, int memberId)

        {
            MembersBAL.UpdateMemberPolicyStatus(policyStatus, memberId, UserName);
            CommonBAL.SaveAudit(UserName, CurrentParlourId, "Policy Status Changed");
        }
        //[HttpPost]
        //[PageRightsAttribute(CurrentPageId = 4)]
        //public JsonResult SaveBeneficiary(Beneficiary_model objModel)
        //{
        //    var Message = string.Empty;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var CurrenrPer = MembersBAL.SumOfBeneficiaryPercentage(objModel.pkiMemberID);
        //            var NewPer = Convert.ToDouble(objModel.Percentages);
        //            var TotalSum = CurrenrPer + NewPer;
        //            if (TotalSum < 100 || TotalSum == 100)
        //            {
        //                objModel.ModifiedUser = User.Identity.Name;
        //                objModel.parlourid = CurrentParlourId;
        //                var AddOnProductID = MembersBAL.SaveBeneficiary(objModel);
        //                if (objModel.pkiBeneficiaryID == 0)
        //                    Message = "Inserted";
        //                else
        //                    Message = "Updated";
        //            }
        //            else 
        //                Message = "PercentageLimit";
        //        }
        //        else
        //        {
        //            foreach (ModelState modelState in ViewData.ModelState.Values)
        //            {
        //                foreach (ModelError error in modelState.Errors)
        //                    Message += "<li>" + error.ErrorMessage + "</li>";
        //            }
        //            if (objModel.DateOfBirth_Beneficiary == DateTime.Now)
        //                Message += "<li>Please enter Birth Date</li>";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Message = ex.Message;
        //    }
        //    return Json(Message, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        [PageRightsAttribute(CurrentPageId = 4)]
        public JsonResult SaveBeneficiary(Beneficiary_model objModel)
        {
            //var ObjBeneficiary = MembersBAL.GetBeneficiaryByIDNo(objModel.IDNumber_Beneficiary,ParlourId);
            var Message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    if (objModel.pkiBeneficiaryID == 0 && MembersBAL.GetBeneficiaryByIDNo(objModel.IDNumber_Beneficiary, ParlourId,MemberId) != null)
                    {
                        Message = "exist";
                    }
                    else
                    {
                        var CurrenrPer = MembersBAL.SumOfBeneficiaryPercentage(objModel.pkiMemberID);
                        var NewPer = Convert.ToDouble(objModel.Percentages);
                        //var newSum = 0.0;
                        var NewCurrent = 0.0;

                        var TotalSum = 0.0;
                        if (objModel.pkiBeneficiaryID > 0)
                        {

                            objModel.ModifiedUser = User.Identity.Name;
                            objModel.parlourid = CurrentParlourId;
                            //var Beneficiary = MembersBAL.SaveBeneficiary(objModel);
                            Beneficiary_model beneficiaryDetails = MembersBAL.GetBeneficiaryByID(objModel.pkiBeneficiaryID, CurrentParlourId);
                            NewCurrent = CurrenrPer - Convert.ToDouble(beneficiaryDetails.Percentages);
                            //Beneficiary_model beneficiary = new Beneficiary_model();
                            //beneficiary.Percentages = beneficiaryDetails.Percentages;
                            NewPer = Convert.ToDouble(objModel.Percentages);
                            //var sum = Convert.ToDouble(OnChange) + CurrenrPer;Convert.ToDouble(NewDetails.Percentages)
                            TotalSum = NewCurrent + NewPer;
                        }
                        else if (objModel.pkiBeneficiaryID == 0)
                        {
                            TotalSum = CurrenrPer + NewPer;
                        }
                        //NewPer = newSum ;

                        if (TotalSum < 100 || TotalSum == 100)
                        {
                            objModel.ModifiedUser = User.Identity.Name;
                            objModel.parlourid = CurrentParlourId;
                            var AddOnProductID = MembersBAL.SaveBeneficiary(objModel);
                            if (objModel.pkiBeneficiaryID == 0)
                                Message = "Inserted";
                            else
                                Message = "Updated";
                        }
                        else
                            Message = "PercentageLimit";
                    }
                }
                else
                {
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                            Message += "<li>" + error.ErrorMessage + "</li>";
                    }
                    if (objModel.DateOfBirth_Beneficiary == DateTime.Now)
                        Message += "<li>Please enter Birth Date</li>";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchBeneficiaryData(Model.Search.BaseSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.BaseSearch, Beneficiary_model>(search, new List<Beneficiary_model>(), o => o.FullName_Beneficiary.Contains(search.SarchText));
            try
            {
                var products = MembersBAL.SearchBeneficiaryData(MemberId).ToList();
                foreach (var prdct in products)
                {
                    prdct.DependencyName = CommonBAL.GetUserTypes().FirstOrDefault().UserTypeName;
                }
                search.TotalRecord = products.Count;
                return Json(new Funeral.Model.SearchResult<Model.Search.BaseSearch, Beneficiary_model>(search, products, o => o.FullName_Beneficiary.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, Beneficiary_model>.Error(searchResult, ex));
            }
        }
        public ActionResult DeleteBeneficiary(int pkiBeneficiaryID)
        {
            Beneficiary_model beneficiary = new Beneficiary_model();
            beneficiary.ModifiedUser = UserID.ToString();
            string ModifiedUser = beneficiary.ModifiedUser;
            MembersBAL.DeleteBeneficiary(pkiBeneficiaryID, ModifiedUser);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BindGroupByCompanyId(Guid CompanyId)
        {
            var Company = CommonBAL.GetSocietyByParlourId(CompanyId).Select(x => new SelectListItem() { Text = x.SocietyName, Value = x.pkiSocietyID.ToString() });
            return Json(Company, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindPlanByCompanyId(Guid CompanyId)
        {
            PlanModel plan = new PlanModel();
            var Company = CommonBAL.GetPlanByParlourId(CompanyId).Select(x => new SelectListItem() { Text = x.PlanName, Value = x.pkiPlanID.ToString() });
            plan.Cover = 0;
            plan.PlanSubscription = 0;
            return Json(Company, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindBranchByCompanyId(Guid CompanyId)
        {
            var Company = CommonBAL.GetBranchByParlourId(CompanyId).Select(x => new SelectListItem() { Text = x.BranchName, Value = x.Brancheid.ToString() });
            return Json(Company, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void ChangeParlour(Guid parlourId, int MemberId)
        {
            CurrentParlourId = parlourId;
        }
        public JsonResult BindPolicyByPlanId(int Id)
        {
            //var Members = new ManageMembersVM();
            //ManageMembersVM Managemembers = new ManageMembersVM();
            var DependencyTypeList = CommonBAL.GetUserTypesByPlanID(CurrentParlourId, Id).Select(x => new SelectListItem() { Text = x.UserTypeName, Value = x.UserTypeId.ToString() });
            return Json(DependencyTypeList, JsonRequestBehavior.AllowGet);
        }
        public bool ValidateBankAccount(string ServiceKey, string AccountNumber, string BranchCode, string AccountType)
        {
            if (AccountType == "Current" || AccountType == "Cheque")
            {
                AccountType = "1";
            }
            else
            {
                AccountType = "2";
            }
            if (AccountNumber == null)
            {
                AccountNumber = "";
            }
            if (BranchCode == null)
            {
                BranchCode = "";
            }


            bool outcome;
            //initialise client 
            niws_validation.NIWS_ValidationClient client = new niws_validation.NIWS_ValidationClient();

            //call the ValidateBankAccount method equal to a string variable
            string Request = client.ValidateBankAccount(ServiceKey, AccountNumber, BranchCode.TrimStart().TrimEnd(), AccountType);

            //close client after response is received
            client.Close();

            //convert response received for request
            int response = Convert.ToInt32(Request);

            //if 0 save to system or do something else
            switch (response)
            {
                case 0: //do something
                    outcome = true;
                    break;
                case 1: //do something
                    outcome = false;
                    break;
                case 2: //do something
                    outcome = false;
                    break;
                case 3: //do something
                    outcome = false;
                    break;
                case 4: //do something
                    outcome = false;
                    break;
                case 100: //do something
                    outcome = false;
                    break;
                default:
                    outcome = false;
                    break;
            }

            return outcome;
        }

        public void AuditTrail()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            //string filenameExtension;
            string filename;
            string result;

            try
            {
                ReportViewer rpw = new ReportViewer();
                rpw.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new MyReportServerCredentials();
                rpw.ServerReport.ReportServerCredentials = irsc;

                rpw.ProcessingMode = ProcessingMode.Remote;
                rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/AuditTrail_Report";
                ReportParameterCollection reportParameters = new ReportParameterCollection();

                reportParameters.Add(new ReportParameter("MemberID", MemberId.ToString()));
                reportParameters.Add(new ReportParameter("Parlourid", CurrentParlourId.ToString()));
                rpw.ServerReport.SetParameters(reportParameters);
                string ExportTypeExtensions = "pdf";
                byte[] bytes = rpw.ServerReport.Render(ExportTypeExtensions, null, out mimeType, out encoding, out ExportTypeExtensions, out streamids, out warnings);
                filename = string.Format("{0}.{1}", "AuditTrail_Report", ExportTypeExtensions);

                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
                result = "true";

            }
            catch (Exception ex)
            {
                result = ex.Message;
                //result = "The attempt to connect to the report server failed.  Check your connection information and that the report server is a compatible version.    ";
                //ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
            }
            //return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult SendEmailReportPartial()
        {
            ReportParameters modal = new ReportParameters();
            modal.parlourId = ParlourId;
            modal.fromDate = DateTime.Now;
            modal.toDate = DateTime.Now;
            modal.ReferenceNumber = MemburNumber;
            return PartialView("_ReportEmailSendModal", modal);
        }
        [HttpPost]
        public ActionResult SendEmailReport(ReportParameters modal)
        {
            if (ModelState.IsValid)
            {
                string ExportTypeExtensions = "pdf";
                modal.ReportName = "Policy Doc";
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                string filename;

                ReportViewer rpw = new ReportViewer();
                rpw.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new MyReportServerCredentials();
                rpw.ServerReport.ReportServerCredentials = irsc;


                rpw.ProcessingMode = ProcessingMode.Remote;
                rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/" + modal.ReportName;
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                //reportParameters.Add(new ReportParameter("DateFrom", modal.fromDate.ToString("yyyy/MM/dd")));
                //reportParameters.Add(new ReportParameter("DateTo", modal.toDate.ToString("yyyy/MM/dd")));
                reportParameters.Add(new ReportParameter("Parlourid", modal.parlourId.ToString()));
                reportParameters.Add(new ReportParameter("MemberID", modal.ReferenceNumber.ToString()));
                rpw.ServerReport.SetParameters(reportParameters);
                byte[] bytes = rpw.ServerReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                filename = string.Format("{0}.{1}", modal.ReportName, ExportTypeExtensions);

                ///---------------------
                //Warning[] warnings;
                //string[] streamids;
                //string mimeType;
                //string encoding;
                ////string filenameExtension;
                //string filename;
                //string result;

                //ReportViewer rpw = new ReportViewer();
                //rpw.ProcessingMode = ProcessingMode.Remote;
                //IReportServerCredentials irsc = new MyReportServerCredentials();
                //rpw.ServerReport.ReportServerCredentials = irsc;

                //rpw.ProcessingMode = ProcessingMode.Remote;
                //rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                //rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/Policy Doc";
                //ReportParameterCollection reportParameters = new ReportParameterCollection();

                //reportParameters.Add(new ReportParameter("MemberID", MemburNumber.ToString()));
                //reportParameters.Add(new ReportParameter("Parlourid", CurrentParlourId.ToString()));
                //rpw.ServerReport.SetParameters(reportParameters);
                //string ExportTypeExtensions = "pdf";
                //byte[] bytes = rpw.ServerReport.Render(ExportTypeExtensions, null, out mimeType, out encoding, out ExportTypeExtensions, out streamids, out warnings);
                //filename = string.Format("{0}.{1}", "Policy Doc", ExportTypeExtensions);

                //Response.ClearHeaders();
                //Response.Clear();
                //Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                //Response.ContentType = mimeType;
                //Response.BinaryWrite(bytes);
                //Response.Flush();
                //Response.End();
                //result = "true";
                ///
                ///


                if (!string.IsNullOrEmpty(modal.Email))
                {
                    MemoryStream s = new MemoryStream(bytes);
                    s.Seek(0, SeekOrigin.Begin);
                    Attachment a = new Attachment(s, filename);
                    MailMessage message = new MailMessage(ConfigurationManager.AppSettings["ReportEmailSenderId"].ToString().Trim(), modal.Email.Trim(), modal.ReportName, "");
                    message.Attachments.Add(a);
                    SmtpClient client = new SmtpClient();
                    client.Send(message);
                    TempData["message"] = ShowMessage(MessageType.Success, "Email Sent Successfully");
                }
                else
                {
                    TempData["message"] = ShowMessage(MessageType.Success, "Please Enter Email");
                }
            }

            return Redirect(Request.UrlReferrer.ToString());
        }
        //=================TEST
        //public JsonResult BindPlanByCompanyId(Guid CompanyId)
        //{
        //    PlanModel plan = new PlanModel();
        //    var Company = CommonBAL.GetPlanByParlourId(CompanyId).Select(x => new SelectListItem() { Text = x.PlanName, Value = x.pkiPlanID.ToString() });
        //    plan.Cover = 0;
        //    plan.PlanSubscription = 0;
        //    return Json(Company, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult BindBranchByCompanyId(Guid CompanyId)
        //{
        //    var Company = CommonBAL.GetBranchByParlourId(CompanyId).Select(x => new SelectListItem() { Text = x.BranchName, Value = x.Brancheid.ToString() });
        //    return Json(Company, JsonRequestBehavior.AllowGet);
        //}
        //=================END

    }

}