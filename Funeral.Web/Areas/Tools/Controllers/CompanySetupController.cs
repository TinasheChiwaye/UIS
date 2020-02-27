using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Areas.Admin.Controllers;
using Funeral.Web.Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static Funeral.Web.Areas.Admin.Controllers.MembersController;

namespace Funeral.Web.Areas.Tools.Controllers
{
    public class CompanySetupController : BaseToolController
    {
        public CompanySetupController() : base(16)
        {
            
            this.dbPageId = 16;
        }

        #region Properties
        public FileUploadModel _fileUploadResult { get; set; }
        #endregion

        #region Page CRUD Opertations

        [PageRightsAttribute(CurrentPageId = 16, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            //ViewBag.HasAccess = HasAccess;
            if(ViewBag.HasAccess == true)
            {
                var model = ToolsSetingBAL.GetSuperUserAccessByID(UserID, ParlourId).FirstOrDefault(x => x.fkiSecureGroupID == 12); //4            
                                                                                                                                    //var model = ToolsSetingBAL.GetSuperUserAccessByID(UserID, ParlourId).FirstOrDefault(x => x.fkiSecureGroupID == 4);

                if (model != null)
                {
                    //IF IsSuperUserAccess is true, means load CompanyList as well as CompanyAddEdit
                    ViewBag.IsSuperUserAccess = true;
                }
                else
                {
                    //IF IsSuperUserAccess is true, means load CompanyAddEdit
                    ViewBag.IsSuperUserAccess = false;
                }
            }
            return View();
        }

        [PageRightsAttribute(CurrentPageId = 16)]
        public PartialViewResult List()
        {
            ViewBag.HasEditRight = HasEditRight;
            ViewBag.HasDeleteRight = HasDeleteRight;

            var pageCountEntries = GetEntriesCount();
            ViewBag.EntriesCount = pageCountEntries;

            Model.Search.CompanySearch search = new Model.Search.CompanySearch();
            search.PageNum = 1;
            search.PageSize = 10;
            search.SarchText = string.Empty;
            search.SortBy = "";
            search.SortOrder = "Asc";
            search.TotalRecord = 0;

            var searchResult = new SearchResult<Model.Search.CompanySearch, ApplicationSettingsModel>(search, new List<ApplicationSettingsModel>(), o => o.ApplicationName.Contains(search.SarchText));

            return PartialView("~/Areas/Tools/Views/CompanySetup/_CompanySetupList.cshtml", search);
        }
        public ActionResult SearchData(Model.Search.CompanySearch search)
        {
            var searchResult = new SearchResult<Model.Search.CompanySearch, ApplicationSettingsModel>(search, new List<ApplicationSettingsModel>(), o => o.ApplicationName.Contains(search.SarchText));

            try
            {
                var companySetups = ToolsSetingBAL.GetAllCompanys(new Guid("7BB52E7C-35BE-459C-A6DB-04A1D9D09EEE"), search.PageSize, search.PageNum, search.SarchText, search.SortBy, search.SortOrder);

                return Json(new SearchResult<Model.Search.CompanySearch, ApplicationSettingsModel>(search, companySetups, o => o.ApplicationName.Contains(search.SarchText)));
            }
            catch (Exception ex)
            {
                return Json(WebApiResult<Model.Search.CompanySearch, ApplicationSettingsModel>.Error(searchResult, ex));
            }
        }

        [PageRightsAttribute(CurrentPageId = 16, Right = new isPageRight[] { isPageRight.HasAdd })]
        public PartialViewResult Add(ApplicationSettingsModel companySetup)
        {
            companySetup = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);

            companySetup.parlourid = ParlourId;

            var companySMSTemplates = ToolsSetingBAL.EditsmsGroupbyID(companySetup.pkiApplicationID);
            companySetup.SMSSettings = BindSmsSendingSettings();
            foreach (var smsTemplate in companySetup.SMSSettings.ToList())
            {
                var template = companySMSTemplates.FirstOrDefault(companySetupSMSTemplate => smsTemplate.ID == companySetupSMSTemplate.fkismstempletID);
                if (template != null)
                {
                    smsTemplate.IsSelected = true;
                }
            }

            companySetup.BankDetails = ToolsSetingBAL.GetBankingByID(companySetup.parlourid);
            companySetup.TermAndConditions = ToolsSetingBAL.SelectApplicationTermsAndCondition(companySetup.parlourid);

            ModelState.Clear();

            return PartialView("~/Areas/Tools/Views/CompanySetup/_CompanySetupAddEdit.cshtml", companySetup);
        }

