using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Funeral.Web.Tools
{
    public partial class UserSetup : AdminBasePage
    {

        #region Page Property

        public List<SecureUserGroupsModel> LoginUserRoles
        {
            get
            {
                return ToolsSetingBAL.EditSecurUserbyID(UserID);
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
        public Guid CompanyParlourId
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
        public string SortByExpression
        {
            get
            {
                object viewState = this.ViewState["sortBYExpression"];

                return (viewState == null) ? "PkiUserID" : (string)viewState;
            }
            set { this.ViewState["sortBYExpression"] = value; }
        }

        private string SortType
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
            this.dbPageId = 21;
        }
        #endregion

        #region Page load event

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            if (!Page.IsPostBack)
            {
                ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(PageSize.ToString()));
                BindSecureGroupList();
                if (Request.QueryString["CompanyParlourID"] != null)
                {
                    CompanyParlourId = new Guid(Request.QueryString["CompanyParlourID"]);
                    ApplicationSettingsModel modelcom;
                    modelcom = ToolsSetingBAL.GetApplictionByParlourID(CompanyParlourId);
                    if (modelcom != null)
                    {
                        if (Request.QueryString["NewId"] != null)
                            ClientScript.RegisterStartupScript(GetType(), "Javascript",
                                "javascript:alert(" + modelcom.ApplicationName +
                                "'Company Details successfully saved. Please Add Company User'); ", true);
                        else
                            ClientScript.RegisterStartupScript(GetType(), "Javascript",
                                "javascript:alert('Add " + modelcom.ApplicationName + " Company New User'); ", true);
                    }
                    BindCustomDetails(CompanyParlourId);
                    BindBranches(CompanyParlourId);
                }
                else
                {
                    CompanyParlourId = ParlourId;
                    BindCustomDetails(ParlourId);
                    BindBranches(ParlourId);
                }
                BindUserList();
            }
            SecureUserGroupsModel model;
            model = ToolsSetingBAL.GetUserAccessByID(UserID, ParlourId);
            if (model == null)
            {

                foreach (ListItem lst in chkSecurityGroup.Items)
                {
                    if (Convert.ToInt32(lst.Value) == 3 || Convert.ToInt32(lst.Value) == 4 || Convert.ToInt32(lst.Value) == 12)
                        lst.Enabled = false;
                }
            }
        }

        #endregion

        #region Page size change event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);

            BindUserList();
        }
        #endregion

        #region Private/Public function and methods
        public void BindSecureGroupList()
        {
            List<SecureGroupModel> model;
            model = ToolsSetingBAL.GetSecureGrouList();
            if (model.Any())
            {
                List<int> list = new List<int> { 12 };
                var result = LoginUserRoles.FirstOrDefault(x => list.Contains(x.fkiSecureGroupID));
                if (result == null) { model.Remove(model.FirstOrDefault(x => x.pkiSecureGroupID == 12)); }
                model.Remove(model.FirstOrDefault(x => x.pkiSecureGroupID == 3));
                chkSecurityGroup.DataSource = model;
                //chkSecurityGroup.DataTextField = "sSecureGroupName";
                //chkSecurityGroup.DataValueField = "pkiSecureGroupID";
                chkSecurityGroup.DataBind();
            }
        }
        /// <summary>
        /// Get All Users from database and bind here.
        /// </summary>
        public void BindUserList()
        {
            gvUsers.PageSize = PageSize;
            gvUsers.DataSource = ToolsSetingBAL.GetAllUsers(Request.QueryString["CompanyParlourID"] != null ? CompanyParlourId : ParlourId, PageSize, PageNum, txtKeyword.Text, SortByExpression, SortType);
            gvUsers.DataBind();
        }
        public void BindUserToUpdate()
        {
            SecureUsersModel model = ToolsSetingBAL.EditUserbyID(SecureUserId, Request.QueryString["CompanyParlourID"] != null ? CompanyParlourId : ParlourId);
            if ((model == null) || (model.parlourid != ParlourId || model.parlourid != CompanyParlourId))
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this User.');</script>");
            }
            else
            {
                CompanyParlourId = model.parlourid;
                SecureUserId = model.PkiUserID;
                txtUserNameUser.Text = model.UserName;
                txtPasswordpass.Attributes.Add("value", model.Password);
                //ParlourId = model.parlourid;
                txtSurname.Text = model.EmployeeSurname;
                txtFullName.Text = model.EmployeeFullname;
                txtIDNumber.Text = model.EmployeeIDNumber;
                txtCellPhone.Text = model.EmployeeContactNumber;
                txtline1.Text = model.EmployeeAddress1;
                txtline2.Text = model.EmployeeAddress2;
                txtline3.Text = model.EmployeeAddress3;
                txtline4.Text = model.EmployeeAddress4;
                //System.DateTime.Now = model.LastModified;
                //UserName = model.ModifiedUser;
                txtUserNameUser.Text = model.Email;
                txtpostalcode.Text = model.Code;
                // txtAgentName.Text = model.AgentName;
                //txtAgentSurname.Text = model.AgentSurname;
                /* changes for custom field implemented on 10th April 2017*/
                //ddlCustom1.SelectedValue = model.CustomId1.ToString();
                //ddlCustom2.SelectedValue = model.CustomId2.ToString();
                //ddlCustom3.SelectedValue = model.CustomId3.ToString();
                /* changes for custom field implemented on 10th April 2017  completed*/
                ddlBankBranch.SelectedValue = model.BranchId.ToString();

                List<SecureUserGroupsModel> modelS = ToolsSetingBAL.EditSecurUserbyID(SecureUserId);
                foreach (ListItem lst in chkSecurityGroup.Items)
                {
                    lst.Selected = false;
                }
                foreach (SecureUserGroupsModel lstModel in modelS)
                {

                    foreach (ListItem lst in chkSecurityGroup.Items)
                    {
                        if (Convert.ToInt32(lst.Value) == lstModel.fkiSecureGroupID)
                            lst.Selected = true;
                    }
                }
                btnSubmite.Text = @"Update";
            }
        }
        public void ClearControl()
        {
            if (Request.QueryString["CompanyParlourID"] != null)
                CompanyParlourId = new Guid("00000000-0000-0000-0000-000000000000");
            btnSubmite.Text = @"Save";
            SecureUserId = 0;
            txtUserNameUser.Text = "";
            txtPasswordpass.Text = "";
            txtSurname.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtIDNumber.Text = string.Empty;
            txtCellPhone.Text = string.Empty;
            txtline1.Text = string.Empty;
            txtline2.Text = string.Empty;
            txtline3.Text = string.Empty;
            txtline4.Text = string.Empty;
            txtpostalcode.Text = string.Empty;
            // txtAgentName.Text = string.Empty;
            // txtAgentSurname.Text = string.Empty;
            lblMessage.Visible = false;

            foreach (ListItem lst in chkSecurityGroup.Items)
            {
                lst.Selected = false;
            }
        }

        #endregion

        #region Button Click Events
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindUserList();
        }
        protected void btnSubmite_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                SecureUsersModel model;
                model = ToolsSetingBAL.GetUserByID(txtUserNameUser.Text, Request.QueryString["CompanyParlourID"] != null ? CompanyParlourId : ParlourId);
                if (model != null && SecureUserId == 0)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, "User Already Exists.");
                }
                else
                {
                    model = new SecureUsersModel();
                    model.PkiUserID = SecureUserId;
                    model.UserName = txtFullName.Text;
                    model.Password = txtPasswordpass.Text;
                    if (CompanyParlourId != new Guid("00000000-0000-0000-0000-000000000000"))
                        model.parlourid = CompanyParlourId;
                    else
                        model.parlourid = ParlourId;
                    model.EmployeeSurname = txtSurname.Text;
                    model.EmployeeFullname = txtFullName.Text;
                    model.EmployeeIDNumber = txtIDNumber.Text;
                    model.EmployeeContactNumber = txtCellPhone.Text;
                    model.EmployeeAddress1 = txtline1.Text;
                    model.EmployeeAddress2 = txtline2.Text;
                    model.EmployeeAddress3 = txtline3.Text;
                    model.EmployeeAddress4 = txtline4.Text;
                    model.LastModified = System.DateTime.Now;
                    model.ModifiedUser = UserName;
                    model.Email = txtUserNameUser.Text;
                    model.Code = txtpostalcode.Text;
                    //model.CustomId1 = Convert.ToInt32(ddlCustom1.SelectedValue);
                    //model.CustomId2 = Convert.ToInt32(ddlCustom2.SelectedValue);
                    model.Active = true;


                    //model.CustomId3 = Convert.ToInt32(ddlCustom3.SelectedValue);

                    if (ddlBankBranch.SelectedIndex != -1)
                        model.BranchId = new Guid(ddlBankBranch.SelectedValue);
                    else
                        model.BranchId = default(Guid);
                    //model.AgentName = txtAgentName.Text;
                    // model.AgentSurname = txtAgentSurname.Text;

                    //================================================================ 
                    int retID = ToolsSetingBAL.SaveUserDetails(model);
                    SecureUserId = retID;

                    // ==================[  User Security Group Insert Delete ]=============================

                    int sguserID = 0;
                    foreach (ListItem lst in chkSecurityGroup.Items)
                    {
                        if (lst.Selected)
                        {
                            SecureUserGroupsModel modelS = new SecureUserGroupsModel();
                            modelS.pkiSecureUserGroups = sguserID;
                            modelS.fkiSecureUserID = SecureUserId;
                            modelS.fkiSecureGroupID = Convert.ToInt32(lst.Value);
                            modelS.sSecureUserGroupDesc = lst.Text;
                            modelS.LastModified = System.DateTime.Now;
                            modelS.ModifiedUser = UserName;
                            if (sguserID == 0)
                            {
                                sguserID = ToolsSetingBAL.SaveUserGroupDetails(modelS);
                                modelS.pkiSecureUserGroups = sguserID;
                            }
                            sguserID = ToolsSetingBAL.SaveUserGroupDetails(modelS);
                        }
                    }
                    //bindEasyPayNumber();
                    ShowMessage(ref lblMessage, MessageType.Success, "User Details successfully saved");
                    BindUserList();
                    if (CompanyParlourId != new Guid("00000000-0000-0000-0000-000000000000"))
                        Response.Redirect("~/Tools/UserSetup.aspx");
                    ClearControl();
                }
                lblMessage.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab512", "goToTab(5)", true);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region gvUserList Properties
        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            BindUserList();
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ClearControl();
            if (e.CommandName == "EditUser")
            {
                SecureUserId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    BindUserToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            if (e.CommandName == "deleteUser")
            {
                int suserId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    ToolsSetingBAL.DeleteUser(suserId);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    BindUserList();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
        }

        #region "Sorting Event"

        private const string Ascending = "ASC";
        private const string Descending = "DESC";
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
            SortByExpression = sortExpression;
            SortType = direction;
        }

        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, Descending);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, Ascending);
            }
            BindUserList();
        }
        #endregion

        #endregion

        #region Custom field changes
        private void BindCustomDetails(Guid selectedParlourId)
        {
            var custom1 = CustomDetailsBAL.GetAllCustomDetailsByParlourId(selectedParlourId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.EmploymentType));
            var custom2 = CustomDetailsBAL.GetAllCustomDetailsByParlourId(selectedParlourId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.PaymentType));
            var custom3 = CustomDetailsBAL.GetAllCustomDetailsByParlourId(selectedParlourId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.Source));
            //ddlCustom1.DataSource = custom1;
            //ddlCustom1.DataTextField = "Name";
            //ddlCustom1.DataValueField = "Id";
            //ddlCustom1.DataBind();
            //ddlCustom1.Items.Insert(0, new ListItem("All", "0"));

            //ddlCustom2.DataSource = custom2;
            //ddlCustom2.DataTextField = "Name";
            //ddlCustom2.DataValueField = "Id";
            //ddlCustom2.DataBind();
            //ddlCustom2.Items.Insert(0, new ListItem("All", "0"));

            //ddlCustom3.DataSource = custom3;
            //ddlCustom3.DataTextField = "Name";
            //ddlCustom3.DataValueField = "Id";
            //ddlCustom3.DataBind();
            //ddlCustom3.Items.Insert(0, new ListItem("All", "0"));

        }

        public void BindBranches(Guid selectedParlourId)
        {
            List<BranchModel> objBranchModel = CommonBAL.BranchByparlourId(selectedParlourId);
            foreach (BranchModel branch in objBranchModel)
            {
                ListItem li = new ListItem();
                li.Text = branch.BranchName;
                li.Value = branch.Brancheid.ToString();
                ddlBankBranch.Items.Add(li);
            }
            ddlBankBranch.Items.Insert(0, new ListItem("Select Branch", "00000000-0000-0000-0000-000000000000"));
        }
        #endregion

    }
}