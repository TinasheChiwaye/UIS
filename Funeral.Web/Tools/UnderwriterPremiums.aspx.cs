using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Tools
{
    public partial class UnderwriterPremiums : AdminBasePage
    {
        #region Page Property

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

                return (viewState == null) ? "PkiUnderwriterPremiumId" : (string)viewState;
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

        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 42;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindNumbers();
                // BindUnderwriterPremium();
                BindUndrewriterSetupName();
                BindPlanList();
                BindRolePlayerType();
                // BindUndewriterList();
            }
        }
        public int UnderwriterPremiumId
        {
            get
            {
                if (ViewState["_PkiUnderwriterPremiumId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_PkiUnderwriterPremiumId"]);
            }
            set
            {
                ViewState["_PkiUnderwriterPremiumId"] = value;
            }
        }

        #region Button Click Events

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UnderwriterPremiumModel Underwriterpremiummodel;
                Underwriterpremiummodel = UnderwriterPremiumBAL.SelectUnderwriterPremiumByName(Convert.ToInt32(ddlPlanUnderwriter.SelectedValue), ParlourId);

                Underwriterpremiummodel = new UnderwriterPremiumModel();
                Underwriterpremiummodel.PkiUnderwriterPremiumId = UnderwriterPremiumId;
                Underwriterpremiummodel.FkiUnderwriterId = Convert.ToInt32(ddlPlanUnderwriter.SelectedValue);
                Underwriterpremiummodel.PlanId = Convert.ToInt32(ddlPlanName.SelectedValue);
                Underwriterpremiummodel.RolePlayerType = ddlDependencyType.SelectedValue;
                Underwriterpremiummodel.PremiumAmount = Convert.ToDecimal(TxtPremiumAmount.Text);
                Underwriterpremiummodel.CoverAmount = Convert.ToDecimal(txtCoverAmount.Text);
                Underwriterpremiummodel.CoverAgeFrom = Convert.ToInt32(ddlCoverAgeFromNum.SelectedValue);
                Underwriterpremiummodel.CoverAgeTo = Convert.ToInt32(ddlCoverAgeToNum.SelectedValue);
                Underwriterpremiummodel.ApplysToDependents = false;
                Underwriterpremiummodel.LastModified = System.DateTime.Now;
                Underwriterpremiummodel.ModifiedUser = UserName;
                Underwriterpremiummodel.CreatedDate = System.DateTime.Now;
                Underwriterpremiummodel.CreatedUser = UserName;
                Underwriterpremiummodel.Parlourid = ParlourId;
                Underwriterpremiummodel.CoverAgeFromType = ddlCoverAgeFromMonthYear.SelectedValue;
                Underwriterpremiummodel.CoverAgeToType = ddlCoverAgeToMonthYear.SelectedValue;
                Underwriterpremiummodel.UnderwriterPremium = Convert.ToDecimal(txtUnderWriterPremium.Text);
                Underwriterpremiummodel.UnderwriterCover = Convert.ToDecimal(txtUnderWriterCover.Text);

                int retID = UnderwriterPremiumBAL.SaveUnderwriterPremium(Underwriterpremiummodel);
                UnderwriterPremiumId = retID;

                ClearControl();
                BindUndewriterPremiumList();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab512", "goToTab(5)", true);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        #endregion
        private void ClearControl()
        {
            btnSubmite.Text = "Save";
            ddlPlanUnderwriter.SelectedValue = "0";
            ddlPlanName.SelectedValue = "0";
            ddlDependencyType.SelectedValue = "0";
            // ddlApplysToDependants.ClearSelection();
            TxtPremiumAmount.Text = string.Empty;
            txtCoverAmount.Text = string.Empty;
            ddlCoverAgeFromMonthYear.SelectedValue = "0";
            ddlCoverAgeFromNum.SelectedValue = "0";
            ddlCoverAgeToMonthYear.SelectedValue = "0";
            ddlCoverAgeToNum.SelectedValue = "0";
            UnderwriterPremiumId = 0;
            txtUnderWriterCover.Text = string.Empty;
            txtUnderWriterPremium.Text = string.Empty;
            // UnderwriterPremiumId = 0;
            // TxtCoverAgeFrom.Text = string.Empty;
            //TxtCoverAgeTo.Text = string.Empty;
        }

        private void BindNumbers()
        {
            int Numbers = 100;
            for (int i = 0; i <= Numbers; i++)
            {
                ddlCoverAgeFromNum.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlCoverAgeToNum.Items.Add(new ListItem(i.ToString(), i.ToString()));

            }

        }

        public void BindUndrewriterSetupName()
        {
            List<UnderwriterSetupModel> objUndrewriterSetupNameModel = CommonBAL.GetUnderwriterSetupNameByParlourId(ParlourId);
            foreach (UnderwriterSetupModel UnderwriterName in objUndrewriterSetupNameModel)
            {
                ListItem li = new ListItem();
                li.Text = UnderwriterName.UnderwriterName;
                li.Value = UnderwriterName.PkiUnderWriterSetupId.ToString();//branch.Brancheid.ToString();
                ddlPlanUnderwriter.Items.Add(li);
            }
        }
        public void BindPlanList()
        {
            List<PlanModel> model = ToolsSetingBAL.GetAllPlansList(ParlourId);
            foreach (PlanModel Planlist in model)
            {
                ListItem li = new ListItem();
                li.Text = Planlist.PlanName;
                li.Value = Planlist.pkiPlanID.ToString();
                ddlPlanName.Items.Add(li);

            }
        }
        public void BindRolePlayerType()
        {
            // gvUnderwriterPremium.PageSize = PageSize;
            List<RolePlayerModel> model = ToolsSetingBAL.GetAllRolePlayer(ParlourId);
            // gvUnderwriterPremium.DataSource = model;
            //gvUnderwriterPremium.DataBind();

            foreach (RolePlayerModel AllRolePlayer in model)
            {
                ListItem li = new ListItem();
                li.Text = AllRolePlayer.Name;
                li.Value = AllRolePlayer.Name;
                ddlDependencyType.Items.Add(li);

            }

        }

        #region Private function & Methods

        #endregion

        #region Bind Underwriter Premium
        private void BindUnderwriterPremium()
        {
            var PlanUnderwriter = ToolsSetingBAL.GetAllUnderwriterPlansByParlourID(this.ParlourId, Convert.ToInt32(UnderwriterPremiumEnums.UnderwriterPlans.PlanUnderwriter));
            var PlanName = ToolsSetingBAL.GetAllUnderwriterPlansByParlourID(this.ParlourId, Convert.ToInt32(UnderwriterPremiumEnums.UnderwriterPlans.PlanName));
            ddlPlanUnderwriter.DataSource = PlanUnderwriter;
            ddlPlanUnderwriter.DataTextField = "Name";
            ddlPlanUnderwriter.DataValueField = "Id";
            ddlPlanUnderwriter.DataBind();
            ddlPlanUnderwriter.Items.Insert(0, new ListItem("", "0"));

            ddlPlanName.DataSource = PlanName;
            ddlPlanName.DataTextField = "Name";
            ddlPlanName.DataValueField = "Id";
            ddlPlanName.DataBind();
            ddlPlanName.Items.Insert(0, new ListItem("", "0"));
        }
        #endregion
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindUndewriterPremiumList();
        }
        public void BindUndewriterPremiumList()
        {
            gvUnderwriterPremium.PageSize = PageSize;
            List<UnderwriterPremiumModel> model = UnderwriterPremiumBAL.SelectAllUnderwriterPremiumByParlourId(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
            gvUnderwriterPremium.DataSource = model;
            gvUnderwriterPremium.DataBind();
        }

        public void BindUndewriterToUpdate()
        {
            // UnderwriterPremiumModel model = client.EditPlanbyID(UnderwriterPremiumId, ParlourId);
            UnderwriterPremiumModel model = UnderwriterPremiumBAL.EditUnderwriterPremiumbyID(UnderwriterPremiumId, ParlourId);

            if ((model == null) || (model.Parlourid != ParlourId))
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Plan.');</script>");
            }
            else
            {
                UnderwriterPremiumId = model.PkiUnderwriterPremiumId;
                if (ddlPlanUnderwriter.Items.FindByValue(model.FkiUnderwriterId.ToString()) != null)
                {
                    ddlPlanUnderwriter.SelectedValue = model.FkiUnderwriterId.ToString();
                }

                ddlDependencyType.SelectedValue = model.RolePlayerType.ToString();
                //txtPremium.Text = model.PlanSubscription.ToString("#,0.00");
                //txtJoiningfee.Text = model.JoiningFee.ToString("#,0.00");
                ddlPlanName.SelectedValue = model.PlanId.ToString();
                //ddlPlanName.SelectedIndex = ddlPlanName.Items.IndexOf(ddlPlanName.Items.FindByValue(model.PlanID.ToString()));
                //  txtDescription.Text = model.PlanDesc;
                try
                {
                    //ddlUnderwriter.SelectedValue = model.UnderwriterId.ToString();
                }
                catch { }
                TxtPremiumAmount.Text = model.PremiumAmount.ToString("#,0.00");
                txtCoverAmount.Text = model.CoverAmount.ToString("#,0.00");

                ddlCoverAgeFromNum.SelectedValue = model.CoverAgeFrom.ToString();
                //txtMianMember.Text = model.Cover.ToString("#,0.00");
                //ddlSpouse.Text = model.Spouse.ToString();
                //ddlChildren.Text = model.Children.ToString();

                ddlCoverAgeToNum.SelectedValue = model.CoverAgeTo.ToString();
                ddlCoverAgeFromMonthYear.SelectedValue = model.CoverAgeFromType.ToString();
                ddlCoverAgeToMonthYear.SelectedValue = model.CoverAgeToType.ToString();
                txtUnderWriterPremium.Text = model.UnderwriterPremium.ToString();
                txtUnderWriterCover.Text = model.UnderwriterCover.ToString();

                btnSubmite.Text = "Update";
            }
        }

        protected void gvUnderwriterPremium_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditPlan")
            {
                UnderwriterPremiumId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    // BindPlanToUpdate();
                    BindUndewriterToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            if (e.CommandName == "deletePlan")
            {
                int SPkiUnderwriterPremiumId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    int retID = UnderwriterPremiumBAL.DeleteUnderwriterPremium(SPkiUnderwriterPremiumId, UserName);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    BindUndewriterPremiumList();
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

        private void SortGridView(string sortExpression, string direction)
        {
            sortBYExpression = sortExpression;
            sortType = direction;
        }

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

        protected void gvUnderwriterPremium_Sorting(object sender, GridViewSortEventArgs e)
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
            // BindPlanList();
            BindUndewriterPremiumList();
        }

        #endregion

        protected void gvUnderwriterPremium_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row?.RowType != DataControlRowType.DataRow)
                return;

            e.Row.Cells[2].Text = ($@"{Currency} " + (Math.Round(Convert.ToDecimal(e.Row.Cells[2].Text), 2)));
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

            e.Row.Cells[3].Text = ($@"{Currency} " + (Math.Round(Convert.ToDecimal(e.Row.Cells[3].Text), 2)));
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;



        }

        protected void gvUnderwriterPremium_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUnderwriterPremium.PageIndex = e.NewPageIndex;
            // BindPlanList();
        }

    }
}