        [PageRightsAttribute(CurrentPageId = 16, Right = new isPageRight[] { isPageRight.HasEdit })]
        public PartialViewResult Edit(int applicationId = 0)
        {
            ApplicationSettingsModel companySetup = new ApplicationSettingsModel();
            var smsSettings = BindSmsSendingSettings();

            if (applicationId != 0)
            {
                companySetup = ToolsSetingBAL.GetApplictionByID(applicationId);

                BankingDetailSending applicationSettingBank = new BankingDetailSending();
                applicationSettingBank = ToolsSetingBAL.GetBankingByID(companySetup.parlourid);
                companySetup.BankDetails = applicationSettingBank;

                ApplicationTnCModel applicationSettingTermsAndConditions = new ApplicationTnCModel();
                applicationSettingTermsAndConditions = ToolsSetingBAL.SelectApplicationTermsAndCondition(companySetup.parlourid);
                companySetup.TermAndConditions = applicationSettingTermsAndConditions;

                var companySMSTemplates = ToolsSetingBAL.EditsmsGroupbyID(companySetup.pkiApplicationID);
                foreach (var smsTemplate in smsSettings.ToList())
                {
                    var template = companySMSTemplates.FirstOrDefault(companySetupSMSTemplate => smsTemplate.ID == companySetupSMSTemplate.fkismstempletID);
                    if (template != null)
                    {
                        smsTemplate.IsSelected = true;
                    }
                }
            }

            companySetup.SMSSettings = smsSettings;

            return PartialView("~/Areas/Tools/Views/CompanySetup/_CompanySetupAddEdit.cshtml", companySetup);
        }
        public ActionResult Update(int applicationId)
        {
            ViewBag.ApplicationId = applicationId;
            return View("Index");
        }
        [HttpPost]
        public ActionResult Save([System.Web.Http.FromBody]ApplicationSettingsModel applicationSetting)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (applicationSetting.pkiApplicationID > 0)
                    {
                        applicationSetting.parlourid = new Guid("00000000-0000-0000-0000-000000000000");
                    }
                    else
                    {
                        applicationSetting.parlourid = Guid.NewGuid();
                    }

                    applicationSetting.Currentparlourid = ParlourId;
                    var application = ToolsSetingBAL.SaveApplication(applicationSetting);
                    Guid retID = application.parlourid;

                    if (applicationSetting.ApplicationLogo?.Length > 0 && !string.IsNullOrEmpty(applicationSetting.ApplicationLogoPath))
                        SaveImage(applicationSetting.pkiApplicationID, applicationSetting.ApplicationLogo, applicationSetting.ApplicationLogoPath);

                    SaveAdditionalApplicationSettings(applicationSetting.parlourid, applicationSetting.IsAutoGeneratedPolicyNo);

                    if (applicationSetting.SMSSettings != null)
                        SaveSMSSendingGroupModel(applicationSetting.pkiApplicationID, applicationSetting.SMSSettings);

                    if (applicationSetting.BankDetails != null)
                        SaveBankDetails(retID, applicationSetting.BankDetails);


                    if (!string.IsNullOrEmpty(applicationSetting.TermAndConditions.TermsAndCondition))
                        SaveTermsAndCondition(retID, applicationSetting.pkiApplicationID, applicationSetting.TermAndConditions);

                    TempData["IsCompanySetupSaved"] = true;
                    TempData.Keep("IsCompanySetupSaved");

