using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class TombstonePayment : AdminBasePage
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

        public int TombstoneId
        {
            get
            {
                if (Request.QueryString["TID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(Request.QueryString["TID"]);
            }
        }

        bool blnFuneralPayment = false;

        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 39;
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
                ucPaymenthistory.TombstoneId = this.TombstoneId;
                ucPaymenthistory.ParlourId = this.ParlourId;
                txtFuneralId.Text = TombstoneId.ToString();
                txtReceivedBy.Text = HttpContext.Current.User.Identity.Name;
                bindServiceListList();
                bindInvoices();
                btnPay.Enabled = HasCreateRight;
            }
        }
        #endregion

        #region Method
        private void bindInvoices()
        {
            ucPaymenthistory.TombstoneId = this.TombstoneId;
            ucPaymenthistory.ParlourId = this.ParlourId;
            ucPaymenthistory.bindInvoices(this.ParlourId);
        }

        public string FormatDateTime(string date)
        {
            return string.IsNullOrEmpty(date) ? string.Empty : Convert.ToDateTime(date).ToString("dd MMM yyyy");
        }
        public void PrintInvoice()
        {
            Response.Redirect("~/Admin/PrintForm.aspx?TBID=" + EncodeQueryString(TombstoneId.ToString()));
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
                if (string.IsNullOrEmpty(txtAmount.Text))
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
                    TombStonesPaymentModel tombstonePayment = new TombStonesPaymentModel();
                    //tombstonePayment.DatePaid = Convert.ToDateTime(txtNextPaymentDate.Text);
                    tombstonePayment.InvoiceID = 0;
                    tombstonePayment.TombstoneID = this.TombstoneId;
                    tombstonePayment.AmountPaid = Convert.ToDecimal(txtAmount.Text.Replace("R ", ""));
                    tombstonePayment.RecievedBy = txtReceivedBy.Text.ToString();
                    tombstonePayment.PaidBy = txtReceivedBy.Text.ToString();
                    tombstonePayment.MemberBranch = string.Empty;
                    tombstonePayment.Notes = txtMohthPaid.Text.ToString();
                    tombstonePayment.parlourid = ParlourId;
                    tombstonePayment.ModifiedUser = HttpContext.Current.User.Identity.Name;
                    int FuneralID = TombStonesPaymentBAL.AddInvoice(tombstonePayment);
                    if (FuneralID != 0)
                    {
                        Common.WebMsgBox.Show("Payment added successfully.");
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
                        TombStonesPaymentModel objTomstonePayemnt = new TombStonesPaymentModel();
                        //tombstonePayment.DatePaid = Convert.ToDateTime(txtNextPaymentDate.Text);
                        objTomstonePayemnt.TombstoneID = TombstoneId;
                        objTomstonePayemnt.AmountPaid = Convert.ToDecimal(txtAmount.Text.Replace("R ", ""));
                        objTomstonePayemnt.RecievedBy = txtReceivedBy.Text.ToString();
                        objTomstonePayemnt.Notes = txtMohthPaid.Text.ToString();
                        objTomstonePayemnt.PaidBy = txtReceivedBy.Text.ToString();
                        objTomstonePayemnt.MemberBranch = string.Empty;
                        objTomstonePayemnt.parlourid = ParlourId;
                        objTomstonePayemnt.ModifiedUser = HttpContext.Current.User.Identity.Name;
                        int FuneralID = TombStonesPaymentBAL.AddInvoice(objTomstonePayemnt);
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
            List<TombStoneServiceSelectModel> objServ = TombStoneBAL.SelectServiceByTombStoneID(TombstoneId).ToList();
            decimal Amt = 0;
            foreach (var item in objServ)
            {
                Amt = Amt + item.Amount;
            }

            TombStoneModel tombstoneModel = TombStoneBAL.SelectTombStoneByParlAndPki(TombstoneId, ParlourId);

            decimal TotalPayment = TombStonesPaymentBAL.TombStonesPaymentSelectByTombstoneID(this.ParlourId, TombstoneId).ToList().Sum(x => x.AmountPaid);


            txtAmount.Text = CalculateFinal(Amt, tombstoneModel.Tax, tombstoneModel.DisCount, TotalPayment);
            gvServiceList.DataSource = objServ;
            gvServiceList.DataBind();
        }

        protected void gvServiceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                try
                {
                    e.Row.Cells[4].Text = (e.Row.Cells[4] != null) ? ("R " + (Math.Round(Convert.ToDecimal(e.Row.Cells[4].Text), 2)).ToString()) : (string.Empty);
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
            Response.Redirect("~/Admin/TombStone.aspx?TID=" + TombstoneId);
        }

        public string CalculateFinal(Decimal sub, decimal Tax, decimal Dis, decimal totalPayment)
        {

            Decimal DisAmt = 0;
            Decimal TaxAmt = 0;
            Decimal ttl = 0;
            Decimal a = 0;
            TaxAmt = (((sub * Tax) / 100));
            a = (sub + TaxAmt);
            DisAmt = (((a * Dis) / 100));
            ttl = (a - DisAmt);
            return (ttl - totalPayment).ToString("N2");

        }
    }
}