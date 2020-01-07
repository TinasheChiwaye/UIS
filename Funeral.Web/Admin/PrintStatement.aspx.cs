using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.UserControl;
using System;
using System.Collections.Generic;

namespace Funeral.Web.Admin
{
    public partial class PrintStatement : System.Web.UI.Page
    {
        //   List<MemberInvoiceModel> obj = new List<MemberInvoiceModel>();
        #region Fields
        #endregion
        #region PageProperty

        public int MemberId
        {
            get
            {
                if (ViewState["_memberId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(ViewState["_memberId"]);
            }
            set
            {

                ViewState["_memberId"] = value;
            }
        }


        public string PolicyNum
        {
            get
            {
                if (ViewState["_policyNo"] == null)
                    return null;
                else
                    return (ViewState["_policyNo"]).ToString();
            }
            set
            {
                ViewState["_policyNo"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["InID"] != null)
            {
                string query = (Request.QueryString["InID"]).ToString();
                string decryptedValue = EncryptionHelper.Decrypt(query);
                string[] arry = decryptedValue.ToString().Split('&');
                Guid ParlourId = new Guid(arry[0]);
                PolicyNum = arry[1].ToString();
                MemberId = Convert.ToInt32(arry[2]);
                lblPolicy.Text = PolicyNum;
                BindData(ParlourId, MemberId);

            }
        }

        public void BindData(Guid ParlourId, int MemberId)
        {
            List<MemberInvoiceModel> objMemberInvoiceModel = MembersBAL.GetInvoicesByMemberID(ParlourId, MemberId);
            gvInvoices.DataSource = objMemberInvoiceModel;
            gvInvoices.DataBind();

        }
    }
}