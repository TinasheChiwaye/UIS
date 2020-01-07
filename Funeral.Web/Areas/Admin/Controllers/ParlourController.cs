using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class ParlourController : BaseAdminController
    {
        // GET: Admin/Parlour
        [HttpPost]
        public void ChangeParlour(Guid parlourId)
        {
            CurrentParlourId = parlourId;
        }
    }
}