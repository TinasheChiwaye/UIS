using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.UserControl;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class FuneralPayment : AdminBasePage
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
        public bool BtnReversal
        {
            get
            {
                if (ViewState["_Reversal"] == null)
                    return false;
                else
                    return Convert.ToBoolean(ViewState["_Reversal"]);
            }
            set
            {

                ViewState["_Reversal"] = value;
            }
        }
        //Int16 iCnt = default(Int16);

        public int FuneralId
        {
            get
            {
                if (Request.QueryString["FID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(Request.QueryString["FID"]);
            }
        }

        bool blnFuneralPayment = false;

        private readonly FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 32;
        }
        #endregion

        #region PageInit
        protected void Page_Init(object sender, EventArgs e)
        {
            //btnPay.Visible = false;
            //SecureUserGroupsModel[] obj = client.EditSecurUserbyID(UserID);
            //List<int> list = new List<int>();
            ////list.Add(7);
            //list.Add(8);
            //list.Add(4);
            //list.Add(12);
            //var result = obj.Where(x => list.Contains(x.fkiSecureGroupID));
            //if (result.FirstOrDefault() != null)
            //{
            //    btnPay.Visible = true;
            //}
            //List<int> list2 = new List<int>();
            //list2.Add(7);
            //list2.Add(4);
            //list2.Add(12);
            //var result2 = obj.Where(x => list2.Contains(x.fkiSecureGroupID));
            //if (result2.FirstOrDefault() != null) { BtnReversal = true; btnPay.Visible = true; }
        }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            btnPrint.Visible = false;
            if (!Page.IsPostBack)
            {
                //txtNextPaymentDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                ucPaymenthistory.FuneralId = this.FuneralId;
                ucPaymenthistory.ParlourId = this.ParlourId;
                txtFuneralId.Text = FuneralId.ToString();
                txtReceivedBy.Text = HttpContext.Current.User.Identity.Name;
                bindServiceListList();
                bindInvoices();
            }
        }
        #endregion

        #region Method
        private void bindInvoices()
        {
            ucPaymenthistory.FuneralId = this.FuneralId;
            ucPaymenthistory.ParlourId = this.ParlourId;
            ucPaymenthistory.bindInvoices(this.ParlourId);
        }

        public string FormatDateTime(string date)
        {
            return string.IsNullOrEmpty(date) ? string.Empty : Convert.ToDateTime(date).ToString("dd MMM yyyy");
        }
        public void PrintInvoice()
        {
            Response.Redirect("~/Admin/PrintForm.aspx?FID=" + EncodeQueryString(FuneralId.ToString()));
        }
        public void PaymentRemindersms(MembersPaymentDetailsModel model)
        {
            if (model.pkiMemberID > 0)
            {
                //Member New Registration Welcome SMS Send 
                int SmsGrupId = Convert.ToInt32(SmsGroupType.Payment);
                smsSendingGroupModel modelSSG = client.GetsmsGroupbyID(SmsGrupId, ParlourId);
                if (modelSSG != null)
                {
                    StringBuilder strsb = new StringBuilder();
                    smsTempletModel _EmailTemplate = client.GetEmailTemplateByID(SmsGrupId, ParlourId);
                    if (_EmailTemplate != null)
                    {
                        MembersModel objMemberModel = client.GetMemberByID(model.pkiMemberID, ParlourId);

                        strsb = new StringBuilder(_EmailTemplate.smsText);
                        strsb = strsb.Replace("@Name", "<p>" + objMemberModel.FullNames + " " + objMemberModel.Surname + "</p>");
                        strsb = strsb.Replace("@DatePayment", "<p>" + model.PaymentDate + "</p>");
                        strsb = strsb.Replace("@NextDatePayment", "<p>" + model.Notes + "</p>");
                        strsb = strsb.Replace("@Paymentby", "<p>" + model.MethodOfPayment + "</p>");
                        string CellNo = (objMemberModel.Cellphone == string.Empty ? "0" : objMemberModel.Cellphone);
                        if (CellNo == "0")
                            CellNo = (objMemberModel.Telephone == string.Empty ? "0" : objMemberModel.Telephone);

                        SendReminderModel smsModel = new SendReminderModel();
                        smsModel.MemeberID = UserID.ToString();
                        smsModel.MemberData = strsb.ToString();
                        smsModel.MemeberToNumber = Convert.ToInt64(CellNo.Replace(" ", ""));
                        smsModel.parlourid = ParlourId;

                        int SendOpration = client.InsertSendReminder(smsModel);
                    }
                }
            }
        }
        public bool EnablePaymentControls(bool blnValue)
        {
            try
            {
                txtReceivedBy.Enabled = blnValue;
                txtAmount.Enabled = blnValue;
                txtMohthPaid.Enabled = blnValue;
                btnPay.Enabled = blnValue;

            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error:- " + ex.Message, MsgBoxStyle.Information);
            }
            return blnValue;
        }
        #endregion

        #region ButtonEvent

        protected void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 intMethod = 0;
                string strMethod = "";
                //wsSMS.API SMS = new wsSMS.API();           

                string strMsg = string.Empty;
                if (string.IsNullOrEmpty(txtAmount.Text) || Convert.ToDecimal(txtAmount.Text) <= 0)
                {
                    Common.WebMsgBox.Show("Enter positive amount value..");
                    txtAmount.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtReceivedBy.Text))
                {
                    Common.WebMsgBox.Show("Must enter received by.");
                    txtReceivedBy.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtMohthPaid.Text))
                {
                    Common.WebMsgBox.Show("Must enter month paid.");
                    txtMohthPaid.Focus();
                    return;
                }
                if (btnPay.Text != "Add Reversal")
                {
                    txtMohthPaid.Text = ddlMethod.SelectedItem.Text + " - " + txtMohthPaid.Text;
                }

                intMethod = Strings.InStr(txtMohthPaid.Text, "Cash", CompareMethod.Text);
                strMethod = ddlMethod.SelectedItem.Text;


                if (btnPay.Text == "Add Payment")
                {
                    FuneralPaymentsModel funeralPayment = new FuneralPaymentsModel();
                    //funeralPayment.DatePaid = Convert.ToDateTime(txtNextPaymentDate.Text);
                    funeralPayment.FuneralID = FuneralId;
                    funeralPayment.AmountPaid = Convert.ToDecimal(txtAmount.Text.Replace(Currency.Trim()+" ", ""));
                    funeralPayment.RecievedBy = txtReceivedBy.Text.ToString();
                    funeralPayment.Notes = txtMohthPaid.Text.ToString();
                    funeralPayment.ParlourId = ParlourId;
                    funeralPayment.UserName = HttpContext.Current.User.Identity.Name;
                    int FuneralID = client.AddFuneralPayments(funeralPayment);
                    if (FuneralID != 0)
                    {
                        Common.WebMsgBox.Show("Payment added successfully.");
                        //ReadFuneralInvoiceIntoDatatable - Remain.

                        btnPrint.Enabled = true;
                    }
                    else
                    {
                        Common.WebMsgBox.Show("Payment failes to save.");
                    }
                }
                else
                {
                    //CheckUserAccess - Remain.
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab5", "confirm('You are about to make a REVERSAL PAYMENT. Continue?')", true);
                    if (Interaction.MsgBox("You are about to make a REVERSAL PAYMENT. Continue?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
                    {
                        FuneralPaymentsModel objFunerals = new FuneralPaymentsModel();
                        //funeralPayment.DatePaid = Convert.ToDateTime(txtNextPaymentDate.Text);
                        objFunerals.FuneralID = FuneralId;
                        objFunerals.AmountPaid = Convert.ToDecimal(txtAmount.Text.Replace(Currency.Trim()+" ", ""));
                        objFunerals.RecievedBy = txtReceivedBy.Text.ToString();
                        objFunerals.Notes = txtMohthPaid.Text.ToString();
                        objFunerals.ParlourId = ParlourId;
                        objFunerals.UserName = HttpContext.Current.User.Identity.Name;
                        int FuneralID = client.AddFuneralPayments(objFunerals);
                    }
                    else
                    {
                        Interaction.MsgBox("Reversal not added successfully", MsgBoxStyle.Information);
                    }
                }

                bindInvoices();
                txtAmount.Text = "";
                hdnAmount.Value = "";
                txtMohthPaid.Text = "";

                ddlMethod.Enabled = true;



            }
            catch (Exception ex)
            {
                Common.WebMsgBox.Show(ex.Message);
            }
        }

        #endregion

        #region Other
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            PrintInvoice();
        }

        public void bindServiceListList()
        {
            List<FuneralServiceSelectModel> objServ = client.SelectServiceByFuneralID(FuneralId).ToList();
            decimal Amt = 0;
            decimal tax = 0;
            foreach (var item in objServ)
            {
                Amt = Amt + item.Amount;
            }

            FuneralModel objQuotation = client.SelectFuneralBypkid(FuneralId, ParlourId);

            decimal TotalPayment = client.ReturnFuneralPayments(this.ParlourId, FuneralId.ToString()).ToList().Sum(x => x.AmountPaid);

            if (objQuotation != null && objQuotation.Tax != null)
            {
                tax = objQuotation.Tax;
            }


            txtAmount.Text = CalculateFinal(Amt, tax, objQuotation.Discount, TotalPayment);

            objServ = client.SelectServiceByFuneralID(FuneralId).ToList();
            //SubTotal.Text = Amt.ToString();
            gvServiceList.DataSource = objServ;
            gvServiceList.DataBind();
        }

        protected void gvServiceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                try
                {
                    e.Row.Cells[4].Text = (e.Row.Cells[4] != null) ? (Currency.Trim()+" " + (Math.Round(Convert.ToDecimal(e.Row.Cells[4].Text), 2)).ToString()) : (string.Empty);
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                }
                catch
                {

                }
            }
        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Funeral.aspx?FID=" + FuneralId);
        }

        public string CalculateFinal(Decimal sub, decimal Tax, decimal Dis, decimal totalPaid)
        {

            Decimal DisAmt = 0;
            Decimal TaxAmt = 0;
            Decimal ttl = 0;
            Decimal a = 0;
            TaxAmt = (((sub * Tax) / 100));
            a = (sub + TaxAmt);
            DisAmt = (((a * Dis) / 100));
            ttl = (a - DisAmt);
            return (ttl - totalPaid).ToString("N2");

        }
    }
}