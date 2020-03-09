using System;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Controllers
{
    public class ParlourController : BaseAdminController
    {
        // GET: Admin/Parlour
        [HttpPost]
        public void ChangeParlour(Guid parlourId)
        {
            if (!parlourId.Equals(Guid.Empty))
            {
                CurrentParlourId = parlourId;
            }
        }
    }
}