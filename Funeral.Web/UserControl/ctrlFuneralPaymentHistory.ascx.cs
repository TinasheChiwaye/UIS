using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Funeral.Web.UserControl
{
    public partial class ctrlFuneralPaymentHistory : BaseUserControl
    {

        public Guid ParlourId { get; set; }
        public int FuneralId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // bindInvoices(ParlourId);
            }
        }

        public void bindInvoices(Guid ParlourId)
        {
            List<FuneralPaymentsModel> modelList = MemberPaymentBAL.ReturnFuneralPayments(ParlourId, FuneralId.ToString()).ToList();
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
            Response.Redirect("~/Admin/PrintForm.aspx?FID=" + AdminBasePage.EncodeQueryString(FuneralId.ToString()));
        }

        protected void gvInvoices_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = (e.Row.Cells[1] != null) ? (string.Format("{0} ", Currency) + (Math.Round(Convert.ToDecimal(e.Row.Cells[1].Text), 2)).ToString()) : (string.Empty);
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            }
        }
    }
}