using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        public IPrincipal UserData { get; }

        //protected override void Initialize(RequestContext requestContext)
        //{
        //    //this.UserData = requestContext.HttpContext.User;
        //}
        private ActionExecutingContext _filterContext;
        public ActionResult RedirectToLogin()
        {
            return Redirect("~/Admin/Login.aspx");
        }
        public BaseAdminController(int pageId = 0)
        {
            if (pageId != 0)
                this.dbPageId = pageId;
        }
        public void BindCompanyList(string type = null)
        {
            List<ApplicationSettingsModel> model = new List<ApplicationSettingsModel>();
            if (this.IsAdministrator)
            {
                if (!string.IsNullOrEmpty(type))
                {
                    model.Add(new ApplicationSettingsModel() { ApplicationName = "All", parlourid = Guid.Empty });
                }
                model.AddRange(ToolsSetingBAL.GetAllApplicationList(ParlourId, 1, 0).ToList());
                if (model == null)
                    model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
            }
            else
            {
                model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
            }
            ViewBag.Companies = model;
        }
        public void BindSchemeCompanyList(string type = null)
        {
            List<ApplicationSettingsModel> model = new List<ApplicationSettingsModel>();
            if (this.IsAdministrator)
            {
                if (!string.IsNullOrEmpty(type))
                {
                    model.Add(new ApplicationSettingsModel() { ApplicationName = "All", parlourid = Guid.Empty });
                }
                model.AddRange(ToolsSetingBAL.GetAllApplicationList(ParlourId, 1, 0).ToList());
                if (model == null)
                    model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
            }
            else
            {
                model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
            }
            ViewBag.Companies = model;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this._filterContext = filterContext;

            if (dbPageId != 0)
            {

                List<Model.tblPageModel> obj = SideMenuModelList;

                HasAccess = obj.Where(x => x.ID == dbPageId).Select(x => x.HasAccess).FirstOrDefault();
                HasCreateRight = obj.Where(x => x.ID == dbPageId).Select(x => x.IsWrite).FirstOrDefault();
                HasReadRight = obj.Where(x => x.ID == dbPageId).Select(x => x.IsRead).FirstOrDefault();
                HasDeleteRight = obj.Where(x => x.ID == dbPageId).Select(x => x.IsDelete).FirstOrDefault();//false
                HasEditRight = obj.Where(x => x.ID == dbPageId).Select(x => x.IsUpdate).FirstOrDefault();
                HasReversalPayment = obj.Where(x => x.ID == dbPageId).Select(x => x.IsReversalPayment).FirstOrDefault();

                List<Funeral.Model.SecureUserGroupsModel> grp = SecurUserGroupModel;//privateClient.EditSecurUserbyID(UserID);
                List<int> list = new List<int>();
                list.Add(4);
                list.Add(12);
                if (!(grp.Where(x => list.Contains(x.fkiSecureGroupID)).FirstOrDefault() == null))
                {
                    HasAccess = true;
                    HasCreateRight = true;
                    HasReadRight = true;
                    HasDeleteRight = true;//false;
                    HasEditRight = true;
                    HasReversalPayment = true;
                }
            }
            else
            {
                //HttpContext.Response.Redirect("~/Admin/403Error.aspx");
                Redirect(string.Format("{0}://{1}{2}/{3}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~").Remove(Url.Content("~").Length - 1), "Error"));

                return;
            }


            System.Web.HttpContext.Current.Session["HasAccess"] = HasAccess;
            System.Web.HttpContext.Current.Session["HasAccess"] = HasAccess;
            System.Web.HttpContext.Current.Session["HasCreateRight"] = HasCreateRight;
            System.Web.HttpContext.Current.Session["HasReadRight"] = HasReadRight;
            System.Web.HttpContext.Current.Session["HasDeleteRight"] = HasDeleteRight;
            System.Web.HttpContext.Current.Session["HasEditRight"] = false;
            System.Web.HttpContext.Current.Session["HasReversalPayment"] = HasReversalPayment;

            base.OnActionExecuting(filterContext);
        }
        public int dbPageId { get; set; }
        public bool HasReadRight { get; set; }
        public bool HasCreateRight { get; set; }
        public bool HasDeleteRight { get; set; }
        public bool HasEditRight { get; set; }
        public bool HasReversalPayment { get; set; }
        public bool HasAccess { get; set; }

        public int UserID
        {
            get
            {
                if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string[] strData = ticket.UserData.Split('|');
                    if (strData.Any())
                        return Convert.ToInt32(strData[3]);
                    else
                        return 0;
                    //return new Guid(string.IsNullOrEmpty(ticket.UserData) ? "00000000-0000-0000-0000-000000000000" : ticket.UserData);
                }
                else
                {
                    return 0;
                    //return new Guid("00000000-0000-0000-0000-000000000000");
                }
            }

        }
        public Guid CurrentParlourId
        {
            get
            {
                if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (Session["CurrentParlourId"] == null)
                    {
                        FormsIdentity id = (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket;
                        string[] strData = ticket.UserData.Split('|');
                        if (strData.Count() > 0)
                            return new Guid(string.IsNullOrEmpty(strData[0]) ? "00000000-0000-0000-0000-000000000000" : strData[0]);
                        else
                            return new Guid("00000000-0000-0000-0000-000000000000");
                    }
                    else
                        return new Guid(Session["CurrentParlourId"].ToString());
                }
                else
                {
                    return new Guid("00000000-0000-0000-0000-000000000000");
                }
            }
            set
            {
                Session["CurrentParlourId"] = value;
            }
        }

        public Guid ParlourId
        {
            get
            {
                if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string[] strData = ticket.UserData.Split('|');
                    if (strData.Count() > 0)
                        return new Guid(string.IsNullOrEmpty(strData[0]) ? "00000000-0000-0000-0000-000000000000" : strData[0]);
                    else
                        return new Guid("00000000-0000-0000-0000-000000000000");
                }
                else
                {
                    return new Guid("00000000-0000-0000-0000-000000000000");
                }
            }

        }

        public string UserName
        {
            get
            {
                if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
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

        public string ApplicationName
        {
            get
            {
                if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string[] strData = ticket.UserData.Split('|');
                    if (strData.Count() > 0)
                    {
                        Session["ApplicationName"] = strData[1].ToString();
                        return strData[1];
                    }
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



        #region Fields
        public int MemberId
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["_memberId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(System.Web.HttpContext.Current.Session["_memberId"]);
            }
            set
            {
                System.Web.HttpContext.Current.Session["_memberId"] = value;
            }
        }
        public string MemburNumber
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["_MemburNumber"] == null)
                    return string.Empty;
                else
                    return System.Web.HttpContext.Current.Session["_MemburNumber"].ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["_MemburNumber"] = value;
            }
        }
        public string ClaimId
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["_ClaimId"] == null)
                    return string.Empty;
                else
                    return System.Web.HttpContext.Current.Session["_ClaimId"].ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Session["_ClaimId"] = value;
            }
        }
        public DataTable LocalQoute
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["LocalQoute"] != null)
                {
                    return System.Web.HttpContext.Current.Session["LocalQoute"] as DataTable;
                }
                else
                {
                    return null;
                }
            }
            set { System.Web.HttpContext.Current.Session["LocalQoute"] = value; }
        }
        public string Currency
        {
            get
            {
                if (System.Web.HttpContext.Current.User != null && System.Web.HttpContext.Current.User.Identity != null && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string[] strData = ticket.UserData.Split('|');
                    return strData.Count() == 5 ? strData[4].Trim() : "R";
                }
                return "R";
            }

        }
        public List<Funeral.Model.tblPageModel> SideMenuModelList
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["SideMenuModelList"] == null)
                {
                    List<tblPageModel> obj = RightsBAL.LoadSideMenu(ParlourId, UserID);
                    System.Web.HttpContext.Current.Session["SideMenuModelList"] = obj.ToList();
                    return obj.ToList();
                }
                else
                {
                    return System.Web.HttpContext.Current.Session["SideMenuModelList"] as List<Funeral.Model.tblPageModel>;
                }
            }
            set
            {
                System.Web.HttpContext.Current.Session["SideMenuModelList"] = value;
            }
        }
        public List<Funeral.Model.SecureUserGroupsModel> SecurUserGroupModel
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["SecurUserGroupModel"] == null)
                {

                    List<SecureUserGroupsModel> obj = ToolsSetingBAL.EditSecurUserbyID(UserID);
                    System.Web.HttpContext.Current.Session["SecurUserGroupModel"] = obj.ToList();
                    return obj.ToList();
                }
                else
                {
                    return System.Web.HttpContext.Current.Session["SecurUserGroupModel"] as List<Funeral.Model.SecureUserGroupsModel>;
                }
            }
            set
            {
                System.Web.HttpContext.Current.Session["SecurUserGroupModel"] = value;
            }
        }
        public List<KeyValue> ClaimStatusTypes
        {
            get
            {
                List<KeyValue> keyValues = new List<KeyValue>();
                keyValues.Add(new KeyValue { Key = "New", NameText = "New", Value = "1" });
                keyValues.Add(new KeyValue { Key = "Assessment", NameText = "Assessment", Value = "2" });
                keyValues.Add(new KeyValue { Key = "ReqsPending", NameText = "ReqsPending", Value = "3" });
                keyValues.Add(new KeyValue { Key = "AllDocsReceived", NameText = "AllDocsReceived", Value = "4" });
                keyValues.Add(new KeyValue { Key = "PreAuth", NameText = "PreAuth", Value = "5" });
                keyValues.Add(new KeyValue { Key = "FinalAuth", NameText = "FinalAuth", Value = "6" });
                keyValues.Add(new KeyValue { Key = "Finance", NameText = "Finance", Value = "7" });
                keyValues.Add(new KeyValue { Key = "Declined", NameText = "Declined", Value = "8" });
                keyValues.Add(new KeyValue { Key = "UnClaimed", NameText = "UnClaimed", Value = "9" });
                keyValues.Add(new KeyValue { Key = "Closed", NameText = "Closed", Value = "10" });
                return keyValues;
            }
        }
        public List<KeyValue> DashboardReportTypes
        {
            get
            {
                List<KeyValue> keyValues = new List<KeyValue>();
                keyValues.Add(new KeyValue { Key = "UIS_Daily Cash Up Summary Dashboard", NameText = "Daily Cash Up Summary", Value = "1" });
                keyValues.Add(new KeyValue { Key = "UIS_Daily Cash Up Summary Dashboard", NameText = "Sent SMS Report", Value = "2" });
                keyValues.Add(new KeyValue { Key = "UIS_Daily Cash Up Summary Dashboard", NameText = "Premiums Collected Report", Value = "3" });
                keyValues.Add(new KeyValue { Key = "UIS_Daily Cash Up Summary Dashboard", NameText = "Total Policy Report", Value = "4" });
                keyValues.Add(new KeyValue { Key = "UIS_Daily Cash Up Summary Dashboard", NameText = "Outstanding Payments Report", Value = "5" });
                return keyValues;
            }
        }
        public bool IsAdministrator
        {
            get
            {

                List<SecureUserGroupsModel> groupList = GetUserGroupRole;
                if (groupList == null)
                    return false;
                var administratorModel = groupList.Where(x => x.fkiSecureGroupID == 12).FirstOrDefault();
                if (administratorModel == null)
                    return false;
                return true;
            }
        }
        public string IPAddress
        {
            get
            {

                return Request.UserHostAddress.ToString();
            }
        }
        public bool IsSuperUser
        {
            get
            {

                List<SecureUserGroupsModel> groupList = GetUserGroupRole;
                if (groupList == null)
                    return false;
                var administratorModel = groupList.FirstOrDefault(x => x.fkiSecureGroupID == 4);
                if (administratorModel == null)
                    return false;
                return true;
            }
        }
        public List<SecureUserGroupsModel> GetUserGroupRole
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["GetUserGroupRole"] == null)
                {

                    List<SecureUserGroupsModel> secureModelList = ToolsSetingBAL.GetSuperUserAccessByID(UserID, ParlourId);
                    System.Web.HttpContext.Current.Session["GetUserGroupRole"] = secureModelList.ToList();
                    return secureModelList.ToList();
                }
                else
                {
                    return System.Web.HttpContext.Current.Session["GetUserGroupRole"] as List<Funeral.Model.SecureUserGroupsModel>;
                }
            }
        }
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
        protected string ShowMessage(MessageType objMessageType, string Message)
        {
            string strMessage = string.Empty;
            return "<div class=\"alert alert-" + objMessageType.ToString().ToLower() + "\">" + Message + "</div>";
        }
        public static string EncodeQueryString(string Id)
        {
            var plaintextBytes = Encoding.UTF8.GetBytes(Id);
            return MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);
        }
        public static string DecodeQueryString(string encodedstring)
        {
            var decryptedBytes = MachineKey.Decode(encodedstring, MachineKeyProtection.All);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
        public string FormatDateTime(string date)
        {
            return string.IsNullOrEmpty(date) ? string.Empty : Convert.ToDateTime(date).ToString("dd MMM yyyy");
        }
        public PageRights CheckPageRights { get; set; }
        public void BindCompanyList(DropDownList ddlCompanyList, HtmlControl dvAdministrator)
        {
            dvAdministrator.Visible = IsAdministrator;
            if (this.IsAdministrator)
            {
                List<ApplicationSettingsModel> model = ToolsSetingBAL.GetAllApplicationList(ParlourId, 1, 0);
                if (model != null)
                {
                    ddlCompanyList.Visible = true;
                    ddlCompanyList.DataTextField = "ApplicationName";
                    ddlCompanyList.DataValueField = "ParlourId";
                    ddlCompanyList.DataSource = model;
                    ddlCompanyList.DataBind();
                    //ddlCompanyList.Items.Insert(0, new ListItem("Select", "0"));
                    ddlCompanyList.SelectedValue = ParlourId.ToString();

                }
                else
                {
                    ddlCompanyList.Items.Insert(0, new ListItem(ApplicationName, ParlourId.ToString()));
                    ddlCompanyList.SelectedValue = ParlourId.ToString();
                }
            }
            else
            {
                ddlCompanyList.Items.Insert(0, new ListItem(ApplicationName, ParlourId.ToString()));
                ddlCompanyList.SelectedValue = ParlourId.ToString();
            }
        }
        #endregion

        protected void RedirectToErrorPage()
        {
            this._filterContext.HttpContext.Response.Redirect(string.Format("{0}://{1}{2}/{3}", this._filterContext.HttpContext.Request.Url.Scheme, this._filterContext.HttpContext.Request.Url.Authority, this._filterContext.HttpContext.Request.ApplicationPath, "Admin/Error/Error403"));
        }
    }
}