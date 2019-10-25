using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.UserControl
{
    public partial class ctrlTombstonePaymentHistory : System.Web.UI.UserControl
    {
        private readonly FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
        public Guid ParlourId { get; set; }
        public int TombstoneId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // bindInvoices(ParlourId);
            }
        }

        public void bindInvoices(Guid ParlourId)
        {
            List<TombStonesPaymentModel> modelList = client.TombStonesPaymentSelectByTombstoneId(ParlourId, TombstoneId).ToList();
            gvInvoices.DataSource = modelList;
            gvInvoices.DataBind();
        }

        protected void gvInvoices_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInvoices.PageIndex = e.NewPageIndex;
            bindInvoices(ParlourId);
        }

        protected void gvInvoices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int InvoiceID = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "PrintPremium")
            {

                PrintInvoice();

            }
            if (e.CommandName == "PrintFullPremium")
            {

                PrintInvoice();
            }

        }
        public void PrintInvoice()
        {
            Response.Redirect("~/Admin/PrintForm.aspx?TBID=" + AdminBasePage.EncodeQueryString(TombstoneId.ToString()));
        }
    }
}