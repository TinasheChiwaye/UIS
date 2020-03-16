using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Tools
{
    public partial class Underwriter : AdminBasePage
    {
        #region Page Property
        public int UnderwriterId
        {
            get
            {
                if (ViewState["_UnderwriterId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_UnderwriterId"]);
            }
            set
            {
                ViewState["_UnderwriterId"] = value;
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

                return (viewState == null) ? "PkiUnderwriterId" : (string)viewState;
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
            this.dbPageId = 31;
        }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindUndewriterList();
                BindPlan();
                BindUnderwriter();
            }
        }
        #endregion

        #region Methods
        public void BindUndewriterList()
        {
            gvUnderwriter.PageSize = PageSize;
            List<UnderwriterModel> model = UnderwriterBAL.SelectAllUnderwriterByParlourId(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
            gvUnderwriter.DataSource = model;
            gvUnderwriter.DataBind();
        }
        public void BindUnderWriterToUpdate()
        {
            UnderwriterModel model = UnderwriterBAL.SelectUnderwriterBypkid(UnderwriterId, ParlourId);
            if ((model == null) || (model.Parlourid != ParlourId))
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Plan.');</script>");
            }
            else
            {
                UnderwriterId = model.PkiUnderwriterId;
                ddlUnderwriterList.SelectedValue = model.UnderwriterName.ToString();
                ddlPlanList.SelectedValue = model.PlanName;
                txtPremium.Text = model.Premium.ToString();

                txtCover.Text = model.Cover.ToString("#,0.00");
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

                btnSubmite.Text = "Update";
            }
        }
        public void ClearControl()
        {
            UnderwriterId = 0;
            txtPremium.Text = string.Empty;

            txtCover.Text = string.Empty;
            ddlSpouse.Text = string.Empty;
            ddlChildren.Text = string.Empty;

            txtFirstYear.Text = string.Empty;
            txtSecondYear.Text = string.Empty;
            txtThirdYear.Text = string.Empty;

            txtSpouseCover.Text = string.Empty;
            txtAdultCover1.Text = string.Empty;
            txtAdultCover2.Text = string.Empty;
            txtAdultCover3.Text = string.Empty;
            txtAdultCover4.Text = string.Empty;

            ddlAdults.Text = string.Empty;

            btnSubmite.Text = "Save";
        }
        #endregion

        #region Event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);

            BindUndewriterList();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindUndewriterList();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        protected void btnSubmite_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                UnderwriterModel model;
                model = UnderwriterBAL.SelectUnderwriterByName(ddlUnderwriterList.SelectedValue, ParlourId);
                if (model != null && UnderwriterId == 0)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, "Underwriter Already Exists.");
                }
                else
                {
                    model = new UnderwriterModel();

                    model.PkiUnderwriterId = UnderwriterId;
                    model.PlanName = ddlPlanList.SelectedValue;
                    model.UnderwriterName = ddlUnderwriterList.SelectedValue;
                    model.Cover = (txtCover.Text == string.Empty ? 0 : Convert.ToDecimal(txtCover.Text));
                    model.Spouse = Convert.ToInt32(ddlSpouse.Text);
                    model.Children = Convert.ToInt32(ddlChildren.Text);
                    model.Adults = Convert.ToInt32(ddlAdults.Text);
                    model.Premium = txtPremium.Text;
                    model.CoverAmount = txtCover.Text;

                    model.Cover22to40year = (txtAdultCover1.Text == string.Empty ? 0 : Convert.ToDecimal(txtAdultCover1.Text));
                    model.Cover41to59year = (txtAdultCover2.Text == string.Empty ? 0 : Convert.ToDecimal(txtAdultCover2.Text));
                    model.Cover60to65year = (txtAdultCover3.Text == string.Empty ? 0 : Convert.ToDecimal(txtAdultCover3.Text));
                    model.Cover66to75year = (txtAdultCover4.Text == string.Empty ? 0 : Convert.ToDecimal(txtAdultCover4.Text));

                    model.Cover0to5year = (txtFirstYear.Text == string.Empty ? 0 : Convert.ToDecimal(txtFirstYear.Text));
                    model.Cover14to21year = (txtFirstYear.Text == string.Empty ? 0 : Convert.ToDecimal(txtSecondYear.Text));
                    model.Cover6to13year = (txtFirstYear.Text == string.Empty ? 0 : Convert.ToDecimal(txtThirdYear.Text));

                    model.SpouseCover = (txtSpouseCover.Text == string.Empty ? 0 : Convert.ToDecimal(txtSpouseCover.Text));
                    model.AdultCover = model.Cover22to40year + model.Cover41to59year + model.Cover60to65year + model.Cover66to75year;
                    model.ChildCover = model.Cover0to5year + model.Cover14to21year + model.Cover6to13year;


                    model.LastModified = System.DateTime.Now;
                    model.Parlourid = ParlourId;
                    model.ModifiedUser = UserName;
                    model.CreateddDate = System.DateTime.Now;
                    model.CreatedUser = UserName;

                    //================================================================ 
                    int retID = UnderwriterBAL.SaveUnderwriter(model);
                    UnderwriterId = retID;

                    ShowMessage(ref lblMessage, MessageType.Success, "Plan Details successfully saved");
                    BindUndewriterList();
                    ClearControl();
                }
                lblMessage.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab512", "goToTab(5)", true);
            }
        }
        #endregion

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

        protected void gvUnderwriter_Sorting(object sender, GridViewSortEventArgs e)
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
            BindUndewriterList();
        }
        #endregion

        #region gvUnderwriterList Properties
        protected void gvUnderwriter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUnderwriter.PageIndex = e.NewPageIndex;
            BindUndewriterList();
        }

        protected void gvUnderwriter_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditUnderwriter")
            {
                UnderwriterId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    BindUnderWriterToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            if (e.CommandName == "deleteUnderwriter")
            {
                int SPlanId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    int retID = UnderwriterBAL.DeleteUnderwriterByID(SPlanId, UserName);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    BindUndewriterList();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
        }
        #endregion

        #region BindUnderWriterDropdown
        public void BindUnderwriter()
        {
            List<UnderwriterSetupModel> model = ToolsSetingBAL.GetAllUnderwriterList(ParlourId);
            if (model != null)
            {
                ddlUnderwriterList.Visible = true;
                ddlUnderwriterList.DataTextField = "UnderwriterName";
                ddlUnderwriterList.DataValueField = "UnderwriterName";
                ddlUnderwriterList.DataSource = model;
                ddlUnderwriterList.DataBind();
                ddlUnderwriterList.Items.Insert(0, "Select Underwriter");
            }
        }
        public void BindPlan()
        {
            List<PlanModel> model = ToolsSetingBAL.GetAllPlansList(ParlourId);
            if (model != null)
            {
                ddlPlanList.Visible = true;
                ddlPlanList.DataTextField = "PlanName";
                ddlPlanList.DataValueField = "PlanName";
                ddlPlanList.DataSource = model;
                ddlPlanList.DataBind();
                ddlPlanList.Items.Insert(0, "Select Plan");
            }
        }
        #endregion
    }
}