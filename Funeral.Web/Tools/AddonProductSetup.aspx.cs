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
    public partial class AddonProductSetup : AdminBasePage
    {
        #region Page Property
        public Guid ProductID
        {
            get
            {
                if (ViewState["_ProductID"] == null)
                    return new Guid("00000000-0000-0000-0000-000000000000");
                else
                    return new Guid(ViewState["_ProductID"].ToString());
            }
            set
            {
                ViewState["_ProductID"] = value;
            }
        }

        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 14;
        }
        #endregion

        #region Page load event
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            if (!Page.IsPostBack)
            {
                BindAddonProductList();
            }
            btnSubmite.Enabled = HasCreateRight;
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

        #region Private/Public function and methods

        public void BindAddonProductList()
        {
            List<AddonProductsModal> model = ToolsSetingBAL.GetAllAddonProductes(ParlourId);
            gvAddones.DataSource = model;
            gvAddones.DataBind();
        }
        public void BindAddonProductToUpdate()
        {
            AddonProductsModal model = ToolsSetingBAL.EditAddonProductbyID(ProductID, ParlourId);
            if (model == null)
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Branch.');</script>");
            }
            else
            {
                ProductID = model.pkiProductID;
                txtDescription.Text = model.ProductDesc;
                txtPremium.Text = model.ProductCost.ToString("#,0.00");
                txtCover.Text = model.ProductCover.ToString("#,0.00");
                chkongoing.Checked = false;
                if (model.IsProductOngoing == 1)
                    chkongoing.Checked = true;
                chkLaybye.Checked = false;
                if (model.IsProductLaybye == 1)
                    chkLaybye.Checked = true;
                txtAddonName.Text = model.ProductName;

                btnSubmite.Text = "Update";
            }
        }
        public void ClearControl()
        {
            btnSubmite.Text = "Save";
            ProductID = new Guid("00000000-0000-0000-0000-000000000000");
            lblMessage.Visible = false;
            
            txtDescription.Text = string.Empty;
            txtPremium.Text = string.Empty;
            txtCover.Text = string.Empty;
            chkongoing.Checked = false;
            chkLaybye.Checked = false;
            txtAddonName.Text = string.Empty; ;
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

                AddonProductsModal model;
                model = ToolsSetingBAL.GetAddonProductByID(txtAddonName.Text, ParlourId);
                if (model != null && ProductID == new Guid("00000000-0000-0000-0000-000000000000"))
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, "Addon Product Already Exists.");
                }
                else
                {
                    model = new AddonProductsModal();
                    model.pkiProductID = ProductID;
                    model.DateCreated = System.DateTime.Now;
                    model.UserID = UserID.ToString();
                    model.ProductDesc = txtDescription.Text;
                    model.ProductCost = Convert.ToDecimal(txtPremium.Text);
                    model.ProductCover = Convert.ToDecimal(txtCover.Text);
                    model.IsProductOngoing = 0;
                    if (chkongoing.Checked)
                        model.IsProductOngoing = 1;
                    model.IsProductLaybye = 0;
                    if (chkLaybye.Checked)
                        model.IsProductLaybye = 1;
                    model.Parlourid = ParlourId;
                    model.LastModified = System.DateTime.Now;
                    model.ModifiedUser = UserName;
                    model.ProductName = txtAddonName.Text;



                    //================================================================ 
                    Guid retID = ToolsSetingBAL.SaveAddonProductDetails(model);
                    ProductID = retID;

                    ShowMessage(ref lblMessage, MessageType.Success, "Addon Product Details successfully saved");
                    BindAddonProductList();
                    ClearControl();
                }
                lblMessage.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab512", "goToTab(5)", true);
            }
        }
        protected void gvAddones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditAddon")
            {
                string PkiGuid = e.CommandArgument.ToString();
                ProductID = new Guid(PkiGuid);
                try
                {
                    BindAddonProductToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            if (e.CommandName == "deleteAddon")
            {
                Guid SBranchId = new Guid(e.CommandArgument.ToString());
                try
                {
                    int retID = ToolsSetingBAL.DeleteAddonProduct(SBranchId);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    BindAddonProductList();
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