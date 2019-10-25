using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.DAL
{
    public class BanksDAL
    {
        public BanksDAL()
        {
        }

        /// <summary>
        /// Select All Bank
        /// </summary>
        /// <returns></returns>
        public static SqlDataReader SelectAll()
        {
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "BanksSelectAll");
        }

        /// <summary>
        /// Select All Bank
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectAlldt()
        {
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "BanksSelectAll");
        }

        /// <summary>
        /// Select All Bank
        /// </summary>
        /// <returns></returns>
        public static SqlDataReader AccountTypeSelectAll()
        {
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "AccoutnTypeSelectAll");
        }

        /// <summary>
        /// get All Acount Select All dt
        /// </summary>
        /// <returns></returns>
        public static DataTable AccountTypeSelectAlldt()
        {
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "AccoutnTypeSelectAll");
        }
    }
}
