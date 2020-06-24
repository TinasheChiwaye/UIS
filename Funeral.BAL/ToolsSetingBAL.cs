using ExcelDataReader;
using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;

namespace Funeral.BAL
{
    public class ToolsSetingBAL
    {
        public static int UploadApplicationLogo(ApplicationSettingsModel model)
        {
            return ToolsSetingDAL.UploadApplicationLogo(model);
        }
        public static ApplicationSettingsModel SaveApplication(ApplicationSettingsModel model)
        {
            DataTable dr = ToolsSetingDAL.SaveApplicationdt(model);
            return FuneralHelper.DataTableMapToList<ApplicationSettingsModel>(dr).FirstOrDefault();
        }
        public static AdditionalApplicationSettingsModel SaveAdditionalApplication(AdditionalApplicationSettingsModel model)
        {
            DataTable dr = ToolsSetingDAL.SaveAdditionalApplicationdt(model);
            return FuneralHelper.DataTableMapToList<AdditionalApplicationSettingsModel>(dr).FirstOrDefault();
        }

        public static AgentInfoSetupModel SaveAgentInfo(AgentInfoSetupModel model)
        {
            DataTable dr = ToolsSetingDAL.SaveAgentInfodt(model);
            return FuneralHelper.DataTableMapToList<AgentInfoSetupModel>(dr).FirstOrDefault();
        }
        public static BankingDetailSending SaveBankingDetail(BankingDetailSending Model)
        {
            DataTable dr = ToolsSetingDAL.SaveBankingDetaildt(Model);
            return FuneralHelper.DataTableMapToList<BankingDetailSending>(dr).FirstOrDefault();
        }
        public static ApplicationSettingsModel GetApplictionByParlourID(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetApplictionByParlourIDdt(ParlourId);
            return FuneralHelper.DataTableMapToList<ApplicationSettingsModel>(dr).FirstOrDefault();
        }

        public static ApplicationSettingsModel GetApplictionByID(int ID)
        {
            DataTable dr = ToolsSetingDAL.GetApplictionByIDdt(ID);
            return FuneralHelper.DataTableMapToList<ApplicationSettingsModel>(dr).FirstOrDefault();
        }
        public static BankingDetailSending GetBankingByID(Guid ID)
        {
            DataTable dr = ToolsSetingDAL.GetBankingByIDdt(ID);
            return FuneralHelper.DataTableMapToList<BankingDetailSending>(dr).FirstOrDefault();
        }
        public static AgentInfoSetupModel GetAgentByID(int ID)
        {
            DataTable dr = ToolsSetingDAL.GetAgentByIDdt(ID);
            return FuneralHelper.DataTableMapToList<AgentInfoSetupModel>(dr).FirstOrDefault();
        }
        public static SecureUserGroupsModel GetUserAccessByID(int ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetUserAccessByIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<SecureUserGroupsModel>(dr).FirstOrDefault();
        }
        public static List<ApplicationSettingsModel> GetAllCompanys(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable dr = ToolsSetingDAL.GetAllCompanysdt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
            return FuneralHelper.DataTableMapToList<ApplicationSettingsModel>(dr);
        }
        public static List<AgentInfoSetupModel> GetAllAgentInfo(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable dr = ToolsSetingDAL.GetAllAgentInfodt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
            return FuneralHelper.DataTableMapToList<AgentInfoSetupModel>(dr);
        }

        public static int DeleteCompany(int ID)
        {
            return ToolsSetingDAL.DeleteCompany(ID);
        }
        public static int DeleteAgent(int ID)
        {
            return ToolsSetingDAL.DeleteAgent(ID);
        }
        public static List<smsSendingGroupModel> EditsmsGroupbyID(int ID)
        {
            DataTable dr = ToolsSetingDAL.EditsmsGroupbyIDdt(ID);
            return FuneralHelper.DataTableMapToList<smsSendingGroupModel>(dr);
        }
        public static smsSendingGroupModel GetsmsGroupbyID(int ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetsmsGroupbyIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<smsSendingGroupModel>(dr).FirstOrDefault();
        }
        public static int SaveSmsGroupDetails(smsSendingGroupModel model)
        {
            return ToolsSetingDAL.SaveSmsGroupDetails(model);
        }
        public static List<SecureGroupModel> GetSecureGrouList()
        {
            DataTable dr = ToolsSetingDAL.GetSecureGrouListdt();
            return FuneralHelper.DataTableMapToList<SecureGroupModel>(dr);
        }
        public static SecureUsersModel GetUserByID(string ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetUserByIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<SecureUsersModel>(dr).FirstOrDefault();
        }
        public static SecureUsersModel GetUserByEmailID(string ID)
        {
            DataTable dr = ToolsSetingDAL.GetUserByEmailIDdt(ID);
            return FuneralHelper.DataTableMapToList<SecureUsersModel>(dr).FirstOrDefault();
        }

