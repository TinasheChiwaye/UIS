using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class Claims : AdminBasePage
    {
        private readonly ISiteSettings _siteConfig = new SiteSettings();
        #region Fields
        #endregion

        #region Page Property

        public int DependentId
        {
            get
            {
                if (ViewState["_DependentId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_DependentId"]);
            }
            set
            {
                ViewState["_DependentId"] = value;
            }
        }
        public int MemberId
        {
            get
            {
                if (ViewState["_MemberId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_MemberId"]);
            }
            set
            {
                ViewState["_MemberId"] = value;
            }
        }
        public int pkiClaimID
        {
            get
            {
                if (ViewState["_pkiClaimID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_pkiClaimID"]);
            }
            set
            {
                ViewState["_pkiClaimID"] = value;
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

                return (viewState == null) ? "pkiClaimID" : (string)viewState;
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

        public string SortOrder
        {
            get
            {
                if (ViewState["_SortOrder"] == null)
                    return "DESC";
                else { return ViewState["_SortOrder"].ToString(); }
            }
            set { ViewState["_SortOrder"] = value; }

        }

        public string SortBy
        {
            get
            {
                if (ViewState["_SortBy"] == null)
                    return "pkiClaimID";
                else { return ViewState["_SortBy"].ToString(); }
            }
            set { ViewState["_SortBy"] = value; }

        }
        public int PageNum1
        {
            get
            {
                if (ViewState["_PageNum1"] == null)
                    return 1;
                else { return Convert.ToInt32(ViewState["_PageNum1"].ToString()); }
            }
            set { ViewState["_PageNum1"] = value; }

        }
        public DataTable LocalQoute
        {
            get
            {
                if (Session["LocalQoute"] != null)
                {
                    return Session["LocalQoute"] as DataTable;
                }
                else
                {
                    return null;
                }
            }
            set { Session["LocalQoute"] = value; }
        }
        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 12;
        }
        #endregion       

        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsAdministrator == true)
                    txtCoverAmt.ReadOnly = false;
                else
                {
                    txtCoverAmt.ReadOnly = true;
                }
                ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(ddlPageSize.Items.FindByValue(PageSize.ToString()));
                GetAllSocietye();
                BindBank();
                //BindPaymentHistoryMember();
                LocalQoute = applictionLogo();
                btnPaymentHistory.Enabled = false;
                LoadUserRights();
                LoadStatus();
                BindCompanyList(ddlCompanyList, dvAdministrator);
            }

        }
        #endregion

        #region Page size change event
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindClaims();
        }
        protected void gvMembersPaymentHistory_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvMembersPaymentHistory.PageIndex = e.NewPageIndex;
            BindPaymentHistoryMember();
        }
        #endregion

        #region Method        
        public void LoadStatus()
        {
            List<StatusModel> statusList = CommonBAL.GetStatus(FuneralEnum.StatusAssociatedTable.Claims.ToString()).ToList();
            ddlStatus.DataSource = statusList;
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Select Claims Status", "0"));
            ddlStatusSearch.DataSource = statusList;
            ddlStatusSearch.DataBind();
            ddlStatusSearch.Items.Insert(0, new ListItem("Any", "0"));
        }
        public void LoadUserRights()
        {
            btnSave.Enabled = this.HasCreateRight;
        }
        public void BindPaymentHistoryMember()
        {
            gvMembersPaymentHistory.DataSource = null;
            gvMembersPaymentHistory.DataBind();
            gvMembersPaymentHistory.PageSize = PageSize;
            StringBuilder sb = new StringBuilder();
            List<MemberInvoiceModel> objMemberInvoiceModel = MembersBAL.GetInvoicesByMemberID(ParlourId, MemberId);
            gvMembersPaymentHistory.DataSource = objMemberInvoiceModel;
            gvMembersPaymentHistory.DataBind();

            if (IsPostBack)
                ClientScript.RegisterStartupScript(GetType(), "hwa", "SearchPopUp('SearchMember');", true);
        }
        public void bindClaimsList()
        {

        }

        public void ClearControl()
        {
            //Claims Details
            pkiClaimID = 0;
            txtClaimDate.Text = string.Empty;
            txtCauseOfDeath.Text = string.Empty;
            txtClaimNotes.Text = string.Empty;
            txtClaimNo.Text = string.Empty;
            txtCoverAmt.Text = string.Empty;
            ckbHostingFuneral.Checked = false;
            //Claimant Details
            ddlClaimantTitle.SelectedValue = "0";
            txtClaimantLastName.Text = string.Empty;
            txtClaimantFirstname.Text = string.Empty;
            txtClaimantIdNumber.Text = string.Empty;
            txtClaimantDateOfBirth.Text = string.Empty;
            txtClaimantAddress1.Text = string.Empty;
            txtClaimantAddress2.Text = string.Empty;
            txtClaimantAddress3.Text = string.Empty;
            txtClaimantCode.Text = string.Empty;
            txtClaimantContactNumber.Text = string.Empty;
            //Beneficiery 
            ddlBank.SelectedValue = "0";
            txtBranch.Text = string.Empty;
            txtBranchcode.Text = string.Empty;
            txtAccountholder.Text = string.Empty;
            txtAccountno.Text = string.Empty;
            ddlAccountType.SelectedValue = "0";
            //Deceased details
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

            //added later
            gvSupportedDocument.DataSource = null;
            gvSupportedDocument.DataBind();
            MemberId = 0;
            DependentId = 0;

            chkAddpayout.Checked = false;
            txtAddpayout.Text = string.Empty;

            btnPaymentHistory.Enabled = false;
            //btnAddDocs.Enabled = false;
            btnPrintClaim.Enabled = false;
        }

        public void bindSupportedDocuments()
        {
            List<ClaimDocumentModel> objSupportedDocumentModel = ClaimsBAL.SelectClaimDocumentsByClaimId(pkiClaimID);
            gvSupportedDocument.DataSource = objSupportedDocumentModel;
            gvSupportedDocument.DataBind();
        }
        #endregion

        #region Claim Report
        public DataTable applictionLogo()
        {
            SqlCommand com1 = new SqlCommand();
            com1.CommandType = CommandType.StoredProcedure;
            com1.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com1.CommandText = "ApplicationSelectByParlourId";
            com1.Parameters.Add(new SqlParameter("@ParlourId", ParlourId));
            SqlDataAdapter adp1 = new SqlDataAdapter(com1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);
            return dt1;
        }
        protected void btnPrintClaim_click(object sender, EventArgs e)
        {
            //BindReportData();
            //ClientScript.RegisterStartupScript(GetType(), "hwa", "selectFollowUpPopUp(\'Report\');", true);


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            string filename;

            try
            {
                ReportViewer rpw = new ReportViewer();
                rpw.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new MyReportServerCredentials();
                rpw.ServerReport.ReportServerCredentials = irsc;

                rpw.ProcessingMode = ProcessingMode.Remote;
                rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                rpw.ServerReport.ReportPath = "/Unplugg IT Solution BI Reporting/Claims doc";
                ReportParameterCollection reportParameters = new ReportParameterCollection();

                reportParameters.Add(new ReportParameter("PkiClaimID", pkiClaimID.ToString()));
                reportParameters.Add(new ReportParameter("Parlourid", this.ParlourId.ToString()));
                rpw.ServerReport.SetParameters(reportParameters);
                string ExportTypeExtensions = "pdf";
                byte[] bytes = rpw.ServerReport.Render(ExportTypeExtensions, null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                filename = string.Format("{0}.{1}", "Claims doc", ExportTypeExtensions);

                Response.ClearHeaders();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.ContentType = mimeType;
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();


            }
            catch (Exception exc)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
            }

            //BindReportData();
            //ClientScript.RegisterStartupScript(GetType(), "hwa", "selectFollowUpPopUp(\'Report\');", true);
        }
        public void BindReportData()
        {
            try
            {
                LocalReport Rpt = ReportViewerdata.LocalReport;
                Rpt.DataSources.Clear();

                MembersModel objmodel = ClaimsBAL.selectMemberByPkidAndParlor(ParlourId, MemberId);
                //PlanModel pm = client.GetPlanDetailsByPlanId(objmodel.fkiPlanID);
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.StoredProcedure;
                com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com.CommandText = "GetPlanDetailsByPlanId";
                com.Parameters.Add(new SqlParameter("@ID", objmodel.fkiPlanID));
                SqlDataAdapter planadp = new SqlDataAdapter(com);
                DataTable PlanDt = new DataTable();
                planadp.Fill(PlanDt);
                SqlCommand com2 = new SqlCommand();
                com2.CommandType = CommandType.StoredProcedure;
                com2.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com2.CommandText = "MemberSelectList";
                com2.Parameters.Add(new SqlParameter("@ID", MemberId));
                com2.Parameters.Add(new SqlParameter("@ParlourId", ParlourId));
                SqlDataAdapter MemberAdp = new SqlDataAdapter(com2);
                DataTable Memberdt = new DataTable();
                MemberAdp.Fill(Memberdt);
                SqlCommand com3 = new SqlCommand();
                com3.CommandType = CommandType.StoredProcedure;
                com3.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com3.CommandText = "GetClaimsbyMemeberNumber";
                com3.Parameters.Add(new SqlParameter("@MemberNumber", pkiClaimID));
                SqlDataAdapter ClaimAdp = new SqlDataAdapter(com3);
                DataTable claimDT = new DataTable();
                ClaimAdp.Fill(claimDT);
                SqlCommand com4 = new SqlCommand();
                com4.CommandType = CommandType.StoredProcedure;
                com4.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com4.CommandText = "SelectFuneralByMemberNo";
                com4.Parameters.Add(new SqlParameter("@MemberNumber", pkiClaimID.ToString()));
                SqlDataAdapter DeceAdp4 = new SqlDataAdapter(com4);
                DataTable decedt = new DataTable();
                DeceAdp4.Fill(decedt);
                SqlCommand com5 = new SqlCommand();
                com5.CommandType = CommandType.StoredProcedure;
                com5.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com5.CommandText = "SelectApplicationTnCByParlourId";
                com5.Parameters.Add(new SqlParameter("@ParlourId", ParlourId));
                SqlDataAdapter TnCadp = new SqlDataAdapter(com5);
                DataTable dtTnC = new DataTable();
                TnCadp.Fill(dtTnC);

                ReportViewerdata.Visible = true;
                ReportViewerdata.LocalReport.EnableExternalImages = true;
                ReportViewerdata.LocalReport.ReportPath = "admin/Reports/ReportLayouts/ReservationClaim.rdlc";
                ReportViewerdata.LocalReport.DataSources.Add(new ReportDataSource("DtApplicationSetting", LocalQoute));
                ReportViewerdata.LocalReport.DataSources.Add(new ReportDataSource("dsGetPlanDetailsByPlanId", PlanDt));
                ReportViewerdata.LocalReport.DataSources.Add(new ReportDataSource("dsMemberSelectList", Memberdt));
                ReportViewerdata.LocalReport.DataSources.Add(new ReportDataSource("DsClaimReoprt", claimDT));
                ReportViewerdata.LocalReport.DataSources.Add(new ReportDataSource("dsFuneralDataset", decedt));
                ReportViewerdata.LocalReport.DataSources.Add(new ReportDataSource("dsTermsAndConditionOfApplication", dtTnC));

                ReportViewerdata.DataBind();
                ReportParameterCollection reportParameters = new ReportParameterCollection();
                reportParameters.Add(new ReportParameter("txtApplicantName", ApplicationName));
                this.ReportViewerdata.LocalReport.SetParameters(reportParameters);
                ReportViewerdata.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }

        }
        #endregion

        #region ButtonEvent

        protected void btnPaymentHistory_click(object sender, EventArgs e)
        {
            BindPaymentHistoryMember();
        }
        protected void btnResetTab_Click(object sender, EventArgs e)
        {
            Response.Redirect("Claims.aspx");
        }
        protected void btnNextTb1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab1", "goToTab(2)", true);
            }

        }
        protected void btnTab2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab1", "goToTab(3)", true);
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindMembeOrDependenciesData();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab1", "goToTab(1)", true);
        }
        protected void btnClaimsSearch_Click(object sender, EventArgs e)
        {
            BindClaims();
        }
        public void BindClaimsSearchDetails()
        {
            try
            {
                List<ClaimsModel> Claimmodel = ClaimsBAL.GetClaimsbyMemeberNumber(pkiClaimID);
                string claimid = pkiClaimID.ToString();
                FuneralModel funeralmodel = FuneralBAL.SelectFuneralByMemberNo(claimid);

                //Claims Details
                if (Claimmodel != null)
                {

                    foreach (var objmodel in Claimmodel)
                    {
                        MemberId = objmodel.fkiMemberID;
                        txtClaimDate.Text = (Convert.ToDateTime(objmodel.ClaimDate)).ToString("dd MMM yyyy");
                        txtCauseOfDeath.Text = objmodel.CourseOfDearth;
                        txtClaimNo.Text = objmodel.pkiClaimID.ToString();
                        txtCoverAmt.Text = objmodel.Cover.ToString("n2");
                        ddlClaimantTitle.SelectedValue = objmodel.ClaimantTitle;
                        txtClaimantAddress1.Text = objmodel.ClaimantAddressLine1;
                        txtClaimantAddress2.Text = objmodel.ClaimantAddressLine2;
                        txtClaimantAddress3.Text = objmodel.ClaimantAddressLine3;
                        txtClaimantLastName.Text = objmodel.ClaimantSurname;
                        txtClaimantFirstname.Text = objmodel.ClaimantFullname;
                        txtClaimantIdNumber.Text = objmodel.ClaimantIDNumber;
                        txtClaimantCode.Text = objmodel.ClaimantCode;
                        txtClaimantDateOfBirth.Text = (Convert.ToDateTime(objmodel.ClaimantDateOfBirth)).ToString("dd MMM yyyy");
                        txtClaimantContactNumber.Text = objmodel.ClaimantContactNumber;
                        txtClaimNotes.Text = objmodel.ClaimNotes;
                        ckbHostingFuneral.Checked = objmodel.HostingFuneral;
                        chkAddpayout.Checked = objmodel.Payout;
                        txtAddpayout.Text = objmodel.PayoutValue.ToString();
                        try
                        {
                            if (objmodel.BeneficiaryBank != null && objmodel.BeneficiaryBank != "")
                                ddlBank.SelectedValue = objmodel.BeneficiaryBank;
                            txtAccountholder.Text = objmodel.BeneficiaryAccountHolder;
                            txtBranch.Text = objmodel.BeneficiaryBankBranch;
                            txtAccountno.Text = objmodel.BeneficiaryAccountNumber;
                            txtBranchcode.Text = objmodel.BeneficiaryBranchCode;
                            if (objmodel.BeneficiaryAccountType != null && objmodel.BeneficiaryAccountType != "")
                                ddlAccountType.SelectedValue = objmodel.BeneficiaryAccountType;
                        }
                        catch (Exception)
                        {
                            goto Finish;
                        }
                        Finish:
                        hdnMemberNo.Value = objmodel.MemberNumber.ToString();
                        //SelectFuneralByMemberNo
                    }
                }

                //Funerals Details
                if (funeralmodel != null)
                {
                    FID = funeralmodel.pkiFuneralID;
                    txtLastName.Text = funeralmodel.Surname;
                    txtFirstname.Text = funeralmodel.FullNames;
                    txtIdNumber.Text = funeralmodel.IDNumber;
                    if (funeralmodel.DateOfBirth != null)
                        txtBirthDay.Text = (Convert.ToDateTime(funeralmodel.DateOfBirth)).ToString("dd MMM yyyy");
                    string Gender = (funeralmodel.Gender).ToString();
                    if (Gender == rdbtnMale.Text)
                    {
                        rdbtnMale.Checked = true;
                    }
                    else
                    {
                        rdbtnFemale.Checked = true;
                    }
                    txtCOD.Text = funeralmodel.CourseOfDearth.ToString();
                    if (funeralmodel.DateOfDeath != null)
                        txtDOD.Text = (Convert.ToDateTime(funeralmodel.DateOfDeath)).ToString("dd MMM yyyy");
                    txtCollection.Text = funeralmodel.BodyCollectedFrom.ToString();
                    txtSerialNo.Text = funeralmodel.BI1663.ToString();
                    txtStreetAddress.Text = funeralmodel.Address1.ToString();
                    txtTown.Text = funeralmodel.Address2.ToString();
                    txtProvince.Text = funeralmodel.Address3.ToString();
                    txtCode.Text = funeralmodel.Code.ToString();
                    if (funeralmodel.DateOfFuneral != null)
                        txtDateOfFuneral.Text = funeralmodel.DateOfFuneral.ToString("dd MMM yyyy");
                    txtTime.Text = Convert.ToDateTime(funeralmodel.TimeOfFuneral).ToShortTimeString();
                    txtDriver.Text = funeralmodel.DriverAndCars.ToString();
                    txtCemetery.Text = funeralmodel.FuneralCemetery.ToString();
                    txtGraveNo.Text = funeralmodel.GraveNo.ToString();
                }
                bindSupportedDocuments();
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    bool flagClaimant = SaveClaimantDetails(); // Save Claimant Details
                    if (flagClaimant == true)
                    {
                        bool flagDeceased = SaveDeceasedDetails(); // Save Funerals Details
                        if (flagDeceased == true)
                        {
                            ClearControl();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                }
            }
        }
        protected void btnSearchMember_Click(object sender, EventArgs e)
        {
            try
            {
                BindMember();
                ClientScript.RegisterStartupScript(GetType(), "hwa", "SearchPopUp('SearchMember');", true);
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }

        }

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
                                int documentId = 0;
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                ClaimDocumentModel ObjSupportedDocumentModel = new ClaimDocumentModel();
                                ObjSupportedDocumentModel.DocContentType = contentType;
                                ObjSupportedDocumentModel.ImageName = filename;
                                ObjSupportedDocumentModel.ImageFile = bytes;
                                ObjSupportedDocumentModel.fkiClaimID = pkiClaimID;
                                ObjSupportedDocumentModel.CreateDate = System.DateTime.Now;
                                ObjSupportedDocumentModel.Parlourid = this.ParlourId;
                                ObjSupportedDocumentModel.LastModified = DateTime.Now;
                                ObjSupportedDocumentModel.ModifiedUser = this.User.Identity.Name;
                                ObjSupportedDocumentModel.DocType = Convert.ToInt32(rdbdocument.SelectedValue.ToString());
                                if (pkiClaimID == 0)
                                { Session["SupportedDocument"] = ObjSupportedDocumentModel; }
                                else
                                    documentId = ClaimsBAL.SaveClaimSupportedDocument(ObjSupportedDocumentModel);
                                if (documentId > 0)
                                {

                                    ShowMessage(ref lblMessage, MessageType.Success, "Supporting document uploaded successfully");

                                }
                                if (documentId > 0)
                                    bindSupportedDocuments();
                            }
                        }
                    }
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab8", "goToTab(8)", true);
                //}
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, "Error in add Document: " + ex.Message);
            }
        }

        protected void cvIdvalidation_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (Page.IsValid)
            {
                bool IDCheck = IdValidationClass.IdValidation(txtClaimantIdNumber.Text);
                args.IsValid = IDCheck;
            }
        }
        protected void cvIdvalidation_ServerValidate2(object source, ServerValidateEventArgs args)
        {
            if (Page.IsValid)
            {
                bool IDCheck = IdValidationClass.IdValidation(txtIdNumber.Text);
                args.IsValid = IDCheck;
            }
        }
        #endregion

        #region GridEvent
        protected void gvClaims_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClaims.PageIndex = e.NewPageIndex;
            BindClaims();
        }


        protected void gvSearchMember_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectMember")
            {
                try
                {
                    txtKeyword.Text = String.Empty;
                    txtKeyword.Text = Convert.ToString(e.CommandArgument);

                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
        }

        protected void gvSupportedDocument_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteClaimsdocumentById")
            {
                ClaimsBAL.DeleteClaimsdocumentById(Convert.ToInt32(e.CommandArgument));
                bindSupportedDocuments();
            }

        }

        #endregion

        #region Private/Public function and methods
        /// <summary>
        /// Get All Members from database and bind here.
        /// </summary>
        public void BindMember()
        {
            DataTable dt = new DataTable();
            SqlCommand com1 = new SqlCommand();
            try
            {
                bool searchchkMember = false;
                bool searchchkWait = false;

                if (chkMember.Checked == true)
                {
                    searchchkMember = true;
                }
                if (chkWait.Checked == true)
                {
                    searchchkWait = true;
                }

                com1.CommandType = CommandType.StoredProcedure;
                com1.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com1.CommandText = "SearchByAllMemberDetails";
                com1.Parameters.Add(new SqlParameter("@ClaimingForMember", searchchkMember));
                com1.Parameters.Add(new SqlParameter("@ApplyWaitingPeriod", searchchkWait));
                com1.Parameters.Add(new SqlParameter("@SearchMemberNo", txtSearchMemberNo.Text));
                com1.Parameters.Add(new SqlParameter("@SearchFullName", txtSearchFullName.Text));
                com1.Parameters.Add(new SqlParameter("@SearchSurname", txtSearchSurname.Text));
                com1.Parameters.Add(new SqlParameter("@SearchIDNumber", txtSearchIDNumber.Text));
                com1.Parameters.Add(new SqlParameter("@SearchDOB", txtSearchDOB.Text));
                string SearchSociety = string.Empty;
                if (ddlSearchSociety.SelectedItem.Text == "Select")
                {
                    SearchSociety = "";
                }
                else
                {
                    SearchSociety = ddlSearchSociety.SelectedItem.Text;
                }
                com1.Parameters.Add(new SqlParameter("@SearchSociety", SearchSociety));

                SqlDataAdapter adp1 = new SqlDataAdapter(com1);

                adp1.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    // ClaimsModel model = client.SelectAllClaimsBySearch(searchchkMember, searchchkWait);
                    //StringBuilder sb = new StringBuilder();
                    gvSearchMember.DataSource = dt;
                    gvSearchMember.DataBind();
                    // MembersViewModel model = client.GetAllMembers(ParlourId, PageSize, PageNum, txtKeyword.Text, SortBy, SortOrder);
                }
                com1.Connection.Close();
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }
            finally
            {
                com1.Dispose();
                dt.Clear();
            }
        }
        public void GetAllSocietye()
        {
            try
            {
                List<SocietyModel> model = ToolsSetingBAL.GetAllSocietyes(ParlourId);
                ddlSearchSociety.DataTextField = "SocietyName";
                ddlSearchSociety.DataValueField = "pkiSocietyID";
                ddlSearchSociety.DataSource = model;
                ddlSearchSociety.DataBind();
                ddlSearchSociety.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }
        }
        private void BindBank()
        {
            ddlBank.DataSource = BanksBAL.SelectAll();
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, new ListItem("Select Bank", "0"));

            ddlAccountType.DataSource = BanksBAL.AccountTypeSelectAll();
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, new ListItem("Select account type", "0"));
        }
        public bool SaveClaimantDetails()
        {
            bool flagClaimant = false;
            if (Page.IsValid)
            {
                try
                {
                    ClaimsModel objClaimsModel = ClaimsBAL.SelectClaimsBypkid(pkiClaimID, ParlourId);
                    // ClaimsModel objClaimsModel = new ClaimsModel();
                    //if (objClaimsModel != null)
                    //{
                    //    ShowMessage(ref lblMessage, MessageType.Danger, "Claimant Details  Already Exists.");
                    //}
                    //else
                    //{
                    objClaimsModel = new ClaimsModel();
                    if (pkiClaimID != 0)
                        objClaimsModel.pkiClaimID = pkiClaimID;//Convert.ToInt32(txtClaimNo.Text);
                    else
                        objClaimsModel.pkiClaimID = 0;
                    objClaimsModel.fkiMemberID = Convert.ToInt32(MemberId);
                    objClaimsModel.MemberNumber = (hdnMemberNo.Value).ToString(); //
                    objClaimsModel.ClaimDate = (Convert.ToDateTime(txtClaimDate.Text));
                    objClaimsModel.ClaimNotes = txtClaimNotes.Text;
                    objClaimsModel.CourseOfDearth = txtCauseOfDeath.Text;
                    objClaimsModel.HostingFuneral = ckbHostingFuneral.Checked;
                    objClaimsModel.ClaimantTitle = ddlClaimantTitle.SelectedValue;
                    objClaimsModel.ClaimantFullname = txtClaimantFirstname.Text;
                    objClaimsModel.ClaimantSurname = txtClaimantLastName.Text;
                    objClaimsModel.ClaimantIDNumber = txtClaimantIdNumber.Text;
                    objClaimsModel.ClaimantDateOfBirth = Convert.ToDateTime(txtClaimantDateOfBirth.Text);
                    objClaimsModel.ClaimantGender = ""; // not in form
                    objClaimsModel.ClaimantAddressLine1 = txtClaimantAddress1.Text;
                    objClaimsModel.ClaimantAddressLine2 = txtClaimantAddress2.Text;
                    objClaimsModel.ClaimantAddressLine3 = txtClaimantAddress3.Text;
                    objClaimsModel.ClaimantAddressLine4 = ""; // not in form
                    objClaimsModel.ClaimantCode = txtClaimantCode.Text;
                    objClaimsModel.ClaimantContactNumber = txtClaimantContactNumber.Text;
                    objClaimsModel.BeneficiaryBank = ddlBank.SelectedValue;
                    objClaimsModel.BeneficiaryAccountHolder = txtAccountholder.Text;
                    objClaimsModel.BeneficiaryBankBranch = txtBranch.Text;
                    objClaimsModel.BeneficiaryAccountNumber = txtAccountno.Text;
                    objClaimsModel.BeneficiaryBranchCode = txtBranchcode.Text;
                    objClaimsModel.BeneficiaryAccountType = ddlAccountType.SelectedValue;
                    objClaimsModel.LoggedBy = UserName;
                    if (txtCoverAmt.Text != string.Empty)
                        objClaimsModel.Cover = Convert.ToDecimal(txtCoverAmt.Text.Replace(Currency.Trim(), string.Empty));
                    objClaimsModel.BodyCollectedFrom = txtCollection.Text;
                    objClaimsModel.ClaimingFor = txtFirstname.Text + " " + txtLastName.Text;// not in form
                    objClaimsModel.parlourid = ParlourId;
                    objClaimsModel.CreatedDate = System.DateTime.Now;
                    objClaimsModel.ModifiedUser = UserName;
                    objClaimsModel.LastModified = System.DateTime.Now;

                    objClaimsModel.Payout = chkAddpayout.Checked;
                    if (txtAddpayout.Text != string.Empty)
                        objClaimsModel.PayoutValue = Convert.ToDecimal(txtAddpayout.Text);
                    else
                        objClaimsModel.PayoutValue = 0;

                    pkiClaimID = ClaimsBAL.SaveClaims(objClaimsModel);

                    //save Supported document
                    if (Session["SupportedDocument"] != null)
                    {
                        ClaimDocumentModel objsavedata = (ClaimDocumentModel)Session["SupportedDocument"];
                        objsavedata.fkiClaimID = pkiClaimID;
                        ClaimsBAL.SaveClaimSupportedDocument(objsavedata);
                        Session["SupportedDocument"] = null;
                    }
                    ShowMessage(ref lblMessage, MessageType.Success, "Claimant Details Successfully Saved.");

                    flagClaimant = true;
                }
                // }
                catch (Exception ex)
                {
                    flagClaimant = false;
                    ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                }
            }
            return flagClaimant;
        }
        public bool SaveDeceasedDetails()
        {
            bool flagDeceased = false;
            if (Page.IsValid)
            {
                try
                {
                    FuneralModel objfunModel = FuneralBAL.SelectFuneralBypkid(FID, ParlourId);
                    if (objfunModel != null && FID == 0)
                    {
                        ShowMessage(ref lblMessage, MessageType.Danger, "Deceased Details  Already Exists.");
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
                        if (txtDOD.Text == null || txtDOD.Text == "")
                            objfunModel.DateOfDeath = null;
                        else
                            objfunModel.DateOfDeath = Convert.ToDateTime(txtDOD.Text);

                        if (txtDateOfFuneral.Text == null || txtDateOfFuneral.Text == "")
                            objfunModel.DateOfFuneral = System.DateTime.Now;
                        else
                            objfunModel.DateOfFuneral = Convert.ToDateTime(txtDateOfFuneral.Text);


                        objfunModel.TimeOfFuneral = Convert.ToDateTime(txtTime.Text);
                        objfunModel.CourseOfDearth = txtCOD.Text;
                        objfunModel.FuneralCemetery = txtTown.Text;
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

                        int a = FuneralBAL.SaveFuneral(objfunModel);

                        string Claimnumber = pkiClaimID.ToString();
                        ClaimsBAL.UpdateMemberNumber(a, hdnMemberNo.Value, Claimnumber);
                        ShowMessage(ref lblMessage, MessageType.Success, "Details Successfully Saved.");

                        flagDeceased = true;
                    }
                }
                catch (Exception ex)
                {
                    flagDeceased = false;
                    ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
                }
            }
            return flagDeceased;
        }
        public void BindDependentUpdate()
        {
            FamilyDependencyModel objmodel = MembersBAL.SelectFamilyDependencyById(DependentId);
            MembersModel obj = ClaimsBAL.selectMemberByPkidAndParlor(ParlourId, objmodel.MemberId);
            MemberId = objmodel.MemberId;
            hdnMemberNo.Value = obj.MemeberNumber.ToString();
            txtClaimDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            txtClaimNo.Text = "";
            txtCauseOfDeath.Text = "";
            txtCoverAmt.Text = objmodel.Cover.ToString("n2");
            txtClaimNotes.Text = "";
            //ClaimantDetails

            ddlClaimantTitle.SelectedValue = obj.Title.ToString();
            txtClaimantFirstname.Text = obj.FullNames.ToString();
            txtClaimantLastName.Text = obj.Surname.ToString();
            txtClaimantIdNumber.Text = obj.IDNumber.ToString();
            txtClaimantDateOfBirth.Text = obj.DateOfBirth.ToString("dd MMM yyyy");
            txtClaimantAddress1.Text = obj.Address1.ToString();
            txtClaimantAddress2.Text = obj.Address2.ToString();
            txtClaimantAddress3.Text = obj.Address3.ToString();
            txtClaimantCode.Text = obj.Code.ToString();
            txtClaimantContactNumber.Text = obj.Cellphone.ToString();
            //BankDetails
            ddlBank.SelectedItem.Text = obj.Bank.ToString();
            txtBranch.Text = obj.Branch.ToString();
            txtBranchcode.Text = obj.BranchCode.ToString();
            txtAccountholder.Text = obj.AccountHolder.ToString();
            txtAccountno.Text = obj.AccountNumber.ToString();
            ddlAccountType.SelectedItem.Text = obj.AccountType.ToString();
            //DeceasedDetails
            txtLastName.Text = objmodel.Surname;
            txtFirstname.Text = objmodel.FullName;
            txtIdNumber.Text = objmodel.IDNumber;
            txtBirthDay.Text = (Convert.ToDateTime(objmodel.DateOfBirth)).ToString("dd MMM yyyy");
            string Gender = (objmodel.Gender).ToString();
            if (Gender == rdbtnMale.Text)
            {
                rdbtnMale.Checked = true;
            }
            else
            {
                rdbtnFemale.Checked = true;
            }
            txtCOD.Text = "";
            txtDOD.Text = "";
            txtCollection.Text = "";
            txtSerialNo.Text = "";
            //txtStreetAddress.Text = objmodel.Address1.ToString();
            //txtTown.Text = objmodel.Address2.ToString();
            //txtProvince.Text = objmodel.Address3.ToString();
            //txtCode.Text = objmodel.Code.ToString();
            txtDateOfFuneral.Text = "";
            txtTime.Text = "";
            txtDriver.Text = "";
            txtCemetery.Text = "";
            txtGraveNo.Text = "";

            btnPaymentHistory.Enabled = true;
        }
        public void BindMainMemUpdate()
        {
            MembersModel objmodel = ClaimsBAL.selectMemberByPkidAndParlor(ParlourId, MemberId);
            PlanModel objpan = ClaimsBAL.GetPlanDetailsByPlanId(objmodel.fkiPlanID);
            hdnMemberNo.Value = objmodel.MemeberNumber.ToString();
            txtClaimDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            txtClaimNo.Text = objmodel.Claimnumber.ToString();
            txtCauseOfDeath.Text = "";
            txtCoverAmt.Text = objpan.Cover.ToString("n2");
            txtClaimNotes.Text = "";
            //ClaimantDetails

            ddlClaimantTitle.SelectedValue = objmodel.Title.ToString();
            txtClaimantFirstname.Text = objmodel.FullNames.ToString();
            txtClaimantLastName.Text = objmodel.Surname.ToString();
            txtClaimantIdNumber.Text = objmodel.IDNumber.ToString();
            txtClaimantDateOfBirth.Text = objmodel.DateOfBirth.ToString("dd MMM yyyy");
            txtClaimantAddress1.Text = objmodel.Address1.ToString();
            txtClaimantAddress2.Text = objmodel.Address2.ToString();
            txtClaimantAddress3.Text = objmodel.Address3.ToString();
            txtClaimantCode.Text = objmodel.Code.ToString();
            txtClaimantContactNumber.Text = objmodel.Cellphone.ToString();
            //BankDetails
            ddlBank.SelectedItem.Text = objmodel.Bank.ToString();
            txtBranch.Text = objmodel.Branch.ToString();
            txtBranchcode.Text = objmodel.BranchCode.ToString();
            txtAccountholder.Text = objmodel.AccountHolder.ToString();
            txtAccountno.Text = objmodel.AccountNumber.ToString();
            ddlAccountType.SelectedItem.Text = objmodel.AccountType.ToString();
            //DeceasedDetails
            txtLastName.Text = objmodel.Surname;
            txtFirstname.Text = objmodel.FullNames;
            txtIdNumber.Text = objmodel.IDNumber;
            txtBirthDay.Text = (Convert.ToDateTime(objmodel.DateOfBirth)).ToString("dd MMM yyyy");
            string Gender = (objmodel.Gender).ToString();
            if (Gender == rdbtnMale.Text)
            {
                rdbtnMale.Checked = true;
            }
            else
            {
                rdbtnFemale.Checked = true;
            }
            txtCOD.Text = "";
            txtDOD.Text = "";
            txtCollection.Text = "";
            txtSerialNo.Text = "";
            txtStreetAddress.Text = objmodel.Address1.ToString();
            txtTown.Text = objmodel.Address2.ToString();
            txtProvince.Text = objmodel.Address3.ToString();
            txtCode.Text = objmodel.Code.ToString();
            txtDateOfFuneral.Text = "";
            txtTime.Text = "";
            txtDriver.Text = "";
            txtCemetery.Text = "";
            txtGraveNo.Text = "";

            btnPaymentHistory.Enabled = true;
        }
        public void BindMembeOrDependenciesData()
        {
            try
            {
                if (txtKeyword.Text != "" || txtKeyword.Text != string.Empty)
                {
                    this.gvMembers.DataSource = null;
                    gvMembers.DataBind();
                    this.gvDependent.DataSource = null;
                    gvDependent.DataBind();
                    if (chkMember.Checked)
                    {
                        List<MembersModel> Mmodel = ClaimsBAL.SelectMembersAndDependencies1(ParlourId, true, txtKeyword.Text);
                        foreach (var search in Mmodel)
                        {
                            ViewState["MemberId"] = search.pkiMemberID;
                        }
                        gvMembers.DataSource = Mmodel;
                        gvMembers.DataBind();
                    }
                    else
                    {
                        List<FamilyDependencyModel> Dmodel = ClaimsBAL.SelectMembersAndDependencies2(ParlourId, false, txtKeyword.Text);
                        gvDependent.DataSource = Dmodel;
                        gvDependent.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }
        }
        public void BindClaims()
        {
            try
            {

                DateTime Datefrom = Convert.ToDateTime("01/01/1753");
                var dateAndTime = DateTime.Now;
                DateTime DateTo = dateAndTime.Date;

                if (txtDateFrom.Text != "" && txtDateTo.Text != "")
                {
                    Datefrom = Convert.ToDateTime(txtDateFrom.Text);
                    DateTo = Convert.ToDateTime(txtDateTo.Text);
                }

                gvClaims.PageSize = PageSize;
                List<ClaimsModel> model = ClaimsBAL.SelectAllClaimsByParlourId(new Guid(ddlCompanyList.SelectedValue), PageSize, PageNum, txtClaimKeyword.Text, sortBYExpression, SortOrder, Datefrom, DateTo, ddlStatusSearch.SelectedValue);

                gvClaims.DataSource = model;
                gvClaims.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab4", "goToTab(4)", true);
            }
            catch (Exception ex)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, ex.Message);
            }
        }
        #endregion

        #region page RowCommand
        protected void gvMembers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectMainMember")
            {
                try
                {
                    MemberId = Convert.ToInt32(e.CommandArgument);
                    BindMainMemUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }
            }
        }
        protected void gvDependent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectDependMember")
            {
                try
                {
                    DependentId = Convert.ToInt32(e.CommandArgument);
                    BindDependentUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }
            }
        }

        protected void gvClaims_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int TempId = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "SelectMember")
                {
                    ClearControl();
                    pkiClaimID = TempId;
                    BindClaimsSearchDetails();
                    btnAddDocs.Enabled = true;
                    btnPrintClaim.Enabled = true;
                    btnPaymentHistory.Enabled = true;
                }
                if (e.CommandName == "ChangeStatus")
                {
                    ClaimsModel objClaimsModel = ClaimsBAL.SelectClaimsBypkid(TempId, ParlourId);

                    string status = objClaimsModel.Status.ToString();
                    if (status == "New" || status == string.Empty || status == "")
                    {
                        ClaimsBAL.UpdateClaimStatus(TempId, "Payment Done");
                    }
                    else if (status == "Payment Done")
                    {
                        ClaimsBAL.UpdateClaimStatus(TempId, "Funeral Done");
                    }
                    else if (status == "Funeral Done")
                    {
                        ClaimsBAL.UpdateClaimStatus(TempId, "New");
                    }
                    BindClaims();
                }
            }
            catch (Exception exc)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                lblMessage.Visible = true;
            }
        }
        protected void btnChangeSubmit_Click(object sender, EventArgs e)
        {
            ChangeStatusReason model = new ChangeStatusReason();
            model.CurrentStatus = hdnOldReason.Value;
            model.ClaimID = Convert.ToInt32(hdnClaimID.Value);
            model.NewStatus = ddlStatus.Text;
            model.ChangedBy = this.UserID;
            model.ParlourID = this.ParlourId;
            model.ChangeReason = txtChangeReason.Text;
            ClaimsBAL.ClaimStatusChangeReason(model);
            ClaimsBAL.UpdateClaimStatus(Convert.ToInt32(hdnClaimID.Value), ddlStatus.Text);
            BindClaims();
            txtChangeReason.Text = string.Empty;
            hdnClaimID.Value = string.Empty;
            hdnOldReason.Value = string.Empty;

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

        protected void gvClaims_Sorting(object sender, GridViewSortEventArgs e)
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
            BindClaims();
        }
        #endregion

        #region "Change status and Reason"
        //[WebMethod]
        //public static string ChangeStatus()
        //{

        //}        
        #endregion

        protected void ddlCompanyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindClaims();
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filename;
            string filenameExtension;
            MemberId = Convert.ToInt32(hdnMemberID.Value);
            try
            {
                LocalReport Rpt = ReportViewerdata.LocalReport;
                Rpt.DataSources.Clear();

                MembersModel objmodel = ClaimsBAL.selectMemberByPkidAndParlor(ParlourId, MemberId);
                if (objmodel != null)
                {
                    ReportViewer rpw = new ReportViewer();
                    rpw.ProcessingMode = ProcessingMode.Remote;
                    IReportServerCredentials irsc = new MyReportServerCredentials();
                    rpw.ServerReport.ReportServerCredentials = irsc;

                    rpw.ProcessingMode = ProcessingMode.Remote;
                    rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
                    rpw.ServerReport.ReportPath = "/Unplugg IT Solution BI Reporting/Claims doc";
                    ReportParameterCollection reportParameters = new ReportParameterCollection();

                    reportParameters.Add(new ReportParameter("PkiClaimID", pkiClaimID.ToString()));
                    reportParameters.Add(new ReportParameter("Parlourid", this.ParlourId.ToString()));
                    rpw.ServerReport.SetParameters(reportParameters);
                    string ExportTypeExtensions = "pdf";



                    byte[] bytes = rpw.ServerReport.Render(ExportTypeExtensions, null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    filename = string.Format("{0}.{1}", "Claims doc", ExportTypeExtensions);

                    MemoryStream s = new MemoryStream(bytes);
                    s.Seek(0, SeekOrigin.Begin);
                    Attachment claimdoc = new Attachment(s, filename);


                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        MailMessage message = new MailMessage(ConfigurationManager.AppSettings["ClaimDocumentSender"].ToString().Trim(), txtEmailAddress.Text.Trim(), "Claims document", "Please find all attached document");


                        List<ClaimDocumentModel> documentModel = ClaimsBAL.SelectClaimDocumentsByClaimId(Convert.ToInt32(hdnClaimID.Value)).ToList();
                        if (documentModel != null)
                        {
                            foreach (var document in documentModel)
                            {
                                message.Attachments.Add(new Attachment(new MemoryStream(document.ImageFile), document.ImageName));
                            }
                        }
                        message.Attachments.Add(claimdoc);

                        smtpClient.Send(message);
                        ShowMessage(ref lblMessage, MessageType.Success, "Email sent successfully");
                    }
                }
                else
                {
                    ShowMessage(ref lblMessage, MessageType.Info, "No member found");
                }
            }
            catch (Exception exc)
            {
                ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
            }
        }
    }
}