using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Security.Principal;
using System.Net;
using Funeral.Model;
using Funeral.Web.App_Start;

namespace Funeral.Web
{
    public partial class SSRSReport : System.Web.UI.Page
    {
        private readonly ISiteSettings _siteConfig = new SiteSettings();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ssrsReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    IReportServerCredentials irsc = new MyReportServerCredentials();
                    ssrsReportViewer1.ServerReport.ReportServerCredentials = irsc;

                    ssrsReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    ssrsReportViewer1.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                    ssrsReportViewer1.ServerReport.ReportPath = "/Unplugg IT Solution BI Reporting/UIS_RPT_AllMembersReport";
                    ssrsReportViewer1.ServerReport.Refresh();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }
    }
}