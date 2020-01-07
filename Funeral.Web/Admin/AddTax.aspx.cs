using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Funeral.Model;
using System.Web.Services;
using System.Data;
using Funeral.Web.App_Start;
using Funeral.BAL;

namespace Funeral.Web.Admin
{
    public partial class AddTax : AdminBasePage
    {
        int TaxId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTaxSetting();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            TaxSetting objtax = new TaxSetting();
            objtax.TaxText = txtTax.Text;
            objtax.TaxValue = Convert.ToDecimal(txtTaxValue.Text);
            if (btnSave.Text == "Save")
            {
                var id = TaxSettingBAL.InsertRecordForTax(objtax);
                if (id > 0)
                {
                    lblMessage.Text = "Record Saved SuccessFully";
                    BindTaxSetting();
                }
                else
                {
                    lblMessage.Text = "Record Not Save";
                }
            }
            else if (btnSave.Text == "Update")
            {
                var id = ViewState["Id"];
                TaxSetting model = new TaxSetting();
                model.Id = Convert.ToInt32(id);
                model.TaxText = txtTax.Text;
                model.TaxValue = Convert.ToDecimal(txtTaxValue.Text);
                var result = TaxSettingBAL.UpdateRecordForTax(model);
                BindTaxSetting();
                btnSave.Text = "Save";
            }
        }
        public void BindTaxSetting()
        {
            List<TaxSetting> taxSettings = TaxSettingBAL.GetAllTaxSettings();
            gvTaxSetting.DataSource = taxSettings;
            gvTaxSetting.DataBind();
        }

        //public void UpdateTaxSetting()
        //{

        //}

        protected void gvTaxSetting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "updateTxt")
            {
                TaxSettingBAL.GetTaxSettingById(Convert.ToInt32(e.CommandArgument));
                var id = Convert.ToInt32(e.CommandArgument);
                var taxSetting = TaxSettingBAL.GetTaxSettingById(id);
                ViewState["Id"] = taxSetting.Id;
                txtTax.Text = taxSetting.TaxText;
                txtTaxValue.Text = taxSetting.TaxValue.ToString();
                btnSave.Text = "Update";
            }
            else if (e.CommandName == "deleteTxt")
            {
                var id = Convert.ToInt32(e.CommandArgument);
                var Result = TaxSettingBAL.DeleteTaxSettingById(id);

                if (Result == 0)
                {
                    lblMessage.Text = "Record Deleted SuccessFully";
                }
                else
                {
                    lblMessage.Text = "Record Not Deleted";
                }
                BindTaxSetting();
            }
        }

        //protected void btnupdate_Click(object sender, EventArgs e)
        //{
        //    UpdateTaxSetting();
        //}
    }
}