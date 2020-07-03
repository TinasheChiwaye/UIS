using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Mvc;

namespace Funeral.Web.Admin
{

    public partial class DebitOrder : AdminBasePage

    //System.Web.UI.Page
    {
        private readonly ISiteSettings _siteConfig = new SiteSettings();

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
        public int PageSize
        {
            get
            {
                if (ViewState["_PageSize"] == null)
                    return 10;
                else { return Convert.ToInt32(ViewState["_PageSize"].ToString()); }

            }
            set { ViewState["_PageSize"] = value; }

        }

        public int PageNum
        {
            get
            {
                if (ViewState["_PageNum"] == null)
                    return 1;
                else { return Convert.ToInt32(ViewState["_PageNum"].ToString()); }
            }
            set { ViewState["_PageNum"] = value; }

        }
        public Int64 TotalRecord
        {
            get
            {
                if (ViewState["_TotalRecord"] == null)
                    return 0;
                else { return Convert.ToInt32(ViewState["_TotalRecord"].ToString()); }
            }
            set { ViewState["_TotalRecord"] = value; }

        }
        public string sortBYExpression
        {
            get
            {
                object viewState = this.ViewState["sortBYExpression"];

                return (viewState == null) ? "pkiTransactionID" : (string)viewState;
            }
            set { this.ViewState["sortBYExpression"] = value; }
        }
        public string sortType
        {
            get
            {
                object viewState = this.ViewState["sortType"];

                return (viewState == null) ? "ASC" : (string)viewState;
            }
            set { this.ViewState["sortType"] = value; }
        }
        public int BatchID
        {
            get
            {
                if (ViewState["_batchId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_batchId"]);
            }
            set
            {
                ViewState["_batchId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //DebitOrderInstruction();
            //BindDate();
            //BindTransactionList();

            if (!IsPostBack)
            {
                BindBatchList();
            }
        }
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            //BindTransactionList();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            //BindTransactionList();
        }
        protected void btn_LoadTransactions(object sender, EventArgs e)
        {
            gvDebitTransactionsList.Visible = true;
            BindTransactionList();
        }

        public void BindTransactionList()
        {
            gvDebitTransactionsList.PageSize = PageSize;
            List<DebitOrderTransactionModel> objTransactionModel = GenerateDebitOrderBAL.SelectTransactionListById(BatchID, ParlourId);
            gvDebitTransactionsList.DataSource = objTransactionModel;
            gvDebitTransactionsList.DataBind();
        }

        public void BindBatchList()
        {
            gvDebitBatches.PageSize = PageSize;
            List<DebitBatchListModel> objBatchModel = GenerateDebitOrderBAL.SelectAllDebitBatchesByParlourId(ParlourId, PageSize, PageNum, txtKeyword.Text, sortBYExpression_Batch, sortType);
            gvDebitBatches.DataSource = objBatchModel;
            gvDebitBatches.DataBind();
        }

        public void PrintQuotation()
        {
            string id = ID.ToString();
            var plaintextBytes = Encoding.UTF8.GetBytes(id);
            var encryptedValue = MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);
            //Response.Redirect("Default2.aspx?name=" + encryptedValue);
            Response.Redirect("~/Admin/PrintForm.aspx?QID=" + encryptedValue);
        }
        public void BindTransactionToUpdate()
        {

        }

        //Transaction List
        protected void gvDebitTransactionsList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                try
                {
                    e.Row.Cells[4].Text = (e.Row.Cells[4] != null) ? ("R " + (Math.Round(Convert.ToDecimal(e.Row.Cells[4].Text), 2)).ToString()) : (string.Empty);
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                }
                catch
                {

                }
            }
        }

        //Batch List
        protected void gvDebitBatches_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                try
                {
                    e.Row.Cells[4].Text = (e.Row.Cells[4] != null) ? ("R " + (Math.Round(Convert.ToDecimal(e.Row.Cells[4].Text), 2)).ToString()) : (string.Empty);
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                }
                catch
                {

                }
            }
        }

        //Transaction List
        protected void gvDebitTransactionsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDebitTransactionsList.PageIndex = e.NewPageIndex;
            //BindTransactionList();
        }

        //Batch List
        protected void gvDebitBatches_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDebitBatches.PageIndex = e.NewPageIndex;
            BindBatchList();
        }


        protected void gvDebitBatches_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "ViewTransactions")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvDebitBatches.Rows[rowindex - 1];
                var value = row.Cells[0];
                BatchID = int.Parse(row.Cells[0].Text);
                gvDebitTransactionsList.Visible = true;
                BindTransactionList();
            }

            if (e.CommandName == "DownloadTransactions")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gridViewRow = gvDebitBatches.Rows[index - 1];
                var input = gridViewRow.Cells[0];
                BatchID = int.Parse(gridViewRow.Cells[0].Text);
                Download_Click();
            }
        }

        private const string ASCENDING = "ASC";
        private const string DESCENDING = "DESC";
        private Label lblMessage;

        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                {
                    ViewState["sortDirection"] = SortDirection.Ascending;
                }

                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }

        private void SortGridView(string sortExpression, string direction)
        {
            sortBYExpression = sortExpression;
            sortType = direction;
        }

        private void SortGridView_Batch(string sortExpression, string direction)
        {
            sortBYExpression_Batch = sortExpression;
            sortType = direction;
        }

        //Transaction
        protected void gvDebitTransactionsList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
            //BindTransactionList();
        }

        //Batch
        protected void gvDebitBatches_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView_Batch(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView_Batch(sortExpression, ASCENDING);
            }
            BindBatchList();
        }

        public string sortBYExpression_Batch
        {
            get
            {
                object viewState = this.ViewState["sortBYExpression"];

                return (viewState == null) ? "pkiDebitBatch" : (string)viewState;
            }
            set { this.ViewState["sortBYExpression"] = value; }
        }

        protected void gvDebitBatches_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        //public ActionResult DownloadReconBillingReportPartial(Guid ParlourId,string ReferenceNumber)
        //{
        //    string ExportTypeExtension = "xls";
        //    string ReportName = "ARL_Scheme_Billing Recon Report";
        //    Warning[] warnings;
        //    string[] streamids;
        //    string mimeType;
        //    string endcoding;
        //    string extension;
        //    string filename;

        //    ReportViewer rpw = new ReportViewer();
        //    rpw.ProcessingMode = ProcessingMode.Remote;
        //    IReportServerCredentials irsc = new MyReportServerCredentials();
        //    rpw.ServerReport.ReportServerCredentials = irsc;

        //    rpw.ProcessingMode = ProcessingMode.Remote;
        //    rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
        //    rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/" + ReportName;
        //    ReportParameterCollection reportParameters = new ReportParameterCollection(); //check
        //    reportParameters.Add(new ReportParameter("DateFrom", DateTime.Now.ToString("yyyy/MM/dd")));
        //    reportParameters.Add(new ReportParameter("DateTo", DateTime.Now.ToString("yyyy/MM/dd")));
        //    reportParameters.Add(new ReportParameter("Parlourid", ParlourId.ToString()));
        //    reportParameters.Add(new ReportParameter("RefNo", ReferenceNumber.ToString())); //fix
        //    rpw.ServerReport.SetParameters(reportParameters);
        //    byte[] bytes = rpw.ServerReport.Render("Excel",null, out mimeType, out endcoding,out extension, out streamids, out warnings);
        //    filename = string.Format("{0}.{1}",ReportName,ExportTypeExtension);


        //    Response.ClearHeaders();
        //    Response.Clear();
        //    Response.AddHeader("Content-Disposition","attachment;file =" + filename);
        //    Response.ContentType = mimeType;
        //    Response.BinaryWrite(bytes);
        //    Response.Flush();
        //    Response.End();            
        //    return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);


        //}


        //protected void Download_Click(object sender, EventArgs e)
        //{
        //    Warning[] warnings;
        //    string[] streamids;
        //    string mimeType;
        //    string encoding;
        //    //string filenameExtension;
        //    string filename;

        //    try
        //    {
        //        ReportViewer rpw = new ReportViewer();
        //        rpw.ProcessingMode = ProcessingMode.Remote;
        //        IReportServerCredentials irsc = new MyReportServerCredentials();
        //        rpw.ServerReport.ReportServerCredentials = irsc;

        //        rpw.ProcessingMode = ProcessingMode.Remote;
        //        rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
        //        rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/RPT_UIS_DebitOrderTransaction";
        //        ReportParameterCollection reportParameters = new ReportParameterCollection();

        //        reportParameters.Add(new ReportParameter("pkiDebitBatch", this.BatchID.ToString()));
        //        reportParameters.Add(new ReportParameter("Parlourid", this.ParlourId.ToString()));
        //        rpw.ServerReport.SetParameters(reportParameters);
        //        string ExportTypeExtensions = "pdf";
        //        byte[] bytes = rpw.ServerReport.Render(ExportTypeExtensions, null, out mimeType, out encoding, out ExportTypeExtensions, out streamids, out warnings);
        //        filename = string.Format("{0}.{1}", "Policy Doc", ExportTypeExtensions);

        //        Response.ClearHeaders();
        //        Response.Clear();
        //        Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
        //        Response.ContentType = mimeType;
        //        Response.BinaryWrite(bytes);
        //        Response.Flush();
        //        Response.End();


        //    }
        //    catch (Exception exc)
        //    {
        //        ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
        //    }
        //}


        protected void Download_Click()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            //string filenameExtension;
            string filename;

            //try
            //{
            //    ReportViewer rpw = new ReportViewer();
            //    rpw.ProcessingMode = ProcessingMode.Remote;
            //    IReportServerCredentials irsc = new MyReportServerCredentials();
            //    rpw.ServerReport.ReportServerCredentials = irsc;

            //    rpw.ProcessingMode = ProcessingMode.Remote;
            //    rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
            //    rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/RPT_UIS_DebitOrderTransaction";
            //    ReportParameterCollection reportParameters = new ReportParameterCollection();

            //    reportParameters.Add(new ReportParameter("pkiDebitBatch", this.BatchID.ToString()));
            //    reportParameters.Add(new ReportParameter("Parlourid", this.ParlourId.ToString()));
            //    rpw.ServerReport.SetParameters(reportParameters);
            //    string ExportTypeExtensions = "pdf";
            //    byte[] bytes = rpw.ServerReport.Render(ExportTypeExtensions, null, out mimeType, out encoding, out ExportTypeExtensions, out streamids, out warnings);
            //    filename = string.Format("{0}.{1}", "RPT_UIS_DebitOrderTransaction", ExportTypeExtensions);

            //    Response.ClearHeaders();
            //    Response.Clear();
            //    Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            //    Response.ContentType = mimeType;
            //    Response.BinaryWrite(bytes);
            //    Response.Flush();
            //    Response.End();


            //}
            //catch (Exception exc)
            //{
            //    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
            //}


            ReportViewer rpw = new ReportViewer();
            rpw.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new MyReportServerCredentials();
            rpw.ServerReport.ReportServerCredentials = irsc;

            rpw.ProcessingMode = ProcessingMode.Remote;
            rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
            rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/RPT_UIS_DebitOrderTransaction";
            ReportParameterCollection reportParameters = new ReportParameterCollection();

            reportParameters.Add(new ReportParameter("pkiDebitBatch", this.BatchID.ToString()));
            reportParameters.Add(new ReportParameter("Parlourid", this.ParlourId.ToString()));
            rpw.ServerReport.SetParameters(reportParameters);
            string ExportTypeExtensions = "pdf";
            byte[] bytes = rpw.ServerReport.Render(ExportTypeExtensions, null, out mimeType, out encoding, out ExportTypeExtensions, out streamids, out warnings);
            filename = string.Format("{0}.{1}", "RPT_UIS_DebitOrderTransaction", ExportTypeExtensions);

            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = mimeType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

    }
}