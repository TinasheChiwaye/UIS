using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class Members : AdminBasePage
    {

        #region Page Property
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
        public string SortBy
        {
            get
            {
                if (ViewState["_SortBy"] == null)
                    return "pkiMemberID";
                else { return ViewState["_SortBy"].ToString(); }
            }
            set { ViewState["_SortBy"] = value; }

        }
        public string SortOrder
        {
            get
            {
                if (ViewState["_SortOrder"] == null)
                    return "ASC";
                else { return ViewState["_SortOrder"].ToString(); }
            }
            set { ViewState["_SortOrder"] = value; }

        }
        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 4;
        }
        #endregion

        #region Page load event
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(PageSize.ToString()));
                LoadStatus();
                BindCompanyList(ddlCompanyList, dvAdministrator);                
                BindMember();
            }
        }
        #endregion

        #region Page size change event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            
            BindMember();
        }
        #endregion

        #region Paging click event
       
        #endregion

        #region Private/Public function and methods
        /// <summary>
        /// Get All Members from database and bind here.
        /// </summary>
        public void BindMember()
        {
            gvMembers.PageSize = PageSize;
            MembersViewModel model = MembersBAL.GetAllMembers(new Guid(ddlCompanyList.SelectedValue), PageSize, PageNum, txtKeyword.Text, SortBy, SortOrder,ddlStatusSearch.Text, "");
            StringBuilder sb = new StringBuilder();
            gvMembers.DataSource = model.MemberList;
            gvMembers.DataBind();
        }
        public void LoadStatus()
        {
            List<StatusModel> statusList = CommonBAL.GetStatus(FuneralEnum.StatusAssociatedTable.Members.ToString()).ToList();
            ddlStatus.DataSource = statusList;
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Select Member Status", "0"));
            ddlStatusSearch.DataSource = statusList;
            ddlStatusSearch.DataBind();
            ddlStatusSearch.Items.Insert(0, new ListItem("Any", "0"));
        }        
        #endregion

        #region Keyword search event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindMember();
        }
        #endregion

        protected void SoryBy_Click(object sender, EventArgs e)
        {
            BindMember();
        }

        protected void lbSortMemberNo_Click(object sender, EventArgs e)
        {
            ToggleSortOrder();
            switch (((LinkButton)sender).ID.ToString())
            {
                case "lbSortMemberNo":
                    SortBy = "MemeberNumber";
                    break;
                case "lbSortMemberName":
                    SortBy = "FullNames";
                    break;
                case "lbSortContactNo":
                    SortBy = "Telephone";
                    break;
                case "lbSortPolicyDate":
                    SortBy = "CoverDate";
                    break;
                default:
                    SortBy = "MemeberNumber";
                    break;
            }
            BindMember();
        }
        public void ToggleSortOrder()
        {
            if (SortOrder == "ASC")
                SortOrder = "DESC";
            else
                SortOrder = "ASC";
        }

        protected void gvMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMembers.PageIndex = e.NewPageIndex;
            BindMember();
        }

        protected void gvMembers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteMemeber")
            {
                try
                {
                    //int retID = client.DeleteMember(Convert.ToInt32(e.CommandArgument));
                    MembersBAL.DeleteMember(Convert.ToInt32(e.CommandArgument));
                    BindMember();
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");                    
                    lblMessage.Visible = true;
                }
                catch(Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }
                
            }
        }

        protected void btnChangeSubmit_Click(object sender, EventArgs e)
        {
            ChangeStatusReason model = new ChangeStatusReason();
            model.CurrentStatus = hdnOldStatus.Value;
            model.fkiMemberId = Convert.ToInt32(hdnMemberId.Value);
            model.NewStatus = ddlStatus.Text;
            model.ChangedBy = this.UserID;
            model.ParlourID = new Guid(hdnParlourId.Value);
            model.ChangeReason = txtChangeReason.Text;
            MembersBAL.MemberStatusChangeReason(model);
            MembersBAL.UpdateMemberStatus(new Guid(hdnParlourId.Value), Convert.ToInt32(hdnMemberId.Value), ddlStatus.Text);
            BindMember();
            txtChangeReason.Text = string.Empty;
            hdnMemberId.Value = string.Empty;
            hdnOldStatus.Value = string.Empty;
        }
    }
}