using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funeral.DAL;
using System.Data.SqlClient;
using System.Data;

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
        public static List<ClaimsModel> SelectAllClaimsByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder,DateTime DateFrom,DateTime DateTo,string status)
        {
            DataTable dr = ClaimsDAL.SelectAllClaimsByParlourIddt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder,DateFrom,DateTo,status);
            return FuneralHelper.DataTableMapToList<ClaimsModel>(dr);
        }
        public static int ClaimsDelete(int ID, string UserName)
        {
            return ClaimsDAL.ClaimsDelete(ID, UserName);
        }
        public static ClaimsViewModel SelectAllClaimsBySearch(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool ClaimingForMember, bool ApplyWaitingPeriod)
        {
            DataTable dt = ClaimsDAL.SelectAllClaimsBySearchdt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder, ClaimingForMember, ApplyWaitingPeriod);
            ClaimsViewModel objViewModel = new ClaimsViewModel();
            objViewModel.ClaimsList = FuneralHelper.DataTableMapToList<ClaimsModel>(dt,true);
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
            DataTable  dr = ClaimsDAL.SelectClaimsBypkiddt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<ClaimsModel>(dr).FirstOrDefault();
        }
        public static int SaveClaimSupportedDocument(ClaimDocumentModel model)
        {
            return ClaimsDAL.SaveClaimSupportedDocument(model);
        }
        public static List<MembersModel> SelectMembersAndDependencies1(Guid ParlourId, bool MainMem, string Keyword)
        {
            DataTable  dr = ClaimsDAL.SelectMembersAndDependenciesdt(ParlourId, MainMem, Keyword);
            return FuneralHelper.DataTableMapToList <MembersModel>(dr);
        }
        //public static List<FamilyDependencyModel> SelectMembersAndDependencies2(Guid ParlourId, bool MainMem, string Keyword)
        //{
        //    SqlDataReader dr = ClaimsDAL.SelectMembersAndDependencies(ParlourId, MainMem, Keyword);
        //    return FuneralHelper.DataReaderMapToList<FamilyDependencyModel>(dr);
        //}
        public static MembersModel selectMemberByPkidAndParlor(Guid ParlourId, int MemId)
        {
            DataTable  dr = ClaimsDAL.selectMemberByPkidAndParlordt(ParlourId, MemId);
            return FuneralHelper.DataTableMapToList<MembersModel>(dr).FirstOrDefault();
        }
        public static PlanModel GetPlanDetailsByPlanId(int planid)
        {
            DataTable  dr = ClaimsDAL.GetPlanDetailsByPlanIddt(planid);
            return FuneralHelper.DataTableMapToList<PlanModel>(dr).FirstOrDefault();
        }
        public static int UpdateMemberNumber(int ID, string MemberNumber, string Claimnumber)
        {
            return ClaimsDAL.UpdateMemberNumber(ID, MemberNumber, Claimnumber);
        }
        public static List<ClaimDocumentModel> SelectClaimDocumentsByClaimId(int fkiClaimID)
        {
            DataTable  dr = ClaimsDAL.SelectClaimDocumentsByClaimIddt(fkiClaimID);
            return FuneralHelper.DataTableMapToList<ClaimDocumentModel>(dr);
        }
        public static int DeleteClaimsdocumentById(int pkiClaimPictureID)
        {
            return ClaimsDAL.DeleteClaimsdocumentById(pkiClaimPictureID);
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
    }
}
