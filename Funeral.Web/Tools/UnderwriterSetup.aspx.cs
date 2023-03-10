using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Tools
{
    public partial class UnderwriterSetup : AdminBasePage
    {
        public int PkiUnderWriterSetupId
        {
            get
            {
                if (ViewState["_PkiUnderWriterSetupId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_PkiUnderWriterSetupId"]);
            }
            set
            {
                ViewState["_PkiUnderWriterSetupId"] = value;
            }
        }

        public string sortBYExpression
        {
            get
            {
                object viewState = this.ViewState["sortBYExpression"];

                return (viewState == null) ? "PkiUnderwriterSetupId" : (string)viewState;
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 43;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUndewriterSetupList();
                BindSchemeList();
                BindSchemeListTable();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UnderwriterSetupModel Setupmodel;
                Setupmodel = UnderwriterSetupBAL.SelectUnderwriterSetupByName(TxtUnderwriterName.Text, ParlourId);
                if (Setupmodel != null && Setupmodel.PkiUnderWriterSetupId == 0)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, "UnderwriterSetup Already Exists.");
                }
                else
                {
                    Setupmodel = new UnderwriterSetupModel();

                    Setupmodel.PkiUnderWriterSetupId = PkiUnderWriterSetupId;
                    Setupmodel.UnderwriterName = TxtUnderwriterName.Text;
                    Setupmodel.ContactPerson = TxtContactPerson.Text;
                    Setupmodel.AddressLine1 = TxtAddressLine1.Text;
                    Setupmodel.AddressLine2 = TxtAddressLine2.Text;
                    Setupmodel.City = TxtCity.Text;
                    Setupmodel.Province = TxtProvince.Text;
                    Setupmodel.PostalCode = TxtPostalCode.Text;
                    Setupmodel.Country = TxtCountry.Text;
                    Setupmodel.ContactNumber = TxtContactNumber.Text;
                    Setupmodel.ContactEmail = TxtContactEmail.Text;
                    Setupmodel.FSPNNumber = txtFSPNNumber.Text;



                    Setupmodel.ModifiedDate = System.DateTime.Now;
                    Setupmodel.Parlourid = Guid.Parse(ddlSchemeList.SelectedValue);
                    //Setupmodel.Parlourid = ParlourId;
                    Setupmodel.ModifiedBy = UserName;
                    Setupmodel.CreateddDate = System.DateTime.Now;
                    Setupmodel.CreatedBy = UserName;


                    int retID = UnderwriterSetupBAL.SaveUnderwriterSetup(Setupmodel);
                    UploadImage(Setupmodel.PkiUnderWriterSetupId);
                    PkiUnderWriterSetupId = retID;
                    if(PkiUnderWriterSetupId != 0)
                    {
                        ShowMessage(ref lblMessage, MessageType.Success, "Record Saved Successfully.");
                    }

                    BindUndewriterSetupList();
                    ClearControl();

                }


            }
            else
            {
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab512", "goToTab(5)", true);
            }
        }
        public void UploadImage(int PkiApplicationID)
        {
            if (fuUnderwriterLogo.HasFile)
            {


                string filename = Path.GetFileName(fuUnderwriterLogo.PostedFile.FileName);
                string contentType = fuUnderwriterLogo.PostedFile.ContentType;
                using (Stream fs = fuUnderwriterLogo.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        System.Drawing.Image Img = System.Drawing.Image.FromStream(fuUnderwriterLogo.PostedFile.InputStream);

                        int H = 100;
                        int W = 100;



                        double ratioX = (double)Img.Height / (double)H;
                        double ratioY = (double)Img.Width / (double)W;
                        double ratio = ratioX < ratioY ? ratioX : ratioY;

                        int newH = Convert.ToInt32(H * ratio);
                        int newW = Convert.ToInt32(W * ratio);


                        UnderwriterSetupModel model = new UnderwriterSetupModel();
                        model.PkiUnderWriterSetupId = PkiUnderWriterSetupId;
                        model.UnderwriterLogo = bytes;
                        model.UnderwriterLogoPath = filename;

                        PkiUnderWriterSetupId = ToolsSetingBAL.UploadUnderwriterLogo(model);

                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        Preview.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(createthumbnail(bytes, newH, newW), 0, (createthumbnail(bytes, newH, newW).Length));
                    }
                }
            }
        }
        public static byte[] createthumbnail(byte[] image, int thumbheight, int thumbwidth)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (System.Drawing.Image thumbnail = System.Drawing.Image.FromStream(new MemoryStream(image)).GetThumbnailImage(thumbheight, thumbwidth, null, new IntPtr()))
                {
                    thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
        protected void btnUploadLogo_Click(object sender, EventArgs e)
        {
            //if (PaymentHistory.Visible == false || PkiUnderWriterSetupId > 0)
            //{
            //    UploadImage(PkiUnderWriterSetupId);

            //}
            if (PkiUnderWriterSetupId > 0)
            {
                UploadImage(PkiUnderWriterSetupId);

            }
            else
            {
                ShowMessage(ref lblMessage, MessageType.Danger, "Please select Company Details after upload image..");
                lblMessage.Visible = true;
            }
            //UploadImage(ApplicationID);
        }
        public void BindUndewriterSetupToUpdate()
        {
            // UnderwriterPremiumModel model = client.EditPlanbyID(UnderwriterPremiumId, ParlourId);
            //UnderwriterSetupModel model = UnderwriterSetupBAL.EditUnderwriterSetupbyID(PkiUnderWriterSetupId, ParlourId);
            UnderwriterSetupModel model = UnderwriterSetupBAL.EditUnderwriterSetupbyID(PkiUnderWriterSetupId);

            if ((model == null) || (model.Parlourid != Guid.Parse(ddlSchemeTableList.SelectedValue)))
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Underwriter.');</script>");
            }
            else
            {
                PkiUnderWriterSetupId = model.PkiUnderWriterSetupId;
                TxtUnderwriterName.Text  = model.UnderwriterName.ToString();
                TxtContactPerson.Text = model.ContactPerson.ToString();
                TxtAddressLine1.Text = model.AddressLine1.ToString();
                TxtAddressLine2.Text = model.AddressLine2.ToString();
                TxtCity.Text = model.City.ToString();

                if (model.UnderwriterLogo != null)
                {
                    string base64String = Convert.ToBase64String(model.UnderwriterLogo, 0, model.UnderwriterLogo.Length);
                    Preview.ImageUrl = "data:image/png;base64," + base64String;
                }
                else
                {
                    Preview.ImageUrl = string.Empty;
                }

                //ddlPlanName.SelectedIndex = ddlPlanName.Items.IndexOf(ddlPlanName.Items.FindByValue(model.PlanID.ToString()));
                //  txtDescription.Text = model.PlanDesc;
                try
                {
                    //ddlUnderwriter.SelectedValue = model.UnderwriterId.ToString();
                }
                catch { }
                TxtProvince.Text = model.Province.ToString();
                TxtPostalCode.Text = model.PostalCode.ToString();

                TxtCountry.Text = model.Country.ToString();
                TxtContactNumber.Text = model.ContactNumber.ToString();
                TxtContactEmail.Text = model.ContactEmail.ToString();
                txtFSPNNumber.Text = model.FSPNNumber.ToString();

                btnSubmite.Text = "Update";
            }
            btnUpload.Enabled = true;
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        private void ClearControl()
        {
            btnSubmite.Text = "Save";
            PkiUnderWriterSetupId = 0;
            TxtUnderwriterName.Text = string.Empty;
            TxtContactPerson.Text = string.Empty;
            TxtAddressLine1.Text = string.Empty;
            TxtAddressLine2.Text = string.Empty;
            TxtCity.Text = string.Empty;
            TxtProvince.Text = string.Empty;
            TxtPostalCode.Text = string.Empty;
            TxtCountry.Text = string.Empty;
            TxtContactNumber.Text = string.Empty;
            TxtContactEmail.Text = string.Empty;
            txtFSPNNumber.Text = string.Empty;
            //ddlSchemeList.SelectedValue = string.Empty;

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindUndewriterSetupList();
        }
        public void BindUndewriterSetupList()
        {
            gvUnderwriterSetup.PageSize = PageSize;
            //List<UnderwriterSetupModel> model = UnderwriterSetupBAL.SelectAllUnderwriterSetupByParlourId(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
            if (ddlSchemeTableList.SelectedValue == string.Empty)
            {
                List<UnderwriterSetupModel> model = UnderwriterSetupBAL.SelectAllUnderwriterSetupByParlourId(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
                gvUnderwriterSetup.DataSource = model;

            }
            else
            {
                List<UnderwriterSetupModel> model = UnderwriterSetupBAL.SelectAllUnderwriterSetupByParlourId(Guid.Parse(ddlSchemeTableList.SelectedValue), PageSize, PageNum, txtKeyword.Text, sortBYExpression, sortType);
                gvUnderwriterSetup.DataSource = model;

            }

            //gvUnderwriterSetup.DataSource = model;
            gvUnderwriterSetup.DataBind();
        }

        protected void gvUnderwriterSetup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditPlan")
            {
                PkiUnderWriterSetupId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    
                    BindUndewriterSetupToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            if (e.CommandName == "deletePlan")
            {
                int PkiUnderWriterSetupId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    int retID = UnderwriterSetupBAL.DeleteUnderwriterSetupByID(PkiUnderWriterSetupId, UserName);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    
                    BindUndewriterSetupList();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
        }

       

       
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
       
          protected void gvUnderwriterSetup_Sorting(object sender, GridViewSortEventArgs e)
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
            // BindPlanList();
            BindUndewriterSetupList();
        }

        #endregion
      
        protected void gvUnderwriterSetup_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row?.RowType != DataControlRowType.DataRow)
                return;

            //e.Row.Cells[2].Text = ($@"{Currency} " + (Math.Round(Convert.ToDecimal(e.Row.Cells[2].Text), 2)));
            //e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;


        }
        protected void gvUnderwriterSetup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUnderwriterSetup.PageIndex = e.NewPageIndex;
            // BindPlanList();
        }

        public void BindSchemeList()
        {
            List<ApplicationSettingsModel> model = new List<ApplicationSettingsModel>();

            if (this.IsAdministrator)
            {
                model = ToolsSetingBAL.GetAllApplicationList(ParlourId, 1, 0).ToList();

                if (model == null)
                {
                    model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
                }
            }
            else
            {
                model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
            }



            foreach (ApplicationSettingsModel society in model)
            {
                ListItem li = new ListItem();
                li.Text = society.ApplicationName;
                li.Value = society.parlourid.ToString();
                ddlSchemeList.Items.Add(li);
                //DDe_society.Items.Add(li);
            }


            //ViewBag.Companies = model;
        }
        public void BindSchemeListTable()
        {
            List<ApplicationSettingsModel> model = new List<ApplicationSettingsModel>();

            if (this.IsAdministrator)
            {
                model = ToolsSetingBAL.GetAllApplicationList(ParlourId, 1, 0).ToList();

                if (model == null)
                {
                    model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
                }
            }
            else
            {
                model.Add(new ApplicationSettingsModel() { ApplicationName = ApplicationName, parlourid = ParlourId });
            }



            foreach (ApplicationSettingsModel society in model)
            {
                ListItem li = new ListItem();
                li.Text = society.ApplicationName;
                li.Value = society.parlourid.ToString();
                ddlSchemeTableList.Items.Add(li);
                //DDe_society.Items.Add(li);
            }


            //ViewBag.Companies = model;
        }


    }
}