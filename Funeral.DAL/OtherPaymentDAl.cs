using Funeral.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Funeral.DAL
{
    /// <summary>
    /// Other Payment Class Details
    /// </summary>
    public class OtherPaymentDAl
    {
        public static int OthePaymentDetailsSave(OtherPaymentModel model)
        {
            try
            {
                DbParameter[] ObjParam = new DbParameter[16];
                ObjParam[0] = new DbParameter("@pkiInvoiceID", DbParameter.DbType.Int, 0, model.pkiInvoiceID);
                ObjParam[1] = new DbParameter("@MemberID", DbParameter.DbType.Int, 0, model.MemberID);
                ObjParam[2] = new DbParameter("@DatePaid", DbParameter.DbType.DateTime, 0, model.DatePaid);
                ObjParam[3] = new DbParameter("@AmountPaid", DbParameter.DbType.Money, 0, model.AmountPaid);
                ObjParam[4] = new DbParameter("@RecievedBy", DbParameter.DbType.NVarChar, 0, model.RecievedBy);
                ObjParam[5] = new DbParameter("@PaidBy", DbParameter.DbType.NVarChar, 0, model.PaidBy);
                ObjParam[6] = new DbParameter("@Notes", DbParameter.DbType.NVarChar, 0, model.Notes);
                ObjParam[7] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.Parlourid);
                ObjParam[8] = new DbParameter("@PaymentBranch", DbParameter.DbType.VarChar, 0, model.PaymentBranch);
                ObjParam[9] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, DateTime.Now);
                ObjParam[10] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, model.ModifiedUser);
                ObjParam[11] = new DbParameter("@InvNumber", DbParameter.DbType.Int, 0, model.InvNumber);
                ObjParam[12] = new DbParameter("@DeviceCollectionID", DbParameter.DbType.UniqueIdentifier, 0, model.DeviceCollectionID);
                ObjParam[13] = new DbParameter("@MethodOfPayment", DbParameter.DbType.NVarChar, 0, model.MethodOfPayment);
                ObjParam[14] = new DbParameter("@Discount", DbParameter.DbType.NVarChar, 0, model.Discount);
                ObjParam[15] = new DbParameter("@PaymentTypeId", DbParameter.DbType.NVarChar, 0, model.PaymentTypeId);

                return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "OtherPaymentDetailsSave", ObjParam));
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public static SqlDataReader OtherPaymentSelectByMemberId(int MemberId, Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[1] = new DbParameter("@MemberID", DbParameter.DbType.Int, 0, MemberId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "OtherInvoiceSelectByMemberId", ObjParam);
        }

        public static DataTable OtherPaymentSelectByMemberIddt(int MemberId, Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[1] = new DbParameter("@MemberID", DbParameter.DbType.Int, 0, MemberId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "OtherInvoiceSelectByMemberId", ObjParam);
        }


        /// <summary>
        /// Select OtherPayment by id
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <param name="Parlourid"></param>
        /// <returns></returns>
        public static SqlDataReader OtherPaymentSelect(int InvoiceId, Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[1] = new DbParameter("@InvoiceId", DbParameter.DbType.Int, 0, InvoiceId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "OtherInvoiceSelect", ObjParam);
        }

        public static DataTable OtherPaymentSelectdt(int InvoiceId, Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[1] = new DbParameter("@InvoiceId", DbParameter.DbType.Int, 0, InvoiceId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "OtherInvoiceSelect", ObjParam);
        }
        //public static SqlDataReader OtherPaymentGetBypkInvoiceId(int pkiInvoiceID, Guid Parlourid)
        //{
        //    DbParameter[] ObjParam = new DbParameter[2];
        //    ObjParam[0] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
        //    ObjParam[1] = new DbParameter("@pkiInvoiceID", DbParameter.DbType.Int, 0, pkiInvoiceID);
        //    return DbConnection.GetDataReader(CommandType.StoredProcedure, "OtherInvoiceSelect", ObjParam);
        //}
        public static SqlDataReader OtherPaymentDetailGetId(int Id)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@Id", DbParameter.DbType.Int, 0, Id);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectPaymentType", ObjParam);
        }
        public static int AddEditGroupPayment(GroupPayment model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "AddEditGroupPayment";
            DbParameter[] ObjParam = new DbParameter[11];
            ObjParam[0] = new DbParameter("@GroupInvoiceID", DbParameter.DbType.Int, 0, model.GroupInvoiceID);
            ObjParam[1] = new DbParameter("@PaymentMethod", DbParameter.DbType.NVarChar, 0, model.PaymentMethod);
            ObjParam[2] = new DbParameter("@SocietyId", DbParameter.DbType.Int, 0, model.SocietyId);
            ObjParam[3] = new DbParameter("@PaidBy", DbParameter.DbType.NVarChar, 0, model.PaidBy);
            ObjParam[4] = new DbParameter("@RecievedBy", DbParameter.DbType.NVarChar, 0, model.RecievedBy);
            ObjParam[5] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[6] = new DbParameter("@Notes", DbParameter.DbType.NVarChar, 0, model.Notes == null ? "" : model.Notes);
            ObjParam[7] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[8] = new DbParameter("@AmountPaid", DbParameter.DbType.Money, 0, model.AmountPaid);
            ObjParam[9] = new DbParameter("@DatePaid", DbParameter.DbType.DateTime, 0, model.DatePaid);
            ObjParam[10] = new DbParameter("@ReferenceNumber", DbParameter.DbType.NVarChar, 0, model.ReferenceNumber);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static DataTable GetAllGroupPaymentList(Guid ParlourId, int GroupId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@GroupId", DbParameter.DbType.Int, 0, GroupId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllGroupPaymentList", ObjParam);
        }
        public static DataTable EditGroupPaymentByID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "EditGroupPaymentByID", ObjParam);
        }
        public static int DeleteGroupPayment(int id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@GroupInvoiceID", DbParameter.DbType.Int, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteGroupPayment", ObjParam));
        }
    }
}






