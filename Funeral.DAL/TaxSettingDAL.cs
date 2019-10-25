using System.Data;
using Funeral.Model;
using System;

namespace Funeral.DAL
{
    public class TaxSettingDAL
    {
        public static DataTable tblRightGetAlldt()
        {
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetTAX_Setting");
        }

        public static int InsertRecordForTax(TaxSetting modelTax)
        {
            try
            {
                DbParameter[] ObjParam = new DbParameter[2];
                ObjParam[0] = new DbParameter("@TaxText", DbParameter.DbType.NVarChar, 0, modelTax.TaxText);
                ObjParam[1] = new DbParameter("@TaxValue", DbParameter.DbType.Decimal, 0, modelTax.TaxValue);
                DbConnection.GetScalarValue(CommandType.StoredProcedure, "InsertRecordForTax", ObjParam);
                return 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
        public static DataTable GetTaxByIddt(int id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@id", DbParameter.DbType.Int, 0, id);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectTaxSettingById", ObjParam);
        }

        public static int DeleteTaxSettingById(int id)
        {
            //string query = "DeleteForTaxById";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@id", DbParameter.DbType.Int, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteForTaxById", ObjParam));
        }
        public static int UpdateRecordForTax(TaxSetting modelTax)
        {
            try
            {
                DbParameter[] ObjParam = new DbParameter[3];
                ObjParam[0] = new DbParameter("@id", DbParameter.DbType.Int, 0, modelTax.Id);
                ObjParam[1] = new DbParameter("@TaxText", DbParameter.DbType.NVarChar, 0, modelTax.TaxText);
                ObjParam[2] = new DbParameter("@TaxValue", DbParameter.DbType.Decimal, 0, modelTax.TaxValue);
                DbConnection.GetScalarValue(CommandType.StoredProcedure, "UpdateRecordForTax", ObjParam);
                return 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
    }
}
