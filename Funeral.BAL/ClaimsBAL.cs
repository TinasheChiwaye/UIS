using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Funeral.BAL
{
    public class ClaimsBAL
    {
        public ClaimsBAL()
        { }

        public static int SaveClaims(ClaimsModel model)
        {
            return ClaimsDAL.SaveClaims(model);
        }
        public static List<ClaimsModel> SelectAllClaimsByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, DateTime DateFrom, DateTime DateTo, string status)
        {
            DataTable dr = ClaimsDAL.SelectAllClaimsByParlourIddt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder, DateFrom, DateTo, status);
            return FuneralHelper.DataTableMapToList<ClaimsModel>(dr);
        }
        public static List<ClaimsModel> SelectAllClaims(Guid ParlourId)
        {
            DataTable dr = ClaimsDAL.SelectAllClaims(ParlourId);
            return FuneralHelper.DataTableMapToList<ClaimsModel>(dr);
        }

        public static int ClaimsDelete(int ID, string UserName)
        {
            return ClaimsDAL.ClaimsDelete(ID, UserName);
        }
        public static int DeleteClaimByID(int ID)
        {
            return ClaimsDAL.DeleteClaimByID(ID);
        }
        public static ClaimsViewModel SelectAllClaimsBySearch(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool ClaimingForMember, bool ApplyWaitingPeriod)
        {
            DataTable dt = ClaimsDAL.SelectAllClaimsBySearchdt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder, ClaimingForMember, ApplyWaitingPeriod);
            ClaimsViewModel objViewModel = new ClaimsViewModel();
            objViewModel.ClaimsList = FuneralHelper.DataTableMapToList<ClaimsModel>(dt, true);
            //dt.NextResult();
            //dt.Read();
            //objViewModel.TotalRecord = Convert.ToInt64(dr["TotalRecord"]);
            //dt.Close();
            //dt.Dispose();
            return objViewModel;
        }
        public static List<ClaimsModel> GetClaimsbyMemeberNumber(int MemeberNo)
        {
            DataTable dt = ClaimsDAL.GetClaimsbyMemeberNumberdt(MemeberNo);
            return FuneralHelper.DataTableMapToList<ClaimsModel>(dt);
        }
        public static ClaimsModel SelectClaimsBypkid(int ID, Guid ParlourId)
        {
            DataTable dr = ClaimsDAL.SelectClaimsBypkiddt(ID, ParlourId);
            //return dr;
            return FuneralHelper.DataTableMapToList<ClaimsModel>(dr).FirstOrDefault();
        }
        public static int SaveClaimSupportedDocument(ClaimDocumentModel model)
        {
            return ClaimsDAL.SaveClaimSupportedDocument(model);
        }
        public static List<MembersModel> SelectMembersAndDependencies1(Guid ParlourId, bool MainMem, string Keyword)
        {
            DataTable dr = ClaimsDAL.SelectMembersAndDependenciesdt(ParlourId, MainMem, Keyword);
            return FuneralHelper.DataTableMapToList<MembersModel>(dr);
        }
        public static List<FamilyDependencyModel> SelectMembersAndDependencies2(Guid ParlourId, bool MainMem, string Keyword)
        {
            SqlDataReader dr = ClaimsDAL.SelectMembersAndDependencies(ParlourId, MainMem, Keyword);
            return FuneralHelper.DataReaderMapToList<FamilyDependencyModel>(dr);
        }
        public static MembersModel selectMemberByPkidAndParlor(Guid ParlourId, int MemId)
        {
            DataTable dr = ClaimsDAL.selectMemberByPkidAndParlordt(ParlourId, MemId);
            return FuneralHelper.DataTableMapToList<MembersModel>(dr).FirstOrDefault();
        }
        public static PlanModel GetPlanDetailsByPlanId(int planid)
        {
            DataTable dr = ClaimsDAL.GetPlanDetailsByPlanIddt(planid);
            return FuneralHelper.DataTableMapToList<PlanModel>(dr).FirstOrDefault();
        }
        public static int UpdateMemberNumber(int ID, string MemberNumber, string Claimnumber)
        {
            return ClaimsDAL.UpdateMemberNumber(ID, MemberNumber, Claimnumber);
        }
        public static List<ClaimDocumentModel> SelectClaimDocumentsByClaimId(int fkiClaimID)
        {
            DataTable dr = ClaimsDAL.SelectClaimDocumentsByClaimIddt(fkiClaimID);
            return FuneralHelper.DataTableMapToList<ClaimDocumentModel>(dr);
        }
        public static int DeleteClaimsdocumentById(int pkiClaimPictureID)
        {
            return ClaimsDAL.DeleteClaimsdocumentById(pkiClaimPictureID);
        }
        public static int ClaimDocumentStatusUpdated(int DocumentId, string ApprovedBy, int ClaimId)
        {
            return ClaimsDAL.ClaimDocumentStatusUpdated(DocumentId, ApprovedBy, ClaimId);
        }
        public static ClaimDocumentModel SelectClaimsDocumentsByPKId(int DocumentId)
        {
            return FuneralHelper.DataTableMapToList<ClaimDocumentModel>(ClaimsDAL.SelectClaimsDocumentsByPKIddt(DocumentId)).FirstOrDefault();
        }
        public static int UpdateClaimStatus(int ID, string status)
        {
            return ClaimsDAL.UpdateClaimStatus(ID, status);
        }
        public static void ClaimStatusChangeReason(ChangeStatusReason model)
        {
            ClaimsDAL.ClaimStatusChangeReason(model);
        }
        public static Tuple<DataTable, DataTable, DataTable, DataTable, DataTable> GetClaimMailReport(MembersModel objmodel, int MemberID, Guid ParlourId, int pkiClaimID)
        {
            return ClaimsDAL.GetClaimMailReport(objmodel, MemberID, ParlourId, pkiClaimID);
        }
        public static bool SendClaimEmail(int ClaimId, string Email, List<ClaimDocumentModel> claimDocumentModel, System.Net.Mail.Attachment claimdoc)
        {
            return ClaimsDAL.SendClaimEmail(ClaimId, Email, claimDocumentModel, claimdoc);
        }
        public static List<StatusModel> GetClaimsStatus()
        {
            DataTable dr = ClaimsDAL.GetClaimStatus();
            return FuneralHelper.DataTableMapToList<StatusModel>(dr);
        }
        //public static List<StatusModel> GetClaimsStatus(List<SecureUserGroupsModel> RoleList)
        //{
        //    List<StatusModel> statusModels = new List<StatusModel>(); ;
        //    foreach (var item in RoleList)
        //    {
        //        DataTable dr = ClaimsDAL.GetClaimStatus(item.fkiSecureGroupID);
        //        var convertStatusModel = FuneralHelper.DataTableMapToList<StatusModel>(dr);
        //        convertStatusModel = statusModels.Count > 0 ? convertStatusModel.Intersect(statusModels).ToList() : convertStatusModel;
        //        statusModels.AddRange(convertStatusModel);
        //    }
        //    return statusModels;
        //}
        public static List<StatusModel> GetClaimsStatus(int UserId)
        {
            DataTable dr = ClaimsDAL.GetClaimStatus(UserId);
            return FuneralHelper.DataTableMapToList<StatusModel>(dr);
        }
        public static List<StatusModel> GetExpectedClaimStatusList(int UserId)
        {
            List<StatusModel> NewList = new List<StatusModel>();
            var AllStatutes = GetClaimsStatus();
            GetClaimsStatus(UserId).ToList().ForEach(s => { NewList.Add(AllStatutes.Where(x => x.SequencePriority == (s.SequencePriority - 1)).FirstOrDefault()); });
            NewList.RemoveAll(item => item == null);
            return NewList;
        }
        public static List<ClaimStatusHistory> GetClaimStatusHistories(int ClaimId)
        {
            DataTable dr = ClaimsDAL.GetClaimStatusHistories(ClaimId);
            return FuneralHelper.DataTableMapToList<ClaimStatusHistory>(dr);
        }
        public static List<ClaimStatusCount> GetStatusCountList_Dashboard(Guid ParlourId, int BookId)
        {
            DataTable dr = ClaimsDAL.GetStatusCountList_Dashboard(ParlourId, BookId);
            return FuneralHelper.DataTableMapToList<ClaimStatusCount>(dr);
        }
        public static ClaimDashboard claimDashboardData(Guid ParlourId, int BookId)
        {
            ClaimDashboard claimDashboard = new ClaimDashboard();
            claimDashboard.claimStatuses = FuneralHelper.DataTableMapToList<ClaimStatusCount>(ClaimsDAL.GetStatusCountList_Dashboard(ParlourId, BookId));
            claimDashboard.claimDashboardLabel = FuneralHelper.DataTableMapToList<ClaimDashboardLabel>(ClaimsDAL.GetClaimDashboarLabel(ParlourId, BookId)).FirstOrDefault();
            claimDashboard.claimCostGraphs = FuneralHelper.DataTableMapToList<ClaimCostGraph>(ClaimsDAL.ClaimCostGraph(ParlourId, BookId));
            claimDashboard.claimPolicyGraph = FuneralHelper.DataTableMapToList<ClaimPolicyGraph>(ClaimsDAL.ClaimDashboardGraph_ByPolicy(ParlourId, BookId));
            return claimDashboard;
        }
        public static bool SendMail_StatusChanged(string ToEmail, string FromEmail, string ApplicationName, string Subject, string msg)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    MailMessage mm = new MailMessage(FromEmail, ToEmail);
                    mm.Subject = Subject;
                    string body = ApplicationName + " ,";
                    body += "<br /><br />";
                    body += msg;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    smtpClient.Send(mm);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static async Task SendMail_StatusChanged_Async(string ToEmail, string FromEmail, string ApplicationName, string Subject, string msg)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    MailMessage mm = new MailMessage(FromEmail, ToEmail);
                    mm.Subject = Subject;
                    string body = ApplicationName + " ,";
                    body += "<br /><br />";
                    body += msg;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    await smtpClient.SendMailAsync(mm);
                }
            }
            catch (Exception e)
            {
            }
        }
        public static List<ClaimDocumentModel> GetClaimDocumentsByClaimId(int fkiClaimID, Guid Parlourid, string MemberType)
        {
            DataTable dr = ClaimsDAL.GetClaimDocumentsByClaimId(fkiClaimID, Parlourid, MemberType);
            return FuneralHelper.DataTableMapToList<ClaimDocumentModel>(dr);
        }
        public static List<FuneralDocument> GetUploadedDocumentList(int pkiClaimID, Guid Parlourid)
        {
            DataTable dr = ClaimsDAL.GetUploadedDocumentList(pkiClaimID, Parlourid);
            return FuneralHelper.DataTableMapToList<FuneralDocument>(dr);
        }
        public static int AddClaimFollowUp(ClaimFollowUp model)
        {
            return ClaimsDAL.AddClaimFollowUp(model);
        }
        public static ClaimDocumentModel GetClaimDocumentsByDocumentId(int pkiClaimPictureID, Guid Parlourid)
        {
            DataTable dr = ClaimsDAL.GetClaimDocumentsByDocumentId(pkiClaimPictureID, Parlourid);
            return FuneralHelper.DataTableMapToList<ClaimDocumentModel>(dr).FirstOrDefault();
        }
        public static void UpdateClaimDocument(ChangeStatusReason model)
        {
            ClaimsDAL.UpdateClaimDocument(model);
        }
        public static void SaveClaimHistory(string IPAddress, int ClaimID, String Message, string CreatedBy, Guid ParlourID, ApplicationSettingsModel applicationSettings, bool SendEmail = true)
        {
            String msg = String.Format(Message, ClaimID, CreatedBy);
            ClaimHistory claimHistory = new ClaimHistory();
            claimHistory.IPAddress = IPAddress;
            claimHistory.FkiClaimId = ClaimID;
            claimHistory.Note = msg;
            claimHistory.ParlourId = ParlourID;
            claimHistory.CreatedBy = CreatedBy;
            ClaimsDAL.SaveClaimHistory(claimHistory);
            #region Send All Action Email
            if (SendEmail)
            {
                var fromMail = ConfigurationManager.AppSettings["ReportEmailSenderId"].ToString().Trim();
                SendMail_StatusChanged(applicationSettings.EmailForClaimNotification, fromMail, applicationSettings.ApplicationName, "ARL Notification", msg);
            }
            #endregion
        }
        public static async Task SaveClaimHistoryAsync(string IPAddress, int ClaimID, String Message, string CreatedBy, Guid ParlourID, ApplicationSettingsModel applicationSettings, bool SendEmail = true)
        {
            String msg = String.Format(Message, ClaimID, CreatedBy);
            ClaimHistory claimHistory = new ClaimHistory();
            claimHistory.IPAddress = IPAddress;
            claimHistory.FkiClaimId = ClaimID;
            claimHistory.Note = msg;
            claimHistory.ParlourId = ParlourID;
            claimHistory.CreatedBy = CreatedBy;
            ClaimsDAL.SaveClaimHistory(claimHistory);
            #region Send All Action Email
            if (SendEmail)
            {
                var fromMail = ConfigurationManager.AppSettings["ReportEmailSenderId"].ToString().Trim();
                await SendMail_StatusChanged_Async(applicationSettings.EmailForClaimNotification, fromMail, applicationSettings.ApplicationName, "ARL Notification", msg);
            }
            #endregion
        }
        public static List<ClaimReasonModel> GetClaimReasonList(Guid Parlourid)
        {
            DataTable dr = ClaimsDAL.GetClaimReasonList(Parlourid);
            return FuneralHelper.DataTableMapToList<ClaimReasonModel>(dr);
        }
        public static List<ClaimFollowUp> GetDocumentFollowUpHistory(int pkiClaimPictureID, Guid Parlourid)
        {
            DataTable dr = ClaimsDAL.GetDocumentFollowUpHistory(pkiClaimPictureID, Parlourid);
            return FuneralHelper.DataTableMapToList<ClaimFollowUp>(dr);
        }
        public static int ChangeClaimAssigned(ClaimAssigned claimAssigned)
        {
            return ClaimsDAL.ChangeClaimAssigned(claimAssigned);
        }
        public static int SaveExternalLink(ExternalUserLink externalUser)
        {
            return ClaimsDAL.SaveExternalLink(externalUser);
        }
        public static int UpdateExternalLink(Guid ExternalToken, bool TokenAccess)
        {
            return ClaimsDAL.UpdateExternalLink(ExternalToken, TokenAccess);
        }
        public static ExternalUserLink GetExternalLink(Guid ExternalToken)
        {
            DataTable dr = ClaimsDAL.GetExternalLink(ExternalToken);
            return FuneralHelper.DataTableMapToList<ExternalUserLink>(dr).FirstOrDefault();
        }
        public static FuneralModel CheckDuplicateClaims(Guid ParlourId, string IDNumber, string PolicyNumber)
        {
            DataTable dr = ClaimsDAL.CheckDuplicateClaims(ParlourId, IDNumber, PolicyNumber);
            return FuneralHelper.DataTableMapToList<FuneralModel>(dr).FirstOrDefault();
        }

    }
}
