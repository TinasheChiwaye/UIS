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
    public class TombStonesPaymentDAL
    {
        public static int AddTombStonesPayment(TombStonesPaymentModel model)
        {
            decimal amountPaid = model.AmountPaid.ToString().Contains(",") == true ? Convert.ToDecimal((model.AmountPaid / 100).ToString().Replace(",", ".")) : model.AmountPaid;
            DbParameter[] ObjParam = new DbParameter[9];
            ObjParam[0] = new DbParameter("@InvoiceID", DbParameter.DbType.Int, 0, model.InvoiceID);//pIntFnrlID
            ObjParam[1] = new DbParameter("@TombstoneID", DbParameter.DbType.Int, 0, model.TombstoneID);//pIntFnrlID
           // ObjParam[2] = new DbParameter("@DatePaid", DbParameter.DbType.DateTime, 0, model.DatePaid);//pIntFnrlID
            ObjParam[2] = new DbParameter("@AmountPaid", DbParameter.DbType.Decimal, 0, amountPaid);
            ObjParam[3] = new DbParameter("@RecievedBy", DbParameter.DbType.VarChar, 0, model.RecievedBy);
            ObjParam[4] = new DbParameter("@PaidBy", DbParameter.DbType.VarChar, 0, model.PaidBy);
            ObjParam[5] = new DbParameter("@Notes", DbParameter.DbType.VarChar, 0, model.Notes);
            ObjParam[6] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[7] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, model.ModifiedUser);
            ObjParam[8] = new DbParameter("@MemberBranch", DbParameter.DbType.VarChar, 0, model.MemberBranch);


            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "TombStonesPaymentSave", ObjParam));

        }

        public static SqlDataReader TombStonesPaymentSelect(Guid parlourid, int invoiceId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@InvoiceID", DbParameter.DbType.Int, 0, invoiceId);//pIntFnrlID
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);//pIntFnrlID
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "TombStonesPaymentSelect", ObjParam);
        }
        public static DataTable  TombStonesPaymentSelectdt(Guid parlourid, int invoiceId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@InvoiceID", DbParameter.DbType.Int, 0, invoiceId);//pIntFnrlID
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);//pIntFnrlID
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "TombStonesPaymentSelect", ObjParam);
        }
        public static SqlDataReader TombStonesPaymentSelectByTombstoneID(Guid parlourid, int tombstoneID)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@TombstoneID", DbParameter.DbType.Int, 0, tombstoneID);//pIntFnrlID
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);//pIntFnrlID
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "TombStonesPaymentSelectByTId", ObjParam);
        }

        public static DataTable TombStonesPaymentSelectByTombstoneIDdt(Guid parlourid, int tombstoneID)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@TombstoneID", DbParameter.DbType.Int, 0, tombstoneID);//pIntFnrlID
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);//pIntFnrlID
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "TombStonesPaymentSelectByTId", ObjParam);
        }
    }
}
