using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin.Reports
{
    public partial class AllReportGenerator : AdminBasePage
    {
        public bool FinanceRole
        {
            get
            {
                if (ViewState["_FinanceRole"] == null)
                    return true;
                else
                    return Convert.ToBoolean(ViewState["_FinanceRole"]);
            }
            set
            {

                ViewState["_FinanceRole"] = value;
            }
        }

        #region PageInit
        protected void Page_Init(object sender, EventArgs e)
        {
            List<SecureUserGroupsModel> obj = ToolsSetingBAL.EditSecurUserbyID(UserID);
            List<int> list = new List<int>();
            list.Add(2);
            list.Add(4);
            list.Add(12);
            var result = obj.Where(x => list.Contains(x.fkiSecureGroupID));
            if (result.FirstOrDefault() != null)
            {
                FinanceRole = false;
            }

        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                ReportList();
                RenderReportType();
                BindDdlBranch();
                BindDdlSociety();
                BindDdlPlanName();
                BindUnderWriterList();
                BindDdlAgent();
                BindDdlUnderwriter();
            }
            else
            {
                lblMessage.Text = string.Empty;
            }


        }

        #region DropDownd Page Load Binding
        public void ReportList()
        {
            // string[] Policystatuslist = client.GetDistinctPolicyStatusList();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "SelectReportList";
            com.Parameters.Add(new SqlParameter("@FinanceRole", FinanceRole));
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
        //public void BindAdminReportPanel(string strddlCall)
        //{
        //    try
        //    {
        //        if (strddlCall == "Main")
        //        {
        //            SqlCommand com = new SqlCommand();
        //            com.CommandType = CommandType.StoredProcedure;
        //            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
        //            com.CommandText = "SelectReportList";
        //            com.Parameters.Add(new SqlParameter("@ReportType", ddlReportType.SelectedItem.Text));
        //            SqlDataAdapter adp = new SqlDataAdapter(com);
        //            DataTable dt = new DataTable();
        //            adp.Fill(dt);

        //            if (dt.Rows.Count > 0)
        //            {
        //                ddlAdminReort.DataSource = dt;
        //                ddlAdminReort.DataTextField = "ReportName";
        //                ddlAdminReort.DataValueField = "ReportName";
        //                ddlAdminReort.DataBind();

        //                ddlAdminReort.Items.Insert(0, new ListItem("Select Report", "0"));
        //                ddlAdminReort.Items.FindByValue("0").Selected = true;
        //            }
        //        }

        //        ClearAllPanels();
        //        string Report = ddlAdminReort.SelectedItem.Value.Replace(" ", "");
        //        if (Report != "0")
        //            ((Panel)pnlAdminReport.FindControl("pnl" + Report)).Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //        lblMessage.ForeColor = System.Drawing.Color.Red;
        //    }
        //}
        public void RenderReportType()
        {
            //ClearAllPanels();
            //pnlFinanceReport.Visible = false;
            //pnlAdminReport.Visible = false;
            //try
            //{
            //    string Report = ddlReportType.SelectedItem.Value.Replace(" ", "");
            //    if (Report != "0")
            //    {
            //        ((Panel)(pnlReportSetter.FindControl("pnl" + Report))).Visible = true;
            //        BindAdminReportPanel("Main");
            //    }
            //    else
            //    {
            //        ddlAdminReort.ClearSelection();
            //        ddlAdminReort.Items.Insert(0, new ListItem("Select Report", "0"));
            //        ddlAdminReort.Items.FindByValue("0").Selected = true;
            //    }
            //}
            //catch
            //{

            //}
        }
        //public void BindDdlExpenseBranch()
        //{
        //    // string[] Policystatuslist = client.GetDistinctPolicyStatusList();
        //    SqlCommand com = new SqlCommand();
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
        //    com.CommandText = "SelectDistinctExpensesBranchList";
        //    SqlDataAdapter adp = new SqlDataAdapter(com);
        //    DataTable dt = new DataTable();
        //    adp.Fill(dt);
        //    if (dt.Rows.Count > 0)
        //    {
        //        ddlExpenseBranch.DataSource = dt;
        //        ddlExpenseBranch.DataTextField = "ExpenseBranch";
        //        ddlExpenseBranch.DataValueField = "ExpenseBranch";
        //        ddlExpenseBranch.DataBind();
        //        ddlExpenseBranch.Items.Insert(0, new ListItem("All", "All"));
        //        ddlExpenseBranch.Items.FindByText("All").Selected = true;
        //    }

        //}
        public void BindDdlBranch()
        {
            
            ddlBranch.DataSource = ToolsSetingBAL.GetAllBranches(ParlourId);
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("ALL", "ALL"));
            ddlBranch.Items.FindByText("ALL").Selected = true;

            ddlMemberBranch.DataSource = ToolsSetingBAL.GetAllBranches(ParlourId);
            ddlMemberBranch.DataBind();
            ddlMemberBranch.Items.Insert(0, new ListItem("ALL", "ALL"));
            ddlMemberBranch.Items.FindByText("ALL").Selected = true;

            ddlMemberPerBranch.DataSource = ToolsSetingBAL.GetAllBranches(ParlourId);
            ddlMemberPerBranch.DataBind();
            ddlMemberPerBranch.Items.Insert(0, new ListItem("ALL", "ALL"));
            ddlMemberPerBranch.Items.FindByText("ALL").Selected = true;
        }
        public void BindDdlSociety()
        {
            ddlSociety.DataSource = ToolsSetingBAL.GetAllSocietye(ParlourId);
            ddlSociety.DataBind();
            ddlSociety.Items.Insert(0, new ListItem("ALL", "ALL"));
            ddlSociety.Items.FindByText("ALL").Selected = true;
        }
        public void BindDdlPlanName()
        {
            ddlPlanname.DataSource = CommonBAL.GetAllPlanName(ParlourId);
            ddlPlanname.DataBind();
            ddlPlanname.Items.Insert(0, new ListItem("ALL", "ALL"));
            ddlPlanname.Items.FindByText("ALL").Selected = true;

            ddlMemberPlan.DataSource = CommonBAL.GetAllPlanName(ParlourId);
            ddlMemberPlan.DataBind();
            ddlMemberPlan.Items.Insert(0, new ListItem("ALL", "ALL"));
            ddlMemberPlan.Items.FindByText("ALL").Selected = true;
        }
        public void BindUnderWriterList()
        {
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
            }

        }
        public void BindDdlAgent()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "SelectDistinctAgent";
            com.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlAgent.DataSource = dt;
                ddlAgent.DataTextField = "Agent";
                ddlAgent.DataValueField = "Agent";
                ddlAgent.DataBind();
                ddlAgent.Items.Insert(0, new ListItem("ALL", "ALL"));
                ddlAgent.Items.FindByText("ALL").Selected = true;


                ddlAgentComition.DataSource = dt;
                ddlAgentComition.DataTextField = "Agent";
                ddlAgentComition.DataValueField = "Agent";
                ddlAgentComition.DataBind();
                ddlAgentComition.Items.Insert(0, new ListItem("ALL", "ALL"));
                ddlAgentComition.Items.FindByText("ALL").Selected = true;
            }

        }
        public void BindDdlUnderwriter()
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "SelectDistinctUnderwriter";
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlUnderwriterPayment.DataSource = dt;
                ddlUnderwriterPayment.DataTextField = "UnderWriter";
                ddlUnderwriterPayment.DataValueField = "UnderWriter";
                ddlUnderwriterPayment.DataBind();
                ddlUnderwriterPayment.Items.Insert(0, new ListItem("ALL", "ALL"));
                ddlUnderwriterPayment.Items.FindByText("ALL").Selected = true;

            }

        }
        #endregion

        #region Events
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string ReportName = ((HtmlButton)sender).ID.ToString();



            if (Page.IsValid)
            {
                try
                {
                    switch (((HtmlButton)sender).ID)
                    {

                        case "btnPnlAllJoinedMemebrs":
                            //btnSendMail.CommandName = "btnPnlAllJoinedMemebrs";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName);
                            break;
                        case "btnPnlMembersByJoinedDate":
                            //btnSendMail.CommandName = "btnPnlMembersByJoinedDate";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindJoinedMembersByDate=" + ddlMemberBranch.SelectedItem.Text + "," + txtDateFrom.Text + "," + txtDateTo.Text + "," + ddlMemberPlan.SelectedItem.Text);
                            break;
                        case "btnPnlPolicyStatus":
                            //btnSendMail.CommandName = "btnPnlPolicyStatus";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindPolicyStatus=" + txtPolicyDateFrom.Text + "," + txtPolicyDateTo.Text + "," + ddlPolicyStatus.SelectedItem.Text.Trim());
                            break;
                        case "btnPnlMembersPerBranch":
                            //btnSendMail.CommandName = "btnPnlMembersPerBranch";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindMembersByBranch=" + ddlMemberPerBranch.SelectedItem.Text);
                            break;

                        case "btnPnlMembersPerPlan":
                            //btnSendMail.CommandName = "btnPnlMembersPerPlan";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindMembersPerPlan=" + ddlPlanname.SelectedItem.Text);
                            break;
                        case "btnPnlMembersUnderWriting":
                            //btnSendMail.CommandName = "btnPnlMembersUnderWriting";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindMembersUnderwriting=" + ddlUnderwriter.SelectedItem.Text);
                            break;
                        case "btnAgentReport":
                            //btnSendMail.CommandName = "btnAgentReport";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindAgentReport=" + ddlAgent.SelectedItem.Text + "," + txtAgentAdminDateFrom.Text + "," + txtAgentAdminDateTo.Text);
                            break;
                        case "btnFuneralArrangement":
                            //btnSendMail.CommandName = "btnAgentReport";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindbtnFuneralArrangement=" + txtFuneralArrangmentDateFrom.Text + "," + txtFuneralArrangmentDateTo.Text);
                            break;
                        case "btnoutstandingPayment":
                            //btnSendMail.CommandName = "btnoutstandingPayment";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName);
                            break;
                        case "btnDepandantsOver":
                            //btnSendMail.CommandName = "btnDepandantsOver";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName);
                            break;

                        //Finance Report cases
                        case "btnAgent":
                            //btnSendMail.CommandName = "btnAgent";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindAgent=" + ddlAgentComition.SelectedItem.Text + "," + txtAgentDateFrom.Text + "," + txtAgentDateTo.Text);
                            break;
                        //case "btnCashofBank":
                        //    //btnSendMail.CommandName = "btnCashofBank";
                        //    iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindCashofBank=" + txtBankPaymentDateFrom.Text + "," + txtBankPaymentDateTo.Text);
                        //    break;
                        case "btnClaims":
                            //btnSendMail.CommandName = "btnClaims";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindClaims=" + txtClaimDateFrom.Text + "," + txtClaimDateTo.Text + "," + ddlClaims.SelectedItem.Text);
                            break;
                        case "btnDetailedPayment":
                            //btnSendMail.CommandName = "btnDetailedPayment";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindDetailedPayment=" + ddlMethod.SelectedItem.Text + "," + txtDatailedPaymentDatefrom.Text + "," + txtDatailedPaymentDateTo.Text);
                            break;
                        case "btnExpenses":
                            //btnSendMail.CommandName = "btnExpenses";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindExpense=" + txtExpensDateFrom.Text + "," + txtExpensDateTo.Text);
                            break;
                        case "btnFuneralPayment":
                            //btnSendMail.CommandName = "btnFuneralPayment";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindFunerlPayment=" + txtFFPDateFrom.Text + "," + txtFFPDateTo.Text);
                            break;
                        case "btnBranch":
                            //btnSendMail.CommandName = "btnBranch";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindPaymentBranch=" + ddlBranch.SelectedItem.Text + "," + txtBranchDateFrom.Text + "," + txtBranchDateTo.Text);
                            break;
                        case "btnPaymentbyDate":
                            //btnSendMail.CommandName = "btnPaymentbyDate";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindPaymentDate=" + txtPaymentbyDateFrom.Text + "," + txtPaymentbyDateTo.Text);
                            break;
                        case "btnSocietty":
                            //btnSendMail.CommandName = "btnSocietty";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindPaymentSocietty=" + ddlSociety.SelectedItem.Text + "," + txtsocietyDateFrom.Text + "," + txtsocietyDateTo.Text);
                            break;
                        case "btnQuoteByDate":
                            //btnSendMail.CommandName = "btnQuoteByDate";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindQuotationByDate=" + txtQuotationDateFrom.Text + "," + txtQuotationDateTo.Text);
                            break;
                        case "btnUnderwriter":
                            //btnSendMail.CommandName = "btnUnderwriter";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindUnderwriterPayment=" + ddlUnderwriterPayment.SelectedItem.Text + "," + txtUnderwriterDateFrom.Text + "," + txtUnderwriterDateTo.Text);
                            break;
                        case "btnMyCashUp":
                            //btnSendMail.CommandName = "btnMyCashUp";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName + "&BindMyCashUp=" + txtDateMyCashUpFrom.Text + "," + txtDateMyCashUpTo.Text);
                            break;
                        case "btnBranches":
                            //btnSendMail.CommandName = "btnPnlAllJoinedMemebrs";
                            iframe1.Src = ResolveUrl("~/Admin/Reports/reportrdlc.aspx?Type=" + ReportName);
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
        public List<ApplicationSettingsModel> GetImage()
        {
            List<ApplicationSettingsModel> plist = new List<ApplicationSettingsModel>();

            ApplicationSettingsModel modelCompany;
            modelCompany = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            string base64String = Convert.ToBase64String(modelCompany.ApplicationLogo, 0, modelCompany.ApplicationLogo.Length);
            var Imgs = "data:image/png;base64," + base64String;
            foreach (var Img in Imgs)
            {
                plist.Add(new ApplicationSettingsModel { parlourid = modelCompany.parlourid, ApplicationLogo = modelCompany.ApplicationLogo });
            }
            return plist;
        }

        protected void ddlAdminReort_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.rvMembersByDateRange.Reset();
            // BindAdminReportPanel("Chieled");
        }
        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.rvMembersByDateRange.Reset();
            RenderReportType();
        }
        #endregion



        //public void ClearAllPanels()
        //{
        //    foreach (Control c in pnlAdminReport.Controls)
        //    {
        //        //  if (c.ID != "pnlAdminReport" && c.ID != "pnlFinanceReport")
        //        //{
        //        if (c is Panel)
        //            c.Visible = false;
        //        // }
        //    }
        //    foreach (Control c in pnlFinanceReport.Controls)
        //    {
        //        //  if (c.ID != "pnlAdminReport" && c.ID != "pnlFinanceReport")
        //        //{
        //        if (c is Panel)
        //            c.Visible = false;
        //        // }
        //    }
        //    divgrid.Visible = false;
        //    divgvDependendance.Visible = false;
        //}


    }
}