        public static int SaveUserDetails(SecureUsersModel model)
        {
            return ToolsSetingDAL.SaveUserDetails(model);
        }
        public static int SaveUserGroupDetails(SecureUserGroupsModel model)
        {
            return ToolsSetingDAL.SaveUserGroupDetails(model);
        }
        public static List<SecureUsersModel> GetAllUsers(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable dr = ToolsSetingDAL.GetAllUsersdt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
            //MembersViewModel objViewModel = new MembersViewModel();
            return FuneralHelper.DataTableMapToList<SecureUsersModel>(dr);
            //dr.NextResult();
            //dr.Read();
            //objViewModel.TotalRecord = Convert.ToInt64(dr["TotalRecord"]);
            //return objViewModel;
        }
        public static int DeleteUser(int ID)
        {
            return ToolsSetingDAL.DeleteUser(ID);
        }
        public static SecureUsersModel EditUserbyID(int ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.EditUserbyIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<SecureUsersModel>(dr).FirstOrDefault();
        }
        public static List<SecureUserGroupsModel> EditSecurUserbyID(int ID)
        {
            DataTable dr = ToolsSetingDAL.EditSecurUserbyIDdt(ID);
            return FuneralHelper.DataTableMapToList<SecureUserGroupsModel>(dr);
        }
        public static BranchModel GetBranchByID(string ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetBranchByIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<BranchModel>(dr).FirstOrDefault();
        }
        public static Guid SaveBranchDetails(BranchModel model)
        {
            return ToolsSetingDAL.SaveBranchDetails(model);
        }
        public static List<BranchModel> GetAllBranches(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetAllBranchesdt(ParlourId);
            return FuneralHelper.DataTableMapToList<BranchModel>(dr);
        }
        public static BranchModel EditBranchbyID(Guid ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.EditBranchbyIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<BranchModel>(dr).FirstOrDefault();
        }
        public static int DeleteBranch(Guid ID)
        {
            return ToolsSetingDAL.DeleteBranch(ID);
        }
        public static List<SocietyModel> GetAllSocietyes(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetAllSocietyesdt(ParlourId);
            return FuneralHelper.DataTableMapToList<SocietyModel>(dr);
        }
        public static List<GroupPaymentList> GetAllSocietyes_PaymentList(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetAllSocietye_PaymentList(ParlourId);
            return FuneralHelper.DataTableMapToList<GroupPaymentList>(dr);
        }
        public static GroupPaymentList GetGroupPayment_ByParlourId(Guid ParlourId,string ReferenceNumber)
        {
            DataTable dr = ToolsSetingDAL.GetGroupPayment_ByParlourId(ParlourId, ReferenceNumber);
            return FuneralHelper.DataTableMapToList<GroupPaymentList>(dr).FirstOrDefault();
        }
        public static List<SocietyModel> GetAllSocietye(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetAllSocietyedt(ParlourId);
            return FuneralHelper.DataTableMapToList<SocietyModel>(dr);
        }
        public static SocietyModel EditSocietybyID(int ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.EditSocietybyIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<SocietyModel>(dr).FirstOrDefault();
        }
        public static SocietyModel GetSocietyByID(string ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetSocietyByIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<SocietyModel>(dr).FirstOrDefault();
        }
        public static int SaveSocietyDetails(SocietyModel model)
        {
            return ToolsSetingDAL.SaveSocietyDetails(model);
        }
        public static int DeleteSociety(int ID)
        {
            return ToolsSetingDAL.DeleteSociety(ID);
        }
        public static int DeleteFuneral(int Id)
        {
            return ToolsSetingDAL.DeleteFuneralService(Id);
        }
        public static List<VendorModel> GetAllVendores(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable dr = ToolsSetingDAL.GetAllVendoresdt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
            return FuneralHelper.DataTableMapToList<VendorModel>(dr);
        }
        public static VendorModel EditVendorbyID(int ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.EditVendorbyIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<VendorModel>(dr).FirstOrDefault();
        }
        public static VendorModel GetVendorByID(string ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetVendorByIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<VendorModel>(dr).FirstOrDefault();
        }
        public static int SaveVendorDetails(VendorModel model)
        {
            return ToolsSetingDAL.SaveVendorDetails(model);
        }
        public static int DeleteVendor(int ID)
        {
            return ToolsSetingDAL.DeleteVendor(ID);
        }
        public static List<ExpensesModel> GetAllExpenseses(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetAllExpensesesdt(ParlourId);
            return FuneralHelper.DataTableMapToList<ExpensesModel>(dr);
        }
        public static ExpensesModel EditExpensesbyID(int ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.EditExpensesbyIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<ExpensesModel>(dr).FirstOrDefault();
        }
        public static ExpensesModel GetExpensesByID(string ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetExpensesByIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<ExpensesModel>(dr).FirstOrDefault();
        }
        public static int SaveExpensesDetails(ExpensesModel model)
        {
            return ToolsSetingDAL.SaveExpensesDetails(model);
        }
        public static int DeleteExpenses(int ID)
        {
            return ToolsSetingDAL.DeleteExpenses(ID);
        }

