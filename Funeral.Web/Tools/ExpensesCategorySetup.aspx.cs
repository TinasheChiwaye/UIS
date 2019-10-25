using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Funeral.Model;
using Funeral.Web.App_Start;
using System.Text;
using System.Web.Services;
using Funeral.BAL;
using System.Data.SqlClient;
using System.IO;

namespace Funeral.Web.Tools
{
    public partial class ExpensesCategorySetup : AdminBasePage
    {
        #region Page Property
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
        public int ExpensesID
        {
            get
            {
                if (ViewState["_ExpensesID"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_ExpensesID"]);
            }
            set
            {
                ViewState["_ExpensesID"] = value;
            }
        }

        #endregion

        #region Page PreInit
        protected void Page_PreInit(object sender, EventArgs e)
        {
            this.dbPageId = 18;
        }
        #endregion

        //#region PageInit
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    SecureUserGroupsModel[] obj = client.EditSecurUserbyID(UserID);
        //    List<int> list = new List<int>();
        //    list.Add(4);
        //    list.Add(12);
        //    var result = obj.Where(x => list.Contains(x.fkiSecureGroupID));
        //    if (result.FirstOrDefault() == null)
        //    {
        //        Response.Redirect("~/Admin/403Error.aspx", false);
        //    }
        //}
        //#endregion

        #region Page load event
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            if (!Page.IsPostBack)
            {
                BindExpensesList();
                btnSubmite.Enabled = HasCreateRight;
            }
        }

        #endregion

        #region Private/Public function and methods

        public void BindExpensesList()
        {
            ExpensesModel[] model = client.GetAllExpenseses(ParlourId);
            gvExpenses.DataSource = model;
            gvExpenses.DataBind();
        }
        public void BindExpensesToUpdate()
        {
            ExpensesModel model = client.EditExpensesbyID(ExpensesID, ParlourId);
            if (model == null)
            {
                Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this Branch.');</script>");
            }
            else
            {
                ExpensesID = model.pkiExpenseCategoryID;
                txtExpensesName.Text = model.Category;

                btnSubmite.Text = "Update";
            }
        }
        public void ClearControl()
        {
            btnSubmite.Text = "Save";
            ExpensesID = 0;
            txtExpensesName.Text = string.Empty;
            lblMessage.Visible = false;
        }
        #endregion

        #region Button Click Events
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        protected void btnSubmite_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                ExpensesModel model;
                model = client.GetExpensesByID(txtExpensesName.Text, ParlourId);
                if (model != null && ExpensesID == 0)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, "Expenses Category Already Exists.");
                }
                else
                {
                    model = new ExpensesModel();
                    model.pkiExpenseCategoryID = ExpensesID;
                    model.Category = txtExpensesName.Text;
                    model.parlourid = ParlourId;
                    model.LastModified = System.DateTime.Now;
                    model.ModifiedUser = UserName;


                    //================================================================ 
                    int retID = client.SaveExpensesDetails(model);
                    ExpensesID = retID;

                    ShowMessage(ref lblMessage, MessageType.Success, "Expenses Category Details successfully saved");
                    BindExpensesList();
                    ClearControl();
                }
                lblMessage.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "goToTab512", "goToTab(5)", true);
            }
        }
        protected void gvExpenses_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditExpenses")
            {
                ExpensesID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    BindExpensesToUpdate();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
            if (e.CommandName == "deleteExpenses")
            {
                int SBranchId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    int retID = client.DeleteExpenses(SBranchId);
                    ShowMessage(ref lblMessage, MessageType.Success, "Record deleted successfully.");
                    lblMessage.Visible = true;
                    BindExpensesList();
                }
                catch (Exception exc)
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, exc.Message);
                    lblMessage.Visible = true;
                }

            }
        }

        #endregion
    }
}