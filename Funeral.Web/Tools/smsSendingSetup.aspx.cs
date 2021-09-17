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
using Funeral.DAL;
using System.Text.RegularExpressions;

namespace Funeral.Web.Tools
{
    public partial class smsSendingSetup : AdminBasePage
    {
        #region Page Property

        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 23;
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
            RequiredFieldValidator10.Enabled = true;
            RegularExpressionValidator4.Enabled = true;
            //BindTempletList();
            bindgvSmsPlaceholder();
        }

        #endregion

        #region Private/Public function and methods

        public void SendMassge(string ToNumber)
        {
            SendReminderModel smsModel = new SendReminderModel();
            smsModel.MemeberID = UserID.ToString();
            smsModel.MemberData = txtMessage.Text;
            smsModel.MemeberToNumber = Convert.ToInt64(ToNumber.Replace(" ", ""));
            smsModel.parlourid = ParlourId;
            int SendOpration = MemberPaymetsDAL.InsertSendReminder(smsModel);
        }

        public void SendBulkMessge()
        {
            SendReminderModel smsModel = new SendReminderModel();
            smsModel.MemeberID = UserID.ToString();
            smsModel.MemberData = txtMessage.Text;
            smsModel.parlourid = ParlourId;
            int SendOpration = MemberPaymetsDAL.SendingBulkSms(smsModel);
        }



        public void ClearControl()
        {
            txtCellphoneNumber.Text = string.Empty;
            txtMessage.Text = string.Empty;
            chkAllMember.Checked = false;
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
            RequiredFieldValidator10.Enabled = false;
            RegularExpressionValidator4.Enabled = false;
            if (chkAllMember.Checked)
            {
                SendBulkMessge();
                ShowMessage(ref lblMessage, MessageType.Success, "SMS Sent Successfully to all Members");
            }
            else
            {
                SendMassge(txtCellphoneNumber.Text);
                ShowMessage(ref lblMessage, MessageType.Success, txtCellphoneNumber.Text + " SMS Sent Successfully");
            }
            ClearControl();
            lblMessage.Visible = true;

        }

        //public void BindTempletList()
        //{
        //    List<smsTempletModel> ModelTemplet = ToolsSetingBAL.GetTemplateList(ParlourId);
        //    ddlTemplate.DataSource = ModelTemplet;
        //    ddlTemplate.DataValueField = "ID";
        //    ddlTemplate.DataTextField = "Name";
        //    ddlTemplate.DataBind();
        //    ddlTemplate.Items.Insert(0, new ListItem("Select Template", "0"));
        //}

        private void bindgvSmsPlaceholder()
        {
            List<SMSPlaceholderModel> placeholder = ToolsSetingBAL.GetPlaceholder();
            gvTemplate.DataSource = placeholder;
            gvTemplate.DataBind();
        }

        #endregion
    }
}