        public static AddonProductsModal GetAddonProductByID(string ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetAddonProductByIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<AddonProductsModal>(dr).FirstOrDefault();
        }
        public static Guid SaveAddonProductDetails(AddonProductsModal model)
        {
            return ToolsSetingDAL.SaveAddonProductDetails(model);
        }
        public static List<AddonProductsModal> GetAllAddonProductes(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetAllAddonProductesdt(ParlourId);
            return FuneralHelper.DataTableMapToList<AddonProductsModal>(dr);
        }
        public static AddonProductsModal EditAddonProductbyID(Guid ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.EditAddonProductbyIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<AddonProductsModal>(dr).FirstOrDefault();
        }
        public static int DeleteAddonProduct(Guid ID)
        {
            return ToolsSetingDAL.DeleteAddonProduct(ID);
        }

        public static List<PlanModel> GetAllPlans(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable dr = ToolsSetingDAL.GetAllPlansdt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
            return FuneralHelper.DataTableMapToList<PlanModel>(dr);
        }
        public static List<PlanModel> GetAllPlansList(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetAllPlansListdt(ParlourId);
            return FuneralHelper.DataTableMapToList<PlanModel>(dr);
        }

        public static List<RolePlayerModel> GetAllRolePlayer(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetAllRolePlayerdt(ParlourId);
            return FuneralHelper.DataTableMapToList<RolePlayerModel>(dr);
        }
        public static PlanModel EditPlanbyID(int ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.EditPlanbyIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<PlanModel>(dr).FirstOrDefault();
        }
        public static List<PlanCreator> EditPlanCreatorbyID(int PlanID)
        {
            DataTable dr = ToolsSetingDAL.EditPlanCreatorbyID(PlanID);
            return FuneralHelper.DataTableMapToList<PlanCreator>(dr).ToList();
        }
        public static PlanModel GetPlanByID(string ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetPlanByIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<PlanModel>(dr).FirstOrDefault();
        }
        public static int SavePlanDetails(PlanModel model)
        {
            return ToolsSetingDAL.SavePlanDetails(model);
        }
        public static int NewSavePlanDetails(PlanModel model)
        {
            return ToolsSetingDAL.NewSavePlanDetails(model);
        }
        public static int SavePlanCreatorDetails(PlanCreator model)
        {
            return ToolsSetingDAL.SavePlanCreatorDetails(model);
        }
        public static int DeletePlan(int ID)
        {
            return ToolsSetingDAL.DeletePlan(ID);
        }
        public static int DeletePlanAndPlanCreator(int ID)
        {
            return ToolsSetingDAL.DeletePlanAndPlanCreator(ID);
        }

        public static List<MembersModel> GetAllMemberCellphon(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetAllMemberCellphondt(ParlourId);
            return FuneralHelper.DataTableMapToList<MembersModel>(dr);
        }

        public static List<smsTempletModel> GetTemplateList(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetTemplateListdt(ParlourId);
            return FuneralHelper.DataTableMapToList<smsTempletModel>(dr);
        }
        public static smsTempletModel GetEmailTemplateByID(int ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetEmailTemplateByIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<smsTempletModel>(dr).FirstOrDefault();
        }


