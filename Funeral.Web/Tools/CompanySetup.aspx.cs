using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.BAL;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace Funeral.Web.Tools
{
    public partial class CompanySetup : AdminBasePage
    {

        #region Page Property
        public int ApplicationID
        {
            get
            {
                if (ViewState["_ApplicationID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_ApplicationID"]);
            }
            set
            {
                ViewState["_ApplicationID"] = value;
            }
        }
        public Guid CompanyParlourID
        {
            get
            {
                if (ViewState["_CompanyParlourID"] == null)
                    return new Guid("00000000-0000-0000-0000-000000000000");
                else
                    return new Guid(ViewState["_CompanyParlourID"].ToString());
            }
            set
            {
                ViewState["_CompanyParlourID"] = value;
            }
        }
        public int PageSize
        {
            get
            {
                if (ViewState["_PageSize"] == null)
                    return 10;
                else { return Convert.ToInt32(ViewState["_PageSize"].ToString()); }

            }
            set { ViewState["_PageSize"] = value; }

        }
        public int PageNum
        {
            get
            {
                if (ViewState["_PageNum"] == null)
                    return 1;
                else { return Convert.ToInt32(ViewState["_PageNum"].ToString()); }
            }
            set { ViewState["_PageNum"] = value; }

        }
        public Int64 TotalRecord
        {
            get
            {
                if (ViewState["_TotalRecord"] == null)
                    return 0;
                else { return Convert.ToInt32(ViewState["_TotalRecord"].ToString()); }
            }
            set { ViewState["_TotalRecord"] = value; }

        }
        public string sortBYExpression
        {
            get
            {
                object viewState = this.ViewState["sortBYExpression"];

                return (viewState == null) ? "pkiApplicationID" : (string)viewState;
            }
            set { this.ViewState["sortBYExpression"] = value; }
        }
        public string sortType
        {
            get
            {
                object viewState = this.ViewState["sortType"];

                return (viewState == null) ? "ASC" : (string)viewState;
            }
            set { this.ViewState["sortType"] = value; }
        }

        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 16;
        }
        #endregion
        //#region PageInit
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    SecureUserGroupsModel[] obj = client.EditSecurUserbyID(UserID);
        //    List<int> list = new List<int>();
        //    list.Add(4);
        //    list.Add(12);
        //    var result = obj.Where(x => list.Contains(x.fkiSecureGroupID));
        //    if (result.FirstOrDefault() == null)
        //    {
        //        Response.Redirect("~/Admin/403Error.aspx", false);
        //    }
        //}
        //#endregion

        #region Page load event
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

            if (!Page.IsPostBack)
            {
                BindsmsGroupList();
                var model = ToolsSetingBAL.GetSuperUserAccessByID(UserID, ParlourId).FirstOrDefault(x => x.fkiSecureGroupID == 12); ;
                if (model != null)
                    BindAllApplicationDetails();
                else
                {
                    BindApplicationbyParlour();
                    foreach (ListItem lst in chksmsGroup.Items)
                    {
                        if (Convert.ToInt32(lst.Value) == 3 || Convert.ToInt32(lst.Value) == 4)
                            lst.Enabled = false;
                    }
                }
                ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(PageSize.ToString()));
            }
        }

        #endregion

        #region Page size change event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindAllApplicationDetails();
        }
        #endregion

        #region Private/Public function and methods

        /// <summary>
        /// Get All Companys from database and bind here.
        /// </summary>
        public void BindAllApplicationDetails()
        {
            gvCompany.PageSize = PageSize;
            List<ApplicationSettingsModel> model = ToolsSetingBAL.GetAllCompanys(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
            gvCompany.DataSource = model;
            gvCompany.DataBind();
        }
        public void BindsmsGroupList()
        {
            List<smsTempletModel> model = ToolsSetingBAL.GetTemplateList(ParlourId);
            if (model != null)
            {
                chksmsGroup.DataSource = model;
                //chkSecurityGroup.DataTextField = "sSecureGroupName";
                //chkSecurityGroup.DataValueField = "pkiSecureGroupID";
                chksmsGroup.DataBind();
            }
        }
        public void BindApplicationToUpdate()
        {
            ApplicationSettingsModel modelCompany = new ApplicationSettingsModel();
            modelCompany = ToolsSetingBAL.GetApplictionByID(ApplicationID);
            if (modelCompany != null)
            {
                ApplicationID = modelCompany.pkiApplicationID;
                txtCompanyName.Text = modelCompany.ApplicationName;
                txtownFirstName.Text = modelCompany.OwnerFirstName;
                txtownLastName.Text = modelCompany.OwnerSurname;
                txtownTelephoneNumber.Text = modelCompany.OwnerTelNumber;
                txtOwnersCellphone.Text = modelCompany.OwnerCellNumber;
                txtTelePhone.Text = modelCompany.ManageTelNumber;
                txtCellphone.Text = modelCompany.ManageCellNumber;
                txtline1.Text = modelCompany.BusinessAddressLine1;
                txtline2.Text = modelCompany.BusinessAddressLine2;
                txtline3.Text = modelCompany.BusinessAddressLine3;
                txtline4.Text = modelCompany.BusinessAddressLine4;
                txtpostalcode.Text = modelCompany.BusinessPostalCode;
                txtFsbNumber.Text = modelCompany.FSBNumber;
                txtRegistrationNumber.Text = modelCompany.RegistrationNumber;
                txtCompanySlogan.Text = modelCompany.ManageSlogan;
                txtEmail.Text = modelCompany.ManageEmail;
                txtFaxNumber.Text = modelCompany.ManageFaxNumber;
                txtOwnersEmail.Text = modelCompany.OwnerEmail;
                txtcompanyRules.Text = modelCompany.ApplicationRules;
                txtVatNo.Text = modelCompany.VatNo;
                txtEmailForClaimNotification.Text = modelCompany.EmailForClaimNotification;


                if (modelCompany.ApplicationLogo != null)
                {
                    string base64String = Convert.ToBase64String(modelCompany.ApplicationLogo, 0, modelCompany.ApplicationLogo.Length);
                    ImagePreview.ImageUrl = "data:image/png;base64," + base64String;
                }
                else
                {
                    ImagePreview.ImageUrl = string.Empty;
                }
                Boolean AutoGeneratePolicyNo = modelCompany.IsAutoGeneratedPolicyNo;
                if (AutoGeneratePolicyNo == false)
                {
                    cbAutoGeneratePolicy.Checked = false;
                }
                else if (AutoGeneratePolicyNo == true)
                {
                    cbAutoGeneratePolicy.Checked = true;
                }

                //foreach (ListItem lst in chksmsGroup.Items)
                //{
                //    lst.Selected = false;
                //}
                List<smsSendingGroupModel> modelS = ToolsSetingBAL.EditsmsGroupbyID(ApplicationID);
                foreach (smsSendingGroupModel lstModel in modelS)
                {

                    foreach (ListItem lst in chksmsGroup.Items)
                    {
                        if (Convert.ToInt32(lst.Value) == lstModel.fkismstempletID)
                            lst.Selected = true;
                    }
                }


                //--------------Additinal ApplicationSettings
                AdditionalApplicationSettingsModel additionalApplicationSettingsModel;
                additionalApplicationSettingsModel = ToolsSetingBAL.GetAdditionalApplicationSettings(ApplicationID);
                if (additionalApplicationSettingsModel != null)
                {
                    if (additionalApplicationSettingsModel.spNetcashAccountNumberMessage == "AccountStatus, Validated")
                    {
                        spNetcashAccountNumberMessage.Text = additionalApplicationSettingsModel.spNetcashAccountNumberMessage;
                        spNetcashAccountNumberMessage.Style.Add("color", "GREEN");
                    }
                    else
                    {
                        spNetcashAccountNumberMessage.Text = additionalApplicationSettingsModel.spNetcashAccountNumberMessage;
                        spNetcashAccountNumberMessage.Style.Add("color", "RED");
                    }

                    if (additionalApplicationSettingsModel.spDebitOrderServikeyMessage == "Debit orders Service Key, Validated")
                    {
                        spDebitOrderServikeyMessage.Text = additionalApplicationSettingsModel.spDebitOrderServikeyMessage;
                        spDebitOrderServikeyMessage.Style.Add("color", "GREEN");
                    }
                    else
                    {
                        spDebitOrderServikeyMessage.Text = additionalApplicationSettingsModel.spDebitOrderServikeyMessage;
                        spDebitOrderServikeyMessage.Style.Add("color", "RED");
                    }

                    if (additionalApplicationSettingsModel.spPaynowServiveKeyMessage == "Pay Now service Key, Validated")
                    {
                        spPaynowServiveKeyMessage.Text = additionalApplicationSettingsModel.spPaynowServiveKeyMessage;
                        spPaynowServiveKeyMessage.Style.Add("color", "GREEN");
                    }
                    else
                    {
                        spPaynowServiveKeyMessage.Text = additionalApplicationSettingsModel.spPaynowServiveKeyMessage;
                        spPaynowServiveKeyMessage.Style.Add("color", "RED");
                    }

                    if (additionalApplicationSettingsModel.spAccountServiceKeyMessage == "Account service Key, Validated")
                    {
                        spAccountServiceKeyMessage.Text = additionalApplicationSettingsModel.spAccountServiceKeyMessage;
                        spAccountServiceKeyMessage.Style.Add("color", "GREEN");
                    }
                    else
                    {
                        spAccountServiceKeyMessage.Text = additionalApplicationSettingsModel.spAccountServiceKeyMessage;
                        spAccountServiceKeyMessage.Style.Add("color", "RED");
                    }


                    //if (additionalApplicationSettingsModel.spSoftwareVendorKeyMessage == "Account service Key, Validated")
                    //{
                    //    spAccountServiceKeyMessage.Text = additionalApplicationSettingsModel.spAccountServiceKeyMessage;
                    //    spAccountServiceKeyMessage.Style.Add("color", "GREEN");
                    //}
                    //else
                    //{
                    //    spAccountServiceKeyMessage.Text = additionalApplicationSettingsModel.spAccountServiceKeyMessage;
                    //    spAccountServiceKeyMessage.Style.Add("color", "RED");
                    //}


                    //spDebitOrderServikeyMessage.Text = additionalApplicationSettingsModel.spDebitOrderServikeyMessage;
                    //spPaynowServiveKeyMessage.Text = additionalApplicationSettingsModel.spPaynowServiveKeyMessage;
                    //spAccountServiceKeyMessage.Text = additionalApplicationSettingsModel.spAccountServiceKeyMessage;

                    txtNetcashAccountNumber.Text = additionalApplicationSettingsModel.spNetcashAccountNumber;
                    txtDebitOrderServicekey.Text = additionalApplicationSettingsModel.spDebitOrderServicekey;
                    txtPaynowServiveKey.Text = additionalApplicationSettingsModel.spPaynowServiveKey;
                    txtAccountServiceKey.Text = additionalApplicationSettingsModel.spAccountServiceKey;
                    txtLastModifiedMessage.Text = additionalApplicationSettingsModel.LastModified.ToString();
                    txtModifiedUserMessage.Text = additionalApplicationSettingsModel.ModifiedUser;
                    txtCurrency.Text = additionalApplicationSettingsModel.Currency;
                    txtSoftwareVendorKey.Text = additionalApplicationSettingsModel.spSoftwareVendorKey;

                }
                //-----------------END-----------------------

                BankingDetailSending Modelbank;
                Modelbank = ToolsSetingBAL.GetBankingByID(modelCompany.parlourid);
                if (Modelbank != null)
                {
                    txtaccountholder.Text = Modelbank.AccountHolder;
                    txtbankname.Text = Modelbank.Bankname;
                    txtaccountnumber.Text = Modelbank.AccountNumber;
                    txtaccounttype.Text = Modelbank.Accounttype;
                    txtbranch.Text = Modelbank.Branch;
                    txtbranchcode.Text = Modelbank.Branchcode;

                    txtFuneralAccountholder.Text = Modelbank.Funeral_AccountHolder;
                    txtFuneralBankname.Text = Modelbank.Funeral_Bankname;
                    txtFuneralAccountNumber.Text = Modelbank.Funeral_AccountNumber;
                    txtFuneralAccountType.Text = Modelbank.Funeral_Accounttype;
                    txtFuneralBranch.Text = Modelbank.Funeral_Branch;
                    txtFuneralBranchCode.Text = Modelbank.Funeral_Branchcode;
                }
                ApplicationTnCModel ModelTnc;
                ModelTnc = ToolsSetingBAL.SelectApplicationTermsAndCondition(modelCompany.parlourid);
                if (ModelTnc != null)
                {
                    txtTnC.Text = ModelTnc.TermsAndCondition;
                    txtTncFuneral.Text = ModelTnc.TermsAndConditionFuneral;
                    txtTncTombstone.Text = ModelTnc.TermsAndConditionTombstone;
                    txtPolicyDeclaration.Text = ModelTnc.Declaration;
                    txtTncQuotation.Text = ModelTnc.QuotationTermsAndCondition;
                }

                btnSubmite.Text = "Update";
                btnUpload.Enabled = true;
            }
        }
        public void BindApplicationbyParlour()
        {
            PaymentHistory.Visible = false;
            ApplicationSettingsModel model;
            model = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            if (model != null)
            {
                ApplicationID = model.pkiApplicationID;
                txtCompanyName.Text = model.ApplicationName;
                txtownFirstName.Text = model.OwnerFirstName;
                txtownLastName.Text = model.OwnerSurname;
                txtownTelephoneNumber.Text = model.OwnerTelNumber;
                txtOwnersCellphone.Text = model.OwnerCellNumber;
                txtTelePhone.Text = model.ManageTelNumber;
                txtCellphone.Text = model.ManageCellNumber;
                txtline1.Text = model.BusinessAddressLine1;
                txtline2.Text = model.BusinessAddressLine2;
                txtline3.Text = model.BusinessAddressLine3;
                txtline4.Text = model.BusinessAddressLine4;
                txtpostalcode.Text = model.BusinessPostalCode;
                txtFsbNumber.Text = model.FSBNumber;
                txtRegistrationNumber.Text = model.RegistrationNumber;
                txtCompanySlogan.Text = model.ManageSlogan;
                txtEmail.Text = model.ManageEmail;
                txtFaxNumber.Text = model.ManageFaxNumber;
                txtOwnersEmail.Text = model.OwnerEmail;
                txtcompanyRules.Text = model.ApplicationRules;
                txtVatNo.Text = model.VatNo;
                txtEmailForClaimNotification.Text = model.EmailForClaimNotification;


                if (model.ApplicationLogo != null)
                {
                    string base64String = Convert.ToBase64String(model.ApplicationLogo, 0, model.ApplicationLogo.Length);
                    ImagePreview.ImageUrl = "data:image/png;base64," + base64String;
                }
                else
                {
                    ImagePreview.ImageUrl = string.Empty;
                }


                List<smsSendingGroupModel> modelS = ToolsSetingBAL.EditsmsGroupbyID(ApplicationID);
                foreach (smsSendingGroupModel lstModel in modelS)
                {

                    foreach (ListItem lst in chksmsGroup.Items)
                    {
                        if (Convert.ToInt32(lst.Value) == lstModel.fkismstempletID)
                            lst.Selected = true;
                    }
                }

                AdditionalApplicationSettingsModel additionalApplicationSettingsModel;
                additionalApplicationSettingsModel = ToolsSetingBAL.GetAdditionalApplicationSettings(ApplicationID);
                if (additionalApplicationSettingsModel != null)
                {
                    if (additionalApplicationSettingsModel.spNetcashAccountNumberMessage == "AccountStatus, Validated")
                    {
                        spNetcashAccountNumberMessage.Text = additionalApplicationSettingsModel.spNetcashAccountNumberMessage;
                        spNetcashAccountNumberMessage.Style.Add("color", "GREEN");
                    }
                    else
                    {
                        spNetcashAccountNumberMessage.Text = additionalApplicationSettingsModel.spNetcashAccountNumberMessage;
                        spNetcashAccountNumberMessage.Style.Add("color", "RED");
                    }

                    if (additionalApplicationSettingsModel.spDebitOrderServikeyMessage == "Debit orders Service Key, Validated")
                    {
                        spDebitOrderServikeyMessage.Text = additionalApplicationSettingsModel.spDebitOrderServikeyMessage;
                        spDebitOrderServikeyMessage.Style.Add("color", "GREEN");
                    }
                    else
                    {
                        spDebitOrderServikeyMessage.Text = additionalApplicationSettingsModel.spDebitOrderServikeyMessage;
                        spDebitOrderServikeyMessage.Style.Add("color", "RED");
                    }

                    if (additionalApplicationSettingsModel.spPaynowServiveKeyMessage == "Pay Now service Key, Validated")
                    {
                        spPaynowServiveKeyMessage.Text = additionalApplicationSettingsModel.spPaynowServiveKeyMessage;
                        spPaynowServiveKeyMessage.Style.Add("color", "GREEN");
                    }
                    else
                    {
                        spPaynowServiveKeyMessage.Text = additionalApplicationSettingsModel.spPaynowServiveKeyMessage;
                        spPaynowServiveKeyMessage.Style.Add("color", "RED");
                    }

                    if (additionalApplicationSettingsModel.spAccountServiceKeyMessage == "Account service Key, Validated")
                    {
                        spAccountServiceKeyMessage.Text = additionalApplicationSettingsModel.spAccountServiceKeyMessage;
                        spAccountServiceKeyMessage.Style.Add("color", "GREEN");
                    }
                    else
                    {
                        spAccountServiceKeyMessage.Text = additionalApplicationSettingsModel.spAccountServiceKeyMessage;
                        spAccountServiceKeyMessage.Style.Add("color", "RED");
                    }


                    //if (additionalApplicationSettingsModel.spSoftwareVendorKeyMessage == "Account service Key, Validated")
                    //{
                    //    spAccountServiceKeyMessage.Text = additionalApplicationSettingsModel.spAccountServiceKeyMessage;
                    //    spAccountServiceKeyMessage.Style.Add("color", "GREEN");
                    //}
                    //else
                    //{
                    //    spAccountServiceKeyMessage.Text = additionalApplicationSettingsModel.spAccountServiceKeyMessage;
                    //    spAccountServiceKeyMessage.Style.Add("color", "RED");
                    //}


                    //spDebitOrderServikeyMessage.Text = additionalApplicationSettingsModel.spDebitOrderServikeyMessage;
                    //spPaynowServiveKeyMessage.Text = additionalApplicationSettingsModel.spPaynowServiveKeyMessage;
                    //spAccountServiceKeyMessage.Text = additionalApplicationSettingsModel.spAccountServiceKeyMessage;

                    txtNetcashAccountNumber.Text = additionalApplicationSettingsModel.spNetcashAccountNumber;
                    txtDebitOrderServicekey.Text = additionalApplicationSettingsModel.spDebitOrderServicekey;
                    txtPaynowServiveKey.Text = additionalApplicationSettingsModel.spPaynowServiveKey;
                    txtAccountServiceKey.Text = additionalApplicationSettingsModel.spAccountServiceKey;
                    txtLastModifiedMessage.Text = additionalApplicationSettingsModel.LastModified.ToString();
                    txtModifiedUserMessage.Text = additionalApplicationSettingsModel.ModifiedUser;
                    txtCurrency.Text = additionalApplicationSettingsModel.Currency;
                    txtSoftwareVendorKey.Text = additionalApplicationSettingsModel.spSoftwareVendorKey;

                }


                BankingDetailSending Modelbank;
                Modelbank = ToolsSetingBAL.GetBankingByID(model.parlourid);
                if (Modelbank != null)
                {
                    txtaccountholder.Text = Modelbank.AccountHolder;
                    txtbankname.Text = Modelbank.Bankname;
                    txtaccountnumber.Text = Modelbank.AccountNumber;
                    txtaccounttype.Text = Modelbank.Accounttype;
                    txtbranch.Text = Modelbank.Branch;
                    txtbranchcode.Text = Modelbank.Branchcode;

                    txtFuneralAccountholder.Text = Modelbank.Funeral_AccountHolder;
                    txtFuneralBankname.Text = Modelbank.Funeral_Bankname;
                    txtFuneralAccountNumber.Text = Modelbank.Funeral_AccountNumber;
                    txtFuneralAccountType.Text = Modelbank.Funeral_Accounttype;
                    txtFuneralBranch.Text = Modelbank.Funeral_Branch;
                    txtFuneralBranchCode.Text = Modelbank.Funeral_Branchcode;

                }

                ApplicationTnCModel ModelTnc;
                ModelTnc = ToolsSetingBAL.SelectApplicationTermsAndCondition(model.parlourid);
                if (ModelTnc != null)
                {
                    txtTnC.Text = ModelTnc.TermsAndCondition;
                    txtTncFuneral.Text = ModelTnc.TermsAndConditionFuneral;
                    txtTncTombstone.Text = ModelTnc.TermsAndConditionTombstone;
                    txtPolicyDeclaration.Text = ModelTnc.Declaration;
                    txtTncQuotation.Text = ModelTnc.QuotationTermsAndCondition;
                }
                //=============IsAutoGeneratedPolicyNumber
                Boolean AutoGeneratePolicyNo = model.IsAutoGeneratedPolicyNo;
                if (AutoGeneratePolicyNo == null || AutoGeneratePolicyNo == false)
                { cbAutoGeneratePolicy.Checked = false; }
                else if (AutoGeneratePolicyNo == true)
                { cbAutoGeneratePolicy.Checked = true; }
                //===============


                //+++++++++++++++++++++++IsAutoGenerateEasyPayNumber
                Boolean IsAutoGenerate = additionalApplicationSettingsModel.GenerateEasyPay;
                if (IsAutoGenerate == null || IsAutoGenerate == false)
                {
                    cbAutoGernerateEasyPayNo.Checked = false;
                }
                else if (IsAutoGenerate == true)
                {
                    cbAutoGernerateEasyPayNo.Checked = true;
                }

                //+++++++++++++++++++++++

                btnSubmite.Text = "Update";
                btnUpload.Enabled = true;
            }
        }
        public void ClearControl()
        {
            btnSubmite.Text = "Save";
            btnUpload.Enabled = false;
            ApplicationID = 0;
            txtCompanyName.Text = string.Empty;
            txtownFirstName.Text = string.Empty;
            txtownLastName.Text = string.Empty;
            txtownTelephoneNumber.Text = string.Empty;
            txtOwnersCellphone.Text = string.Empty;
            txtTelePhone.Text = string.Empty;
            txtCellphone.Text = string.Empty;
            txtline1.Text = string.Empty;
            txtline2.Text = string.Empty;
            txtline3.Text = string.Empty;
            txtline4.Text = string.Empty;
            txtpostalcode.Text = string.Empty;
            txtFsbNumber.Text = string.Empty;
            txtRegistrationNumber.Text = string.Empty;
            txtCompanySlogan.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtFaxNumber.Text = string.Empty;
            txtOwnersEmail.Text = string.Empty;
            lblMessage.Visible = false;
            ImagePreview.ImageUrl = string.Empty;
            txtcompanyRules.Text = string.Empty;

            foreach (ListItem lst in chksmsGroup.Items)
            {
                lst.Selected = false;
            }

            txtaccountholder.Text = string.Empty;
            txtbankname.Text = string.Empty;
            txtaccountnumber.Text = string.Empty;
            txtaccounttype.Text = string.Empty;
            txtbranch.Text = string.Empty;
            txtbranchcode.Text = string.Empty;
            cbAutoGeneratePolicy.Checked = false;
            txtTnC.Text = string.Empty;
            txtTncFuneral.Text = string.Empty;
            txtTncTombstone.Text = string.Empty;
            txtVatNo.Text = string.Empty;
            txtPolicyDeclaration.Text = string.Empty;
            txtFuneralAccountholder.Text = string.Empty;
            txtFuneralBankname.Text = string.Empty;
            txtFuneralAccountNumber.Text = string.Empty;
            txtFuneralAccountType.Text = string.Empty;
            txtFuneralBranch.Text = string.Empty;
            txtFuneralBranchCode.Text = string.Empty;
        }

        #endregion

        #region Button Click Events
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (PaymentHistory.Visible == false || ApplicationID > 0)
            {
                UploadImage(ApplicationID);

            }
            else
            {
                ShowMessage(ref lblMessage, MessageType.Danger, "Please select Company Details after upload image..");
                lblMessage.Visible = true;
            }
            //UploadImage(ApplicationID);
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (PaymentHistory.Visible == false || ApplicationID > 0)
            {
                RemoveImage(ApplicationID);
                ImagePreview.ImageUrl = string.Empty;

            }
            else
            {
                ShowMessage(ref lblMessage, MessageType.Danger, "Please select Company Details after upload image..");
                lblMessage.Visible = true;
            }
        }

        public void RemoveImage(int pkiApplicationID)
        {
            ApplicationID = ToolsSetingBAL.RemoveApplicationLogo(pkiApplicationID);
        }
        public void UploadImage(int PkiApplicationID)
        {
            if (fucompanyLogo.HasFile)
            {


                string filename = Path.GetFileName(fucompanyLogo.PostedFile.FileName);
                string contentType = fucompanyLogo.PostedFile.ContentType;
                using (Stream fs = fucompanyLogo.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        System.Drawing.Image Img = System.Drawing.Image.FromStream(fucompanyLogo.PostedFile.InputStream);

                        int H = 100;
                        int W = 100;



                        double ratioX = (double)Img.Height / (double)H;
                        double ratioY = (double)Img.Width / (double)W;
                        double ratio = ratioX < ratioY ? ratioX : ratioY;

                        int newH = Convert.ToInt32(H * ratio);
                        int newW = Convert.ToInt32(W * ratio);


                        ApplicationSettingsModel model = new ApplicationSettingsModel();
                        model.pkiApplicationID = PkiApplicationID;
                        model.ApplicationLogo = bytes;
                        model.ApplicationLogoPath = filename;

                        ApplicationID = ToolsSetingBAL.UploadApplicationLogo(model);

                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        ImagePreview.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(createthumbnail(bytes, newH, newW), 0, (createthumbnail(bytes, newH, newW).Length));
                    }
                }
            }
        }
        protected void btnSubmite_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                ApplicationSettingsModel model;
                //model = client.GetMemberByIDNum(txtCompanyName.Text, ParlourId);
                //if (model != null)
                //{
                //    ShowMessage(ref lblMessage, MessageType.Danger, "Member Already Exists.");
                //}
                //else
                //{
                model = new ApplicationSettingsModel();
                model.pkiApplicationID = ApplicationID;
                model.ApplicationName = txtCompanyName.Text;
                model.ApplicationLogoPath = string.Empty;
                model.OwnerFirstName = txtownFirstName.Text;
                model.OwnerSurname = txtownLastName.Text;
                model.OwnerTelNumber = txtownTelephoneNumber.Text;
                model.OwnerCellNumber = txtOwnersCellphone.Text;
                model.ManagerFirstName = string.Empty;
                model.ManageSurname = string.Empty;
                model.ManageTelNumber = txtTelePhone.Text;
                model.ManageCellNumber = txtCellphone.Text;
                model.BusinessAddressLine1 = txtline1.Text;
                model.BusinessAddressLine2 = txtline2.Text;
                model.BusinessAddressLine3 = txtline3.Text;
                model.BusinessAddressLine4 = txtline4.Text;
                model.BusinessPostalCode = txtpostalcode.Text;
                model.FSBNumber = txtFsbNumber.Text;
                model.CereliaAPIKey = string.Empty;
                model.RegistrationNumber = txtRegistrationNumber.Text;
                model.ManageSlogan = txtCompanySlogan.Text;
                model.ManageEmail = txtEmail.Text;
                model.ManageFaxNumber = txtFaxNumber.Text;
                model.OwnerEmail = txtOwnersEmail.Text;
                model.ApplicationRules = txtcompanyRules.Text;
                model.VatNo = txtVatNo.Text;
                model.EmailForClaimNotification = txtEmailForClaimNotification.Text;
                model.UserName = UserName;
                //===============================Allow Auto generate Policy Number===============
                Boolean PolicyNumber = false;
                if (cbAutoGeneratePolicy.Checked)
                {
                    PolicyNumber = true;
                    //CommonBAL.SaveAudit(UserName, ParlourId, "Activated Policy Number Auto Generate");
                }
                else
                {
                    PolicyNumber = false;
                    //CommonBAL.SaveAudit(UserName, ParlourId, "De-Activated Policy Number Auto Generate");
                }
                model.IsAutoGeneratedPolicyNo = PolicyNumber;

                //======================================

                //model.ApplicationLogo = txtOwnersEmail.Text;
                //model.ApplicationRules = txtOwnersEmail.Text;


                if (ApplicationID > 0)
                    model.parlourid = new Guid("00000000-0000-0000-0000-000000000000");
                else
                    model.parlourid = Guid.NewGuid();
                //=============
                model.Currentparlourid = ParlourId;
                //================================================================ 
                model = ToolsSetingBAL.SaveApplication(model);
                UploadImage(model.pkiApplicationID);
                Guid retID = model.parlourid;
                //=========================================================================
                //ApplicationAdditionalInsert
                AdditionalApplicationSettingsModel Adsmodel = new AdditionalApplicationSettingsModel();
                Adsmodel.pkiParlourid = model.parlourid;
                Adsmodel.spUPuser = "";
                Adsmodel.spUPpass = "";
                Adsmodel.spUPpinpad = "";
                Adsmodel.spValuser = "";
                Adsmodel.spValpass = "";
                Adsmodel.spValpinpad = "";
                Adsmodel.spCCuser = "";
                Adsmodel.spCCpass = "";
                Adsmodel.spCCpinpad = "";
                Adsmodel.spNetcashAccountNumber = txtNetcashAccountNumber.Text;
                Adsmodel.spAccountServiceKey = txtAccountServiceKey.Text;
                Adsmodel.spDebitOrderServicekey = txtDebitOrderServicekey.Text;
                Adsmodel.spPaynowServiveKey = txtPaynowServiveKey.Text;
                Adsmodel.LastModified = System.DateTime.Now;
                Adsmodel.ModifiedUser = UserName;
                Adsmodel.spSoftwareVendorKey = txtSoftwareVendorKey.Text;
                Adsmodel.Currency = txtCurrency.Text.TrimEnd().TrimStart();
                //Adsmodel.spSoftwareVendorKeyMessage = spSoftwareVendorMessage.Text;

                string membernumber = "no";
                if (PolicyNumber) { membernumber = "yes"; }
                Adsmodel.GenerateMember = membernumber;

                Boolean EasyPayNo = false;
                if (cbAutoGernerateEasyPayNo.Checked)
                {
                    EasyPayNo = true;
                }
                else
                {
                    EasyPayNo = false;
                }
                Adsmodel.GenerateEasyPay = EasyPayNo;

                Adsmodel = ToolsSetingBAL.SaveAdditionalApplication(Adsmodel);

                // ==========================[ SMS Group Insert Delete ]===================================
                int sguserID = 0;
                foreach (ListItem lst in chksmsGroup.Items)
                {
                    if (lst.Selected)
                    {
                        smsSendingGroupModel modelS = new smsSendingGroupModel();
                        modelS.ID = sguserID;
                        modelS.fkiCompanyID = model.pkiApplicationID;
                        modelS.fkismstempletID = Convert.ToInt32(lst.Value);
                        modelS.smstempletName = lst.Text;
                        modelS.LastModified = System.DateTime.Now;
                        modelS.ModifiedUser = UserName;
                        sguserID = ToolsSetingBAL.SaveSmsGroupDetails(modelS);
                    }
                }
                //   ==========================[END SMS Group Insert Delete ]===================================

                //   ==========================[Start Banking Detail Insert Delete ]===================================
                BankingDetailSending Model;

                Model = new BankingDetailSending();
                //Model.ID = ApplicationID;
                Model.AccountHolder = txtaccountholder.Text;
                Model.Bankname = txtbankname.Text;
                Model.AccountNumber = txtaccountnumber.Text;
                Model.Accounttype = txtaccounttype.Text;
                Model.Branch = txtbranch.Text;
                Model.Branchcode = txtbranchcode.Text;
                Model.LastModified = System.DateTime.Now;
                Model.ModifiedUser = UserName;
                Model.parlourid = retID;
                Model.Funeral_AccountHolder = txtFuneralAccountholder.Text;
                Model.Funeral_Bankname = txtFuneralBankname.Text;
                Model.Funeral_AccountNumber = txtFuneralAccountNumber.Text;
                Model.Funeral_Accounttype = txtFuneralAccountType.Text;
                Model.Funeral_Branch = txtFuneralBranch.Text;
                Model.Funeral_Branchcode = txtFuneralBranchCode.Text;


                Model = ToolsSetingBAL.SaveBankingDetail(Model);

                //   ==========================[END Banking Detail Insert Delete ]===================================
                //================================[Insert Update Terms & Condition]===============================
                ApplicationTnCModel objtc = new ApplicationTnCModel();
                objtc.pkiAppTC = ApplicationID;
                objtc.fkiApplicationID = ApplicationID;
                objtc.TermsAndCondition = txtTnC.Text;
                objtc.LastModified = DateTime.Now;
                objtc.ModifiedUser = UserName;
                objtc.parlourid = retID;
                objtc.TermsAndConditionFuneral = txtTncFuneral.Text;
                objtc.TermsAndConditionTombstone = txtTncTombstone.Text;
                objtc.QuotationTermsAndCondition = txtTncQuotation.Text;
                objtc.Declaration = txtPolicyDeclaration.Text;
                
                int a = ToolsSetingBAL.SaveTermsAndCondition(objtc);



                if (ApplicationID > 0)
                {
                    BindAllApplicationDetails();
                    if (PaymentHistory.Visible != false)
                    {
                        ClearControl();
                    }
                }
                else
                {
                    CompanyParlourID = retID;
                    Response.Redirect("~/Tools/UserSetup.aspx?CompanyParlourID=" + CompanyParlourID + "&NewId=1");
                }


                //bindEasyPayNumber();
                ShowMessage(ref lblMessage, MessageType.Success, "Company Details successfully saved");
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab512", "goToTab(5)", true);
            }

            BindApplicationbyParlour();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Admin/Default.aspx");
            if (PaymentHistory.Visible != false)
            {
                ClearControl();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindAllApplicationDetails();
        }

        #endregion

        #region gvCompanyList Properties
        protected void gvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompany.PageIndex = e.NewPageIndex;
            BindAllApplicationDetails();
        }

        protected void gvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ClearControl();
            if (e.CommandName == "EditCompany")
            {
                ApplicationID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    BindApplicationToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            if (e.CommandName == "deleteCompany")
            {
                int SApplicationID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    int retID = ToolsSetingBAL.DeleteCompany(SApplicationID);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    BindAllApplicationDetails();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
        }

        #region "Sorting Event"

        private const string ASCENDING = "ASC";
        private const string DESCENDING = "DESC";
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Ascending;
                }

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }
        private void SortGridView(string sortExpression, string direction)
        {
            sortBYExpression = sortExpression;
            sortType = direction;
        }

        protected void gvCompany_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
            BindAllApplicationDetails();
        }
        #endregion

        #endregion

        
    }
}