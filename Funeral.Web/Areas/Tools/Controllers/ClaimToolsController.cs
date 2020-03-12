using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.Areas.Admin.Controllers;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Tools.Controllers
{
    public class ClaimToolsController : BaseToolController
    {
        // GET: Tools/ClaimTools
        public ActionResult ClaimStatus()
        {
            dynamic model = new ExpandoObject();
            List<SecureGroupModel> UserRoleList = new List<SecureGroupModel>();
            UserRoleList.Add(new SecureGroupModel { pkiSecureGroupID = 0, sSecureGroupName = "Select Role" });
            UserRoleList.AddRange(ToolsSetingBAL.GetSecureGrouList().Where(sg => sg.pkiSecureGroupID != 12 && sg.pkiSecureGroupID != 4).ToList());
            model.UserRoleList = UserRoleList;
            return View(model);
        }
        [HttpPost]
        public ActionResult ClaimRightsPartial(int RoleId)
        {
            var list = ToolsSetingBAL.GetClaimRightsCollectionByRoleId(RoleId);
            return PartialView("_ClaimRightsPage", list);
        }
        [HttpPost]
        public ActionResult SaveClaimRights(List<ClaimRightsList> claimRightsList)
        {
            if (claimRightsList.Count > 0)
            {
                ToolsSetingBAL.DeleteClaimRights(claimRightsList[0].RoleId, UserName);
                claimRightsList = claimRightsList.Where(x => x.AvailableRights == true).ToList();
                foreach (var item in claimRightsList)
                {
                    ClaimRightsList claimRights = new ClaimRightsList();
                    claimRights.ClaimRightsId = item.ClaimRightsId;
                    claimRights.ClaimStatusId = item.ClaimStatusId;
                    claimRights.CreatedBy = UserName;
                    claimRights.RoleId = item.RoleId;
                    ToolsSetingBAL.SaveClaimRights(claimRights);
                }
                TempData["message"] = ShowMessage(MessageType.Success, "Claim Rights save successfully");
            }
            else
                TempData["message"] = ShowMessage(MessageType.Danger, "Claim Rights not added,Please try again");
            return RedirectToAction("ClaimStatus", "ClaimTools");
        }
    }
}