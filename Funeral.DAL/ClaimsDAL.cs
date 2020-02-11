using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;

namespace Funeral.DAL
{
    public sealed class ClaimsDAL
    {
        private ClaimsDAL()
        { }

        public static int SaveClaims(ClaimsModel model)
        {
            string query = "SaveClaims";

            DbParameter[] ObjParam = new DbParameter[35];

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
            ObjParam[33] = new DbParameter("@CreatedBy", DbParameter.DbType.Int, 0, model.CreatedBy);
            ObjParam[34] = new DbParameter("@Email", DbParameter.DbType.NVarChar, 0, model.Email);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }

        public static SqlDataReader SelectAllClaimsByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, DateTime DateFrom, DateTime DateTo, string status)
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

        public static DataTable SelectAllClaims(Guid ParlourId)
        {
            string SP = "SelectAllClaims";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, SP, ObjParam));
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

        public static int DeleteClaimByID(int ID)
        {
            //pkiClaimID
            string sp = "DeleteClaimsByID";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiClaimID", DbParameter.DbType.Int, 0, ID);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, sp, ObjParam));
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
        public static DataSet SelectAllClaimsBySearchds(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool ClaimingForMember, bool ApplyWaitingPeriod)
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

        public static DataTable SelectClaimsBypkiddt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@pkiClaimID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectClaimsBypkid", ObjParam);
        }
        public static int SaveClaimSupportedDocument(ClaimDocumentModel model)
        {
            string query = "SaveClaimSupportedDocument";
            DbParameter[] ObjParam = new DbParameter[10];

            ObjParam[0] = new DbParameter("@ImageName", DbParameter.DbType.NVarChar, 0, model.ImageName);
            ObjParam[1] = new DbParameter("@ImageFile", DbParameter.DbType.Image, 0, model.ImageFile);
            ObjParam[2] = new DbParameter("@fkiClaimID", DbParameter.DbType.Int, 0, model.fkiClaimID);
            ObjParam[3] = new DbParameter("@CreateDate", DbParameter.DbType.DateTime, 0, model.CreateDate);
            ObjParam[4] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.Parlourid);
            ObjParam[5] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[6] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[7] = new DbParameter("@DocContentType", DbParameter.DbType.VarChar, 0, model.DocContentType);
            ObjParam[8] = new DbParameter("@DocumentStatus", DbParameter.DbType.VarChar, 0, model.Status);
            ObjParam[9] = new DbParameter("@DocType", DbParameter.DbType.Int, 0, model.DocType);

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

        public static DataTable SelectMembersAndDependenciesdt(Guid ParlourId, bool MainMem, string Keyword)
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
        public static DataTable selectMemberByPkidAndParlordt(Guid ParlourId, int MemId)
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
        public static DataTable GetPlanDetailsByPlanIddt(int planid)
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

        public static DataTable SelectClaimDocumentsByClaimIddt(int fkiClaimID)
        {
            string query = "SelectClaimDocumentsByClaimId";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@fkiClaimID", DbParameter.DbType.Int, 0, fkiClaimID);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, query, ObjParam);
        }

        public static int DeleteClaimsdocumentById(int pkiClaimPictureID)
        {
            string query = "DeleteClaimDocuments";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiClaimPictureID", DbParameter.DbType.Int, 0, pkiClaimPictureID);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static int ClaimDocumentStatusUpdated(int DocumentId, string ApprovedBy, int ClaimId)
        {
            string query = "UpdateClaimDocumentStatus";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@DocumentId", DbParameter.DbType.Int, 0, DocumentId);
            ObjParam[1] = new DbParameter("@ClaimId", DbParameter.DbType.Int, 0, ClaimId);
            ObjParam[2] = new DbParameter("@ApprovedBy", DbParameter.DbType.NVarChar, 0, ApprovedBy);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

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
            string query = "ClaimStatusChangeLog_new";
            DbParameter[] ObjParam = new DbParameter[7];
            ObjParam[0] = new DbParameter("@ChangeReason", DbParameter.DbType.NVarChar, 0, model.ChangeReason);
            ObjParam[1] = new DbParameter("@ClaimID", DbParameter.DbType.Int, 0, model.ClaimID);
            ObjParam[2] = new DbParameter("@ParlourID", DbParameter.DbType.UniqueIdentifier, 0, model.ParlourID);
            ObjParam[3] = new DbParameter("@ChangedBy", DbParameter.DbType.Int, 0, model.ChangedBy);
            ObjParam[4] = new DbParameter("@CurrentStatus", DbParameter.DbType.NVarChar, 0, model.CurrentStatus);
            ObjParam[5] = new DbParameter("@NewStatus", DbParameter.DbType.NVarChar, 0, model.NewStatus);
            ObjParam[6] = new DbParameter("@Comment", DbParameter.DbType.NVarChar, 0, model.Comment);
            DbConnection.ExecuteNonQuery(CommandType.StoredProcedure, query, ObjParam);
        }
        public static Tuple<DataTable, DataTable, DataTable, DataTable, DataTable> GetClaimMailReport(MembersModel objmodel, int MemberID, Guid ParlourId, int pkiClaimID)
        {
            #region planadp datatble
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "GetPlanDetailsByPlanId";
            com.Parameters.Add(new SqlParameter("@ID", objmodel.fkiPlanID));
            SqlDataAdapter planadp = new SqlDataAdapter(com);
            DataTable PlanDt = new DataTable();
            planadp.Fill(PlanDt);
            #endregion
            #region MemberAdp datatble
            SqlCommand com2 = new SqlCommand();
            com2.CommandType = CommandType.StoredProcedure;
            com2.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com2.CommandText = "MemberSelectList";
            com2.Parameters.Add(new SqlParameter("@ID", MemberID));
            com2.Parameters.Add(new SqlParameter("@ParlourId", ParlourId));
            SqlDataAdapter MemberAdp = new SqlDataAdapter(com2);
            DataTable Memberdt = new DataTable();
            MemberAdp.Fill(Memberdt);
            #endregion
            #region ClaimAdp datatble
            SqlCommand com3 = new SqlCommand();
            com3.CommandType = CommandType.StoredProcedure;
            com3.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com3.CommandText = "GetClaimsbyMemeberNumber";
            com3.Parameters.Add(new SqlParameter("@MemberNumber", pkiClaimID));
            SqlDataAdapter ClaimAdp = new SqlDataAdapter(com3);
            DataTable claimDT = new DataTable();
            ClaimAdp.Fill(claimDT);
            #endregion
            #region DeceAdp4 datatble
            SqlCommand com4 = new SqlCommand();
            com4.CommandType = CommandType.StoredProcedure;
            com4.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com4.CommandText = "SelectFuneralByMemberNo";
            com4.Parameters.Add(new SqlParameter("@MemberNumber", pkiClaimID.ToString()));
            SqlDataAdapter DeceAdp4 = new SqlDataAdapter(com4);
            DataTable decedt = new DataTable();
            DeceAdp4.Fill(decedt);
            #endregion
            #region TnCadp datatble
            SqlCommand com5 = new SqlCommand();
            com5.CommandType = CommandType.StoredProcedure;
            com5.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com5.CommandText = "SelectApplicationTnCByParlourId";
            com5.Parameters.Add(new SqlParameter("@ParlourId", ParlourId));
            SqlDataAdapter TnCadp = new SqlDataAdapter(com5);
            DataTable dtTnC = new DataTable();
            TnCadp.Fill(dtTnC);
            #endregion
            return Tuple.Create(PlanDt, Memberdt, claimDT, decedt, dtTnC);
        }
        public static bool SendClaimEmail(int ClaimId, string Email, List<ClaimDocumentModel> documentModel, Attachment claimdoc)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    MailMessage message = new MailMessage(ConfigurationManager.AppSettings["ClaimDocumentSender"].ToString().Trim(), Email.Trim(), "Claim document", "Please find all attached document");
                    if (documentModel != null)
                    {
                        foreach (var document in documentModel)
                        {
                            message.Attachments.Add(new Attachment(new MemoryStream(document.ImageFile), document.ImageName));
                        }
                    }
                    message.Attachments.Add(claimdoc);
                    smtpClient.Send(message);
                }
                return true;
            }
            catch (Exception e)
            { return false; }
        }
        public static DataTable GetClaimStatus()
        {
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetClaimStatuses");
        }
        //public static DataTable GetClaimStatus(int RoleId)
        //{
        //    DbParameter[] ObjParam = new DbParameter[1];
        //    ObjParam[0] = new DbParameter("@RoleId", DbParameter.DbType.Int, 0, RoleId);
        //    return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetClaimStatuses", ObjParam);
        //}
        public static DataTable GetClaimStatus(int UserId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@UserId", DbParameter.DbType.Int, 0, UserId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetClaimStatuses_ByUserId", ObjParam);
        }
        public static DataTable GetClaimStatusHistories(int ClaimId)
        {
            string SP = "getClaimStatusHistory";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ClaimId", DbParameter.DbType.Int, 0, ClaimId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, SP, ObjParam));
        }
        public static DataTable GetStatusCountList_Dashboard(Guid ParlourId)
        {
            string SP = "GetClaimStatusCount_dashboard";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, SP, ObjParam));
        }
        public static DataTable GetClaimDocumentsByClaimId(int fkiClaimID, Guid Parlourid, string MemberType)
        {
            string query = "GetClaimDocumentsByClaimId";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@fkiClaimID", DbParameter.DbType.Int, 0, fkiClaimID);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[2] = new DbParameter("@MemberType", DbParameter.DbType.NVarChar, 0, MemberType);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, query, ObjParam);
        }
        public static DataTable GetClaimDocumentsByDocumentId(int pkiClaimPictureID, Guid Parlourid)
        {
            string query = "GetClaimDocumentsByClaimPictureID";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@pkiClaimPictureID", DbParameter.DbType.Int, 0, pkiClaimPictureID);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, @Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, query, ObjParam);
        }
        public static DataTable GetUploadedDocumentList(int pkiClaimID, Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[1] = new DbParameter("@pkiClaimID", DbParameter.DbType.Int, 0, pkiClaimID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetUploadedDocumentList", ObjParam);
        }

        public static int AddClaimFollowUp(ClaimFollowUp claimFollow)
        {
            string query = "AddClaimFollowUp";
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@ClaimId", DbParameter.DbType.Int, 0, claimFollow.ClaimId);
            ObjParam[1] = new DbParameter("@Comment", DbParameter.DbType.NVarChar, 0, claimFollow.Comment);
            ObjParam[2] = new DbParameter("@FollowUpType", DbParameter.DbType.NVarChar, 0, claimFollow.FollowUpType);
            ObjParam[3] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, claimFollow.ParlourId);
            ObjParam[4] = new DbParameter("@CreatedBy", DbParameter.DbType.NVarChar, 0, claimFollow.CreatedBy);
            ObjParam[5] = new DbParameter("@pkiClaimPictureID", DbParameter.DbType.Int, 0, claimFollow.pkiClaimPictureID);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static void UpdateClaimDocument(ChangeStatusReason model)
        {
            string query = "UpdateDocumentStatus";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@pkiClaimPictureID", DbParameter.DbType.Int, 0, model.ID);
            ObjParam[1] = new DbParameter("@ChangedBy", DbParameter.DbType.NVarChar, 0, model.ChangedBy);
            ObjParam[2] = new DbParameter("@NewStatus", DbParameter.DbType.NVarChar, 0, model.NewStatus);
            DbConnection.ExecuteNonQuery(CommandType.StoredProcedure, query, ObjParam);
        }
        public static void SaveClaimHistory(ClaimHistory model)
        {
            string query = "SaveClaimHistory";
            DbParameter[] ObjParam = new DbParameter[5];
            ObjParam[0] = new DbParameter("@FkiClaimId", DbParameter.DbType.Int, 0, model.FkiClaimId);
            ObjParam[1] = new DbParameter("@IPAddress", DbParameter.DbType.NVarChar, 0, model.IPAddress);
            ObjParam[2] = new DbParameter("@Note", DbParameter.DbType.NVarChar, 0, model.Note);
            ObjParam[3] = new DbParameter("@CreatedBy", DbParameter.DbType.NVarChar, 0, model.CreatedBy);
            ObjParam[4] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, model.ParlourId);
            DbConnection.ExecuteNonQuery(CommandType.StoredProcedure, query, ObjParam);
        }
        public static DataTable GetClaimReasonList(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetClaimReasonList", ObjParam);
        }
        public static DataTable GetDocumentFollowUpHistory(int pkiClaimPictureID, Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[1] = new DbParameter("@pkiClaimPictureID", DbParameter.DbType.Int, 0, pkiClaimPictureID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetDcoumentFollowUpHistory", ObjParam);
        } 
        public static int ChangeClaimAssigned(ClaimAssigned claimAssigned)
        {
            string query = "ChangeClaimAssigned";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@AssignedTo", DbParameter.DbType.Int, 0, claimAssigned.NewAssigned);
            ObjParam[1] = new DbParameter("@pkiClaimID", DbParameter.DbType.Int, 0, claimAssigned.ClaimId);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static int SaveExternalLink(ExternalUserLink external)
        {
            string query = "SaveExternalUserLink";
            DbParameter[] ObjParam = new DbParameter[5];
            ObjParam[0] = new DbParameter("@ClaimId", DbParameter.DbType.Int, 0, external.ClaimId);
            ObjParam[1] = new DbParameter("@Email", DbParameter.DbType.NVarChar, 0, external.Email);
            ObjParam[2] = new DbParameter("@ExternalToken", DbParameter.DbType.UniqueIdentifier, 0, external.ExternalToken);
            ObjParam[3] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, external.parlourId);
            ObjParam[4] = new DbParameter("@SentBy", DbParameter.DbType.NVarChar, 0, external.SentBy);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static int UpdateExternalLink(Guid ExternalToken,bool TokenAccess)
        {
            string query = "UpdateExternalUserLink";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ExternalToken", DbParameter.DbType.UniqueIdentifier, 0, ExternalToken);
            ObjParam[1] = new DbParameter("@TokenAccess", DbParameter.DbType.Bit, 0, TokenAccess);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static DataTable GetExternalLink(Guid ExternalToken)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ExternalToken", DbParameter.DbType.UniqueIdentifier, 0, ExternalToken);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetExternalUserLink", ObjParam);
        }
    }
}
