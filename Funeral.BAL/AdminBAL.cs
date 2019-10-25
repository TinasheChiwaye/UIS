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
    public class AdminBAL
    {
        #region Constructor

        #endregion

        #region Methods
        /// <summary>
        /// Do Admin Login
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static AdminModel AdminLogin(string Email, string Password)
        {
            DataTable dt = Funeral.DAL.AdminDAL.AdminLogindt(Email, Password);
            return FuneralHelper.DataTableMapToList<AdminModel>(dt).FirstOrDefault();
            //if (dr != null && dr.HasRows)
            //{
            //    return MakeObject(dr);
            //}
            //else
            //    return null;
        }
        #endregion

        #region Make Object
        private static AdminModel MakeObject(SqlDataReader dr)
        {
            AdminModel obj = new AdminModel();
            obj.Email = Convert.ToString(dr["Email"]);
            obj.PkiUserID = Convert.ToInt32(dr["PkiUserID"]);
            obj.Password = Convert.ToString(dr["Password"]);
            obj.Name = Convert.ToString(dr["Name"]);
            return obj;
        }
        #endregion
    }
}
