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
    public partial class VendorSetup : AdminBasePage
    {
        #region Page Property
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
        public int VendorID
        {
            get
            {
                if (ViewState["_VendorID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_VendorID"]);
            }
            set
            {
                ViewState["_VendorID"] = value;
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

                return (viewState == null) ? "pkiVendorID" : (string)viewState;
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
            this.dbPageId = 22;
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
                ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(PageSize.ToString()));
                BindVendorList();
                btnSubmite.Enabled = HasCreateRight;
            }
        }
        #endregion

        #region Page size change event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);

            BindVendorList();
        }
        #endregion

        #region Private/Public function and methods

        public void BindVendorList()
        {
            gvVendores.PageSize = PageSize;
            VendorModel[] model = client.GetAllVendores(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
            gvVendores.DataSource = model;
            gvVendores.DataBind();
        }
        public void BindVendorToUpdate()
        {
            VendorModel model = client.EditVendorbyID(VendorID, ParlourId);
            if (model == null)
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Branch.');</script>");
            }
            else
            {
                VendorID = model.pkiVendorID;
                txtVendorName.Text = model.VendorName;

                btnSubmite.Text = "Update";
            }
        }
        public void ClearControl()
        {
            btnSubmite.Text = "Save";
            VendorID = 0;
            txtVendorName.Text = string.Empty;
            lblMessage.Visible = false;
        }
        #endregion

        #region Button Click Events
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        protected void btnSubmite_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                VendorModel model;
                model = client.GetVendorByID(txtVendorName.Text, ParlourId);
                if (model != null && VendorID == 0)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, "Vendor Already Exists.");
                }
                else
                {
                    model = new VendorModel();
                    model.pkiVendorID = VendorID;
                    model.VendorName = txtVendorName.Text;
                    model.parlourid = ParlourId;
                    model.LastModified = System.DateTime.Now;
                    model.ModifiedUser = UserName;


                    //================================================================ 
                    int retID = client.SaveVendorDetails(model);
                    VendorID = retID;

                    ShowMessage(ref lblMessage, MessageType.Success, "Vendor Details successfully saved");
                    BindVendorList();
                    ClearControl();
                }
                lblMessage.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab512", "goToTab(5)", true);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindVendorList();
        }

        #endregion

        #region gvVendoresList Properties
        protected void gvVendores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVendores.PageIndex = e.NewPageIndex;
            BindVendorList();
        }

        protected void gvVendores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditVendor")
            {
                VendorID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    BindVendorToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            if (e.CommandName == "deleteVendor")
            {
                int SBranchId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    int retID = client.DeleteVendor(SBranchId);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    BindVendorList();
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

        protected void gvVendores_Sorting(object sender, GridViewSortEventArgs e)
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
            BindVendorList();
        }
        #endregion

        #endregion
    }
}