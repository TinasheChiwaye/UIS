using Funeral.Model;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Funeral.DAL
{
    public class MembersDAL
    {

        public static int SaveMember(MembersModel model)
        {
            try
            {
                AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
                string query = "SaveMembers";
                DbParameter[] ObjParam = new DbParameter[56];
                ObjParam[0] = new DbParameter("@pkiMemberID", DbParameter.DbType.Int, 0, model.pkiMemberID);
                ObjParam[1] = new DbParameter("@CreateDate", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
                ObjParam[2] = new DbParameter("@MemberType", DbParameter.DbType.NVarChar, 0, model.MemberType);
                ObjParam[3] = new DbParameter("@Title", DbParameter.DbType.NVarChar, 0, model.Title);
                ObjParam[4] = new DbParameter("@FullNames", DbParameter.DbType.NVarChar, 0, model.FullNames);
                ObjParam[5] = new DbParameter("@Surname", DbParameter.DbType.NVarChar, 0, model.Surname);
                ObjParam[6] = new DbParameter("@Gender", DbParameter.DbType.NVarChar, 0, model.Gender);
                ObjParam[7] = new DbParameter("@IDNumber", DbParameter.DbType.NVarChar, 0, model.IDNumber);
                // ObjParam[8] = new DbParameter("@DateOfBirth", DbParameter.DbType.DateTime, 0, model.DateOfBirth);
                ObjParam[8] = new DbParameter("@DateOfBirth", DbParameter.DbType.DateTime, 0, model.DateOfBirth);
                ObjParam[9] = new DbParameter("@Telephone", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Telephone) ? (object)DBNull.Value : (object)model.Telephone);
                ObjParam[10] = new DbParameter("@Cellphone", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Cellphone) ? (object)DBNull.Value : (object)model.Cellphone);
                ObjParam[11] = new DbParameter("@Address1", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Address1) ? (object)DBNull.Value : (object)model.Address1);
                ObjParam[12] = new DbParameter("@Address2", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Address2) ? (object)DBNull.Value : (object)model.Address2);
                ObjParam[13] = new DbParameter("@Address3", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Address3) ? (object)DBNull.Value : (object)model.Address3);
                ObjParam[14] = new DbParameter("@Address4", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Address4) ? (object)DBNull.Value : (object)model.Address4);
                ObjParam[15] = new DbParameter("@Code", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Code) ? (object)DBNull.Value : (object)model.Code);
                ObjParam[16] = new DbParameter("@MemeberNumber", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.MemeberNumber) ? (object)DBNull.Value : (object)model.MemeberNumber);
                ObjParam[17] = new DbParameter("@MemberSociety", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.MemberSociety) ? (object)DBNull.Value : (object)model.MemberSociety);
                ObjParam[18] = new DbParameter("@fkiPlanID", DbParameter.DbType.Int, 0, model.fkiPlanID);
                ObjParam[19] = new DbParameter("@Active", DbParameter.DbType.Bit, 0, model.Active);
                
                ObjParam[20] = new DbParameter("@InceptionDate", DbParameter.DbType.DateTime, 0, model.InceptionDate);
                ObjParam[21] = new DbParameter("@Claimnumber", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Claimnumber) ? (object)DBNull.Value : (object)model.Claimnumber);
                ObjParam[22] = new DbParameter("@PolicyStatus", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.PolicyStatus) ? (object)DBNull.Value : (object)model.PolicyStatus);
                ObjParam[23] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
                ObjParam[24] = new DbParameter("@Agent", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Agent) ? (object)DBNull.Value : (object)model.Agent);
                ObjParam[25] = new DbParameter("@AccountHolder", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.AccountHolder) ? (object)DBNull.Value : (object)model.AccountHolder);
                ObjParam[26] = new DbParameter("@Bank", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Bank) ? (object)DBNull.Value : (object)model.Bank);
                ObjParam[27] = new DbParameter("@BranchCode", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.BranchCode) ? (object)DBNull.Value : (object)model.BranchCode);
                ObjParam[28] = new DbParameter("@BankBranch", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Branch) ? (object)DBNull.Value : (object)model.Branch);
                ObjParam[29] = new DbParameter("@AccountNumber", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.AccountNumber) ? (object)DBNull.Value : (object)model.AccountNumber);
                ObjParam[30] = new DbParameter("@AccountType", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.AccountType) ? (object)DBNull.Value : (object)model.AccountType);
                //   ObjParam[31] = new DbParameter("@DebitDate", DbParameter.DbType.DateTime, 0, (model.DebitDate == DateTime.MaxValue ? string.Empty : model.DebitDate.ToString()));
                ObjParam[31] = new DbParameter("@DebitDate", DbParameter.DbType.DateTime, 0, model.DebitDate);
                ObjParam[32] = new DbParameter("@MemberBranch", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.MemberBranch) ? (object)DBNull.Value : (object)model.MemberBranch);
                ObjParam[33] = new DbParameter("@CoverDate", DbParameter.DbType.DateTime, 0, model.CoverDate);
                //ObjParam[34] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
                ObjParam[34] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
                ObjParam[35] = new DbParameter("@Email", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Email) ? (object)DBNull.Value : (object)model.Email);

                //Not Supplied Yet
                ObjParam[36] = new DbParameter("@Province", DbParameter.DbType.NVarChar, 0, string.Empty);
                ObjParam[37] = new DbParameter("@Policyname", DbParameter.DbType.NVarChar, 0, string.Empty);
                ObjParam[38] = new DbParameter("@PolicyPremium", DbParameter.DbType.NVarChar, 0, string.Empty);
                ObjParam[39] = new DbParameter("@PolicyNo", DbParameter.DbType.NVarChar, 0, string.Empty);
                ObjParam[40] = new DbParameter("@Society", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.MemberSociety) ? (object)DBNull.Value : (object)model.MemberSociety);
                ObjParam[41] = new DbParameter("@PolicyUnderwrittenDate", DbParameter.DbType.NVarChar, 0, string.Empty);
                ObjParam[42] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
                ObjParam[43] = new DbParameter("@Citizenship", DbParameter.DbType.VarChar, 0, String.IsNullOrWhiteSpace(model.Citizenship) ? (object)DBNull.Value : (object)model.Citizenship);
                ObjParam[44] = new DbParameter("@Passport", DbParameter.DbType.VarChar, 0, String.IsNullOrWhiteSpace(model.Passport) ? (object)DBNull.Value : (object)model.Passport);
                ObjParam[45] = new DbParameter("@EasyPayNo", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.EasyPayNo) ? (object)DBNull.Value : (object)model.EasyPayNo);
                ObjParam[46] = new DbParameter("@pkiAdditionalMemberInfo", DbParameter.DbType.UniqueIdentifier, 0, model.pkiAdditionalMemberInfo);
                ObjParam[47] = new DbParameter("@StartDate", DbParameter.DbType.DateTime, 0, model.StartDate);

                ObjParam[48] = new DbParameter("@CustomId1", DbParameter.DbType.Int, 0, model.CustomId1);
                ObjParam[49] = new DbParameter("@CustomId2", DbParameter.DbType.Int, 0, model.CustomId2);
                ObjParam[50] = new DbParameter("@CustomId3", DbParameter.DbType.Int, 0, model.CustomId3);
                //New data feilds [Tab 2] - Postal Address
                ObjParam[51] = new DbParameter("@Address1_Post", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Address1_Post) ? (object)DBNull.Value : (object)model.Address1_Post);
                ObjParam[52] = new DbParameter("@Address2_Post", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Address2_Post) ? (object)DBNull.Value : (object)model.Address2_Post);
                ObjParam[53] = new DbParameter("@Address3_Post", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Address3_Post) ? (object)DBNull.Value : (object)model.Address3_Post);
                ObjParam[54] = new DbParameter("@Address4_Post", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Address4_Post) ? (object)DBNull.Value : (object)model.Address4_Post);
                ObjParam[55] = new DbParameter("@Code_Post", DbParameter.DbType.NVarChar, 0, String.IsNullOrWhiteSpace(model.Code_Post) ? (object)DBNull.Value : (object)model.Code_Post);

                return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static int DeleteMember(int id)
        {
            //model.InceptionDate = System.DateTime.Now;
            //model.LastModified = System.DateTime.Now;
            //model.DebitDate = System.DateTime.Now;
            //model.CoverDate = System.DateTime.Now;

            string query = "DeleteMembers";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiMemberID", DbParameter.DbType.Int, 0, id);


            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static int MemberDeleteLogical(int id, string UserName)
        {
            string query = "update Members set IsDeleted = 1, DeletedDate=@DateTime, DeletedBy=@UserName where pkiMemberID=@pkiMemberID";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@pkiMemberID", DbParameter.DbType.Int, 0, id);
            ObjParam[1] = new DbParameter("@UserName", DbParameter.DbType.NVarChar, 0, UserName);
            ObjParam[2] = new DbParameter("@DateTime", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }

        public static SqlDataReader GetMemberByID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "MemberSelect", ObjParam);
        }

        public static DataTable GetMemberByIDdt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberSelect", ObjParam);
        }

        public static SqlDataReader GetMemberByIDNum(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "MemberSelectByIdNum", ObjParam);
        }

        public static DataTable GetMemberByIDNumdt(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberSelectByIdNum", ObjParam);
        }

        public static DataTable GetMemberByIDNumber(string ID, Guid ParlourId, int PlanId)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[2] = new DbParameter("@PlanId", DbParameter.DbType.Int, 0, PlanId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberSelectByIdNumber", ObjParam);
        }

        public static SqlDataReader GetDependencByIDNum(string ID, Guid ParlourId, int MemberID)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[2] = new DbParameter("@fkiMemberID", DbParameter.DbType.Int, 0, MemberID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetDependencByIDNum", ObjParam);
        }

        public static DataTable GetDependencByIDNumdt(string ID, Guid ParlourId, int MemberID)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[2] = new DbParameter("@fkiMemberID", DbParameter.DbType.Int, 0, MemberID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetDependencByIDNum", ObjParam);
        }

        public static SqlDataReader GetAllMembers(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "MemberSelectAllByPage", ObjParam);
        }
        //public static DataSet GetAllMembersdt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, string status)
        //{
        //    DbParameter[] ObjParam = new DbParameter[7];
        //    try
        //    {
        //        ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
        //        ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
        //        ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, (Keyword == null) ? string.Empty : Keyword);
        //        ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
        //        ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
        //        ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
        //        ObjParam[6] = new DbParameter("@Status", DbParameter.DbType.VarChar, 0, status);
        //        if (ParlourId == Guid.Empty)
        //        {
        //            return DbConnection.GetDataSet(CommandType.StoredProcedure, "MemberSelectAll_WithoutParlour", ObjParam);
        //        }
        //        else
        //        {
        //            return DbConnection.GetDataSet(CommandType.StoredProcedure, "MemberSelectAllByPage", ObjParam);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public static DataSet GetAllMembersdt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, string status, string BookName)
        {
            DbParameter[] ObjParam = new DbParameter[8];
            try
            {
                ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
                ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
                ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, (Keyword == null) ? string.Empty : Keyword);
                ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
                ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
                ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
                ObjParam[6] = new DbParameter("@Status", DbParameter.DbType.VarChar, 0, status);
                ObjParam[7] = new DbParameter("@BookName", DbParameter.DbType.NVarChar, 0, BookName);

                if (ParlourId == Guid.Empty)
                {
                    return DbConnection.GetDataSet(CommandType.StoredProcedure, "MemberSelectAll_WithoutParlour", ObjParam);
                }
                else
                {
                    return DbConnection.GetDataSet(CommandType.StoredProcedure, "MemberSelectAllByPagedt_Test", ObjParam);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static SqlDataReader GetPolicyByParlourId(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetpolicyNamesListByParlourId", ObjParam);
        }
        public static DataTable GetPolicyByParlourIddt(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetpolicyNamesListByParlourId", ObjParam);
        }


        public static SqlDataReader GetFamilyDependencyByMemberID(Guid Parlourid, int MemberId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[1] = new DbParameter("@MemberId", DbParameter.DbType.Int, 0, MemberId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GET_Dependent_And_Extended_Family", ObjParam);
        }

        public static DataTable GetFamilyDependencyByMemberIDdt(Guid Parlourid, int MemberId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[1] = new DbParameter("@MemberId", DbParameter.DbType.Int, 0, MemberId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GET_Dependent_And_Extended_Family", ObjParam);
        }

        public static SqlDataReader GetInvoicesByMemberID(Guid Parlourid, int MemberId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[1] = new DbParameter("@MemberId", DbParameter.DbType.VarChar, 0, MemberId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetInvoices_new", ObjParam);
        }

        public static DataTable GetInvoicesByMemberIDdt(Guid Parlourid, int MemberId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[1] = new DbParameter("@MemberId", DbParameter.DbType.VarChar, 0, MemberId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetInvoices_new", ObjParam);
        }

        public static SqlDataReader GetSocietyByParlourId(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SocietyByparlourId", ObjParam);
        }
        public static DataTable GetSocietyByParlourIddt(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SocietyByparlourId", ObjParam);
        }

        public static SqlDataReader GetAllSociety(Guid parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourid);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "getAllSociety", ObjParam);
        }

        public static DataTable GetAllSocietydt(Guid parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "getAllSociety", ObjParam);
        }
        public static SqlDataReader GetBranchByParlourId(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "BranchByparlourId", ObjParam);
        }
        public static DataTable GetBranchByParlourIddt(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "BranchByparlourId", ObjParam);
        }

        public static SqlDataReader GetAllBranch(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "rptAllBranch", ObjParam);
        }
        public static DataTable GetAllBranchdt(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "rptAllBranch", ObjParam);
        }

        public static SqlDataReader GetAllPlanName(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "rptAllPlanName", ObjParam);
        }

        public static DataTable GetAllPlanNamedt(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "rptAllPlanName", ObjParam);
        }

        public static DataTable GetPlanSubscriptionByPlanIdNewMember(int pkiPlanID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, pkiPlanID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetPlanSubscriptionByPlanIdNewMember", ObjParam);
        }
        public static DataTable NewGetPlanSubscriptionByPlanIdNewMember(int pkiPlanID, int UserTypeId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, pkiPlanID);
            ObjParam[1] = new DbParameter("@UserTypeId", DbParameter.DbType.Int, 0, UserTypeId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "NewGetPlanSubscriptionByPlanIdNewMember", ObjParam);
        }
        public static DataTable GetPolicyDetailsBetweenAge(int pkiPlanID, int Age, int UserTypeId)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, pkiPlanID);
            ObjParam[1] = new DbParameter("@Age", DbParameter.DbType.Int, 0, Age);
            ObjParam[2] = new DbParameter("@UserTypeId", DbParameter.DbType.Int, 0, UserTypeId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetPolicyDetailsBetweenAge_NEW", ObjParam);
        }
        public static DataTable GetPlanSubscriptionByPlanIddt(int pkiPlanID, Guid parlorId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, pkiPlanID);
            ObjParam[1] = new DbParameter("@ParlouId", DbParameter.DbType.UniqueIdentifier, 0, parlorId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetPlanSubscriptionByPlanId", ObjParam);
        }
        //public static DataTable GetPlanSubscriptionByPlanIddt(int pkiPlanID, Guid parlorId, int UserType, int Age )
        //{
        //    DbParameter[] ObjParam = new DbParameter[4];
        //    ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, pkiPlanID);
        //    ObjParam[1] = new DbParameter("@ParlouId", DbParameter.DbType.UniqueIdentifier, 0, parlorId);
        //    ObjParam[2] = new DbParameter("@UserType", DbParameter.DbType.Int, 0, UserType);
        //    ObjParam[3] = new DbParameter("@Age", DbParameter.DbType.Int, 0, Age);
        //    return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetPolicyDetailsBetweenAge_NEW", ObjParam);
        //}

        public static SqlDataReader GetWaitingPeriodByPlanId(int pkiPlanID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, pkiPlanID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetWaitingPeriodByPlanId", ObjParam);
        }

        public static DataTable GetWaitingPeriodByPlanIddt(int pkiPlanID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, pkiPlanID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetWaitingPeriodByPlanId", ObjParam);
        }

        public static SqlDataReader GetPlanUnderwriterByPlanId(int pkiPlanID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, pkiPlanID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetPlanUnderwriterByPlanId", ObjParam);
        }

        public static DataTable GetPlanUnderwriterByPlanIddt(int pkiPlanID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, pkiPlanID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetPlanUnderwriterByPlanId", ObjParam);
        }

        public static DataTable UpdatePlanUnderwriterByPlanIddt(int pkiPlanID, string planUnderwriter)
        {
            DbParameter[] ObjParam = new DbParameter[2];

            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, pkiPlanID);
            ObjParam[1] = new DbParameter("@PlanUnderwriter", DbParameter.DbType.VarChar, 1, planUnderwriter);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "UpdatePlanUnderwriterByPlanId", ObjParam);
        }

        public static SqlDataReader GetMemberNumber(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "ReturnmemberNumber", ObjParam);
        }

        public static DataTable GetMemberNumberdt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "ReturnmemberNumber", ObjParam);
        }

        public static int NotesSaveMember(MemberNotesModel ModelNotes)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@Notes", DbParameter.DbType.VarChar, 0, ModelNotes.Notes);
            ObjParam[1] = new DbParameter("@fkiMemberID", DbParameter.DbType.Int, 0, ModelNotes.fkiMemberID);
            ObjParam[2] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, ModelNotes.ModifiedUser);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "AddMemberNotes", ObjParam));

        }
        public static SqlDataReader MemberNotesByMemberID(int MemberID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@MemberID", DbParameter.DbType.Int, 0, MemberID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "MemberNotesByMemberID", ObjParam);
        }

        public static DataTable MemberNotesByMemberIDdt(int MemberID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@MemberID", DbParameter.DbType.Int, 0, MemberID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberNotesByMemberID", ObjParam);
        }

        //edit note
        public static SqlDataReader MemberNotesBypkiNoteID(int pkiNoteID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiNoteID", DbParameter.DbType.Int, 0, pkiNoteID);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "MemberNotesBypkiNoteID", ObjParam));
        }

        public static DataTable MemberNotesBypkiNoteIDdt(int pkiNoteID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiNoteID", DbParameter.DbType.Int, 0, pkiNoteID);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberNotesBypkiNoteID", ObjParam));
        }

        public static int NoteUpdateMember(MemberNotesModel ModelNotes)
        {
            DbParameter[] ObjParam = new DbParameter[5];
            ObjParam[0] = new DbParameter("@Notes", DbParameter.DbType.VarChar, 0, ModelNotes.Notes);
            ObjParam[1] = new DbParameter("@fkiMemberID", DbParameter.DbType.Int, 0, ModelNotes.fkiMemberID);
            ObjParam[2] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, ModelNotes.ModifiedUser);
            ObjParam[3] = new DbParameter("@pkiNoteID", DbParameter.DbType.Int, 0, ModelNotes.pkiNoteID);
            ObjParam[4] = new DbParameter("@LastModifiedDate", DbParameter.DbType.DateTime, 0, ModelNotes.LastModified);
            return (Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "UpdateMemberNotes", ObjParam)));
        }


        /// <summary>
        ///SupportDocuments
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public static SqlDataReader SelectSupportDocumentsByMemberId(int MemberId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@MemberId", DbParameter.DbType.Int, 0, MemberId);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectSupportDocumentsByMemberId", ObjParam));
        }
        public static DataTable SelectSupportDocumentsByMemberIddt(int MemberId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@MemberId", DbParameter.DbType.Int, 0, MemberId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectSupportDocumentsByMemberId", ObjParam));
        }
        public static SqlDataReader SelectSupportDocumentsById(int DocumentId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPictureID", DbParameter.DbType.Int, 0, DocumentId);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectSupportDocumentsById", ObjParam));
        }

        public static DataTable SelectSupportDocumentsByIddt(int DocumentId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPictureID", DbParameter.DbType.Int, 0, DocumentId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectSupportDocumentsById", ObjParam));
        }
        public static int SaveSupportDocument(SupportedDocumentModel model)
        {
            DbParameter[] ObjParam = new DbParameter[9];
            ObjParam[0] = new DbParameter("@ImageName", DbParameter.DbType.VarChar, 0, model.ImageName);
            ObjParam[1] = new DbParameter("@ImageFile", DbParameter.DbType.Image, 0, model.ImageFile);
            ObjParam[2] = new DbParameter("@fkiMemberID", DbParameter.DbType.Int, 0, model.fkiMemberID);
            ObjParam[3] = new DbParameter("@CreateDate", DbParameter.DbType.DateTime, 0, model.CreateDate);
            ObjParam[4] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[5] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[6] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, model.ModifiedUser);
            ObjParam[7] = new DbParameter("@DocContentType", DbParameter.DbType.VarChar, 0, model.DocContentType);
            ObjParam[8] = new DbParameter("@DocType", DbParameter.DbType.Int, 0, model.DocType);
            //SqlCommand cmd = new SqlCommand();
            //cmd.Parameters.AddWithValue("@ImageName", model.ImageName);
            //cmd.Parameters.AddWithValue("@fkiMemberID", model.fkiMemberID);
            //cmd.Parameters.AddWithValue("@CreateDate", model.CreateDate);
            //cmd.Parameters.AddWithValue("@parlourid", model.parlourid);
            //cmd.Parameters.AddWithValue("@LastModified", model.LastModified);
            //cmd.Parameters.AddWithValue("@ModifiedUser", model.ModifiedUser);
            //cmd.Parameters.AddWithValue("@ImageFile", model.ImageFile);
            //cmd.CommandText = "SaveSupportedDocument";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Connection = new SqlConnection(DbConnection.sqlConnectionstring);
            //cmd.Connection.Open();
            //cmd.ExecuteScalar();
            //cmd.Connection.Close();
            //return 1;
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "SaveSupportedDocument", ObjParam));
        }


        //product

        public static SqlDataReader selectProductName(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectProductName", ObjParam));
        }

        public static DataTable selectProductNamedt(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectProductName", ObjParam));
        }
        public static SqlDataReader MemberListBind(Guid pkiProductID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiProductID", DbParameter.DbType.UniqueIdentifier, 0, pkiProductID);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "MemberAddonSelect", ObjParam));
        }

        public static DataTable MemberListBinddt(Guid pkiProductID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiProductID", DbParameter.DbType.UniqueIdentifier, 0, pkiProductID);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberAddonSelect", ObjParam));
        }

        public static SqlDataReader GetDistinctPolicyStatusList()
        {

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetDistinctPolicyStatusList");
        }

        public static DataTable GetDistinctPolicyStatusListdt()
        {

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetDistinctPolicyStatusList");
        }

        public static int SaveAddonProducts(MemberAddonProductsModel ModalProduct)
        {
            DbParameter[] ObjParam = new DbParameter[10];
            ObjParam[0] = new DbParameter("@DateCreated", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            // ObjParam[1] = new DbParameter("@UserID", DbParameter.DbType.VarChar, 0, ModalProduct.UserID);
            ObjParam[1] = new DbParameter("@fkiMemberid", DbParameter.DbType.Int, 0, ModalProduct.fkiMemberid);
            ObjParam[2] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            ObjParam[3] = new DbParameter("@ProductCost", DbParameter.DbType.Decimal, 0, ModalProduct.ProductCost);
            //  ObjParam[5] = new DbParameter("@ProductName", DbParameter.DbType.VarChar, 0, ModalProduct.ProductName);
            ObjParam[4] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.parlourid);
            ObjParam[5] = new DbParameter("@fkiProductID", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.fkiProductID);
            ObjParam[6] = new DbParameter("@UserId", DbParameter.DbType.VarChar, 0, ModalProduct.UserID);
            ObjParam[7] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, ModalProduct.ModifiedUser);
            ObjParam[8] = new DbParameter("@Deleted", DbParameter.DbType.Int, 0, ModalProduct.Deleted);
            ObjParam[9] = new DbParameter("@pkiMemberProductID", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.pkiMemberProductID);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "AddMemberAddonProducts", ObjParam));
        }
        public static int NewSaveAddonProducts(MemberAddonProductsModel ModalProduct)
        {
            DbParameter[] ObjParam = new DbParameter[11];
            ObjParam[0] = new DbParameter("@DateCreated", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            ObjParam[1] = new DbParameter("@fkiMemberid", DbParameter.DbType.Int, 0, ModalProduct.fkiMemberid);
            ObjParam[2] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            ObjParam[3] = new DbParameter("@ProductCost", DbParameter.DbType.Decimal, 0, ModalProduct.ProductCost == null ? 0 : ModalProduct.ProductCost);
            ObjParam[4] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.parlourid);
            ObjParam[5] = new DbParameter("@fkiProductID", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.fkiProductID);
            ObjParam[6] = new DbParameter("@UserId", DbParameter.DbType.VarChar, 0, ModalProduct.UserID);
            ObjParam[7] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, ModalProduct.ModifiedUser);
            ObjParam[8] = new DbParameter("@Deleted", DbParameter.DbType.Int, 0, ModalProduct.Deleted);
            ObjParam[9] = new DbParameter("@pkiMemberProductID", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.pkiMemberProductID);
            ObjParam[10] = new DbParameter("@DependencyType", DbParameter.DbType.Int, 0, ModalProduct.DependencyType);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "NewAddMemberAddonProducts", ObjParam));
        }
        public static int NewUpdateAddonProducts(MemberAddonProductsModel ModalProduct)
        {
            DbParameter[] ObjParam = new DbParameter[11];

            ObjParam[0] = new DbParameter("@DateCreated", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            ObjParam[1] = new DbParameter("@fkiMemberid", DbParameter.DbType.Int, 0, ModalProduct.fkiMemberid);
            ObjParam[2] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            ObjParam[3] = new DbParameter("@ProductCost", DbParameter.DbType.Decimal, 0, ModalProduct.ProductCost);
            ObjParam[4] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.parlourid);
            ObjParam[5] = new DbParameter("@fkiProductID", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.fkiProductID);
            ObjParam[6] = new DbParameter("@UserId", DbParameter.DbType.VarChar, 0, ModalProduct.UserID);
            ObjParam[7] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, ModalProduct.ModifiedUser);
            ObjParam[8] = new DbParameter("@Deleted", DbParameter.DbType.Int, 0, ModalProduct.Deleted);
            ObjParam[9] = new DbParameter("@pkiMemberProductID", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.pkiMemberProductID);
            ObjParam[10] = new DbParameter("@DependencyType", DbParameter.DbType.Int, 0, ModalProduct.DependencyType);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "NewAddMemberAddonProducts", ObjParam));
        }

        public static SqlDataReader GetAddonProducts(int fkiMemberid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@fkiMemberid", DbParameter.DbType.Int, 0, fkiMemberid);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectMemberAddonProducts", ObjParam);
        }
        public static DataTable GetAddonProductsdt(int fkiMemberid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@fkiMemberid", DbParameter.DbType.Int, 0, fkiMemberid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectMemberAddonProducts", ObjParam);
        }
        public static DataTable GetAddonProductsList(int fkiMemberid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@fkiMemberid", DbParameter.DbType.Int, 0, fkiMemberid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberAddonProductsList", ObjParam);
        }

        public static void DeleteAddonProduct(Guid pkiMemberProductID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiMemberProductID", DbParameter.DbType.UniqueIdentifier, 0, pkiMemberProductID);
            DbConnection.GetDataReader(CommandType.StoredProcedure, "MemberAddonProductDelete", ObjParam);
        }

        public static void DeleteAddonProductdt(Guid pkiMemberProductID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiMemberProductID", DbParameter.DbType.UniqueIdentifier, 0, pkiMemberProductID);
            DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberAddonProductDelete", ObjParam);
        }


        public static SqlDataReader SelectMembarAddonProductBypkiMemberProductID(Guid pkiMemberProductID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiMemberProductID", DbParameter.DbType.UniqueIdentifier, 0, pkiMemberProductID);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectMembarAddonProductBypkiMemberProductID", ObjParam));
        }

        public static DataTable SelectMembarAddonProductBypkiMemberProductIDdt(Guid pkiMemberProductID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiMemberProductID", DbParameter.DbType.UniqueIdentifier, 0, pkiMemberProductID);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectMembarAddonProductBypkiMemberProductID", ObjParam));
        }

        public static int AddonProductUpdateMember(MemberAddonProductsModel ModelProduct) //Update
        {
            DbParameter[] ObjParam = new DbParameter[5];
            // ObjParam[0] = new DbParameter("@DateCreated", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            // ObjParam[1] = new DbParameter("@UserID", DbParameter.DbType.VarChar, 0, ModalProduct.UserID);
            ObjParam[0] = new DbParameter("@pkiMemberProductID", DbParameter.DbType.UniqueIdentifier, 0, ModelProduct.pkiMemberProductID);
            ObjParam[1] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            ObjParam[2] = new DbParameter("@ProductCost", DbParameter.DbType.Decimal, 0, ModelProduct.ProductCost);
            //  ObjParam[5] = new DbParameter("@ProductName", DbParameter.DbType.VarChar, 0, ModalProduct.ProductName);
            //  ObjParam[4] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.parlourid);
            ObjParam[3] = new DbParameter("@fkiProductID", DbParameter.DbType.UniqueIdentifier, 0, ModelProduct.fkiProductID);
            // ObjParam[6] = new DbParameter("@UserId", DbParameter.DbType.VarChar, 0, ModalProduct.UserID);
            ObjParam[4] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, ModelProduct.ModifiedUser);
            // ObjParam[8] = new DbParameter("@Deleted", DbParameter.DbType.Int, 0, ModalProduct.Deleted);         
            return (Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "UpdateAddonProduct", ObjParam)));
        }
        public static int NewAddonProductUpdateMember(MemberAddonProductsModel ModelProduct) //Update
        {
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pkiMemberProductID", DbParameter.DbType.UniqueIdentifier, 0, ModelProduct.pkiMemberProductID);
            ObjParam[1] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            ObjParam[2] = new DbParameter("@ProductCost", DbParameter.DbType.Decimal, 0, ModelProduct.ProductCost);
            ObjParam[3] = new DbParameter("@fkiProductID", DbParameter.DbType.UniqueIdentifier, 0, ModelProduct.fkiProductID);
            ObjParam[4] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, ModelProduct.ModifiedUser);
            ObjParam[5] = new DbParameter("@DependencyType", DbParameter.DbType.Int, 0, ModelProduct.DependencyType);
            return (Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "NewUpdateAddonProduct", ObjParam)));
        }
        public static double SumOfPremium(int fkiMemberid, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@MemberId", DbParameter.DbType.Int, 0, fkiMemberid);
            return Convert.ToDouble(DbConnection.GetScalarValue(CommandType.StoredProcedure, "TotalProductSelectAll", ObjParam));
        }

        public static SqlDataReader GetCountry()
        {
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectAllCompany");
        }
        public static DataTable GetCountrydt()
        {
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectAllCompany");
        }

        #region Familydependencty
        public static int SaveFamilyDependency(FamilyDependencyModel model)
        {
            DbParameter[] ObjParam = new DbParameter[17];
            ObjParam[0] = new DbParameter("@FullName", DbParameter.DbType.VarChar, 0, model.FullName);
            // ObjParam[1] = new DbParameter("@ImageFile", SqlDbType.Binary , 1000, model.ImageFile);
            ObjParam[1] = new DbParameter("@Surname", DbParameter.DbType.VarChar, 0, model.Surname);
            ObjParam[2] = new DbParameter("@IDNumber", DbParameter.DbType.VarChar, 0, model.IDNumber);
            ObjParam[3] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[4] = new DbParameter("@DependencyType", DbParameter.DbType.VarChar, 0, model.DependencyType);
            ObjParam[5] = new DbParameter("@fkiMemberID", DbParameter.DbType.Int, 0, model.MemberId);
            ObjParam[6] = new DbParameter("@Age", DbParameter.DbType.Int, 0, model.Age);
            ObjParam[7] = new DbParameter("@CoverDate", DbParameter.DbType.DateTime, 0, model.CoverDate);
            ObjParam[8] = new DbParameter("@InceptionDate", DbParameter.DbType.DateTime, 0, model.InceptionDate);
            ObjParam[9] = new DbParameter("@Premium", DbParameter.DbType.Decimal, 0, model.Premium);
            ObjParam[10] = new DbParameter("@Relationship", DbParameter.DbType.Int, 0, model.Relationship);
            ObjParam[11] = new DbParameter("@DateOfBirth", DbParameter.DbType.DateTime, 0, model.DateOfBirth);
            ObjParam[12] = new DbParameter("@Gender", DbParameter.DbType.NVarChar, 0, model.Gender);
            ObjParam[13] = new DbParameter("@StartDate", DbParameter.DbType.DateTime, 0, model.StartDate);
            ObjParam[14] = new DbParameter("@DependentStatus", DbParameter.DbType.NVarChar, 0, model.DependentStatus);
            ObjParam[15] = new DbParameter("@Cover", DbParameter.DbType.Decimal, 0, model.Cover);
            ObjParam[16] = new DbParameter("@Passport", DbParameter.DbType.VarChar, 0, String.IsNullOrWhiteSpace(model.Passport) ? (object)DBNull.Value : (object)model.Passport);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "SaveFamilyDependency_NEW", ObjParam));
        }
        public static DataSet CheckFamilyDependency(FamilyDependencyModel model)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@fkiMemberID", DbParameter.DbType.Int, 0, model.MemberId);
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            var dt = (DbConnection.GetDataTable(CommandType.StoredProcedure, "GetDependenciesCount_ByMemberId", ObjParam));
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }
        public static int UpdateFamilyDependency(FamilyDependencyModel model)
        {
            DbParameter[] ObjParam = new DbParameter[18];
            ObjParam[0] = new DbParameter("@FullName", DbParameter.DbType.VarChar, 0, model.FullName);
            ObjParam[1] = new DbParameter("@Surname", DbParameter.DbType.VarChar, 0, model.Surname);
            ObjParam[2] = new DbParameter("@IDNumber", DbParameter.DbType.VarChar, 0, model.IDNumber);
            ObjParam[3] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[4] = new DbParameter("@DependencyType", DbParameter.DbType.VarChar, 0, model.DependencyType);
            ObjParam[5] = new DbParameter("@fkiMemberID", DbParameter.DbType.Int, 0, model.MemberId);
            ObjParam[6] = new DbParameter("@Age", DbParameter.DbType.Int, 0, model.Age);
            ObjParam[7] = new DbParameter("@pkiDependentID", DbParameter.DbType.Int, 0, model.pkiDependentID);
            ObjParam[8] = new DbParameter("@CoverDate", DbParameter.DbType.DateTime, 0, model.CoverDate);
            ObjParam[9] = new DbParameter("@InceptionDate", DbParameter.DbType.DateTime, 0, model.InceptionDate);
            ObjParam[10] = new DbParameter("@Premium", DbParameter.DbType.Decimal, 0, model.Premium);
            ObjParam[11] = new DbParameter("@Relationship", DbParameter.DbType.Int, 0, model.Relationship);
            ObjParam[12] = new DbParameter("@DateOfBirth", DbParameter.DbType.DateTime, 0, model.DateOfBirth);
            ObjParam[13] = new DbParameter("@Gender", DbParameter.DbType.NVarChar, 0, model.Gender);
            ObjParam[14] = new DbParameter("@StartDate", DbParameter.DbType.DateTime, 0, model.StartDate);
            ObjParam[15] = new DbParameter("@DependentStatus", DbParameter.DbType.NVarChar, 0, model.DependentStatus);
            ObjParam[16] = new DbParameter("@Cover", DbParameter.DbType.Decimal, 0, model.Cover);
            ObjParam[17] = new DbParameter("@Passport", DbParameter.DbType.VarChar, 0, String.IsNullOrWhiteSpace(model.Passport) ? (object)DBNull.Value : (object)model.Passport);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "UpdateFamilyDependency", ObjParam));
        }

        public static SqlDataReader GetFamilyDependencyTypes()
        {
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetFamilyDependecyTypes");
        }

        public static DataTable GetFamilyDependencyTypesdt()
        {
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetFamilyDependecyTypes");
        }
        public static SqlDataReader SelectRelationship()
        {
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectRelationship");
        }

        public static DataTable SelectRelationshipdt()
        {
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectRelationship");
        }

        public static SqlDataReader SelectFamilyDependencyById(int pkiDependentID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiDependentID", DbParameter.DbType.Int, 0, pkiDependentID);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectFamilyDependencyById", ObjParam));
        }

        public static DataTable SelectFamilyDependencyByIddt(int pkiDependentID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiDependentID", DbParameter.DbType.Int, 0, pkiDependentID);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectFamilyDependencyById", ObjParam));
        }

        public static bool DeleteDependentById(int pkiDependentID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiDependentID", DbParameter.DbType.Int, 0, pkiDependentID);
            DbConnection.ExecuteNonQuery(CommandType.Text, "Delete from Dependencies Where pkiDependentID=@pkiDependentID", ObjParam);
            return true;
        }
        //public static bool DeleteDependentById_U(int pkiDependentID)
        //{
        //    DbParameter[] ObjParam = new DbParameter[1];
        //    ObjParam[0] = new DbParameter("@pkiDependentID", DbParameter.DbType.Int, 0, pkiDependentID);
        //    DbConnection.ExecuteNonQuery(CommandType.Text, "Delete from Dependencies Where pkiDependentID=@pkiDependentID", ObjParam);
        //    return true;
        //}
        public static bool DeleteSUpportdocumentById(int pkiPictureID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPictureID", DbParameter.DbType.Int, 0, pkiPictureID);
            DbConnection.ExecuteNonQuery(CommandType.Text, "Delete from MemberDocuments Where pkiPictureID=@pkiPictureID", ObjParam);
            return true;
        }
        #endregion
        public static SqlDataReader SelectAllAgent(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectDistinctAgent", ObjParam);
        }

        public static DataTable SelectAllAgentdt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectDistinctAgent", ObjParam);
        }

        public static SqlDataReader SelectAllUnderwritter(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SelectDistinctUnderwritter",ObjParam);
        }

        public static DataTable SelectAllUnderwritterdt(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid",DbParameter.DbType.UniqueIdentifier,0,Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectDistinctUnderwritter",ObjParam);
        }

        public static SqlDataReader GetInvoiceByid(int InvoiceId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@InvoiceID", DbParameter.DbType.Int, 0, InvoiceId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetInvoiceByid", ObjParam);
        }

        public static DataTable GetInvoiceByiddt(int InvoiceId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@InvoiceID", DbParameter.DbType.Int, 0, InvoiceId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetInvoiceByid", ObjParam);
        }
        public static SqlDataReader PolicyStatusPieChart(Guid parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "PolicyStatusPieChart", ObjParam);
        }

        public static DataTable PolicyStatusPieChartdt(Guid parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "PolicyStatusPieChart", ObjParam);
        }

        public static SqlDataReader SelectPolicyPremiumByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool isAdmin, bool isSuperUser, string UserName)
        {
            string query = "GetPolicyPremiumByParlourId";
            DbParameter[] ObjParam = new DbParameter[9];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[6] = new DbParameter("@IsAdmin", DbParameter.DbType.Bit, 0, isAdmin);
            ObjParam[7] = new DbParameter("@UserName", DbParameter.DbType.VarChar, 0, UserName);
            ObjParam[8] = new DbParameter("@IsSuperUser", DbParameter.DbType.Bit, 0, isSuperUser);

            return (DbConnection.GetDataReader(CommandType.StoredProcedure, query, ObjParam));
        }

        public static DataTable SelectPolicyPremiumByParlourIddt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool isAdmin, bool isSuperUser, string UserName)
        {
            string query = "GetPolicyPremiumByParlourId";
            DbParameter[] ObjParam = new DbParameter[9];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[6] = new DbParameter("@IsAdmin", DbParameter.DbType.Bit, 0, isAdmin);
            ObjParam[7] = new DbParameter("@UserName", DbParameter.DbType.VarChar, 0, UserName);
            ObjParam[8] = new DbParameter("@IsSuperUser", DbParameter.DbType.Bit, 0, isSuperUser);

            return (DbConnection.GetDataTable(CommandType.StoredProcedure, query, ObjParam));
        }
        public static void UpdateMemberStatus(Guid parlourId, int id, string status)
        {
            string query = "UpdateMemberStatus";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@status", DbParameter.DbType.NVarChar, 0, status);
            ObjParam[1] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourId);
            ObjParam[2] = new DbParameter("@memberId", DbParameter.DbType.Int, 0, id);


            DbConnection.ExecuteNonQuery(CommandType.StoredProcedure, query, ObjParam);
        }

        public static void MemberStatusChangeReason(ChangeStatusReason model)
        {
            string query = "INSERT INTO [dbo].[MemberStatusChangeReason]([ChangeReason],[fkiMemberId],[ParlourID],[CreatedDate],[ChangedBy],[CurrentStatus],[NewStatus])VALUES(@ChangeReason,@fkiMemberId,@ParlourID,GetDate(),@ChangedBy,@CurrentStatus,@NewStatus)";
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@ChangeReason", DbParameter.DbType.NVarChar, 0, model.ChangeReason);
            ObjParam[1] = new DbParameter("@fkiMemberId", DbParameter.DbType.Int, 0, model.fkiMemberId);
            ObjParam[2] = new DbParameter("@ParlourID", DbParameter.DbType.UniqueIdentifier, 0, model.ParlourID);
            ObjParam[3] = new DbParameter("@ChangedBy", DbParameter.DbType.Int, 0, model.ChangedBy);
            ObjParam[4] = new DbParameter("@CurrentStatus", DbParameter.DbType.NVarChar, 0, model.CurrentStatus);
            ObjParam[5] = new DbParameter("@NewStatus", DbParameter.DbType.NVarChar, 0, model.NewStatus);
            DbConnection.ExecuteNonQuery(CommandType.Text, query, ObjParam);
        }
        public static DataTable GetPolicyByMemberIDNumberdt(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllPolicyByIDNumber", ObjParam);
        }
        public static DataTable GetPolicyBy_FKMemberId(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllPolicyByFKMemberId", ObjParam);
        }
        public static DataTable GetPolicyByMemberPassportNumber(string Passport, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, Passport);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllPolicyByPassport", ObjParam);
        }
        public static double SumOfPremium(int planId, int fkiMemberid, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@MemberId", DbParameter.DbType.Int, 0, fkiMemberid);
            ObjParam[2] = new DbParameter("@PlanId", DbParameter.DbType.Int, 0, planId);
            return Convert.ToDouble(DbConnection.GetScalarValue(CommandType.StoredProcedure, "TotalProductSelectOnChange", ObjParam));
        }

        public static int DeleteMemberNote(int id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiNoteID", DbParameter.DbType.Int, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteMemberNotesBypkiNoteID", ObjParam));
        }

        public static void MemberRowImportToMember(string MemberType, Guid ImportId)
        {
            //string query = "MemberRowImportToMember";
            //DbParameter[] ObjParam = new DbParameter[2];
            //ObjParam[0] = new DbParameter("@MemberType", DbParameter.DbType.NVarChar, 0, MemberType);
            //ObjParam[1] = new DbParameter("@ImportId", DbParameter.DbType.UniqueIdentifier, 0, ImportId);
            //DbConnection.ExecuteNonQuery(CommandType.StoredProcedure, query, ObjParam);
            using (SqlCommand sqlCommand = new SqlCommand("MemberRowImportToMember"))
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ToString());
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.AddWithValue("@MemberType", MemberType);
                sqlCommand.Parameters.AddWithValue("@ImportId", ImportId);
                sqlCommand.ExecuteNonQuery();
            }
        }
        public static void SaveMemberStaging(string MemberType, Guid ImportId)
        {
            string query = "SaveMemberStaging_New"; //"SaveMemberStagingNew";//"SaveMemberStaging";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@MemberType", DbParameter.DbType.NVarChar, 0, MemberType);
            ObjParam[1] = new DbParameter("@ImportId", DbParameter.DbType.UniqueIdentifier, 0, ImportId);
            DbConnection.ExecuteNonQuery(CommandType.StoredProcedure, query, ObjParam);
        }

        public static int GetLastCopiedMemberForDependency()
        {
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "GetLastCopiedMemberForDependency"));
        }

        public static void UpdateMemberPolicyStatus(string policyStatus, int memberId)
        {
            string query = "UpdateMemberPolicyStatus";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@PolicyStatus", DbParameter.DbType.NVarChar, 0, policyStatus);
            ObjParam[1] = new DbParameter("@MemberId", DbParameter.DbType.Int, 1, memberId);
            DbConnection.ExecuteNonQuery(CommandType.StoredProcedure, query, ObjParam);
        }
        public static int SaveBeneficiary(Beneficiary_model ModalProduct)
        {
            DbParameter[] ObjParam = new DbParameter[12];
            ObjParam[0] = new DbParameter("@pkiBeneficiaryID", DbParameter.DbType.Int, 0, ModalProduct.pkiBeneficiaryID);
            ObjParam[1] = new DbParameter("@fkiMemberID", DbParameter.DbType.Int, 0, ModalProduct.pkiMemberID);
            ObjParam[2] = new DbParameter("@FullName", DbParameter.DbType.VarChar, 0, ModalProduct.FullName_Beneficiary);
            ObjParam[3] = new DbParameter("@Surname", DbParameter.DbType.VarChar, 0, ModalProduct.Surname_Beneficiary);
            ObjParam[4] = new DbParameter("@DateOfBirth", DbParameter.DbType.DateTime, 0, ModalProduct.DateOfBirth_Beneficiary);
            ObjParam[5] = new DbParameter("@IDNumber", DbParameter.DbType.VarChar, 0, ModalProduct.IDNumber_Beneficiary);
            ObjParam[6] = new DbParameter("@RelationshipType", DbParameter.DbType.VarChar, 0, ModalProduct.RelationshipType_Beneficiary);
            ObjParam[7] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ModalProduct.parlourid);
            ObjParam[8] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, ModalProduct.ModifiedUser);
            ObjParam[9] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, System.DateTime.Now);
            ObjParam[10] = new DbParameter("@Percentage", DbParameter.DbType.Decimal, 0, ModalProduct.Percentages);
            ObjParam[11] = new DbParameter("@CellPhoneNumber", DbParameter.DbType.NVarChar, 0, ModalProduct.Cellphone_Beneficiary);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "NewAddEditBeneficiaries", ObjParam));
        }
        public static DataTable SearchBeneficiaryData(int fkiMemberid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@fkiMemberid", DbParameter.DbType.Int, 0, fkiMemberid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SelectMemberBeneficiaries", ObjParam);
        }
        public static void DeleteBeneficiary(int pkiBeneficiaryID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiBeneficiaryID", DbParameter.DbType.Int, 0, pkiBeneficiaryID);
            DbConnection.GetDataReader(CommandType.StoredProcedure, "DeleteBeneficiary", ObjParam);
        }
        public static DataTable GetExtendedFamilyList(Guid Parlourid, int MemberId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[1] = new DbParameter("@MemberId", DbParameter.DbType.Int, 0, MemberId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GETDependentExtendedFamily", ObjParam);
        }
        public static DataTable GetUnderwriterList(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetUnderWriterList", ObjParam);
        }
        public static void ReadyToImportMember( Guid ImportId)
        {
            string query = "ImportMemberReadyForJob";//"SaveMemberStaging";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ImportId", DbParameter.DbType.UniqueIdentifier, 0, ImportId);
            DbConnection.ExecuteNonQuery(CommandType.StoredProcedure, query, ObjParam);
        }


        //=================TEST
        public static DataTable GetPlanByParlourIddt(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "PlanByparlourId", ObjParam);
        }
        //=================end

        public static SqlDataReader GetMonthsToPayByID(int MemberId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            //ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[0] = new DbParameter("@MemberId", DbParameter.DbType.VarChar, 0, MemberId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetMonthsToPay", ObjParam);
        }

        public static DataTable GetMonthsToPayByIDdt(int MemberId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            //ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            ObjParam[0] = new DbParameter("@MemberId", DbParameter.DbType.VarChar, 0, MemberId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetMonthsToPay", ObjParam);
        }

        //============TEST=============

        public static DataTable GetMemberByPassport(string Passport, Guid ParlourId, int PlanId)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@Passport", DbParameter.DbType.NVarChar, 0, Passport);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[2] = new DbParameter("@PlanId", DbParameter.DbType.Int, 0, PlanId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberSelectByPassportdt", ObjParam);
        }


        //============TEST END=============


    }
}
