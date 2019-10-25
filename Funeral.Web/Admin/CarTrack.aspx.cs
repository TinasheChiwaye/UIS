using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class WebForm5 : AdminBasePage
    {
        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 1;
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}