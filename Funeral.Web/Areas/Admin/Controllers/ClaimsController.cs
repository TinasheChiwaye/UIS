using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class ClaimsController : Controller
    {
        // GET: Admin/Claims
        public ActionResult Index()
        {
            return View();
        }
    }
}