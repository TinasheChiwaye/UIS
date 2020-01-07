using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.FuneralServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Funeral.Web.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        FuneralServicesClient client = new FuneralServicesClient();
        public bool ToolAuth { get; set; }
        AdminBasePage ABP = new AdminBasePage();
        public Admin()
        {

        }
        #region PageInit
        protected void Page_Init(object sender, EventArgs e)
        {
            List<SecureUserGroupsModel> obj = ToolsSetingBAL.EditSecurUserbyID(ABP.UserID);
            List<int> list = new List<int>();
            list.Add(4);
            list.Add(12);
            var result = obj.Where(x => list.Contains(x.fkiSecureGroupID));
            if (result.FirstOrDefault() != null)
            {
                ToolAuth = true;
            }
            else { ToolAuth = false; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ltrCompany.Text = ABP.ApplicationName;
                BindDashbordDetails();
            }

        }
        public void BindDashbordDetails()
        {
            ApplicationSettingsModel modelCompany = ToolsSetingBAL.GetApplictionByParlourID(ABP.ParlourId);
            if (modelCompany != null)
            {
                if (modelCompany.ApplicationLogo != null)
                {
                    string base64String = Convert.ToBase64String(modelCompany.ApplicationLogo, 0, modelCompany.ApplicationLogo.Length);
                    Image1.ImageUrl = "data:image/png;base64," + base64String;
                }
                else
                {
                    Image1.ImageUrl = string.Empty;
                }
            }

        }
    }
}