using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Funeral.DAL
{
    public class DebitOrderTransactionsDAL
    {
        public DebitOrderTransactionsDAL()
        {
        }

        public static SqlDataReader SelectAllTransactionsById(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParams = new DbParameter[2];
            ObjParams[0] = new DbParameter("@pkiTransactionID", DbParameter.DbType.Int, 0, ID);
            ObjParams[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetDebitTransactions");
        }

        public static DataTable SelectAllTransactionsByIddt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParams = new DbParameter[2];
            ObjParams[0] = new DbParameter("@pkiTransactionID", DbParameter.DbType.Int, 0, ID);
            ObjParams[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetDebitTransactions");
        }


        public static SqlDataReader SelectAllTransactionsByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "GetDebitTransactionsbyParlourIds", ObjParam));
        }

        public static DataTable SelectAllTransactionsByParlourIddt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "GetDebitTransactionsbyParlourIds", ObjParam));
        }

        public static SqlDataReader SelectAllBatchesById(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParams = new DbParameter[2];
            ObjParams[0] = new DbParameter("@pkiDebitBatch", DbParameter.DbType.Int, 0, ID);
            ObjParams[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetDebitBatchesByIds");
        }
        public static DataTable SelectAllBatchesByIddt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParams = new DbParameter[2];
            ObjParams[0] = new DbParameter("@pkiDebitBatch", DbParameter.DbType.Int, 0, ID);
            ObjParams[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetDebitBatchesByIds");
        }

        public static SqlDataReader SelectAllBatchesByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "GetDebitBatchesByParlourIds", ObjParam));
        }


        public static DataTable SelectAllBatchesByParlourIddt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "GetDebitBatchesByParlourIds", ObjParam));
        }


        public static SqlDataReader GetAllTransactionListById(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParams = new DbParameter[2];
            ObjParams[0] = new DbParameter("@pkiDebitBatch", DbParameter.DbType.Int, 0, ID);
            ObjParams[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetDebitBatchesByIds");
        }
        public static DataTable GetAllTransactionListByIddt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParams = new DbParameter[2];
            ObjParams[0] = new DbParameter("@pkiDebitBatch", DbParameter.DbType.Int, 0, ID);
            ObjParams[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetDebitBatchesByIds");
        }

        public static SqlDataReader GetAllTransactionListByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "GetDebitBatchesByParlourIds", ObjParam));
        }


        public static DataTable GetAllTransactionListByParlourIddt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "GetDebitBatchesByParlourIds", ObjParam));
        }

        //test
        public static SqlDataReader GetTransactionListById(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParams = new DbParameter[2];
            ObjParams[0] = new DbParameter("@pkiDebitBatch", DbParameter.DbType.Int, 0, ID);
            ObjParams[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "getTransactionsList", ObjParams);
        }

        public static DataTable GetTransactionListByIddt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParams = new DbParameter[2];
            ObjParams[1] = new DbParameter("@pkiDebitBatch", DbParameter.DbType.Int, 0, ID);
            ObjParams[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "getTransactionsList", ObjParams);
        }

    }
}




