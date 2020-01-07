using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Tools
{
    public partial class Rights : AdminBasePage
    {
        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 29;
        }
        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bntSubmintData.Enabled = false;
                LoadDropdownGroupData();
            }
        }
        public void LoadDropdownGroupData()
        {
            List<SecureGroupModel> data = ToolsSetingBAL.GetSecureGrouList().Where(sg => sg.pkiSecureGroupID != 12 && sg.pkiSecureGroupID != 4).ToList();
            ddlGroupId.DataSource = data;
            ddlGroupId.DataTextField = "sSecureGroupName";
            ddlGroupId.DataValueField = "pkiSecureGroupID";
            
            ddlGroupId.DataBind();
            ddlGroupId.Items.Insert(0, new ListItem("Select", ""));
        
        }
        public void ResetAll()
        {
            gvRight.DataSource = null;
            gvRight.DataBind();
            LoadDropdownGroupData();
            bntSubmintData.Enabled = false;
        }
       
        protected void itemSelected(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlGroupId.SelectedItem.Value))
            {
                List<NewRightsModel> model = RightsBAL.GetRightsByGroupId(ParlourId, Convert.ToInt32(ddlGroupId.SelectedItem.Value)).OrderBy(x=>x.PageName).ToList();
                //List<tblPageModel> model = client.GetAllActiveTblPages().ToList();
                if (!model.Any()) return;
                bntSubmintData.Enabled = true;
                gvRight.DataSource = model;
                gvRight.DataBind();
            }
            else 
            {
                ResetAll();
            }
        }

        protected void bntSubmintData_click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvRight.Rows)
            {
                try
                {
                    NewRightsModel rightsModel = new NewRightsModel();
                    rightsModel.ID = Convert.ToInt32((row.FindControl("hdfRightId") as HiddenField).Value);
                    rightsModel.PageId = Convert.ToInt32((row.FindControl("hdnPageId") as HiddenField).Value);
                    rightsModel.GroupId = Convert.ToInt32(ddlGroupId.SelectedItem.Value);
                    rightsModel.HasAccess = Convert.ToBoolean((row.FindControl("chkhasRights") as CheckBox).Checked);
                    rightsModel.IsRead = Convert.ToBoolean((row.FindControl("chkIsRead") as CheckBox).Checked);
                    rightsModel.IsWrite = Convert.ToBoolean((row.FindControl("chkIsWrite") as CheckBox).Checked);
                    rightsModel.IsDelete = Convert.ToBoolean((row.FindControl("chkIsDelete") as CheckBox).Checked);
                    rightsModel.IsUpdate = Convert.ToBoolean((row.FindControl("chkIsUpdate") as CheckBox).Checked);
                    rightsModel.IsReversalPayment = Convert.ToBoolean((row.FindControl("chkIsPaymentReversal") as CheckBox).Checked);
                    
                    rightsModel.ParlourId = ParlourId;
                    RightsBAL.SaveTblRights(rightsModel);
                }
                catch { }
            }
            ResetAll();
            LoadDropdownGroupData();
        }
    }
}