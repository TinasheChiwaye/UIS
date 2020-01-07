using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class ScheduleEmailReport : AdminBasePage
    {
        #region Fields

        #endregion
        public int ID
        {
            get
            {
                if (ViewState["_ID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_ID"]);
            }
            set
            {
                ViewState["_ID"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 44;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                BindScheduleDetail();
                ReportList();
            }
        }
        #region ButtonEvent

        public void BindScheduleDetail()
        {
            List<ScheduleEmailReportModel> model = ScheduleEmailReportBAL.GetScheduleEmailReportByParlourId(this.ParlourId).ToList();
            gvSchedule.DataSource = model;
            gvSchedule.DataBind();
        }

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
        public void ClearControl()
        {
            btnSave.Text = "Save";
            ddlfrequency.ClearSelection();
            txtTime.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtDateFrom.Text = string.Empty;
            txtDateTo.Text = string.Empty;
            ddlReort.SelectedIndex = -1;
            ddlReportType.SelectedIndex = -1;


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            if (btnSave.Text == "Save")
            {

                ScheduleEmailReportModel modal = new ScheduleEmailReportModel();
               modal.pkiScheduleId = 0;
                modal.ReportType = ddlReportType.SelectedValue;
                modal.Report = hfAdminReport.Value;
                modal.Frequency = ddlfrequency.SelectedValue;

                if (modal.Frequency == "Custome")
                {
                    modal.DateFrom = Convert.ToDateTime(txtDateFrom.Text);
                    modal.DateTo = Convert.ToDateTime(txtDateTo.Text);
                }
                else
                {
                    
                    modal.DateFrom = new DateTime(1753, 1, 1);
                    modal.DateTo = DateTime.MaxValue;
                }
                modal.Email = txtEmail.Text;
                modal.Time = Convert.ToDateTime(txtTime.Text);
                modal.ParlourId = ParlourId;

                int a = ScheduleEmailReportBAL.SaveScheduleEmailReport(modal);
                ViewState["QuotationID"] = a;
                BindScheduleDetail();
                ClearControl();

                ShowMessage(ref lblMessage, MessageType.Success, "ScheduleEmailReport saved successfully");
            }
            else
            {
                string Scheduleid = hdnScheduleId.Value;
                ScheduleEmailReportModel model = ScheduleEmailReportBAL.GetScheduleById(Convert.ToInt32(Scheduleid), ParlourId);
                model.pkiScheduleId = Convert.ToInt32(Scheduleid);
                model.ReportType = ddlReportType.SelectedValue;
                model.Report = ddlReort.SelectedValue;
                model.Frequency = ddlfrequency.SelectedValue;
                if (model.Frequency == "Custome")
                {
                    model.DateFrom = Convert.ToDateTime(txtDateFrom.Text);
                    model.DateTo = Convert.ToDateTime(txtDateTo.Text);
                }
                else
                {
                     model.DateFrom = new DateTime(1753, 1, 1);
                    model.DateTo = DateTime.MaxValue;
                }
                model.Email = txtEmail.Text;
                model.Time = Convert.ToDateTime(txtTime.Text);
                model.ParlourId = ParlourId;
                int a = ScheduleEmailReportBAL.UpdateScheduleByScheduleId(model);
                ViewState["QuotationID"] = a;
                BindScheduleDetail();
                ClearControl();
                ShowMessage(ref lblMessage, MessageType.Success, "ScheduleEmailReport Update successfully");

            }
            #endregion
        }

        protected void gvSchedule_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteSchedule")
            {
                try
                {
                    ScheduleEmailReportBAL.DeleteScheduleEmailReport(Convert.ToInt32(e.CommandArgument));
                    BindScheduleDetail();
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            else
            {
                int selectedScheduleId = Convert.ToInt32(e.CommandArgument);
                hdnScheduleId.Value = selectedScheduleId.ToString();
                ScheduleEmailReportModel model = ScheduleEmailReportBAL.GetScheduleById(selectedScheduleId, ParlourId);
                ddlfrequency.SelectedValue = model.Frequency;
                if (model.Frequency == "Custome")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hwa1", "ShowTab5();", true);
                }
                txtDateFrom.Text = model.DateFrom.ToString();
                txtDateTo.Text = model.DateTo.ToString();
               txtTime.Text = model.Time.ToString("HH:mm:ss");
                txtEmail.Text = model.Email;
                btnSave.Text = "Update";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
    }
}