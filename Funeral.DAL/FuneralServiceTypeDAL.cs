using Funeral.Model;
using System;
using System.Data;

namespace Funeral.DAL
{
    public class FuneralServiceTypeDAL
    {
        public static int Save(FuneralServiceTypeVm model)
        {
            string query = "FuneralServiceTypeSave";

            DbParameter[] ObjParam = new DbParameter[3];

            ObjParam[0] = new DbParameter("@Id", DbParameter.DbType.Int, 0, model.Id);
            ObjParam[1] = new DbParameter("@FuneralServiceType", DbParameter.DbType.VarChar, 0, model.FuneralServiceType);
            ObjParam[2] = new DbParameter("@IsActive", DbParameter.DbType.Bit, 0, model.IsActive);
            
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }

        public static DataTable SelectById(int Id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@Id", DbParameter.DbType.Int, 0, Id);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "FuneralServiceTypeSelect", ObjParam));
        }

        public static DataTable SelectAll()
        { 
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "FuneralServiceTypeSelectAll"));
        }
       
    }
}
