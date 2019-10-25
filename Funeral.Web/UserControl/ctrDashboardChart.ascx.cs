using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using Funeral.Web.FuneralServiceReference;
using Funeral.Web.Common;
using Funeral.Model;
using Funeral.Web.App_Start;
using System.Web.Services;
using System.Web.Security;


namespace Funeral.Web.UserControl
{

    public partial class ctrDashboardChart : System.Web.UI.UserControl
    {
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();      

        public string ChartLabels = null;
        public string ChartData1 = null;
        public string ChartData2 = null;
        public string ChartTotalCount = null;
        public string PolicyPieChartTotalCount = null;
        public string PolicyPieChartLabels = null;
        public string PolicyPieChartData1 = null;
        public string PolicyPremiumPieChartTotalCount = null;
        public string PolicyPremiumPieChartLabels = null;
        public string PolicyPremiumPieChartData = null;
        public string Currency { get; set; }
        public string UserName
        {
            get;
            set;
        }
        public Guid ParlourId
        {
            get;
            set;
        }
        public bool IsAdministrator
        {
            get;
            set;
        }

        public bool IsSuperUser
        {
            get;
            set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            //ctrDashboardChart1 C = new ctrDashboardChart1();
          // bool IsAdmin= C.GetUserAccessByID();
          // if (IsAdmin==true)
          // { 
            if (!IsPostBack)
            {
                BarChart();
                //}
                PolicyPichart();
                PremiumCollectionChart();
            }
            
            
        }

