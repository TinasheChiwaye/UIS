using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin.Reports
{
    public partial class Reports : AdminBasePage
    {
        private readonly ISiteSettings _siteConfig = new SiteSettings();
        Guid CompanyId;

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 8;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    btnExport.Enabled = this.HasReadRight;
                    ReportList();
                    BindCompanyList(ddlCompanyList, dvAdministrator);
                    CompanyId = ParlourId;
                    BindDdlBranch();
                    BindDdlSociety();
                    BindDdlAgent();
                    BindUnderWriterList();
                    BindCustomDetails();

                }
                catch (Exception ex)
                {
                    //Response.Write(ex.ToString());
                }
            }
        }
        #region Test Code
        public void SSRSReport()
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            ReportViewer rpw = new ReportViewer();
            rpw.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new MyReportServerCredentials();
            rpw.ServerReport.ReportServerCredentials = irsc;

            rpw.ProcessingMode = ProcessingMode.Remote;
            rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
            rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/Unplugg IT Active Parlours";

            //ArrayList reportParam = new ArrayList();
            //reportParam = ReportDefaultPatam();

            //ReportParameter[] param = new ReportParameter[reportParam.Count];
            //for (int k = 0; k < reportParam.Count; k++)
            //{
            //    param[k] = (ReportParameter)reportParam[k];
            //}


            //ReportParameterCollection reportParameters = new ReportParameterCollection();

            //reportParameters.Add(new ReportParameter("parlourid", "6DCBA090-F363-47E6-93F5-6DEF8F80703E"));
            //reportParameters.Add(new ReportParameter("parlourid", "6DCBA090-F363-47E6-93F5-6DEF8F80703E"));
            //reportParameters.Add(new ReportParameter("parlourid", "6DCBA090-F363-47E6-93F5-6DEF8F80703E"));
            //reportParameters.Add(new ReportParameter("parlourid", "6DCBA090-F363-47E6-93F5-6DEF8F80703E"));
            //reportParameters.Add(new ReportParameter("parlourid", "6DCBA090-F363-47E6-93F5-6DEF8F80703E"));
            //reportParameters.Add(new ReportParameter("parlourid", "6DCBA090-F363-47E6-93F5-6DEF8F80703E"));
            //reportParameters.Add(new ReportParameter("parlourid", "6DCBA090-F363-47E6-93F5-6DEF8F80703E"));
            //reportParameters.Add(new ReportParameter("parlourid", "6DCBA090-F363-47E6-93F5-6DEF8F80703E"));

            //ssrsReportViewer1.ServerReport.SetParameters(reportParameters);
            //rpw.ServerReport.SetParameters(param);
            rpw.ServerReport.Refresh();
            byte[] bytes = rpw.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=Monthly BI Stats.pdf");
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download


            //ssrsReportViewer1.ProcessingMode = ProcessingMode.Local;
            //IReportServerCredentials irsc = new MyReportServerCredentials();
            //ssrsReportViewer1.ServerReport.ReportServerCredentials = irsc;

            //ssrsReportViewer1.ProcessingMode = ProcessingMode.Remote;
            //ssrsReportViewer1.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
            //ssrsReportViewer1.ServerReport.Refresh();
        }
        public void Test()
        {

            //
            ReportViewer rpw = new ReportViewer();
            rpw.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new MyReportServerCredentials();
            rpw.ServerReport.ReportServerCredentials = irsc;

            rpw.ProcessingMode = ProcessingMode.Remote;
            rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
            rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/" + ddlAdminReort.SelectedItem.Text;

            //

            var p_PayPeriod = new ReportParameter("parlourid", "F7CAF4C0-B3AB-44E0-8C8A-76AA4E8CDA6D");

            //reportParameters.Add(new ReportParameter("parlourid", "F7CAF4C0-B3AB-44E0-8C8A-76AA4E8CDA6D"));
            //rpw.ServerReport.SetParameters(p_PayPeriod);           
            //
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string filename;

            byte[] bytes = rpw.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            filename = string.Format("{0}.{1}", ddlAdminReort.SelectedItem.Text, "pdf");
            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = mimeType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
        #endregion

        #region Bind Dropdown
        public void ReportList()
        {
            // string[] Policystatuslist = client.GetDistinctPolicyStatusList();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "SelectReportList";
            com.Parameters.Add(new SqlParameter("@FinanceRole", false));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //DataRow[] results = dt.Select("category = 'Finance Report'");
            if (dt.Rows.Count > 0)
            {
                ddlReportType.DataSource = dt;
                ddlReportType.DataTextField = "category";
                ddlReportType.DataValueField = "category";
                ddlReportType.DataBind();

                ddlReportType.Items.Insert(0, new ListItem("Select Report Type", "0"));
                ddlReportType.Items.FindByValue("0").Selected = true;
            }


        }
        public void BindDdlBranch()
        {
            //var ParlourId= ddlCompanyList.SelectedItem;
            ddlBranch.DataSource = ToolsSetingBAL.GetAllBranches(CompanyId);
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("", ""));
            ddlBranch.Items.Insert(1, new ListItem("ALL", "ALL"));
            // ddlBranch.Items.FindByText("ALL").Selected = true;

            //ddlMemberBranch.DataSource = client.GetAllBranch(ParlourId);
            //ddlMemberBranch.DataBind();
            //ddlMemberBranch.Items.Insert(0, new ListItem("ALL", "ALL"));
            //ddlMemberBranch.Items.FindByText("ALL").Selected = true;

            //ddlMemberPerBranch.DataSource = client.GetAllBranch(ParlourId);
            //ddlMemberPerBranch.DataBind();
            //ddlMemberPerBranch.Items.Insert(0, new ListItem("ALL", "ALL"));
            //ddlMemberPerBranch.Items.FindByText("ALL").Selected = true;
        }
        public void BindDdlSociety()
        {
            ddlSociety.DataSource = ToolsSetingBAL.GetAllSocietye(CompanyId);
            ddlSociety.DataBind();
            ddlSociety.Items.Insert(0, new ListItem("", ""));
            ddlSociety.Items.Insert(1, new ListItem("ALL", "ALL"));
            //ddlSociety.Items.FindByText("ALL").Selected = true;
        }
        public void BindDdlAgent()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "SelectUsers";
            com.Parameters.Add(new SqlParameter("@Parlourid", CompanyId));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlAgent.DataSource = dt;
                ddlAgent.DataTextField = "Agent";
                ddlAgent.DataValueField = "Agent";
                ddlAgent.DataBind();
                ddlAgent.Items.Insert(0, new ListItem("", ""));
                ddlAgent.Items.Insert(1, new ListItem("ALL", "ALL"));

            }

        }
        public void BindUnderWriterList()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "SelectDistinctUnderwriterPlans";
            com.Parameters.AddWithValue("@ParlourId", CompanyId);
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlUnderwriter.DataSource = dt;
                ddlUnderwriter.DataTextField = "PlanUnderwriter";
                ddlUnderwriter.DataValueField = "PlanUnderwriter";
                ddlUnderwriter.DataBind();
                ddlUnderwriter.Items.Insert(0, new ListItem("", ""));
                ddlUnderwriter.Items.Insert(1, new ListItem("All", "All"));
                // ddlUnderwriter.Items.FindByText("All").Selected = true;
            }

        }
        #endregion

        #region ButtonClickEvent
        protected void ExportClickEvent(object sender, EventArgs e)
        {
            try
            {
                ReportViewer rpw = new ReportViewer();
                rpw.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new MyReportServerCredentials();
                rpw.ServerReport.ReportServerCredentials = irsc;


                rpw.ProcessingMode = ProcessingMode.Remote;
                rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/" + hfAdminReport.Value;
                ReportParameterCollection reportParameters = new ReportParameterCollection();


                if (chk_dateDisabled.Checked)
                {
                    if (hfAdminReport.Value.Contains("Daily"))
                    {
                        reportParameters.Add(new ReportParameter("DateFrom", DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                        reportParameters.Add(new ReportParameter("DateTo", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    }
                    else
                    {
                        reportParameters.Add(new ReportParameter("DateFrom", DateTime.Now.AddYears(-100).ToString("yyyy-MM-dd HH:mm:ss.fff")));
                        reportParameters.Add(new ReportParameter("DateTo", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtDateFrom.Text))
                        reportParameters.Add(new ReportParameter("DateFrom", txtDateFrom.Text));
                    if (!string.IsNullOrEmpty(txtDateTo.Text))
                        reportParameters.Add(new ReportParameter("DateTo", txtDateTo.Text));
                }

                if (IsAdministrator != true)
                {
                    if (!string.IsNullOrEmpty(ddlBranch.SelectedItem.Text))
                        reportParameters.Add(new ReportParameter("Branch", ddlBranch.SelectedItem.Text));
                }

                if (!string.IsNullOrEmpty(ddlSociety.SelectedItem.Text))
                    reportParameters.Add(new ReportParameter("Society", ddlSociety.SelectedItem.Text));
                if (!string.IsNullOrEmpty(ddlAgent.SelectedItem.Text))
                    reportParameters.Add(new ReportParameter("Agent", ddlAgent.SelectedItem.Text));
                if (!string.IsNullOrEmpty(ddlUnderwriter.SelectedItem.Text))
                    reportParameters.Add(new ReportParameter("Underwriter", ddlUnderwriter.SelectedItem.Text));
                if (!string.IsNullOrEmpty(ddlMethod.SelectedItem.Text))
                    reportParameters.Add(new ReportParameter("PaymentType", ddlMethod.SelectedItem.Text));
                if (!string.IsNullOrEmpty(ddlPolicyStatus.SelectedItem.Text))
                    reportParameters.Add(new ReportParameter("PolicyStatus", ddlPolicyStatus.SelectedItem.Text));

                if (ddlCustom1.SelectedIndex != -1)
                    reportParameters.Add(new ReportParameter("CustomId1", ddlCustom1.SelectedValue));
                if (ddlCustom2.SelectedIndex != -1)
                    reportParameters.Add(new ReportParameter("CustomId2", ddlCustom2.SelectedValue));
                if (ddlCustom3.SelectedIndex != -1)
                    reportParameters.Add(new ReportParameter("CustomId3", ddlCustom3.SelectedValue));

                if (IsAdministrator == true)
                {
                    reportParameters.Add(new ReportParameter("Parlourid", ddlCompanyList.SelectedValue.ToString()));
                }
                else
                {
                    reportParameters.Add(new ReportParameter("Parlourid", this.ParlourId.ToString()));
                }

                if (hfAdminReport.Value.Contains("Cash"))
                {
                    reportParameters.Add(new ReportParameter("UserId", this.UserID.ToString()));
                }


                rpw.ServerReport.SetParameters(reportParameters);
                //
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                string filename;
                //
                string ExportTypeName = rptExportType.SelectedItem.Text;
                string ExportTypeExtensions;
                if (ExportTypeName == "PDF")
                    ExportTypeExtensions = "pdf";
                else if (ExportTypeName == "Word")
                    ExportTypeExtensions = "docx";
                else
                    ExportTypeExtensions = "xls";
                //

                byte[] bytes = rpw.ServerReport.Render(ExportTypeName, null, out mimeType, out encoding, out extension, out streamids, out warnings);
                filename = string.Format("{0}.{1}", hfAdminReport.Value, ExportTypeExtensions);
                //MailSend
                if (!string.IsNullOrEmpty(txtcemail.Text))
                {
                    MemoryStream s = new MemoryStream(bytes);
                    s.Seek(0, SeekOrigin.Begin);
                    Attachment a = new Attachment(s, filename);
                    MailMessage message = new MailMessage(ConfigurationManager.AppSettings["ReportEmailSenderId"].ToString().Trim(), txtcemail.Text.Trim(), hfAdminReport.Value, "");
                    message.Attachments.Add(a);
                    SmtpClient client = new SmtpClient();
                    //ServicePointManager.ServerCertificateValidationCallback = delegate(object d, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    client.Send(message);
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Email Sent Successfully.";
                }
                //
                else
                {
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                    Response.ContentType = mimeType;
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }
        }
        #endregion

        #region Custom Field Impementation
        #region Bind Custom Detailss
        private void BindCustomDetails()
        {
            var custom1 = CustomDetailsBAL.GetAllCustomDetailsByParlourId(this.CompanyId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.Custom1));
            var custom2 = CustomDetailsBAL.GetAllCustomDetailsByParlourId(this.CompanyId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.Custom2));
            var custom3 = CustomDetailsBAL.GetAllCustomDetailsByParlourId(this.CompanyId, Convert.ToInt32(CustomDetailsEnums.CustomDetailsType.Custom3));
            ddlCustom1.DataSource = custom1;
            ddlCustom1.DataTextField = "Name";
            ddlCustom1.DataValueField = "Id";
            ddlCustom1.DataBind();
            ddlCustom1.Items.Insert(0, new ListItem("All", "0"));

            ddlCustom2.DataSource = custom2;
            ddlCustom2.DataTextField = "Name";
            ddlCustom2.DataValueField = "Id";
            ddlCustom2.DataBind();
            ddlCustom2.Items.Insert(0, new ListItem("All", "0"));

            ddlCustom3.DataSource = custom3;
            ddlCustom3.DataTextField = "Name";
            ddlCustom3.DataValueField = "Id";
            ddlCustom3.DataBind();
            ddlCustom3.Items.Insert(0, new ListItem("All", "0"));

        }
        #endregion

        #endregion

        protected void ddlCompanyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                CompanyId = new Guid(ddlCompanyList.SelectedValue);
                BindDdlBranch();
                BindDdlSociety();
                BindDdlAgent();
                BindUnderWriterList();
                BindCustomDetails();
            }
        }
    }
}