using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.DAL
{
    public sealed class ClaimsDAL
    {
        private ClaimsDAL()
        { }

        public static int SaveClaims(ClaimsModel model)
        {
            string query = "SaveClaims";

            DbParameter[] ObjParam = new DbParameter[33];

            ObjParam[0] = new DbParameter("@pkiClaimID", DbParameter.DbType.Int, 0, model.pkiClaimID);
            ObjParam[1] = new DbParameter("@fkiMemberID", DbParameter.DbType.Int, 0, model.fkiMemberID);
            ObjParam[2] = new DbParameter("@MemberNumber", DbParameter.DbType.NVarChar, 0, model.MemberNumber);
            ObjParam[3] = new DbParameter("@ClaimDate", DbParameter.DbType.DateTime, 0, model.ClaimDate);
            ObjParam[4] = new DbParameter("@ClaimNotes", DbParameter.DbType.NVarChar, 0, model.ClaimNotes);
            ObjParam[5] = new DbParameter("@CourseOfDearth", DbParameter.DbType.NVarChar, 0, model.CourseOfDearth);
            ObjParam[6] = new DbParameter("@HostingFuneral", DbParameter.DbType.Bit, 0, model.HostingFuneral);
            ObjParam[7] = new DbParameter("@ClaimantTitle", DbParameter.DbType.NVarChar, 0, model.ClaimantTitle);
            ObjParam[8] = new DbParameter("@ClaimantFullname", DbParameter.DbType.NVarChar, 0, model.ClaimantFullname);
            ObjParam[9] = new DbParameter("@ClaimantSurname", DbParameter.DbType.NVarChar, 0, model.ClaimantSurname);
            ObjParam[10] = new DbParameter("@ClaimantIDNumber", DbParameter.DbType.NVarChar, 0, model.ClaimantIDNumber);
            ObjParam[11] = new DbParameter("@ClaimantDateOfBirth", DbParameter.DbType.DateTime, 0, model.ClaimantDateOfBirth);
            ObjParam[12] = new DbParameter("@ClaimantGender", DbParameter.DbType.NVarChar, 0, model.ClaimantGender);
            ObjParam[13] = new DbParameter("@ClaimantAddressLine1", DbParameter.DbType.NVarChar, 0, model.ClaimantAddressLine1);
            ObjParam[14] = new DbParameter("@ClaimantAddressLine2", DbParameter.DbType.NVarChar, 0, model.ClaimantAddressLine2);
            ObjParam[15] = new DbParameter("@ClaimantAddressLine3", DbParameter.DbType.NVarChar, 0, model.ClaimantAddressLine3);
            ObjParam[16] = new DbParameter("@ClaimantAddressLine4", DbParameter.DbType.NVarChar, 0, model.ClaimantAddressLine4);
            ObjParam[17] = new DbParameter("@ClaimantCode", DbParameter.DbType.NVarChar, 0, model.ClaimantCode);
            ObjParam[18] = new DbParameter("@ClaimantContactNumber", DbParameter.DbType.NVarChar, 0, model.ClaimantContactNumber);
            ObjParam[19] = new DbParameter("@BeneficiaryBank", DbParameter.DbType.NVarChar, 0, model.BeneficiaryBank);
            ObjParam[20] = new DbParameter("@BeneficiaryAccountHolder", DbParameter.DbType.NVarChar, 0, model.BeneficiaryAccountHolder);
            ObjParam[21] = new DbParameter("@BeneficiaryAccountNumber", DbParameter.DbType.NVarChar, 0, model.BeneficiaryAccountNumber);
            ObjParam[22] = new DbParameter("@BeneficiaryBankBranch", DbParameter.DbType.NVarChar, 0, model.BeneficiaryBankBranch);
            ObjParam[23] = new DbParameter("@BeneficiaryBranchCode", DbParameter.DbType.NVarChar, 0, model.BeneficiaryBranchCode);
            ObjParam[24] = new DbParameter("@BeneficiaryAccountType", DbParameter.DbType.NVarChar, 0, model.BeneficiaryAccountType);
            ObjParam[25] = new DbParameter("@LoggedBy", DbParameter.DbType.NVarChar, 0, model.LoggedBy);
            ObjParam[26] = new DbParameter("@Cover", DbParameter.DbType.Money, 0, model.Cover);
            ObjParam[27] = new DbParameter("@BodyCollectedFrom", DbParameter.DbType.NVarChar, 0, model.BodyCollectedFrom);
            ObjParam[28] = new DbParameter("@ClaimingFor", DbParameter.DbType.NVarChar, 0, model.ClaimingFor);
            ObjParam[29] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[30] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, model.ModifiedUser);
            ObjParam[31] = new DbParameter("@Payout", DbParameter.DbType.Bit, 0, model.Payout);
            ObjParam[32] = new DbParameter("@PayoutValue", DbParameter.DbType.Money, 0, model.PayoutValue);


            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }

        public static SqlDataReader SelectAllClaimsByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, DateTime DateFrom, DateTime DateTo,string status)
        {
            string SP = "SelectAllClaimsByParlourId";
            DbParameter[] ObjParam = new DbParameter[9];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[6] = new DbParameter("@dateFrom", DbParameter.DbType.DateTime, 0, DateFrom);
            ObjParam[7] = new DbParameter("@dateTo", DbParameter.DbType.DateTime, 0, DateTo);
            ObjParam[8] = new DbParameter("@status", DbParameter.DbType.VarChar, 0, status);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, SP, ObjParam));
        }

        public static DataTable SelectAllClaimsByParlourIddt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, DateTime DateFrom, DateTime DateTo, string status)
        {
            string SP = "SelectAllClaimsByParlourId";
            DbParameter[] ObjParam = new DbParameter[9];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[6] = new DbParameter("@dateFrom", DbParameter.DbType.DateTime, 0, DateFrom);
            ObjParam[7] = new DbParameter("@dateTo", DbParameter.DbType.DateTime, 0, DateTo);
            ObjParam[8] = new DbParameter("@status", DbParameter.DbType.VarChar, 0, status);


            return DbConnection.GetDataTable(CommandType.StoredProcedure, SP, ObjParam);
        }

        public static int ClaimsDelete(int ID, string UserName)
        {
            string query = "update Claims set IsDeleted = 1, DeletedDate=getdate(), DeletedBy=@UserName where pkiClaimID=@pkiClaimID";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@pkiFuneralID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@UserName", DbParameter.DbType.NVarChar, 0, UserName);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }
        public static SqlDataReader SelectAllClaimsBySearch(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool ClaimingForMember, bool ApplyWaitingPeriod)
        {
            DbParameter[] ObjParam = new DbParameter[8];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[6] = new DbParameter("@ClaimingForMember", DbParameter.DbType.Bit, 0, ClaimingForMember);
            ObjParam[7] = new DbParameter("@ApplyWaitingPeriod", DbParameter.DbType.Bit, 0, ApplyWaitingPeriod);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectAllClaimsBySearch", ObjParam);
        }
        public static DataTable SelectAllClaimsBySearchdt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool ClaimingForMember, bool ApplyWaitingPeriod)
        {
            DbParameter[] ObjParam = new DbParameter[8];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[6] = new DbParameter("@ClaimingForMember", DbParameter.DbType.Bit, 0, ClaimingForMember);
            ObjParam[7] = new DbParameter("@ApplyWaitingPeriod", DbParameter.DbType.Bit, 0, ApplyWaitingPeriod);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectAllClaimsBySearch", ObjParam);
        }
        public static DataSet  SelectAllClaimsBySearchds(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool ClaimingForMember, bool ApplyWaitingPeriod)
        {
            DbParameter[] ObjParam = new DbParameter[8];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[6] = new DbParameter("@ClaimingForMember", DbParameter.DbType.Bit, 0, ClaimingForMember);
            ObjParam[7] = new DbParameter("@ApplyWaitingPeriod", DbParameter.DbType.Bit, 0, ApplyWaitingPeriod);

            return DbConnection.GetDataSet(CommandType.StoredProcedure, "SelectAllClaimsBySearch", ObjParam);
        }
        //public static DataSet SelectAllClaimsBySearchdt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool ClaimingForMember, bool ApplyWaitingPeriod)
        //{
        //    DbParameter[] ObjParam = new DbParameter[8];

        //    ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
        //    ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
        //    ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
        //    ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
        //    ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
        //    ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
        //    ObjParam[6] = new DbParameter("@ClaimingForMember", DbParameter.DbType.Bit, 0, ClaimingForMember);
        //    ObjParam[7] = new DbParameter("@ApplyWaitingPeriod", DbParameter.DbType.Bit, 0, ApplyWaitingPeriod);

        //    return DbConnection.GetDataSet(CommandType.StoredProcedure, "SelectAllClaimsBySearch", ObjParam);
        //}

        public static SqlDataReader GetClaimsbyMemeberNumber(int MemeberNo)
        {
            string SP = "GetClaimsbyMemeberNumber";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@MemberNumber", DbParameter.DbType.Int, 0, MemeberNo);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, SP, ObjParam));
        }
        //public static DataTable  GetClaimsbyMemeberNumberdt(int MemeberNo)
        //{
        //    string SP = "GetClaimsbyMemeberNumber";
        //    DbParameter[] ObjParam = new DbParameter[1];
        //    ObjParam[0] = new DbParameter("@MemberNumber", DbParameter.DbType.Int, 0, MemeberNo);
        //    return (DbConnection.GetDataTable(CommandType.StoredProcedure, SP, ObjParam));
        //}

        public static DataTable GetClaimsbyMemeberNumberdt(int MemeberNo)
        {
            string SP = "GetClaimsbyMemeberNumber";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@MemberNumber", DbParameter.DbType.Int, 0, MemeberNo);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, SP, ObjParam);
        }

        public static SqlDataReader SelectClaimsBypkid(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@pkiClaimID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectClaimsBypkid", ObjParam);
        }

        public static DataTable  SelectClaimsBypkiddt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@pkiClaimID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectClaimsBypkid", ObjParam);
        }
        public static int SaveClaimSupportedDocument(ClaimDocumentModel model)
        {
            string query = "SaveClaimSupportedDocument";
            DbParameter[] ObjParam = new DbParameter[9];

            ObjParam[0] = new DbParameter("@ImageName", DbParameter.DbType.NVarChar, 0, model.ImageName);
            ObjParam[1] = new DbParameter("@ImageFile", DbParameter.DbType.Image, 0, model.ImageFile);
            ObjParam[2] = new DbParameter("@fkiClaimID", DbParameter.DbType.Int, 0, model.fkiClaimID);
            ObjParam[3] = new DbParameter("@CreateDate", DbParameter.DbType.DateTime, 0, model.CreateDate);
            ObjParam[4] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.Parlourid);
            ObjParam[5] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[6] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[7] = new DbParameter("@DocContentType", DbParameter.DbType.VarChar, 0, model.DocContentType);
            ObjParam[8] = new DbParameter("@DocType", DbParameter.DbType.Int, 0, model.DocType);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static SqlDataReader SelectMembersAndDependencies(Guid ParlourId, bool MainMem, string Keyword)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@ClaimingForMember", DbParameter.DbType.Bit, 0, MainMem);
            ObjParam[2] = new DbParameter("@keyword", DbParameter.DbType.NVarChar, 0, Keyword);


            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectMembersAndDependencies", ObjParam));
        }

        public static DataTable  SelectMembersAndDependenciesdt(Guid ParlourId, bool MainMem, string Keyword)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@ClaimingForMember", DbParameter.DbType.Bit, 0, MainMem);
            ObjParam[2] = new DbParameter("@keyword", DbParameter.DbType.NVarChar, 0, Keyword);


            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectMembersAndDependencies", ObjParam));
        }

        public static SqlDataReader selectMemberByPkidAndParlor(Guid ParlourId, int MemId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@ID", DbParameter.DbType.Int, 0, MemId);

            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "selectMemberByPkidAndParlor", ObjParam));
        }
        public static DataTable  selectMemberByPkidAndParlordt(Guid ParlourId, int MemId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@ID", DbParameter.DbType.Int, 0, MemId);

            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "selectMemberByPkidAndParlor", ObjParam));
        }
        public static SqlDataReader GetPlanDetailsByPlanId(int planid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, planid);

            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "GetPlanDetailsByPlanId", ObjParam));
        }
        public static DataTable  GetPlanDetailsByPlanIddt(int planid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, planid);

            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "GetPlanDetailsByPlanId", ObjParam));
        }
        public static int UpdateMemberNumber(int ID, string MemberNumber, string Claimnumber)
        {
            string query = "update Funerals set MemeberNumber = @MemeberNumber,Claimnumber=@Claimnumber where pkiFuneralID=@pkiFuneralID";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@MemeberNumber", DbParameter.DbType.NVarChar, 0, MemberNumber);
            ObjParam[1] = new DbParameter("@pkiFuneralID", DbParameter.DbType.Int, 0, ID);
            ObjParam[2] = new DbParameter("@Claimnumber", DbParameter.DbType.NVarChar, 0, Claimnumber);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }
        public static SqlDataReader SelectClaimDocumentsByClaimId(int fkiClaimID)
        {
            string query = "SelectClaimDocumentsByClaimId";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@fkiClaimID", DbParameter.DbType.Int, 0, fkiClaimID);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, query, ObjParam);
        }

        public static DataTable  SelectClaimDocumentsByClaimIddt(int fkiClaimID)
        {
            string query = "SelectClaimDocumentsByClaimId";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@fkiClaimID", DbParameter.DbType.Int, 0, fkiClaimID);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, query, ObjParam);
        }
        public static int DeleteClaimsdocumentById(int pkiClaimPictureID)
        {
            string query = "Delete from ClaimDocuments Where pkiClaimPictureID=@pkiClaimPictureID";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiClaimPictureID", DbParameter.DbType.Int, 0, pkiClaimPictureID);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }
        public static SqlDataReader SelectClaimsDocumentsByPKId(int DocumentId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiClaimPictureID", DbParameter.DbType.Int, 0, DocumentId);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectClaimsDocumentsByPKId", ObjParam));
        }
        public static DataTable SelectClaimsDocumentsByPKIddt(int DocumentId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiClaimPictureID", DbParameter.DbType.Int, 0, DocumentId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectClaimsDocumentsByPKId", ObjParam));
        }
        public static int UpdateClaimStatus(int ID, string status)
        {
            string query = "update Claims set [Status] = @Status where pkiClaimID=@pkiClaimID";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@Status", DbParameter.DbType.NVarChar, 0, status);
            ObjParam[1] = new DbParameter("@pkiClaimID", DbParameter.DbType.Int, 0, ID);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }

        public static void ClaimStatusChangeReason(ChangeStatusReason model)
        {
            string query = "INSERT INTO [dbo].[ClaimStatusChangeReason]([ChangeReason],[ClaimID],[ParlourID],[CreatedDate],[ChangedBy],[CurrentStatus],[NewStatus])VALUES(@ChangeReason,@ClaimID,@ParlourID,GetDate(),@ChangedBy,@CurrentStatus,@NewStatus)";
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@ChangeReason", DbParameter.DbType.NVarChar, 0, model.ChangeReason);
            ObjParam[1] = new DbParameter("@ClaimID", DbParameter.DbType.Int, 0, model.ClaimID);
            ObjParam[2] = new DbParameter("@ParlourID", DbParameter.DbType.UniqueIdentifier, 0, model.ParlourID);
            ObjParam[3] = new DbParameter("@ChangedBy", DbParameter.DbType.Int, 0, model.ChangedBy);
            ObjParam[4] = new DbParameter("@CurrentStatus", DbParameter.DbType.NVarChar, 0, model.CurrentStatus);
            ObjParam[5] = new DbParameter("@NewStatus", DbParameter.DbType.NVarChar, 0, model.NewStatus);

            DbConnection.ExecuteNonQuery(CommandType.Text, query, ObjParam);
        }
    }
}
