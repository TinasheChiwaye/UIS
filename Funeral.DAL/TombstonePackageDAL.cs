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
    public class TombstonePackageDAL
    {
        private TombstonePackageDAL()
        { 
        }

       
        public static SqlDataReader SelectPackageServiceByPackgeId(Guid ParlourId, int PackgeId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@fkiPackageId", DbParameter.DbType.VarChar, 0, PackgeId);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "TombstonePackageServicesSelectByPackgeId", ObjParam));
        }
        public static DataTable  SelectPackageServiceByPackgeIddt(Guid ParlourId, int PackgeId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@fkiPackageId", DbParameter.DbType.VarChar, 0, PackgeId);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "TombstonePackageServicesSelectByPackgeId", ObjParam));
        }
        public static SqlDataReader SelectAllPackage(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataReader(CommandType.Text, "SELECT [pkiPackageID],[PackageName],[parlourid],[LastModified],[ModifiedUser] FROM [dbo].[TombstonePackages] Where parlourid=@parlourid and ISNULL(IsActive,1)=1 Order By PackageName  ", ObjParam));
        }
        public static DataTable SelectAllPackagedt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataTable(CommandType.Text, "SELECT [pkiPackageID],[PackageName],[parlourid],[LastModified],[ModifiedUser] FROM [dbo].[TombstonePackages] Where parlourid=@parlourid and ISNULL(IsActive,1)=1 Order By PackageName  ", ObjParam));
        }

        public static int SavePackage(TombstonePackageModel model)
        {
            DbParameter[] ObjParam = new DbParameter[4];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, model.ParlourId);
            ObjParam[1] = new DbParameter("@PackageName", DbParameter.DbType.VarChar, 0, model.PackageName);
            ObjParam[2] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, model.ModifiedUser);
            ObjParam[3] = new DbParameter("@IsActive", DbParameter.DbType.Bit, 0, model.IsActive);
           
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "SaveTombstonePackages", ObjParam));
        }

        public static int SavePackageService(TombstonePackageModel model)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@fkiServiceID", DbParameter.DbType.Int, 0, model.fkiServiceID);            
            ObjParam[1] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, model.ModifiedUser);
            ObjParam[2] = new DbParameter("@fkiPackageID", DbParameter.DbType.Int, 0, model.fkiPackageID);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "SaveTombstonePackageServicesSelection", ObjParam));
        }

        public static void DeletePackageService(int Id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPackageID", DbParameter.DbType.Int, 0, Id);

            DbConnection.GetScalarValue(CommandType.Text, "DELETE FROM [dbo].[TombstonePackageServicesSelection] WHERE pkiPackageServiceID=@pkiPackageID", ObjParam);
        }

        public static void DeleteTombstonePackage(int Id)
        {
            DbParameter[] objParam = new DbParameter[1];
            objParam[0] = new DbParameter("@pkiPackageID", DbParameter.DbType.Int, 0, Id);
            DbConnection.GetScalarValue(CommandType.Text, "DELETE FROM [dbo].[TombstonePackages] WHERE pkiPackageID=@pkiPackageID", objParam);
        }

    }
}
