using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class TombStone : AdminBasePage
    {
        #region Fields
        #endregion

        #region Page Property
        public int TSID
        {
            get
            {
                if (ViewState["_TSID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_TSID"]);
            }
            set
            {
                ViewState["_TSID"] = value;
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

                return (viewState == null) ? "pkiTombstoneID" : (string)viewState;
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
            this.dbPageId = 11;
        }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(PageSize.ToString()));
                bindTombStoneList();
                //GetTombStoneInvoiceNumber();
                btnService.Enabled = false;
                if (Request.QueryString["TID"] != null)
                {
                    TSID = Convert.ToInt32(Request.QueryString["TID"]);
                    bindTombStoneUpdateList();
                }
                btnSave.Enabled = HasCreateRight;
            }
        }
        #endregion

        #region Page size change event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            bindTombStoneList();
        }
        #endregion

        #region Method        

        public void bindTombStoneList()
        {
            gvTombStonelList.PageSize = PageSize;
            List<TombStoneModel> objTombModel = TombStoneBAL.SelectAllTombStoneByParlourId(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
            gvTombStonelList.DataSource = objTombModel;
            gvTombStonelList.DataBind();
        }
        public void ClearControl()
        {
            TSID = 0;
            txtLastName.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtIdNumber1.Text = string.Empty;
            txtPolicyNumber.Text = string.Empty;
            txtAppDate.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtAdd1.Text = string.Empty;
            txtadd2.Text = string.Empty;
            txtadd3.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtCell.Text = string.Empty;
            txtDLastName.Text = string.Empty;
            txtDFirstname.Text = string.Empty;
            txtDIdNumber.Text = string.Empty;
            txtCemetery.Text = string.Empty;
            txtGraveNo.Text = string.Empty;
            txtMessage.Text = string.Empty;
            txtNotes.Text = string.Empty;
            txtDateOfMemorial.Text = string.Empty;
            ImagePreview.ImageUrl = string.Empty;

            txtContactNumber.Text = string.Empty;
            txtContactPerson.Text = string.Empty;

            btnUpload.Visible = false;
            btnService.Enabled = false;
            btnCreateInvoice.Enabled = false;
            btnViewPayments.Enabled = false;


        }
        public void bindTombStoneUpdateList()
        {
            TombStoneModel model = TombStoneBAL.SelectTombStoneByParlAndPki(TSID, ParlourId);
            if ((model == null) || (model.parlourid != ParlourId))
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this TombStone.');</script>");
            }
            else
            {
                btnUpload.Enabled = true;
                TSID = model.pkiTombstoneID;
                txtLastName.Text = model.LastName.ToString();
                txtFirstName.Text = model.FirstName.ToString();
                txtIdNumber1.Text = model.IDNumber.ToString();
                txtPolicyNumber.Text = model.PolicyNumber.ToString();
                if (model.DateOfApplication != null)
                    txtAppDate.Text = (Convert.ToDateTime(model.DateOfApplication)).ToString("dd MMM yyyy");

                txtTel.Text = model.TelNumber.ToString();
                txtAdd1.Text = model.Address1.ToString();
                txtadd2.Text = model.Address2.ToString();
                txtadd3.Text = model.Address3.ToString();
                txtCode.Text = model.Code.ToString();
                txtCell.Text = model.CellNumber.ToString();
                string Deceased = model.Deceased.ToString();
                if (Deceased == rdbBuried.Text)
                {
                    rdbBuried.Checked = true;
                }
                else
                {
                    rdbCremated.Checked = true;
                }
                if (model.DateOFMemorial != null)
                    txtDateOfMemorial.Text = (Convert.ToDateTime(model.DateOFMemorial)).ToString("dd MMM yyyy");

                txtCemetery.Text = model.CemeterOrCrematorium.ToString();
                txtGraveNo.Text = model.GraveNo.ToString();
                string Erect = model.Erect.ToString();
                if (Erect == rdbYes.Text)
                {
                    rdbYes.Checked = true;
                }
                else
                {
                    rdbNo.Checked = true;
                }
                txtMessage.Text = model.TombStoneMessage.ToString();
                txtNotes.Text = model.Notes.ToString();
                txtDFirstname.Text = model.DeceasedFirstName.ToString();
                txtDIdNumber.Text = model.DeceasedIDNumber.ToString();
                txtDLastName.Text = model.DeceasedLastName.ToString();

                txtContactPerson.Text = model.ContactPerson;
                txtContactNumber.Text = model.ContactPersonNumber;

                if (model.ImagePath != null) { ImagePreview.ImageUrl = model.ImagePath; }
                else { ImagePreview.ImageUrl = string.Empty; }

                btnUpload.Visible = true;
                btnService.Enabled = true;
                btnCreateInvoice.Enabled = true;
                btnViewPayments.Enabled = true;

            }
        }
        public void UploadImage(int? a)
        {
            if (flTombStone.HasFile)
            {
                //int b = TSID;
                string strpath = System.IO.Path.GetExtension(flTombStone.FileName);
                if (strpath != ".jpg" && strpath != ".jpeg" && strpath != ".gif" && strpath != ".png")
                {
                    vSummaryQuotation.ForeColor = System.Drawing.Color.Red;
                    vSummaryQuotation.HeaderText = "Only image formats (jpg, png, gif) are accepted ";
                }
                else
                {                    
                    {
                        if ( a != 0) { TSID = Convert.ToInt32(a);}
                        string Str = System.DateTime.Now.Ticks.ToString();                        
                        string filename = TSID + "_" + Str +"_" + Path.GetFileName(flTombStone.PostedFile.FileName);
                        FuneralGeneralMethods.GetUploadPath("TombImage");
                        flTombStone.SaveAs(FuneralGeneralMethods.CreateFolder("~/Upload/TombImage/" + ParlourId) + filename);                        
                        string ImagePath = "~/Upload/TombImage/" + ParlourId+"/" + filename;
                        int b = TombStoneBAL.UploadTombImage(filename, ImagePath, TSID, ParlourId);
                        bindTombStoneUpdateList();
                    }
                }
            }

        }
        public void PrintQuotation()
        {
            string id = TSID.ToString();
            var plaintextBytes = Encoding.UTF8.GetBytes(id);
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);
            //Response.Redirect("Default2.aspx?name=" + encryptedValue);
            Response.Redirect("~/Admin/PrintForm.aspx?TBID=" + encryptedValue);
        }
        #endregion

        #region Button Event
        protected void btnService_Click(object sender, EventArgs e)
        {
            string id = TSID.ToString();
            var plaintextBytes = Encoding.UTF8.GetBytes(id);
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);
            Response.Redirect("~/Admin/TombStoneServiceSelect.aspx?ID=" + encryptedValue);
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindTombStoneList();
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int a = 0;
            UploadImage(a);
        }

        protected void btnSubmitClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    TombStoneModel objTomb = TombStoneBAL.SelectTombStoneByParlAndPki(TSID, ParlourId);
                    if (objTomb != null && TSID == 0)
                    {
                        ShowMessage(ref lblMessage, MessageType.Danger, "TombStone Details  Already Exists.");
                    }
                    else
                    {
                        objTomb = new TombStoneModel();
                        objTomb.pkiTombstoneID = TSID;
                        objTomb.LastName = txtLastName.Text;
                        objTomb.FirstName = txtFirstName.Text;
                        objTomb.IDNumber = txtIdNumber1.Text;
                        objTomb.PolicyNumber = txtPolicyNumber.Text;
                        if (txtAppDate.Text == null || txtAppDate.Text == "")
                        { objTomb.DateOfApplication = null; }
                        else
                        {
                            objTomb.DateOfApplication = Convert.ToDateTime(txtAppDate.Text);
                        }
                        objTomb.TelNumber = txtTel.Text;
                        objTomb.Address1 = txtAdd1.Text;
                        objTomb.Address2 = txtadd2.Text;
                        objTomb.Address3 = txtadd3.Text;
                        objTomb.Code = txtCode.Text;
                        objTomb.CellNumber = txtCell.Text;
                        objTomb.DeceasedLastName = txtDLastName.Text;
                        objTomb.DeceasedFirstName = txtDFirstname.Text;
                        objTomb.DeceasedIDNumber = txtDIdNumber.Text;
                        string Deceased = string.Empty;
                        if (rdbBuried.Checked) { Deceased = "Buried"; }
                        else if (rdbCremated.Checked) { Deceased = "Cremated"; }
                        objTomb.Deceased = Deceased;
                        if (txtDateOfMemorial.Text == null || txtDateOfMemorial.Text == "")
                        { objTomb.DateOFMemorial = null; }
                        else
                        {
                            objTomb.DateOFMemorial = Convert.ToDateTime(txtDateOfMemorial.Text);
                        }
                        objTomb.CemeterOrCrematorium = txtCemetery.Text;
                        objTomb.GraveNo = txtGraveNo.Text;
                        string Erect = string.Empty;
                        if (rdbYes.Checked) { Erect = "Yes"; }
                        else if (rdbNo.Checked) { Erect = "No"; }
                        objTomb.Erect = Erect;
                        objTomb.TombStoneMessage = txtMessage.Text;
                        objTomb.Notes = txtNotes.Text;
                        objTomb.parlourid = ParlourId;
                        objTomb.ModifiedUser = UserName;
                        objTomb.InvoiceNumber = "";
                        objTomb.ContactPerson = txtContactPerson.Text;
                        objTomb.ContactPersonNumber = txtContactNumber.Text;

                        int a = TombStoneBAL.SaveTombStone(objTomb);
                        UploadImage(a);
                        ShowMessage(ref lblMessage, MessageType.Success, "TombStone Details Successfully Saved.");
                        ClearControl();
                        bindTombStoneList();
                        btnUpload.Enabled = false;
                        btnService.Enabled = false;
                        //GetTombStoneInvoiceNumber();

                    }


                }
                catch (Exception ex)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                }
            }
        }

        #endregion

        #region pagelist
        protected void gvTombStone_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTombStonelList.PageIndex = e.NewPageIndex;
            bindTombStoneList();
        }
        #endregion

        #region control event
        protected void cvIdvalidation_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Page.IsValid)
            {
                bool IDCheck = IdValidationClass.IdValidation(txtIdNumber1.Text);
                args.IsValid = IDCheck;
            }
        }
        protected void cvIdvalidation_ServerValidate2(object source, ServerValidateEventArgs args)
        {
            if (Page.IsValid)
            {
                bool IDCheck = IdValidationClass.IdValidation(txtDIdNumber.Text);
                args.IsValid = IDCheck;
            }
        }
        protected void gvTombStoneList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                try
                {
                    e.Row.Cells[4].Text = (e.Row.Cells[4] != null) ? ((Math.Round(Convert.ToDecimal(e.Row.Cells[4].Text), 2)).ToString()) : (string.Empty);
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                }
                catch
                {

                }
            }
        }
        protected void gvTombStone_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditTombStone")
            {
                TSID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    bindTombStoneUpdateList();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }
            }
            if (e.CommandName == "DeleteTombStone")
            {
                TombStoneBAL.TombStoneDelete(Convert.ToInt32(e.CommandArgument), UserName);
                bindTombStoneList();
            }
            if (e.CommandName == "PrintTombstone")
            {
                TSID = Convert.ToInt32(e.CommandArgument);
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

        protected void gvTombStone_Sorting(object sender, GridViewSortEventArgs e)
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
            bindTombStoneList();
        }
        #endregion

        protected void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            if (TSID > 0)
            {
                Response.Redirect("~/Admin/TombstonePayment.aspx?TID=" + TSID);
            }
            else
            {
                ShowMessage(ref lblMessage, MessageType.Warning, "No tombstone selected");
            }
        }

        protected void btnViewPayments_Click(object sender, EventArgs e)
        {
            ucPaymentHistory1.TombstoneId = this.TSID;
            ucPaymentHistory1.ParlourId = this.ParlourId;
            ucPaymentHistory1.bindInvoices(this.ParlourId);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
}