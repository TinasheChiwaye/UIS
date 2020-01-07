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
    public partial class QuotationServices : AdminBasePage
    {
        #region Fields

        public int QuotationID
        {
            get
            {
                if (ViewState["_QuotationID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_QuotationID"]);
            }
            set
            {
                ViewState["_QuotationID"] = value;
            }
        }
        public int DiscountID
        {
            get
            {
                if (ViewState["_DiscountID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_DiscountID"]);
            }
            set
            {
                ViewState["_DiscountID"] = value;
            }
        }

        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 44;
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
        //    var result = obj.Where(x => list.Contains(x.fkiSecureGroupID));
        //    if (result.FirstOrDefault() == null)
        //    {
        //        Response.Redirect("~/Admin/403Error.aspx", false);          
        //    }
        //}
        //#endregion

        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString.ToString()))
            {
                var decryptedBytes = MachineKey.Decode(Request.QueryString["ID"], MachineKeyProtection.All);
                var decryptedValue = Encoding.UTF8.GetString(decryptedBytes);
                int QouID = Convert.ToInt32(decryptedValue);
                ViewState["_QuotationID"] = QouID;
            }
            else
            {
                Response.Redirect("~/Admin/Qoutation.aspx");
            }
            if (!IsPostBack)
            {
                BindTax();
                GetCompDetails();
                bindServicesType();
                GetQuotationData();
                bindServiceListList();
                DiscountCal();
                //GetQuotationNumber();
                BindRejectMsg();
                BindAllPackage();
                LoadUserRights();
            }
        }
        #endregion

        #region Method
        public void LoadUserRights()
        {
            btnPackageService.Enabled = HasCreateRight;
            AddServ.Enabled = HasCreateRight;
            btnSave.Enabled = HasCreateRight;
            btnPrint.Enabled = HasCreateRight;
        }
        public void QuotationPrint(int? QutID)
        {
            string id = QutID.ToString();
            var plaintextBytes = Encoding.UTF8.GetBytes(id);
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);
            //Response.Redirect("Default2.aspx?name=" + encryptedValue);
            Response.Redirect("~/Admin/PrintForm.aspx?QID=" + encryptedValue);
        }
        //public void GetQuotationNumber()
        //{
        //    QuotationModel objQuotation = client.SelectQuotationByQuotationId(QuotationID, ParlourId);
        //    if (objQuotation.QuotationNumber == null || objQuotation.QuotationNumber == "")
        //    {
        //        QuotationModel obj = client.GetQuotationNumberByID2(ParlourId);
        //        if (obj.QuotationNumber2 == string.Empty || obj.QuotationNumber2 == "")
        //        {
        //            ViewState["QuotationNumber2"] = "0001";
        //        }
        //        else
        //        {
        //            string s = obj.QuotationNumber2;
        //            s = (Int32.Parse(s) + 1).ToString("D4");
        //            txtQuotationNumber.Text = s;
        //        }
        //    }
        //}
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

        public void bindServicesType()
        {
            try
            {
                List<QuotationServicesModel> objQuoSer = QuotationBAL.GetAllQuotationServices(ParlourId);
                ddlServices.DataTextField = "ServiceName";
                ddlServices.DataValueField = "pkiServiceID";
                ddlServices.DataSource = objQuoSer;
                ddlServices.DataBind();
                ddlServices.Items.Insert(0, new ListItem("Select", "0"));

                QuotationDiscountModel objid = QuotationBAL.GetAllQuotationDiscounts(QuotationID);
                if (objid == null)
                {
                    ViewState["DiscountID"] = "0";
                }
                else
                {
                    ViewState["DiscountID"] = objid.DiscountID;
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }
        }

        public void GetCompDetails()
        {
            ApplicationSettingsModel objApp = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            txtcompanyRules.Text = objApp.ApplicationName + Environment.NewLine + objApp.BusinessAddressLine1 + "," + Environment.NewLine + objApp.BusinessAddressLine2 + "," + Environment.NewLine + objApp.BusinessAddressLine3 + "," + Environment.NewLine + objApp.BusinessAddressLine4 + Environment.NewLine + objApp.BusinessPostalCode;
            ApplicationTnCModel ModelTnc;
            ModelTnc = ToolsSetingBAL.SelectApplicationTermsAndCondition(ParlourId);
            if (ModelTnc != null)
            {
                txtTnc.Text = ModelTnc.TermsAndCondition;
            }
        }


        public void GetQuotationData()
        {
            QuotationModel objQuotation = QuotationBAL.SelectQuotationByQuotationId(QuotationID, ParlourId);
            txtStatus.Text = objQuotation.QuotationStatus;
            txtTo.Text = objQuotation.ContactTitle + " " + objQuotation.ContactLastName + " " + objQuotation.ContactFirstName + Environment.NewLine + objQuotation.AddressLine1 + "," + Environment.NewLine + objQuotation.AddressLine2 + "," + Environment.NewLine + objQuotation.AddressLine3 + "," + Environment.NewLine + objQuotation.AddressLine4 + Environment.NewLine + objQuotation.Code;
            txtQuotationNumber.Text = objQuotation.QuotationNumber;
            txtDate.Text = objQuotation.DateOfQuotation.ToString("dd'/'MM'/'yyyy");
            txtNotes.Text = objQuotation.Notes;
            ddlTax.SelectedValue = (objQuotation.Tax).ToString("N2");
            txtDiscount.Text = (objQuotation.Discount).ToString();
            if (txtStatus.Text == "Accept")
            {
                Accept.Visible = false;
            }

        }
        public void BindRejectMsg()
        {
            QuotationMessageModel objmsg = QuotationBAL.SelectQuotationMessageByID(QuotationID);
            if (objmsg != null)
            {
                txtReject.Text = objmsg.Message.ToString();
                ViewState["pkidQuotationMsg"] = objmsg.pkidQuotationMsg;
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
        public void BindServiceToUpdate()
        {
            int serviceID = Convert.ToInt32(ViewState["ID"]);
            QuotationServicesModel model = QuotationBAL.SelectServiceByQouAndID(QuotationID, serviceID);
            if ((model == null) || (model.QuotationID != QuotationID))
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

        public void bindServiceListList()
        {
            List<QuotationServicesModel> objServ = QuotationBAL.SelectServiceByQoutationID(QuotationID);
            decimal Amt = 0;
            foreach (var item in objServ)
            {
                Amt = Amt + item.Amount;
            }
            SubTotal.Text = Amt.ToString();

            gvServiceList.DataSource = objServ;
            gvServiceList.DataBind();

        }
        public void ClearService()
        {
            ddlServices.ClearSelection();
            txtSerDes.Text = string.Empty;
            txtNumber.Text = "0".ToString();
            txtRate.Text = "0".ToString();
            txtTotal.Text = string.Empty;
        }
        #endregion

        #region Event
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Qoutation.aspx");
        }

        protected void btn_Print(object sender, EventArgs e)
        {
            int QutID = QuotationID;
            QuotationPrint(QutID);
        }

        protected void ChangeData(object sender, EventArgs e)
        {

            int Description = Convert.ToInt32(ddlServices.SelectedValue);
            QuotationServicesModel objQuotation = QuotationBAL.GetServiceByID(Description);

            txtSerDes.Text = objQuotation.ServiceDesc;
            decimal money = objQuotation.ServiceCost;
            string rate = String.Format("{0:#.00}", money);
            txtRate.Text = rate;
            SubTotalcal();
        }
        protected void SaveAllDetails(object sender, EventArgs e)
        {
            string Notes = txtNotes.Text;
            string QuotaNum = txtQuotationNumber.Text;
            int a = QuotationBAL.UpdateAllData(QuotationID, Notes, QuotaNum);
            //Discount Insert
            QuotationDiscountModel objQDM = new QuotationDiscountModel();
            objQDM.DiscountID = Convert.ToInt32(ViewState["DiscountID"]);
            objQDM.QuotationID = QuotationID;
            //objQDM.Amount = Convert.ToInt32((lblDisAmt2.Text).ToString());
            objQDM.LastModified = DateTime.Now;
            objQDM.ModifiedUser = UserName;
            int b = QuotationBAL.SaveDiscountAndAmount(objQDM);
            Decimal Tax = Convert.ToDecimal(ddlTax.SelectedValue);
            Decimal dis = 0;
            if (txtDiscount.Text == null || txtDiscount.Text == "")
            {
                dis = 0;
            }
            else
            {
                dis = Convert.ToDecimal(txtDiscount.Text);
            }

            int save = QuotationBAL.SaveQuotationTaxAndDiscount(QuotationID, Tax, dis);

            ShowMessage(ref lblMessage, MessageType.Success, " Successfully Saved.");

        }
        protected void eveDiscountChange(object sender, EventArgs e)
        {
            DiscountCal();
        }

        protected void calculation(object sender, EventArgs e)
        {
            SubTotalcal();
        }

        protected void btncrtService_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    // QuotationServicesModel objSer = client.SelectQuotationByQuotationId(ID, ParlourId);
                    QuotationServicesModel objSer = null;
                    if (objSer != null)
                    {
                        //ShowMessage(ref lblMessage, MessageType.Danger, "Member Dependenc  Already Exists.");
                    }
                    else
                    {
                        objSer = new QuotationServicesModel();
                        if (ViewState["ID"] != null)
                        {
                            objSer.pkiQuotationSelectionID = Convert.ToInt32(ViewState["ID"]);
                            ViewState["ID"] = null;
                        }
                        objSer.QuotationID = QuotationID;
                        objSer.fkiServiceID = Convert.ToInt32(ddlServices.SelectedValue);
                        objSer.Quantity = Convert.ToInt32(txtNumber.Text);
                        objSer.lastModified = System.DateTime.Now;
                        objSer.modifiedUser = UserName;
                        objSer.ServiceRate = Convert.ToDecimal(txtRate.Text);

                        int a = QuotationBAL.SaveService(objSer);
                        ViewState["QuotationID"] = a;
                        ShowMessage(ref lblMessage, MessageType.Success, "Quotation Successfully Saved.");

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
        protected void btnAcceptClick(object sender, EventArgs e)
        {
            try
            {
                string status = "Accept";
                int accept = QuotationBAL.QuotationStatus(QuotationID, ParlourId, status);
                Response.Redirect("~/Admin/Funeral.aspx", false);
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }
        }

        protected void btn_Reject(object sender, EventArgs e)
        {
            try
            {

                QuotationMessageModel objmsg = null;
                if (objmsg != null)
                {
                    //ShowMessage(ref lblMessage, MessageType.Danger, "Member Dependenc  Already Exists.");
                }
                else
                {

                    objmsg = new QuotationMessageModel();
                    if (ViewState["pkidQuotationMsg"] != null)
                    {
                        objmsg.pkidQuotationMsg = Convert.ToInt32(ViewState["pkidQuotationMsg"]);
                    }
                    objmsg.QuotationID = QuotationID;
                    objmsg.Message = txtReject.Text;
                    objmsg.CreatedDate = System.DateTime.Now;
                    objmsg.LastModified = System.DateTime.Now;
                    objmsg.ModifiedUser = UserName;
                    int a = QuotationBAL.SaveQuotationMessage(objmsg);

                    string status = "Reject";
                    int accept = QuotationBAL.QuotationStatus(QuotationID, ParlourId, status);
                    Response.Redirect("~/Admin/Funeral.aspx", false);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
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
                QuotationBAL.DeleteServiceByID(Convert.ToInt32(e.CommandArgument));
                bindServiceListList();
            }

        }
        protected void gvServiceList_RowDataBound(object sender, GridViewRowEventArgs e)
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
        #endregion

        #region Package related work
        private void BindAllPackage()
        {
            ddlPackage.DataSource = FuneralPackageBAL.GetAllPackage(this.ParlourId);
            ddlPackage.DataTextField = "PackageName";
            ddlPackage.DataValueField = "PackageName";
            ddlPackage.DataBind();
            ddlPackage.Items.Insert(0, new ListItem("Select Package", "0"));
        }

        protected void btnPackageService_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    List<PackageServicesSelectionModel> modelList = FuneralPackageBAL.GetPackageService(this.ParlourId, ddlPackage.SelectedValue).ToList();
                    //FuneralServiceSelectModel objSer = null;

                    QuotationServicesModel objSer = null;

                    foreach (var item in modelList)
                    {
                        objSer = new QuotationServicesModel();
                        if (ViewState["ID"] != null)
                        {
                            objSer.pkiQuotationSelectionID = Convert.ToInt32(ViewState["ID"]);
                            ViewState["ID"] = null;
                        }
                        objSer.QuotationID = QuotationID;
                        objSer.fkiServiceID = item.fkiServiceID;
                        objSer.Quantity = 1;
                        objSer.lastModified = System.DateTime.Now;
                        objSer.modifiedUser = UserName;
                        objSer.ServiceRate = item.ServiceCost;
                        QuotationBAL.SaveService(objSer);
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
        #endregion

        public void BindTax()
        {
            List<TaxSetting> taxSettings = TaxSettingBAL.GetAllTaxSettings().ToList();
            ddlTax.DataSource = taxSettings;
            ddlTax.DataTextField = "TaxText";
            ddlTax.DataValueField = "TaxValue";
            ddlTax.DataBind();
            ddlTax.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}