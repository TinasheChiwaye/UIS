using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.DAL
{

    public class DatabaseFactory
    {
        static string strcon = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString();

        public DatabaseFactory()
        {
        }

        /// <summary>
        /// MemberInfoViaIDNumber
        /// </summary>
        /// <param name="SAIDNumber"></param>
        /// <param name="PolicyNumber"></param>
        /// <param name="IDToken"></param>
        /// <param name="UISMOBILEAPIKey"></param>
        /// <param name="DeviceIDTheMAC"></param>
        /// <param name="RequestID"></param>
        /// <returns></returns>
        public static DataTable MemberInfoViaIDNumber(string SAIDNumber, string PolicyNumber, string IDToken, string UISMOBILEAPIKey, string DeviceIDTheMAC, string RequestID)
        {

            SqlConnection Objcon = new SqlConnection(strcon);
            DataTable ObjDt = new DataTable();
            try
            {
                Objcon.Open();
                SqlDataAdapter da = new SqlDataAdapter("uspGetMemberInfoViaIDNumber", Objcon);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@SAIDNumber", SAIDNumber);
                da.SelectCommand.Parameters.AddWithValue("@PolicyNumber", PolicyNumber);
                da.SelectCommand.Parameters.AddWithValue("@IDToken", IDToken);
                da.SelectCommand.Parameters.AddWithValue("@UISMOBILEAPIKey", UISMOBILEAPIKey);
                da.SelectCommand.Parameters.AddWithValue("@DeviceIDTheMAC", DeviceIDTheMAC);
                da.SelectCommand.Parameters.AddWithValue("@RequestID", RequestID);
                da.Fill(ObjDt);
            }
            catch { }
            finally { Objcon.Close(); }
            return ObjDt;
        }

        /// <summary>
        /// GetServiceProviderInfo
        /// </summary>
        /// <param name="UISMOBILEAPIKey"></param>
        /// <returns></returns>
        public static DataTable GetServiceProviderInfo(string UISMOBILEAPIKey)
        {
            SqlConnection Objcon = new SqlConnection(strcon);
            DataTable ObjDt = new DataTable();
            try
            {
                Objcon.Open();
                SqlDataAdapter da = new SqlDataAdapter("uspGetServiceProviderInfo", Objcon);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@UISMOBILEAPIKey", UISMOBILEAPIKey);
                da.Fill(ObjDt);
            }
            catch { }
            finally { Objcon.Close(); }
            return ObjDt;
        }
        //uspGetServiceProviderInfo

        /// <summary>
        /// GetPolicyStatement
        /// </summary>
        /// <param name="PolicyID"></param>
        /// <param name="DeviceID"></param>
        /// <param name="VendorID"></param>
        /// <param name="UISMOBILEAPIKey"></param>
        /// <param name="RequestID"></param>
        /// <returns></returns>
        public static DataTable GetPolicyStatement(string PolicyID, string DeviceID, string VendorID, string UISMOBILEAPIKey, string RequestID)
        {
            SqlConnection Objcon = new SqlConnection(strcon);
            DataTable ObjDt = new DataTable();
            try
            {
                Objcon.Open();
                SqlDataAdapter da = new SqlDataAdapter("uspGetPolicyStatement", Objcon);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@PolicyID", PolicyID);
                da.SelectCommand.Parameters.AddWithValue("@DeviceID", DeviceID);
                da.SelectCommand.Parameters.AddWithValue("@VendorID", VendorID);
                da.SelectCommand.Parameters.AddWithValue("@UISMOBILEAPIKey", UISMOBILEAPIKey);
                da.SelectCommand.Parameters.AddWithValue("@RequestID", RequestID);
                da.Fill(ObjDt);
            }
            catch { }
            finally { Objcon.Close(); }
            return ObjDt;
        }

        
    }
}
