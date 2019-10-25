using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Tools
{
    public partial class CustomManagement : AdminBasePage
    {
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 28;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCustomType();
                BindAllData();
                btnSubmit.Enabled = HasCreateRight;
            }
        }

        #region event and methods
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                CustomDetails model = new CustomDetails();
                model.CustomType = (CustomDetailsEnums.CustomDetailsType)Convert.ToInt32(ddlCustomType.SelectedValue);
                model.Description = txtDescription.Text;
                model.Name = txtName.Text.Trim();
                model.ParlourId = this.ParlourId;
                model.CreatedBy = this.UserID;
                int Id = 0;
                if (string.IsNullOrEmpty(hdnId.Value))
                {                    
                    Id = client.SaveCustomDetails(model);
                    ClearControl();
                    this.ShowMessage(ref lblMessage, MessageType.Success, "Custom detail saved successfully");
                }
                else
                {
                    model.Id = Convert.ToInt32(hdnId.Value);
                    client.UpdateCustomDetails(model);
                    this.ShowMessage(ref lblMessage, MessageType.Success, "Custom detail udpated successfully");
                }

                BindAllData();
            }
        }

        private void ClearControl()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            hdnId.Value = string.Empty;
        }
        #endregion

        #region Private function & Methods
        private void BindAllData()
        {            
            gvCustom.DataSource = client.GetAllCustomDetailsByParlourId(this.ParlourId, Convert.ToInt32(ddlCustomType.SelectedValue));
            gvCustom.DataBind();
        }

        private void BindCustomType()
        {
            ddlCustomType.DataSource = EnumatorExtention.GetAll<CustomDetailsEnums.CustomDetailsType>();
            ddlCustomType.DataTextField = "Value";
            ddlCustomType.DataValueField = "Key";
            ddlCustomType.DataBind();
        }

        protected void ddlCustomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAllData();
        }

        private void BindAllDataToControl(int Id)
        {
            CustomDetails model = client.GetCustomDetails(this.ParlourId, Id, Convert.ToInt32(ddlCustomType.SelectedValue));
            if (model != null)
            {
                hdnId.Value = model.Id.ToString();
                txtDescription.Text = model.Description;
                txtName.Text = model.Name;
            }
        }
        #endregion

        protected void gvCustom_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string EventType = e.CommandName;
            switch (EventType)
            {
                case "EditCustom":
                    BindAllDataToControl(Convert.ToInt32(e.CommandArgument));
                    break;
                case "DeleteCustom":
                    CustomDetails model = new CustomDetails();
                    model.Id = Convert.ToInt32(e.CommandArgument);
                    model.ParlourId = this.ParlourId;
                    model.CustomType = (CustomDetailsEnums.CustomDetailsType)Convert.ToInt32(ddlCustomType.SelectedValue);
                    client.DeleteCustomDetails(model);
                    model = null;
                    BindAllData();
                    break;
                default:
                    break;
            }
        }

    }
}