        public static int UpdatesmsTemplate(smsTempletModel model)
        {
            return ToolsSetingDAL.UpdatesmsTemplate(model);
        }
        public static int SaveTermsAndCondition(ApplicationTnCModel Model1)
        {
            return ToolsSetingDAL.SaveTermsAndCondition(Model1);
        }
        public static ApplicationTnCModel SelectApplicationTermsAndCondition(Guid parlourid)
        {
            DataTable dr = ToolsSetingDAL.SelectApplicationTermsAndConditiondt(parlourid);
            return FuneralHelper.DataTableMapToList<ApplicationTnCModel>(dr).FirstOrDefault();
        }
        public static int UpdateAutoPolicyNo(ApplicationSettingsModel Model1)
        {
            return ToolsSetingDAL.UpdateAutoPolicyNo(Model1);
        }
        public static List<FuneralServiceManageModel> SelectFuneralManageServiceByParlID(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable dr = ToolsSetingDAL.SelectFuneralManageServiceByParlIDdt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
            return FuneralHelper.DataTableMapToList<FuneralServiceManageModel>(dr);
        }
        public static int SaveFuneralManageService(FuneralServiceManageModel Model1)
        {
            return ToolsSetingDAL.SaveFuneralManageService(Model1);
        }
        public static FuneralServiceManageModel SelectFuneralManageServiceByParlANdID(int pkiServiceID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.SelectFuneralManageServiceByParlANdIDdt(pkiServiceID, ParlourId);
            return FuneralHelper.DataTableMapToList<FuneralServiceManageModel>(dr).FirstOrDefault();
        }
        public static int DeleteFuneralManageServiceById(int pkiServiceID)
        {
            return ToolsSetingDAL.DeleteFuneralManageServiceById(pkiServiceID);
        }
        public static List<ApplicationSettingsModel> GetAllApplicationList(Guid parlourid, int param, int AppID)
        {
            DataTable dr = ToolsSetingDAL.GetAllApplicationListdt(parlourid, param, AppID);
            return FuneralHelper.DataTableMapToList<ApplicationSettingsModel>(dr);
        }
        public static ApplicationSettingsModel GetAllApplicationList2(Guid parlourid, int param, int AppID)
        {
            DataTable dr = ToolsSetingDAL.GetAllApplicationListdt(parlourid, param, AppID);
            return FuneralHelper.DataTableMapToList<ApplicationSettingsModel>(dr).FirstOrDefault();
        }

        public static List<SecureUserGroupsModel> GetSuperUserAccessByID(int ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetSuperUserAccessByIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<SecureUserGroupsModel>(dr);
        }
        public static List<SecureUserGroupsModel> GetSuperUserAccessByUserId(int ID)
        {
            DataTable dr = ToolsSetingDAL.GetSuperUserAccessByUserId(ID);
            return FuneralHelper.DataTableMapToList<SecureUserGroupsModel>(dr);
        }



        public static List<UnderwriterPremiumModel> GetAllUnderwriterPlansByParlourID(Guid ParlourID, int UnderwriterPlanSelection)
        {
            //DataTable  dr = ToolsSetingDAL.GetAllUnderwriterPlansByParlourIDdt(ParlourID, UnderwriterPlanSelection);
            //List<UnderwriterPremiumModel> model = FuneralHelper.DataTableMapToList<UnderwriterPremiumModel>(dr);
            //foreach (var item in model)
            //{
            //    item.UnderwriterPlanSelection = (UnderwriterPremiumEnums.UnderwriterPlans)UnderwriterPlanSelection;
            //}
            //return model;
            return null;
        }
        public static List<UnderwriterSetupModel> GetAllUnderwriterList(Guid parlourid)
        {
            DataTable dr = ToolsSetingDAL.GetAllUnderwriterList(parlourid);
            return FuneralHelper.DataTableMapToList<UnderwriterSetupModel>(dr);
        }



