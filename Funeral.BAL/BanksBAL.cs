using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.BAL
{
    public class BanksBAL
    {
        public BanksBAL()
        { 
        }

        /// <summary>
        /// Select All Bank
        /// </summary>
        /// <returns></returns>
        public static List<BankModel> SelectAll()
        {
            DataTable dr = BanksDAL.SelectAlldt();
            return FuneralHelper.DataTableMapToList<BankModel>(dr);
        }

        /// <summary>
        /// Select All Bank
        /// </summary>
        /// <returns></returns>
        //public static List<BankModel> SelectAllReader()
        //{
        //    SqlDataReader dr = BanksDAL.SelectAll();
        //    return FuneralHelper.DataReaderMapToList<BankModel>(dr);
        //}

        /// <summary>
        /// Account Type Select All
        /// </summary>
        /// <returns></returns>
        public static List<AccountTypeModel> AccountTypeSelectAll()
        {
            DataTable dr = BanksDAL.AccountTypeSelectAlldt();
            return FuneralHelper.DataTableMapToList<AccountTypeModel>(dr);
        }

        /// <summary>
        /// Account Type Select All
        /// </summary>
        /// <returns></returns>
        //public static List<AccountTypeModel> AccountTypeSelectAllReader()
        //{

        //    SqlDataReader dr = BanksDAL.AccountTypeSelectAll();
        //    return FuneralHelper.DataReaderMapToList<AccountTypeModel>(dr);
        //}
    }
}
