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
using Funeral.BAL;

namespace Funeral.Web.Admin.Reports
{
    public partial class ReportGenerator : AdminBasePage
    {
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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindStatusPolicyList();
                BindUnderWriterList();
                BindDdlBranch();
                BindDdlPlanName();
                // SelectReportPanel();
            }
            else
            {
                lblMessage.Text = string.Empty;
            }
        }
        #region Methods

        public void BindStatusPolicyList()
        {
            //     string[] Policystatuslist = client.GetDistinctPolicyStatusList();
            //SqlCommand com = new SqlCommand();
            //com.CommandType = CommandType.StoredProcedure;
            //com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            //com.CommandText = "GetDistinctPolicyStatusList";
            //SqlDataAdapter adp = new SqlDataAdapter(com);
            //DataTable dt = new DataTable();
            //adp.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    chklPolicyStatus.DataSource = dt;
            //    chklPolicyStatus.DataTextField = "PolicyStatus";
            //    chklPolicyStatus.DataValueField = "PolicyStatus";
            //    chklPolicyStatus.DataBind();
            //    chklPolicyStatus.Items.Insert(0, new ListItem("All", "All"));
            //    chklPolicyStatus.Items.FindByText("All").Selected = true;
            //    //Bind PolicyStatus DDl
            //    ddlPolicyStatus.DataSource = dt;
            //    ddlPolicyStatus.DataTextField = "PolicyStatus";
            //    ddlPolicyStatus.DataValueField = "PolicyStatus";
            //    ddlPolicyStatus.DataBind();
            //    ddlPolicyStatus.Items.Insert(0, new ListItem("All", "All"));
            //}

        }
        public void BindUnderWriterList()
        {
            // string[] Policystatuslist = client.GetDistinctPolicyStatusList();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "SelectDistinctUnderwriterPlans";
            com.Parameters.AddWithValue("@ParlourId", ParlourId);
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlUnderwriter.DataSource = dt;
                ddlUnderwriter.DataTextField = "PlanUnderwriter";
                ddlUnderwriter.DataValueField = "PlanUnderwriter";
                ddlUnderwriter.DataBind();
                ddlUnderwriter.Items.Insert(0, new ListItem("All", "All"));
                ddlUnderwriter.Items.FindByText("All").Selected = true;
                //Bind PolicyStatus DDl
            }

        }
        public void BindDdlBranch()
        {
            ddlBranch.DataSource = ToolsSetingBAL.GetAllBranches(ParlourId);
            ddlBranch.DataBind();

            ddlMemberBranch.DataSource = ToolsSetingBAL.GetAllBranches(ParlourId);
            ddlMemberBranch.DataBind();
        }
        public void BindDdlPlanName()
        {
            ddlPlanname.DataSource = CommonBAL.GetAllPlanName(ParlourId);
            ddlPlanname.DataBind();

            ddlMemberPlan.DataSource = CommonBAL.GetAllPlanName(ParlourId);
            ddlMemberPlan.DataBind();

            //ddlOutstandPlanName.DataSource = client.GetAllPkanName();
            //ddlOutstandPlanName.DataBind();
           // ddlOutstandPlanName.Items.Insert(-1, "All");
        }
        public void SelectReportPanel()
        {
            switch (ReportName)
            {
                case "allmembers":
                    pnlReportSetter.Visible = false;
                    BindAllJoinedMembers();
                    break;
                case "membersjoinedbydate":
                    pnlMembersByJoinedDate.Visible = true;
                    break;
                case "policystatus":
                    pnlPolicyStatus.Visible = true;
                    break;
                case "membersperbranch":
                    pnlMembersPerBranch.Visible = true;
                    break;
                case "membersperplan":
                    pnlMembersPerPlan.Visible = true;
                    break;
                case "membersunderwriting":
                    pnlMembersUnderWriting.Visible = true;
                    break;
                default:
                    break;

            }
        }
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
            //   if (dt.Rows.Count > 0)
            // {
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "All Members Joined";
            reportParameters.Add(new ReportParameter("txtReportName", ReportName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
            //ReportDataSource rds = new ReportDataSource("dsChart", ObjectDataSourceJoinedMembersbyDate);

            //ReportDataSource rds2 = new ReportDataSource("DataSet1", ObjectDataSourceJoinedMembersbyDate);
            //rvMembersByDateRange.LocalReport.DataSources.Clear();

            //rvMembersByDateRange.LocalReport.DataSources.Add(rds);

            //rvMembersByDateRange.LocalReport.DataSources.Add(rds2);
            /*  }
              else
              {
                  lblMessage.Text = "No Record Found.";
              }*/
        }
        public string CheckedPolicyStates()
        {
            string result = string.Empty;
            //foreach (ListItem li in chklPolicyStatus.Items)
            //{
            //    if (li.Selected)
            //    {
            //        result = result + li.Text.Trim() + ",";
            //        if (li.Text == "All")
            //        {
            //            foreach (ListItem lis in chklPolicyStatus.Items)
            //            {
            //                result = result + lis.Text.Trim() + ",";
            //            }

            //        }
            //    }
            //}
            //result = result.TrimEnd(',');
            return result;
        }
        public void BindJoinedMembersByDate()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTallmembers";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@branch", ddlMemberBranch.SelectedItem.Text));
            com.Parameters.Add(new SqlParameter("@StartDate", Convert.ToDateTime(txtDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@EndDate", Convert.ToDateTime(txtDateTo.Text)));
            com.Parameters.Add(new SqlParameter("@MemberPlan", ddlMemberPlan.SelectedItem.Text));
            com.Parameters.Add(new SqlParameter("@PolicyStatusList", CheckedPolicyStates()));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Members Per Branch " + ddlMemberBranch.SelectedItem.Text + " and Per Plan " + ddlMemberPlan.SelectedItem.Text + " Between " + txtDateFrom.Text + " And " + txtDateTo.Text;
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

        public void BindPolicyStatus()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTallmembers";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@branch", "All"));
            com.Parameters.Add(new SqlParameter("@StartDate", Convert.ToDateTime(txtPolicyDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@EndDate", Convert.ToDateTime(txtPolicyDateTo.Text)));
            com.Parameters.Add(new SqlParameter("@PolicyStatus", ddlPolicyStatus.SelectedItem.Text.Trim()));
            com.Parameters.Add(new SqlParameter("@PolicyStatusList", CheckedPolicyStates()));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Members By PolicyStatus Matching with '" + ddlPolicyStatus.SelectedItem.Text + "'";
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
            //    rvMembersByDateRange.LocalReport.DataSources.Clear();
            //    rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            //    rvMembersByDateRange.DataBind();

            //    lblMessage.Text = "No Record Found.";
            //}
        }
        public void BindMembersByBranch()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTallmembers";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@branch", ddlBranch.SelectedItem.Text));
            com.Parameters.Add(new SqlParameter("@StartDate", null));
            com.Parameters.Add(new SqlParameter("@EndDate", null));
            com.Parameters.Add(new SqlParameter("@PolicyStatus", null));
            com.Parameters.Add(new SqlParameter("@PolicyStatusList", CheckedPolicyStates()));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Members Per Branch " + ddlBranch.SelectedItem.Text;
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

        public void BindMembersPerPlan()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTallmembers";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@branch", "All"));
            com.Parameters.Add(new SqlParameter("@StartDate", null));
            com.Parameters.Add(new SqlParameter("@EndDate", null));
            com.Parameters.Add(new SqlParameter("@MemberPlan", ddlPlanname.SelectedItem.Text));
            com.Parameters.Add(new SqlParameter("@PolicyStatusList", CheckedPolicyStates()));
            //com.Parameters.Add(new SqlParameter("@Plan", txtMemberPlan.Text.Trim()));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Members Per Plan " + ddlPlanname.SelectedItem.Text;
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
        public void BindMembersUnderwriting()
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
            com.Parameters.Add(new SqlParameter("@Underwriterplan", ddlUnderwriter.SelectedItem.Text.Trim()));
            //  com.Parameters.Add(new SqlParameter("@Plan",null)); 
            //com.Parameters.Add(new SqlParameter("@MemberUnderWriting", txtMemberUnderWriting .Text.Trim()));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/JoinedMembersByDate.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Members with UnderWriting Plan " + ddlUnderwriter.SelectedItem.Text;
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
        public void BindAgentReport()
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
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rptAgentReport.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DsRptAgentReport", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Agent Report  for " + txtAgent.Text + "  between " + txtAgentDateFrom.Text + " And " + txtAgentDateTo.Text ;
            reportParameters.Add(new ReportParameter("txtHederRendor", ReportName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindbtnFuneralArrangement()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "RPTFuneralArrangement";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(txtFuneralArrangmentDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(txtFuneralArrangmentDateTo.Text)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rptFuneralArrangement.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DSRptFuneralArrangement", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            //ReportName = "Agent Report  for " + txtAgent.Text + "  between " + txtAgentDateFrom.Text + " And " + txtAgentDateTo.Text;
            //reportParameters.Add(new ReportParameter("txtHederRendor", ReportName));
            //rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();


        }
        public void BindOutstandingPayment()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "rptOutStandingGroupPayment";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@StartDate", Convert.ToDateTime(txtOutstandingPaymentDateFrom.Text)));
            com.Parameters.Add(new SqlParameter("@EndDate", Convert.ToDateTime(txtOutstandingPaymentDateTo.Text)));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            rvMembersByDateRange.Visible = true;
            rvMembersByDateRange.LocalReport.ReportPath = "admin/Reports/ReportLayouts/rptOutstandingPayment.rdlc";
            rvMembersByDateRange.LocalReport.DataSources.Clear();
            rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("DsRdlcOutstandingPayment", dt));
            rvMembersByDateRange.DataBind();

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            ReportName = "Outstanding Payment Between " + txtOutstandingPaymentDateFrom.Text + " And " + txtOutstandingPaymentDateTo.Text;
            reportParameters.Add(new ReportParameter("txtHederName", ReportName));
            rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
            rvMembersByDateRange.LocalReport.Refresh();
            
        }

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
                case "btnPnlAllJoinedMemebrs":
                    BindAllJoinedMembers();
                    break;
                case "btnPnlMembersByJoinedDate":
                    BindJoinedMembersByDate();
                    break;
                case "btnPnlPolicyStatus":
                    BindPolicyStatus();
                    break;
                case "btnPnlMembersPerBranch":
                    BindMembersByBranch();
                    break;
                case "btnPnlMembersPerPlan":
                    BindMembersPerPlan();
                    break;
                case "btnPnlMembersUnderWriting":
                    BindMembersUnderwriting();
                    break;
                case "btnAgentReport":
                    BindAgentReport();
                    break;
                case "btnFuneralArrangement":
                    BindbtnFuneralArrangement();
                    break;
                case "btnoutstandingPayment":
                    BindOutstandingPayment();
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
                    switch (((HtmlButton)sender).ID)
                    {

                        case "btnPnlAllJoinedMemebrs":
                            btnSendMail.CommandName = "btnPnlAllJoinedMemebrs";
                            BindAllJoinedMembers();
                            break;
                        case "btnPnlMembersByJoinedDate":
                            btnSendMail.CommandName = "btnPnlMembersByJoinedDate";
                            BindJoinedMembersByDate();
                            break;
                        case "btnPnlPolicyStatus":
                            btnSendMail.CommandName = "btnPnlPolicyStatus";
                            BindPolicyStatus();
                            break;
                        case "btnPnlMembersPerBranch":
                            btnSendMail.CommandName = "btnPnlMembersPerBranch";
                            BindMembersByBranch();
                            break;
                        case "btnPnlMembersPerPlan":
                            btnSendMail.CommandName = "btnPnlMembersPerPlan";
                            BindMembersPerPlan();
                            break;
                        case "btnPnlMembersUnderWriting":
                            btnSendMail.CommandName = "btnPnlMembersUnderWriting";
                            BindMembersUnderwriting();
                            break;
                        case "btnAgentReport":
                            btnSendMail.CommandName = "btnAgentReport";
                            BindAgentReport();
                            break;
                        case "btnFuneralArrangement":
                            btnSendMail.CommandName = "btnAgentReport";
                            BindbtnFuneralArrangement();
                            break;
                        case "btnoutstandingPayment":
                            btnSendMail.CommandName = "btnoutstandingPayment";
                            BindOutstandingPayment();
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
        #endregion

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

    }
}