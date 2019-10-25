using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Services;
using Funeral.BAL;
using System.Data.SqlClient;
using System.IO;
using Microsoft.VisualBasic;
using System.Web.Security;
using Funeral.Web.UserControl;
namespace Funeral.Web.Admin
{
    public partial class OtherPayment : AdminBasePage
    {  
        #region Fields
        public int MemberId
        {
            get
            {
                if (ViewState["_memberId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_memberId"]);
            }
            set
            {

                ViewState["_memberId"] = value;
            }
        }
        public int InvoiceID
        {
            get
            {
                if (ViewState["_InvoiceID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_InvoiceID"]);
            }
            set
            {

                ViewState["_InvoiceID"] = value;
            }
        }
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 30;
        }
        #endregion

        #region PageLoad
        
        protected void Page_Load(object sender, EventArgs e)
        {
             
            if (!Page.IsPostBack)
            {
              
                if (Request.QueryString["ID"] != null)
                {
                    BindCustomDetails();
                    bindMemberPlanDetailsWithBalance();
                    bindOtherPayment(Convert.ToInt32(Request.QueryString["ID"].ToString()), new Guid(Request.QueryString["ParlourId"]));

                }
            }
            btnPay.Enabled = HasCreateRight;
        }
        #endregion

        #region Method
        public void bindMemberPlanDetailsWithBalance()
        {
            MembersModel model = client.GetMemberByID(Convert.ToInt32(Request.QueryString["ID"].ToString()), new Guid(Request.QueryString["ParlourId"]));
            if ((model == null))
                return;

            txtNextPaymentDate.Text = FormatDateTime(DateTime.Now.ToString());
            txtReceivedBy.Text = HttpContext.Current.User.Identity.Name;
            txtMember.Text = model.FullNames;
            txtMemberNo.Text = model.MemeberNumber;


        }

        public void bindOtherPayment(int MemberId, Guid ParlourId)
        {
            gvOtherPayment.DataSource = client.OtherPaymentSelectByMemberId(MemberId, ParlourId).ToList();
            gvOtherPayment.DataBind();

        }

        public void PrintInvoice(int pkiInvoiceID, int type, int MemberId)
        {
            string encryptedValue = EncryptionHelper.Encrypt(pkiInvoiceID.ToString() + "&" + txtMemberNo.Text + "&" + MemberId + "&" + type);
            Response.Redirect("~/Admin/PrintPaymentReceipt.aspx?OInID=" + encryptedValue);
        }
        private void ClearControl()
        {
            txtnotes.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtNextPaymentDate.Text = string.Empty;
            ddlMethod.SelectedValue = "0";
            ddlPaymentType.SelectedValue = "0";
            InvoiceID = 0;
        }
        #endregion

        #region ButtonEvent
        protected void btnPay_click(object sender, EventArgs e)
        {
            OtherPaymentModel model = new OtherPaymentModel();
            model.MemberID = Convert.ToInt32(Request.QueryString["Id"]);
            model.RecievedBy = txtReceivedBy.Text;
            model.AmountPaid = Convert.ToDecimal(txtAmount.Text);
            model.MethodOfPayment = ddlMethod.SelectedItem.Value;
            model.DatePaid = Convert.ToDateTime(txtNextPaymentDate.Text);
            model.PaymentTypeId = Convert.ToInt32(ddlPaymentType.SelectedItem.Value);
            model.Notes = txtnotes.Text;       
            model.Parlourid = new Guid(Request.QueryString["ParlourId"]);
            model.ModifiedUser = this.UserName;
            int InvoiceID = client.OtherPaymentsSave(model);

            if (InvoiceID > 0)
            {
                ShowMessage(ref lblMessage, MessageType.Success, "Payment  added successfully.");
                bindOtherPayment(Convert.ToInt32(model.MemberID), new Guid(Request.QueryString["ParlourId"]));
                ClearControl();
            }
            else
            {
                ShowMessage(ref lblMessage, MessageType.Warning, "Payment  Is Not Add please Try Again.");
            
            }
        }

        protected void gvInvoices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int pkiInvoiceID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "PrintPremium")
            {
                try
                {
                    PrintInvoice(pkiInvoiceID, 1, MemberId);
                }
                catch (Exception ex)
                {
                    this.ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                }
            }
            else if (e.CommandName == "PrintFullPremium")
            {
                try
                {
                    PrintInvoice(pkiInvoiceID, 2, MemberId);
                }
                catch (Exception ex)
                {
                    this.ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                }
            }

            else if (e.CommandName == "EditInvoice")
            {
                try
                {
                    BindEditOtherPayment(pkiInvoiceID, ParlourId);
                    bindOtherPayment(Convert.ToInt32(Request.QueryString["ID"].ToString()), new Guid(Request.QueryString["ParlourId"]));
                }
                catch (Exception ex)
                {
                    this.ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                }
            }
        }

        protected void gvInvoices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Guid ParlourId = new Guid(Request.QueryString["ParlourId"]);
            int MemberID = Convert.ToInt32(Request.QueryString["MemberId"]);

            gvOtherPayment.PageIndex = e.NewPageIndex;
            bindOtherPayment(InvoiceID, ParlourId);

        }

        private void BindCustomDetails()
        {
            var custom1 = client.GetAllCustomDetailsByParlourId(this.ParlourId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.PaymentType));
            ddlPaymentType.DataSource = custom1;
            ddlPaymentType.DataTextField = "Name";
            ddlPaymentType.DataValueField = "Id";
            ddlPaymentType.DataBind();
            ddlPaymentType.Items.Insert(0, new ListItem("Select Payment Type", "0"));
        }
        #endregion
        public void BindEditOtherPayment(int InvoiceID, Guid ParlourId)
        {
            if (InvoiceID > 0)
            {
                OtherPaymentModel model = client.GetOtherPayment(InvoiceID, ParlourId);
                InvoiceID = model.pkiInvoiceID;
                txtNextPaymentDate.Text = Convert.ToString(model.DatePaid);
                txtAmount.Text = Convert.ToString(model.AmountPaid);
                ddlMethod.SelectedValue = model.MethodOfPayment;
                ddlPaymentType.SelectedValue = Convert.ToString(model.PaymentTypeId);
                txtnotes.Text = model.Notes;
                ParlourId = model.Parlourid;
            }

        }
    }
}