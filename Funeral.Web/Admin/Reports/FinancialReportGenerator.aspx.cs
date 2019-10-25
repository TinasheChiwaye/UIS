using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using Microsoft.VisualBasic;
using System.Web.UI.HtmlControls;
using Funeral.Web.App_Start;
using System.IO;
using System.Net.Mail;


namespace Funeral.Web.Admin.Reports
{
    public partial class FinancialReportGenerator : AdminBasePage
    {
        string ReportName
        {
            get
            {
                try
                {
                    if (ViewState["ReportName"] != null)
                    {
                        return ViewState["ReportName"].ToString();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["ReportName"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDdlExpenseBranch();
                BindDdlBranch();
                BindDdlSociety();
            }
            else
            {
                lblMessage.Text = string.Empty;
            }
        }
        #region Methods
        public void BindDdlExpenseBranch()
        {
            // string[] Policystatuslist = client.GetDistinctPolicyStatusList();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "SelectDistinctExpensesBranchList";
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlExpenseBranch.DataSource = dt;
                ddlExpenseBranch.DataTextField = "ExpenseBranch";
                ddlExpenseBranch.DataValueField = "ExpenseBranch";
                ddlExpenseBranch.DataBind();
                ddlExpenseBranch.Items.Insert(0, new ListItem("All", "All"));
                ddlExpenseBranch.Items.FindByText("All").Selected = true;

                //Bind PolicyStatus DDl
                /*  ddlPolicyStatus.DataSource = dt;
                  ddlPolicyStatus.DataTextField = "PolicyStatus";
                  ddlPolicyStatus.DataValueField = "PolicyStatus";
                  ddlPolicyStatus.DataBind();
                  ddlPolicyStatus.Items.Insert(0, new ListItem("All", "All"));
                 * */
            }

        }
        public void BindDdlBranch()
        {
            FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
            ddlBranch.DataSource = client.GetAllBranch(ParlourId);
            ddlBranch.DataBind();
        }
        public void BindDdlSociety()
        {
            FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
            ddlSociety.DataSource = client.GetAllSociety(ParlourId);
            ddlSociety.DataBind();
        }

        public void BindAgent()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTReturnAgentComm";

            com.Parameters.Add(new SqlParameter("@parlourId", this.ParlourId));
            com.Parameters.Add(new SqlParameter("@Agent", txtAgent.Text));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(txtAgentDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(txtAgentDateTo.Text)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcAgentCommision.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtAgentComm", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Agent Commision Report";
            //reportParameters.Add(new ReportParameter("txtHederRendor", "Agent Commission Reports for " + ddlBranch.SelectedItem.Text + "  between " + txtDateFrom.Text + " And " + txtDateTo.Text));
            //rvAgentComm.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindCashofBank()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "rptCashofBankPayment";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@StartDate", Convert.ToDateTime(txtBankPaymentDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@EndDate", Convert.ToDateTime(txtBankPaymentDateTo.Text)));
            com.Parameters.Add(new SqlParameter("@Company", "ALL"));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcBankPayment.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("rptdsBankPayment", dt));
            rvMembersByDateRange.DataBind();
            //ReportName = "All Payment";
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            //reportParameters.Add(new ReportParameter("txtBranches", "Hammanskraal"));
            //reportParameters.Add(new ReportParameter("txtUser", "Admin"));
            //rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindClaims()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "ReadMemberClaim";
            com.Parameters.Add(new SqlParameter("@ClaimNo", string.Empty));
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@datefrom", Convert.ToDateTime(txtClaimDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@dateto", Convert.ToDateTime(txtClaimDateTo.Text)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcBankPayment.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("rdsClaimsReport", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Claims Reports between " + txtClaimDateFrom.Text + " And " + txtClaimDateTo.Text;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindDetailedPayment()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "ReturnDetailedPayments";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@datefrom", txtDatailedPaymentDatefrom.Text));
            com.Parameters.Add(new SqlParameter("@dateto", txtDatailedPaymentDateTo.Text));

            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
           
                rvMembersByDateRange.Visible = true;
                rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rdlcDetailedPayment.rdlc";
                rvMembersByDateRange.LocalReport.DataSources.Clear();
                rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsDetailedPayment", dt));
                rvMembersByDateRange.DataBind();

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                ReportName = "Detailed Payment Between " + txtDatailedPaymentDatefrom.Text.Trim() + " and " + txtDatailedPaymentDateTo.Text.Trim();
                reportParameters.Add(new ReportParameter("txtReportName", ReportName));
                rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
                rvMembersByDateRange.LocalReport.Refresh();

           
        }
        public void BindExpense()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTReturnExpenses";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@datefrom", Convert.ToDateTime(txtExpensDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@dateto", Convert.ToDateTime(txtExpensDateTo.Text)));
            com.Parameters.Add(new SqlParameter("@branch", ddlExpenseBranch.SelectedItem.Text));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rdlcExpenses.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsExpenses", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Expense On " + ddlExpenseBranch.SelectedItem.Text.Trim() + " Between " + txtExpensDateFrom.Text + " and " + txtExpensDateTo.Text;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
            //ReportDataSource rds = new ReportDataSource("dsChart", ObjectDataSourceJoinedMembersbyDate);

            //ReportDataSource rds2 = new ReportDataSource("DataSet1", ObjectDataSourceJoinedMembersbyDate);
            //rvMembersByDateRange.LocalReport.DataSources.Clear();

            //rvMembersByDateRange.LocalReport.DataSources.Add(rds);

            //rvMembersByDateRange.LocalReport.DataSources.Add(rds2);

            //}
            //else
            //{
            //    lblMessage.Text = "No Record Found.";
            //}
        }
        public void BindFunerlPayment()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTReturnFuneralPayments";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@datefrom", Convert.ToDateTime(txtDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@dateto", Convert.ToDateTime(txtDateTo.Text)));

            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/FuneralPayments.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsFuneralPayment", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Funeral payments made between " + txtDateFrom.Text + " and " + txtDateTo.Text;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
            //ReportDataSource rds = new ReportDataSource("dsChart", ObjectDataSourceJoinedMembersbyDate);

            //ReportDataSource rds2 = new ReportDataSource("DataSet1", ObjectDataSourceJoinedMembersbyDate);
            //rvMembersByDateRange.LocalReport.DataSources.Clear();

            //rvMembersByDateRange.LocalReport.DataSources.Add(rds);

            //rvMembersByDateRange.LocalReport.DataSources.Add(rds2);


        }
        public void BindPaymentBranch()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "rptUnderwriteraMemberPayments";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@Branch", ddlBranch.SelectedItem.Text));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(txtBranchDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(txtBranchDateTo.Text)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcPaymentbyBranch.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("rdlcDataPayment", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Payments By " + ddlBranch.SelectedItem.Text + "  between " + txtBranchDateFrom.Text + " And " + txtBranchDateTo.Text;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));            
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindPaymentDate()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "rptUnderwriteraMemberPayments";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(txtPaymentbyDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(txtPaymentbyDateTo.Text)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcPaymentbyBranch.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("rdlcDataPayment", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Payments between " + txtPaymentbyDateFrom.Text + " And " + txtPaymentbyDateTo.Text;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindPaymentSocietty()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "rptUnderwriteraMemberPayments";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@Society", ddlSociety.SelectedItem.Text));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(txtsocietyDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(txtsocietyDateTo.Text)));
            // com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(txtDateTo.Text)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcPaymentbyBranch.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("rdlcDataPayment", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Payments By " + ddlSociety.SelectedItem.Text + "  between " + txtsocietyDateFrom.Text + " And " + txtsocietyDateTo.Text;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindQuotationByDate()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTQuotationByDate";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@datefrom", Convert.ToDateTime(txtQuotationDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@dateto", Convert.ToDateTime(txtQuotationDateTo.Text)));

            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rdlcQuotationByDate.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsQuotationByDate", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Report By Quotation Dates Between " + txtQuotationDateFrom.Text + " and " + txtQuotationDateTo.Text;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();

        }
        public void BindUnderwriterPayment()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "rptUnderwriteraMemberPayments";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@Underwiter", txtUnderwiter.Text));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(txtUnderwriterDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(txtUnderwriterDateTo.Text)));
            // com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(txtDateTo.Text)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rptUnderwriterPayments.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsrptUnderwriterPaymentData", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName="Underwriter Payments for " + txtUnderwiter.Text + "  between " + txtUnderwriterDateFrom.Text + " And " + txtUnderwriterDateTo.Text;
            reportParameters.Add(new ReportParameter("txtHederRendor", ReportName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }


        #endregion

        #region Events
        public void Sendmail()
        {
            string extension = ".doc";
            switch (ddlReportFormat.SelectedValue)
            {
                case "Word":
                    extension = ".doc";
                    break;
                case "Pdf":
                    extension = ".pdf";
                    break;
                case "Excel":
                    extension = ".xls";
                    break;
                default:
                    break;
            }
            byte[] bytes = rvMembersByDateRange.LocalReport.Render(ddlReportFormat.SelectedValue);
            MemoryStream s = new MemoryStream(bytes);
            s.Seek(0, SeekOrigin.Begin);
            Attachment a = new Attachment(s, ReportName + extension);
            MailMessage message = new MailMessage(ConfigurationManager.AppSettings["ReportEmailSenderId"].ToString().Trim(), txtReportEmail.Text.Trim(), "Funeral Report-" + ReportName, "");
            message.Attachments.Add(a);
            SmtpClient client = new SmtpClient();
            client.Send(message);
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Email Sent Successfully.";
            switch (btnSendMail.CommandName)
            {
                case "btnAgent":
                    BindAgent();
                    break;
                case "btnCashofBank":
                    BindCashofBank();
                    break;
                case "btnClaims":
                    BindClaims();
                    break;
                case "btnDetailedPayment":
                    BindDetailedPayment();
                    break;
                case "btnExpenses":
                    BindExpense();
                    break;
                case "btnFuneralPayment":
                    BindFunerlPayment();
                    break;
                case "btnBranch":
                    BindPaymentBranch();
                    break;
                case "btnPaymentbyDate":
                    BindPaymentDate();
                    break;
                case "btnSocietty":
                    BindPaymentSocietty();
                    break;
                case "btnQuoteByDate":
                    BindQuotationByDate();
                    break;
                case "btnUnderwriter":
                    BindUnderwriterPayment();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Events
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    rvMembersByDateRange.Reset();
                    lblMessage.Text = string.Empty;
                    switch (((HtmlButton)sender).ID)
                    {
                        case "btnAgent":
                            btnSendMail.CommandName = "btnAgent";
                            BindAgent();
                            break;
                        case "btnCashofBank":
                            btnSendMail.CommandName = "btnCashofBank";
                            BindCashofBank();
                            break;
                        case "btnClaims":
                            btnSendMail.CommandName = "btnClaims";
                            BindClaims();
                            break;
                        case "btnDetailedPayment":
                            btnSendMail.CommandName = "btnDetailedPayment";
                            BindDetailedPayment();
                            break;
                        case "btnExpenses":
                            btnSendMail.CommandName = "btnExpenses";
                            BindExpense();
                            break;
                        case "btnFuneralPayment":
                            btnSendMail.CommandName = "btnFuneralPayment";
                            BindFunerlPayment();
                            break;
                        case "btnBranch":
                            btnSendMail.CommandName = "btnBranch";
                            BindPaymentBranch();
                            break;
                        case "btnPaymentbyDate":
                            btnSendMail.CommandName = "btnPaymentbyDate";
                            BindPaymentDate();
                            break;
                        case "btnSocietty":
                            btnSendMail.CommandName = "btnSocietty";
                            BindPaymentSocietty();
                            break;
                        case "btnQuoteByDate":
                            btnSendMail.CommandName = "btnQuoteByDate";
                            BindQuotationByDate();
                            break;
                        case "btnUnderwriter":
                            btnSendMail.CommandName = "btnUnderwriter";
                            BindUnderwriterPayment();
                            break;
                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
        }
        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                if (ReportName == string.Empty)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "*Generate Report First To Send Email.";
                }
                else
                {
                    Sendmail();
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error:" + ex.Message;
            }
        }
        #endregion

    }
}