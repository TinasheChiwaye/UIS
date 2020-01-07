using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;


namespace Funeral.Web.Tools
{
    public partial class PlanSetup : AdminBasePage
    {
        #region Page Property
        public int PlanID
        {
            get
            {
                if (ViewState["_PlanID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_PlanID"]);
            }
            set
            {
                ViewState["_PlanID"] = value;
            }
        }
        public int PageSize
        {
            get
            {
                if (ViewState["_PageSize"] == null)
                    return 10;
                else { return Convert.ToInt32(ViewState["_PageSize"].ToString()); }

            }
            set { ViewState["_PageSize"] = value; }

        }
        public int PageNum
        {
            get
            {
                if (ViewState["_PageNum"] == null)
                    return 1;
                else { return Convert.ToInt32(ViewState["_PageNum"].ToString()); }
            }
            set { ViewState["_PageNum"] = value; }

        }
        public Int64 TotalRecord
        {
            get
            {
                if (ViewState["_TotalRecord"] == null)
                    return 0;
                else { return Convert.ToInt32(ViewState["_TotalRecord"].ToString()); }
            }
            set { ViewState["_TotalRecord"] = value; }

        }
        public string sortBYExpression
        {
            get
            {
                object viewState = this.ViewState["sortBYExpression"];

                return (viewState == null) ? "pkiPlanID" : (string)viewState;
            }
            set { this.ViewState["sortBYExpression"] = value; }
        }
        public string sortType
        {
            get
            {
                object viewState = this.ViewState["sortType"];

                return (viewState == null) ? "ASC" : (string)viewState;
            }
            set { this.ViewState["sortType"] = value; }
        }

        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 19;
        }
        #endregion

        //#region PageInit
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    SecureUserGroupsModel[] obj = client.EditSecurUserbyID(UserID);
        //    List<int> list = new List<int>();
        //    list.Add(4);
        //    list.Add(12);
        //    var result = obj.Where(x => list.Contains(x.fkiSecureGroupID));
        //    if (result.FirstOrDefault() == null)
        //    {
        //        Response.Redirect("~/Admin/403Error.aspx", false);
        //    }
        //}
        //#endregion

