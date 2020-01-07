using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Funeral.Model;
using Funeral.Web.App_Start;
using System.Text;
using System.Web.Services;
using Funeral.BAL;
using System.Data.SqlClient;
using System.IO;

namespace Funeral.Web.Tools
{
    public partial class AgentInfoSetup : AdminBasePage
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
        public string sortBYExpression
        {
            get
            {
                object viewState = this.ViewState["sortBYExpression"];

                return (viewState == null) ? "ID" : (string)viewState;
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
            this.dbPageId = 17;
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
            if (!Page.IsPostBack)
            {
                ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(PageSize.ToString()));
                BindAllAgentDetails();
                btnSubmite.Enabled = HasCreateRight;
            }
        }

        #endregion

        #region Page size change event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindAllAgentDetails();
        }
        #endregion

        #region Private/Public function and methods
        public void BindAllAgentDetails()
        {
            gvCompany.PageSize = PageSize;
            List<AgentInfoSetupModel> model = ToolsSetingBAL.GetAllAgentInfo(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
            gvCompany.DataSource = model;
            gvCompany.DataBind();
        }
        public void ClearControl()
        {
            btnSubmite.Text = "Save";
            ApplicationID = 0;
            txtaddress1.Text = string.Empty;
            txtaddress2.Text = string.Empty;
            txtaddress3.Text = string.Empty;
            txtaddress4.Text = string.Empty;
            txtcode.Text = string.Empty;
            txtcontactnumber.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtfullname.Text = string.Empty;
            txtpercentage.Text = string.Empty;
            txtsurname.Text = string.Empty;

        }
        public void BindApplicationToUpdate()
        {
            AgentInfoSetupModel ModelAgent;
            ModelAgent = ToolsSetingBAL.GetAgentByID(ApplicationID);
            if (ModelAgent != null) 
            {
                txtsurname.Text = ModelAgent.Surname;
                txtfullname.Text = ModelAgent.Fullname;
                txtpercentage.Text = ModelAgent.percentage.ToString("#,0.00");
                txtcontactnumber.Text = ModelAgent.ContactNumber;
                txtcode.Text = ModelAgent.Code;
                txtEmail.Text = ModelAgent.Email;
                txtaddress1.Text = ModelAgent.Address1;
                txtaddress2.Text = ModelAgent.Address2;
                txtaddress3.Text = ModelAgent.Address3;
                txtaddress4.Text = ModelAgent.Address4;
            }
            btnSubmite.Text = "Update";
        }

        #endregion

        #region Button Click Events
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindAllAgentDetails();
        }
        protected void btnSubmite_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                AgentInfoSetupModel model = new AgentInfoSetupModel();
                model.ID = ApplicationID;
                model.Surname = txtsurname.Text;
                model.Fullname = txtfullname.Text;
                model.percentage = (txtpercentage.Text == string.Empty ? 0 : Convert.ToDecimal(txtpercentage.Text));
                model.ContactNumber = txtcontactnumber.Text;
                model.Code = txtcode.Text;
                model.Email = txtEmail.Text;
                model.Address1 = txtaddress1.Text;
                model.Address2 = txtaddress2.Text;
                model.Address3 = txtaddress3.Text;
                model.Address4 = txtaddress4.Text;
                model.LastModified = System.DateTime.Now;
                model.ModifiedUser = UserName;
                model.parlourid = ParlourId;

                model = ToolsSetingBAL.SaveAgentInfo(model);


                ShowMessage(ref lblMessage, MessageType.Success, "Agent Info successfully saved");
                BindAllAgentDetails();
                ClearControl();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab512", "goToTab(5)", true);
            }
        }

        #endregion

        #region gvCompanyList Properties
        protected void gvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCompany.PageIndex = e.NewPageIndex;
            BindAllAgentDetails();
        }
        
        protected void gvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
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
                    int retID = ToolsSetingBAL.DeleteAgent(SApplicationID);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    BindAllAgentDetails();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
        }

        #endregion

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
            BindAllAgentDetails();
        }
        #endregion

       

    }
}