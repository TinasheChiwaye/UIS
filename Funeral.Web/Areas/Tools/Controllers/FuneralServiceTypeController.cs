using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Funeral.Web.Common;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Funeral.Web.Areas.Tools.Controllers
{
    public class FuneralServiceTypeController : Controller
    {
        // GET: Tools/FuneralServiceType
        public ActionResult Index()
        {
            return View();
        }


        public PartialViewResult FuneralServiceTypeList()
        {
            return PartialView("~/Areas/Tools/Views/FuneralServiceType/_FuneralTypeList.cshtml", FuneralServiceTypeBAL.SelectAll());
        }
        public JsonResult GetData()
        {
            return Json(FuneralServiceTypeBAL.SelectAll());
        }
        public PartialViewResult Add(FuneralServiceTypeVm model)
        {
            ModelState.Clear();
            return PartialView("~/Areas/Tools/Views/FuneralServiceType/_AddUpdate.cshtml", model);
        }

        public PartialViewResult GetRecord(int id)
        {
            var updateRercord = FuneralServiceTypeBAL.Select(id);
            return PartialView("~/Areas/Tools/Views/FuneralServiceType/_AddUpdate.cshtml", updateRercord);
        }

        public ActionResult Save(FuneralServiceTypeVm model)
        {

            if (ModelState.IsValid)
            {
              
                var saveedId = FuneralServiceTypeBAL.Save(model);

                //TempData["IsBranchSetupSaved"] = true;
                //TempData.Keep("IsBranchSetupSaved");
                TempData["IsFuneralTypeSetupSaved"] = true;
                TempData.Keep("IsFuneralTypeSetupSaved");

                return RedirectToAction("Index", "FuneralServiceType", new { area = "Tools" });
            }

            return RedirectToAction(ControllerContext.RouteData.Values["action"] as string, ControllerContext.RouteData.Values["controller"] as string, new { area = "Tools" });
        }
    }
}