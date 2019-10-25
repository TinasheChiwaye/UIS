using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Neodynamic.SDK.Web;

namespace Funeral.Web.Admin
{
    public partial class PrinterConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           Neodynamic.SDK.Web.WebClientPrint.GetWcppDetectionMetaTag();
           
        }
    }
}