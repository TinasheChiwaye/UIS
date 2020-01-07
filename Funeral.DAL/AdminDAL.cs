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
    public class AdminDAL
    {

        /// <summary>
        /// Do Admin Login
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static SqlDataReader AdminLogin(string Email, string Password)
        {
            string query = "AdminLogin";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@email", DbParameter.DbType.NVarChar, 0, Email);
            ObjParam[1] = new DbParameter("@password", DbParameter.DbType.NVarChar, 0, Password);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, query, ObjParam);
        }

        public static DataTable AdminLogindt(string Email, string Password)
        {
            string query = "AdminLogin";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@email", DbParameter.DbType.NVarChar, 0, Email);
            ObjParam[1] = new DbParameter("@password", DbParameter.DbType.NVarChar, 0, Password);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, query, ObjParam);
        }

        /// <summary>
        /// Do Admin Login
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static DataSet AdminLoginDS(string Email, string Password)
        {
            string query = "AdminLogin";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@email", DbParameter.DbType.NVarChar, 0, Email);
            ObjParam[1] = new DbParameter("@password", DbParameter.DbType.NVarChar, 0, Password);
            return DbConnection.GetDataSet(CommandType.StoredProcedure, query, ObjParam);
        }
        public static int UpdateAdminLoginPassword(int UserID, Guid parlourId, string password)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 0, UserID);
            ObjParam[1] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourId);
            ObjParam[2] = new DbParameter("@password", DbParameter.DbType.VarChar, 0, password);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "UpdateAdminLoginPassword", ObjParam));
        }
        public static int AddUpdateTo_ForgotPassword(int ForgotPassId, int UserID, Guid parlourId, string password)
        {
            DbParameter[] ObjParam = new DbParameter[4];
            ObjParam[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 0, UserID);
            ObjParam[1] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourId);
            ObjParam[2] = new DbParameter("@password", DbParameter.DbType.VarChar, 0, password);
            ObjParam[3] = new DbParameter("@ForgotPassId", DbParameter.DbType.Int, 0, ForgotPassId);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "AddUpdate_ForgotPassword", ObjParam));
        }
        public static DataTable GetForgotPasswordDetails(int ForgotId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ForgotId", DbParameter.DbType.Int, 0, ForgotId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetForgotPasswordDetails", ObjParam);
        }
        public static DataTable AdminLogindt(string Email)
        {
            string query = "GetAdminByEmail";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@email", DbParameter.DbType.NVarChar, 0, Email);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, query, ObjParam);
        }
    }
}
