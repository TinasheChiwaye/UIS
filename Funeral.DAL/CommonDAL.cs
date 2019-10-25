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
    }
}
