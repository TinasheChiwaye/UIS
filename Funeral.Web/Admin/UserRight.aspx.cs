using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class UserRight : AdminBasePage
    {
        public int ID
        {
            get
            {
                if (ViewState["_ID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_ID"]);
            }
            set
            {
                ViewState["_ID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProgressStatus model = ToolsSetingBAL.CheckProgressStatus(ID,this.ParlourId);
               

                if (model != null)
                {
                    if (model.CompanyStatus == true)
                    { imgCompany1.Visible = true; }
                    else { imgCompany2.Visible = true; }

                    if (model.BranchStatus == true)
                    { imgBranch1.Visible = true; }
                    else { imgBranch2.Visible = true; }

                    if (model.UserStatus == true)
                    { imgUsers1.Visible = true; }
                    else { imgUsers2.Visible = true; }

                    if (model.PlanStatus == true)
                    { imgPlans1.Visible = true; }
                    else { imgPlans2.Visible = true; }

                    if (model.UnderWriterStatus == true)
                    { imgUnderwriterPlanSetup1.Visible = true; }
                    else { imgUnderwriterPlanSetup2.Visible = true; }

                    if (model.SocietyStatus == true)
                    { imgSocieties1.Visible = true; }
                    else { imgSocieties2.Visible = true; }

                    if (model.FuneralServiceStatus == true)
                    { imgServices1.Visible = true; }
                    else { imgServices2.Visible = true; }

                    if (model.AddonProductStatus == true)
                    { imgProduct1.Visible = true; }
                    else { imgProduct2.Visible = true; }

                    if (model.DependentStatus == true)
                    { imgDependents1.Visible = true; }
                    else { imgDependents2.Visible = true; }

                    if(model.MemberStatus == true)
                    { imgLifes1.Visible = true;}
                    else { imgLifes2.Visible = true; }
                }
            }
        }
    }
}