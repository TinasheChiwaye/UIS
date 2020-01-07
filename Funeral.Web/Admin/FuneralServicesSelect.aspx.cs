using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class FuneralServicesSelect : AdminBasePage
    {
        #region Fields
        #endregion

        #region PageProperty
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
        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 33;
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

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString.ToString()))
            {
                var decryptedBytes = MachineKey.Decode(Request.QueryString["ID"], MachineKeyProtection.All);
                var decryptedValue = Encoding.UTF8.GetString(decryptedBytes);
                int TempID = Convert.ToInt32(decryptedValue);
                ViewState["_FID"] = TempID;
            }
            else
            {
                Response.Redirect("~/Admin/Funeral.aspx");
            }
            if (!IsPostBack)
            {
                BindTax();
                bindServicesType();
                GetCompDetails();
                GetFuneralData();
                bindServiceListList();
                DiscountCal();
                BindAllPackage();
                LoadUserRights();

                UcFuneralPaymentHistory1.FuneralId = this.FID;
                UcFuneralPaymentHistory1.ParlourId = this.ParlourId;
                UcFuneralPaymentHistory1.bindInvoices(this.ParlourId);
            }

        }
        #endregion

        #region Method
        public void LoadUserRights()
        {
            btnPackageService.Enabled = this.HasCreateRight;
            AddServ.Enabled = this.HasCreateRight;
            btnSave.Enabled = this.HasCreateRight;
            btnPrint.Enabled = this.HasReadRight;
        }
        protected void btnPrintClick(object sender, EventArgs e)
        {
            string id = FID.ToString();
            var plaintextBytes = Encoding.UTF8.GetBytes(id);
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);

            Response.Redirect("~/Admin/PrintForm.aspx?FID=" + encryptedValue);
        }

        public void bindServicesType()
        {
            try
            {
                List<QuotationServicesModel> objQuoSer = QuotationBAL.GetAllQuotationServices(ParlourId);
                ddlServices.DataTextField = "ServiceName";
                ddlServices.DataValueField = "pkiServiceID";
                ddlServices.DataSource = objQuoSer;
                ddlServices.DataBind();
                ddlServices.Items.Insert(0, new ListItem("Select Service", "0"));


            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }
        }
        public void GetCompDetails()
        {
            ApplicationSettingsModel objApp = new ApplicationSettingsModel();
            objApp = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            txtcompanyRules.Text = objApp.ApplicationName + Environment.NewLine + objApp.BusinessAddressLine1 + "," + Environment.NewLine + objApp.BusinessAddressLine2 + "," + Environment.NewLine + objApp.BusinessAddressLine3 + "," + Environment.NewLine + objApp.BusinessAddressLine4 + Environment.NewLine + objApp.BusinessPostalCode;
            ApplicationTnCModel ModelTnc;
            ModelTnc = ToolsSetingBAL.SelectApplicationTermsAndCondition(ParlourId);
            if (ModelTnc != null)
            {
                txtTnc.Text = ModelTnc.TermsAndCondition;
            }
        }
        public void GetFuneralData()
        {
            FuneralModel objQuotation = FuneralBAL.SelectFuneralBypkid(FID, ParlourId);
            txtTo.Text = objQuotation.Surname + " " + objQuotation.FullNames + Environment.NewLine + objQuotation.Address1 + "," + Environment.NewLine + objQuotation.Address2 + "," + Environment.NewLine + objQuotation.Address3 + "," + Environment.NewLine + objQuotation.Address4 + Environment.NewLine + objQuotation.Code;
            txtInvoiceNumber.Text = objQuotation.InvoiceNumber;
            string CreatedDate = objQuotation.CreatedDate.ToString();
            if (CreatedDate == null || CreatedDate == "01-01-0001 00:00:00")//"01/01/0001 00:00:00"
            {
                txtDate.Text = System.DateTime.Now.ToString("dd/MMM/yyyy");
            }
            else
            {
                txtDate.Text = objQuotation.CreatedDate.ToString("dd/MMM/yyyy");
            }
            if (objQuotation.Tax != null)
            {
                ddlTax.SelectedValue = (objQuotation.Tax).ToString("N2");
            }
            //string datenow = txtDate.Text;
            DateTime Start = DateTime.Parse(txtDate.Text);
            DateTime end = Start.AddHours(48);
            txtDueDate.Text = end.ToString("dd/MMM/yyyy");
            txtDiscount.Text = (objQuotation.Discount).ToString();
            txtNotes.Text = objQuotation.Notes;
        }
        public void bindServiceListList()
        {
            List<FuneralServiceSelectModel> objServ = FuneralBAL.SelectServiceByFuneralID(FID);
            decimal Amt = 0;
            foreach (var item in objServ)
            {
                Amt = Amt + item.Amount;
            }
            SubTotal.Text = Amt.ToString();
            gvServiceList.DataSource = objServ;
            gvServiceList.DataBind();
        }
        public void BindServiceToUpdate()
        {
            int serviceID = Convert.ToInt32(ViewState["ID"]);
            FuneralServiceSelectModel model = FuneralBAL.SelectServiceByFunAndID(FID, serviceID);
            if ((model == null) || (model.fkiFuneralID != FID))
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Quotation.');</script>");
            }
            else
            {

                ddlServices.SelectedValue = model.pkiServiceID.ToString();
                txtSerDes.Text = model.ServiceDesc;
                txtNumber.Text = model.Quantity.ToString();
                txtRate.Text = model.ServiceRate.ToString();

                AddServ.Text = "Update";
            }

        }
        public void ClearService()
        {
            ddlServices.ClearSelection();
            txtSerDes.Text = string.Empty;
            txtNumber.Text = "0".ToString();
            txtRate.Text = "0".ToString();
            txtTotal.Text = string.Empty;
            AddServ.Text = "Add Services";
        }
        #endregion

        #region Calculation
        public void DiscountCal()
        {
            try
            {
                Decimal Dis = 0;
                Decimal DisAmt = 0;
                Decimal sub = Convert.ToDecimal(SubTotal.Text);
                Decimal Tax = Convert.ToDecimal(ddlTax.SelectedValue);
                Decimal TaxAmt = 0;
                Decimal ttl = 0;
                Decimal a = 0;

                TaxAmt = (((sub * Tax) / 100));
                a = (sub + TaxAmt);
                lblTax2.Text = " + " + (TaxAmt).ToString("N2");
                if (txtDiscount.Text == null || txtDiscount.Text == "")
                    Dis = 0;
                else
                    Dis = Convert.ToDecimal(txtDiscount.Text);

                DisAmt = (((a * Dis) / 100));
                lblDisAmt2.Text = "Less:" + (DisAmt).ToString("N2");

                ttl = (a - DisAmt);
                txtGrandTotal.Text = (ttl).ToString("N2");

            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }
        }
        public void SubTotalcal()
        {
            decimal rate = Convert.ToDecimal(txtRate.Text);
            if (txtNumber.Text == string.Empty)
            {
                //txtNumber.Text = Convert.ToInt64(1);
                int qty = Convert.ToInt32(1);
                txtTotal.Text = (rate * qty).ToString();
            }
            //int qty = Convert.ToInt32(txtNumber.Text);
            else
            {
                int qty = Convert.ToInt32(txtNumber.Text);
                txtTotal.Text = (rate * qty).ToString();
            }

        }
        #endregion

        #region Event
        protected void eveDiscountChange(object sender, EventArgs e)
        {
            DiscountCal();
        }
        protected void ChangeData(object sender, EventArgs e)
        {
            int Description = Convert.ToInt32(ddlServices.SelectedValue);
            QuotationServicesModel objQuotation = QuotationBAL.GetServiceByID(Description);

            txtSerDes.Text = objQuotation.ServiceDesc;
            decimal money = objQuotation.ServiceCost;
            string rate = String.Format("{0:#.00}", money);
            txtRate.Text = rate;
            //SubTotalcal();
        }
        protected void calculation(object sender, EventArgs e)
        {
            SubTotalcal();
        }
        #endregion

        #region ButtonEvent
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Funeral.aspx");
        }

        protected void SaveAllDetails(object sender, EventArgs e)
        {
            string Notes = txtNotes.Text;
            // Decimal Discount = Convert.ToDecimal(txtDiscount.Text);
            Decimal Tax = Convert.ToDecimal(ddlTax.SelectedValue);
            Decimal Discount = 0;
            if (txtDiscount.Text == null || txtDiscount.Text == "")
            {
                Discount = 0;
            }
            else
            {
                Discount = Convert.ToDecimal(txtDiscount.Text);
            }

            int a = FuneralBAL.UpdateAllFuneralData(FID, Notes, Discount, Tax);
            ShowMessage(ref lblMessage, MessageType.Success, " Successfully Saved.");
        }
        protected void btncrtService_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    // QuotationServicesModel objSer = client.SelectQuotationByQuotationId(ID, ParlourId);
                    FuneralServiceSelectModel objSer = null;
                    if (objSer != null)
                    {
                        //ShowMessage(ref lblMessage, MessageType.Danger, "Member Dependenc  Already Exists.");
                    }
                    else
                    {
                        objSer = new FuneralServiceSelectModel();
                        if (ViewState["ID"] != null)
                        {
                            objSer.pkiFuneralServiceSelectionID = Convert.ToInt32(ViewState["ID"]);
                            ViewState["ID"] = null;
                        }
                        objSer.fkiFuneralID = FID;
                        objSer.fkiServiceID = Convert.ToInt32(ddlServices.SelectedValue);
                        objSer.Quantity = Convert.ToInt32(txtNumber.Text);
                        objSer.lastModified = System.DateTime.Now;
                        objSer.modifiedUser = UserName;
                        objSer.ServiceRate = Convert.ToDecimal(txtRate.Text);

                        int a = FuneralBAL.SaveFuneralService(objSer);
                        ViewState["FuneralID"] = a;
                        ShowMessage(ref lblMessage, MessageType.Success, "Service Successfully Saved.");

                        bindServiceListList();
                        ClearService();
                        DiscountCal();
                    }


                }
                catch (Exception ex)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                }
            }
        }

        #endregion

        #region Control Event
        protected void gvService_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "EditService")
            {
                ViewState["ID"] = Convert.ToInt32(e.CommandArgument);
                try
                {
                    BindServiceToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }
            }
            if (e.CommandName == "DeleteService")
            {
                FuneralBAL.DeleteFuneralServiceByID(Convert.ToInt32(e.CommandArgument));
                bindServiceListList();
            }

        }
        protected void gvServiceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                try
                {
                    e.Row.Cells[4].Text = (e.Row.Cells[4] != null) ? (Currency.Trim() + " " + (Math.Round(Convert.ToDecimal(e.Row.Cells[4].Text), 2)).ToString()) : (string.Empty);
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                }
                catch
                {

                }
            }
        }
        #endregion

        #region Service related work
        private void BindAllPackage()
        {
            ddlPackage.DataSource = FuneralPackageBAL.GetAllPackage(this.ParlourId);
            ddlPackage.DataTextField = "PackageName";
            ddlPackage.DataValueField = "PackageName";
            ddlPackage.DataBind();
            ddlPackage.Items.Insert(0, new ListItem("Select Package", "0"));
        }
        #endregion
        protected void btnPackageService_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    List<PackageServicesSelectionModel> modelList = FuneralPackageBAL.GetPackageService(this.ParlourId, ddlPackage.SelectedValue);
                    FuneralServiceSelectModel objSer = null;

                    foreach (var item in modelList)
                    {
                        objSer = new FuneralServiceSelectModel();
                        if (ViewState["ID"] != null)
                        {
                            objSer.pkiFuneralServiceSelectionID = Convert.ToInt32(ViewState["ID"]);
                            ViewState["ID"] = null;
                        }
                        objSer.fkiFuneralID = FID;
                        objSer.fkiServiceID = item.fkiServiceID;
                        objSer.Quantity = 1;
                        objSer.lastModified = System.DateTime.Now;
                        objSer.modifiedUser = UserName;
                        objSer.ServiceRate = item.ServiceCost;

                        FuneralBAL.SaveFuneralService(objSer);
                    }


                    ShowMessage(ref lblMessage, MessageType.Success, "Package Successfully Added.");

                    bindServiceListList();
                    ClearService();
                    DiscountCal();
                }
                catch (Exception ex)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                }
            }
        }

        #region TxtBind

        //private void BindCountry()
        //{
        //    ddlCitizenship.DataSource = client.GetCountry();
        //    ddlCitizenship.DataBind();
        //    ddlCitizenship.ClearSelection(); //making sure the previous selection has been cleared
        //    ddlCitizenship.Items.FindByValue("ZA").Selected = true;
        //}
        public void BindTax()
        {
            List<TaxSetting> taxSettings = TaxSettingBAL.GetAllTaxSettings();
            ddlTax.DataSource = taxSettings;
            ddlTax.DataTextField = "TaxText";
            ddlTax.DataValueField = "TaxValue";
            ddlTax.DataBind();
            ddlTax.Items.Insert(0, new ListItem("Select", "0"));
        }
        #endregion

    }
}