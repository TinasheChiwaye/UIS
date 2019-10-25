using ClosedXML.Excel;
using Funeral.Model;
using Funeral.Web.App_Start;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Funeral.Web.Admin.Reports
{

    public partial class reportrdlc : AdminBasePage
    {
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();

        #region Properties
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
        string ReportSendComand
        {
            get
            {
                try
                {
                    if (ViewState["ReportSendComand"] != null)
                    {
                        return ViewState["ReportSendComand"].ToString();
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
                ViewState["ReportSendComand"] = value;
            }
        }
        #endregion

        public DataTable LocalQoute
        {
            get
            {
                if (Session["LocalQoute"] != null)
                {
                    return Session["LocalQoute"] as DataTable;
                }
                else
                {
                    return null;
                }
            }
            set { Session["LocalQoute"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LocalQoute = applictionLogo();
                BindReportAll();
                
            }
        }
        public DataTable applictionLogo()
        {
            SqlCommand com1 = new SqlCommand();
            com1.CommandType = CommandType.StoredProcedure;
            com1.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com1.CommandText = "ApplicationSelectByParlourId";
            com1.Parameters.Add(new SqlParameter("@ParlourId", ParlourId));
            SqlDataAdapter adp1 = new SqlDataAdapter(com1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);
            return dt1;
        }
        public void BindReportAll()
        {
            string ReportName = string.Empty;
            if (Request.QueryString["Type"] != null)
            {
                ReportName = Request.QueryString["Type"].ToString();
            }
            divrdlc.Visible = true;
            switch (ReportName)
            {

                case "btnPnlAllJoinedMemebrs":
                    ReportSendComand = "btnPnlAllJoinedMemebrs";
                    BindAllJoinedMembers();
                    break;
                case "btnPnlMembersByJoinedDate":
                    ReportSendComand = "btnPnlMembersByJoinedDate";
                    string MembersByJoinedDateArray = Request.QueryString["BindJoinedMembersByDate"].ToString();
                    string[] MembersByJoinedDate = MembersByJoinedDateArray.Split(',');
                    BindJoinedMembersByDate(MembersByJoinedDate[0].ToString(), MembersByJoinedDate[1].ToString(), MembersByJoinedDate[2].ToString(), MembersByJoinedDate[3].ToString());
                    break;
                case "btnPnlPolicyStatus":
                    ReportSendComand = "btnPnlPolicyStatus";
                    string PolicyStatusArray = Request.QueryString["BindPolicyStatus"].ToString();
                    string[] PolicyStatus = PolicyStatusArray.Split(',');
                    BindPolicyStatus(PolicyStatus[0].ToString(), PolicyStatus[1].ToString(), PolicyStatus[2].ToString());
                    break;
                case "btnPnlMembersPerBranch":
                    ReportSendComand = "btnPnlMembersPerBranch";
                    string MembersByBranch = Request.QueryString["BindMembersByBranch"].ToString();
                    BindMembersByBranch(MembersByBranch);
                    break;
                case "btnPnlMembersPerPlan":
                    ReportSendComand = "btnPnlMembersPerPlan";
                    string MembersPerPlan = Request.QueryString["BindMembersPerPlan"].ToString();
                    BindMembersPerPlan(MembersPerPlan);
                    break;
                case "btnPnlMembersUnderWriting":
                    ReportSendComand = "btnPnlMembersUnderWriting";
                    string MembersUnderwriting = Request.QueryString["BindMembersUnderwriting"].ToString();
                    BindMembersUnderwriting(MembersUnderwriting);
                    break;
                case "btnAgentReport":
                    ReportSendComand = "btnAgentReport";
                    string AgentReportArray = Request.QueryString["BindAgentReport"].ToString();
                    string[] AgentReport = AgentReportArray.Split(',');
                    BindAgentReport(AgentReport[0].ToString(), AgentReport[1].ToString(), AgentReport[2].ToString());
                    break;
                case "btnFuneralArrangement":
                    ReportSendComand = "btnAgentReport";
                    string FuneralArrangementArray = Request.QueryString["BindbtnFuneralArrangement"].ToString();
                    string[] FuneralArrangement = FuneralArrangementArray.Split(',');
                    BindbtnFuneralArrangement(FuneralArrangement[0].ToString(), FuneralArrangement[1].ToString());
                    break;
                case "btnoutstandingPayment":
                    ReportSendComand = "btnoutstandingPayment";
                    BindOutstandingPayment();
                    divrdlc.Visible = false;
                    break;
                case "btnDepandantsOver":
                    ReportSendComand = "btnDepandantsOver";
                    BindDepandendants();
                    divrdlc.Visible = false;
                    break;

                //Finance Report cases
                case "btnAgent":
                    ReportSendComand = "btnAgent";
                    string AgentArray = Request.QueryString["BindAgent"].ToString();
                    string[] strAgentReport = AgentArray.Split(',');
                    BindAgent(strAgentReport[0].ToString(), strAgentReport[1].ToString(), strAgentReport[2].ToString());
                    break;
                //case "btnCashofBank":
                //    ReportSendComand = "btnCashofBank";
                //    string CashofBankArray = Request.QueryString["BindCashofBank"].ToString();
                //    string[] CashofBank = CashofBankArray.Split(',');
                //    BindCashofBank(CashofBank[0].ToString(), CashofBank[1].ToString());
                //    break;
                case "btnClaims":
                    ReportSendComand = "btnClaims";
                    string ClaimsArray = Request.QueryString["BindClaims"].ToString();
                    string[] Claims = ClaimsArray.Split(',');
                    BindClaims(Claims[0].ToString(), Claims[1].ToString(), Claims[2].ToString());
                    break;
                case "btnDetailedPayment":
                    ReportSendComand = "btnDetailedPayment";
                    string DetailedPaymentArray = Request.QueryString["BindDetailedPayment"].ToString();
                    string[] DetailedPayment = DetailedPaymentArray.Split(',');
                    BindDetailedPayment(DetailedPayment[0].ToString(), DetailedPayment[1].ToString(), DetailedPayment[2].ToString());
                    break;
                case "btnExpenses":
                    ReportSendComand = "btnExpenses";
                    string ExpenseArray = Request.QueryString["BindExpense"].ToString();
                    string[] strExpense = ExpenseArray.Split(',');
                    BindExpense(strExpense[0].ToString(), strExpense[1].ToString());
                    break;
                case "btnFuneralPayment":
                    ReportSendComand = "btnFuneralPayment";
                    string FunerlPaymentArray = Request.QueryString["BindFunerlPayment"].ToString();
                    string[] FunerlPayment = FunerlPaymentArray.Split(',');
                    BindFunerlPayment(FunerlPayment[0].ToString(), FunerlPayment[1].ToString());
                    break;
                case "btnBranch":
                    ReportSendComand = "btnBranch";
                    string PaymentBranchtArray = Request.QueryString["BindPaymentBranch"].ToString();
                    string[] PaymentBranch = PaymentBranchtArray.Split(',');
                    BindPaymentBranch(PaymentBranch[0].ToString(), PaymentBranch[1].ToString(), PaymentBranch[2].ToString());
                    break;
                case "btnPaymentbyDate":
                    ReportSendComand = "btnPaymentbyDate";
                    string PaymentDateArray = Request.QueryString["BindPaymentDate"].ToString();
                    string[] PaymentDate = PaymentDateArray.Split(',');
                    BindPaymentDate(PaymentDate[0].ToString(), PaymentDate[1].ToString());
                    break;
                case "btnSocietty":
                    ReportSendComand = "btnSocietty";
                    string PaymentSociettyArray = Request.QueryString["BindPaymentSocietty"].ToString();
                    string[] PaymentSocietty = PaymentSociettyArray.Split(',');
                    BindPaymentSocietty(PaymentSocietty[0].ToString(), PaymentSocietty[1].ToString(), PaymentSocietty[2].ToString());
                    break;
                case "btnQuoteByDate":
                    ReportSendComand = "btnQuoteByDate";
                    string QuotationByDateArray = Request.QueryString["BindQuotationByDate"].ToString();
                    string[] QuotationByDate = QuotationByDateArray.Split(',');
                    BindQuotationByDate(QuotationByDate[0].ToString(), QuotationByDate[1].ToString());
                    break;
                case "btnUnderwriter":
                    ReportSendComand = "btnUnderwriter";
                    string UnderwriterPaymentArray = Request.QueryString["BindUnderwriterPayment"].ToString();
                    string[] UnderwriterPayment = UnderwriterPaymentArray.Split(',');
                    BindUnderwriterPayment(UnderwriterPayment[0].ToString(), UnderwriterPayment[1].ToString(), UnderwriterPayment[2].ToString());
                    break;
                case "btnMyCashUp":
                    ReportSendComand = "btnMyCashUp";
                    string MyCashUpArray = Request.QueryString["BindMyCashUp"].ToString();
                    string[] MyCashUp = MyCashUpArray.Split(',');
                    BindMyCashUp(MyCashUp[0].ToString(), MyCashUp[1].ToString());
                    break;
                case "btnBranches":
                    ReportSendComand = "btnBranches";
                    //string BrachesArray = Request.QueryString["BindBraches"].ToString();
                    //string[] strBrachesReport = BrachesArray.Split(',');
                    BindMemberBranch();
                    break;
            }

        }
        #region Admin Report Method
        public void BindAllJoinedMembers()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTallmembers";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@branch", "All"));
            com.Parameters.Add(new SqlParameter("@StartDate", null));
            com.Parameters.Add(new SqlParameter("@EndDate", null));
            com.Parameters.Add(new SqlParameter("@MemberPlan", "-1"));
            com.Parameters.Add(new SqlParameter("@PolicyStatusList", CheckedPolicyStates()));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            rvMembersByDateRange.Visible = true;
            //rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rptAllMember.rdlc";
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            //rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsAllMembers", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "All Members Joined";
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindJoinedMembersByDate(string MemberBranch, string DateFrom, string DateTo, string MemberPlan)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTallmembers";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@branch", MemberBranch));
            com.Parameters.Add(new SqlParameter("@StartDate", Convert.ToDateTime(DateFrom)));
            com.Parameters.Add(new SqlParameter("@EndDate", Convert.ToDateTime(DateTo)));
            com.Parameters.Add(new SqlParameter("@MemberPlan", MemberPlan));
            com.Parameters.Add(new SqlParameter("@PolicyStatusList", CheckedPolicyStates()));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            rvMembersByDateRange.Visible = true;
            //rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            //rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rptAllMember.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));

            //rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsAllMembers", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Members Per Branch " + MemberBranch + " and Per Plan " + MemberPlan + " Between " + DateFrom + " And " + DateTo;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
        }
        public void BindPolicyStatus(string DateFrom, string DateTo, string PolicyStatus)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTallmembers";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@branch", "ALL"));
            com.Parameters.Add(new SqlParameter("@StartDate", Convert.ToDateTime(DateFrom)));
            com.Parameters.Add(new SqlParameter("@EndDate", Convert.ToDateTime(DateTo)));
            com.Parameters.Add(new SqlParameter("@PolicyStatus", PolicyStatus));
            com.Parameters.Add(new SqlParameter("@PolicyStatusList", CheckedPolicyStates()));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));

            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Members By PolicyStatus Matching with '" + PolicyStatus + "'";
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
        }
        public void BindMembersByBranch(string MemberPerBranch)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTallmembers";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@branch", MemberPerBranch));
            com.Parameters.Add(new SqlParameter("@StartDate", null));
            com.Parameters.Add(new SqlParameter("@EndDate", null));
            com.Parameters.Add(new SqlParameter("@PolicyStatus", null));
            com.Parameters.Add(new SqlParameter("@PolicyStatusList", CheckedPolicyStates()));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));

            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Members Per Branch " + MemberPerBranch;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
        }
        public void BindMembersPerPlan(string Planname)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTallmembers";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@branch", "All"));
            com.Parameters.Add(new SqlParameter("@StartDate", null));
            com.Parameters.Add(new SqlParameter("@EndDate", null));
            com.Parameters.Add(new SqlParameter("@MemberPlan", Planname));
            com.Parameters.Add(new SqlParameter("@PolicyStatusList", CheckedPolicyStates()));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            //rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rptAllMember.rdlc";
            
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));

            //rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Members Per Plan " + Planname;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
        }
        public void BindMembersUnderwriting(string MembersUnderwriting)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTallmembers";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@branch", "All"));
            com.Parameters.Add(new SqlParameter("@StartDate", null));
            com.Parameters.Add(new SqlParameter("@EndDate", null));
            com.Parameters.Add(new SqlParameter("@PolicyStatus", null));
            com.Parameters.Add(new SqlParameter("@PolicyStatusList", CheckedPolicyStates()));
            com.Parameters.Add(new SqlParameter("@Underwriterplan", MembersUnderwriting));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            //rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rptAllMember.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            //rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsAllMembers", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));

            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Members with UnderWriting Plan " + MembersUnderwriting;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
        }
        public void BindAgentReport(string Agent, string AgentAdminDateFrom, string AgentAdminDateTo)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTReturnAgentComm";
            com.Parameters.Add(new SqlParameter("@parlourId", this.ParlourId));
            com.Parameters.Add(new SqlParameter("@Agent", Agent));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(AgentAdminDateFrom)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(AgentAdminDateTo)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rptAgentReport.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DsRptAgentReport", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Agent Report  for " + Agent + "  between " + AgentAdminDateFrom + " And " + AgentAdminDateTo;
            reportParameters.Add(new ReportParameter("txtHederRendor", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindbtnFuneralArrangement(string FuneralArrangmentDateFrom, string FuneralArrangmentDateTo)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTFuneralArrangement";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(FuneralArrangmentDateFrom)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(FuneralArrangmentDateTo)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rptFuneralArrangement.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DSRptFuneralArrangement", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));

            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            //ReportName = "Agent Report  for " + txtAgent.Text + "  between " + txtAgentDateFrom.Text + " And " + txtAgentDateTo.Text;
            //reportParameters.Add(new ReportParameter("txtHederRendor", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindOutstandingPayment()
        {
            divgrid.Visible = true;
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTOutstandingPayments1";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            //com.Parameters.Add(new SqlParameter("@StartDate", Convert.ToDateTime(txtOutstandingPaymentDateFrom.Text)));
            //com.Parameters.Add(new SqlParameter("@EndDate", Convert.ToDateTime(txtOutstandingPaymentDateTo.Text)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);


            gvOutstanding.DataSource = dt;
            gvOutstanding.DataBind();
            ReportName = "Outstanding Payments";
           
        }
        public void BindDepandendants()
        {
            divgvDependendance.Visible = true;
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPT_Dependents_Ove_21age";
            com.Parameters.Add(new SqlParameter("@parlourId", ParlourId));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            gvDepandandants.DataSource = dt;
            gvDepandandants.DataBind();
            ReportName = "Dependents Ove 21 Age";
        }
        //public void BindOutstandingPayment()
        //{
        //    SqlCommand com = new SqlCommand();
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
        //    com.CommandText = "rptOutStandingGroupPayment";
        //    com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
        //    com.Parameters.Add(new SqlParameter("@StartDate", Convert.ToDateTime(txtOutstandingPaymentDateFrom.Text)));
        //    com.Parameters.Add(new SqlParameter("@EndDate", Convert.ToDateTime(txtOutstandingPaymentDateTo.Text)));
        //    SqlDataAdapter adp = new SqlDataAdapter(com);
        //    DataTable dt = new DataTable();
        //    adp.Fill(dt);
        //    //if (dt.Rows.Count > 0)
        //    //{
        //    rvMembersByDateRange.Visible = true;
        //    rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rptOutstandingPayment.rdlc";
        //    rvMembersByDateRange.LocalReport.DataSources.Clear();
        //    rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DsRdlcOutstandingPayment", dt));
        //    rvMembersByDateRange.DataBind();

        //    ReportParameterCollection reportParameters = new ReportParameterCollection();
        //    ReportName = "Outstanding Payment Between " + txtOutstandingPaymentDateFrom.Text + " And " + txtOutstandingPaymentDateTo.Text;
        //    reportParameters.Add(new ReportParameter("txtHederName", ReportName));
        //    reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
        //    rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
        //    rvMembersByDateRange.LocalReport.Refresh();

        //}
        #endregion

        #region FinanceReportMethod
        public void BindMemberBranch()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RptBranchPremium";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.EnableExternalImages = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcMemberBranches.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dtMemberBranches", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Branches Report";
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindAgent(string AgentComition, string AgentDateFrom, string AgentDateTo)
        {
           // //Add logo
           // ApplicationSettingsModel modelCompany;
           // modelCompany = client.GetApplictionByParlourID(ParlourId);
           // string base64String = Convert.ToBase64String(modelCompany.ApplicationLogo);
           // Bitmap image;
           // using (var imageStream = new MemoryStream(modelCompany.ApplicationLogo, false))
           // {
           //      image = new Bitmap(imageStream);
           //     image.Save(Server.MapPath("Logo.jpg"));
           // }
           // //string base64String = Convert.ToBase64String(modelCompany.ApplicationLogo, 0, modelCompany.ApplicationLogo.Length);
           //// var Imgs = "data:image/png;base64," + base64String;
           // //
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTReturnAgentComm";
            com.Parameters.Add(new SqlParameter("@parlourId", this.ParlourId));
            com.Parameters.Add(new SqlParameter("@Agent", AgentComition));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(AgentDateFrom)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(AgentDateTo)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.EnableExternalImages = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcAgentCommision.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtAgentComm", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Agent Commision Report";
            //string imagePath = new Uri(Server.MapPath("Logo.jpg")).AbsoluteUri;
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            //reportParameters.Add(new ReportParameter("ImagePath", imagePath));
            //reportParameters.Add(new ReportParameter("ImagePathmimeType", "image/png", true));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        //public void BindCashofBank(string BankPaymentDateFrom, string BankPaymentDateTo)
        //{
        //    SqlCommand com = new SqlCommand();
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
        //    com.CommandText = "rptCashofBankPayment";
        //    com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
        //    com.Parameters.Add(new SqlParameter("@StartDate", Convert.ToDateTime(BankPaymentDateFrom)));
        //    com.Parameters.Add(new SqlParameter("@EndDate", Convert.ToDateTime(BankPaymentDateTo)));
        //    com.Parameters.Add(new SqlParameter("@Company", "ALL"));
        //    SqlDataAdapter adp = new SqlDataAdapter(com);
        //    DataTable dt = new DataTable();
        //    adp.Fill(dt);

        //    rvMembersByDateRange.Visible = true;
        //    rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcBankPayment.rdlc";
        //    rvMembersByDateRange.LocalReport.DataSources.Clear();
        //    rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("rptdsBankPayment", dt));
        //    rvMembersByDateRange.DataBind();
        //    //ReportName = "All Payment";
        //    ReportParameterCollection reportParameters = new ReportParameterCollection();
        //    reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
        //    rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
        //    rvMembersByDateRange.LocalReport.Refresh();


        //}
        public void BindClaims(string ClaimDateFrom, string ClaimDateTo, string ClaimStatus)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "ReadMemberClaim";
            //com.Parameters.Add(new SqlParameter("@ClaimNo", string.Empty));
            com.Parameters.Add(new SqlParameter("@status", ClaimStatus));
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@datefrom", Convert.ToDateTime(ClaimDateFrom)));
            com.Parameters.Add(new SqlParameter("@dateto", Convert.ToDateTime(ClaimDateTo)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rptClaimsReport.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("rdsClaimsReport", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Claims Reports between " + ClaimDateFrom + " And " + ClaimDateTo;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindDetailedPayment(string DatailedPaymentMethod, string DatailedPaymentDateFrom, string DatailedPaymentDateTo)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "ReturnDetailedPayments";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@datefrom", DatailedPaymentDateFrom));
            com.Parameters.Add(new SqlParameter("@dateto", DatailedPaymentDateTo));
            com.Parameters.Add(new SqlParameter("@MethodOfPayment", DatailedPaymentMethod));

            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rdlcDetailedPayment.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsDetailedPayment", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Payment Between " + DatailedPaymentDateFrom + " and " + DatailedPaymentDateTo;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
        }
        public void BindExpense(string ExpensDateFrom, string ExpensDateTo)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTReturnExpenses";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@datefrom", Convert.ToDateTime(ExpensDateFrom)));
            com.Parameters.Add(new SqlParameter("@dateto", Convert.ToDateTime(ExpensDateTo)));
            //com.Parameters.Add(new SqlParameter("@branch", ddlExpenseBranch.SelectedItem.Text));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rdlcExpenses.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsExpenses", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            //ReportName = "Expense On " + ddlExpenseBranch.SelectedItem.Text.Trim() + " Between " + txtExpensDateFrom.Text + " and " + txtExpensDateTo.Text;
            ReportName = "Expense Between " + ExpensDateFrom + " and " + ExpensDateTo;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
        }
        public void BindFunerlPayment(string FFPDateFrom, string FFPDateTo)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTReturnFuneralPayments";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@datefrom", Convert.ToDateTime(FFPDateFrom)));
            com.Parameters.Add(new SqlParameter("@dateto", Convert.ToDateTime(FFPDateTo)));

            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/FuneralPayments.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsFuneralPayment", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Funeral payments made between " + FFPDateFrom + " and " + FFPDateTo;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
        }
        public void BindPaymentBranch(string Branch, string BranchDateFrom, string BranchDateTo)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "rptUnderwriteraMemberPayments";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@Branch", Branch));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(BranchDateFrom)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(BranchDateTo)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcPaymentbyBranch.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("rdlcDataPayment", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Payments By " + Branch + "  between " + BranchDateFrom + " And " + BranchDateTo;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
        }
        public void BindPaymentDate(string PaymentbyDateFrom, string PaymentbyDateTo)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "rptUnderwriteraMemberPayments";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(PaymentbyDateFrom)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(PaymentbyDateTo)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcPaymentbyBranch.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("rdlcDataPayment", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Payments between " + PaymentbyDateFrom + " And " + PaymentbyDateTo;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindPaymentSocietty(string Society, string SocietyDateFrom, string SocietyDateTo)
        {

            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "rptUnderwriteraMemberPayments";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@Society", Society));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(SocietyDateFrom)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(SocietyDateTo)));
            // com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(txtDateTo.Text)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcPaymentbyBranch.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("rdlcDataPayment", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Payments By " + Society + "  between " + SocietyDateFrom + " And " + SocietyDateTo;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindQuotationByDate(string QuotationDateFrom, string QuotationDateTo)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTQuotationByDate1";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@datefrom", Convert.ToDateTime(QuotationDateFrom)));
            com.Parameters.Add(new SqlParameter("@dateto", Convert.ToDateTime(QuotationDateTo)));

            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rdlcQuotationByDate.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();

            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsQuotationByDate", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsQuotationByDate1", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));

            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Report By Quotation Dates Between " + QuotationDateFrom + " and " + QuotationDateTo;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();

        }
        public void BindUnderwriterPayment(string UnderwriterPayment, string UnderwriterPaymentDateFrom, string UnderwriterPaymentDateTo)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "rptUnderwriteraMemberPayments";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@Underwiter", UnderwriterPayment));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(UnderwriterPaymentDateFrom)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(UnderwriterPaymentDateTo)));
            // com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(txtDateTo.Text)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rptUnderwriterPayments.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsrptUnderwriterPaymentData", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Underwriter Payments for " + UnderwriterPayment + "  between " + UnderwriterPaymentDateFrom + " And " + UnderwriterPaymentDateTo;
            reportParameters.Add(new ReportParameter("txtHederRendor", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
        }
        public void BindMyCashUp(string MyCashUpDateFrom, string MyCashUpDateTo)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "rptMyCashUp";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@Userid", HttpContext.Current.User.Identity.Name));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(MyCashUpDateFrom)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(MyCashUpDateTo)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "Admin/Reports/FINANCIAL_REPORTS/ReportLayouts/rdlcPaymentbyBranch.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("rdlcDataPayment", dt));
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Payments between " + MyCashUpDateFrom + " And " + MyCashUpDateTo;
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
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
            Attachment a;
            if (ReportName == "Outstanding Payments" || ReportName == "Dependents Ove 21 Age")
            {
                string spname;
                if (ReportName == "Outstanding Payments")
                {
                    spname = "RPTOutstandingPayments1";
                }
                else
                {
                    spname = "RPT_Dependents_Ove_21age";
                }
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.StoredProcedure;
                com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com.CommandText = spname;
                com.Parameters.Add(new SqlParameter("@parlourId", ParlourId));
                SqlDataAdapter adp = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                //Set DataTable Name which will be the name of Excel Sheet.
                dt.TableName = "GridView_Data";

                //Create a New Workbook.
                XLWorkbook wb = new XLWorkbook();
                wb.Worksheets.Add(dt);
                MemoryStream memoryStream = new MemoryStream();
                wb.SaveAs(memoryStream);
                //Convert MemoryStream to Byte array.
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                a = new Attachment(new MemoryStream(bytes), ReportName + ".xlsx");

            }
            else
            {
                byte[] bytes = rvMembersByDateRange.LocalReport.Render(ddlReportFormat.SelectedValue);
                MemoryStream s = new MemoryStream(bytes);
                s.Seek(0, SeekOrigin.Begin);
                a = new Attachment(s, ReportName + extension);
            }
            MailMessage message = new MailMessage(ConfigurationManager.AppSettings["ReportEmailSenderId"].ToString().Trim(), txtReportEmail.Text.Trim(), "Funeral Report-" + ReportName, "");
            message.Attachments.Add(a);
            SmtpClient client = new SmtpClient();
            client.Send(message);
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Email Sent Successfully.";
            BindReportAll();
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
        protected void btnReminder_Click(object sender, EventArgs e)
        {
            lblMassageChk1.Text = string.Empty;
            FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
            SendReminderModel objPayment = new SendReminderModel();
            int SuccessCount = 0;
            int FailedCount = 0;
            foreach (GridViewRow item in gvOutstanding.Rows)
            {
                CheckBox chkceck = item.FindControl("SelectCheckBox") as CheckBox;
                if (chkceck.Checked)
                {
                    Label lblText = item.FindControl("lblMemberID") as Label;
                    //objPayment.MemeberID = lblText.Text;
                    objPayment.MemeberID = UserID.ToString(); 
                    objPayment.MemberData = "Member No : " + item.Cells[2].Text +
                                    ", Name : " + item.Cells[3].Text + item.Cells[4].Text +
                                    ", Address : " + item.Cells[5].Text +
                                    ", Balance Due :" + item.Cells[7].Text +
                                    ", Plan Name :" + item.Cells[8].Text +
                                    ", Subsciption :" + item.Cells[9].Text;
                    if (item.Cells[6].Text != "" && item.Cells[6].Text != null && item.Cells[6].Text != "&nbsp;")
                        objPayment.MemeberToNumber = Convert.ToInt64(item.Cells[6].Text.Replace(" ", ""));
                    objPayment.parlourid = ParlourId;

                    int SendOpration = client.InsertSendReminder(objPayment);
                    if (SendOpration != 0)

                        SuccessCount++;
                    FailedCount++;
                }
            }
            lblMassageChk1.Text = "Send Reminder Success is " + SuccessCount.ToString() + " and Failed " + (FailedCount - SuccessCount).ToString();
            lblMassageChk1.ForeColor = System.Drawing.Color.Green;
        }
        protected void btnOutProcessPayment_Click(object sender, EventArgs e)
        {
            lblMassageChk1.Text = string.Empty;
            int chkCount = 0;
            string strMemberId = string.Empty;
            //gvOutstanding.Columns[0].Visible = true;
            foreach (GridViewRow item in gvOutstanding.Rows)
            {
                CheckBox chkceck = item.FindControl("SelectCheckBox") as CheckBox;
                if (chkceck.Checked)
                {
                    chkCount++;

                    Label lblVoznikZacetnice = (Label)item.Cells[0].FindControl("lblMemberID");
                    strMemberId = lblVoznikZacetnice.Text;
                    //strMemberId = item.Cells[2].Text;
                }
            }
            if (chkCount == 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "redirectParent('" + strMemberId + "','" + ParlourId + "');", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Warning", "abc('test')", true);'PFS0007','6dcba090-f363-47e6-93f5-6def8f80703e'
                //ClientScript.RegisterStartupScript(GetType(), "Load", "<script type='text/javascript'>window.parent.location.href = '~/Admin/ManageMembersPayment.aspx?Id='" + strMemberId + "' & ParlourId = '" + ParlourId + "'; </script>");
                //Response.Redirect("~/Admin/ManageMembersPayment.aspx?Id=" + strMemberId + "&ParlourId=" + ParlourId);
            }
            else
            {
                lblMassageChk1.Text = "Please select one members";
                lblMassageChk1.ForeColor = System.Drawing.Color.Red;
            }
           // gvOutstanding.Columns[0].Visible = false;
        }
        protected void btnSendReminderDpndc_Click(object sender, EventArgs e)
        {
            lblMassageChk1.Text = string.Empty;
            FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
            SendReminderModel objPayment = new SendReminderModel();
            int SuccessCount = 0;
            int FailedCount = 0;
            foreach (GridViewRow item in gvDepandandants.Rows)
            {
                CheckBox chkceck = item.FindControl("SelectDepCheckBox") as CheckBox;
                if (chkceck.Checked)
                {
                    Label lblText = item.FindControl("lblMemberID") as Label;
                    //objPayment.MemeberID = lblText.Text;
                    objPayment.MemeberID = UserID.ToString();
                    objPayment.MemberData = "Member No : " + item.Cells[2].Text +
                                    ", Name : " + item.Cells[3].Text +
                                    ", Dependent Name : " + item.Cells[6].Text +
                                    ", Age :" + item.Cells[10].Text +
                                    ", Plan Name :" + item.Cells[5].Text +
                                    ", Dependency Type :" + item.Cells[8].Text;
                    if (item.Cells[4].Text != "" && item.Cells[4].Text != null && item.Cells[4].Text != "&nbsp;")
                        objPayment.MemeberToNumber = Convert.ToInt64(item.Cells[4].Text.Replace(" ", ""));
                    objPayment.parlourid = ParlourId;

                    int SendOpration = client.InsertSendReminder(objPayment);
                    if (SendOpration != 0)
                        SuccessCount++;
                    FailedCount++;
                }
            }
            lblMassageChk.Text = "Send Reminder Success is " + SuccessCount.ToString() + " and Failed " + (FailedCount - SuccessCount).ToString();
            lblMassageChk.ForeColor = System.Drawing.Color.Green;
        }
        protected void btnoutstandmember_Click(object sender, EventArgs e)
        {
            lblMassageChk1.Text = string.Empty;
            int chkCount = 0;
            string strMemberId = string.Empty;
            foreach (GridViewRow item in gvOutstanding.Rows)
            {
                CheckBox chkceck = item.FindControl("SelectCheckBox") as CheckBox;
                Label lblText = item.FindControl("lblMemberID") as Label;
                if (chkceck.Checked)
                {
                    chkCount++;
                    strMemberId = lblText.Text.ToString();
                }
            }
            if (chkCount == 1)
            {
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.StoredProcedure;
                com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com.CommandText = "RPTMeberTransactionHistory";
                com.Parameters.Add(new SqlParameter("@MemberID", strMemberId));

                SqlDataAdapter adp = new SqlDataAdapter(com);
                DataSet stset = new DataSet();
                adp.Fill(stset);
                DataTable dt = stset.Tables[0];
                DataTable dt1 = stset.Tables[1];
                divgrid.Visible = false;
                divgvDependendance.Visible = false;
                divrdlc.Visible = true;
                rvMembersByDateRange.Visible = true;
                rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rptMemberTransaction.rdlc";
                rvMembersByDateRange.LocalReport.DataSources.Clear();
                rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsMmbrsTransactionHistory", dt));
                rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DsmemberTransactionList", dt1));
                rvMembersByDateRange.DataBind();

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                ReportName = "Outstanding Payment Between ";
                reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
                reportParameters.Add(new ReportParameter("txtAddress", "SHOP 1 PP SHOPING CENTER 969 KOMA ROAD"));
                rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
                rvMembersByDateRange.LocalReport.Refresh();
            }
            else
            {
                lblMassageChk1.Text = "Please select one members";
                lblMassageChk1.ForeColor = System.Drawing.Color.Red;
            }
        }
        #endregion
        public string CheckedPolicyStates()
        {
            string result = "All";
            return result;
        }
    }
}
