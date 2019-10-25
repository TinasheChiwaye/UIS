using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;

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
        public static List<UserType> GetUserTypes(bool includeMainMember = true)
        {
            List<UserType> keyValues = new List<UserType>();
            if (includeMainMember)
                keyValues.Add(new UserType { UserTypeId = 1, UserTypeName = "Main Member" });

            keyValues.Add(new UserType { UserTypeId = 2, UserTypeName = "Spouse" });
            keyValues.Add(new UserType { UserTypeId = 3, UserTypeName = "Extended" });
            keyValues.Add(new UserType { UserTypeId = 4, UserTypeName = "Child" });
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
        public static DataSet GetDashboardLableDetails(Guid ParlourId,bool IsAdministrator,bool IsSuperUser,string UserName)
        {
            DataTable dr = ToolsSetingDAL.GetDashboardLableDetails(ParlourId,IsAdministrator,IsSuperUser,UserName);
            DataSet ds = new DataSet();
            ds.Tables.Add(dr);
            return ds;
        }
        public static int SMSTopup_save(ConsumableOrders Model)
        {
            return CommonDAL.SMSTopup_save(Model);
        }
        public static void SendOutstandingMessagesToAll(Guid ParlourId)
        {
            CommonDAL.SendOutstandingMessagesToAll(ParlourId);

        }
    }
}
