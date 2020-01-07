using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Tools
{
    public partial class TombstonePackage : AdminBasePage
    {
        #region Fields
        private int PackageId
        {
            get
            {
                if (ViewState["PackageId"] != null)
                {
                    return Convert.ToInt32(ViewState["PackageId"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["PackageId"] = value;
            }
        }
       
        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 27;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetPackageList();
                BindFuneralServiceList();
                btnAddService.Visible = false;
                btnSavePackage.Enabled = HasCreateRight;
            }
        }

        private void BindFuneralServiceList()
        {
            List<QuotationServicesModel> objQuoSer = QuotationBAL.GetAllQuotationServices(ParlourId);
            ddlServices.DataTextField = "ServiceName";
            ddlServices.DataValueField = "pkiServiceID";
            ddlServices.DataSource = objQuoSer;
            ddlServices.DataBind();
            ddlServices.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void GetPackageList()
        {
            gvPackageList.DataSource = TombstonePackageBAL.SelectAllPackage(this.ParlourId);
            gvPackageList.DataBind();
        }

        protected void gvPackageList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditService")
            {
                string[] splittedValue = e.CommandArgument.ToString().Split('~');
                txtPackageName.Text = splittedValue[0];
                this.PackageId = Convert.ToInt32(splittedValue[1]);
                BindSelectedService();
                btnAddService.Visible = true;
            }
            else
            {
                TombstonePackageBAL.DeleteTombstonePackage(Convert.ToInt32(e.CommandArgument));
                GetPackageList();
            }
        }



        private void BindSelectedService()
        {
            gvPackageService.DataSource = TombstonePackageBAL.SelectPackageServiceByPackgeId(this.ParlourId, this.PackageId);
            gvPackageService.DataBind();
        }

        protected void gvPackageService_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteService")
            {
                TombstonePackageBAL.DeletePackageService(Convert.ToInt32(e.CommandArgument));
                BindSelectedService();
            }
        }

        protected void gvPackageList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPackageList.PageIndex = e.NewPageIndex;
            GetPackageList();
        }

        protected void btnSavePackage_Click(object sender, EventArgs e)
        {
            TombstonePackageModel model = new TombstonePackageModel();
            model.PackageName = txtPackageName.Text.Trim();
            model.ModifiedUser = this.UserName;
            model.ParlourId = this.ParlourId;
            model.IsActive = true;
            this.PackageId = TombstonePackageBAL.SavePackage(model);
            GetPackageList();
            btnAddService.Visible = true;
            btnAddService_Click(null, null);
        }

        protected void btnAddService_Click(object sender, EventArgs e)
        {
            TombstonePackageModel model = new TombstonePackageModel();
            model.PackageName = txtPackageName.Text.Trim();
            model.ModifiedUser = this.UserName;
            model.ParlourId = this.ParlourId;
            model.fkiPackageID = this.PackageId;
            model.fkiServiceID = Convert.ToInt32(ddlServices.SelectedValue);
            TombstonePackageBAL.SavePackageService(model);
            BindSelectedService();
        }
    }
}