                    return RedirectToAction("Index", "CompanySetup", new { area = "Tools" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            TempData["IsAgentInfoSetupSaved"] = false;
            TempData.Keep("IsAgentInfoSetupSaved");

            return RedirectToAction("Update", "CompanySetup", new { area = "Tools", applicationId = applicationSetting.pkiApplicationID });
        }
        private void SaveAdditionalApplicationSettings(Guid parlourId, bool policyNumber)
        {

            AdditionalApplicationSettingsModel Adsmodel = new AdditionalApplicationSettingsModel();

            Adsmodel.pkiParlourid = parlourId;
            Adsmodel.spUPuser = "";
            Adsmodel.spUPpass = "";
            Adsmodel.spUPpinpad = "";
            Adsmodel.spValuser = "";
            Adsmodel.spValpass = "";
            Adsmodel.spValpinpad = "";
            Adsmodel.spCCuser = "";
            Adsmodel.spCCpass = "";
            Adsmodel.spCCpinpad = "";

            string membernumber = "no";
            if (policyNumber)
                membernumber = "yes";

            Adsmodel.GenerateMember = membernumber;
            Adsmodel = ToolsSetingBAL.SaveAdditionalApplication(Adsmodel);
        }
        private void SaveSMSSendingGroupModel(int applicationId, List<smsTempletModel> smsSettings)
        {

            int sguserID = 0;

            foreach (var smsSetting in smsSettings)
            {
                if (smsSetting.IsSelected)
                {
                    smsSendingGroupModel smsSendingGroup = new smsSendingGroupModel();

                    smsSendingGroup.ID = sguserID;
                    smsSendingGroup.fkiCompanyID = applicationId;
                    smsSendingGroup.fkismstempletID = smsSetting.ID;
                    smsSendingGroup.smstempletName = smsSetting.Name;
                    smsSendingGroup.LastModified = System.DateTime.Now;
                    smsSendingGroup.ModifiedUser = UserName;
                    sguserID = ToolsSetingBAL.SaveSmsGroupDetails(smsSendingGroup);
                }
            }
        }
        private void SaveBankDetails(Guid parlourId, BankingDetailSending bankDetails)
        {

            BankingDetailSending Model = new BankingDetailSending();

            Model.AccountHolder = bankDetails.AccountHolder;
            Model.Bankname = bankDetails.Bankname;
            Model.AccountNumber = bankDetails.AccountNumber;
            Model.Accounttype = bankDetails.Accounttype;
            Model.Branch = bankDetails.Branch;
            Model.Branchcode = bankDetails.Branchcode;
            Model.LastModified = System.DateTime.Now;
            Model.ModifiedUser = UserName;
            Model.parlourid = parlourId;

            Model = ToolsSetingBAL.SaveBankingDetail(Model);
        }
        private void SaveTermsAndCondition(Guid parlourId, int applicationId, ApplicationTnCModel termsAndCondition)
        {

            ApplicationTnCModel applicationTermsAndCondition = new ApplicationTnCModel();

            applicationTermsAndCondition.pkiAppTC = applicationId;
            applicationTermsAndCondition.fkiApplicationID = applicationId;
            applicationTermsAndCondition.TermsAndCondition = termsAndCondition.TermsAndCondition;
            applicationTermsAndCondition.LastModified = DateTime.Now;
            applicationTermsAndCondition.ModifiedUser = UserName;
            applicationTermsAndCondition.parlourid = parlourId;
            applicationTermsAndCondition.TermsAndConditionFuneral = termsAndCondition.TermsAndConditionFuneral;
            applicationTermsAndCondition.TermsAndConditionTombstone = termsAndCondition.TermsAndConditionTombstone;
            applicationTermsAndCondition.Declaration = termsAndCondition.Declaration;

            ToolsSetingBAL.SaveTermsAndCondition(applicationTermsAndCondition);
        }
        #endregion

        #region File Upload
        public ActionResult UploadFiles()
        {
            string fileUrl = "";
            _fileUploadResult = new FileUploadModel();
            HttpFileCollectionBase files = Request.Files;

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];

                using (Stream fs = file.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        System.Drawing.Image Img = System.Drawing.Image.FromStream(file.InputStream);

                        int H = 100;
                        int W = 100;

                        double ratioX = (double)Img.Height / (double)H;
                        double ratioY = (double)Img.Width / (double)W;
                        double ratio = ratioX < ratioY ? ratioX : ratioY;

                        int newH = Convert.ToInt32(H * ratio);
                        int newW = Convert.ToInt32(W * ratio);

                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        fileUrl = "data:image/png;base64," + Convert.ToBase64String(createthumbnail(bytes, newH, newW), 0, (createthumbnail(bytes, newH, newW).Length));

                        _fileUploadResult.ThumbnailData = fileUrl;
                        _fileUploadResult.FileName = file.FileName;
                    }
                }

            }
            return Json(_fileUploadResult, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteFile(string fileName)
        {
            var message = "Success";
            _fileUploadResult = new FileUploadModel();

            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public static byte[] createthumbnail(byte[] image, int thumbheight, int thumbwidth)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (System.Drawing.Image thumbnail = System.Drawing.Image.FromStream(new MemoryStream(image)).GetThumbnailImage(thumbheight, thumbwidth, null, new IntPtr()))
                {
                    thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
        public void SaveImage(int applicationId, byte[] applicationLogo, string applicationLogoPath)
        {
            ApplicationSettingsModel model = new ApplicationSettingsModel();
            model.pkiApplicationID = applicationId;
            model.ApplicationLogo = applicationLogo;
            model.ApplicationLogoPath = applicationLogoPath;

            ToolsSetingBAL.UploadApplicationLogo(model);
        }
        #endregion

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
        public List<smsTempletModel> BindSmsSendingSettings()
        {
            List<smsTempletModel> smsTemplates = new List<smsTempletModel>();
            smsTemplates = ToolsSetingBAL.GetTemplateList(ParlourId);
            return smsTemplates;
        }
    }
}