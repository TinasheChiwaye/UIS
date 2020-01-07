using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

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
            DataTable dt = AdminDAL.AdminLogindt(Email, Password);
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


        public static AdminModel GetLoginDetails(string Email)
        {
            DataTable dt = AdminDAL.AdminLogindt(Email);
            return FuneralHelper.DataTableMapToList<AdminModel>(dt).FirstOrDefault();
        }
        public static ForgotPasswordModel GetForgotPasswordDetails(int ForgotId)
        {
            DataTable dt = AdminDAL.GetForgotPasswordDetails(ForgotId);
            return FuneralHelper.DataTableMapToList<ForgotPasswordModel>(dt).FirstOrDefault();
        }

        public static int UpdateAdminLoginPassword(int UserId, Guid parlourid, string password)
        {
            return AdminDAL.UpdateAdminLoginPassword(UserId, parlourid, password);
        }
        public static int AddUpdateTo_ForgotPassword(int ForgotPassId, int UserId, Guid parlourid, string password)
        {
            return AdminDAL.AddUpdateTo_ForgotPassword(ForgotPassId, UserId, parlourid, password);
        }
        #region Forgote Passowrd
        public static bool SendForgotPasswordLink(string ToEmail, string FromEmail, string fullname, string url)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    MailMessage mm = new MailMessage(FromEmail, ToEmail);
                    mm.Subject = "Funeral Administration Forgot Password Link";
                    string body = "Hi " + fullname + ",";
                    body += "<br /><br />Please click on following link to reset your password.";
                    body += "<br /><a href = '" + url + "'>Click here to reset your password.</a>";
                    body += "<br /><br />Thanks";
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    smtpClient.Send(mm);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static string encription(string values)
        {
            var plaintextBytes = Encoding.UTF8.GetBytes(values);
            return MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);
        }
        public static string Decryption(string value)
        {
            if (String.IsNullOrWhiteSpace(value)) return null;
            var decryptedBytes = MachineKey.Decode(value, MachineKeyProtection.All);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        #endregion

    }
}
