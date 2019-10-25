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
   public  class UnderwriterPremiumDAL
    {
        private UnderwriterPremiumDAL() { }
        public static SqlDataReader SelectAllUnderwriterPremiumByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
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
                return (DbConnection.GetDataReader(CommandType.StoredProcedure, "UnderWriterPremiumSelectAllByParlourId", ObjParam));

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataTable SelectAllUnderwriterPremiumByParlourIddt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
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
                return (DbConnection.GetDataTable(CommandType.StoredProcedure, "UnderWriterPremiumSelectAllByParlourId", ObjParam));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int SaveUnderwriterPremium(UnderwriterPremiumModel model)
        {
            try
            {
                string query = "UnderWriterPremiumSave";
                DbParameter[] ObjParam = new DbParameter[18];

                ObjParam[0] = new DbParameter("@PkiUnderwriterPremiumId", DbParameter.DbType.Int, 0, model.PkiUnderwriterPremiumId);
                ObjParam[1] = new DbParameter("@FkiUnderwriterId", DbParameter.DbType.Int, 0, model.FkiUnderwriterId);

                ObjParam[2] = new DbParameter("@PlanID", DbParameter.DbType.Int, 0, model.PlanId);
                ObjParam[3] = new DbParameter("@RolePlayerType", DbParameter.DbType.NVarChar, 0, model.RolePlayerType);

                ObjParam[4] = new DbParameter("@PremiumAmount", DbParameter.DbType.NVarChar, 0, model.PremiumAmount);
                ObjParam[5] = new DbParameter("@CoverAmount", DbParameter.DbType.NVarChar, 0, model.CoverAmount);
                ObjParam[6] = new DbParameter("@CoverAgeFrom", DbParameter.DbType.Int, 0, model.CoverAgeFrom);
                ObjParam[7] = new DbParameter("@CoverAgeTo", DbParameter.DbType.Int, 0, model.CoverAgeTo);
                ObjParam[8] = new DbParameter("@ApplysToDependents", DbParameter.DbType.Bit, 0,model.ApplysToDependents);
               
                ObjParam[9] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.Parlourid);
               
                ObjParam[10] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
                ObjParam[11] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
                ObjParam[12] = new DbParameter("@CreatedDate", DbParameter.DbType.DateTime, 0, model.CreatedDate);
                ObjParam[13] = new DbParameter("@CreatedUser", DbParameter.DbType.NVarChar, 0, model.CreatedUser);
                ObjParam[14] = new DbParameter("@CoverAgeFromType", DbParameter.DbType.NVarChar, 0, model.CoverAgeFromType);
                ObjParam[15] = new DbParameter("@CoverAgeToType", DbParameter.DbType.NVarChar, 0, model.CoverAgeToType);
                ObjParam[16] = new DbParameter("@UnderwriterPremium", DbParameter.DbType.NVarChar, 0, model.UnderwriterPremium);
                ObjParam[17] = new DbParameter("@UnderwriterCover", DbParameter.DbType.NVarChar, 0, model.UnderwriterCover);

                return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static SqlDataReader SelectUnderwriterPremiumBypkid(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@PkiUnderwriterPremiumId", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectUnderwriterPremiumBypkid", ObjParam);
        }
        public static DataTable SelectUnderwriterPremiumBypkiddt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@PkiUnderwriterPremiumId", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectUnderwriterPremiumBypkid", ObjParam);
        }
        public static int DeleteUnderwriterPremiumByID(int PkiUnderwriterId, string UserName)
        {
            string query = "update [dbo].[UnderwriterPlanPremiums] set IsDeleted = 1, DeletedDate=@DateTime, DeletedBy=@UserName where PkiUnderwriterId=@PkiUnderwriterId";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@PkiUnderwriterPremiumId", DbParameter.DbType.Int, 0, PkiUnderwriterId);
            ObjParam[1] = new DbParameter("@UserName", DbParameter.DbType.NVarChar, 0, UserName);
            ObjParam[2] = new DbParameter("@DateTime", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }

        public static SqlDataReader SelectUnderwriterPremiumByName(int FkiUnderwriterId, Guid ParlourId)
        {
            string query = "select * from [dbo].[UnderwriterPlanPremiums] where FkiUnderwriterId = @FkiUnderwriterId and Parlourid = @Parlourid";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@FkiUnderwriterId", DbParameter.DbType.NVarChar, 0, FkiUnderwriterId);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataReader(CommandType.Text, query, ObjParam);
        }
        public static DataTable SelectUnderwriterPremiumByNamedt(int FkiUnderwriterId, Guid ParlourId)
        {
            string query = "select * from [dbo].[UnderwriterPlanPremiums] where FkiUnderwriterId = @FkiUnderwriterId and Parlourid = @Parlourid";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@FkiUnderwriterId", DbParameter.DbType.NVarChar, 0, FkiUnderwriterId);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataTable(CommandType.Text, query, ObjParam);
        }
        public static SqlDataReader SelectUnderwriterPremiumNotDeleted()
        {
            string query = "select * from [dbo].[UnderwriterPlanPremiums] where IsDeleted is null";

            return DbConnection.GetDataReader(CommandType.Text, query);
        }
        public static DataTable SelectUnderwriterPremiumNotDeleteddt()
        {
            string query = "select * from [dbo].[UnderwriterPlanPremiums] where IsDeleted is null";

            return DbConnection.GetDataTable(CommandType.Text, query);
        }

        public static SqlDataReader GetUnderwriterSetupNameByParlourId(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "UnderwriterSetupUnderwriterNameByparlourId", ObjParam);
        }
        public static DataTable GetUnderwriterSetupNameByParlourIddt(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "UnderwriterSetupUnderwriterNameByparlourId", ObjParam);
        }

        public static SqlDataReader EditUnderwriterPremiumbyID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "UnderWriterPremiumSelect", ObjParam);
        }
        public static DataTable EditUnderwriterPremiumbyIDdt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "UnderWriterPremiumSelect", ObjParam);
        }

        public static int DeleteUnderwriterPremium(int id,string UserName)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@PkiUnderwriterPremiumId", DbParameter.DbType.Int, 0, id);
            ObjParam[1] = new DbParameter("@DeletedBy", DbParameter.DbType.NVarChar, 0, UserName);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "UnderWriterPremiumDelete", ObjParam));
        }

        
    }
}
