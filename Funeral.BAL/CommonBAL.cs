using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Funeral.BAL
{
    public class CommonBAL
    {
        public static List<KeyValue> ColumnList()
        {
            List<KeyValue> keyValues = new List<KeyValue>();
            keyValues.Add(new KeyValue { Key = "CreateDate", Value = "CreateDate", NameText = "CreateDate" });
            keyValues.Add(new KeyValue { Key = "MemberType", Value = "MemberType", NameText = "Relationship Type" });
            keyValues.Add(new KeyValue { Key = "Title", Value = "Title", NameText = "Title" });
            keyValues.Add(new KeyValue { Key = "Full Names", Value = "Full Names", NameText = "Full Names" });
            keyValues.Add(new KeyValue { Key = "Surname", Value = "Surname", NameText = "Surname" });
            keyValues.Add(new KeyValue { Key = "Gender", Value = "Gender", NameText = "Gender" });
            keyValues.Add(new KeyValue { Key = "Active", Value = "Active", NameText = "Active" });
            keyValues.Add(new KeyValue { Key = "ID Number", Value = "ID Number", NameText = "ID Number" });
            keyValues.Add(new KeyValue { Key = "Date Of Birth", Value = "Date Of Birth", NameText = "Date Of Birth" });
            keyValues.Add(new KeyValue { Key = "Telephone", Value = "Telephone", NameText = "Telephone" });
            keyValues.Add(new KeyValue { Key = "Cellphone", Value = "Cellphone", NameText = "Cellphone" });
            keyValues.Add(new KeyValue { Key = "Address1", Value = "Address1", NameText = "Address1" });
            keyValues.Add(new KeyValue { Key = "Address2", Value = "Address2", NameText = "Address2" });
            keyValues.Add(new KeyValue { Key = "Address3", Value = "Address3", NameText = "Address3" });
            keyValues.Add(new KeyValue { Key = "Address4", Value = "Address4", NameText = "Address4" });
            keyValues.Add(new KeyValue { Key = "Code", Value = "Code", NameText = "Code" });
            keyValues.Add(new KeyValue { Key = "MemeberNumber", Value = "MemeberNumber", NameText = "Policy Number" });
            keyValues.Add(new KeyValue { Key = "MemberSociety", Value = "MemberSociety", NameText = "Group Name" });
            keyValues.Add(new KeyValue { Key = "InceptionDate", Value = "InceptionDate", NameText = "InceptionDate" });
            keyValues.Add(new KeyValue { Key = "Claimnumber", Value = "Claimnumber", NameText = "Claimnumber" });
            keyValues.Add(new KeyValue { Key = "PolicyStatus", Value = "PolicyStatus", NameText = "PolicyStatus" });
            keyValues.Add(new KeyValue { Key = "parlourid", Value = "parlourid", NameText = "Parlourid" });
            keyValues.Add(new KeyValue { Key = "Agent", Value = "Agent", NameText = "Agent" });
            keyValues.Add(new KeyValue { Key = "AccountHolder", Value = "AccountHolder", NameText = "AccountHolder" });
            keyValues.Add(new KeyValue { Key = "Bank", Value = "Bank", NameText = "Bank" });
            keyValues.Add(new KeyValue { Key = "BranchCode", Value = "BranchCode", NameText = "BranchCode" });
            keyValues.Add(new KeyValue { Key = "Branch", Value = "Branch", NameText = "Branch" });
            keyValues.Add(new KeyValue { Key = "AccountNumber", Value = "AccountNumber", NameText = "AccountNumber" });
            keyValues.Add(new KeyValue { Key = "AccountType", Value = "AccountType", NameText = "AccountType" });
            keyValues.Add(new KeyValue { Key = "DebitDate", Value = "DebitDate", NameText = "DebitDate" });
            keyValues.Add(new KeyValue { Key = "MemberBranch", Value = "MemberBranch", NameText = "Scheme Name" });
            keyValues.Add(new KeyValue { Key = "CoverDate", Value = "CoverDate", NameText = "CoverDate" });
            keyValues.Add(new KeyValue { Key = "Email", Value = "Email", NameText = "Email" });
            keyValues.Add(new KeyValue { Key = "Citizenship", Value = "Citizenship", NameText = "Citizenship" });
            keyValues.Add(new KeyValue { Key = "Passport", Value = "Passport", NameText = "Passport" });
            keyValues.Add(new KeyValue { Key = "CustomId1", Value = "CustomId1", NameText = "CustomId1" });
            keyValues.Add(new KeyValue { Key = "CustomId2", Value = "CustomId2", NameText = "CustomId2" });
            keyValues.Add(new KeyValue { Key = "CustomId3", Value = "CustomId3", NameText = "CustomId3" });
            keyValues.Add(new KeyValue { Key = "PlanName", Value = "PlanName", NameText = "Plan Name" });
            //keyValues.Add(new KeyValue { Key = "Premium", Value = "Premium", NameText = "Premium" });
            keyValues.Add(new KeyValue { Key = "Premium", Value = "Premium", NameText = "Premium" });
            keyValues.Add(new KeyValue { Key = "Age", Value = "Age", NameText = "Age" });
            keyValues.Add(new KeyValue { Key = "PlanDesc", Value = "PlanDesc", NameText = "PlanDesc" });
            keyValues.Add(new KeyValue { Key = "PlanSubscription", Value = "PlanSubscription", NameText = "PlanSubscription" });
            keyValues.Add(new KeyValue { Key = "Cover", Value = "Cover", NameText = "Cover" });
            keyValues.Add(new KeyValue { Key = "WaitingPeriod", Value = "WaitingPeriod", NameText = "WaitingPeriod" });
            keyValues.Add(new KeyValue { Key = "PolicyLaps", Value = "PolicyLaps", NameText = "PolicyLaps" });
            keyValues.Add(new KeyValue { Key = "PlanUnderwriter", Value = "PlanUnderwriter", NameText = "PlanUnderwriter" });
            keyValues.Add(new KeyValue { Key = "JoiningFee", Value = "JoiningFee", NameText = "JoiningFee" });
            keyValues.Add(new KeyValue { Key = "UnderwriterId", Value = "UnderwriterId", NameText = "UnderwriterId" });
            keyValues.Add(new KeyValue { Key = "AgeBand", Value = "AgeBand", NameText = "AgeBand" });
            keyValues.Add(new KeyValue { Key = "AgeFrom", Value = "AgeFrom", NameText = "AgeFrom" });
            keyValues.Add(new KeyValue { Key = "AgeTo", Value = "AgeTo", NameText = "AgeTo" });
            keyValues.Add(new KeyValue { Key = "UnderwriterCover", Value = "UnderwriterCover", NameText = "UnderwriterCover" });
            keyValues.Add(new KeyValue { Key = "UnderwriterPremium", Value = "UnderwriterPremium", NameText = "UnderwriterPremium" });
            keyValues.Add(new KeyValue { Key = "ReinsurancePremium", Value = "ReinsurancePremium", NameText = "ReinsurancePremium" });
            keyValues.Add(new KeyValue { Key = "OfficePremium", Value = "OfficePremium", NameText = "OfficePremium" });
            keyValues.Add(new KeyValue { Key = "EffectiveDate", Value = "EffectiveDate", NameText = "Effective Date" });
            keyValues.Add(new KeyValue { Key = "StartDate", Value = "StartDate", NameText = "Start Date" });
            keyValues.Add(new KeyValue { Key = "RefNumber", Value = "RefNumber", NameText = "Employee Number" });
            return keyValues;
        }
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
        public static List<PlanModel> GetPlanByParlourId(Guid parlourid)
        {
            DataTable dr = MembersDAL.GetPlanByParlourIddt(parlourid);
            return FuneralHelper.DataTableMapToList<PlanModel>(dr);
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
            keyValues.Add(new UserType { UserTypeId = 7, UserTypeName = "Extended Rider 1" });
            keyValues.Add(new UserType { UserTypeId = 8, UserTypeName = "Extended Rider 2" });
            keyValues.Add(new UserType { UserTypeId = 9, UserTypeName = "Extended Rider 3" });
            keyValues.Add(new UserType { UserTypeId = 10, UserTypeName = "Extended Rider 4" });
            keyValues.Add(new UserType { UserTypeId = 11, UserTypeName = "Extended Rider 5" });           
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

        public static Dashboard GetClaimDashboardLableDetails(Guid ParlourId, bool IsAdministrator, bool IsSuperUser, string UserName, string Currency)
        {
            Dashboard Claimdashboard = new Dashboard();
            DataTable dr = ToolsSetingDAL.GetDashboardLableDetails(ParlourId, IsAdministrator, IsSuperUser, UserName);
            DataSet ds = new DataSet();
            ds.Tables.Add(dr);

            if (ds.Tables.Count > 0)
            {
                Claimdashboard.TodayTotalPayment = Currency + " " + ds.Tables[0].Rows[0]["TodayPayment"].ToString();
                Claimdashboard.OutstandingPaymentsCount = Currency + " " + ds.Tables[0].Rows[0]["OutstandingCollection"].ToString();
                Claimdashboard.TotalSMSCreditsCount = ds.Tables[0].Rows[0]["SMSBalalnce"].ToString();
            }
            return Claimdashboard;
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

        public static DashboardChart GetClaimsDashboardChart(Guid ParlourId, bool IsAdministrator, string UserName, bool IsSuperUser)
        {
            var barCharts = GetClaimsDashboardBarChart(ParlourId, IsAdministrator, UserName, IsSuperUser);
            var PolicyCHart = ClaimPolicyPichart(ParlourId, IsAdministrator, UserName, IsSuperUser);
            var premiumChart = ClaimPremiumCollectionChart(ParlourId, IsAdministrator, UserName, IsSuperUser);
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

        public static DashboardChart GetClaimsDashboardBarChart(Guid ParlourId, bool IsAdministrator, string UserName, bool IsSuperUser)
        {
            DashboardChart dashboardChart = new DashboardChart();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand com1 = new SqlCommand();
                com1.CommandType = CommandType.StoredProcedure;
                com1.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com1.CommandText = "GetUserDetailsByClaimsChart";
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

        public static DashboardChart ClaimPolicyPichart(Guid ParlourId, bool IsAdministrator, string UserName, bool IsSuperUser)
        {
            DashboardChart dashboardChart = new DashboardChart();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand com1 = new SqlCommand();
                com1.CommandType = CommandType.StoredProcedure;
                com1.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com1.CommandText = "PolicyStatusPieClaimsChart";
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

        public static DashboardChart ClaimPremiumCollectionChart(Guid ParlourId, bool IsAdministrator, string UserName, bool IsSuperUser)
        {
            DashboardChart dashboardChart = new DashboardChart();
            try
            {
                DataTable dt = new DataTable();
                SqlCommand com1 = new SqlCommand();
                com1.CommandType = CommandType.StoredProcedure;
                com1.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString);
                com1.CommandText = "PremiumCollectionClaimsChart";
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
        public static BankDetails GetBankDetails_ByParlourId(Guid ParlourId)
        {
            DataTable dr = CommonDAL.GetBankDetails_ByParlourId(ParlourId);
            return FuneralHelper.DataTableMapToList<BankDetails>(dr).FirstOrDefault();
        }
        public static List<ClaimReasonModel> GetClaimReasonByClaimStatus(string ClaimStatus, Guid Parlourid)
        {
            DataTable dr = CommonDAL.GetClaimReasonByClaimStatus(ClaimStatus, Parlourid);
            return FuneralHelper.DataTableMapToList<ClaimReasonModel>(dr);
        }
        public static int SaveAudit(string Username, Guid ParlourId, string AuditDesc)
        {
            return CommonDAL.AddAudit(Username, ParlourId, AuditDesc);
        }
    }
}
