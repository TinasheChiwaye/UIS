using System;
using System.Data;
using System.Data.Common;
using Funeral.Model;
using System.Data.SqlClient;




namespace Funeral.DAL
{

    public sealed class QuotationDAL
    {
        private QuotationDAL() { }


        public static int SaveQuotation(QuotationModel model)
        {
            string query = "SaveQuotation";
            DbParameter[] ObjParam = new DbParameter[16];
            if(model.TelNumber == null)
            {
                model.TelNumber = "";
            }

            ObjParam[0] = new DbParameter("@ContactTitle", DbParameter.DbType.NVarChar, 0, model.ContactTitle);
            ObjParam[1] = new DbParameter("@ContactFirstName", DbParameter.DbType.NVarChar, 0, model.ContactFirstName);
            ObjParam[2] = new DbParameter("@ContactLastName", DbParameter.DbType.NVarChar, 0, model.ContactLastName);
            ObjParam[3] = new DbParameter("@TelNumber", DbParameter.DbType.NVarChar, 0, model.TelNumber);
            ObjParam[4] = new DbParameter("@CellNumber", DbParameter.DbType.NVarChar, 0, model.CellNumber);
            ObjParam[5] = new DbParameter("@AddressLine1", DbParameter.DbType.NVarChar, 0, model.AddressLine1 == null ? "" : model.AddressLine1);
            ObjParam[6] = new DbParameter("@AddressLine2", DbParameter.DbType.NVarChar, 0, model.AddressLine2 == null ? "" : model.AddressLine2);
            ObjParam[7] = new DbParameter("@AddressLine3", DbParameter.DbType.NVarChar, 0, model.AddressLine3 == null ? "" : model.AddressLine3);
            ObjParam[8] = new DbParameter("@AddressLine4", DbParameter.DbType.NVarChar, 0, model.AddressLine4 == null ? "" : model.AddressLine4);
            ObjParam[9] = new DbParameter("@Code", DbParameter.DbType.NVarChar, 0, model.Code == null ? "" : model.Code);
            ObjParam[10] = new DbParameter("@DateOfQuotation", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            ObjParam[11] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[12] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            ObjParam[13] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[14] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, model.QuotationID);
            ObjParam[15] = new DbParameter("@QuotationNumber", DbParameter.DbType.NVarChar, 0, model.QuotationNumber);


            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }


        public static int UpdateQuotation(QuotationModel model)
        {
            string query = "UpdateQuotation";
            DbParameter[] ObjParam = new DbParameter[15];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, model.QuotationID);
            ObjParam[1] = new DbParameter("@ContactTitle", DbParameter.DbType.NVarChar, 0, model.ContactTitle);
            ObjParam[2] = new DbParameter("@ContactFirstName", DbParameter.DbType.NVarChar, 0, model.ContactFirstName);
            ObjParam[3] = new DbParameter("@ContactLastName", DbParameter.DbType.NVarChar, 0, model.ContactLastName);
            ObjParam[4] = new DbParameter("@TelNumber", DbParameter.DbType.NVarChar, 0, model.TelNumber);
            ObjParam[5] = new DbParameter("@CellNumber", DbParameter.DbType.NVarChar, 0, model.CellNumber);
            ObjParam[6] = new DbParameter("@AddressLine1", DbParameter.DbType.NVarChar, 0, model.AddressLine1);
            ObjParam[7] = new DbParameter("@AddressLine2", DbParameter.DbType.NVarChar, 0, model.AddressLine2);
            ObjParam[8] = new DbParameter("@AddressLine3", DbParameter.DbType.NVarChar, 0, model.AddressLine3);
            ObjParam[9] = new DbParameter("@AddressLine4", DbParameter.DbType.NVarChar, 0, model.AddressLine4);
            ObjParam[10] = new DbParameter("@Code", DbParameter.DbType.NVarChar, 0, model.Code);
            ObjParam[11] = new DbParameter("@DateOfQuotation", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            ObjParam[12] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[13] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            ObjParam[14] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);


            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }

