using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class Dashboard : AdminBasePage
    {
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
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

                return (viewState == null) ? "PaymentBranch" : (string)viewState;
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

        #region Page PreIntit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 0;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ContactUC.Currency = Currency;
                // BindDashbordDetails();
                ContactUC.UserName = this.UserName;
                ContactUC.ParlourId = this.ParlourId;
                HideData.Visible = false;
                ///List<SecureUserGroupsModel> model = client.GetSuperUserAccessByID(UserID, ParlourId).ToList();
                ContactUC.IsAdministrator = false;
                ContactUC.IsSuperUser = false;
                //DivpaymentCollection.Visible = false;
                BindDashboardLabel();
                if (IsAdministrator == true || IsSuperUser == true)
                {
                    ContactUC.IsAdministrator = IsAdministrator;
                    ContactUC.IsSuperUser = IsSuperUser;

                    bindPolicyPremiumList();
                    HideData.Visible = true;
                    bindCompanyType();
                    
                }
            }
        }
        //Coommented on 5-12-16 coz Log image set at side bar on Admin.Master Page
        //public void BindDashbordDetails()
        //{
        //    ApplicationSettingsModel modelCompany;
        //    modelCompany = client.GetApplictionByParlourID(ParlourId);
        //    if (modelCompany != null)
        //    {
        //        if (modelCompany.ApplicationLogo != null)
        //        {
        //            string base64String = Convert.ToBase64String(modelCompany.ApplicationLogo, 0, modelCompany.ApplicationLogo.Length);
        //            Image1.ImageUrl = "data:image/png;base64," + base64String;
        //        }
        //        else
        //        {
        //            Image1.ImageUrl = string.Empty;
        //        }
        //    }
        //}


        #region Function
        public void bindCompanyType()
        {
            try
            {
                ApplicationSettingsModel ComName = client.GetAllApplicationList2(ParlourId, 0, 0);
                if (ComName != null)
                {
                    ApplicationSettingsModel[] model = client.GetAllApplicationList(ParlourId, 1, 0);
                    if (model != null)
                    {
                        ddlCompanyList.Visible = true;
                        ddlCompanyList.DataTextField = "ApplicationName";
                        ddlCompanyList.DataValueField = "pkiApplicationID";
                        ddlCompanyList.DataSource = model;
                        ddlCompanyList.DataBind();
                        ddlCompanyList.Items.Insert(0, new ListItem("Select", "0"));
                        int a = SelectName();
                        ddlCompanyList.SelectedValue = a.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }
        }
        public void bindPolicyPremiumList()
        {
            if (!IsPostBack)
            {
                gvPolicyPremium.PageSize = PageSize;
                List<PolicyPremiumDashboardModel> objModel = client.SelectPolicyPremiumByParlourId(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType, ContactUC.IsAdministrator, ContactUC.IsSuperUser, this.UserName).ToList();
                gvPolicyPremium.DataSource = objModel;
                gvPolicyPremium.DataBind();
            }
            else
            {

                if (ddlCompanyList.SelectedValue == "" || ddlCompanyList.SelectedValue == null)
                {
                    gvPolicyPremium.PageSize = PageSize;
                    List<PolicyPremiumDashboardModel> objModel = client.SelectPolicyPremiumByParlourId(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType, ContactUC.IsAdministrator, ContactUC.IsSuperUser, this.UserName).ToList();
                    gvPolicyPremium.DataSource = objModel;
                    gvPolicyPremium.DataBind();

                }
                else
                {
                    int AppId = Convert.ToInt32(ddlCompanyList.SelectedValue);
                    ApplicationSettingsModel ComName = client.GetAllApplicationList2(ParlourId, 2, AppId);
                    Guid selectedCompany;
                    if (Convert.ToInt32(ddlCompanyList.SelectedValue) > 0)
                    {
                        selectedCompany = ComName.parlourid;
                    }
                    else
                    {
                        selectedCompany = ParlourId;
                    }

                    gvPolicyPremium.PageSize = PageSize;
                    ContactUC.ParlourId = selectedCompany;
                    ContactUC.UserName = this.UserName;
                    List<PolicyPremiumDashboardModel> objModel = client.SelectPolicyPremiumByParlourId(selectedCompany, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType, false, ContactUC.IsSuperUser, this.UserName).ToList(); // Id Administrator pass true else it should be false to select selected company data those not admin
                    ContactUC.LoadChart();
                    gvPolicyPremium.DataSource = objModel;
                    gvPolicyPremium.DataBind();
                }
            }
        }

        public void BindDashboardLabel()
        {
            //DivpaymentCollection.Visible = true;
            var datasetTable = CommonBAL.GetDashboardLableDetails(this.ParlourId,this.IsAdministrator,this.IsSuperUser,this.UserName);
            if (datasetTable.Tables.Count > 0)
            {
                lblTodayTotalPayment.InnerText = this.Currency + " " + datasetTable.Tables[0].Rows[0]["TodayPayment"].ToString();
                lblOutstandingPaymentsCount.InnerText = this.Currency + " " + datasetTable.Tables[0].Rows[0]["OutstandingCollection"].ToString();
                lblTotalSMSCreditsCount.InnerText = datasetTable.Tables[0].Rows[0]["SMSBalalnce"].ToString();
                
                var getLable = datasetTable.Tables[0].Rows[0]["PaymentLabel"].ToString();
                //lblPaymentUpDownIcon.Attributes.Add("class", "fa fa-arrow-" + getLable);
                //lblPaymentUpDownIcon.Attributes.Add("style", "color:" + (getLable == "up" ? "green" : "red"));
            }
        }
        public int SelectName()
        {
            ApplicationSettingsModel ComName = client.GetAllApplicationList2(ParlourId, 0, 0);

            int pkiID = ComName.pkiApplicationID;
            return pkiID;

        }
        #endregion
        #region Page size change event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            bindPolicyPremiumList();
        }
        #endregion
        #region events
        protected void ChangeCompanydata(object sender, EventArgs e)
        {
            bindPolicyPremiumList();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindPolicyPremiumList();
        }
        #endregion
        #region Control event
        protected void gvPolicyPremium_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        e.Row.Cells[1].Text = (e.Row.Cells[1] != null) ? (string.Format("{0} ", Currency) + (Math.Round(Convert.ToDecimal(e.Row.Cells[1].Text), 2)).ToString()) : (string.Empty);
                        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    }
                    catch
                    {

                    }
                }
            }
        }
        #endregion
        #region pagelist
        protected void gvPolicyPremium_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPolicyPremium.PageIndex = e.NewPageIndex;
            bindPolicyPremiumList();
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

        protected void gvPolicyPremium_Sorting(object sender, GridViewSortEventArgs e)
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
            bindPolicyPremiumList();
        }
        #endregion

        protected void btnAddTopUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTopupSMS.Value != "" || txtTopupSMS.Value != null)
                {
                    ConsumableOrders model1 = new ConsumableOrders();
                    model1.DateCreated = DateTime.Now;
                    model1.SMSQyt = txtTopupSMS.Value;
                    model1.Parlourid = this.ParlourId;
                    model1.LastModified = DateTime.Now;
                    model1.ModifiedUser = this.UserName;
                    CommonBAL.SMSTopup_save(model1);
                    ScriptManager.RegisterClientScriptBlock(this.Page,GetType(), "alert", "alert('SMS Qty. Inserted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Alert", "alert('Please Enter Topup SMS');", true);
                }
            }
            catch (Exception ee)
            {
                string msg = ee.Message;
                ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Alert", "alert('" + msg + "');", true);
            }
        }

        protected void btnSendMessagesoutstaning_Click(object sender, EventArgs e)
        {
            CommonBAL.SendOutstandingMessagesToAll(this.ParlourId);
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "alert", "alert('Outstanding SMS Sent successfully');", true);
        }
    }
}