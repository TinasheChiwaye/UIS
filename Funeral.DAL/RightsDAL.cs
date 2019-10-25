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
    public class RightsDAL
    {
       
        public static SqlDataReader tblRightGetAll()
        {
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "tblRightGetAll");
        }

        public static DataTable tblRightGetAlldt()
        {
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "tblRightGetAll");
        }

        public static int SavetblRight(RightsModel model)
        {
            try
            {
                string query = "SavetblRight";
                DbParameter[] ObjParam = new DbParameter[24];

                ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, model.ID);
                ObjParam[1] = new DbParameter("@ParentRightId", DbParameter.DbType.NVarChar, 0, model.ParentRightId);
                ObjParam[2] = new DbParameter("@ApplicationID", DbParameter.DbType.Int, 0, model.ApplicationID);
                ObjParam[3] = new DbParameter("@Formkey", DbParameter.DbType.NVarChar, 0, model.Formkey);
                ObjParam[4] = new DbParameter("@RoleId", DbParameter.DbType.NVarChar, 0, model.RoleId);
                ObjParam[5] = new DbParameter("@MenuName", DbParameter.DbType.NVarChar, 0, model.MenuName);
                ObjParam[6] = new DbParameter("@IconClassName", DbParameter.DbType.NVarChar, 0, model.IconClassName);
                ObjParam[7] = new DbParameter("@MenuURL", DbParameter.DbType.NVarChar, 0, model.MenuURL);
                ObjParam[8] = new DbParameter("@MenuLevel", DbParameter.DbType.Int, 0, model.MenuLevel);
                ObjParam[9] = new DbParameter("@MenuOrder", DbParameter.DbType.Int, 0, model.MenuOrder);
                ObjParam[10] = new DbParameter("@InMenu", DbParameter.DbType.Bit, 0, model.InMenu);
                ObjParam[11] = new DbParameter("@IsForm", DbParameter.DbType.Bit, 0, model.IsForm);
                ObjParam[12] = new DbParameter("@IsCreate", DbParameter.DbType.Bit, 0, model.IsCreate);
                ObjParam[13] = new DbParameter("@IsRead", DbParameter.DbType.Bit, 0, model.IsRead);
                ObjParam[14] = new DbParameter("@IsUpdate", DbParameter.DbType.Bit, 0, model.IsUpdate);
                ObjParam[15] = new DbParameter("@IsDelete", DbParameter.DbType.Bit, 0, model.IsDelete);
                ObjParam[16] = new DbParameter("@IsOthers", DbParameter.DbType.Bit, 0, model.IsOthers);
                ObjParam[17] = new DbParameter("@CreatedDate", DbParameter.DbType.DateTime, 0, model.CreatedDate);
                ObjParam[18] = new DbParameter("@CreateBy", DbParameter.DbType.NVarChar, 0, model.CreateBy);
                ObjParam[19] = new DbParameter("@UpdateDate", DbParameter.DbType.DateTime, 0, model.UpdateDate);
                ObjParam[20] = new DbParameter("@UpdateBy", DbParameter.DbType.NVarChar, 0, model.UpdateBy);
                ObjParam[21] = new DbParameter("@FormName", DbParameter.DbType.NVarChar, 0, model.FormName);
                ObjParam[22] = new DbParameter("@Description", DbParameter.DbType.NVarChar, 0, model.Description);
                ObjParam[23] = new DbParameter("@IsMenu", DbParameter.DbType.Bit, 0, model.IsMenu);

                return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        public static SqlDataReader SelecttblRightById(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelecttblRightById", ObjParam);
        }

        public static DataTable SelecttblRightByIddt(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelecttblRightById", ObjParam);
        }

        public static SqlDataReader GetRightsByGroupId(Guid ParlourId, int GroupId)
        {
            try
            {
                DbParameter[] ObjParam = new DbParameter[2];
                ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
                ObjParam[1] = new DbParameter("@GroupId", DbParameter.DbType.Int, 0, GroupId);


                return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetRightsByGroupId", ObjParam);
            }
            catch (Exception e)
            { throw e; }
        }

        public static DataTable GetRightsByGroupIddt(Guid ParlourId, int GroupId)
        {
            try
            {
                DbParameter[] ObjParam = new DbParameter[2];
                ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
                ObjParam[1] = new DbParameter("@GroupId", DbParameter.DbType.Int, 0, GroupId);


                return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetRightsByGroupId", ObjParam);
            }
            catch (Exception e)
            { throw e; }
        }


        public static SqlDataReader GetAllActiveTblPages()
        {
            try
            {
                string Query = "select * from [dbo].[tblPages] Where IsActive = 1"; 

                return DbConnection.GetDataReader(CommandType.Text, Query);
            }
            catch (Exception e)
            { throw e; }
        }

        public static DataTable GetAllActiveTblPagesdt()
        {
            try
            {
                string Query = "select * from [dbo].[tblPages] Where IsActive = 1";

                return DbConnection.GetDataTable(CommandType.Text, Query);
            }
            catch (Exception e)
            { throw e; }
        }

        public static int SaveTblRights(NewRightsModel model)
        {
            try
            {
                string query = "SaveTblRights";
                DbParameter[] ObjParam = new DbParameter[10];

                ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, model.ID);
                ObjParam[1] = new DbParameter("@PageId", DbParameter.DbType.Int, 0, model.PageId);
                ObjParam[2] = new DbParameter("@GroupId", DbParameter.DbType.Int, 0, model.GroupId);
                ObjParam[3] = new DbParameter("@HasAccess", DbParameter.DbType.Bit, 0, model.HasAccess);
                ObjParam[4] = new DbParameter("@IsRead", DbParameter.DbType.Bit, 0, model.IsRead);
                ObjParam[5] = new DbParameter("@IsWrite", DbParameter.DbType.Bit, 0, model.IsWrite);
                ObjParam[6] = new DbParameter("@IsDelete", DbParameter.DbType.Bit, 0, model.IsDelete);
                ObjParam[7] = new DbParameter("@IsUpdate", DbParameter.DbType.Bit, 0, model.IsUpdate);
                ObjParam[8] = new DbParameter("@IsReversalPayment", DbParameter.DbType.Bit, 0, model.IsReversalPayment);
                ObjParam[9] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, model.ParlourId);
                

                return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static SqlDataReader LoadSideMenu(Guid ParlourId, int UserId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@UserId", DbParameter.DbType.Int, 0, UserId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "LoadSideMenu", ObjParam);
        }

        public static DataTable LoadSideMenudt(Guid ParlourId, int UserId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@UserId", DbParameter.DbType.Int, 0, UserId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "LoadSideMenu", ObjParam);
        }

       
    }
}
