using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Funeral.Model;
using Funeral.BAL;

namespace Funeral.Web.Tools
{
    public partial class ManageMenu : AdminBasePage
    {
        #region Fields
        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 10;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMenuTable();
            }
        }

        public void LoadMenuTable()
        {
            StringBuilder strtab = new StringBuilder();
            List<RightsModel> data = RightsBAL.tblRightGetAll();
            int sno = 1;
            foreach (var item in data)
            {
                
                strtab.AppendLine("<tr class='gradeA'>");
                strtab.AppendLine("<td>" + sno + "</td>");
                strtab.AppendLine("<td>" + item.FormName + "</td>");
                strtab.AppendLine("<td>" + item.Formkey + "</td>");
                strtab.AppendLine("<td>" + item.MenuName + "</td>");
                strtab.AppendLine("<td>" + item.MenuURL + "</td>");
                strtab.AppendLine("<td>" + item.Description + "</td>");
                strtab.AppendLine("<td><input type='checkbox' disabled=disabled checked='" + item.InMenu + "'/></td>");
                strtab.AppendLine("<td><a href='/Tools/AddEditMenu.aspx?menuid=" + item.ID + "'><i class='fa fa-edit'></i></a></td>");
                strtab.AppendLine("</tr>");
                sno++;
            }
            ibody.InnerHtml = strtab.ToString();
        }
    }
}