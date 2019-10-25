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
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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

        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();

        public static string GetdataPremium;

        [PageRightsAttribute(CurrentPageId = 4)]
        public ActionResult Index()
        {
            //ViewBag.HasAccess = HasAccess;
            if (ViewBag.HasAccess == true)
            {
                var statusList = client.GetStatus(FuneralEnum.StatusAssociatedTable.Members.ToString()).Select(x => new SelectListItem() { Text = x.ID.ToString(), Value = x.Status });
                ViewBag.StatusList = statusList;

                //ViewBag.HasEditRight = HasEditRight;
                //ViewBag.HasDeleteRight = HasDeleteRight;

                ViewBag.totalPremium = Currency;

                LoadStatus();
                LoadEntriesCount();
                BindCompanyList();

                Model.Search.MemberSearch search = new Model.Search.MemberSearch();
                search.CompanyId = new Guid(ParlourId.ToString());
                search.PageNum = 1;
                search.PageSize = 10;
                search.SarchText = "";
                search.SortBy = "";
                search.SortOrder = "Asc";
                search.StatusId = "0";
                search.TotalRecord = 0;

                var searchResult = new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, new List<MembersModel>(), o => o.IDNumber.Contains(search.SarchText) || o.MemeberNumber.Contains(search.SarchText));
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
            //List<SelectListItem> statusListItems = new List<SelectListItem>();
            //foreach (var statusListItem in statusList)
            //{
            //    statusListItems.Add(new SelectListItem { Text = statusListItem.Status, Value = Convert.ToString(statusListItem.ID) });
            //}

            ViewBag.Statuses = statusList;
        }
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
        public void BindCompanyList()
        {
            List<SelectListItem> companyListItems = new List<SelectListItem>();
            List<ApplicationSettingsModel> model = new List<ApplicationSettingsModel>();

            if (this.IsAdministrator)
            {
                model = ToolsSetingBAL.GetAllApplicationList(ParlourId, 1, 0).ToList();

                if (model == null)
                {
                    model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
                }
            }
            else
            {
                model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
            }

            ViewBag.Companies = model;
        }
        [HttpPost]
        public ActionResult SearchData(Model.Search.MemberSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, new List<MembersModel>(), o => o.IDNumber.ToLower().Contains(search.SarchText.ToLower()));
            try
            {
                var members = MembersBAL.GetAllMembers(ParlourId, search.PageSize, search.PageNum, search.SarchText, search.SortBy, search.SortOrder, search.StatusId.ToString());
                return Json(new Funeral.Model.SearchResult<Model.Search.MemberSearch, MembersModel>(search, members.MemberList, o => o.IDNumber.ToLower().Contains(search.SarchText.ToLower()) || o.MemeberNumber.ToLower().Contains(search.SarchText.ToLower()) || o.Surname.ToLower().Contains(search.SarchText.ToLower()) || o.FullNames.ToLower().Contains(search.SarchText.ToLower()) || o.Cellphone.ToLower().Contains(search.SarchText.ToLower()) || o.EasyPayNo.ToLower().Contains(search.SarchText.ToLower())));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.MemberSearch, MembersModel>.Error(searchResult, ex));
            }
        }
        [HttpPost]
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
        public ActionResult ManageMembers(int pkiMemberID = 0)
        {
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
            app = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
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
                member = MembersBAL.GetMemberByID(pkiMemberID, ParlourId);

            var Managemembers = new ManageMembersVM();


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
            Managemembers.AgentList = MembersBAL.SelectAllAgent(ParlourId).Select(x => new SelectListItem() { Text = x.Agent, Value = x.AgentID.ToString() });
            Managemembers.PolicyList = CommonBAL.GetPolicyByParlourId(ParlourId).Select(x => new SelectListItem() { Text = x.PlanName, Value = x.pkiPlanID.ToString() });
            Managemembers.countryList = MembersBAL.GetCountry().Select(x => new SelectListItem() { Text = x.Name, Value = x.CountryCode });
            Managemembers.BranchList = CommonBAL.GetBranchByParlourId(ParlourId).Select(x => new SelectListItem() { Text = x.BranchName, Value = x.Brancheid.ToString() });
            Managemembers.ProductAddOnList = MembersBAL.SelectProductName(ParlourId).Select(x => new SelectListItem() { Text = x.ProductName, Value = x.pkiProductID.ToString() });
            Managemembers.SocietyList = CommonBAL.GetSocietyByParlourId(ParlourId).Select(x => new SelectListItem() { Text = x.SocietyName, Value = x.pkiSocietyID.ToString() });
            Managemembers.CustomPaymentMethod = CustomDetailsBAL.GetAllCustomDetailsByParlourId(ParlourId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.Custom1)).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
            Managemembers.CustomGrouping2 = CustomDetailsBAL.GetAllCustomDetailsByParlourId(ParlourId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.Custom2)).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
            Managemembers.CustomGrouping3 = CustomDetailsBAL.GetAllCustomDetailsByParlourId(ParlourId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.Custom3)).Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
            Managemembers.Member = member;            
            Managemembers.DependencyTypeList = CommonBAL.GetUserTypes().Select(x => new SelectListItem() { Text = x.UserTypeName, Value = x.UserTypeId.ToString() });

            Managemembers.ExtendedFamily = MembersBAL.GetExtendedFamilyList(ParlourId, MemberId).Select(x => new SelectListItem() { Text = x.FullName, Value = x.pkiDependentID.ToString() });



            PolicyModel objPolicyModel = CommonBAL.GetPlanSubscriptionByPlanId(member.fkiPlanID, ParlourId).ToList().FirstOrDefault();
            Managemembers.Member.PolicyPremium = objPolicyModel != null ? objPolicyModel.PlanSubscription : string.Empty;
            if (pkiMemberID != 0)
            {
                string totalPremium = BindTotalPremium(pkiMemberID);
                Managemembers.TotalPremium = Convert.ToInt32(totalPremium);
            }

            var BankMemberNumber = CommonBAL.GetMemberNumber(ParlourId);

            var secureUserGroupModel = ToolsSetingBAL.GetUserAccessByID(UserID, ParlourId);

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
            MemburNumber = client.GetMemberNumber(ParlourId);
            ViewBag.IsMemberNumberEmpty = false;
            if (MemburNumber == string.Empty || MemburNumber == "")
            {
                ViewBag.IsMemberNumberEmpty = true;
            }

            //txtEasyToPay.Enabled = false;
        }
        private void ValidateInception(ref MembersModel member, int pkiMemberId)
        {

            SecureUserGroupsModel model = ToolsSetingBAL.GetUserAccessByID(UserID, ParlourId);

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
            ApplicationSettingsModel app = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
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
        //private void ValidateIdNumber()
        //{
        //    ViewBag.IsIdNumberReadOnly = true;
        //    if (IsAdministrator || IsSuperUser)
        //    {
        //        ViewBag.IsIdNumberReadOnly = false;
        //    }
        //}

        [PageRightsAttribute(CurrentPageId = 4)]
        private void ValidateCreateRights()
        {
            ViewBag.HasCreateRight = HasCreateRight;
        }
        public string BindTotalPremium(int pkiMemberId)
        {
            if (pkiMemberId != 0)
            {
                return client.SumPremium(pkiMemberId, ParlourId).ToString();
            }
            return "0.0";
        }
        public ActionResult BindInvoices(Model.Search.BaseSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.BaseSearch, MemberInvoiceModel>(search, new List<MemberInvoiceModel>(), o => o.InvNumber.Contains(search.SarchText));

            try
            {
                var invoices = MembersBAL.GetInvoicesByMemberID(ParlourId, MemberId).ToList();
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
            if (pkiMemberId != 0)
            {
                member = MembersBAL.GetMemberByID(pkiMemberId, ParlourId);
                PolicyModel objPolicyModel = CommonBAL.GetPlanSubscriptionByPlanId(member.fkiPlanID, ParlourId).ToList().FirstOrDefault();
                member.PolicyPremium = objPolicyModel != null ? objPolicyModel.PlanSubscription : string.Empty;
                member.TotalPremium = Convert.ToInt32(BindTotalPremium(pkiMemberId));
            }

            return Json(new { success = true, Member = member }, JsonRequestBehavior.AllowGet);
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
            addOnProduct.parlourid = this.ParlourId;
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
            addOnProduct.parlourid = this.ParlourId;
            addOnProduct.pkiMemberProductID = addOnProduct.pkiMemberProductID;
            var AddonProductID = MembersBAL.NewAddonProductUpdateMember(addOnProduct);
            return Json(new { success = true, Product = addOnProduct, msg = "Product Updated Successfully" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteAddOnProduct(Guid pkiProductId)
        {
            MembersBAL.MemberAddonProductsRemove(pkiProductId);
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
            return Json(new { Product, ProductCost = product.FirstOrDefault().ProductCost }, JsonRequestBehavior.AllowGet); //new { ProductPrice = Currency.Trim() + " " + product.FirstOrDefault().ProductCost, ProductCost = product.FirstOrDefault().ProductCost }
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
                                    ObjSupportedDocumentModel.parlourid = this.ParlourId;
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
                var familyDependancies = MembersBAL.GetFamilyDependencyByMemberID(ParlourId, MemberId);
                search.Currency = Currency;
                search.TotalRecord = familyDependancies.Count;
                return Json(new Funeral.Model.SearchResult<Model.Search.BaseSearch, FamilyDependencyModel>(search, familyDependancies, o => o.FullName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.BaseSearch, FamilyDependencyModel>.Error(searchResult, ex));
            }
        }
        //public ActionResult SaveDependancy(FamilyDependencyModel depe)
        //{
        //    //memberNote.Notes = memberNote.Notes.Trim();
        //    //memberNote.fkiMemberID = MemberId;
        //    //memberNote.NoteDate = DateTime.Now;
        //    //memberNote.ModifiedUser = this.User.Identity.Name;
        //    //client.SaveMemberNote(memberNote);
        //    //return Json(new { success = true, Note = memberNote }, JsonRequestBehavior.AllowGet);
        //}
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
            bool isdocumentDeleted = MembersBAL.DeleteSUpportdocumentById(pkiPictureID);
            if (isdocumentDeleted)
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetPremiumForPolicy(string policyId)
        //{
        //    var data = new List<PolicyModel>();
        //    if (!string.IsNullOrEmpty(policyId))
        //    {
        //        data = CommonBAL.GetPlanSubscriptionByPlanIdNewMember(Convert.ToInt32(policyId));
        //    }
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetWaitingPeriodByPlanId(int policyId, string Date)
        //{
        //    DateTime PolicyDate = Convert.ToDateTime(Date);


        //    var data = CommonBAL.GetWaitingPeriodByPlanId(policyId);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
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

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
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
                Member.parlourid = ParlourId;
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

            Member.ModifiedUser = UserName;
            Member.Active = false;

            int retId = MembersBAL.SaveMembers(Member);
            Member.pkiMemberID = retId;

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

                if (objPolicyModel.WaitingPeriod != 0 && objPolicyModel.WaitingPeriod == null)
                {
                    Response.Add(DateTime.Now.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
                }
                else if (date != null)
                {
                    DateTime PolicystartDate = Convert.ToDateTime(date);
                    Response.Add(PolicystartDate.AddMonths(CommonBAL.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
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
            var memberDetails = MembersBAL.GetMemberByID(MemberId, ParlourId);
            return Json(memberDetails, JsonRequestBehavior.AllowGet);
        }
        [PageRightsAttribute(CurrentPageId = 4)]
        public JsonResult UpdateManageMembers(MembersModel Member)
        {
            return Json(Member, JsonRequestBehavior.AllowGet);
        }

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
            ObjFamilyDependencyModel = MembersBAL.GetDependencByIDNum(dependency.IDNumber, ParlourId, MemberId);

            if (ObjFamilyDependencyModel == null)
            {
                ObjFamilyDependencyModel = new FamilyDependencyModel();
                ObjFamilyDependencyModel.Age = AgeFromDOB(Convert.ToDateTime(dependency.DateOfBirth));
                ObjFamilyDependencyModel.parlourid = this.ParlourId;
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
                ObjFamilyDependencyModel.Gender = dependency.Gender;
                ObjFamilyDependencyModel.Relationship = Convert.ToInt32(dependency.Relationship);
                ObjFamilyDependencyModel.DependencyType = dependency.DependencyType;
                ObjFamilyDependencyModel.Premium = dependency.Premium;
            }

            int documentId = MembersBAL.SaveFamilyDependency(ObjFamilyDependencyModel);

            return Json(new { success = true, Dependency = dependency }, JsonRequestBehavior.AllowGet);
        }

        [PageRightsAttribute(CurrentPageId = 4)]
        public JsonResult UpdateDependancy(int pkiDependantId, FamilyDependencyModel dependency)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            }

            dependency.Age = AgeFromDOB(Convert.ToDateTime(dependency.DateOfBirth));
            dependency.parlourid = this.ParlourId;
            dependency.MemberId = this.MemberId;
            dependency.pkiDependentID = pkiDependantId;
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



            dependency.Relationship = Convert.ToInt32("1");


            int documentId = MembersBAL.UpdateFamilyDependency(dependency);

            return Json(dependency, JsonRequestBehavior.AllowGet);

        }
        public ActionResult BindDependency(Model.Search.BaseSearch search)
        {
            var searchResult = new Funeral.Model.SearchResult<Model.Search.BaseSearch, FamilyDependencyModel>(search, new List<FamilyDependencyModel>(), o => o.FullName.Contains(search.SarchText));

            try
            {
                var dependencies = MembersBAL.GetFamilyDependencyByMemberID(ParlourId, MemberId);
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
            bool isDependencyDeleted = MembersBAL.GetFamilyDependencyTypes(pkiDependantId);
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
                Response.Add(string.IsNullOrEmpty(client.GetPlanUnderwriterByPlanId(id)) ? string.Empty : client.GetPlanUnderwriterByPlanId(id));
                int WaitingPeriod = client.GetWaitingPeriodByPlanId(id);

                if (objPolicyModel != null)
                {
                    if (objPolicyModel.WaitingPeriod != 0 && objPolicyModel.WaitingPeriod == null)
                    {
                        Response.Add(DateTime.Now.AddMonths(client.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
                    }
                    else if (date.ToLongDateString() != "" || date.ToLongDateString() != string.Empty)
                    {
                        DateTime PolicystartDate = Convert.ToDateTime(date);
                        Response.Add(PolicystartDate.AddMonths(client.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy"));
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

        [HttpPost]
        [PageRightsAttribute(CurrentPageId = 4)]
        public ActionResult BindMembers([System.Web.Http.FromUri]string IdNumber, [System.Web.Http.FromUri]string PassportNumber, [System.Web.Http.FromBody] Model.Search.BaseSearch search)
        {

            var searchResult = new Funeral.Model.SearchResult<Model.Search.BaseSearch, MembersModel>(search, new List<MembersModel>(), o => o.Surname.Contains(search.SarchText));

            try
            {

                List<MembersModel> policies = new List<MembersModel>();
                if (IdNumber == "" || IdNumber == null || IdNumber == "0")
                    policies = MembersBAL.GetPolicyByMemberPassportNumber(PassportNumber.ToString(), this.ParlourId).ToList();
                else
                    policies = MembersBAL.GetPolicyByMemberIDNumber(IdNumber.ToString(), this.ParlourId).ToList();

                PolicyModel objPolicyModel = new PolicyModel();
                objPolicyModel = CommonBAL.GetPlanSubscriptionByPlanId(policies[0].fkiPlanID, ParlourId).FirstOrDefault();

                var totalPremium = MembersBAL.SumOfPremium(policies[0].fkiPlanID, policies[0].FK_MemberId, ParlourId);

                MembersModel model = client.GetMemberByID(Convert.ToInt32(MemberId), ParlourId);

                foreach (var item in policies)
                {
                    item.PolicyPremium = objPolicyModel.PlanSubscription;
                    item.TotalPremium = totalPremium;
                    item.EasyPayNo = model.EasyPayNo;
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
            MembersModel model = client.GetMemberByID(Convert.ToInt32(MemberId), ParlourId);
            model.pkiMemberID = Convert.ToInt32(MemberId);
            model.MemberBranch = policy.MemberBranch;
            model.Agent = policy.Agent;
            model.MemberSociety = policy.MemberSociety;
            model.InceptionDate = policy.InceptionDate;
            model.fkiPlanID = policy.fkiPlanID;
            model.MemeberNumber = policy.MemeberNumber;
            model.EasyPayNo = policy.EasyPayNo;
            model.Email = policy.Email;
            model.MemberBranch = policy.MemberBranch;
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
            int retID = client.SaveMember(model);
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
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));

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
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));

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
                //rpw.ServerReport.ReportPath = "/Unplugg IT Solution BI Reporting/Unplugg IT Busy Days //UIS All Members Report";
                rpw.ServerReport.ReportPath = "/Unplugg IT Solution BI Reporting/Policy Doc";
                // rpw.ServerReport.ReportPath = "/Unplugg IT Solution BI Reporting/UIS_Customer Payments Statement";
                ReportParameterCollection reportParameters = new ReportParameterCollection();

                reportParameters.Add(new ReportParameter("MemberID", MemburNumber.ToString()));
                reportParameters.Add(new ReportParameter("Parlourid", ParlourId.ToString()));
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
            ObjFamilyDependencyModel = MembersBAL.GetDependencByIDNum(dependency.IDNumber, ParlourId, MemberId);

            if (ObjFamilyDependencyModel == null)
            {
                ObjFamilyDependencyModel = new FamilyDependencyModel();
                ObjFamilyDependencyModel.Age = AgeFromDOB(Convert.ToDateTime(dependency.DateOfBirth));
                ObjFamilyDependencyModel.parlourid = this.ParlourId;
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
            FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
            //List<string> Response = new List<string>();
            var Response = string.Empty;
            if (date != "" || date != string.Empty)
            {
                DateTime PolicystartDate = Convert.ToDateTime(date);
                Response = PolicystartDate.AddMonths(client.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy");
            }
            else
            {
                Response = DateTime.Now.AddMonths(client.GetWaitingPeriodByPlanId(id)).ToString("dd MMM yyyy");
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
                objProductModel.parlourid = ParlourId;
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
            MembersBAL.UpdateMemberPolicyStatus(policyStatus, memberId);
        }
        [HttpPost]
        [PageRightsAttribute(CurrentPageId = 4)]
        public JsonResult SaveBeneficiary(Beneficiary_model objModel)
        {
            var Message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    objModel.ModifiedUser = User.Identity.Name;
                    objModel.parlourid = ParlourId;
                    var AddOnProductID = MembersBAL.SaveBeneficiary(objModel);
                    if (objModel.pkiBeneficiaryID == 0)
                        Message = "Inserted";
                    else
                        Message = "Updated";
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
                foreach (var prdct in products) { prdct.DependencyName = CommonBAL.GetUserTypes().Where(x => x.UserTypeId == prdct.DependencyType_Beneficiary).FirstOrDefault().UserTypeName; }
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
            MembersBAL.DeleteBeneficiary(pkiBeneficiaryID);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}