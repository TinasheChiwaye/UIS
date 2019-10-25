using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class NewRegistration : System.Web.UI.Page
    {
        #region Page Property
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();

        public enum MessageType
        {
            Success = 0,
            Info = 1,
            Warning = 2,
            Danger = 3
        }
        public enum SmsGroupType
        {
            Birthday = 1,
            Payment = 2,
            Outstanding = 3,
            sms21 = 4,
            Welcome = 5
        }
        public enum SecureGroup
        {
            Finance = 2,
            Administration = 3,
            SuperUsers = 4,
            DataCapturer = 5,
            Quotations = 6,
            ReversalPayments = 7,
            Payments = 8,
            FuneralCapture = 9,
            Claims = 10,
            CashUp = 11,
            Adminstrator = 12
        }
        protected void ShowMessage(ref Label lblMessage, MessageType objMessageType, string Message)
        {
            string strMessage = string.Empty;
            strMessage = "<div class=\"alert alert-" + objMessageType.ToString().ToLower() + "\">" + Message + "</div>";
            lblMessage.Text = strMessage;
        }

        public string UserName
        {
            get
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string[] strData = ticket.UserData.Split('|');
                    if (strData.Count() > 0)
                        return strData[2];
                    else
                        return string.Empty;
                    //return new Guid(string.IsNullOrEmpty(ticket.UserData) ? "00000000-0000-0000-0000-000000000000" : ticket.UserData);
                }
                else
                {
                    return string.Empty;
                    //return new Guid("00000000-0000-0000-0000-000000000000");
                }
            }

        }

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
        public int SecureUserId
        {
            get
            {
                if (ViewState["_UserID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_UserID"]);
            }
            set
            {
                ViewState["_UserID"] = value;
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
            //this.dbPageId = 16;
        }
       
        #region Page load event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
            
        }

        #endregion
        #region Private/Public function and methods                
        public void ClearControl()
        {
            btnSubmite.Text = "Save";

            ApplicationID = 0;
            txtCompanyName.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;          
            txtCellphone.Text = string.Empty;
            txtline1.Text = string.Empty;
            txtline2.Text = string.Empty;
            txtline3.Text = string.Empty;
            txtline4.Text = string.Empty;
            txtpostalcode.Text = string.Empty;
            txtFsbNumber.Text = string.Empty;
            txtRegistrationNumber.Text = string.Empty;          
            txtEmail.Text = string.Empty;          
            //lblMessage.Visible = false;          
        }

        #endregion

        #region Button Click Events        
        protected void btnSubmite_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Guid ParlourId = Guid.NewGuid();
                ApplicationSettingsModel model;
                SecureUsersModel model1;
                model1 = client.GetUserByEmailID(txtEmail.Text);
                if (model1 != null)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, "User Already Exists.");
                }
                else
                {

                    model = new ApplicationSettingsModel();
                    model.pkiApplicationID = 0;
                    model.ApplicationName = txtCompanyName.Text;
                    model.ApplicationLogoPath = string.Empty;
                    model.OwnerFirstName = txtFirstName.Text;
                    model.OwnerSurname = txtLastName.Text;
                    model.OwnerTelNumber = txtCellphone.Text;
                    model.OwnerCellNumber = txtCellphone.Text;
                    model.ManagerFirstName = string.Empty;
                    model.ManageSurname = string.Empty;
                    model.ManageTelNumber = txtCellphone.Text;
                    model.ManageCellNumber = txtCellphone.Text;
                    model.BusinessAddressLine1 = txtline1.Text;
                    model.BusinessAddressLine2 = txtline2.Text;
                    model.BusinessAddressLine3 = txtline3.Text;
                    model.BusinessAddressLine4 = txtline4.Text;
                    model.BusinessPostalCode = txtpostalcode.Text;
                    model.FSBNumber = txtFsbNumber.Text;
                    model.CereliaAPIKey = string.Empty;
                    model.RegistrationNumber = txtRegistrationNumber.Text;
                    model.ManageSlogan = string.Empty;
                    model.ManageEmail = txtEmail.Text;
                    model.ManageFaxNumber = string.Empty;
                    model.OwnerEmail = txtEmail.Text;
                    model.ApplicationRules = string.Empty;
                    model.VatNo = string.Empty;
                    model.IsAutoGeneratedPolicyNo = false;
                    
                    model.parlourid = ParlourId;

                  
                    //=============
                    model.Currentparlourid = ParlourId;
                    model = client.SaveApplication(model);
                    Guid retID = model.parlourid;
                    if (retID != null)
                    {
                        //SecureUserId = Convert.ToInt32(retID);

                        //================================Contact Detail==========================
                        model1 = new SecureUsersModel();
                        model1.PkiUserID = 0;
                        model1.UserName = txtFirstName.Text;
                        model1.Password = password.Text;
                        model1.parlourid = ParlourId;
                        model1.EmployeeSurname = txtLastName.Text;
                        model1.EmployeeFullname = txtFirstName.Text;
                        model1.EmployeeIDNumber = string.Empty;
                        model1.EmployeeContactNumber = txtCellphone.Text;
                        model1.EmployeeAddress1 = txtline1.Text;
                        model1.EmployeeAddress2 = txtline2.Text;
                        model1.EmployeeAddress3 = txtline3.Text;
                        model1.EmployeeAddress4 = txtline4.Text;
                        model1.LastModified = DateTime.Now;
                        model1.ModifiedUser = txtFirstName.Text;

                        model1.Email = txtEmail.Text;
                        model1.CustomId1 = 0;
                        model1.CustomId2 = 0;
                        model1.CustomId3 = 0;

                        model1.Active = false;
                        //================================================================ 

                        SecureUserId = client.SaveUserDetails(model1);
                    }
                    // UploadImage(model.pkiApplicationID);

                    // ==================[  User Security Group Insert Delete ]=============================

                    int sguserID = 0;

                    SecureUserGroupsModel modelS = new SecureUserGroupsModel();
                    modelS.pkiSecureUserGroups = sguserID;
                    modelS.fkiSecureUserID = SecureUserId;
                    modelS.fkiSecureGroupID = 4;
                    modelS.sSecureUserGroupDesc = "Super Users";
                    modelS.LastModified = System.DateTime.Now;
                    modelS.ModifiedUser = txtFirstName.Text;

                    sguserID = client.SaveUserGroupDetails(modelS);
                    modelS.pkiSecureUserGroups = sguserID;

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

                    // string membernumber = "no";
                    // if (PolicyNumber) { membernumber = "yes"; }
                    Adsmodel.GenerateMember = "yes";

                    Adsmodel = client.SaveAdditionalApplication(Adsmodel);

                    //================================[Insert Update Terms & Condition]===============================
                    ApplicationTnCModel objtc = new ApplicationTnCModel();
                    objtc.pkiAppTC = ApplicationID;
                    objtc.fkiApplicationID = ApplicationID;
                    objtc.TermsAndCondition = string.Empty;
                    objtc.TermsAndCondition = string.Empty;
                    objtc.LastModified = DateTime.Now;
                    objtc.ModifiedUser = UserName;
                    objtc.parlourid = retID;
                    objtc.TermsAndConditionFuneral = string.Empty;
                    objtc.TermsAndConditionTombstone = string.Empty;
                    objtc.Declaration = string.Empty;
                    objtc.TermsAndConditionFuneral = string.Empty;
                    objtc.TermsAndConditionTombstone = string.Empty;
                    objtc.Declaration = string.Empty;
                    int a = client.SaveTermsAndCondition(objtc);

                    if (retID != null)
                    {
                        if (SendActivationEmail(txtEmail.Text.Trim(), ParlourId,SecureUserId))
                        {                           
                           
                            ShowMessage(ref lblMessage, MessageType.Success, "Your registration completed successfully, Please check your email  for email verfication.");
                            lnkLogin.Visible = true;
                           
                        }
                    }
                }
                ClearControl();
            }
        }
        private bool SendActivationEmail(string userId, Guid ParlourId,int secureId)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    string fromEmailId = ConfigurationManager.AppSettings["ClaimDocumentSender"] == null ? "UISWEB@unpluggit.co.za" : ConfigurationManager.AppSettings["ClaimDocumentSender"].ToString().Trim();
                    MailMessage mm = new MailMessage(fromEmailId, txtEmail.Text.Trim());
                    mm.Subject = "Funeral Administration Registration";
                    string body = "Hi " + txtFirstName.Text.Trim() + ",";
                    body += "<br /><br />Thanks for registering with us on our portal. Please click on following link to activate your account.";
                    body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("NewRegistration.aspx","Activation.aspx?code="+ ParlourId + "&Id="+ secureId.ToString()) + "'>Click here to activate your account.</a>";
                    body += "<br /><br />Thanks";
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    smtpClient.Send(mm);
                    return true;
                    //ShowMessage(ref lblMessage, MessageType.Success, "Email sent successfully");
                }
            }
            catch (Exception exc)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, "Company added successfully but failed to send email to your email id. Please contact to administrator to activate your account.");
               return false;
            }
            
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
           
            //Response.Redirect("Login.aspx");
        }
        #endregion
        #endregion
    }
}