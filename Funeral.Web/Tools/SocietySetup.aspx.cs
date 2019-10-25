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
    public partial class SocietySetup : AdminBasePage
    {
        #region Page Property
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
        public int SocietyID
        {
            get
            {
                if (ViewState["_SocietyID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_SocietyID"]);
            }
            set
            {
                ViewState["_SocietyID"] = value;
            }
        }

        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 20;
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
                BindSocietyList();
                btnSubmite.Enabled = HasCreateRight;
            }
        }

        #endregion

        #region Private/Public function and methods

        public void BindSocietyList()
        {
            SocietyModel[] model = client.GetAllSocietyes(ParlourId);
            gvSocietyes.DataSource = model;
            gvSocietyes.DataBind();
        }
        public void BindSocietyToUpdate()
        {
            SocietyModel model = client.EditSocietybyID(SocietyID, ParlourId);
            if (model == null)
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Branch.');</script>");
            }
            else
            {
                SocietyID = model.pkiSocietyID;
                txtSocietyName.Text = model.SocietyName;

                btnSubmite.Text = "Update";
            }
        }
        public void ClearControl()
        {
            btnSubmite.Text = "Save";
            SocietyID = 0;
            txtSocietyName.Text = string.Empty;
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

                SocietyModel model;
                model = client.GetSocietyByID(txtSocietyName.Text, ParlourId);
                if (model != null && SocietyID == 0)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, "Society Already Exists.");
                }
                else
                {
                    model = new SocietyModel();
                    model.pkiSocietyID = SocietyID;
                    model.SocietyName = txtSocietyName.Text;
                    model.parlourid = ParlourId;
                    model.LastModified = System.DateTime.Now;
                    model.ModifiedUser = UserName;


                    //================================================================ 
                    int retID = client.SaveSocietyDetails(model);
                    SocietyID = retID;

                    ShowMessage(ref lblMessage, MessageType.Success, "Society Details successfully saved");
                    BindSocietyList();
                    ClearControl();
                }
                lblMessage.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab512", "goToTab(5)", true);
            }
        }
        protected void gvSocietyes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditSociety")
            {
                SocietyID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    BindSocietyToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            if (e.CommandName == "deleteSociety")
            {
                int SBranchId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    int retID = client.DeleteSociety(SBranchId);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    BindSocietyList();
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