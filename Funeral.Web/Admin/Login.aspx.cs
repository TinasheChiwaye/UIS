using Funeral.Model;
using Funeral.Web.FuneralServiceReference;
using System;
using System.ServiceModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;


namespace Funeral.Web.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        //   FuneralServiceReference.FuneralServicesClient serviceClient = new FuneralServiceReference.FuneralServicesClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
            }
        }

        protected void Login_click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    AdminModel model = BAL.AdminBAL.AdminLogin(username.Text, password.Text);
                    if (model != null)
                    {

                        string UserName = username.Text;
                        Session["UserName"] = UserName;
                        string cookiestr;
                        FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, UserName, DateTime.Now,
                            DateTime.Now.AddMinutes(30), false, model.parlourid.ToString() + "|" + model.ApplicationName + "|" + model.Name + "|" + model.PkiUserID + "|" + model.Currency);
                        cookiestr = FormsAuthentication.Encrypt(tkt);
                        HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                        ck.Expires = tkt.Expiration;
                        ck.Path = FormsAuthentication.FormsCookiePath;
                        FormsAuthentication.SetAuthCookie(UserName, false);
                        Response.Cookies.Add(ck);
                        try
                        {
                            //Session["SessionVariablesClass"] = serviceClient.tblRightGetAll();
                            Session["Loginparlourid"] = model.parlourid;
                            Session["SessionVariablesClass"] = BAL.RightsBAL.LoadSideMenu(model.parlourid, model.PkiUserID);
                        }
                        catch { }
                        Response.Redirect("~/Admin/Dashboard/Index");
                        //Response.Redirect("Dashboard.aspx", false);

                    }
                    else
                    {
                        //  ErrorMessage.InnerHtml = "<div id=\"ErrMsg\" class=\"message error closeable\" ><span class=\"message-close\"></span><h3>Error!<p> Invalid user name of password</p></h3> </div>";
                        lblMessage.Text = "<div class='ibox-content'><div class='alert alert-Danger'>Invalid user name or password</div></div>";
                    }
                }
                catch (FaultException<FuneralServiceFault> fault)
                {
                    lblMessage.Text = "<div class='ibox-content'><div class='alert alert-Danger'>" + fault.Detail.Message + "</div></div>";

                }
                catch (Exception ex)
                {
                    lblMessage.Text = "<div class='ibox-content'><div class='alert alert-Danger'>" + ex.Message + "</div></div>";
                    // ErrorMessage.InnerHtml = "<div id=\"ErrMsg\" class=\"message error closeable\" ><span class=\"message-close\"></span><h3>Error!<p>" + ex.Message + "</p></h3> </div>";
                }
            }
        }
    }
}