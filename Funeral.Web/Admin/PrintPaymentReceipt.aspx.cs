using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.UserControl;
using System;

namespace Funeral.Web.Admin
{
    public partial class PrintPaymentReceipt : AdminBasePage
    {
        #region Fields
        
        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 10;
        }
        #endregion

        #region PageProperty
        public int OtherInvoiceId
        {
            get
            {
                if (ViewState["OtherInvoiceId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["OtherInvoiceId"]);
            }
            set
            {
                ViewState["OtherInvoiceId"] = value;
            }
        }

        public int InvoiceId
        {
            get
            {
                if (ViewState["_receipt"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_receipt"]);
            }
            set
            {
                ViewState["_receipt"] = value;
            }
        }
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
        public int ReqType
        {
            get
            {
                if (ViewState["_type"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_type"]);
            }
            set
            {

                ViewState["_type"] = value;
            }
        }
        public string PolicyNum
        {
            get
            {
                if (ViewState["_policyNo"] == null)
                    return null;
                else
                    return (ViewState["_policyNo"]).ToString();
            }
            set
            {
                ViewState["_policyNo"] = value;
            }
        }
        #endregion

        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString.ToString()))
            {
                if (Request.QueryString["InID"] != null)
                {
                    string query = (Request.QueryString["InID"]).ToString();
                    string decryptedValue = EncryptionHelper.Decrypt(query);
                    string[] arry = decryptedValue.ToString().Split('&');
                    InvoiceId = Convert.ToInt32(arry[0]);
                    PolicyNum = arry[1].ToString();
                    MemberId = Convert.ToInt32(arry[2]);
                    ReqType = Convert.ToInt32(arry[3]);
                }
                else if (Request.QueryString["OInID"] != null)
                {
                    string query = (Request.QueryString["OInID"]).ToString();
                    string decryptedValue = EncryptionHelper.Decrypt(query);
                    string[] arry = decryptedValue.ToString().Split('&');
                    OtherInvoiceId = Convert.ToInt32(arry[0]);
                    PolicyNum = arry[1].ToString();
                    MemberId = Convert.ToInt32(arry[2]);
                    ReqType = Convert.ToInt32(arry[3]);
                }
            }

            else
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
            }
            if (!IsPostBack)
            {
                HideArea.Visible = false;
                if (OtherInvoiceId == 0)
                {
                    BindData();
                    PrintDuplicateOrNot();
                }
                else
                {
                    BindOtherInvoiceData();
                }

                if (ReqType == 2)
                {
                    ReceiptId.Visible = false;
                    lblReceiptId.Visible = false;
                    HideArea.Visible = true;
                    tableTitle.Visible = false;
                    BindAddress();
                }

            }
        }
        #endregion

        #region Function
        public void BindData()
        {
            MembersModel Mmodel = MembersBAL.GetMemberByID(MemberId, ParlourId);
            NewMemberInvoiceModel getmodel = MembersBAL.GetInvoiceByid(InvoiceId);
            //string[] MonthPaid = getmodel.Notes.Split('-');
            lblReceiptId.Text = InvoiceId.ToString();
            tableTitle.Text = ApplicationName;
            //lblBranchname.Text = Mmodel.MemberBranch.ToString();
            //lblreceipt.Text = getmodel.InvNumber.ToString();
            lblPolicy.Text = PolicyNum;
            lblDatePaid.Text = getmodel.DatePaid.ToString("dd-MMM-yyyy");
            lblAmtpaid.Text = Currency.Trim()+ " " + getmodel.AmountPaid.ToString("F");
            lblPolicyholder.Text = Mmodel.Surname + " " + Mmodel.FullNames;
            lblRecivedBy.Text = getmodel.RecievedBy;
            lblTimePrint.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm");
            //lblmethod.Text = getmodel.MethodOfPayment;
            lblMonthPaid.Text = getmodel.Notes.ToString();
            //lblMonthPaid.Text = getmodel.DatePaid.ToString("dd-MMM-yyyy");
            lblunderwriter.Text = "";
            ltrParlourname.Text = ApplicationName;
        }

        public void BindOtherInvoiceData()
        {
            OtherPaymentModel model = OtherPaymentBAL.OtherPaymentSelect(OtherInvoiceId, ParlourId);

            //string[] MonthPaid = getmodel.Notes.Split('-');
            tableTitle.Text = ApplicationName;
            //lblBranchname.Text = Mmodel.MemberBranch.ToString();
            //lblreceipt.Text = getmodel.InvNumber.ToString();
            lblReceiptNumber.Text = "Receipt Number : " + OtherInvoiceId.ToString();
            lblPolicy.Text = PolicyNum;
            lblDatePaid.Text = model.DatePaid.ToString("dd-MMM-yyyy");
            lblAmtpaid.Text = Currency.Trim() + " " + model.AmountPaid.ToString("F");
            lblPolicyholder.Text = string.Empty;
            lblRecivedBy.Text = model.RecievedBy;
            lblTimePrint.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm");
            //lblmethod.Text = getmodel.MethodOfPayment;
            lblMonthPaid.Text = model.Notes.ToString();
            //lblMonthPaid.Text = getmodel.DatePaid.ToString("dd-MMM-yyyy");
            lblunderwriter.Text = "";
            ltrParlourname.Text = ApplicationName;
        }
        public void BindAddress()
        {

            ApplicationSettingsModel model = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            lblReceiptNumber.Text = "Receipt Number : " + InvoiceId.ToString();
            lbladd1.Text = model.BusinessAddressLine1.ToString();
            lbladd2.Text = model.BusinessAddressLine2.ToString();
            lbladd3.Text = model.BusinessAddressLine3.ToString();
            lbladd4.Text = model.BusinessPostalCode.ToString();
            lblfpsnub.Text = "FPS Number: " + model.FSBNumber;
            lblTelCell.Text = model.ManageTelNumber.ToString() + " | " + model.ManageCellNumber.ToString();
        }

        private void PrintDuplicateOrNot()
        {
            ltrHead.Text = string.Empty;
            int counter = MemberPaymentBAL.GetPrintCounter(InvoiceId, this.ParlourId);
            if (counter > 0)
            {
                ltrHead.Text = "Duplicate";
            }
        }
        #endregion
    }
}