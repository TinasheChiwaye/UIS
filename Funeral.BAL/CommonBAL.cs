using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.BAL
{
    public class CommonBAL
    {
        public static List<PolicyModel> GetPolicyByParlourId(Guid parlourid)
        {
            DataTable dr = MembersDAL.GetPolicyByParlourIddt(parlourid);
            return FuneralHelper.DataTableMapToList<PolicyModel>(dr);
        }
        public static List<SocietyModel> GetSocietyByParlourId(Guid parlourid)
        {
            DataTable dr = MembersDAL.GetSocietyByParlourIddt(parlourid);
            return FuneralHelper.DataTableMapToList<SocietyModel>(dr);
        }
        public static List<SocietyModel> GetAllSociety(Guid parlourid)
        {
            DataTable dr = MembersDAL.GetAllSocietydt(parlourid);
            return FuneralHelper.DataTableMapToList<SocietyModel>(dr);
        }

        public static List<BranchModel> GetBranchByParlourId(Guid parlourid)
        {
            DataTable dr = MembersDAL.GetBranchByParlourIddt(parlourid);
            return FuneralHelper.DataTableMapToList<BranchModel>(dr);
        }
        public static List<UnderwriterSetupModel> GetUnderwriterSetupNameByParlourId(Guid parlourid)
        {
            DataTable dr = UnderwriterPremiumDAL.GetUnderwriterSetupNameByParlourIddt(parlourid);
            return FuneralHelper.DataTableMapToList<UnderwriterSetupModel>(dr);
        }

        public static List<UnderwriterPremiumModel> EditUnderwriterPremiumbyID(int ID, Guid parlourid)
        {
            DataTable dr = UnderwriterPremiumDAL.EditUnderwriterPremiumbyIDdt(ID, parlourid);
            return FuneralHelper.DataTableMapToList<UnderwriterPremiumModel>(dr);
        }
        public static List<UnderwriterSetupModel> EditUnderwriterSetupbyID(int ID, Guid parlourid)
        {
            DataTable dr = UnderwriterSetupDAL.EditUnderwriterSetupbyIDdt(ID, parlourid);
            return FuneralHelper.DataTableMapToList<UnderwriterSetupModel>(dr);
        }



        public static List<BranchModel> GetAllBranch(Guid Parlourid)
        {
            DataTable dr = MembersDAL.GetAllBranchdt(Parlourid);
            return FuneralHelper.DataTableMapToList<BranchModel>(dr);
        }
        public static List<PlanModel> GetAllPlanName(Guid ParlourId)
        {
            DataTable dr = MembersDAL.GetAllPlanNamedt(ParlourId);
            return FuneralHelper.DataTableMapToList<PlanModel>(dr);
        }
        public static List<PolicyModel> GetPlanSubscriptionByPlanId(int pkiPlanID, Guid parlorId)
        {
            DataTable dr = MembersDAL.GetPlanSubscriptionByPlanIddt(pkiPlanID, parlorId);
            return FuneralHelper.DataTableMapToList<PolicyModel>(dr);
        }
        //public static List<PolicyModel> GetPlanSubscriptionByPlanId(int pkiPlanID, Guid parlorId, int UserType,int Age)
        //{
        //    DataTable dr = MembersDAL.GetPlanSubscriptionByPlanIddt(pkiPlanID, parlorId, UserType,Age);
        //    return FuneralHelper.DataTableMapToList<PolicyModel>(dr);
        //}
        public static int GetWaitingPeriodByPlanId(int pkiPlanID)
        {
            DataTable dr = MembersDAL.GetWaitingPeriodByPlanIddt(pkiPlanID);
            if (dr != null && dr.Rows.Count > 0)
            {

                return Convert.ToInt32(dr.Rows[0]["WaitingPeriod"].ToString());
            }
            else return 0;
        }
        public static string GetPlanUnderwriterByPlanId(int pkiPlanID)
        {
            DataTable dr = MembersDAL.GetPlanUnderwriterByPlanIddt(pkiPlanID);
            if (dr != null && dr.Rows.Count > 0)
            {

                return dr.Rows[0]["PlanUnderWriter"].ToString();
            }
            else return string.Empty;
        }

        public static void UpdatePlanUnderwriterByPlanId(int pkiPlanID, string planUnderwriter)
        {
            MembersDAL.UpdatePlanUnderwriterByPlanIddt(pkiPlanID, planUnderwriter);
        }
        public static string GetMemberNumber(Guid ParlourId)
        {
            DataTable dr = MembersDAL.GetMemberNumberdt(ParlourId);
            if (dr != null && dr.Rows.Count > 0)
            {

                return dr.Rows[0]["MemberNo"].ToString();
            }
            else return string.Empty;
        }
        /// <summary>
        /// Get status list
        /// </summary>
        /// <param name="associatedTable"></param>
        /// <returns></returns>
        public static List<StatusModel> GetStatus(string associatedTable)
        {
            DataTable dr = CommonDAL.GetStatus(associatedTable);
            return FuneralHelper.DataTableMapToList<StatusModel>(dr);
        }

        public static List<BankModel> GetBankList()
        {
            DataTable dr = CommonDAL.GetBankList();
            return FuneralHelper.DataTableMapToList<BankModel>(dr);
        }
        public static List<Provinces> GetProvinces()
        {
            DataTable dr = CommonDAL.GetProvinces();
            return FuneralHelper.DataTableMapToList<Provinces>(dr);
        }

        public static List<AccountTypeModel> GetAccountTypeList()
        {
            DataTable dr = CommonDAL.GetAccountTypeList();
            return FuneralHelper.DataTableMapToList<AccountTypeModel>(dr);
        }

        public static List<PolicyModel> GetPlanSubscriptionByPlanIdNewMember(int pkiPlanID)
        {
            DataTable dr = MembersDAL.GetPlanSubscriptionByPlanIdNewMember(pkiPlanID);
            return FuneralHelper.DataTableMapToList<PolicyModel>(dr);
        }
        public static List<PolicyModel> NewGetPlanSubscriptionByPlanIdNewMember(int pkiPlanID, int UserTypeId)
        {
            DataTable dr = MembersDAL.NewGetPlanSubscriptionByPlanIdNewMember(pkiPlanID, UserTypeId);
            return FuneralHelper.DataTableMapToList<PolicyModel>(dr);
        }
        public static List<PolicyModel> GetPolicyDetailsBetweenAge(int pkiPlanID, int Age, int UserType)
        {
            DataTable dr = MembersDAL.GetPolicyDetailsBetweenAge(pkiPlanID, Age, UserType);
            return FuneralHelper.DataTableMapToList<PolicyModel>(dr);
        }
        public static List<UserType> GetUserTypes(bool includeMainMember = true)
        {
            List<UserType> keyValues = new List<UserType>();
            if (includeMainMember)
                keyValues.Add(new UserType { UserTypeId = 1, UserTypeName = "Main Member" });
            keyValues.Add(new UserType { UserTypeId = 2, UserTypeName = "Spouse" });
            keyValues.Add(new UserType { UserTypeId = 3, UserTypeName = "Extended" });
            keyValues.Add(new UserType { UserTypeId = 4, UserTypeName = "Child" });
            keyValues.Add(new UserType { UserTypeId = 5, UserTypeName = "Adult" });
            keyValues.Add(new UserType { UserTypeId = 6, UserTypeName = "Extended Child" });
            return keyValues;
        }
        public static List<UnderwriterModel> GetUnderwriterList(Guid Parlourid)
        {
            DataTable dr = MembersDAL.GetUnderwriterList(Parlourid);
            return FuneralHelper.DataTableMapToList<UnderwriterModel>(dr);
        }
        public static List<SocietyModel> GetAllSocietyesList(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetAllSocietyesList(ParlourId);
            return FuneralHelper.DataTableMapToList<SocietyModel>(dr);
        }
        public static DataSet GetDashboardLableDetails(Guid ParlourId, bool IsAdministrator, bool IsSuperUser, string UserName)
        {
            DataTable dr = ToolsSetingDAL.GetDashboardLableDetails(ParlourId, IsAdministrator, IsSuperUser, UserName);
            DataSet ds = new DataSet();
            ds.Tables.Add(dr);
            return ds;
        }
        public static Dashboard GetDashboardLableDetails(Guid ParlourId, bool IsAdministrator, bool IsSuperUser, string UserName, string Currency)
        {
            Dashboard dashboard = new Dashboard();
            DataTable dr = ToolsSetingDAL.GetDashboardLableDetails(ParlourId, IsAdministrator, IsSuperUser, UserName);
            DataSet ds = new DataSet();
            ds.Tables.Add(dr);

            if (ds.Tables.Count > 0)
            {
                dashboard.TodayTotalPayment = Currency + " " + ds.Tables[0].Rows[0]["TodayPayment"].ToString();
                dashboard.OutstandingPaymentsCount = Currency + " " + ds.Tables[0].Rows[0]["OutstandingCollection"].ToString();
                dashboard.TotalSMSCreditsCount = ds.Tables[0].Rows[0]["SMSBalalnce"].ToString();
            }
            return dashboard;
        }
        public static int SMSTopup_save(ConsumableOrders Model)
        {
            return CommonDAL.SMSTopup_save(Model);
        }
        public static void SendOutstandingMessagesToAll(Guid ParlourId)
        {
            CommonDAL.SendOutstandingMessagesToAll(ParlourId);

        }

        #region Dashboard
        public static DashboardChart GetDashboardChart(Guid ParlourId, bool IsAdministrator, string UserName, bool IsSuperUser)
        {
            var barCharts = GetDashboardBarChart(ParlourId, IsAdministrator, UserName, IsSuperUser);
            var PolicyCHart = PolicyPichart(ParlourId, IsAdministrator, UserName, IsSuperUser);
            var premiumChart = PremiumCollectionChart(ParlourId, IsAdministrator, UserName, IsSuperUser);
            DashboardChart dashboardChart = new DashboardChart();
            #region Bar Chart 
            dashboardChart.ChartTotalCount = barCharts.ChartTotalCount == "" ? "0" : barCharts.ChartTotalCount;
            dashboardChart.ChartLabels = barCharts.ChartLabels == "" ? "0" : barCharts.ChartLabels;
            dashboardChart.ChartData1 = barCharts.ChartData1 == "" ? "0" : barCharts.ChartData1;
            dashboardChart.ChartData2 = barCharts.ChartData2 == "" ? "0" : barCharts.ChartData2;
            dashboardChart.dataCollection = barCharts.dataCollection;
            #endregion
            #region Plolicy Chart 
            dashboardChart.PolicyPieChartTotalCount = PolicyCHart.PolicyPieChartTotalCount == "" ? "0" : PolicyCHart.PolicyPieChartTotalCount;
            dashboardChart.PolicyPieChartLabels = PolicyCHart.PolicyPieChartLabels == "" ? "0" : PolicyCHart.PolicyPieChartLabels;
            dashboardChart.PolicyPieChartData1 = PolicyCHart.PolicyPieChartData1 == "" ? "0" : PolicyCHart.PolicyPieChartData1;
            #endregion
            #region Pie Chart
            dashboardChart.PolicyPremiumPieChartTotalCount = premiumChart.PolicyPremiumPieChartTotalCount == "" ? "0" : premiumChart.PolicyPremiumPieChartTotalCount;
            dashboardChart.PolicyPremiumPieChartLabels = premiumChart.PolicyPremiumPieChartLabels == "" ? "0" : premiumChart.PolicyPremiumPieChartLabels;
            dashboardChart.PolicyPremiumPieChartData = premiumChart.PolicyPremiumPieChartData == "" ? "0" : premiumChart.PolicyPremiumPieChartData;
            #endregion
            return dashboardChart;
        }
        public static DashboardChart GetDashboardBarChart(Guid ParlourId, bool IsAdministrator, string UserName, bool IsSuperUser)
        {
            DashboardChart dashboardChart = new DashboardChart();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand com1 = new SqlCommand();
                com1.CommandType = CommandType.StoredProcedure;
                com1.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com1.CommandText = "GetUserDetailsByChart";
                com1.Parameters.Add(new SqlParameter("@ParlourId", ParlourId));
                com1.Parameters.Add(new SqlParameter("@IsAdmin", IsAdministrator));
                com1.Parameters.Add(new SqlParameter("@UserName", UserName));
                com1.Parameters.Add(new SqlParameter("@IsSuperUser", IsSuperUser));
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
                dashboardChart.ChartTotalCount = Convert.ToString(TotalUserCount);
                string YAxis = string.Join(",", y);
                string XAxis = string.Join(",", x);
                dashboardChart.ChartLabels = XAxis;
                dashboardChart.ChartData1 = YAxis;
                dashboardChart.ChartData2 = YAxis;
                dashboardChart.dataCollection = dt;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return dashboardChart;
        }
        public static DashboardChart PolicyPichart(Guid ParlourId, bool IsAdministrator, string UserName, bool IsSuperUser)
        {
            DashboardChart dashboardChart = new DashboardChart();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand com1 = new SqlCommand();
                com1.CommandType = CommandType.StoredProcedure;
                com1.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com1.CommandText = "PolicyStatusPieChart";
                com1.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
                com1.Parameters.Add(new SqlParameter("@IsAdmin", IsAdministrator));
                com1.Parameters.Add(new SqlParameter("@UserName", UserName));
                com1.Parameters.Add(new SqlParameter("@IsSuperUser", IsSuperUser));
                SqlDataAdapter adp1 = new SqlDataAdapter(com1);
                adp1.Fill(dt);
                string[] x = new string[dt.Rows.Count];
                decimal[] y = new decimal[dt.Rows.Count];
                Int32 TotalPolicy = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
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
                dashboardChart.PolicyPieChartTotalCount = TotalPolicy.ToString();
                string YAxis = string.Join(",", y);
                string XAxis = string.Join(",", x);
                dashboardChart.PolicyPieChartLabels = XAxis;
                dashboardChart.PolicyPieChartData1 = YAxis;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return dashboardChart;
        }
        public static DashboardChart PremiumCollectionChart(Guid ParlourId, bool IsAdministrator, string UserName, bool IsSuperUser)
        {
            DashboardChart dashboardChart = new DashboardChart();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand com1 = new SqlCommand();
                com1.CommandType = CommandType.StoredProcedure;
                com1.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com1.CommandText = "PremiumCollectionChart";
                com1.Parameters.Add(new SqlParameter("@parlourid", ParlourId));
                com1.Parameters.Add(new SqlParameter("@IsAdmin", IsAdministrator));
                com1.Parameters.Add(new SqlParameter("@UserName", UserName));
                com1.Parameters.Add(new SqlParameter("@IsSuperUser", IsSuperUser));
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
                dashboardChart.PolicyPremiumPieChartTotalCount = TotalPremiumCount.ToString();
                string YAxis = string.Join(",", y);
                string XAxis = string.Join(",", x);
                dashboardChart.PolicyPremiumPieChartLabels = XAxis;
                dashboardChart.PolicyPremiumPieChartData = YAxis;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return dashboardChart;
        }
        public static List<BranchModel> BranchByparlourId(Guid parlourid)
        {
            return CommonBAL.GetBranchByParlourId(parlourid);
        }
        #endregion


        public static List<FuneralDocument> GetDocumentList(Guid Parlourid)
        {
            DataTable dr = CommonDAL.GetDocumentList(Parlourid);
            return FuneralHelper.DataTableMapToList<FuneralDocument>(dr);
        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public static List<SecureUsersModel> GetAllUser(Guid parlourid)
        {
            DataTable dr = CommonDAL.GetAllUser(parlourid);
            return FuneralHelper.DataTableMapToList<SecureUsersModel>(dr);
        }
        //public static DataTable GetDataTable_FromGridView(GridView dtg)
        //{
        //    DataTable dt = new DataTable();
        //    if (dtg.HeaderRow != null)
        //    {
        //        for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
        //        {
        //            dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
        //        }
        //    }

        //    foreach (GridViewRow row in dtg.Rows)
        //    {
        //        DataRow dr;
        //        dr = dt.NewRow();
        //        for (int i = 0; i < row.Cells.Count; i++)
        //        {
        //            string value = "";
        //            foreach (Control cc in row.Cells[i].Controls)
        //            {
        //                value = ((TextBox)cc).Text;
        //            }
        //            dr[i] = value.Replace(" ", "").Replace("&nbsp;", "");
        //        }
        //        dt.Rows.Add(dr);
        //    }
        //    return dt;
        //}
    }
}
