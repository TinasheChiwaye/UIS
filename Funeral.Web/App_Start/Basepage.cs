using Funeral.BAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Funeral.Web.App_Start
{
    public class AdminBasePage : System.Web.UI.Page
    {

        public int dbPageId { get; set; }
        public bool HasReadRight { get; set; }
        public bool HasCreateRight { get; set; }
        public bool HasDeleteRight { get; set; }
        public bool HasEditRight { get; set; }
        public bool HasReversalPayment { get; set; }
        public bool HasAccess { get; set; }
        public Guid ParlourId
        {
            get
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)User.Identity;
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
        public string ApplicationName
        {
            get
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)User.Identity;
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
                    {
                        return strData[2];
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
        public int UserID
        {
            get
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)User.Identity;
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
        public string Currency
        {
            get
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)User.Identity;
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
                if (Session["SideMenuModelList"] == null)
                {
                    List<tblPageModel> obj = RightsBAL.LoadSideMenu(ParlourId, UserID);
                    Session["SideMenuModelList"] = obj.ToList();
                    return obj.ToList();
                }
                else
                {
                    return Session["SideMenuModelList"] as List<Funeral.Model.tblPageModel>;
                }
            }
            set
            {
                Session["SideMenuModelList"] = value;
            }
        }

        public List<Funeral.Model.SecureUserGroupsModel> SecurUserGroupModel
        {
            get
            {
                if (Session["SecurUserGroupModel"] == null)
                {
                    List<SecureUserGroupsModel> obj = ToolsSetingBAL.EditSecurUserbyID(UserID);
                    Session["SecurUserGroupModel"] = obj.ToList();
                    return obj.ToList();
                }
                else
                {
                    return Session["SecurUserGroupModel"] as List<Funeral.Model.SecureUserGroupsModel>;
                }
            }
            set
            {
                Session["SecurUserGroupModel"] = value;
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
        public List<SecureUserGroupsModel> RoleId_List
        {
            get
            {
                return ToolsSetingBAL.GetSuperUserAccessByUserId(UserID);
            }
        }
        public List<SecureUserGroupsModel> GetUserGroupRole
        {
            get
            {
                if (Session["GetUserGroupRole"] == null)
                {
                    List<SecureUserGroupsModel> secureModelList = ToolsSetingBAL.GetSuperUserAccessByID(UserID, ParlourId);
                    Session["GetUserGroupRole"] = secureModelList.ToList();
                    return secureModelList.ToList();
                }
                else
                {
                    return Session["GetUserGroupRole"] as List<Funeral.Model.SecureUserGroupsModel>;
                }
            }
        }
        protected override void OnInit(EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {

                if (dbPageId != 0)
                {
                    List<Model.tblPageModel> obj = SideMenuModelList;

                    if (!(obj.Where(x => x.ID == dbPageId).Any()))
                    {
                        Response.Redirect("~/Admin/403Error.aspx", false);
                    }
                    else
                    {
                        HasAccess = obj.Where(x => x.ID == dbPageId).Select(x => x.HasAccess).FirstOrDefault();
                        HasCreateRight = obj.Where(x => x.ID == dbPageId).Select(x => x.IsWrite).FirstOrDefault();
                        HasReadRight = obj.Where(x => x.ID == dbPageId).Select(x => x.IsRead).FirstOrDefault();
                        HasDeleteRight = obj.Where(x => x.ID == dbPageId).Select(x => x.IsDelete).FirstOrDefault();
                        HasEditRight = obj.Where(x => x.ID == dbPageId).Select(x => x.IsUpdate).FirstOrDefault();
                        HasReversalPayment = obj.Where(x => x.ID == dbPageId).Select(x => x.IsReversalPayment).FirstOrDefault();
                    }

                    List<SecureUserGroupsModel> grp = SecurUserGroupModel;
                    List<int> list = new List<int>();
                    list.Add(4);
                    list.Add(12);
                    if (!(grp.Where(x => list.Contains(x.fkiSecureGroupID)).FirstOrDefault() == null))
                    {
                        HasAccess = true;
                        HasCreateRight = true;
                        HasReadRight = true;
                        HasDeleteRight = true; 
                        HasEditRight = true;
                        HasReversalPayment = true;
                    }
                }
                base.OnInit(e);
            }
            else
            {
                Response.Redirect("~/Admin/Login.aspx", false);
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

    }

    public class PageRights
    {
        public PageRights()
        {
            this.HasReadRight = false;
            this.HasCreateRight = false;
            this.HasDeleteRight = false;
            this.HasEditRight = false;
            this.HasAccess = false;
        }
        public bool HasReadRight { get; set; }
        public bool HasCreateRight { get; set; }
        public bool HasDeleteRight { get; set; }
        public bool HasEditRight { get; set; }
        public bool HasAccess { get; set; }
    }
}