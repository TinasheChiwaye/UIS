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
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Configuration;
using System.ServiceModel;
using Funeral.Web.FuneralServiceReference;
using Funeral.Web.Common;
using System.Globalization;
using System.Net.Mail;

namespace Funeral.Web.Admin
{
    public partial class ManageMembersPayment : AdminBasePage
    {
        private readonly ISiteSettings _siteConfig = new SiteSettings();

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
                if (ViewState["FuneralId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["FuneralId"]);
            }
            set
            {

                ViewState["FuneralId"] = value;
            }
        }

        bool blnFuneralPayment = false;

        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 34;
        }
        #endregion

        //#region PageInit
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //     btnPay.Visible = false;
        //    SecureUserGroupsModel[] obj = client.EditSecurUserbyID(UserID);
        //    List<int> list = new List<int>();
        //    //list.Add(7);
        //    list.Add(8);
        //    list.Add(4);
        //    list.Add(12);
        //    var result = obj.Where(x => list.Contains(x.fkiSecureGroupID));
        //    if (result.FirstOrDefault() != null)
        //    {
        //        btnPay.Visible = true;               
        //    }
        //    List<int> list2 = new List<int>();
        //    list2.Add(7);            
        //    list2.Add(4);
        //    list2.Add(12);
        //    var result2 = obj.Where(x => list2.Contains(x.fkiSecureGroupID));
        //    if (result2.FirstOrDefault() != null) { BtnReversal = true; btnPay.Visible = true; }
        //}
        //#endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            btnPrint.Visible = false;
            if (!Page.IsPostBack)
            {
                //if (IsSuperUser == true)
                //{
                //    txtMohthPaid.ReadOnly = false;
                //}
                LoadUserRights();
                if (Request.QueryString["ID"] != null)
                {
                    bindMemberPlanDetailsWithBalance();
                    bool IsJoiningFee = false;
                    IsJoiningFeePaid(out IsJoiningFee);
                }

            }
        }
        #endregion

        #region Method
        public void LoadUserRights()
        {
            btnPay.Enabled = this.HasCreateRight;
            BtnReversal = this.HasReversalPayment;
        }
        public void bindMemberPlanDetailsWithBalance()
        {
            Guid ParlourId = new Guid(Request.QueryString["ParlourId"]);
            MembersPaymentDetailsModel model = MemberPaymentBAL.ReturnMemberPlanDetailsWithBalance(Request.QueryString["ID"].ToString(), new Guid(Request.QueryString["ParlourId"]));
            if ((model == null))
            {

            }
            else
            {
                hdnParlourid.Value = model.ParlourId.ToString();
                txtNextPaymentDate.Text = FormatDateTime(DateTime.Now.ToString());
                txtReceivedBy.Text = HttpContext.Current.User.Identity.Name;
                txtMember.Text = model.FullNames;
                txtMemberNo.Text = model.MemeberNumber;
                txtPlan.Text = model.PlanName;
                txtPremium.Text = model.PlanSubscription;
                txtSpouse.Text = model.Spouse.ToString();
                txtChildren.Text = model.Children.ToString();
                txtAdults.Text = model.Adults.ToString();
                txtWaitingPeriod.Text = model.WaitingPeriod.ToString();
                txtCover.Text = model.Cover;
                txtPolicyLaps.Text = model.PolicyLaps.ToString();
                txtMemberStauts.Text = model.PolicyStatus;
                hdnOldStatus.Value = model.PolicyStatus;
                if (model.PolicyStatus == "1" || model.PolicyStatus == "Active")
                    txtMemberStauts.Text = "Active";
                if (model.PolicyStatus == "0" || model.PolicyStatus == "waiting")
                    txtMemberStauts.Text = "waiting";
                //txtJoiningFee.Text = model.JoiningFee.ToString("0.00");
                txtJoiningFee.Text = model.JoiningFee;
                //txtNextPaymentDate.Text = model.NextPaymentDate.ToString();
                txtPolicyBalance.Text = model.Balance.ToString(Currency.Trim() + " 0.00");
                txtMonthOwing.Text = model.MonthOwing.ToString();
                txtLatePenalty.Text = model.LatePaymentPenalty.ToString(Currency.Trim() + " 0.00");

                txtMohthPaid.Text = model.NextPaymentDate.ToString("MMM-yyyy");
                hdnLastpaymentDate.Value = model.NextPaymentDate.ToString();
                ViewState["_memberId"] = Convert.ToInt32(model.pkiMemberID);
                ViewState["NextPaymentDate"] = model.NextPaymentDate.ToString("MMM-yyyy");

                txtTotalPremium.Text = Convert.ToDouble(MembersBAL.SumOfPremium(Convert.ToInt32(model.pkiMemberID), new Guid(Request.QueryString["ParlourId"]))).ToString(Currency.Trim() + " 0.00");



                if (txtMemberStauts.Text == "Trial" || txtMemberStauts.Text == "Active" || txtMemberStauts.Text == "1")
                {
                    btnReunstate.Enabled = false;
                    btnRejoin.Enabled = false;
                }
                else
                {
                    btnReunstate.Enabled = true;
                    btnRejoin.Enabled = true;
                }

                bindInvoices(new Guid(Request.QueryString["ParlourId"]), Convert.ToInt32(model.pkiMemberID));

            }
        }

        public void bindInvoices(Guid ParlourId, int MemberId)
        {
            StringBuilder sb = new StringBuilder();
            List<MemberInvoiceModel> objMemberInvoiceModel = MembersBAL.GetInvoicesByMemberID(ParlourId, MemberId);
            gvInvoices.DataSource = objMemberInvoiceModel;
            gvInvoices.DataBind();
        }
        public string FormatDateTime(string date)
        {
            return string.IsNullOrEmpty(date) ? string.Empty : Convert.ToDateTime(date).ToString("dd MMM yyyy");
        }
        public void PrintInvoice(int InvoiceId, int type)
        {
            string id = InvoiceId.ToString();
            string Policyno = txtMemberNo.Text;
            string encryptedValue = EncryptionHelper.Encrypt(id + "&" + Policyno + "&" + MemberId + "&" + type);
            Response.Redirect("~/Admin/PrintPaymentReceipt.aspx?InID=" + encryptedValue);
        }
        public void PaymentRemindersms(MembersPaymentDetailsModel model)
        {
            if (model.pkiMemberID > 0)
            {
                //Member New Registration Welcome SMS Send 
                int SmsGrupId = Convert.ToInt32(SmsGroupType.Payment);
                smsSendingGroupModel modelSSG = ToolsSetingBAL.GetsmsGroupbyID(SmsGrupId, ParlourId);
                if (modelSSG != null)
                {
                    StringBuilder strsb = new StringBuilder();
                    smsTempletModel _EmailTemplate = ToolsSetingBAL.GetEmailTemplateByID(SmsGrupId, ParlourId);
                    if (_EmailTemplate != null)
                    {
                        MembersModel objMemberModel = MembersBAL.GetMemberByID(model.pkiMemberID, ParlourId);

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

                        int SendOpration = MemberPaymentBAL.InsertSendReminder(smsModel);
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
                //txtMohthPaid.Enabled = blnValue;
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
        protected void btnRejoin_Click(object sender, EventArgs e)
        {
            try
            {
                string txtMonthFrom = null;
                string strMonths = null;
                string strRejoin = null;
                //txtMohthPaid.Enabled = false;
                txtMonthFrom = ViewState["NextPaymentDate"].ToString();

                for (int iCnt = 1; iCnt <= Convert.ToInt32(txtMonthOwing.Text) - 1; iCnt++)
                {
                    strMonths = DateTime.Now.AddMonths(iCnt).ToString("MMM-yyyy");

                }
                txtMohthPaid.Text = "Rejoin Policy and start trial period because policy Lapsed: " + txtMonthFrom + " to " + strMonths;
                ddlNoOfMonths.SelectedIndex = 0;
            }
            catch
            {

            }
        }

        protected void btnReunstate_Click(object sender, EventArgs e)
        {
            try
            {
                string txtMonthFrom = null;
                string strMonths = null;
                txtAmount.Enabled = false;
                ddlNoOfMonths.Enabled = false;
                rfvNoOfMonth.Enabled = false;
                //txtMohthPaid.Enabled = false;
                txtAmount.Text = txtPolicyBalance.Text;
                hdnAmount.Value = txtPolicyBalance.Text;
                txtMonthFrom = ViewState["NextPaymentDate"].ToString();
                for (int iCnt = 1; iCnt <= Convert.ToInt32(txtMonthOwing.Text) - 1; iCnt++)
                { strMonths = DateTime.Now.AddMonths(iCnt).ToString("MMM-yyyy"); }

                ddlNoOfMonths.SelectedValue = txtMonthOwing.Text.Replace("-", "");
                txtMohthPaid.Text = "Reinstate Policy and PAYMENT OF balance owing from: " + txtMonthFrom + " to " + strMonths;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error:- " + ex.Message, MsgBoxStyle.Information);
            }
        }
        protected void btnPay_Click(object sender, EventArgs e)
        {
            try
            {

                Int32 intMethod = 0;
                string strMethod = "";
                //wsSMS.API SMS = new wsSMS.API();           

                string strMsg = string.Empty;
                if (string.IsNullOrEmpty(txtReceivedBy.Text))
                {
                    Common.WebMsgBox.Show("Incorrect data provided");
                    txtReceivedBy.Focus();
                    return;
                }
                if (txtMemberStauts.Text == "Lapsed" & !txtMohthPaid.Text.Contains("Reinstate Policy") & !txtMohthPaid.Text.Contains("Rejoin Policy"))
                {
                    Common.WebMsgBox.Show("This policy has Lapsed, please take the corrective action.");
                    return;
                }
                if (ddlNoOfMonths.SelectedIndex == -1 & !blnFuneralPayment & btnPay.Text != "Add Reversal" & !txtMohthPaid.Text.Contains("Joining Fee") & !txtMohthPaid.Text.Contains("Reinstate Policy") & !txtMohthPaid.Text.Contains("Rejoin Policy"))
                {
                    Common.WebMsgBox.Show("Select Number of Months");
                    ddlNoOfMonths.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtMohthPaid.Text))
                {
                    Common.WebMsgBox.Show("Incorrect data provided");
                    txtMohthPaid.Focus();
                    return;
                }
                if (btnPay.Text != "Add Reversal")
                {
                    txtMohthPaid.Text = ddlMethod.SelectedItem.Text + " - " + txtMohthPaid.Text;
                }

                intMethod = Strings.InStr(txtMohthPaid.Text, "Cash", CompareMethod.Text);

                strMethod = ddlMethod.SelectedItem.Text;
                //Remain
                //if (txtMohthPaid.Text.Contains("Reinstate Policy") | txtMohthPaid.Text.Contains("Rejoin Policy"))
                //{
                //    //SupervisorLogin frmSupervisorLogin = default(SupervisorLogin);

                //    //blnAuthenticated = false;
                //    //frmSupervisorLogin = new SupervisorLogin();
                //    //frmSupervisorLogin.ShowDialog();

                //    //if (!blnAuthenticated)
                //    //{
                //    //    break; // TODO: might not be correct. Was : Exit Try
                //    //}
                //}
                Guid ParlID = new Guid(Request.QueryString["ParlourId"]);
                MembersModel objmember = MembersBAL.GetMemberByID(MemberId, ParlID);

                if (!blnFuneralPayment)
                {
                    if (btnPay.Text != "Add Reversal" & !string.IsNullOrEmpty(txtTotalPremium.Text) & !txtMohthPaid.Text.Contains("Joining Fee"))
                    {
                        if (Convert.ToDouble(hdnAmount.Value.Replace(Currency.Trim() + " ", "")) < Convert.ToDouble(txtTotalPremium.Text.Replace(Currency.Trim() + " ", "")))
                        {
                            ShowMessage(ref lblMessage, MessageType.Danger, "Payment cannot be less than premium amount.");
                            Common.WebMsgBox.Show("Payment cannot be less than premium amount.");
                            return;

                        }
                    }
                    if (btnPay.Text == "Add Payment")
                    {
                       
                        MembersPaymentDetailsModel objPayment = new MembersPaymentDetailsModel();
                        objPayment.pkiMemberID = MemberId;
                        objPayment.Amount = Convert.ToDecimal(hdnAmount.Value.Replace(Currency.Trim() + " ", ""));
                        objPayment.LatePaymentPenalty = Convert.ToDecimal(txtLatePenalty.Text.Replace(Currency.Trim() + " ", ""));
                        objPayment.ReceivedBy = txtReceivedBy.Text.ToString();
                        objPayment.Notes = txtMohthPaid.Text.ToString();
                        objPayment.MethodOfPayment = strMethod;
                        objPayment.MemeberNumber = txtMemberNo.Text.ToString();
                        objPayment.PaymentDate = Convert.ToDateTime(txtNextPaymentDate.Text);
                        objPayment.NextPaymentDate = Convert.ToDateTime(hdnLastpaymentDate.Value).AddMonths(Convert.ToInt32(ddlNoOfMonths.SelectedValue));
                        objPayment.Branch = objmember.MemberBranch;
                        objPayment.ParlourId = ParlID;
                        MembersBAL.UpdateMemberStatus(ParlID, MemberId, "Active");
                        string s = objPayment.PolicyStatus;
                      //  objPayment.PolicyStatus = "Active";
                        bool IsJoiningFee = false;
                        IsJoiningFeePaid(out IsJoiningFee);
                        int PaymentID = MemberPaymentBAL.AddPayments(objPayment, IsJoiningFee);
                        if (PaymentID != 0)
                        {
                            Common.WebMsgBox.Show("Payment not added successfully.");
                        }
                        else
                        {
                            Common.WebMsgBox.Show("Payment added successfully.");
                            PrintPayment(objPayment);
                            PaymentRemindersms(objPayment);
                            bindMemberPlanDetailsWithBalance();
                        }

                    }
                    else
                    {
                        if (Interaction.MsgBox("You are about to make a REVERSAL PAYMENT. Continue?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
                        {
                            //MembersPaymentDetailsModel objPayment = new MembersPaymentDetailsModel();
                            //objPayment.pkiMemberID = MemberId;
                            //objPayment.Amount = Convert.ToDecimal(hdnAmount.Value.Replace("R ", ""));
                            //objPayment.LatePaymentPenalty = Convert.ToDecimal(txtLatePenalty.Text.Replace("R ", ""));
                            //objPayment.ReceivedBy = txtReceivedBy.Text.ToString();
                            //objPayment.Notes = txtMohthPaid.Text.ToString();
                            //objPayment.MethodOfPayment = strMethod;
                            //objPayment.MemeberNumber = txtMemberNo.Text.ToString();
                            //objPayment.PaymentDate = Convert.ToDateTime(txtNextPaymentDate.Text);
                            //objPayment.NextPaymentDate = Convert.ToDateTime(hdnLastpaymentDate.Value).AddMonths(Convert.ToInt32(ddlNoOfMonths.SelectedValue));
                            //// 
                            //objPayment.Branch = objmember.MemberBranch;
                            //objPayment.parlourid = ParlID;

                            //int PaymentID = client.AddReversalPayments(objPayment);
                            //if (PaymentID != 0)
                            //{
                            //    Common.WebMsgBox.Show("Reversal added successfully.");
                            //}
                            //else
                            //{
                            //    Common.WebMsgBox.Show("Reversal not added successfully.");
                            //}
                        }
                    }


                    //ReturnMemberPayment - Done
                    //FuneralPaymentsModel[] objFuneralPaymentModel = client.ReturnMemberPayment(Convert.ToString(MemberId));
                    //gvInvoices.DataSource = objFuneralPaymentModel;
                    //gvInvoices.DataBind();
                }
                else
                {
                    if (btnPay.Text == "Add Payment")
                    {
                       
                        FuneralPaymentsModel funeralPayment = new FuneralPaymentsModel();
                        funeralPayment.FuneralID = FuneralId;
                        funeralPayment.AmountPaid = Convert.ToDecimal(hdnAmount.Value.Replace(Currency.Trim() + " ", ""));
                        funeralPayment.RecievedBy = txtReceivedBy.Text.ToString();
                        funeralPayment.Notes = txtMohthPaid.Text.ToString();
                        funeralPayment.ParlourId = new Guid(Request.QueryString["ParlourId"]);
                        funeralPayment.UserName = HttpContext.Current.User.Identity.Name;
                        int FuneralID = MemberPaymentBAL.AddFuneralPayments(funeralPayment);
                        if (FuneralID != 0)
                        {
                            Common.WebMsgBox.Show("Payment added successfully.");
                            //ReadFuneralInvoiceIntoDatatable - Remain.

                            btnPrint.Enabled = true;
                        }
                        else
                        {
                            Common.WebMsgBox.Show("Payment not added successfully.");
                        }
                    }
                    else
                    {
                        //CheckUserAccess - Remain.
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab5", "confirm('You are about to make a REVERSAL PAYMENT. Continue?')", true);
                        if (Interaction.MsgBox("You are about to make a REVERSAL PAYMENT. Continue?", MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
                        {
                            FuneralPaymentsModel objFunerals = new FuneralPaymentsModel();
                            objFunerals.FuneralID = FuneralId;
                            objFunerals.AmountPaid = Convert.ToDecimal(hdnAmount.Value.Replace(Currency.Trim() + " ", ""));
                            objFunerals.RecievedBy = txtReceivedBy.Text.ToString();
                            objFunerals.Notes = txtMohthPaid.Text.ToString();
                            objFunerals.ParlourId = new Guid(Request.QueryString["ParlourId"]);
                            objFunerals.UserName = HttpContext.Current.User.Identity.Name;
                            int FuneralID = MemberPaymentBAL.AddFuneralPayments(objFunerals);
                        }
                        else
                        {
                            Interaction.MsgBox("Reversal not added successfully", MsgBoxStyle.Information);
                        }
                    }
                    //ReturnFuneralPayments - Done
                    //FuneralPaymentsModel[] objFuneralPaymentModel = client.ReturnFuneralPayments(Convert.ToString(0));
                    //gvInvoices.DataSource = objFuneralPaymentModel;
                    //gvInvoices.DataBind();

                }
                Guid ParlourId = new Guid(Request.QueryString["ParlourId"]);
                bindInvoices(ParlourId, MemberId);
                txtAmount.Text = "";
                hdnAmount.Value = "";
                txtMohthPaid.Text = "";

                ddlMethod.Enabled = true;
                ddlNoOfMonths.Enabled = true;
                rfvNoOfMonth.Enabled = true;

            }
            catch (Exception ex)
            {
                Common.WebMsgBox.Show(ex.Message.ToString());
                //this.Cursor = Cursors.Default;
                //Interaction.MsgBox("Error:- " + ex.Message, MsgBoxStyle.Information, strAppName);

            }
        }

        #endregion

        #region EVent
        protected void txtMemberNo_TextChanged(object sender, EventArgs e)
        {
            MembersPaymentDetailsModel model = MemberPaymentBAL.ReturnMemberPlanDetailsWithBalance(Convert.ToString(txtMemberNo.Text));
            if ((model == null))
            {

            }
            else
            {

                txtNextPaymentDate.Text = FormatDateTime(DateTime.Now.ToString());
                txtReceivedBy.Text = HttpContext.Current.User.Identity.Name;
                txtMember.Text = model.FullNames;
                txtMemberNo.Text = model.MemeberNumber;
                txtPlan.Text = model.PlanName;
                txtPremium.Text = model.PlanSubscription;
                txtSpouse.Text = model.Spouse.ToString();
                txtChildren.Text = model.Children.ToString();
                txtAdults.Text = model.Adults.ToString();
                txtWaitingPeriod.Text = model.WaitingPeriod.ToString();
                txtCover.Text = model.Cover;
                txtPolicyLaps.Text = model.PolicyLaps.ToString();
                txtMemberStauts.Text = model.PolicyStatus;
                txtJoiningFee.Text = model.JoiningFee;
                //txtJoiningFee.Text = model.JoiningFee.ToString("0.00");
                //txtNextPaymentDate.Text = model.NextPaymentDate.ToString();
                txtPolicyBalance.Text = model.Balance.ToString(Currency.Trim() + " 0.00");
                txtMonthOwing.Text = model.MonthOwing.ToString();
                txtLatePenalty.Text = model.LatePaymentPenalty.ToString(Currency.Trim() + " 0.00");

                txtMohthPaid.Text = model.NextPaymentDate.ToString("MMM-yyyy");
                ViewState["_memberId"] = Convert.ToInt32(model.pkiMemberID);
                ViewState["NextPaymentDate"] = model.NextPaymentDate.ToString("MMM-yyyy");

                txtTotalPremium.Text = Convert.ToDouble(MembersBAL.SumOfPremium(Convert.ToInt32(model.pkiMemberID), model.ParlourId)).ToString(Currency.Trim() + " 0.00");



                if (txtMemberStauts.Text == "Trial" || txtMemberStauts.Text == "Active")
                {
                    btnReunstate.Enabled = false;
                    btnRejoin.Enabled = false;
                }
                else
                {
                    btnReunstate.Enabled = true;
                    btnRejoin.Enabled = true;
                }

                bindInvoices(model.ParlourId, Convert.ToInt32(model.pkiMemberID));
            }
        }

        #endregion

        #region Webevent
        [WebMethod]
        public static string CalculateAmount(int noOfMonths, double TotalPremieum, double LatePanelty, string NextDate)
        {
            if (System.Web.HttpContext.Current.User?.Identity == null || !System.Web.HttpContext.Current.User.Identity.IsAuthenticated) return "R";
            FormsIdentity id = (FormsIdentity)System.Web.HttpContext.Current.User.Identity;
            FormsAuthenticationTicket ticket = id.Ticket;
            string[] strData = ticket.UserData.Split('|');
            string Currency = strData.Count() == 5 ? strData[4] : "R";

            string Amount = "";
            DateTime NextDate1 = Convert.ToDateTime(NextDate);
            string monthPaid = string.Empty;
            Amount = Convert.ToDouble(TotalPremieum * Convert.ToInt32(noOfMonths) + LatePanelty).ToString(Currency.Trim() + " 0.00");
            if (noOfMonths > 1)
            {
                if (NextDate1.Year == NextDate1.AddMonths(noOfMonths - 1).Year)
                    monthPaid = string.Format("{0}-{1} {2}", NextDate1.ToString("MMM"), NextDate1.AddMonths(noOfMonths - 1).ToString("MMM"), NextDate1.AddMonths(noOfMonths - 1).ToString("yyyy"));
                else
                    monthPaid = string.Format("{0} {1}-{2} {3}", NextDate1.ToString("MMM"), NextDate1.ToString("yyyy"), NextDate1.AddMonths(noOfMonths - 1).ToString("MMM"), NextDate1.AddMonths(noOfMonths - 1).ToString("yyyy"));

            }
            else
                monthPaid = string.Format("{0}-{1}", NextDate1.AddMonths(noOfMonths - 1).ToString("MMM"), NextDate1.AddMonths(noOfMonths - 1).ToString("yyyy"));

            double TotalPremium = Convert.ToDouble(TotalPremieum * Convert.ToInt32(noOfMonths) + LatePanelty);
            return Amount + "~" + monthPaid + "~" + TotalPremium;
            //return "{\"Amount\":\"" + Amount + "\",\"monthPaid\":\"" + monthPaid + "\",\"TotalPremium\":\"" + TotalPremium + "\"}";
        }

        #endregion

        #region PageEvent
        protected void gvInvoices_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Checking the RowType of the Row  
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //If Salary is less than 10000 than set the Cell BackColor to Red and ForeColor to White  
                if (!BtnReversal)
                {
                    e.Row.Cells[5].Visible = false;
                }
            }
        }
        protected void gvInvoices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int InvoiceID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "PrintPremium")
            {
                try
                {
                    PrintInvoice(InvoiceID, 1);
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
                    PrintInvoice(InvoiceID, 2);
                }
                catch (Exception ex)
                {
                    this.ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                }
            }
            else if (e.CommandName == "PaymentReversal")
            {
                int PaymentID = MemberPaymentBAL.AddReversalPayments(InvoiceID, this.UserName, this.ParlourId);
                if (PaymentID != 0)
                {
                    Guid ParlourId = new Guid(Request.QueryString["ParlourId"]);
                    bindInvoices(ParlourId, MemberId);
                    Common.WebMsgBox.Show("Reversal added successfully.");
                }
                else
                {
                    Common.WebMsgBox.Show("Reversal not added successfully.");
                }
            }

        }

        protected void gvInvoices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Guid ParlourId = new Guid(Request.QueryString["ParlourId"]);
            gvInvoices.PageIndex = e.NewPageIndex;
            bindInvoices(ParlourId, MemberId);
        }

        #endregion

        #region Other
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            MembersPaymentDetailsModel objPayment = new MembersPaymentDetailsModel();
        }

        public void PrintPayment(MembersPaymentDetailsModel model)
        {
            /*  
            Response.Redirect("PrinterReciept.aspx?PolicyNr=" + model.MemeberNumber.ToString() +
                                "&DatePaid=" + model.PaymentDate.ToString("YYYY-MMM-dd") +
                                "&AmountPaid=" + model.Amount.ToString() +
                                "&LateAmountPaid=" + model.LatePaymentPenalty.ToString() +
                                "&PolicyHolder=" + model.FullNames +
                                "&ReceivedBy=" + model.ReceivedBy +
                                "&TimePrinted=" + DateTime.Now.ToString("YYYY-MMM-dd hh:mm") +
                                "&Method=" + model.MethodOfPayment +
                                "&Notes=" + model.Notes +
                                "&Plan=" + model.PlanName ); 
            
             *  Dim zlabel As New Com.SharpZebra.Example.BorderedLabel("   Branch Name...... " & vbTab & vbTab & ConfigurationManager.AppSettings("branch"), _
                                                               "   Receipt Nr....... " & vbTab & vbTab & intReceipt, _
                               "   Policy Nr........ " & vbTab & vbTab & txtMemberNo.Text.ToUpper, _
                               "   Date Paid:....... " & vbTab & vbTab & dtDatepaid.ToString("dd-MMM-yyyy"), _
                               "   Amount Paid...... " & vbTab & vbTab & FormatCurrency(txtAmount.Text, 2), _
                               "   Policy Holder.... " & vbTab & vbTab & BL.pFullNames & "  " & BL.pSurname, _
                               "   Received By...... " & vbTab & vbTab & txtRecievedBy.Text, _
                               "   Time Printed..... " & vbTab & vbTab & DateTime.Now.ToString("dd-MMM-yyyy hh:mm"), _
                               "   Method........... " & vbTab & vbTab & strMethod, _
                               notes, _
                               "   Underwriter...... " & vbTab & vbTab & BL.pAddress4, "123", strAppName, dtAdditionalAppSettings.Rows(0).Item("slogan"))
 
             * 
             */


        }
        #endregion

        #region Is Joining fee paid
        private void IsJoiningFeePaid(out bool IsJoiningFee)
        {
            IsJoiningFee = false;
            JoiningFeeModel joiningFeeModel = MemberPaymentBAL.JoiningFees(this.MemberId, this.ParlourId);
            if (joiningFeeModel != null)
            {
                if (joiningFeeModel.Paid == false)
                {
                    txtMohthPaid.Text = "Joining Fee";
                    hdnAmount.Value = joiningFeeModel.JoiningFeeAmount.ToString();
                    txtAmount.Text = joiningFeeModel.JoiningFeeAmount.ToString(Currency.Trim() + " 0.00");
                    ddlNoOfMonths.Enabled = false;
                    rfvNoOfMonth.Enabled = false;
                    IsJoiningFee = true;
                }
            }
        }
        #endregion
        public void printStatement()
        {
            Guid ParlourId = new Guid(Request.QueryString["ParlourId"]);
            string Policyno = txtMemberNo.Text;
            string encryptedValue = EncryptionHelper.Encrypt(ParlourId + "&" + Policyno + "&" + MemberId);
            Response.Redirect("~/Admin/PrintStatement.aspx?InID=" + encryptedValue);
        }
        protected void lbtnStatement_Click(object sender, EventArgs e)
        {
            //printStatement();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            string filename;

            try
            {
                ReportViewer rpw = new ReportViewer();
                rpw.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new MyReportServerCredentials();
                rpw.ServerReport.ReportServerCredentials = irsc;

                rpw.ProcessingMode = ProcessingMode.Remote;
                rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/UIS_Customer Payments Statement";
                ReportParameterCollection reportParameters = new ReportParameterCollection();

                reportParameters.Add(new ReportParameter("MemberID", txtMemberNo.Text));
                reportParameters.Add(new ReportParameter("Parlourid", this.ParlourId.ToString()));
                rpw.ServerReport.SetParameters(reportParameters);
                string ExportTypeExtensions = "pdf";
                byte[] bytes = rpw.ServerReport.Render(ExportTypeExtensions, null, out mimeType, out encoding, out ExportTypeExtensions, out streamids, out warnings);
                filename = string.Format("{0}.{1}", "UIS_Customer Payments Statement", ExportTypeExtensions);

                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();


            }
            catch (Exception exc)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
            }



        }
    }
}