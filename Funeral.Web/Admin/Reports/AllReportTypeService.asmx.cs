using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Funeral.Web
{
    /// <summary>
    /// Summary description for AllReportTypeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class AllReportTypeService : System.Web.Services.WebService
    {
        List<ReportType> list = new List<ReportType>();
        [WebMethod]
        public List<ReportType> GetAllEmployees(string employeeId)
        {
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
            com.CommandText = "SelectReportList";
            com.Parameters.Add(new SqlParameter("@ReportType", employeeId));
            SqlDataAdapter adp = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            list.Add(new ReportType { Id = "0", Name = "Select Report" });
            if (dt.Rows.Count > 0)
            {
                foreach (System.Data.DataRow dr in dt.Rows)
                {
                    list.Add(new ReportType { Id = dr[0].ToString(), Name = dr[0].ToString() });
                }
            }
            return list;
        }
       
    }
}
public class ReportType
{
    public string Id { get; set; }
    public string Name { get; set; }
}