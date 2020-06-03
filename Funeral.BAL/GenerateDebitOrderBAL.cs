using System;
using Funeral.DAL;
using Funeral.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Funeral.BAL
{
    public class GenerateDebitOrderBAL
    {
        public GenerateDebitOrderBAL()
        {
        }

        //public static List<DebitInstructionModel> SelectAll()
        //{
        //    DataTable dt = DebitOrderDAL.SelectAlldt();
        //    return FuneralHelper.DataTableMapToList<DebitInstructionModel>(dt);
        //}

        //public static List<DebitStatusModel> SelectAllDebitOrderStatus()
        //{
        //    DataTable dt = DebitOrderDAL.SelectAllStatusdt();
        //    return FuneralHelper.DataTableMapToList<DebitStatusModel>(dt);
        //}

        //public static List<DebitOrderTransactions> SelectAllTransactions()
        //{
        //    DataTable dr = DebitOrderTransactionsDAL.SelectAllTransactionsdt();
        //    return FuneralHelper.DataTableMapToList<DebitOrderTransactions>(dr);
        //}


        public static DebitOrderTransactionModel SelectAllTransactionsById(int ID, Guid ParlourId)
        {
            DataTable dr = DebitOrderTransactionsDAL.SelectAllTransactionsByIddt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<DebitOrderTransactionModel>(dr).FirstOrDefault();
        }

        public static List<DebitOrderTransactionModel> SelectAllTransactionsByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable dr = DebitOrderTransactionsDAL.SelectAllTransactionsByParlourIddt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);

            return FuneralHelper.DataTableMapToList<DebitOrderTransactionModel>(dr);
        }

        public static DebitBatchListModel SelectAllDebitBatchesById(int ID, Guid ParlourId)
        {
            DataTable dr = DebitOrderTransactionsDAL.SelectAllBatchesByIddt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<DebitBatchListModel>(dr).FirstOrDefault();
        }

        public static List<DebitBatchListModel> SelectAllDebitBatchesByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable dr = DebitOrderTransactionsDAL.SelectAllBatchesByParlourIddt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
            return FuneralHelper.DataTableMapToList<DebitBatchListModel>(dr);
        }

        //Test
        public static List<DebitOrderTransactionModel> SelectTransactionListById(int ID, Guid ParlourId)
        {
            DataTable dr = DebitOrderTransactionsDAL.GetTransactionListByIddt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<DebitOrderTransactionModel>(dr);
        }
    }
}
