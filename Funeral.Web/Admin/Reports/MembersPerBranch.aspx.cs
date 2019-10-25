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
using Funeral.Web.App_Start;

namespace Funeral.Web.Admin.Reports
{
    public partial class MembersPerBranch : AdminBasePage
	{
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void BindJoinedMembersByDate()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "allmembers";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            com.Parameters.Add(new SqlParameter("@branch", txtBranch.Text));
            com.Parameters.Add(new SqlParameter("@StartDate", null));
            com.Parameters.Add(new SqlParameter("@EndDate", null));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rvMembersByDateRange.Visible = true;
                rvMembersByDateRange.LocalReport.ReportPath = "Reports/JoinedMembersByDate.rdlc";
                rvMembersByDateRange.LocalReport.DataSources.Clear();
                rvMembersByDateRange.LocalReport.DataSources.Add(new ReportDataSource("dsJoinedMembersByDate", dt));
                rvMembersByDateRange.DataBind();

                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("txtReportName", "Members Per Branch " + txtBranch.Text));
                rvMembersByDateRange.LocalReport.SetParameters(reportParameters);
                rvMembersByDateRange.LocalReport.Refresh();
                //ReportDataSource rds = new ReportDataSource("dsChart", ObjectDataSourceJoinedMembersbyDate);

                //ReportDataSource rds2 = new ReportDataSource("DataSet1", ObjectDataSourceJoinedMembersbyDate);
                //rvMembersByDateRange.LocalReport.DataSources.Clear();

                //rvMembersByDateRange.LocalReport.DataSources.Add(rds);

                //rvMembersByDateRange.LocalReport.DataSources.Add(rds2);
            }
            else
            {
                lblMessage.Text = "No Record Found.";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    BindJoinedMembersByDate();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                }
            }
        }
    }
}