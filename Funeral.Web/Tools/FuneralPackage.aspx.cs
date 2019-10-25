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
    public partial class FuneralPackage : AdminBasePage
    {
        #region Fields
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
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
            this.dbPageId = 26;
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

        #region Mine
        private void BindFuneralServiceList()
        {
            QuotationServicesModel[] objQuoSer = client.GetAllQuotationServices(ParlourId);
            ddlServices.DataTextField = "ServiceName";
            ddlServices.DataValueField = "pkiServiceID";
            ddlServices.DataSource = objQuoSer;
            ddlServices.DataBind();
            ddlServices.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void GetPackageList()
        {
            gvPackageList.DataSource = client.GetAllPackage(this.ParlourId);
            gvPackageList.DataBind();
        }

        protected void gvPackageList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string commandName = e.CommandName;
            string[] splittedValue = e.CommandArgument.ToString().Split('~');
            switch (commandName)
            {
                case "EditService":
                    
                    txtPackageName.Text = splittedValue[0];
                    this.PackageId = Convert.ToInt32(splittedValue[1]);
                    BindSelectedService();
                    btnAddService.Visible = true;
                    break;
                case "DeletePackage":

                    this.client.DeletePackage(Convert.ToInt32(e.CommandArgument), this.ParlourId);
                    GetPackageList();
                    break;
                default:
                    break;
            }

        }

        private void BindSelectedService()
        {
            gvPackageService.DataSource = client.GetPackageServiceByPackgeId(this.ParlourId, this.PackageId);
            gvPackageService.DataBind();
        }

        protected void gvPackageService_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteService")
            {
                client.DeletePackageService(Convert.ToInt32(e.CommandArgument));
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
            PackageServiceModel model = new PackageServiceModel();
            model.PackageName = txtPackageName.Text.Trim();
            model.ModifiedUser = this.UserName;
            model.ParlourId = this.ParlourId;
            int currentPackageId = client.SavePackage(model);
            if (currentPackageId == 0)
            {
                this.ShowMessage(ref lblMessage, MessageType.Warning, string.Format("{0} Package already exist"));
            }
            else
            {
                this.PackageId = currentPackageId;
                GetPackageList();
                btnAddService.Visible = true;
                btnAddService_Click(null, null);
            }
        }

        protected void btnAddService_Click(object sender, EventArgs e)
        {
            PackageServiceModel model = new PackageServiceModel();
            model.PackageName = txtPackageName.Text.Trim();
            model.ModifiedUser = this.UserName;
            model.ParlourId = this.ParlourId;
            model.fkiPackageID = this.PackageId;
            model.fkiServiceID = Convert.ToInt32(ddlServices.SelectedValue);
            client.SavePackageService(model);
            BindSelectedService();
        }
        #endregion
    }
}