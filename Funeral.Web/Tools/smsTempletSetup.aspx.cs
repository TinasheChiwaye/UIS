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
    public partial class smsTempletSetup : AdminBasePage
    {
        #region Page Property
        public int ID
        {
            get
            {
                if (ViewState["_ID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_ID"]);
            }
            set
            {
                ViewState["_ID"] = value;
            }
        }
        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 24;
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

                BindTempletList();
                bindgvSmsPlaceholder();
            }
        }

        #endregion

        #region Private/Public function and methods

        public void BindTempletList()
        {
            List<smsTempletModel> ModelTemplet = ToolsSetingBAL.GetTemplateList(ParlourId);
            ddlTemplate.DataSource = ModelTemplet;
            ddlTemplate.DataValueField = "ID";
            ddlTemplate.DataTextField = "Name";
            ddlTemplate.DataBind();
            ddlTemplate.Items.Insert(0, new ListItem("Select Template", "0"));
        }
        public void ClearControl()
        {
            ID = 0;
            txtMessage.Text = string.Empty;
            ddlTemplate.ClearSelection();
            ddlTemplate.Items.FindByValue("0").Selected = true;
            lblMessage.Visible = false;
        }
        #endregion

        #region Button Click Events
        protected void btnSubmite_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (ID > 0)
                    {
                        smsTempletModel _EmailTemplate = new smsTempletModel();
                        _EmailTemplate.ID = ID;
                        _EmailTemplate.Name = ddlTemplate.Text;
                        _EmailTemplate.smsText = txtMessage.Text;
                        _EmailTemplate.ModifiedUser = UserName;

                        int retID = ToolsSetingBAL.UpdatesmsTemplate(_EmailTemplate);

                        if (retID > 0)
                        {
                            ShowMessage(ref lblMessage, MessageType.Success, "Temlate Save Successfully.");
                            BindTempletList();
                            ClearControl();
                        }
                        else
                        {
                            ShowMessage(ref lblMessage, MessageType.Danger, "System facing Some issues to Save Temlate.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, "Error:" + ex.Message);
                }
                lblMessage.Visible = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ID = Convert.ToInt32(ddlTemplate.SelectedItem.Value);
            smsTempletModel ObjList = ToolsSetingBAL.GetEmailTemplateByID(ID,ParlourId);
            ID = 0;
            txtMessage.Text = string.Empty;
            if (ObjList != null)
            {
                ID = ObjList.ID;
                txtMessage.Text = ObjList.smsText;
            }
        }

        private void bindgvSmsPlaceholder()
        {
            List<SMSPlaceholderModel> placeholder = ToolsSetingBAL.GetPlaceholder();
            gvTemplate.DataSource = placeholder;
            gvTemplate.DataBind();
        }

        #endregion
    }
}