        public static ProgressStatus CheckProgressStatus(int ID, Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.CheckProgressStatus(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<ProgressStatus>(dr).FirstOrDefault();

        }
        public static List<VendorModel> GetVendorNameByParlourId(Guid parlourid)
        {
            DataTable dr = ToolsSetingDAL.GetVendorNameByParlourId(parlourid);
            return FuneralHelper.DataTableMapToList<VendorModel>(dr);
        }
        public static List<string> ValidateExcelColumns(string filePath)
        {
            List<string> ColumnNameList = new List<string>();
            int ColumCount = 0;
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var column = result.Tables[0].Columns;
                    ColumCount = result.Tables[0].Columns.Count;
                    int cnt = column.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        ColumnNameList.Add(column[i].ColumnName);
                    }
                }
            }
            return ColumnNameList;
        }
        public static DataSet ReadExcel(string fileName, string fileExt)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });
                    return result;
                }
            }
        }
        public static List<ClaimRightsList> GetClaimRightsCollectionByRoleId(int RoleID)
        {
            DataTable dr = ToolsSetingDAL.GetClaimRightsCollectionByRoleId(RoleID);
            return FuneralHelper.DataTableMapToList<ClaimRightsList>(dr);
        }
        public static int SaveClaimRights(ClaimRightsList model)
        {
            return ToolsSetingDAL.SaveClaimRights(model);
        }
        public static int DeleteClaimRights(int RoleId, string CreatedBy)
        {
            return ToolsSetingDAL.DeleteClaimRights(RoleId, CreatedBy);
        }
        public static List<PlanStagingList> AddPlansAndGetPlanList_Staging(Guid newImportedId)
        {
            DataTable dr = ToolsSetingDAL.AddPlansAndGetPlanList_Staging(newImportedId);
            return FuneralHelper.DataTableMapToList<PlanStagingList>(dr);
        }
        public static int SavePlanCreatorStagingDetails(PlanStagingList model)
        {
            return ToolsSetingDAL.SavePlanCreatorStagingDetails(model);
        }
        public static DataSet GetImportedMemberList(Guid newImportedId, string columnName)
        {
            return ToolsSetingDAL.GetImportedMemberList(newImportedId, columnName);
        }
        public static int SaveImportedHistory(string filename, string MappingColumn, Guid parlourId, bool IsImported, string CreatedBy, Guid newImportedId, string MemberType)
        {
            ImportedHistory importedHistory = new ImportedHistory();
            importedHistory.ImportedFilename = filename;
            importedHistory.MappingColumn = MappingColumn;
            importedHistory.parlourId = parlourId;
            importedHistory.IsImported = IsImported;
            importedHistory.ImportedBy = CreatedBy;
            importedHistory.NewImportedId = newImportedId;
            importedHistory.MemberType = MemberType;
            return ToolsSetingDAL.SaveImportedHistory(importedHistory);
        }
        public static List<ImportedHistory> GetImportedHistory(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetImportedHistory(ParlourId);
            return FuneralHelper.DataTableMapToList<ImportedHistory>(dr);
        }
        public static ImportedHistory GetImportedHistory_ByImportedId(int ImportedId)
        {
            DataTable dr = ToolsSetingDAL.GetImportedHistory_ByImportedId(ImportedId);
            return FuneralHelper.DataTableMapToList<ImportedHistory>(dr).FirstOrDefault();
        }
        public static ImportedHistory GetImportedHistory_ByNewImportedId(Guid ImportedId)
        {
            DataTable dr = ToolsSetingDAL.GetImportedHistory_ByNewImportedId(ImportedId);
            return FuneralHelper.DataTableMapToList<ImportedHistory>(dr).FirstOrDefault();
        }
        public static Tuple<List<string>, List<string>> GetXMLColumn(string MappingColumn)
        {
            List<string> DatabaseColumns = new List<string>();
            List<string> ExcelColumns = new List<string>();
            if (MappingColumn != null)
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(MappingColumn);
                XmlNode node = xml.FirstChild;
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    if (node.ChildNodes[i].Attributes != null)
                    {
                        foreach (XmlAttribute at in node.ChildNodes[i].Attributes)
                        {
                            if (at.LocalName.Contains("DatabaseColumn"))
                            {
                                DatabaseColumns.Add(at.Value);
                            }
                            if (at.LocalName.Contains("ExcelColumn"))
                            {
                                ExcelColumns.Add(at.Value);
                            }
                        }
                    }
                }
            }
            return Tuple.Create(DatabaseColumns, ExcelColumns);
        }
        public static int SaveMappedDependents(string SystemType, string ExcelType, Guid parlourId, Guid newImportedId, string CreatedBy)
        {
            return ToolsSetingDAL.SaveMappedDependents(SystemType, ExcelType, parlourId, newImportedId, CreatedBy);
        }
        public static List<MappedDependents> GetMappedDependent(Guid ImportedId)
        {
            DataTable dr = ToolsSetingDAL.GetMappedDependent(ImportedId);
            return FuneralHelper.DataTableMapToList<MappedDependents>(dr);
        }
        public static void UpdateMemberStagingTable(DataTable dt)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    String[] columnList = new String[] { "ID", "Date Of Birth", "InceptionDate", "CoverDate", "DebitDate", "EffectiveDate", "StartDate" }; //list of my specific columns 
                    IsAllColumnExist(dt, columnList);
                    DataTable _dbTable = new DataView(dt).ToTable(false, columnList);
                    ToolsSetingDAL.UpdateMemberStagingTable(_dbTable);
                }
            }
        }
        private static void IsAllColumnExist(DataTable tableNameToCheck, String[] columnsNames)
        {
            try
            {
                if (null != tableNameToCheck && tableNameToCheck.Columns != null)
                {
                    foreach (string columnName in columnsNames)
                    {
                        if (!tableNameToCheck.Columns.Contains(columnName))
                        {
                            System.Data.DataColumn newColumn = new System.Data.DataColumn(columnName, typeof(System.String));
                            newColumn.DefaultValue = null;
                            tableNameToCheck.Columns.Add(newColumn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
