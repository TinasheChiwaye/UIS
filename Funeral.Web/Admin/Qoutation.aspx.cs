using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class Qoutation : AdminBasePage
    {
        #region Fields
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();

        #endregion

        #region Page Property
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

                return (viewState == null) ? "QuotationID" : (string)viewState;
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
            this.dbPageId = 9;
        }
        #endregion
        //#region PageInit
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    SecureUserGroupsModel[] obj = client.EditSecurUserbyID(UserID);      
        //    List<int> list = new List<int>();
        //    list.Add(6);
        //    list.Add(4);
        //    list.Add(12);             
        //    var  result =  obj.Where(x => list.Contains(x.fkiSecureGroupID));            
        //    if (result.FirstOrDefault() == null) 
        //    {
        //        Response.Redirect("~/Admin/403Error.aspx", false);                             
        //    }
        //}
        //#endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(PageSize.ToString()));   
                bindQoutationList();
               // GetQuotationNumber();
                SelectServices.Enabled = false;
                btnService.Enabled = HasCreateRight;
            }
        }
        #endregion

        #region Page size change event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            bindQoutationList();
        }
        #endregion

        #region Methods
        //public void GetQuotationNumber()
        //{
        //    QuotationModel obj = client.GetQuotationNumberByID2(ParlourId);
        //    if (obj.QuotationNumber2 == string.Empty || obj.QuotationNumber2 == "")
        //    {
        //        ViewState["QuotationNumber2"] = "0001";
        //    }
        //    else
        //    {
        //    string s = obj.QuotationNumber2;
        //    s = (Int32.Parse(s) + 1).ToString("D4");
        //    ViewState["QuotationNumber2"]= s;
        //    }

        //}
        public void QuotationServicePage(int? QutID)
        {
            string id = QutID.ToString();
            var plaintextBytes = Encoding.UTF8.GetBytes(id);
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);
            //Response.Redirect("Default2.aspx?name=" + encryptedValue);
            Response.Redirect("~/Admin/QuotationServices.aspx?ID=" + encryptedValue);
        }
        public void bindQoutationList()
        {
            gvQuotationList.PageSize = PageSize;
            QuotationModel[] objQuotationModel = client.GetAllQuotationByParlourId(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
            gvQuotationList.DataSource = objQuotationModel;
            gvQuotationList.DataBind();                    
        }
        public void BindQuotationToUpdate()
        {
           
            QuotationModel model = client.SelectQuotationByQuotationId(ID,ParlourId);
            if ((model == null)|| (model.parlourid != ParlourId))
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Quotation.');</script>");
            }
            else //(model != null)
            {
                ID = model.QuotationID;
                ddlMethod.SelectedValue=model.ContactTitle.ToString();
                txtLastName.Text = model.ContactLastName.ToString();
                txtFirstname.Text = model.ContactFirstName.ToString();
                txtCellphone.Text = model.CellNumber.ToString();
                txtTelePhone.Text = model.TelNumber.ToString();
                txtPhysicalAddress.Text = model.AddressLine1.ToString();
                txtStreetAddress.Text = model.AddressLine2.ToString();
                txtTown.Text = model.AddressLine3.ToString();
                txtProvince.Text = model.AddressLine4.ToString();
                txtCode.Text = model.Code.ToString();
                //txtThirdYear.Text = model.Cover6to13year.ToString("#,0.00");
                ViewState["QuotationID"] = ID;
                btnService.Text = "Update";
                SelectServices.Enabled = true;
            }
        }
        public void ClearControl()
        {
            btnService.Text = "Create Quotation";
            ID = 0;
            ddlMethod.ClearSelection();
            txtLastName.Text = string.Empty;
            txtFirstname.Text = string.Empty;
            txtCellphone.Text = string.Empty;
            txtTelePhone.Text = string.Empty;
            txtPhysicalAddress.Text = string.Empty;
            txtStreetAddress.Text = string.Empty;
            txtTown.Text = string.Empty;
            txtProvince.Text = string.Empty;
            txtCode.Text = string.Empty;
            SelectServices.Enabled = false;
        }
        public void PrintQuotation()
        {
            string id = ID.ToString();
            var plaintextBytes = Encoding.UTF8.GetBytes(id);
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);
            //Response.Redirect("Default2.aspx?name=" + encryptedValue);
            Response.Redirect("~/Admin/PrintForm.aspx?QID=" + encryptedValue);
        }

        #endregion

        #region ButtonEvent
        protected void btncrtQoutationServices_Click(object sender, EventArgs e)
        {
            int QutID = Convert.ToInt32(ViewState["QuotationID"]);
            QuotationServicePage(QutID);  
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        protected void btncrtQoutation_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    QuotationModel objQuotationModel = client.SelectQuotationByQuotationId(ID, ParlourId);
                    if (objQuotationModel != null && ID ==0)
                    {
                        ShowMessage(ref lblMessage, MessageType.Danger, "Quotation is  Already Exists.");
                    }
                    else
                    {
                        objQuotationModel = new QuotationModel();
                        objQuotationModel.QuotationID = ID;
                        if (ddlMethod.Text == "Select" || ddlMethod.SelectedValue == "Select")
                        {
                            objQuotationModel.ContactTitle = " ";
                        }
                        else
                        {
                            objQuotationModel.ContactTitle = ddlMethod.Text;
                        }
                        objQuotationModel.ContactLastName = txtLastName.Text;
                        objQuotationModel.ContactFirstName = txtFirstname.Text;
                        objQuotationModel.CellNumber = txtCellphone.Text;
                        objQuotationModel.TelNumber = txtTelePhone.Text;
                        objQuotationModel.AddressLine1 = txtPhysicalAddress.Text;
                        objQuotationModel.AddressLine2 = txtStreetAddress.Text;
                        objQuotationModel.AddressLine3 =txtTown.Text;
                        objQuotationModel.AddressLine4 = txtProvince.Text;
                        objQuotationModel.Code = txtCode.Text;
                        objQuotationModel.DateOfQuotation = System.DateTime.Now;
                        objQuotationModel.parlourid = this.ParlourId;
                        objQuotationModel.ModifiedUser = UserName;
                        objQuotationModel.LastModified = System.DateTime.Now;
                        objQuotationModel.QuotationNumber = "";

                        int a = client.SaveQuotation(objQuotationModel);
                        ViewState["QuotationID"] = a;
                        ShowMessage(ref lblMessage, MessageType.Success, "Quotation Successfully Saved.");
                        ClearControl();
                        bindQoutationList();
                        SelectServices.Enabled = true;
                    }
                    

                }
                catch (Exception ex)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                }
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            bindQoutationList();
        }
        #endregion

        #region control event
        protected void gvQuotationList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                try
                {
                    e.Row.Cells[4].Text = (e.Row.Cells[4] != null) ? ("R " + (Math.Round(Convert.ToDecimal(e.Row.Cells[4].Text), 2)).ToString()) : (string.Empty);
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                }
                catch
                {

                }
            }
        }
        protected void gvQuotation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            if (e.CommandName == "EditQuotation")
            {
                ID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    BindQuotationToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            if (e.CommandName == "DeleteQuotation")
            {
                client.DeleteQuotation(Convert.ToInt32(e.CommandArgument),UserName);
                bindQoutationList();
            }
            if (e.CommandName == "PrintQuotation")
            {
                ID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    PrintQuotation();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }
            }
           
        }
        #endregion

        #region pagelist 
        protected void gvQuotation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvQuotationList.PageIndex = e.NewPageIndex;
            bindQoutationList();
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

        protected void gvQuotation_Sorting(object sender, GridViewSortEventArgs e)
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
            bindQoutationList();
        }
        #endregion
       
    }
}