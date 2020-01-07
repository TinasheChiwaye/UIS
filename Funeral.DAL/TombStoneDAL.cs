using Funeral.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Funeral.DAL
{
    public sealed class TombStoneDAL
    {
        private TombStoneDAL()
        {
        }

        public static SqlDataReader SelectAllTombStoneByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectAllTombStoneByParlourId", ObjParam));
        }

        public static DataTable SelectAllTombStoneByParlourIddt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectAllTombStoneByParlourId", ObjParam));
        }
        public static int TombStoneDelete(int ID, string UserName)
        {
            string query = "update tblTombStones set IsDeleted = 1, DeletedDate=@DateTime, DeletedBy=@UserName where pkiTombstoneID=@pkiTombstoneID";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@pkiTombstoneID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@UserName", DbParameter.DbType.NVarChar, 0, UserName);
            ObjParam[2] = new DbParameter("@DateTime", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }

        public static int SaveTombStone(TombStoneModel model)
        {
            string query = "SaveTombStone";
            DbParameter[] ObjParam = new DbParameter[26];

            ObjParam[0] = new DbParameter("@pkiTombstoneID", DbParameter.DbType.Int, 0, model.pkiTombstoneID);
            ObjParam[1] = new DbParameter("@LastName", DbParameter.DbType.NVarChar, 0, model.LastName);
            ObjParam[2] = new DbParameter("@FirstName", DbParameter.DbType.NVarChar, 0, model.FirstName);
            ObjParam[3] = new DbParameter("@IDNumber", DbParameter.DbType.NVarChar, 0, model.IDNumber);
            ObjParam[4] = new DbParameter("@DateOfApplication", DbParameter.DbType.DateTime, 0, model.DateOfApplication);
            ObjParam[5] = new DbParameter("@Address1", DbParameter.DbType.NVarChar, 0, (model.Address1 == null ? "" : model.Address1));
            ObjParam[6] = new DbParameter("@Address2", DbParameter.DbType.NVarChar, 0, (model.Address2 == null ? "" : model.Address2));
            ObjParam[7] = new DbParameter("@Address3", DbParameter.DbType.NVarChar, 0, (model.Address3 == null ? "" : model.Address3));
            ObjParam[8] = new DbParameter("@Code", DbParameter.DbType.NVarChar, 0, model.Code);
            ObjParam[9] = new DbParameter("@TelNumber", DbParameter.DbType.NVarChar, 0, (model.TelNumber == null ? "" : model.TelNumber));
            ObjParam[10] = new DbParameter("@CellNumber", DbParameter.DbType.NVarChar, 0, (model.CellNumber == null ? "" : model.CellNumber));
            ObjParam[11] = new DbParameter("@DeceasedLastName", DbParameter.DbType.NVarChar, 0, model.DeceasedLastName);
            ObjParam[12] = new DbParameter("@DeceasedFirstName", DbParameter.DbType.NVarChar, 0, model.DeceasedFirstName);
            ObjParam[13] = new DbParameter("@DeceasedIDNumber", DbParameter.DbType.NVarChar, 0, model.DeceasedIDNumber);
            ObjParam[14] = new DbParameter("@Deceased", DbParameter.DbType.NVarChar, 0, model.Deceased);
            ObjParam[15] = new DbParameter("@DateOFMemorial", DbParameter.DbType.DateTime, 0, model.DateOFMemorial);
            ObjParam[16] = new DbParameter("@CemeterOrCrematorium", DbParameter.DbType.NVarChar, 0, (model.CemeterOrCrematorium == null ? "" : model.CemeterOrCrematorium));
            ObjParam[17] = new DbParameter("@GraveNo", DbParameter.DbType.NVarChar, 0, (model.GraveNo == null ? "" : model.GraveNo));
            ObjParam[18] = new DbParameter("@Erect", DbParameter.DbType.NVarChar, 0, model.Erect);
            ObjParam[19] = new DbParameter("@TombStoneMessage", DbParameter.DbType.NVarChar, 0, (model.TombStoneMessage == null ? "" : model.TombStoneMessage));
            ObjParam[20] = new DbParameter("@Notes", DbParameter.DbType.NVarChar, 0, (model.Notes == null ? "" : model.Notes));
            ObjParam[21] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[22] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[23] = new DbParameter("@InvoiceNumber", DbParameter.DbType.NVarChar, 0, model.InvoiceNumber);

            ObjParam[24] = new DbParameter("@ContactPerson", DbParameter.DbType.NVarChar, 0, (model.ContactPerson == null ? "" : model.ContactPerson));
            ObjParam[25] = new DbParameter("@ContactPersonNumber", DbParameter.DbType.NVarChar, 0, (model.ContactPersonNumber == null ? "" : model.ContactPersonNumber));
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }

        public static DataTable GetServiceByIDdt(int ID)
        {
            string query = "select * from FuneralServices where pkiServiceID=@pkiServiceID";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiServiceID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataTable(CommandType.Text, query, ObjParam);
        }

        public static SqlDataReader SelectTombStoneByParlAndPki(int pkiTombstoneID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@pkiTombstoneID", DbParameter.DbType.Int, 0, pkiTombstoneID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectTombStoneByParlAndPki", ObjParam);
        }
        public static DataTable SelectTombStoneByParlAndPkidt(int pkiTombstoneID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@pkiTombstoneID", DbParameter.DbType.Int, 0, pkiTombstoneID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectTombStoneByParlAndPki", ObjParam);
        }
        public static int UploadTombImage(string ImageName, string ImagePath, int pkiTombstoneID, Guid parlourid)
        {
            string query = "UploadTombImage";
            DbParameter[] ObjParam = new DbParameter[4];
            ObjParam[0] = new DbParameter("@ImageName", DbParameter.DbType.NVarChar, 0, ImageName);
            ObjParam[1] = new DbParameter("@ImagePath", DbParameter.DbType.NVarChar, 0, ImagePath);
            ObjParam[2] = new DbParameter("@pkiTombstoneID", DbParameter.DbType.Int, 0, pkiTombstoneID);
            ObjParam[3] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static SqlDataReader GetInvoiceNumOfTombByParlID(Guid ParlourId)
        {
            string query = "select (Max(InvoiceNumber)) as InvoiceNumber2 from tblTombStones where InvoiceNumber IS not NULL and parlourid=@parlourid";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.Text, query, ObjParam);
        }

        public static DataTable GetInvoiceNumOfTombByParlIDdt(Guid ParlourId)
        {
            string query = "select (Max(InvoiceNumber)) as InvoiceNumber2 from tblTombStones where InvoiceNumber IS not NULL and parlourid=@parlourid";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.Text, query, ObjParam);
        }
        public static SqlDataReader SelectServiceByTombStoneID(int fkiTombstoneID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@fkiTombstoneID", DbParameter.DbType.Int, 0, fkiTombstoneID);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectServiceByTombStoneID", ObjParam));
        }
        public static DataTable SelectServiceByTombStoneIDdt(int fkiTombstoneID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@fkiTombstoneID", DbParameter.DbType.Int, 0, fkiTombstoneID);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectServiceByTombStoneID", ObjParam));
        }
        public static int DeleteTombstoneServiceByID(int pkiTombStoneSelectionID)
        {
            string query = "Delete from tblTombStonesServiceSelect where  pkiTombStoneSelectionID= @pkiTombStoneSelectionID";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiTombStoneSelectionID", DbParameter.DbType.Int, 0, pkiTombStoneSelectionID);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }
        public static SqlDataReader SelectServiceByTombAndID(int fkiTombstoneID, int pkiTombStoneSelectionID)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@fkiTombstoneID", DbParameter.DbType.Int, 0, fkiTombstoneID);
            ObjParam[1] = new DbParameter("@pkiTombStoneSelectionID", DbParameter.DbType.Int, 0, pkiTombStoneSelectionID);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectServiceByTombAndID", ObjParam);
        }

        public static DataTable SelectServiceByTombAndIDdt(int fkiTombstoneID, int pkiTombStoneSelectionID)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@fkiTombstoneID", DbParameter.DbType.Int, 0, fkiTombstoneID);
            ObjParam[1] = new DbParameter("@pkiTombStoneSelectionID", DbParameter.DbType.Int, 0, pkiTombStoneSelectionID);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectServiceByTombAndID", ObjParam);
        }
        public static int SaveTombStoneService(TombStoneServiceSelectModel model)
        {
            string query = "SaveTombStoneService";
            DbParameter[] ObjParam = new DbParameter[7];

            ObjParam[0] = new DbParameter("@fkiTombstoneID", DbParameter.DbType.Int, 0, model.fkiTombstoneID);
            ObjParam[1] = new DbParameter("@fkiServiceID", DbParameter.DbType.Int, 0, model.fkiServiceID);
            ObjParam[2] = new DbParameter("@Quantity", DbParameter.DbType.Int, 0, model.Quantity);
            ObjParam[3] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.lastModified);
            ObjParam[4] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, model.modifiedUser);
            ObjParam[5] = new DbParameter("@ServiceRate", DbParameter.DbType.Money, 0, model.ServiceRate);
            ObjParam[6] = new DbParameter("@pkiTombStoneSelectionID", DbParameter.DbType.Int, 0, model.pkiTombStoneSelectionID);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static int UpdateAllTombStoneData(int pkiTombstoneID, Decimal DisCount, Decimal Tax, string InvoiceNumber)
        {
            string query = "update tblTombStones set DisCount=@DisCount,Tax=@Tax  where pkiTombstoneID=@pkiTombstoneID";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@pkiTombstoneID", DbParameter.DbType.Int, 0, pkiTombstoneID);
            ObjParam[1] = new DbParameter("@DisCount", DbParameter.DbType.Decimal, 0, DisCount);
            ObjParam[2] = new DbParameter("@Tax", DbParameter.DbType.Money, 0, Tax);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }

        public static DataTable GetAllTombStoneServicesdt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllTombStoneServices", ObjParam);
        }

        public static DataTable SelectTombStoneByTombStoneIddt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@TombStoneID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectTombStoneByTombStoneId", ObjParam);
        }
    }
}
