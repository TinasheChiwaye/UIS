using Funeral.BAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class UserRightController : BaseAdminController
    {
        public UserRightController() : base(0) {
            ViewBag.HasAccess = HasAccess;
            this.dbPageId = 0;
        }
        // GET: Admin/UserRight
        public ActionResult Index()
        {
            ProgressStatus model = ToolsSetingBAL.CheckProgressStatus(0, this.ParlourId);
            return View(model);
        }
    }
}