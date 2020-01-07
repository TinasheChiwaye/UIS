using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class WebForm2 : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindBranches();

        }
        public void BindBranches()
        {
            List<BranchModel> objBranchModel = CommonBAL.BranchByparlourId(ParlourId);
            foreach (BranchModel branch in objBranchModel)
            {
                ListItem li = new ListItem();
                li.Text = branch.BranchName;
                li.Value = branch.BranchName;//branch.Brancheid.ToString();
                ddlBankBranch.Items.Add(li);
                ddlBankBranch.Items.Add(li);
            }
        }
    }
}