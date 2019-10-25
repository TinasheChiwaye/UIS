using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Tools.Controllers
{
    public class UnderwriterController : Controller
    {
        // GET: Tools/Underwriter
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}