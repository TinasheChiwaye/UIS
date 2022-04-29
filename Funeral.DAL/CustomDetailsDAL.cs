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
    public class CustomDetailsDAL
    {
        public static int CustomDetailsSave(CustomDetails model)
        {
            DbParameter[] ObjParam = new DbParameter[5];
            ObjParam[0] = new DbParameter("@Name", DbParameter.DbType.VarChar, 0, model.Name);
            ObjParam[1] = new DbParameter("@Description", DbParameter.DbType.VarChar, 0, model.Description);
            ObjParam[2] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, model.ParlourId);
            ObjParam[3] = new DbParameter("@CreatedBy", DbParameter.DbType.Int, 0, model.CreatedBy);
            ObjParam[4] = new DbParameter("@CustomType", DbParameter.DbType.Int, 0, (int)model.CustomType);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "CustomDetailsSave", ObjParam));
        }

        public static void CustomDetailsUpdate(CustomDetails model)
        {
            try
            {
                DbParameter[] ObjParam = new DbParameter[6];
                ObjParam[0] = new DbParameter("@Id", DbParameter.DbType.Int, 0, model.Id);
                ObjParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 0, model.Name);
                ObjParam[2] = new DbParameter("@Description", DbParameter.DbType.VarChar, 0, model.Description);
                ObjParam[3] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, model.ParlourId);
                ObjParam[4] = new DbParameter("@CreatedBy", DbParameter.DbType.Int, 0, model.CreatedBy);
                ObjParam[5] = new DbParameter("@CustomType", DbParameter.DbType.Int, 0, (int)model.CustomType);
                DbConnection.ExecuteNonQuery(CommandType.StoredProcedure, "CustomDetailsUpdate", ObjParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CustomDetailsDelete(CustomDetails model)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@Id", DbParameter.DbType.Int, 0, model.Id);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, model.ParlourId);
            ObjParam[2] = new DbParameter("@CustomType", DbParameter.DbType.Int, 0, (int)model.CustomType);

            DbConnection.ExecuteNonQuery(CommandType.StoredProcedure, "CustomDetailsDelete", ObjParam);
        }

        public static SqlDataReader GetCustomDetails(int Id, Guid ParlourId, int CustomType)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@Id", DbParameter.DbType.Int, 0, Id);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[2] = new DbParameter("@CustomType", DbParameter.DbType.Int, 0, CustomType);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "CustomDetailsSelect", ObjParam);
        }

        public static DataTable GetCustomDetailsdt(int Id, Guid ParlourId, int CustomType)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@Id", DbParameter.DbType.Int, 0, Id);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[2] = new DbParameter("@CustomType", DbParameter.DbType.Int, 0, CustomType);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "CustomDetailsSelect", ObjParam);
        }

        public static SqlDataReader GetAllCustomDetailsByParlourId(Guid ParlourId, int CustomType)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@CustomType", DbParameter.DbType.Int, 0, CustomType);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "CustomDetailsSelectAllByParlourId", ObjParam);
        }

        public static DataTable GetAllCustomDetailsByParlourIddt(Guid ParlourId, int CustomType)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@CustomType", DbParameter.DbType.Int, 0, CustomType);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "CustomDetailsSelectAllByParlourId", ObjParam);
        }
    }
}
