using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.Areas.Admin.Models;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static Funeral.Model.FuneralEnum;

namespace Funeral.Web.Controllers
{
    public class ExternalUserController : Controller
    {
        // GET: ExternalUser
        public ActionResult Index(Guid ExternalToken)
        {
            ExternalUserLink external = ClaimsBAL.GetExternalLink(ExternalToken);
            if (external != null)
            {
                ViewBag.ExternalToken = ExternalToken;
                ClaimStatusHistoryModal claimStatusHistoryModal = new ClaimStatusHistoryModal();
                claimStatusHistoryModal.claimStatusHistory = ClaimsBAL.GetClaimStatusHistories(external.ClaimId);
                claimStatusHistoryModal.claimsModel = ClaimsBAL.SelectClaimsBypkid(external.ClaimId, external.parlourId);
                claimStatusHistoryModal.funeralModel = FuneralBAL.GetFuneralByClaimId(external.ClaimId);
                claimStatusHistoryModal.memberInvoices = MembersBAL.GetInvoicesByMemberID(external.parlourId, claimStatusHistoryModal.claimsModel.fkiMemberID);
                claimStatusHistoryModal.claimDocuments = ClaimsBAL.GetClaimDocumentsByClaimId(external.ClaimId, external.parlourId, claimStatusHistoryModal.funeralModel.MemberType);
                ClaimsBAL.UpdateExternalLink(external.ExternalToken, true);
                return View(claimStatusHistoryModal);
            }
            else
            {
                return RedirectToAction("ExternalLinkError", "ExternalUser");
            }
        }
        public ActionResult ExternalLinkError()
        {
            return View();
        }
        #region Upload Claim Document
        [HttpGet]
        public ActionResult ClaimDocumentPartial(int ClaimId, Guid parlourId, int pkiClaimPictureID, Guid ExternalToken)
        {
            ClaimsModel data = ClaimsBAL.SelectClaimsBypkid(ClaimId, parlourId);
            ClaimDocument claimDocument = new ClaimDocument();
            claimDocument.ParlourId = data.parlourid;
            claimDocument.fkiMemberId = data.fkiMemberID;
            claimDocument.PkiClaimId = data.pkiClaimID;
            claimDocument.Status = ClaimDocumentStatusEnum.Pending.ToString();
            claimDocument.DocumentId = pkiClaimPictureID;
            claimDocument.ExternalToken = ExternalToken;
            return PartialView("_UploadClaimDocumentForm", claimDocument);
        }
        [HttpPost]
        public ActionResult SaveClaimDocument(ClaimDocument document, HttpPostedFileBase fuSupportDocument)
        {
            try
            {
                if (fuSupportDocument != null)
                {
                    string fileName = Path.GetFileName(fuSupportDocument.FileName);
                    var path = (dynamic)null;
                    string Str = System.DateTime.Now.Ticks.ToString();
                    path = 1012 + "_" + Str + "_" + fileName;
                    string fname = Server.MapPath("~/Upload/FuneralDocument/" + document.ParlourId + "/");
                    if (!Directory.Exists(fname))
                        Directory.CreateDirectory(fname);
                    fname = fname + path;
                    fuSupportDocument.SaveAs(fname);
                    string dbPath = "~/Upload/FuneralDocument/" + document.ParlourId + "/" + path;
                    ClaimDocumentModel claimDoc = new ClaimDocumentModel();
                    claimDoc.ImageName = fileName;
                    Byte[] docPath = Encoding.ASCII.GetBytes(dbPath);
                    claimDoc.ImageFile = docPath;
                    claimDoc.fkiClaimID = Convert.ToInt32(document.PkiClaimId);
                    claimDoc.Parlourid = document.ParlourId;
                    claimDoc.DocType = document.DocumentId;
                    claimDoc.Status = ClaimDocumentStatusEnum.Pending.ToString();
                    claimDoc.DocContentType = fuSupportDocument.ContentType;
                    claimDoc.CreateDate = System.DateTime.Now;
                    claimDoc.ModifiedUser = "External User";
                    claimDoc.LastModified = System.DateTime.Now;
                    int docID = ClaimsBAL.SaveClaimSupportedDocument(claimDoc);
                    #region ClaimHistory
                    ClaimsBAL.UpdateExternalLink(document.ExternalToken, false);
                    var applicationSettings = ToolsSetingBAL.GetApplictionByParlourID(document.ParlourId);
                    ClaimsBAL.SaveClaimHistory(Request.UserHostAddress.ToString(), document.PkiClaimId, StaticMessages.ClaimDocumentUploaded, "External User", document.ParlourId, applicationSettings);
                    #endregion
                }
            }
            catch (Exception e)
            {
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        #endregion
    }
}