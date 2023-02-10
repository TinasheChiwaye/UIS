using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Funeral.BAL
{
    public class FuneralBAL
    {
        public FuneralBAL()
        {
        }

        public static List<FuneralModel> SelectAllFuneralByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, DateTime? FromDate, DateTime? ToDate, string status)
        {
            DataTable dr = FuneralDAL.SelectAllFuneralByParlourIddt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder, FromDate, ToDate, status);
            return FuneralHelper.DataTableMapToList<FuneralModel>(dr);
        }
        public static int FuneralDelete(int ID, string UserName)
        {
            return FuneralDAL.FuneralDelete(ID, UserName);
        }

        public static int SaveFuneral(FuneralModel model)
        {
            return FuneralDAL.SaveFuneral(model);
        }


        public static FuneralModel SelectFuneralBypkid(int ID, Guid ParlourId)
        {
            DataTable dr = FuneralDAL.SelectFuneralBypkiddt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<FuneralModel>(dr).FirstOrDefault();
        }
        public static FuneralModel SelectFuneralByMemberNo(string MemberNo)
        {
            DataTable dr = FuneralDAL.SelectFuneralByMemberNodt(MemberNo);
            return FuneralHelper.DataTableMapToList<FuneralModel>(dr).FirstOrDefault();
        }

        public static FuneralModel SelectFuneralByFuneralId(int ID, Guid ParlourId)
        {
            DataTable dr = FuneralDAL.SelectFuneralByFuneralIddt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<FuneralModel>(dr).FirstOrDefault();
        }
        #region service
        public static int SaveFuneralService(FuneralServiceSelectModel model)
        {
            return FuneralDAL.SaveFuneralService(model);
        }
        public static int UpdateFuneralService(FuneralServiceSelectModel model)
        {
            return FuneralDAL.UpdateFuneralService(model);
        }
        public static List<FuneralServiceSelectModel> GetAllFuneralServices(Guid ParlourId)
        {
            DataTable dr = FuneralDAL.GetAllFuneralServicesdt(ParlourId);
            return FuneralHelper.DataTableMapToList<FuneralServiceSelectModel>(dr);
        }
        public static List<FuneralServiceSelectModel> SelectServiceByFuneralID(int fkiFuneralID)
        {
            DataTable dr = FuneralDAL.SelectServiceByFuneralIDdt(fkiFuneralID);
            return FuneralHelper.DataTableMapToList<FuneralServiceSelectModel>(dr);
        }
        public static FuneralServiceSelectModel SelectServiceByFunAndID(int fkiFuneralID, int pkiFuneralServiceSelectionID)
        {
            DataTable dr = FuneralDAL.SelectServiceByFunAndIDdt(fkiFuneralID, pkiFuneralServiceSelectionID);
            return FuneralHelper.DataTableMapToList<FuneralServiceSelectModel>(dr).FirstOrDefault();
        }

        public static FuneralModel SelectFuneralByParlAndPki(int pkiFuneralID, Guid ParlourId)
        {
            DataTable dr = FuneralDAL.SelectFuneralByParlAndPkidt(pkiFuneralID, ParlourId);
            return FuneralHelper.DataTableMapToList<FuneralModel>(dr).FirstOrDefault();
        }
        public static int DeleteFuneralServiceByID(int pkiFuneralServiceSelectionID)
        {
            return FuneralDAL.DeleteFuneralServiceByID(pkiFuneralServiceSelectionID);
        }


        public static FuneralModel GetInvoiceNumberByID(Guid ParlourId)
        {
            DataTable dr = FuneralDAL.GetInvoiceNumberByIDdt(ParlourId);
            return FuneralHelper.DataTableMapToList<FuneralModel>(dr).FirstOrDefault();
        }
        public static int UpdateAllFuneralData(int pkiFuneralID, string Notes, Decimal DisCount, Decimal Tax)
        {
            return FuneralDAL.UpdateAllFuneralData(pkiFuneralID, Notes, DisCount, Tax);
        }

        public static int UpdateAllFuneralServiceData(int pkiFuneralID, string InvoiceNumber, Decimal DisCount, Decimal Tax, string Notes)
        {
            return FuneralDAL.UpdateAllFuneralServiceData(pkiFuneralID, InvoiceNumber, DisCount, Tax, Notes);
        }
        public static int SaveFuneralSupportedDocument(FuneralDocumentModel model)
        {
            return FuneralDAL.SaveFuneralSupportedDocument(model);
        }
        public static List<FuneralDocumentModel> SelectFuneralDocumentsByMemberId(int fkiFuneralID)
        {
            DataTable dr = FuneralDAL.SelectFuneralDocumentsByMemberIddt(fkiFuneralID);
            return FuneralHelper.DataTableMapToList<FuneralDocumentModel>(dr);
        }
        public static int DeleteFuneraldocumentById(int pkiFuneralPictureID)
        {
            return FuneralDAL.DeleteFuneraldocumentById(pkiFuneralPictureID);
        }
        public static FuneralDocumentModel SelectFuneralDocumentsByPKId(int DocumentId)
        {
            return FuneralHelper.DataTableMapToList<FuneralDocumentModel>(FuneralDAL.SelectFuneralDocumentsByPKIddt(DocumentId)).FirstOrDefault();
        }


        #endregion
        public static FuneralModel SelectFuneralByClaimId(int ID)
        {
            DataTable dr = FuneralDAL.SelectFuneralByClaimIdddt(ID);
            return FuneralHelper.DataTableMapToList<FuneralModel>(dr).FirstOrDefault();
        }
        public static FuneralModel GetFuneralByClaimId(int ClaimId)
        {
            DataTable dr = FuneralDAL.GetFuneralByClaimId(ClaimId);
            return FuneralHelper.DataTableMapToList<FuneralModel>(dr).FirstOrDefault();
        }
        public static int UpdatePolicyStatus(int ClaimId, string IDNumber, Guid parlourid)
        {
            return FuneralDAL.UpdatePolicyStatus_MemberOfDependent(ClaimId, IDNumber, parlourid);
        }
        public static DataTable GetIdNumberAutocompleteData(string idNumber, Guid parlourid)
        {
            return FuneralDAL.GetIdNumberAutocompleteData(idNumber, parlourid);
        }
        public static int FuneralAssignedToUser(int? AssignedTo, int? PkiFuneralID, string ddlFuneralStatus)
        {
            return FuneralDAL.FuneralAssignedToUser(AssignedTo, PkiFuneralID, ddlFuneralStatus);
        }
        public static int FuneralScheduleAddEvent(int funeralId, int userId, string eventDescription, DateTime startDate, DateTime endDate)
        {
            return FuneralDAL.FuneralScheduleAddEvent(funeralId, userId, eventDescription, startDate, endDate);
        }
        public static DataTable GetFuneralScheduleEvents(int funeralId)
        {
            return FuneralDAL.GetFuneralScheduleEvents(funeralId);
        }
        public static int FuneralScheduleEditEvent(DateTime startDate, DateTime endDate, int id)
        {
            return FuneralDAL.FuneralScheduleEditEvent(startDate, endDate, id);
        }
        public static List<DownloadSchedulesViewModel> GetDownLoadCalenderList(DateTime? dateFrom, DateTime? dateTo)
        {
            return FuneralHelper.DataTableMapToList<DownloadSchedulesViewModel>(FuneralDAL.GetDownLoadCalenderList(dateFrom,dateTo));
        }
    }
}
