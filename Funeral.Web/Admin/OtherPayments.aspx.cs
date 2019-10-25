using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class OtherPayments : AdminBasePage
    {

        #region Page Property
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
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
            this.dbPageId = 30;
        }
        #endregion

        #region Page load event
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblMessage.Visible = false;
            if (!IsPostBack)
            {
                ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(PageSize.ToString()));
                //BindMember();
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

        #region Private/Public function and methods
        /// <summary>
        /// Get All Members from database and bind here.
        /// </summary>
        //public void BindMember()
        //{
        //    gvMembers.PageSize = PageSize;
        //    MembersPaymentViewModel model = client.GetAllPayentMembers(ParlourId, txtPolicyNo.Text, txtIDNo.Text, PageSize, PageNum, SortBy, SortOrder,"");
        //    gvMembers.DataSource = model.MemberList;
        //    gvMembers.DataBind();

        //    //gvMembers.Columns[1].Visible = false;
        //}
        public void BindMember()
        {
            gvMembers.PageSize = PageSize;
            MembersPaymentViewModel model = client.GetAllPayentMembers(ParlourId, txtPolicyNo.Text, txtIDNo.Text, PageSize, PageNum, SortBy, SortOrder, "");
            gvMembers.DataSource = model.MemberList;
            gvMembers.DataBind();
        }
        #endregion

        #region Keyword search event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindMember();
        }

        protected void gvMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMembers.PageIndex = e.NewPageIndex;
            BindMember();
        }
        #endregion

        protected void gvMembers_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvMembers.PageIndex = e.NewPageIndex;
            BindMember();
        }
    }
}