        /// <summary>
        /// Deletes a record from the Quotations table by a composite primary key.
        /// </summary>
        /// <param name="quotationID"></param>
        public static int QuotationDelete(int id, string UserName)
        {
            string query = "update Quotations set IsDeleted = 1, DeletedDate=@DateTime, DeletedBy=@UserName where QuotationID=@QuotationID";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, id);
            ObjParam[1] = new DbParameter("@UserName", DbParameter.DbType.NVarChar, 0, UserName);
            ObjParam[2] = new DbParameter("@DateTime", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }
        public static SqlDataReader SelectQuotationByQuotationId(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectQuotationByQuotationId", ObjParam);
        }
        public static DataTable  SelectQuotationByQuotationIddt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectQuotationByQuotationId", ObjParam);
        }
        public static SqlDataReader SelectAllByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectAllByParlourId", ObjParam));
        }

        public static DataTable  SelectAllByParlourIddt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectAllByParlourId", ObjParam));
        }
        public static SqlDataReader GetQuotationNumberByID(Guid ParlourId)
        {
            string query = "select QuotationNumber from Quotations where parlourid=@parlourid and QuotationNumber IS not NULL";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.Text, query, ObjParam);
        }

        public static DataTable  GetQuotationNumberByIDdt(Guid ParlourId)
        {
            string query = "select QuotationNumber from Quotations where parlourid=@parlourid and QuotationNumber IS not NULL";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.Text, query, ObjParam);
        }
        public static SqlDataReader GetAllQuotationServices(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetAllQuotationServices", ObjParam);
        }

        public static DataTable  GetAllQuotationServicesdt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllQuotationServices", ObjParam);
        }
        public static SqlDataReader GetServiceByID(int ID)
        {
            string query = "select * from FuneralServices where pkiServiceID=@pkiServiceID";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiServiceID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataReader(CommandType.Text, query, ObjParam);
        }

        public static DataTable  GetServiceByIDdt(int ID)
        {
            string query = "select * from FuneralServices where pkiServiceID=@pkiServiceID";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiServiceID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataTable(CommandType.Text, query, ObjParam);
        }

        public static int SaveService(QuotationServicesModel model)
        {
            string query = "SaveService";
            DbParameter[] ObjParam = new DbParameter[7];

            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, model.QuotationID);
            ObjParam[1] = new DbParameter("@fkiServiceID", DbParameter.DbType.Int, 0, model.fkiServiceID);
            ObjParam[2] = new DbParameter("@Quantity", DbParameter.DbType.Int, 0, model.Quantity);
            ObjParam[3] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.lastModified);
            ObjParam[4] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, model.modifiedUser);
            ObjParam[5] = new DbParameter("@ServiceRate", DbParameter.DbType.Money, 0, model.ServiceRate);
            ObjParam[6] = new DbParameter("@pkiQuotationSelectionID", DbParameter.DbType.Int, 0, model.pkiQuotationSelectionID);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static SqlDataReader SelectServiceByQoutationID(int QuotationID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, QuotationID);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectServiceByQoutationID", ObjParam));
        }

        public static DataTable  SelectServiceByQoutationIDdt(int QuotationID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, QuotationID);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectServiceByQoutationID", ObjParam));
        }
        public static int DeleteServiceByID(int pkiQuotationSelectionID)
        {
            string query = "Delete from QuotationServicesSelection where pkiQuotationSelectionID = @pkiQuotationSelectionID";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiQuotationSelectionID", DbParameter.DbType.Int, 0, pkiQuotationSelectionID);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }
        public static SqlDataReader GetQuotationNumberByID2(Guid ParlourId)
        {
            string query = "select (Max(QuotationNumber)) as QuotationNumber2 from Quotations where QuotationNumber IS not NULL and parlourid=@parlourid";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.Text, query, ObjParam);
        }

        public static DataTable  GetQuotationNumberByID2dt(Guid ParlourId)
        {
            string query = "select (Max(QuotationNumber)) as QuotationNumber2 from Quotations where QuotationNumber IS not NULL and parlourid=@parlourid";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.Text, query, ObjParam);
        }
        public static int UpdateAllData(int QuotationID, string Notes, string QuotationNumber)
        {
            string query = "update Quotations set Notes=@Notes where QuotationID=@QuotationID";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, QuotationID);
            ObjParam[1] = new DbParameter("@Notes", DbParameter.DbType.NVarChar, 0, Notes);           
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }

        public static int SaveDiscountAndAmount(QuotationDiscountModel model)
        {
            string query = "SaveDiscountAndAmount";
            DbParameter[] ObjParam = new DbParameter[5];

            ObjParam[0] = new DbParameter("@DiscountID", DbParameter.DbType.Int, 0, model.DiscountID);
            ObjParam[1] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, model.QuotationID);
            ObjParam[2] = new DbParameter("@Amount", DbParameter.DbType.Int, 0, model.Amount);
            ObjParam[3] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[4] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, model.ModifiedUser);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static SqlDataReader GetAllQuotationDiscounts(int QuotationID)
        {
            string query = "select DiscountID from QuotationDiscounts where QuotationID = @QuotationID";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, QuotationID);
            return DbConnection.GetDataReader(CommandType.Text, query, ObjParam);
        }

        public static DataTable  GetAllQuotationDiscountsdt(int QuotationID)
        {
            string query = "select DiscountID from QuotationDiscounts where QuotationID = @QuotationID";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, QuotationID);
            return DbConnection.GetDataTable(CommandType.Text, query, ObjParam);
        }
        public static SqlDataReader SelectServiceByQouAndID(int QuotationID, int pkiQuotationSelectionID)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, QuotationID);
            ObjParam[1] = new DbParameter("@pkiQuotationSelectionID", DbParameter.DbType.Int, 0, pkiQuotationSelectionID);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectServiceByQouAndID", ObjParam);
        }
        public static DataTable  SelectServiceByQouAndIDdt(int QuotationID, int pkiQuotationSelectionID)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, QuotationID);
            ObjParam[1] = new DbParameter("@pkiQuotationSelectionID", DbParameter.DbType.Int, 0, pkiQuotationSelectionID);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectServiceByQouAndID", ObjParam);
        }
        public static int SaveQuotationMessage(QuotationMessageModel model)
        {
            string query = "SaveQuotaionMessage";
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pkidQuotationMsg", DbParameter.DbType.Int, 0, model.pkidQuotationMsg);
            ObjParam[1] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, model.QuotationID);
            ObjParam[2] = new DbParameter("@Message", DbParameter.DbType.NVarChar, 0, model.Message);
            ObjParam[3] = new DbParameter("@CreatedDate", DbParameter.DbType.DateTime, 0, model.CreatedDate);
            ObjParam[4] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[5] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static SqlDataReader SelectQuotationMessageByID(int QuotationID)
        {
            string query = "select * from QuotationMessage where QuotationID=@QuotationID";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, QuotationID);
            return DbConnection.GetDataReader(CommandType.Text, query, ObjParam);
        }

        public static DataTable  SelectQuotationMessageByIDdt(int QuotationID)
        {
            string query = "select * from QuotationMessage where QuotationID=@QuotationID";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, QuotationID);
            return DbConnection.GetDataTable(CommandType.Text, query, ObjParam);
        }
        public static int SaveQuotationTaxAndDiscount(int QuotationID, Decimal Tax, Decimal Discount)
        {
            string query = "SaveQuotationTaxAndDiscount";
            DbParameter[] ObjParam = new DbParameter[3];

            ObjParam[0] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, QuotationID);
            ObjParam[1] = new DbParameter("@Tax", DbParameter.DbType.Money, 0, Tax);
            ObjParam[2] = new DbParameter("@Discount", DbParameter.DbType.Decimal, 0, Discount);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static int QuotationStatus(int QuotationID, Guid parlourid,string status)
        {
            string Query="update Quotations set QuotationStatus=@QuotationStatus  where QuotationID=@QuotationID and parlourid=@parlourid";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@QuotationStatus",DbParameter.DbType.NVarChar,0,status);
            ObjParam[1] = new DbParameter("@QuotationID", DbParameter.DbType.Int, 0, QuotationID);
            ObjParam[2] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, Query, ObjParam));
        }
        
    }
}
