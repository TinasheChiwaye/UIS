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
    public partial class BranchSetup : AdminBasePage
    {
        #region Page Property
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
        public Guid BranchID
        {
            get
            {
                if (ViewState["_BranchID"] == null)
                    return new Guid("00000000-0000-0000-0000-000000000000");
                else
                    return new Guid(ViewState["_BranchID"].ToString());
            }
            set
            {
                ViewState["_BranchID"] = value;
            }
        }

        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 15;
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
                BindBranchesList();
                btnSubmite.Enabled = HasCreateRight;
            }
        }

        #endregion

        #region Private/Public function and methods

        public void BindBranchesList()
        {
            BranchModel[] model = client.GetAllBranches(ParlourId);
            gvBranches.DataSource = model;
            gvBranches.DataBind();
        }
        public void BindBranchToUpdate()
        {
            BranchModel model = client.EditBranchbyID(BranchID, ParlourId);
            if ((model == null) || (model.Parlourid != ParlourId))
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Branch.');</script>");
            }
            else
            {
                BranchID = model.Brancheid;
                txtBranchName.Text = model.BranchName;
                txtPhone.Text = model.TelNumber;
                txtCellPhone.Text = model.CellNumber;
                txtline1.Text = model.Address1;
                txtline2.Text = model.Address2;
                txtline3.Text = model.Address3;
                txtline4.Text = model.Address4;
                txtpostalcode.Text = model.Code;
                txtBranchCode.Text = model.BranchCode;
                txtRegion.Text = model.Region;

                btnSubmite.Text = "Update";
            }
        }
        public void ClearControl()
        {
            btnSubmite.Text = "Save";
            BranchID = new Guid("00000000-0000-0000-0000-000000000000");
            txtBranchName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtCellPhone.Text = string.Empty;
            txtline1.Text = string.Empty;
            txtline2.Text = string.Empty;
            txtline3.Text = string.Empty;
            txtline4.Text = string.Empty;
            txtpostalcode.Text = string.Empty;
            txtBranchCode.Text = string.Empty;
            txtRegion.Text = string.Empty;
            lblMessage.Visible = false;

        }
        #endregion

        #region Button Click Events
        protected void btnSubmite_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                BranchModel model;
                model = client.GetBranchByID(txtBranchName.Text, ParlourId);
                if (model != null && BranchID == new Guid("00000000-0000-0000-0000-000000000000"))
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, "Branch Already Exists.");
                }
                else
                {
                    model = new BranchModel();
                    model.Brancheid = BranchID;
                    model.BranchName = txtBranchName.Text;
                    model.Parlourid = ParlourId;
                    model.TelNumber = txtPhone.Text;
                    model.CellNumber = txtCellPhone.Text;
                    model.Address1 = txtline1.Text;
                    model.Address2 = txtline2.Text;
                    model.Address3 = txtline3.Text;
                    model.Address4 = txtline4.Text;
                    model.Code = txtpostalcode.Text;
                    model.BranchCode = txtBranchCode.Text;
                    model.Region = txtRegion.Text;
                    model.LastModified = System.DateTime.Now;
                    model.ModifiedUser = UserName;
                   

                    //================================================================ 
                    Guid retID = client.SaveBranchDetails(model);
                    BranchID = retID;

                    ShowMessage(ref lblMessage, MessageType.Success, "Branch Details successfully saved");
                    BindBranchesList();
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
        protected void gvBranches_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditBranch")
            {
                BranchID = new Guid(e.CommandArgument.ToString());
                try
                {
                    BindBranchToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            if (e.CommandName == "deleteBranch")
            {
                Guid SBranchId = new Guid(e.CommandArgument.ToString());
                try
                {
                    int retID = client.DeleteBranch(SBranchId);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    BindBranchesList();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
        }

        #endregion
    }
}