using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Tools
{
    public partial class FuneralServices : AdminBasePage
    {
        #region Fields
        #endregion

        #region Page Property
        public int ServiceID
        {
            get
            {
                if (ViewState["_ServiceID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_ServiceID"]);
            }
            set
            {
                ViewState["_ServiceID"] = value;
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

                return (viewState == null) ? "pkiServiceID" : (string)viewState;
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
            this.dbPageId = 25;
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

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(PageSize.ToString()));
                bindFuneralServiceList();
                ddlCompanyList.Visible = false;
                // SecureUserGroupsModel model = client.GetSuperUserAccessByID(UserID, ParlourId).Where(x => x.fkiSecureGroupID == 12).FirstOrDefault();
                if (IsAdministrator)
                { bindCompanyType(); }
                btnAddService.Enabled = HasCreateRight;
                BindVenderName();
            }
        }
        #endregion

        #region Page size change event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            bindFuneralServiceList();
        }
        #endregion

        #region Method
        public void bindCompanyType()
        {
            try
            {
                ApplicationSettingsModel ComName = ToolsSetingBAL.GetAllApplicationList2(ParlourId, 0, 0);
                if (ComName != null)
                {
                    List<ApplicationSettingsModel> model = ToolsSetingBAL.GetAllApplicationList(ParlourId, 1, 0);
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
        public int SelectName()
        {
            ApplicationSettingsModel ComName = ToolsSetingBAL.GetAllApplicationList2(ParlourId, 0, 0);

            int pkiID = ComName.pkiApplicationID;
            return pkiID;

        }
        public void bindFuneralServiceList()
        {
            if (!IsPostBack)
            {
                gvFuneralServiceList.PageSize = PageSize;
                List<FuneralServiceManageModel> objModel = ToolsSetingBAL.SelectFuneralManageServiceByParlID(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
                gvFuneralServiceList.DataSource = objModel;
                gvFuneralServiceList.DataBind();
                ViewState["NewParlourID"] = ParlourId;
            }
            else
            {

                if (ddlCompanyList.SelectedValue == "" || ddlCompanyList.SelectedValue == null || ddlCompanyList.SelectedValue == "0")
                {
                    gvFuneralServiceList.PageSize = PageSize;
                    List<FuneralServiceManageModel> objModel = ToolsSetingBAL.SelectFuneralManageServiceByParlID(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
                    gvFuneralServiceList.DataSource = objModel;
                    gvFuneralServiceList.DataBind();

                }
                else
                {
                    int AppId = Convert.ToInt32(ddlCompanyList.SelectedValue);
                    ApplicationSettingsModel ComName = ToolsSetingBAL.GetAllApplicationList2(ParlourId, 2, AppId);
                    Guid New = ComName.parlourid;
                    ViewState["NewParlourID"] = New;
                    gvFuneralServiceList.PageSize = PageSize;
                    List<FuneralServiceManageModel> objModel = ToolsSetingBAL.SelectFuneralManageServiceByParlID(New, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
                    gvFuneralServiceList.DataSource = objModel;
                    gvFuneralServiceList.DataBind();
                }
            }
        }
        public void BindFuneralServiceToUpdate()
        {
            btnReset.Enabled = true;
            SecureUserGroupsModel Access = ToolsSetingBAL.GetSuperUserAccessByID(UserID, ParlourId).Where(x => x.fkiSecureGroupID == 12).FirstOrDefault();
            FuneralServiceManageModel model = ToolsSetingBAL.SelectFuneralManageServiceByParlANdID(ServiceID, ParlourId);
            if ((Access == null) && (model == null))
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Service.');</script>");
            }
            else
            {
                ServiceID = model.pkiServiceID;
                txtServicename.Text = model.ServiceName;
                txtServiceCost.Text = (model.ServiceCost).ToString("N2");
                txtServiceDesc.Text = model.ServiceDesc;
                txtQty.Text = (model.QTY).ToString();
                btnAddService.Text = "Save";
            }
        }

        public void BindVenderName()
        {
            List<VendorModel> obVendorNameModel = ToolsSetingBAL.GetVendorNameByParlourId(ParlourId);
            foreach (VendorModel FuneralService in obVendorNameModel)
            {
                ListItem li = new ListItem();
                li.Text = FuneralService.VendorName;
                li.Value = FuneralService.pkiVendorID.ToString();//branch.Brancheid.ToString();
                ddlVendor.Items.Add(li);
            }
        }
        public void ClearControl()
        {
            ServiceID = 0;
            txtQty.Text = string.Empty;
            txtServiceCost.Text = string.Empty;
            txtServiceDesc.Text = string.Empty;
            txtServicename.Text = string.Empty;
            ddlVendor.SelectedValue = "0";
            txtCostOfSale.Text = string.Empty;
            btnAddService.Text = "Save My Service";
            btnReset.Enabled = false;

        }
        #endregion

        #region Button Event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindFuneralServiceList();
        }

        protected void btnSaveFuneralService_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    FuneralServiceManageModel objModel;
                    objModel = new FuneralServiceManageModel();

                    objModel.pkiServiceID = ServiceID;
                    objModel.VendorId = Convert.ToInt32(ddlVendor.SelectedValue);
                    objModel.CostOfSale = Convert.ToInt32(txtCostOfSale.Text);

                    objModel.ServiceName = (txtServicename.Text).ToString();
                    objModel.ServiceDesc = (txtServiceDesc.Text).ToString();
                    if (txtServiceCost.Text == string.Empty || txtServiceCost.Text == "")
                    { objModel.ServiceCost = 0; }
                    else
                    { objModel.ServiceCost = Convert.ToDecimal(txtServiceCost.Text); }
                    if (txtQty.Text == string.Empty || txtQty.Text == "")
                    { objModel.QTY = 0; }
                    else
                    { objModel.QTY = Convert.ToInt32(txtQty.Text); }
                    //SecureUserGroupsModel Access = client.GetSuperUserAccessByID(UserID, ParlourId).Where(x => x.fkiSecureGroupID == 12).FirstOrDefault(); ;
                    if (IsAdministrator)
                    {
                        Guid NewParlourID = (Guid)(ViewState["NewParlourID"]);
                        objModel.parlourid = NewParlourID;
                    }
                    else
                    { objModel.parlourid = ParlourId; }

                    objModel.ModifiedUser = UserName;


                    int a = ToolsSetingBAL.SaveFuneralManageService(objModel);

                    ShowMessage(ref lblMessage, MessageType.Success, "Service Successfully Saved.");
                    ClearControl();
                    bindFuneralServiceList();

                }
                catch (Exception ex)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                }
            }

        }
        protected void btn_ClearControl(object sender, EventArgs e)
        {
            ClearControl();
        }
        protected void ChangeCompanydata(object sender, EventArgs e)
        {
            bindFuneralServiceList();
        }
        #endregion

        #region control event
        protected void gvFuneralServiceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                try
                {
                    e.Row.Cells[2].Text = (e.Row.Cells[2] != null) ? (Currency + " " + (Math.Round(Convert.ToDecimal(e.Row.Cells[2].Text), 2)).ToString()) : (string.Empty);
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                    e.Row.Cells[5].Text = (e.Row.Cells[5] != null) ? (Currency + " " + (Math.Round(Convert.ToDecimal(e.Row.Cells[5].Text), 2)).ToString()) : (string.Empty);
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                }
                catch
                {

                }
            }
        }
        protected void gvFuneralService_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditFuneralService")
            {
                ServiceID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    BindFuneralServiceToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }
            }

            if (e.CommandName == "deleteFuneral")
            {
                int FuneralID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    int delID = ToolsSetingBAL.DeleteFuneral(FuneralID);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    bindFuneralServiceList();
                }
                catch (Exception ex)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                    lblMessage.Visible = true;
                }
            }

        }
        #endregion

        #region pagelist
        protected void gvFuneralService_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFuneralServiceList.PageIndex = e.NewPageIndex;
            bindFuneralServiceList();
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

        protected void gvFuneralService_Sorting(object sender, GridViewSortEventArgs e)
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
            bindFuneralServiceList();
        }
        #endregion

    }
}