using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;

namespace Funeral.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public RedirectResult LogOut() {
            FormsAuthentication.SignOut();
            var url = string.Format("{0}://{1}{2}/{3}", this.HttpContext.Request.Url.Scheme, this.HttpContext.Request.Url.Authority, this.HttpContext.Request.ApplicationPath, "/admin/login.aspx");
            return Redirect(url);
        }
    }
}