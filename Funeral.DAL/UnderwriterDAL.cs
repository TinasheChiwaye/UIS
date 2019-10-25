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
    public sealed class UnderwriterDAL
    {
        private UnderwriterDAL() { }
        public static SqlDataReader SelectAllUnderwriterByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            try
            {
                DbParameter[] ObjParam = new DbParameter[6];
                ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
                ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
                ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
                ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
                ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
                ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
                return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectAllUnderwriterByParlourId", ObjParam));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataTable SelectAllUnderwriterByParlourIddt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            try
            {
                DbParameter[] ObjParam = new DbParameter[6];
                ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
                ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
                ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
                ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
                ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
                ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
                return (DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectAllUnderwriterByParlourId", ObjParam));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int SaveUnderwriter(UnderwriterModel model)
        {
            try
            {
                string query = "SaveUnderwriter";
                DbParameter[] ObjParam = new DbParameter[25];

                ObjParam[0] = new DbParameter("@PkiUnderwriterId", DbParameter.DbType.Int, 0, model.PkiUnderwriterId);
                ObjParam[1] = new DbParameter("@UnderwriterName", DbParameter.DbType.NVarChar, 0, model.UnderwriterName);
                ObjParam[2] = new DbParameter("@PlanName", DbParameter.DbType.NVarChar, 0, model.PlanName);
                ObjParam[3] = new DbParameter("@Premium", DbParameter.DbType.NVarChar, 0, model.Premium);
                ObjParam[4] = new DbParameter("@CoverAmount", DbParameter.DbType.NVarChar, 0, model.CoverAmount);
                ObjParam[5] = new DbParameter("@Spouse", DbParameter.DbType.Int, 0, model.Spouse);
                ObjParam[6] = new DbParameter("@Children", DbParameter.DbType.Int, 0, model.Children);
                ObjParam[7] = new DbParameter("@Adults", DbParameter.DbType.Int, 0, model.Adults);
                ObjParam[8] = new DbParameter("@Cover", DbParameter.DbType.Decimal, 0, model.Cover);
                ObjParam[9] = new DbParameter("@SpouseCover", DbParameter.DbType.Decimal, 0, model.SpouseCover);
                ObjParam[10] = new DbParameter("@ChildCover", DbParameter.DbType.Decimal, 0, model.ChildCover);
                ObjParam[11] = new DbParameter("@AdultCover", DbParameter.DbType.Decimal, 0, model.AdultCover);
                ObjParam[12] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.Parlourid);
                ObjParam[13] = new DbParameter("@PlanUnderwriter", DbParameter.DbType.NVarChar, 0, model.PlanUnderwriter);
                ObjParam[14] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
                ObjParam[15] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
                ObjParam[16] = new DbParameter("@CreateddDate", DbParameter.DbType.DateTime, 0, model.CreateddDate);
                ObjParam[17] = new DbParameter("@CreatedUser", DbParameter.DbType.NVarChar, 0, model.CreatedUser);
                ObjParam[18] = new DbParameter("@Cover0to5year", DbParameter.DbType.Decimal, 0, model.Cover0to5year);
                ObjParam[19] = new DbParameter("@Cover6to13year", DbParameter.DbType.Decimal, 0, model.Cover6to13year);
                ObjParam[20] = new DbParameter("@Cover14to21year", DbParameter.DbType.Decimal, 0, model.Cover14to21year);
                ObjParam[21] = new DbParameter("@Cover22to40year", DbParameter.DbType.Decimal, 0, model.Cover22to40year);
                ObjParam[22] = new DbParameter("@Cover41to59year", DbParameter.DbType.Decimal, 0, model.Cover41to59year);
                ObjParam[23] = new DbParameter("@Cover60to65year", DbParameter.DbType.Decimal, 0, model.Cover60to65year);
                ObjParam[24] = new DbParameter("@Cover66to75year", DbParameter.DbType.Decimal, 0, model.Cover66to75year);

                return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static SqlDataReader SelectUnderwriterBypkid(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@PkiUnderwriterId", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectUnderwriterBypkid", ObjParam);
        }
        public static DataTable  SelectUnderwriterBypkiddt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@PkiUnderwriterId", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectUnderwriterBypkid", ObjParam);
        }
        public static int DeleteUnderwriterByID(int PkiUnderwriterId, string UserName)
        {
            string query = "update [dbo].[Underwriter] set IsDeleted = 1, DeletedDate=@DateTime, DeletedBy=@UserName where PkiUnderwriterId=@PkiUnderwriterId";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@PkiUnderwriterId", DbParameter.DbType.Int, 0, PkiUnderwriterId);
            ObjParam[1] = new DbParameter("@UserName", DbParameter.DbType.NVarChar, 0, UserName);
            ObjParam[2] = new DbParameter("@DateTime", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }
        
        public static SqlDataReader SelectUnderwriterByName(string UnderwriterName, Guid ParlourId)
        {
            string query = "select * from [dbo].[Underwriter] where UnderwriterName = @UnderwriterName and Parlourid = @Parlourid";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@UnderwriterName", DbParameter.DbType.NVarChar, 0, UnderwriterName);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataReader(CommandType.Text, query, ObjParam);
        }
        public static DataTable SelectUnderwriterByNamedt(string UnderwriterName, Guid ParlourId)
        {
            string query = "select * from [dbo].[Underwriter] where UnderwriterName = @UnderwriterName and Parlourid = @Parlourid";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@UnderwriterName", DbParameter.DbType.NVarChar, 0, UnderwriterName);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataTable(CommandType.Text, query, ObjParam);
        }
        public static SqlDataReader SelectUnderwriterNotDeleted()
        {
            string query = "select * from [dbo].[Underwriter] where IsDeleted is null";
            
            return DbConnection.GetDataReader(CommandType.Text, query);
        }
        public static DataTable SelectUnderwriterNotDeleteddt()
        {
            string query = "select * from [dbo].[Underwriter] where IsDeleted is null";

            return DbConnection.GetDataTable(CommandType.Text, query);
        }
    }
}
