using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class Funeral : AdminBasePage
    {
        #region Fields
        #endregion

        #region Page Property
        public int FID
        {
            get
            {
                if (ViewState["_FID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_FID"]);
            }
            set
            {
                ViewState["_FID"] = value;
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

                return (viewState == null) ? "pkiFuneralID" : (string)viewState;
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
            this.dbPageId = 10;
        }
        #endregion
        //#region PageInit
        //protected void Page_Init(object sender, EventArgs e)
        //{            
        //    SecureUserGroupsModel[] obj = client.EditSecurUserbyID(UserID);
        //    List<int> list = new List<int>();
        //    list.Add(9);
        //    list.Add(4);
        //    list.Add(12);
        //    var result = obj.Where(x => list.Contains(x.fkiSecureGroupID));
        //    if (result.FirstOrDefault() == null)
        //    {
        //        Response.Redirect("~/Admin/403Error.aspx", false);
        //    }
        //}
        //#endregion

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(PageSize.ToString()));
                bindFuneralList();
                btnService.Enabled = false;
                BtnAddDocs.Enabled = false;
                if (Request.QueryString["FID"] != null)
                {
                    FID = Convert.ToInt32(Request.QueryString["FID"]);
                    BindFuneralToUpdate();
                }
                LoadUserRights();
            }
        }
        #endregion

        #region Page size change event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            bindFuneralList();
        }
        #endregion

        #region Methods
        public void LoadUserRights()
        {
            btnSave.Enabled = this.HasCreateRight;
        }
        public void bindFuneralList()
        {
            gvFuneralList.PageSize = PageSize;
            List<FuneralModel> objFuneralModel = FuneralBAL.SelectAllFuneralByParlourId(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType,null,null,null);
            gvFuneralList.DataSource = objFuneralModel;
            gvFuneralList.DataBind();
        }

        public void ClearControl()
        {
            FID = 0;
            txtLastName.Text = string.Empty;
            txtFirstname.Text = string.Empty;
            txtIdNumber.Text = string.Empty;
            txtBirthDay.Text = string.Empty;
            rdbtnMale.Checked = true;
            txtCOD.Text = string.Empty;
            txtDOD.Text = string.Empty;
            txtCollection.Text = string.Empty;
            txtSerialNo.Text = string.Empty;
            txtStreetAddress.Text = string.Empty;
            txtTown.Text = string.Empty;
            txtProvince.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtDateOfFuneral.Text = string.Empty;
            txtDriver.Text = string.Empty;
            txtCemetery.Text = string.Empty;
            txtGraveNo.Text = string.Empty;

            btnService.Enabled = false;
            BtnAddDocs.Enabled = false;
            btnClear.Enabled = false;

            txtContactNumber.Text = string.Empty;
            txtContactPerson.Text = string.Empty;

            gvSupportedDocument.DataSource = null;
            gvSupportedDocument.DataBind();

        }
        public void BindFuneralToUpdate()
        {
            bindSupportedDocuments();
            btnClear.Enabled = true;
            FuneralModel model = FuneralBAL.SelectFuneralBypkid(FID, ParlourId);
            if ((model == null) || (model.parlourid != ParlourId))
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Funeral.');</script>");
            }
            else //(model != null)
            {
                FID = model.pkiFuneralID;
                txtLastName.Text = model.Surname.ToString();
                txtFirstname.Text = model.FullNames.ToString();
                txtIdNumber.Text = model.IDNumber.ToString();
                txtBirthDay.Text = (Convert.ToDateTime(model.DateOfBirth)).ToString("dd MMM yyyy");
                string gender = model.Gender.ToString();
                if (gender == rdbtnMale.Text)
                {
                    rdbtnMale.Checked = true;
                }
                else
                {
                    rdbtnFemale.Checked = true;
                }
                txtCOD.Text = model.CourseOfDearth.ToString();
                txtDOD.Text = (Convert.ToDateTime(model.DateOfDeath)).ToString("dd MMM yyyy");
                txtCollection.Text = model.BodyCollectedFrom.ToString();
                txtSerialNo.Text = model.BI1663.ToString();
                txtStreetAddress.Text = model.Address1.ToString();
                txtTown.Text = model.Address2.ToString();
                txtProvince.Text = model.Address3.ToString();
                txtCode.Text = model.Code.ToString();
                txtDateOfFuneral.Text = model.DateOfFuneral.ToString("dd MMM yyyy");
                txtDriver.Text = model.DriverAndCars.ToString();
                txtCemetery.Text = model.FuneralCemetery.ToString();
                txtGraveNo.Text = model.GraveNo.ToString();
                txtContactPerson.Text = model.ContactPerson;
                txtContactNumber.Text = model.ContactPersonNumber;
                btnService.Enabled = true;
                BtnAddDocs.Enabled = true;
                btnProcessPayment.Enabled = true;
                btnViewPayments.Enabled = true;
            }
        }
        public void PrintQuotation()
        {
            string id = FID.ToString();
            var plaintextBytes = Encoding.UTF8.GetBytes(id);
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);
            //Response.Redirect("Default2.aspx?name=" + encryptedValue);
            Response.Redirect("~/Admin/PrintForm.aspx?FID=" + encryptedValue);
        }
        #endregion

        #region ButtonEvent
        protected void btn_AddDocs(object sender, EventArgs e)
        {
            string id = FID.ToString();
            var plaintextBytes = Encoding.UTF8.GetBytes(id);
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);
            Response.Redirect("~/Admin/FuneralDocument.aspx?ID=" + encryptedValue);
        }
        protected void btnServiceClick(object sender, EventArgs e)
        {
            string id = FID.ToString();
            var plaintextBytes = Encoding.UTF8.GetBytes(id);
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);

            Response.Redirect("~/Admin/FuneralServicesSelect.aspx?ID=" + encryptedValue);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindFuneralList();
        }
        protected void btnSaveFuneral_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    FuneralModel objfunModel = FuneralBAL.SelectFuneralBypkid(FID, ParlourId);
                    if (objfunModel != null && FID == 0)
                    {
                        ShowMessage(ref lblMessage, MessageType.Danger, "Funeral Details  Already Exists.");
                    }
                    else
                    {
                        objfunModel = new FuneralModel();
                        objfunModel.pkiFuneralID = FID;
                        objfunModel.FullNames = txtFirstname.Text;
                        objfunModel.Surname = txtLastName.Text;
                        string gender = string.Empty;
                        if (rdbtnMale.Checked)
                        {
                            gender = "Male";
                        }
                        else if (rdbtnFemale.Checked)
                        {
                            gender = "Female";
                        }
                        objfunModel.Gender = gender;
                        objfunModel.IDNumber = txtIdNumber.Text;
                        if (txtBirthDay.Text == null || txtBirthDay.Text == "")
                        { objfunModel.DateOfBirth = null; }
                        else
                        {
                            objfunModel.DateOfBirth = Convert.ToDateTime(txtBirthDay.Text);
                        }
                        objfunModel.DateOfDeath = Convert.ToDateTime(txtDOD.Text);
                        objfunModel.DateOfFuneral = Convert.ToDateTime(txtDateOfFuneral.Text);
                        objfunModel.TimeOfFuneral = Convert.ToDateTime(txtTIme.Text);
                        objfunModel.CourseOfDearth = txtCOD.Text;
                        objfunModel.FuneralCemetery = txtCemetery.Text;
                        objfunModel.Address1 = txtStreetAddress.Text;
                        objfunModel.Address2 = txtTown.Text;
                        objfunModel.Address3 = txtProvince.Text;
                        objfunModel.Code = txtCode.Text;
                        objfunModel.BodyCollectedFrom = txtCollection.Text;
                        objfunModel.BI1663 = txtSerialNo.Text;
                        objfunModel.DriverAndCars = txtDriver.Text;
                        objfunModel.GraveNo = txtGraveNo.Text;
                        objfunModel.parlourid = ParlourId;
                        objfunModel.LastModified = System.DateTime.Now;
                        objfunModel.ModifiedUser = UserName;

                        objfunModel.ContactPerson = txtContactPerson.Text;
                        objfunModel.ContactPersonNumber = txtContactNumber.Text;

                        FID = FuneralBAL.SaveFuneral(objfunModel);

                        ShowMessage(ref lblMessage, MessageType.Success, "Funeral Successfully Saved.");
                        //ClearControl();
                        //bindFuneralList();
                        BindFuneralToUpdate();
                        btnService.Enabled = true;
                    }
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
        #endregion

        #region DocumentSupport
        #region Method
        public void bindSupportedDocuments()
        {
            List<FuneralDocumentModel> objSupportedDocumentModel = FuneralBAL.SelectFuneralDocumentsByMemberId(FID);
            gvSupportedDocument.DataSource = objSupportedDocumentModel;
            gvSupportedDocument.DataBind();
        }
        #endregion
        #region ButtonEvent
        protected void BtnDocumentSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (fuSupportDocument.HasFile)
                    {
                        string filename = Path.GetFileName(fuSupportDocument.PostedFile.FileName);
                        string contentType = fuSupportDocument.PostedFile.ContentType;
                        using (Stream fs = fuSupportDocument.PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                FuneralDocumentModel ObjSupportedDocumentModel = new FuneralDocumentModel();
                                ObjSupportedDocumentModel.DocContentType = contentType;
                                ObjSupportedDocumentModel.ImageName = filename;
                                ObjSupportedDocumentModel.ImageFile = bytes;
                                ObjSupportedDocumentModel.fkiFuneralID = FID;
                                ObjSupportedDocumentModel.CreateDate = System.DateTime.Now;
                                ObjSupportedDocumentModel.Parlourid = this.ParlourId;
                                ObjSupportedDocumentModel.LastModified = DateTime.Now;
                                ObjSupportedDocumentModel.ModifiedUser = this.User.Identity.Name;
                                ObjSupportedDocumentModel.DocType = Convert.ToInt32(rdbdocument.SelectedValue.ToString());
                                //int documentId = client.SaveFuneralSupportedDocument(ObjSupportedDocumentModel);

                                int documentId = FuneralBAL.SaveFuneralSupportedDocument(ObjSupportedDocumentModel);
                                //int documentId = FuneralBAL.SaveFuneralSupportedDocument(ObjSupportedDocumentModel);
                                if (documentId > 0)
                                {

                                    ShowMessage(ref lblMessage, MessageType.Success, "Supporting document uploaded successfully");

                                }
                                BindFuneralToUpdate();

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, "Error in add Document: " + ex.Message);
            }
        }

        protected void gvSupportedDocument_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteDocument")
            {
                FuneralBAL.DeleteFuneraldocumentById(Convert.ToInt32(e.CommandArgument));
                bindSupportedDocuments();
            }

        }
        #endregion

        #endregion

        #region control event
        protected void cvIdvalidation_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Page.IsValid)
            {
                bool IDCheck = IdValidationClass.IdValidation(txtIdNumber.Text);
                args.IsValid = IDCheck;
            }
        }
        protected void gvFuneralList_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void gvFuneral_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditFuneral")
            {
                FID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    BindFuneralToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }
            }
            if (e.CommandName == "DeleteFuneral")
            {
                FuneralBAL.FuneralDelete(Convert.ToInt32(e.CommandArgument), UserName);
                bindFuneralList();
            }
            if (e.CommandName == "PrintFuneral")
            {
                FID = Convert.ToInt32(e.CommandArgument);
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
        protected void gvFuneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFuneralList.PageIndex = e.NewPageIndex;
            bindFuneralList();
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

        protected void gvFuneral_Sorting(object sender, GridViewSortEventArgs e)
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
            bindFuneralList();
        }
        #endregion

        protected void btnProcessPayment_Click(object sender, EventArgs e)
        {
            if (FID > 0)
            {
                Response.Redirect("~/Admin/FuneralPayment.aspx?FID=" + FID);
            }
            else
            {
                ShowMessage(ref lblMessage, MessageType.Warning, "No funeral selected");
            }
        }

        protected void btnViewPayments_Click(object sender, EventArgs e)
        {
            UcFuneralPaymentHistory1.FuneralId = this.FID;
            UcFuneralPaymentHistory1.ParlourId = this.ParlourId;
            UcFuneralPaymentHistory1.bindInvoices(this.ParlourId);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

    }
}