using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;

namespace Funeral.Web.Tools
{
    public partial class AddEditMenu : AdminBasePage
    {
        #region Fields
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindParentRoleForm();
                if (Request.QueryString["menuid"] != null)
                {
                    int Id = Convert.ToInt32(Request.QueryString["menuid"]);
                    BindDataForUpdate(Id);
                }
            }
        }
        private void BindParentRoleForm()
        {
            try
            {
                List<RightsModel> Model = RightsBAL.tblRightGetAll();
                ddlParentRole.DataSource = Model;
                ddlParentRole.DataTextField = "MenuName";
                ddlParentRole.DataValueField = "ID";
                ddlParentRole.DataBind();
                ddlParentRole.Items.Insert(0, new ListItem() { 
                Text="Select Parent Role",Value="0"
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void btnSaveMenu_click(object sender, EventArgs e)
        {
            try
            {
                RightsModel RightsModels = new RightsModel();
                if (hdfID.Value == "" || hdfID.Value == "0" || hdfID.Value == null)
                {
                    RightsModels.ID = 0;
                    RightsModels.CreateBy = UserName;
                    RightsModels.CreatedDate = DateTime.Now;
                }
                else
                {
                    RightsModels.ID = Convert.ToInt32(hdfID.Value);
                    RightsModels.UpdateBy = UserName;
                    RightsModels.UpdateDate = DateTime.Now;
                }
                RightsModels.ApplicationID = Convert.ToInt32(ddlapplication.SelectedValue);
                RightsModels.InMenu = chkInmenu.Checked;
                RightsModels.MenuName = txtMenuName.Text;
                RightsModels.MenuURL = txtMenuURL.Text;
                RightsModels.MenuLevel = Convert.ToInt32(ddlmenulevel.SelectedValue);
                RightsModels.ParentRightId = ddlParentRole.SelectedValue.ToString();
                RightsModels.IsMenu = chkIsMenu.Checked;
                RightsModels.IsOthers = chkIsOthers.Checked;
                RightsModels.MenuOrder = Convert.ToInt32(txtMenuOrder.Text);
                RightsModels.IsForm = chkIsForm.Checked;
                RightsModels.FormName = txtFormName.Text;
                RightsModels.Formkey = txtFormkey.Text;
                RightsModels.IconClassName = txtIconClassName.Text;
                RightsModels.IsRead = chkIsRead.Checked;
                RightsModels.IsCreate = chkIsCreate.Checked;
                RightsModels.IsUpdate = chkIsUpdate.Checked;
                RightsModels.IsDelete = chkIsDelete.Checked;
                RightsModels.Description = txtDescription.Text;
                RightsModels.RoleId = ddlParentRole.SelectedValue;

                if (RightsModels.ID == 0)
                {
                    int a = RightsBAL.SavetblRight(RightsModels);
                    ShowMessage(ref lblMessage, MessageType.Success, "Menu Successfully Saved.");
                }
                else
                {
                    int a = RightsBAL.SavetblRight(RightsModels);
                    ShowMessage(ref lblMessage, MessageType.Success, "Menu Successfully Updated.");
                }
                BindParentRoleForm();
                Clearcontrol();
               
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }
        }
        public void Clearcontrol()
        {
            ddlapplication.SelectedValue = "0";
            chkInmenu.Checked = false;
            txtMenuName.Text = string.Empty;
            txtMenuURL.Text = string.Empty;
            ddlmenulevel.SelectedValue = "0";
            ddlParentRole.SelectedValue = "0";
            chkIsMenu.Checked = false;
            chkIsOthers.Checked = false;
            txtMenuOrder.Text = "0";
            chkIsForm.Checked = false;
            txtFormName.Text = string.Empty;
            txtFormkey.Text = string.Empty;
            txtIconClassName.Text = string.Empty;
            chkIsRead.Checked = false;
            chkIsCreate.Checked = false;
            chkIsUpdate.Checked = false;
            chkIsDelete.Checked = false;
            txtDescription.Text = string.Empty;
            hdfID.Value = "0";
        }
        public void BindDataForUpdate(int id)
        {
            RightsModel RightsModels = RightsBAL.SelecttblRightById(id);
            ddlapplication.SelectedValue = RightsModels.ApplicationID.ToString();
            chkInmenu.Checked = RightsModels.InMenu;
            txtMenuName.Text = RightsModels.MenuName;
            txtMenuURL.Text = RightsModels.MenuURL;
            ddlmenulevel.SelectedValue = RightsModels.MenuLevel.ToString();
            ddlParentRole.SelectedValue = RightsModels.RoleId.ToString();
            chkIsMenu.Checked = RightsModels.IsMenu;
            chkIsOthers.Checked = RightsModels.IsOthers;
            txtMenuOrder.Text = RightsModels.MenuOrder.ToString();
            chkIsForm.Checked = RightsModels.IsForm;
            txtFormName.Text = RightsModels.FormName;
            txtFormkey.Text = RightsModels.Formkey;
            txtIconClassName.Text = RightsModels.IconClassName;
            chkIsRead.Checked = RightsModels.IsRead;
            chkIsCreate.Checked = RightsModels.IsCreate;
            chkIsUpdate.Checked = RightsModels.IsUpdate;
            chkIsDelete.Checked = RightsModels.IsDelete;
            txtDescription.Text = RightsModels.Description;
            hdfID.Value = RightsModels.ID.ToString();
        }
    }
}