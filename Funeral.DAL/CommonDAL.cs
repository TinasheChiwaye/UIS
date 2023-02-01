using Funeral.Model;
using System;
using System.Data;

namespace Funeral.DAL
{
    public class CommonDAL
    {
        private CommonDAL() { }

        /// <summary>
        /// Get all status by Associated Table
        /// </summary>
        /// <param name="associatedTable"></param>
        /// <returns></returns>
        public static DataTable GetStatus(string associatedTable)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@AssociatedTable", DbParameter.DbType.VarChar, 0, associatedTable.ToString());
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "StatusSelectByAssociatedTable", ObjParam);
        }
        public static int SMSTopup_save(ConsumableOrders ModalProduct)
        {
            DbParameter[] ObjParam = new DbParameter[6];
            //ObjParam[0] = new DbParameter("@OrderID", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.OrderID);
            ObjParam[0] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, ModalProduct.LastModified);
            ObjParam[1] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, ModalProduct.ModifiedUser);
            ObjParam[2] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.Parlourid);
            ObjParam[3] = new DbParameter("@SMSQyt", DbParameter.DbType.NVarChar, 0, ModalProduct.SMSQyt);
            ObjParam[4] = new DbParameter("@DateCreated", DbParameter.DbType.DateTime, 0, ModalProduct.DateCreated);
            ObjParam[5] = new DbParameter("@UserID", DbParameter.DbType.VarChar, 0, ModalProduct.ModifiedUser);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "AddSMSQTY", ObjParam));
        }
        public static void SendOutstandingMessagesToAll(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            DbConnection.GetDataTable(CommandType.StoredProcedure, "SendsmsAllMembersOutstanding", ObjParam);
        }
        public static DataTable GetBankList()
        {
            string sp = "GetBankList";
            return DbConnection.GetDataTable(CommandType.StoredProcedure, sp);
        }
        public static DataTable GetProvinces()
        {
            string sp = "GetProvincesList";
            return DbConnection.GetDataTable(CommandType.StoredProcedure, sp);
        }
        //GetAccountTypeList
        public static DataTable GetAccountTypeList()
        {
            string sp = "GetAccountTypeList";
            return DbConnection.GetDataTable(CommandType.StoredProcedure, sp);
        }
        public static DataTable GetDocumentList(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetDocumentList_ByParlourId", ObjParam);
        }
        public static DataTable GetAllUser(Guid parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "getAllSecureUser", ObjParam);
        }
        public static DataTable GetBankDetails_ByParlourId(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetBanksByParlourId", ObjParam);
        }
        public static DataTable GetClaimReasonByClaimStatus(string ClaimStatus, Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ClaimStatus", DbParameter.DbType.NVarChar, 0, ClaimStatus);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetClaimReasonByClaimStatus", ObjParam);
        }
        public static int AddAudit(string strUserName, Guid pgParlourID, string actionDesc)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@strUserName", DbParameter.DbType.VarChar, 0, strUserName);
            ObjParam[1] = new DbParameter("@pgParlourID", DbParameter.DbType.UniqueIdentifier, 0, pgParlourID);
            ObjParam[2] = new DbParameter("@actionDesc", DbParameter.DbType.VarChar, 0, actionDesc);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "AddAudit", ObjParam));
        }

        public static int SendReportParameters(string DateFrom, string DateTo, string Branch, string Society, string Agent, string Underwriter, 
            string PaymentType, string PolicyStatus, string Custom1, string Custom2, string Custom3, Guid ParlourId, string Email, string ReportName)
        {
            DbParameter[] ObjParam = new DbParameter[14];
            ObjParam[0] = new DbParameter("@DateFrom", DbParameter.DbType.VarChar, 0, DateFrom);
            ObjParam[1] = new DbParameter("@DateTo", DbParameter.DbType.VarChar, 0, DateTo);
            ObjParam[2] = new DbParameter("@Branch", DbParameter.DbType.VarChar, 0, Branch);
            ObjParam[3] = new DbParameter("@Society", DbParameter.DbType.VarChar, 0, Society);
            ObjParam[4] = new DbParameter("@Agent", DbParameter.DbType.VarChar, 0, Agent);
            ObjParam[5] = new DbParameter("@Underwritter", DbParameter.DbType.VarChar, 0, Underwriter);
            ObjParam[6] = new DbParameter("@PaymentType", DbParameter.DbType.VarChar, 0, PaymentType);
            ObjParam[7] = new DbParameter("@PolicyStatus", DbParameter.DbType.VarChar, 0, PolicyStatus);
            ObjParam[8] = new DbParameter("@CustomId1", DbParameter.DbType.VarChar, 0, Custom1);
            ObjParam[9] = new DbParameter("@CustomId2", DbParameter.DbType.VarChar, 0, Custom2);
            ObjParam[10] = new DbParameter("@CustomId3", DbParameter.DbType.VarChar, 0, Custom3);
            ObjParam[11] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[12] = new DbParameter("@Email", DbParameter.DbType.VarChar, 0, Email);
            ObjParam[13] = new DbParameter("@ReportName", DbParameter.DbType.VarChar, 0, ReportName);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "ExecuteReport", ObjParam));
        }
    }
}