        public void LoadChart()
        {
            BarChart();
            //}
            PolicyPichart();
            PremiumCollectionChart();
        }
        public DataTable BarChart()
        {
            DataTable dt = new DataTable();
            try
            {                
                SqlCommand com1 = new SqlCommand();
                com1.CommandType = CommandType.StoredProcedure;
                com1.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com1.CommandText = "GetUserDetailsByChart";
                com1.Parameters.Add(new SqlParameter("@ParlourId", this.ParlourId));
                com1.Parameters.Add(new SqlParameter("@IsAdmin", IsAdministrator));
                com1.Parameters.Add(new SqlParameter("@UserName", this.UserName));
                com1.Parameters.Add(new SqlParameter("@IsSuperUser", this.IsSuperUser));
                SqlDataAdapter adp1 = new SqlDataAdapter(com1);

                adp1.Fill(dt);

                string[] x = new string[dt.Rows.Count];
                decimal[] y = new decimal[dt.Rows.Count];
                Int32 TotalUserCount = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = Convert.ToString(dt.Rows[i]["CreateDate1"]);
                    y[i] = Convert.ToInt32(dt.Rows[i]["CountUser"]);
                    TotalUserCount = TotalUserCount + Convert.ToInt32(dt.Rows[i]["CountUser"]);
                }
                ChartTotalCount = Convert.ToString(TotalUserCount);
                string YAxis = string.Join(",", y);
                string XAxis = string.Join(",", x);
                this.ChartLabels = XAxis;
                this.ChartData1 = YAxis;
                this.ChartData2 = YAxis;

                //this.ChartLabels = "'January', 'February', 'March', 'April', 'May', 'June', 'July'";
                //this.ChartData1 = "65, 59, 80, 81, 56, 55, 40";
                //this.ChartData2 = "28, 48, 40, 19, 86, 27, 90";

                //Call the Javascript function from C#
             //   Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DrawChart()", true);


                //BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y });
                //BarChart1.CategoriesAxis = string.Join(",", x);
                //BarChart1.ChartTitle = string.Format("{0} Created User List ", "Last 30 Days");
                //if (x.Length > 3)
                //{
                //    BarChart1.ChartWidth = (x.Length * 100).ToString();
                //}
                //// BarChart1.Visible = ddlCountries.SelectedItem.Value != "";
                //BarChart1.Visible = true;

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return dt;
        }
        public void PolicyPichart()
        {            
           DataTable dt = new DataTable();
           try
           {           
               
               SqlCommand com1 = new SqlCommand();
               com1.CommandType = CommandType.StoredProcedure;
               com1.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
               com1.CommandText = "PolicyStatusPieChart";
               com1.Parameters.Add(new SqlParameter("@parlourid", this.ParlourId));
               com1.Parameters.Add(new SqlParameter("@IsAdmin", IsAdministrator));
               com1.Parameters.Add(new SqlParameter("@UserName", this.UserName));
               com1.Parameters.Add(new SqlParameter("@IsSuperUser", this.IsSuperUser));
               SqlDataAdapter adp1 = new SqlDataAdapter(com1);
               adp1.Fill(dt);
               string[] x = new string[dt.Rows.Count];
               decimal[] y = new decimal[dt.Rows.Count];
               Int32 TotalPolicy = 0;
               for (int i = 0; i <  dt.Rows.Count; i++)
               {
                   if (Convert.ToString(dt.Rows[i]["PolicyStatus"]) == "")
                   {
                       x[i] = "'Not Defined'";
                   }
                   else
                   {
                       x[i] = "'" + Convert.ToString(dt.Rows[i]["PolicyStatus"]) + "'";
                   }
                   y[i] = Convert.ToInt32(dt.Rows[i]["PolicyStatusCount"]);
                   TotalPolicy = TotalPolicy + Convert.ToInt32(dt.Rows[i]["PolicyStatusCount"]);
               }
               PolicyPieChartTotalCount = TotalPolicy.ToString();
               string YAxis = string.Join(",", y);
               string XAxis = string.Join(",", x);
               this.PolicyPieChartLabels = XAxis;
               this.PolicyPieChartData1 = YAxis;
              // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DrawChart()", true);
           }
           catch (Exception ex)
           {
               Console.Write(ex.ToString());
           }
            
        }
        public void PremiumCollectionChart()
        { 
         DataTable dt = new DataTable();
         try
         {
             AdminBasePage ABP = new AdminBasePage();
             SqlCommand com1 = new SqlCommand();
             com1.CommandType = CommandType.StoredProcedure;
             com1.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
             com1.CommandText = "PremiumCollectionChart";
             com1.Parameters.Add(new SqlParameter("@parlourid", this.ParlourId));
             com1.Parameters.Add(new SqlParameter("@IsAdmin", IsAdministrator));
             com1.Parameters.Add(new SqlParameter("@UserName", this.UserName));
             com1.Parameters.Add(new SqlParameter("@IsSuperUser", this.IsSuperUser));
             SqlDataAdapter adp1 = new SqlDataAdapter(com1);
             adp1.Fill(dt);
             string[] x = new string[dt.Rows.Count];
             decimal[] y = new decimal[dt.Rows.Count];
             Int32 TotalPremiumCount = 0;
             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 x[i] = Convert.ToString(dt.Rows[i]["DatePaid1"]);
                 y[i] = Convert.ToInt32(dt.Rows[i]["CountUser"]);
                 TotalPremiumCount = TotalPremiumCount + Convert.ToInt32(dt.Rows[i]["CountUser"]);
             }
             PolicyPremiumPieChartTotalCount = TotalPremiumCount.ToString();
             string YAxis = string.Join(",", y);
             string XAxis = string.Join(",", x);
             this.PolicyPremiumPieChartLabels = XAxis;
             this.PolicyPremiumPieChartData = YAxis;
         }
         catch (Exception ex)
         {
             Console.Write(ex.ToString());
         }
        }
      
    }
    public partial class ctrDashboardChart1 : AdminBasePage
    {
        FuneralServiceReference.FuneralServicesClient client = new FuneralServiceReference.FuneralServicesClient();      
        public bool GetUserAccessByID()
        {
            bool IsAdmin=false;
            try
            {
                SecureUserGroupsModel model;
                model = client.GetUserAccessByID(UserID, ParlourId);
                if (model != null)
                {
                    IsAdmin = true;
                }
                else
                {
                    IsAdmin = false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return IsAdmin;
        } 
    }
   
}