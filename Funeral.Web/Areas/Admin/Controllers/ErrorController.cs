using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error403()
        {
            return View();
        }
    }
}