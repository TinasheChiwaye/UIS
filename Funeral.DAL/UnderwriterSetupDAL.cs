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
   public  class UnderwriterSetupDAL
    {
        private UnderwriterSetupDAL() { }
        public static SqlDataReader SelectAllUnderwriterSetupByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
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
               // return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectAllUnderwriterSetupByParlourId", ObjParam));
                return (DbConnection.GetDataReader(CommandType.StoredProcedure, "UnderWriterSetupSelectAllByParlourId", ObjParam));

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataTable SelectAllUnderwriterSetupByParlourIddt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
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
                return (DbConnection.GetDataTable(CommandType.StoredProcedure, "UnderWriterSetupSelectAllByParlourId", ObjParam));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int SaveUnderwriterSetup(UnderwriterSetupModel model)
        {
            try
            {
                string query = "UnderWriterSetupSave";
                DbParameter[] ObjParam = new DbParameter[17];

                ObjParam[0] = new DbParameter("@PkiUnderWriterSetupId", DbParameter.DbType.Int, 0, model.PkiUnderWriterSetupId);
               
                ObjParam[1] = new DbParameter("@UnderwriterName", DbParameter.DbType.NVarChar, 0, model.UnderwriterName);
                ObjParam[2] = new DbParameter("@ContactPerson", DbParameter.DbType.NVarChar, 0, model.ContactPerson);
                ObjParam[3] = new DbParameter("@AddressLine1", DbParameter.DbType.NVarChar, 0, model.AddressLine1);
                ObjParam[4] = new DbParameter("@AddressLine2", DbParameter.DbType.NVarChar, 0, model.AddressLine2);
                ObjParam[5] = new DbParameter("@City", DbParameter.DbType.NVarChar, 0, model.City);
                ObjParam[6] = new DbParameter("@Province", DbParameter.DbType.NVarChar, 0, model.Province);
                ObjParam[7] = new DbParameter("@PostalCode", DbParameter.DbType.NVarChar, 0, model.PostalCode);
                ObjParam[8] = new DbParameter("@Country", DbParameter.DbType.NVarChar, 0, model.Country);
                ObjParam[9] = new DbParameter("@ContactNumber", DbParameter.DbType.NVarChar, 0, model.ContactNumber);
                ObjParam[10] = new DbParameter("@ContactEmail", DbParameter.DbType.NVarChar, 0, model.ContactEmail);
                ObjParam[11] = new DbParameter("@FSPNNumber", DbParameter.DbType.NVarChar, 0, model.FSPNNumber);
                ObjParam[12] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.Parlourid);

                ObjParam[13] = new DbParameter("@CreatedBy", DbParameter.DbType.NVarChar, 0, model.CreatedBy);
                ObjParam[14] = new DbParameter("@ModifiedDate", DbParameter.DbType.DateTime, 0, model.ModifiedDate);
                ObjParam[15] = new DbParameter("@ModifiedBy", DbParameter.DbType.NVarChar, 0, model.ModifiedBy);
                ObjParam[16] = new DbParameter("@CreateddDate", DbParameter.DbType.DateTime, 0, model.CreateddDate);
               
                return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static SqlDataReader SelectUnderwriterSetupBypkid(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@PkiUnderwriterSetupId", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "UnderWriterSetupSelectAllByParlourId", ObjParam);
        }
        public static DataTable SelectUnderwriterSetupBypkiddt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@PkiUnderwriterSetupId", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "UnderWriterSetupSelectAllByParlourId", ObjParam);
        }
        public static int DeleteUnderwriterSetupByID(int PkiUnderWriterSetupId, string UserName)
        {
            //string query = "update [dbo].[UnderWriterSetup] set IsDeleted = 1, DeletedBy=@UserName where PkiUnderwriterSetupId=@PkiUnderwriterSetupId";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@PkiUnderWriterSetupId", DbParameter.DbType.Int, 0, PkiUnderWriterSetupId);
            ObjParam[1] = new DbParameter("@DeletedBy", DbParameter.DbType.NVarChar, 0, UserName);
            //ObjParam[2] = new DbParameter("@DateTime", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "UnderWriterSetupDelete", ObjParam));
        }

        public static SqlDataReader SelectUnderwriterSetupByName(string UnderwriterName, Guid ParlourId)
        {
            string query = "select * from [dbo].[UnderWriterSetup] where UnderwriterName = @UnderwriterName and Parlourid = @Parlourid";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@UnderwriterName", DbParameter.DbType.NVarChar, 0, UnderwriterName);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataReader(CommandType.Text, query, ObjParam);
        }
        public static DataTable SelectUnderwriterSetupByNamedt(string UnderwriterName, Guid ParlourId)
        {
            string query = "select * from [dbo].[UnderWriterSetup] where UnderwriterName = @UnderwriterName and Parlourid = @Parlourid";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@UnderwriterName", DbParameter.DbType.NVarChar, 0, UnderwriterName);
            ObjParam[1] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataTable(CommandType.Text, query, ObjParam);
        }
        public static SqlDataReader SelectUnderwriterSetupNotDeleted()
        {
            string query = "select * from [dbo].[UnderWriterSetup] where IsDeleted is null";

            return DbConnection.GetDataReader(CommandType.Text, query);
        }
        public static DataTable SelectUnderwriterSetupNotDeleteddt()
        {
            string query = "select * from [dbo].[UnderWriterSetup] where IsDeleted is null";

            return DbConnection.GetDataTable(CommandType.Text, query);
        }

        public static SqlDataReader EditUnderwriterSetupbyID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "UnderWriterSetupSelect", ObjParam);
        }
        public static DataTable EditUnderwriterSetupbyIDdt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "UnderWriterSetupSelect", ObjParam);
        }


    }
}