        #region Page load event
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            if (!Page.IsPostBack)
            {
                ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(PageSize.ToString()));
                BindPlanList();
                //  BindUnderwriterDropdown();
                BindCompanyList(ddlCompanyList, dvAdministrator);
                btnSubmite.Enabled = HasCreateRight;
            }
        }

        #endregion

        #region Page size change event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);

            BindPlanList();
        }
        #endregion

        #region Private/Public function and methods
        /// <summary>
        /// Get All Plans from database and bind here.
        /// </summary>
        /// 
        //public void BindUnderwriterDropdown()
        //{

        //    UnderwriterModel[] data = client.SelectUnderwriterNotDeleted();
        //    ddlUnderwriter.DataSource = data;
        //    ddlUnderwriter.DataValueField = "PkiUnderwriterId";
        //    ddlUnderwriter.DataTextField = "UnderwriterName";
        //    ddlUnderwriter.DataBind();
        //    ddlUnderwriter.Items.Insert(0, new ListItem("Select", "0"));
        //}
        public void BindPlanList()
        {
            gvPlan.PageSize = PageSize;
            List<PlanModel> model = ToolsSetingBAL.GetAllPlans(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
            gvPlan.DataSource = model;
            gvPlan.DataBind();
        }
        public void BindPlanToUpdate()
        {
            PlanModel model = ToolsSetingBAL.EditPlanbyID(PlanID, ParlourId);
            if ((model == null) || (model.parlourid != ParlourId))
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Plan.');</script>");
            }
            else
            {
                PlanID = model.pkiPlanID;
                txtPlanName.Text = model.PlanName;
                txtPremium.Text = model.PlanSubscription.ToString("#,0.00");
                //txtPremium.Text = model.PlanSubscription.ToString("#,0.00");
                //txtJoiningfee.Text = model.JoiningFee.ToString("#,0.00");
                txtJoiningfee.Text = model.JoiningFee.ToString("#,0.00");
                txtDescription.Text = model.PlanDesc;
                try
                {
                    //ddlUnderwriter.SelectedValue = model.UnderwriterId.ToString();
                }
                catch { }
                //txtUnderwriterName.Text = model.PlanUnderwriter;
                //ddlMianMember.Text = model.MainMember.ToString();
                txtMianMember.Text = model.Cover.ToString("#,0.00");
                //txtMianMember.Text = model.Cover.ToString("#,0.00");
                ddlSpouse.Text = model.Spouse.ToString();
                ddlChildren.Text = model.Children.ToString();

                txtFirstYear.Text = model.Cover0to5year.ToString("#,0.00");
                txtSecondYear.Text = model.Cover14to21year.ToString("#,0.00");
                txtThirdYear.Text = model.Cover6to13year.ToString("#,0.00");

                txtSpouseCover.Text = model.SpouseCover.ToString("#,0.00");
                txtAdultCover1.Text = model.Cover22to40year.ToString("#,0.00");
                txtAdultCover2.Text = model.Cover41to59year.ToString("#,0.00");
                txtAdultCover3.Text = model.Cover60to65year.ToString("#,0.00");
                txtAdultCover4.Text = model.Cover66to75year.ToString("#,0.00");

                ddlAdults.Text = model.Adults.ToString();
                ddlWaitingPeriod.Text = model.WaitingPeriod.ToString();
                ddlLaspsedperiod.Text = model.PolicyLaps.ToString();
                //txtUnderwriterSpit.Text = model.UnderwriterSplit.ToString("#,0.00");
                txtAgentSpit.Text = model.AgentSplit.ToString("#,0.00");
                txtOfficeSplit.Text = model.OfficeSplit.ToString("#,0.00");
                txtCompanySplit.Text = model.CompanySplit.ToString("#,0.00");
                txtHeadManager.Text = model.HeadManagerSplit.ToString("#,0.00");
                txtAdminSplit.Text = model.AdminSplit.ToString("#,0.00");
                txtCashPayout.Text = model.CashPayout.ToString("#,0.00");

                btnSubmite.Text = "Update";
            }
        }
        public void ClearControl()
        {
            btnSubmite.Text = "Save";
            PlanID = 0;
            txtPlanName.Text = string.Empty;
            txtPlanName.Text = string.Empty;
            txtPremium.Text = string.Empty;
            txtJoiningfee.Text = string.Empty;
            txtDescription.Text = string.Empty;
            //txtUnderwriterName.Text = string.Empty;
            //ddlMianMember.ClearSelection();
            //ddlMianMember.Items.FindByValue("0").Selected = true;
            txtMianMember.Text = string.Empty;
            txtSpouseCover.Text = string.Empty;
            txtAdultCover1.Text = string.Empty;
            txtAdultCover2.Text = string.Empty;
            txtAdultCover3.Text = string.Empty;
            txtAdultCover4.Text = string.Empty;
            ddlSpouse.ClearSelection();
            ddlSpouse.Items.FindByValue("0").Selected = true;
            ddlChildren.ClearSelection();
            ddlChildren.Items.FindByValue("0").Selected = true;
            txtFirstYear.Text = string.Empty;
            txtSecondYear.Text = string.Empty;
            txtThirdYear.Text = string.Empty;
            ddlAdults.ClearSelection();
            ddlAdults.Items.FindByValue("0").Selected = true;
            ddlWaitingPeriod.ClearSelection();
            ddlWaitingPeriod.Items.FindByValue("0").Selected = true;
            ddlLaspsedperiod.ClearSelection();
            ddlLaspsedperiod.Items.FindByValue("0").Selected = true;
            //  txtUnderwriterSpit.Text = string.Empty;
            txtAgentSpit.Text = string.Empty;
            txtOfficeSplit.Text = string.Empty;
            txtCompanySplit.Text = string.Empty;
            txtHeadManager.Text = string.Empty;
            txtAdminSplit.Text = string.Empty;
            txtCashPayout.Text = string.Empty;
            lblMessage.Visible = false;

        }
        #endregion

        #region Button Click Events
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        protected void btnSubmite_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                PlanModel model;
                model = ToolsSetingBAL.GetPlanByID(txtPlanName.Text, ParlourId);
                if (model != null && PlanID == 0)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, "Plan Already Exists.");
                }
                else
                {
                    model = new PlanModel();

                    model.pkiPlanID = PlanID;
                    model.PlanName = txtPlanName.Text;
                    model.PlanSubscription = (txtPremium.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtPremium.Text.Replace(Currency.Trim() + " ", "")));
                    model.JoiningFee = (txtJoiningfee.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtJoiningfee.Text.Replace(Currency.Trim() + " ", "")));
                    model.PlanDesc = txtDescription.Text;
                    //model.PlanUnderwriter = txtUnderwriterName.Text;
                    // model.PlanUnderwriter = ddlUnderwriter.SelectedItem.Text;
                    try
                    {
                        // model.UnderwriterId = Convert.ToInt32(ddlUnderwriter.SelectedItem.Value);
                    }
                    catch { }
                    //model.MainMember = Convert.ToInt32(ddlMianMember.Text);
                    model.Cover = (txtMianMember.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtMianMember.Text.Replace(Currency.Trim() + " ", "")));
                    model.Spouse = Convert.ToInt32(ddlSpouse.Text);
                    model.Children = Convert.ToInt32(ddlChildren.Text);
                    model.Adults = Convert.ToInt32(ddlAdults.Text);

                    model.Cover22to40year = (txtAdultCover1.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtAdultCover1.Text.Replace(Currency.Trim() + " ", "")));
                    model.Cover41to59year = (txtAdultCover2.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtAdultCover2.Text.Replace(Currency.Trim() + " ", "")));
                    model.Cover60to65year = (txtAdultCover3.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtAdultCover3.Text.Replace(Currency.Trim() + " ", "")));
                    model.Cover66to75year = (txtAdultCover4.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtAdultCover4.Text.Replace(Currency.Trim() + " ", "")));

                    model.Cover0to5year = (txtFirstYear.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtFirstYear.Text.Replace(Currency.Trim() + " ", "")));
                    model.Cover14to21year = (txtFirstYear.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtSecondYear.Text.Replace(Currency.Trim() + " ", "")));
                    model.Cover6to13year = (txtFirstYear.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtThirdYear.Text.Replace(Currency.Trim() + " ", "")));

                    model.SpouseCover = (txtSpouseCover.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtSpouseCover.Text));
                    model.AdultCover = model.Cover22to40year + model.Cover41to59year + model.Cover60to65year + model.Cover66to75year;
                    model.ChildCover = model.Cover0to5year + model.Cover14to21year + model.Cover6to13year;

                    model.WaitingPeriod = Convert.ToInt32(ddlWaitingPeriod.Text);
                    model.PolicyLaps = Convert.ToInt32(ddlLaspsedperiod.Text);
                    //   model.UnderwriterSplit = (txtUnderwriterSpit.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtUnderwriterSpit.Text.Replace(Currency.Trim() + " ", "")));
                    model.AgentSplit = (txtAgentSpit.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtAgentSpit.Text.Replace(Currency.Trim() + " ", "")));
                    model.OfficeSplit = (txtOfficeSplit.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtOfficeSplit.Text.Replace(Currency.Trim() + " ", "")));
                    model.CompanySplit = (txtCompanySplit.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtCompanySplit.Text.Replace(Currency.Trim() + " ", "")));
                    model.HeadManagerSplit = (txtHeadManager.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtHeadManager.Text.Replace(Currency.Trim() + " ", "")));
                    model.AdminSplit = (txtAdminSplit.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtAdminSplit.Text.Replace(Currency.Trim() + " ", "")));
                    model.CashPayout = (txtCashPayout.Text.Replace(Currency.Trim() + " ", "") == string.Empty ? 0 : Convert.ToDecimal(txtCashPayout.Text.Replace(Currency.Trim() + " ", "")));

                    model.LastModified = System.DateTime.Now;
                    model.parlourid = ParlourId;
                    model.ModifiedUser = UserName;

                    //================================================================ 
                    int retID = ToolsSetingBAL.SavePlanDetails(model);
                    PlanID = retID;

                    ShowMessage(ref lblMessage, MessageType.Success, "Plan Details successfully saved");
                    BindPlanList();
                    ClearControl();
                }
                lblMessage.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab512", "goToTab(5)", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPlanList();
        }

        #endregion

        #region gvPlanList Properties
        protected void gvPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPlan.PageIndex = e.NewPageIndex;
            BindPlanList();
        }

        protected void gvPlan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditPlan")
            {
                PlanID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    BindPlanToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            if (e.CommandName == "deletePlan")
            {
                int SPlanId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    int retID = ToolsSetingBAL.DeletePlan(SPlanId);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    BindPlanList();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
        }

        #region "Sorting Event"

        private const string ASCENDING = "ASC";
        private const string DESCENDING = "DESC";
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Ascending;
                }

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }
        private void SortGridView(string sortExpression, string direction)
        {
            sortBYExpression = sortExpression;
            sortType = direction;
        }

        protected void gvPlan_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
            BindPlanList();
        }
        #endregion

        #endregion

        protected void gvPlan_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row?.RowType != DataControlRowType.DataRow)
                return;

            e.Row.Cells[2].Text = ($@"{Currency} " + (Math.Round(Convert.ToDecimal(e.Row.Cells[2].Text), 2)));
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
        }
    }
}