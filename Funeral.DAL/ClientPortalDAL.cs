using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.DAL
{
    public class ClientPortalDAL
    {
        public static DataSet GetAllMembersdt(string IDNumber, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, string status)
        {
            DbParameter[] ObjParam = new DbParameter[7];
            try
            {
                ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
                ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
                ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, (Keyword == null) ? string.Empty : Keyword);
                ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
                ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
                ObjParam[5] = new DbParameter("@IDNumber", DbParameter.DbType.NVarChar, 0, IDNumber);
                ObjParam[6] = new DbParameter("@Status", DbParameter.DbType.VarChar, 0, status);
                return DbConnection.GetDataSet(CommandType.StoredProcedure, "getMyPoliceByIDnumber", ObjParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
