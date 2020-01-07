using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI.WebControls;


namespace Funeral.Web.Admin
{
    public partial class PrintForm : AdminBasePage
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
        public int TBID
        {
            get
            {
                if (ViewState["_TBID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_TBID"]);
            }
            set
            {
                ViewState["_TBID"] = value;
            }
        }
        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 36;
        }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString.ToString()))
            {
                if (Request.QueryString["QID"] != null)
                {
                    var decryptedBytes = MachineKey.Decode(Request.QueryString["QID"], MachineKeyProtection.All);
                    var decryptedValue = Encoding.UTF8.GetString(decryptedBytes);
                    int QouID = Convert.ToInt32(decryptedValue);
                    ViewState["_QuotationID"] = QouID;
                }
                else if (Request.QueryString["FID"] != null)
                {
                    var decryptedBytes = MachineKey.Decode(Request.QueryString["FID"], MachineKeyProtection.All);
                    var decryptedValue = Encoding.UTF8.GetString(decryptedBytes);
                    int FID = Convert.ToInt32(decryptedValue);
                    ViewState["_FID"] = FID;
                }
                else if (Request.QueryString["TBID"] != null)
                {
                    var decryptedBytes = MachineKey.Decode(Request.QueryString["TBID"], MachineKeyProtection.All);
                    var decryptedValue = Encoding.UTF8.GetString(decryptedBytes);
                    int TBID = Convert.ToInt32(decryptedValue);
                    ViewState["_TBID"] = TBID;
                }
            }

            else
            {
                Response.Redirect("~/Admin/Dashboard.aspx");
            }
            if (!IsPostBack)
            {
                lblSubtotal.Visible = false;
                getServiceData();
                BindCompany();
                BindCustomerDetails();
                TotalCal();
            }
        }
        #endregion

        #region Bind Data Method
        public void BindCompany()
        {
            ApplicationSettingsModel modelCompany;
            modelCompany = ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
            if (modelCompany != null)
            {
                lblAppliName.Text = modelCompany.ApplicationName.ToString();
                lblAdd1.Text = modelCompany.BusinessAddressLine1.ToString();
                lblAdd2.Text = modelCompany.BusinessAddressLine2.ToString();
                lblAdd3.Text = modelCompany.BusinessAddressLine3.ToString();
                lblAdd4.Text = modelCompany.BusinessAddressLine4.ToString();
                lblCode.Text = modelCompany.BusinessPostalCode.ToString();
                lblEmail.Text = modelCompany.ManageEmail.ToString();
                lblVat.Text = modelCompany.VatNo.ToString();
                lblSlogan.Text = modelCompany.ManageSlogan.ToString();

                lblTelephone.Text = modelCompany.OwnerTelNumber;
                lblCellNumber.Text = modelCompany.OwnerCellNumber;

                lblContactEmail.Text = modelCompany.OwnerEmail;

                BankingDetailSending Modelbank = ToolsSetingBAL.GetBankingByID(modelCompany.parlourid);
                if (Modelbank != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<b>Account Holder &#09;:</b>{0}<br/>", Modelbank.AccountHolder);
                    sb.AppendFormat("<b>Bank name &#09;:</b>{0}<br/>", Modelbank.Bankname);
                    sb.AppendFormat("<b>Account Number &#09;:</b>{0}<br/>", Modelbank.AccountNumber);
                    sb.AppendFormat("<b>Account type &#09;:</b>{0}<br/>", Modelbank.Accounttype);
                    sb.AppendFormat("<b>Branch &#09;:</b>{0}<br/>", Modelbank.Branch);
                    sb.AppendFormat("<b>Branch code &#09;:</b>{0}<br/>", Modelbank.Branchcode);
                    lblBankDetails.Text = sb.ToString();
                }

                if (modelCompany.ApplicationLogo != null)
                {
                    string base64String = Convert.ToBase64String(modelCompany.ApplicationLogo, 0, modelCompany.ApplicationLogo.Length);
                    ImagePreview.ImageUrl = "data:image/png;base64," + base64String;
                }
                else
                {
                    ImagePreview.ImageUrl = string.Empty;
                }
                ApplicationTnCModel ModelTnc;
                ModelTnc = ToolsSetingBAL.SelectApplicationTermsAndCondition(modelCompany.parlourid);
                if (ModelTnc != null)
                {
                    if (QuotationID != 0 && ParlourId != Guid.Empty)
                    {
                        lblTNC.Text = ModelTnc.TermsAndCondition;
                    }
                    else if (FID != 0 && ParlourId != Guid.Empty)
                    {
                        lblTNC.Text = ModelTnc.TermsAndConditionFuneral;
                    }
                    else if (TBID != 0 && ParlourId != Guid.Empty)
                    {
                        lblTNC.Text = ModelTnc.TermsAndConditionTombstone;
                    }
                }

            }
        }
        public void BindCustomerDetails()
        {
            if (QuotationID != 0 && ParlourId != Guid.Empty)
            {
                QuotationModel objQuotation = QuotationBAL.SelectQuotationByQuotationId(QuotationID, ParlourId);
                lblName.Text = objQuotation.ContactTitle + " " + objQuotation.ContactLastName + " " + objQuotation.ContactFirstName;
                lblcAdd1.Text = objQuotation.AddressLine1;
                lblcAdd2.Text = objQuotation.AddressLine2;
                lblcAdd3.Text = objQuotation.AddressLine3;
                lblcAdd4.Text = objQuotation.AddressLine4;
                lblcCode.Text = objQuotation.Code;
                lblDate.Text = objQuotation.DateOfQuotation.ToString("dd MMM yyyy");
                lblInvoiceDateTitle.Text = "Quotation date";
                lblInvoiceDate.Text = objQuotation.DateOfQuotation.ToString("dd MMM yyyy");
                lblNumber1.Text = "Quotation Number";
                lblNumber2.Text = objQuotation.QuotationNumber.ToString();
                lblHeading.Text = "Quotation";
                lblTax.Text = (objQuotation.Tax).ToString("N2");
                lblDiscount.Text = (objQuotation.Discount).ToString();
                lblNotes.Text = objQuotation.Notes;


            }
            else if (FID != 0 && ParlourId != Guid.Empty)
            {
                FuneralModel model = FuneralBAL.SelectFuneralBypkid(FID, ParlourId);
                lblName.Text = string.Format("<b>Contact Person : </b>{0}<br/><b>Contact Number : </b>{1}", model.ContactPerson, model.ContactPersonNumber);
                lblcAdd1.Text = model.Address1;
                lblcAdd2.Text = model.Address2;
                lblcAdd3.Text = model.Address3;
                lblcAdd4.Text = model.Address4;
                lblcCode.Text = model.Code;
                //string CreatedDate = model.CreatedDate.ToString();
                if (model.CreatedDate == null || model.CreatedDate == DateTime.MinValue)//"01/01/0001 00:00:00"
                {
                    if (model.DateOfFuneral != DateTime.MinValue)
                        lblDate.Text = model.DateOfFuneral.ToString("dd/MMM/yyyy");
                    else
                        lblDate.Text = System.DateTime.Now.ToString("dd/MMM/yyyy");
                }
                else
                {
                    lblDate.Text = model.CreatedDate.ToString("dd/MMM/yyyy");
                }

                lblInvoiceDateTitle.Text = "Invoice date";
                lblInvoiceDate.Text = lblDate.Text;

                //string datenow = lblDate.Text;
                DateTime Start = DateTime.Parse(lblDate.Text);
                DateTime end = Start.AddHours(48);
                lblDueDate1.Text = end.ToString("dd/MMM/yyyy");
                lblDueDate.Text = "Due Date";
                lblNumber1.Text = "Invoice Number";
                lblNumber2.Text = model.InvoiceNumber.ToString();
                lblHeading.Text = "Funeral Invoice";
                lblTax.Text = (model.Tax).ToString("N2");
                lblDiscount.Text = (model.Discount).ToString();
                lblNotes.Text = model.Notes;
                lblDecename1.Text = "Name:";
                lblDecename2.Text = string.Format("{0} {1}", model.FullNames, model.Surname);
                lblDeceIDnum1.Text = "ID Number:";
                lblDeceIDnum2.Text = model.IDNumber;
                lblDeceBI1.Text = "BI663 Serial No:";
                lblDeceBI2.Text = model.BI1663;
                lblCemetery1.Text = "Cemetery:";
                lblCemetery2.Text = model.FuneralCemetery;
                lblDeceDate1.Text = "Date: " + (model.DateOfFuneral).ToString("dd/MMM/yyyy") + " ";
                lblDeceDate2.Text = "At " + (model.TimeOfFuneral).ToString("hh:mm");
                lblsemi.Text = ":";
            }
            else if (TBID != 0 && ParlourId != Guid.Empty)
            {
                TombStoneModel model = TombStoneBAL.SelectTombStoneByParlAndPki(TBID, ParlourId);
                lblName.Text = string.Format("<b>Contact Person : </b>{0}<br/><b>Contact Number : </b>{1}", model.ContactPerson, model.ContactPersonNumber);
                lblcAdd1.Text = model.Address1;
                lblcAdd2.Text = model.Address2;
                lblcAdd3.Text = model.Address3;
                lblcAdd4.Text = model.Address4;
                lblcCode.Text = model.Code;
                //string CreatedDate = model.CreatedDate.ToString();
                if (model.CreatedDate == null || model.CreatedDate == DateTime.MinValue)//"01/01/0001 00:00:00"
                {
                    if (model.DateOfApplication != null && model.DateOfApplication != DateTime.MinValue)
                        lblDate.Text = Convert.ToDateTime(model.DateOfApplication).ToString("dd/MMM/yyyy");
                    else
                        lblDate.Text = System.DateTime.Now.ToString("dd/MMM/yyyy");
                    lblDate.Text = System.DateTime.Now.ToString("dd/MMM/yyyy");
                }
                else
                {
                    lblDate.Text = model.CreatedDate.ToString("dd/MMM/yyyy");
                }

                //lblInvoiceDateTitle.Text = "Invoice date";
                //lblInvoiceDate.Text = lblDate.Text;



                string datenow = lblDate.Text;
                DateTime Start = DateTime.Parse(datenow);
                DateTime end = Start.AddHours(48);
                lblDueDate1.Text = end.ToString("dd/MMM/yyyy");
                lblDueDate.Text = "Due Date :";
                lblNumber1.Text = "Invoice Number";
                lblNumber2.Text = model.InvoiceNumber.ToString();
                lblHeading.Text = "Tombstone Invoice";
                lblTax.Text = (model.Tax).ToString("N2");
                lblDiscount.Text = (model.DisCount).ToString();
                lblNotes.Text = model.Notes;
                lblMsg1.Text = "Message:";
                lblMsg2.Text = model.TombStoneMessage;
                lblsemi.Text = ":";

                lblInvoiceDateTitle.Text = "<b>Deceased Info &nbsp;&nbsp;</b>";
                lblDecename1.Text = "<b>Id number &nbsp;&nbsp;</b>";
                lblDeceIDnum1.Text = "<b>Date of death &nbsp;:&nbsp;</b>";
                lblDeceBI1.Text = "<b>Cemetery name &nbsp;:&nbsp;</b>";
                lblCemetery1.Text = "<b>Grave no &nbsp;:&nbsp;</b>";

                lblInvoiceDate.Text = string.Format("{0} {1}", model.DeceasedLastName, model.DeceasedFirstName);
                lblDecename2.Text = model.DeceasedIDNumber;
                lblDeceIDnum2.Text = model.DateOFMemorial.HasValue ? Convert.ToDateTime(model.DateOFMemorial).ToString("dd/MMM/yyyy") : "Not provided";
                lblDeceBI2.Text = model.CemeterOrCrematorium;
                lblCemetery2.Text = model.GraveNo;
            }
        }

        public string getServiceData()
        {
            string data = "";
            decimal TotalPayment = 0;
            StringBuilder sb = new StringBuilder();
            if (QuotationID != 0)
            {
                List<QuotationServicesModel> objServ = QuotationBAL.SelectServiceByQoutationID(QuotationID);
                if (objServ != null)
                {
                    sb.Append("<table  border='1'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th class='service'>SERVICE</th>");
                    sb.Append("<th class='desc'>DESCRIPTION</th>");
                    sb.Append("<th>PRICE</th>");
                    sb.Append("<th>QTY</th>");
                    sb.Append("<th>TOTAL</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tbody>");
                    foreach (var Service in objServ)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td class='service'>");
                        sb.Append(Service.ServiceName);
                        sb.Append("</td>");
                        sb.Append("<td class='desc'>");
                        sb.Append(Service.ServiceDesc);
                        sb.Append("</td>");
                        sb.Append("<td class='unit'>");
                        sb.Append(Service.ServiceRate);
                        sb.Append("</td>");
                        sb.Append("<td class='qty'>");
                        sb.Append(Service.Quantity);
                        sb.Append("</td>");
                        sb.Append("<td class='total'>");
                        sb.Append(Currency.Trim() + " " + Service.Amount);
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4'>"); sb.Append("SUBTOTAL"); sb.Append("</td>");
                    decimal Amt = 0;
                    foreach (var item in objServ)
                    {
                        Amt = Amt + item.Amount;
                    }
                    sb.Append("<td class='total'>"); sb.Append(Currency.Trim() + " " + Amt); sb.Append("</td>");
                    sb.Append("</tr>");
                    lblSubtotal.Text = Currency.Trim() + " " + Amt.ToString();
                    sb.Append("</tbody>");
                    sb.Append("</table");
                }
            }
            else if (FID != 0)
            {
                List<FuneralServiceSelectModel> objServ = FuneralBAL.SelectServiceByFuneralID(FID);
                TotalPayment = MemberPaymentBAL.ReturnFuneralPayments(this.ParlourId, FID.ToString()).Sum(x => x.AmountPaid);
                if (objServ != null)
                {
                    sb.Append("<table  border='1'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th class='service'>SERVICE</th>");
                    sb.Append("<th class='desc'>DESCRIPTION</th>");
                    sb.Append("<th>PRICE</th>");
                    sb.Append("<th>QTY</th>");
                    sb.Append("<th>TOTAL</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tbody>");
                    foreach (var Service in objServ)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td class='service'>");
                        sb.Append(Service.ServiceName);
                        sb.Append("</td>");
                        sb.Append("<td class='desc'>");
                        sb.Append(Service.ServiceDesc);
                        sb.Append("</td>");
                        sb.Append("<td class='unit'>");
                        sb.Append(Service.ServiceRate);
                        sb.Append("</td>");
                        sb.Append("<td class='qty'>");
                        sb.Append(Service.Quantity);
                        sb.Append("</td>");
                        sb.Append("<td class='total'>");
                        sb.Append(Currency.Trim() + " " + Service.Amount);
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4'>"); sb.Append("SUBTOTAL"); sb.Append("</td>");
                    decimal Amt = 0;
                    foreach (var item in objServ)
                    {
                        Amt = Amt + item.Amount;
                    }
                    sb.Append("<td class='total'>"); sb.Append(Currency.Trim() + " " + Amt); sb.Append("</td>");
                    sb.Append("</tr>");
                    lblSubtotal.Text = Currency.Trim() + " " + Amt.ToString();
                    sb.Append("</tbody>");
                    sb.Append("</table");
                }
            }
            else if (TBID != 0)
            {
                List<TombStoneServiceSelectModel> objServ = TombStoneBAL.SelectServiceByTombStoneID(TBID);
                TotalPayment = TombStonesPaymentBAL.TombStonesPaymentSelectByTombstoneID(this.ParlourId, TBID).ToList().Sum(x => x.AmountPaid);
                if (objServ != null)
                {
                    sb.Append("<table  border='1'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th class='service'>SERVICE</th>");
                    sb.Append("<th class='desc'>DESCRIPTION</th>");
                    sb.Append("<th>PRICE</th>");
                    sb.Append("<th>QTY</th>");
                    sb.Append("<th>TOTAL</th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tbody>");
                    foreach (var Service in objServ)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td class='service'>");
                        sb.Append(Service.ServiceName);
                        sb.Append("</td>");
                        sb.Append("<td class='desc'>");
                        sb.Append(Service.ServiceDesc);
                        sb.Append("</td>");
                        sb.Append("<td class='unit'>");
                        sb.Append(Service.ServiceRate);
                        sb.Append("</td>");
                        sb.Append("<td class='qty'>");
                        sb.Append(Service.Quantity);
                        sb.Append("</td>");
                        sb.Append("<td class='total'>");
                        sb.Append(Currency.Trim() + " " + Service.Amount);
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4'>"); sb.Append("SUBTOTAL"); sb.Append("</td>");
                    decimal Amt = 0;
                    foreach (var item in objServ)
                    {
                        Amt = Amt + item.Amount;
                    }
                    sb.Append("<td class='total'>"); sb.Append(Currency.Trim() + " " + Amt); sb.Append("</td>");
                    sb.Append("</tr>");
                    lblSubtotal.Text = Currency.Trim() + " " + Amt.ToString();
                    sb.Append("</tbody>");
                    sb.Append("</table");
                }
            }
            lblTotalPayment.Text = Currency.Trim() + " " + TotalPayment == null ? "0" : TotalPayment.ToString("N2");
            ltrData.Text = sb.ToString();
            return data;
        }
        public void TotalCal()
        {
            if (lblSubtotal.Text != "" && lblSubtotal.Text != string.Empty && lblDiscount.Text != "" && lblDiscount.Text != string.Empty && lblTax.Text != "" && lblTax.Text != string.Empty)
            {
                //double r = Convert.ToDouble(lblDiscount.Text);
                //double s = Convert.ToDouble(lblSubtotal.Text);
                //double tax = Convert.ToDouble(lblTax.Text);
                //double di = (s - ((s * r) / 100));
                //double ttl = (di + ((di * tax) / 100));
                // lblGtotal.Text = ttl.ToString();
                string totalPayment = lblTotalPayment.Text.ToString().Replace(Currency.ToString(), string.Empty).Trim();
                decimal totalPaid = Convert.ToDecimal(totalPayment);

                Decimal Dis = 0;
                Decimal DisAmt = 0;

                string subTotal = lblSubtotal.Text.ToString().Replace(Currency.ToString(), string.Empty).Trim();
                Decimal sub = Convert.ToDecimal(subTotal);
                Decimal Tax = Convert.ToDecimal(lblTax.Text);
                Decimal TaxAmt = 0;
                Decimal ttl = 0;
                Decimal a = 0;

                TaxAmt = (((sub * Tax) / 100));
                a = (sub + TaxAmt);
                lblTax2.Text = " + " + (TaxAmt).ToString("N2");
                if (string.IsNullOrEmpty(lblDiscount.Text))
                    Dis = 0;
                else
                    Dis = Convert.ToDecimal(lblDiscount.Text.Replace("R", ""));

                DisAmt = (((a * Dis) / 100));
                lblDisAmt2.Text = " - " + (DisAmt).ToString("N2");

                ttl = (a - DisAmt);

                lblGtotal.Text = Currency.Trim() + " " + (ttl - totalPaid).ToString("N2");
            }
        }
        #endregion

    }
}