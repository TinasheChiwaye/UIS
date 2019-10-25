using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class Activation : System.Web.UI.Page
    {
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();
        protected void Page_Load(object sender, EventArgs e)
      {
            

               
                string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : Guid.Empty.ToString();
                int secureid = int.Parse(Request.QueryString["Id"]);
           

                if (code != null)
                {
                Guid ParlourId = new Guid(code);
                SecureUsersModel model = client.EditUserbyID(secureid, ParlourId);
                if ((model == null) || (model.parlourid != ParlourId || model.PkiUserID != secureid))
                {
                    Response.Write("<script>alert('Sorry!you are not authorized to perform edit on this User.');</script>");
                }
                else if(model.Active == false)
                {

                    /* changes for custom field implemented on 10th April 2017  completed*/
                    model.Active = true;
                    int UserId = client.SaveUserDetails(model);
                  ltMessage.Text = "Registration Account Active Successfully";

                }
                else
                {
                    ltMessage.Text = "";
                }
                
                }

           

        }

        
    }
}