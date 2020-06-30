using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Client.Controllers
{
    public class LoginController : Controller
    {
        // GET: Client/Login
        public ActionResult Index()
        {
            login loginmodel = new login();
            return View(loginmodel);
        }
        [HttpPost]
        public ActionResult ClientLogin(login l)
        {
            TempData["loginMessage"] = "";
            return RedirectToAction("Login");
        }
    }
}