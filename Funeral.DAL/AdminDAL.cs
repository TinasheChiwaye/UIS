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
    }
}
