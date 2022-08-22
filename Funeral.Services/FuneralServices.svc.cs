using Funeral.BAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace Funeral.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FuneralServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FuneralServices.svc or FuneralServices.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode =
            AspNetCompatibilityRequirementsMode.Allowed)]
    public class FuneralServices : IFuneralServices
    {
        #region Admin Login Section
        /// <summary>
        /// Do Admin Login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AdminModel DoLogin(string userName, string password)
        {
            try
            {
                return AdminBAL.AdminLogin(userName, password);
            }
            catch (Exception exc)
            {
                throw new FaultException<FuneralServiceFault>(new FuneralServiceFault(exc.Message));
            }
        }
        #endregion

        #region Members Section
        public int SaveMember(MembersModel model)
        {
            return MembersBAL.SaveMembers(model);
        }
        public int DeleteMember(int ID)
        {
            return MembersBAL.DeleteMember(ID);
        }
        public int MemberDeleteLogical(int id, string UserName)
        {
            return MembersBAL.MemberDeleteLogical(id, UserName);
        }
        public MembersModel GetMemberByID(int ID, Guid ParlourId)
        {
            return MembersBAL.GetMemberByID(ID, ParlourId);
        }

        public MembersViewModel GetAllMembers(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, string status)
        {
            return MembersBAL.GetAllMembers(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder, status, "");
        }
        public List<PolicyModel> GetPolicyByParlourId(Guid parlourid)
        {
            return CommonBAL.GetPolicyByParlourId(parlourid);
        }
        public List<FamilyDependencyModel> FamilyDependency(Guid parlourid, int MemberId)
        {
            return MembersBAL.GetFamilyDependencyByMemberID(parlourid, MemberId);
        }
        public List<RelationShipModel> SelectRelationship()
        {
            return MembersBAL.SelectRelationship();
        }
        public List<PolicyModel> GetPlanSubscriptionByPlanId(int pkiPlanID, Guid parlorId)
        {
            return CommonBAL.GetPlanSubscriptionByPlanId(pkiPlanID, parlorId);
        }
        public int GetWaitingPeriodByPlanId(int pkiPlanID)
        {
            return CommonBAL.GetWaitingPeriodByPlanId(pkiPlanID);
        }
        public List<string> GetDistinctPolicyStatusList()
        {
            return MembersBAL.GetDistinctPolicyStatusList();
        }
        #endregion

        #region Tools Seting Section
        public ApplicationSettingsModel SaveApplication(ApplicationSettingsModel model)
        {
            return ToolsSetingBAL.SaveApplication(model);
        }
        public AdditionalApplicationSettingsModel SaveAdditionalApplication(AdditionalApplicationSettingsModel model)
        {
            return ToolsSetingBAL.SaveAdditionalApplication(model);
        }
        public AgentInfoSetupModel SaveAgentInfo(AgentInfoSetupModel model)
        {
            return ToolsSetingBAL.SaveAgentInfo(model);
        }
        public BankingDetailSending SaveBankingDetail(BankingDetailSending Model)
        {
            return ToolsSetingBAL.SaveBankingDetail(Model);
        }
        public ApplicationSettingsModel GetApplictionByParlourID(Guid ParlourId)
        {
            return ToolsSetingBAL.GetApplictionByParlourID(ParlourId);
        }
        public ApplicationSettingsModel GetApplictionByID(int ID)
        {
            return ToolsSetingBAL.GetApplictionByID(ID);
        }
        public BankingDetailSending GetBankingByID(Guid ID)
        {
            return ToolsSetingBAL.GetBankingByID(ID);
        }
        public AgentInfoSetupModel GetAgentByID(int ID)
        {
            return ToolsSetingBAL.GetAgentByID(ID);
        }
        public List<ApplicationSettingsModel> GetAllCompanys(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            return ToolsSetingBAL.GetAllCompanys(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
        }
        public List<AgentInfoSetupModel> GetAllAgentInfo(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            return ToolsSetingBAL.GetAllAgentInfo(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
        }
        public SecureUserGroupsModel GetUserAccessByID(int ID, Guid ParlourId)
        {
            return ToolsSetingBAL.GetUserAccessByID(ID, ParlourId);
        }
        public int DeleteCompany(int ID)
        {
            return ToolsSetingBAL.DeleteCompany(ID);
        }
        public int DeleteAgent(int ID)
        {
            return ToolsSetingBAL.DeleteAgent(ID);
        }
        public List<smsSendingGroupModel> EditsmsGroupbyID(int ID)
        {
            return ToolsSetingBAL.EditsmsGroupbyID(ID);
        }
        public smsSendingGroupModel GetsmsGroupbyID(int ID, Guid ParlourId)
        {
            return ToolsSetingBAL.GetsmsGroupbyID(ID, ParlourId);
        }
        public int SaveSmsGroupDetails(smsSendingGroupModel model)
        {
            return ToolsSetingBAL.SaveSmsGroupDetails(model);
        }
        public List<SecureGroupModel> GetSecureGrouList()
        {
            return ToolsSetingBAL.GetSecureGrouList();
        }
        public SecureUsersModel GetUserByID(string ID, Guid ParlourId)
        {
            return ToolsSetingBAL.GetUserByID(ID, ParlourId);
        }
        public SecureUsersModel GetUserByEmailID(string ID)
        {
            return ToolsSetingBAL.GetUserByEmailID(ID);
        }
        public int SaveUserDetails(SecureUsersModel model)
        {
            return ToolsSetingBAL.SaveUserDetails(model);
        }
        public int SaveUserGroupDetails(SecureUserGroupsModel model)
        {
            return ToolsSetingBAL.SaveUserGroupDetails(model);
        }
        public List<SecureUsersModel> GetAllUsers(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            return ToolsSetingBAL.GetAllUsers(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
        }
        public int DeleteUser(int ID)
        {
            return ToolsSetingBAL.DeleteUser(ID);
        }
        public SecureUsersModel EditUserbyID(int ID, Guid ParlourId)
        {
            return ToolsSetingBAL.EditUserbyID(ID, ParlourId);
        }
        public List<SecureUserGroupsModel> EditSecurUserbyID(int ID)
        {
            return ToolsSetingBAL.EditSecurUserbyID(ID);
        }
        public BranchModel GetBranchByID(string ID, Guid ParlourId)
        {
            return ToolsSetingBAL.GetBranchByID(ID, ParlourId);
        }
        public Guid SaveBranchDetails(BranchModel model)
        {
            return ToolsSetingBAL.SaveBranchDetails(model);
        }
        public List<BranchModel> GetAllBranches(Guid ParlourId)
        {
            return ToolsSetingBAL.GetAllBranches(ParlourId);
        }
        public BranchModel EditBranchbyID(Guid ID, Guid ParlourId)
        {
            return ToolsSetingBAL.EditBranchbyID(ID, ParlourId);
        }
        public int DeleteBranch(Guid ID)
        {
            return ToolsSetingBAL.DeleteBranch(ID);
        }
        public List<SocietyModel> GetAllSocietyes(Guid ParlourId)
        {
            return ToolsSetingBAL.GetAllSocietyes(ParlourId);
        }
        public List<SocietyModel> GetAllSocietye(Guid ParlourId)
        {
            return ToolsSetingBAL.GetAllSocietyes(ParlourId);
        }
        public SocietyModel EditSocietybyID(int ID, Guid ParlourId)
        {
            return ToolsSetingBAL.EditSocietybyID(ID, ParlourId);
        }
        public SocietyModel GetSocietyByID(string ID, Guid ParlourId)
        {
            return ToolsSetingBAL.GetSocietyByID(ID, ParlourId);
        }
        public int SaveSocietyDetails(SocietyModel model)
        {
            return ToolsSetingBAL.SaveSocietyDetails(model);
        }
        public int DeleteSociety(int ID)
        {
            return ToolsSetingBAL.DeleteSociety(ID);
        }
        public List<VendorModel> GetAllVendores(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            return ToolsSetingBAL.GetAllVendores(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
        }
        public VendorModel EditVendorbyID(int ID, Guid ParlourId)
        {
            return ToolsSetingBAL.EditVendorbyID(ID, ParlourId);
        }
        public VendorModel GetVendorByID(string ID, Guid ParlourId)
        {
            return ToolsSetingBAL.GetVendorByID(ID, ParlourId);
        }
        public int SaveVendorDetails(VendorModel model)
        {
            return ToolsSetingBAL.SaveVendorDetails(model);
        }
        public int DeleteVendor(int ID)
        {
            return ToolsSetingBAL.DeleteVendor(ID);
        }
        public List<ExpensesModel> GetAllExpenseses(Guid ParlourId)
        {
            return ToolsSetingBAL.GetAllExpenseses(ParlourId);
        }
        public ExpensesModel EditExpensesbyID(int ID, Guid ParlourId)
        {
            return ToolsSetingBAL.EditExpensesbyID(ID, ParlourId);
        }
        public ExpensesModel GetExpensesByID(string ID, Guid ParlourId)
        {
            return ToolsSetingBAL.GetExpensesByID(ID, ParlourId);
        }
        public int SaveExpensesDetails(ExpensesModel model)
        {
            return ToolsSetingBAL.SaveExpensesDetails(model);
        }
        public int DeleteExpenses(int ID)
        {
            return ToolsSetingBAL.DeleteExpenses(ID);
        }
        public AddonProductsModal GetAddonProductByID(string ID, Guid ParlourId)
        {
            return ToolsSetingBAL.GetAddonProductByID(ID, ParlourId);
        }
        public Guid SaveAddonProductDetails(AddonProductsModal model)
        {
            return ToolsSetingBAL.SaveAddonProductDetails(model);
        }
        public List<AddonProductsModal> GetAllAddonProductes(Guid ParlourId)
        {
            return ToolsSetingBAL.GetAllAddonProductes(ParlourId);
        }
        public AddonProductsModal EditAddonProductbyID(Guid ID, Guid ParlourId)
        {
            return ToolsSetingBAL.EditAddonProductbyID(ID);
        }
        public int DeleteAddonProduct(Guid ID)
        {
            return ToolsSetingBAL.DeleteAddonProduct(ID);
        }
        public List<PlanModel> GetAllPlans(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            return ToolsSetingBAL.GetAllPlans(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
        }
        public PlanModel EditPlanbyID(int ID)
        {
            return ToolsSetingBAL.EditPlanbyID(ID);
        }
        public PlanModel GetPlanByID(string ID, Guid ParlourId)
        {
            return ToolsSetingBAL.GetPlanByID(ID, ParlourId);
        }
        public int SavePlanDetails(PlanModel model)
        {
            return ToolsSetingBAL.SavePlanDetails(model);
        }
        public int DeletePlan(int ID)
        {
            return ToolsSetingBAL.DeletePlan(ID);
        }
        public List<MembersModel> GetAllMemberCellphon(Guid ParlourId)
        {
            return ToolsSetingBAL.GetAllMemberCellphon(ParlourId);
        }
        public List<smsTempletModel> GetTemplateList(Guid ParlourId)
        {
            return ToolsSetingBAL.GetTemplateList(ParlourId);
        }
        public smsTempletModel GetEmailTemplateByID(int ID, Guid ParlourId)
        {
            return ToolsSetingBAL.GetEmailTemplateByID(ID, ParlourId);
        }
        public int UpdatesmsTemplate(smsTempletModel model)
        {
            return ToolsSetingBAL.UpdatesmsTemplate(model);
        }
        public int SaveTermsAndCondition(ApplicationTnCModel Model1)
        {
            return ToolsSetingBAL.SaveTermsAndCondition(Model1);
        }
        public ApplicationTnCModel SelectApplicationTermsAndCondition(Guid parlourid)
        {
            return ToolsSetingBAL.SelectApplicationTermsAndCondition(parlourid);
        }
        public int UpdateAutoPolicyNo(ApplicationSettingsModel Model1)
        {
            return ToolsSetingBAL.UpdateAutoPolicyNo(Model1);
        }
        #endregion

        #region Dependencies
        public List<MemberInvoiceModel> GetInvoices(Guid parlourid, int memberId)
        {
            return MembersBAL.GetInvoicesByMemberID(parlourid, memberId);
        }
        public List<SocietyModel> SocietyByparlourId(Guid parlourid)
        {
            return CommonBAL.GetSocietyByParlourId(parlourid);
        }
        public List<SocietyModel> GetAllSociety(Guid parlourid)
        {
            return CommonBAL.GetAllSociety(parlourid);
        }
        public List<BranchModel> BranchByparlourId(Guid parlourid)
        {
            return CommonBAL.GetBranchByParlourId(parlourid);
        }
        public List<BranchModel> GetAllBranch(Guid Parlourid)
        {
            return CommonBAL.GetAllBranch(Parlourid);
        }
        public List<PlanModel> GetAllPlanName(Guid ParlourId)
        {
            return CommonBAL.GetAllPlanName(ParlourId);
        }
        public bool DeleteDependentById(int dependentId)
        {
            return MembersBAL.GetFamilyDependencyTypes(dependentId);
        }
        public bool DeleteSUpportdocumentById(int DocumentId)
        {
            return MembersBAL.DeleteSUpportdocumentById(DocumentId);
        }
        #endregion

        #region Members note
        public int SaveMemberNote(MemberNotesModel model)
        {
            return MembersBAL.NotesSaveMember(model);
        }
        public int UpdateNotesMember(MemberNotesModel modal)
        {
            return MembersBAL.UpdateNotesMember(modal);
        }

        public List<MemberNotesModel> GetMemberNoteByMemberID(int MemberID)
        {
            return MembersBAL.GetMemberNoteByMemberID(MemberID);
        }
        #endregion


        #region SupportDocuments
        public List<SupportedDocumentModel> SelectSupportDocumentsByMemberId(int MemberId)
        {
            return MembersBAL.SelectSupportDocumentsByMemberId(MemberId);
        }

        public int SaveSupportDocument(SupportedDocumentModel model)
        {
            return MembersBAL.SaveSupportDocument(model);

        }
        public SupportedDocumentModel SelectSupportDocumentsById(int DocumentId)
        {
            return MembersBAL.SelectSupportDocumentsById(DocumentId);

        }
        public List<FamilyDependencyTypeModel> GetAllFamilyDependencyTypes()
        {
            return MembersBAL.GetAllFamilyDependencyTypes();
        }
        public int SaveFamilyDependency(FamilyDependencyModel model)
        {
            try
            {
                return MembersBAL.SaveFamilyDependency(model);
            }
            catch (Exception exc)
            {
                throw new FaultException<FuneralServiceFault>(new FuneralServiceFault(exc.Message));
            }

        }
        public int UpdateFamilyDependency(FamilyDependencyModel model)
        {
            try
            {
                return MembersBAL.UpdateFamilyDependency(model);
            }
            catch (Exception exc)
            {
                throw new FaultException<FuneralServiceFault>(new FuneralServiceFault(exc.Message));
            }
        }
        public FamilyDependencyModel SelectFamilyDependencyById(int pkiDependentID)
        {
            return MembersBAL.SelectFamilyDependencyById(pkiDependentID);
        }

        #endregion

        #region AddonProducts
        public List<AddonProductsModal> SelectProductAddonProducts(Guid ParlourId)
        {
            return MembersBAL.SelectProductName(ParlourId);
        }
        public int SaveAddonProducts(MemberAddonProductsModel model)
        {
            return MembersBAL.SaveAddonProducts(model);
        }
        public List<MemberAddonProductsModel> SelectMemberAddonProducts(int MemberId)
        {
            return MembersBAL.SelectMemberAddonProducts(MemberId);
        }
        public void MemberAddonProductsRemove(Guid MemberID)
        {
            MembersBAL.MemberAddonProductsRemove(MemberID);
        }
        public List<AddonProductsModal> MemberListBindAddonProduct(Guid pkiProductID)
        {
            return MembersBAL.MemberListBindAddonProduct(pkiProductID);
        }
        public List<MemberAddonProductsModel> SelectMembarAddonProductBypkiMemberProductID(Guid MemberId)
        {
            return MembersBAL.SelectMembarAddonProductBypkiMemberProductID(MemberId);
        }
        public int AddonProductUpdateMember(MemberAddonProductsModel model)
        {
            return MembersBAL.AddonProductUpdateMember(model);
        }
        #endregion

        #region Bank Module
        public List<AccountTypeModel> AccountTypeSelectAll()
        {
            return BanksBAL.AccountTypeSelectAll();
        }
        public List<BankModel> GetAllBank()
        {
            return BanksBAL.SelectAll();
        }
        public string GetMemberNumber(Guid ParlourId)
        {
            return CommonBAL.GetMemberNumber(ParlourId);
        }
        public List<AgentModel> GetAllAgent(Guid ParlourId)
        {
            return MembersBAL.SelectAllAgent(ParlourId);
        }
        #endregion

        #region Bank Module
        public List<CountryModel> GetCountry()
        {
            return MembersBAL.GetCountry();
        }
        #endregion
        public double SumPremium(int fkiMemberid, Guid ParlourId)
        {
            return MembersBAL.SumOfPremium(fkiMemberid, ParlourId);
        }

        #region Members Payment Section
        public MembersPaymentViewModel GetAllPayentMembers(Guid ParlourId, string PolicyNo, string IDNumber, int PageSize, int PageNum, string SortBy, string SortOrder, string PolicyStatus)
        {
            return MemberPaymentBAL.GetAllPayentMembers(ParlourId, PolicyNo, IDNumber, PageSize, PageNum, SortBy, SortOrder, PolicyStatus);
        }

        public MembersPaymentDetailsModel GetMemberPaymnetDetailsByID(int ID)
        {
            return MemberPaymentBAL.GetMemberPaymnetDetailsByID(ID);
        }

        public MembersPaymentDetailsModel GetMemberPlanDetailsWithBalance(string MemberNo, Guid ParlourId)
        {
            return MemberPaymentBAL.ReturnMemberPlanDetailsWithBalance(MemberNo, ParlourId);
        }

        public MembersPaymentDetailsModel GetMemberPlanDetailsWithByMemberNo(string MemberNo)
        {
            return MemberPaymentBAL.ReturnMemberPlanDetailsWithBalance(MemberNo);
        }

        public int AddPayments(MembersPaymentDetailsModel model, bool IsJoiningFee)
        {
            return MemberPaymentBAL.AddPayments(model, IsJoiningFee);
        }

        public int AddReversalPayments(int InvoiceId, string UserId, Guid Parlourid)
        {
            return MemberPaymentBAL.AddReversalPayments(InvoiceId, UserId, Parlourid);
        }

        public int AddFuneralPayments(FuneralPaymentsModel model)
        {
            return MemberPaymentBAL.AddFuneralPayments(model);
        }
        public List<FuneralPaymentsModel> ReturnFuneralPayments(Guid parlourid, string pIntFnrlID)
        {
            return MemberPaymentBAL.ReturnFuneralPayments(parlourid, pIntFnrlID);
        }
        public List<FuneralPaymentsModel> ReturnMemberPayment(string intMemberID)
        {
            return MemberPaymentBAL.ReturnMemberPayment(intMemberID);
        }
        #endregion

        #region check Duplicate Member
        public MembersModel GetMemberByIDNum(string ID, Guid ParlourId)
        {
            return MembersBAL.GetMemberByIDNum(ID, ParlourId);
        }
        public FamilyDependencyModel GetDependencByIDNum(string ID, Guid ParlourId, int MemberID)
        {
            return MembersBAL.GetDependencByIDNum(ID, ParlourId, MemberID);
        }
        #endregion
        public int InsertSendReminder(SendReminderModel model)
        {
            return MemberPaymentBAL.InsertSendReminder(model);
        }

        #region Quotation
        public int SaveQuotation(QuotationModel model)
        {
            return QuotationBAL.SaveQuotation(model);
        }
        public int UpdateQuotation(QuotationModel model)
        {
            return QuotationBAL.UpdateQuotation(model);
        }
        public int DeleteQuotation(int ID, string UserName)
        {
            return QuotationBAL.QuotationDelete(ID, UserName);
        }
        public QuotationModel SelectQuotationByQuotationId(int ID, Guid ParlourId)
        {
            return QuotationBAL.SelectQuotationByQuotationId(ID, ParlourId);
        }
        public List<QuotationModel> GetAllQuotationByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            return QuotationBAL.SelectQuotationByQuotationId(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
        }
        public List<QuotationModel> GetQuotationNumberByID(Guid ParlourId)
        {
            return QuotationBAL.GetQuotationNumberByID(ParlourId);
        }

        #region QuotationServices
        public List<QuotationServicesModel> GetAllQuotationServices(Guid ParlourId)
        {
            return QuotationBAL.GetAllQuotationServices(ParlourId);
        }
        public QuotationServicesModel GetServiceByID(int ID)
        {
            return QuotationBAL.GetServiceByID(ID);
        }
        public int SaveService(QuotationServicesModel model)
        {
            return QuotationBAL.SaveService(model);
        }
        public List<QuotationServicesModel> SelectServiceByQoutationID(int QuotationID)
        {
            return QuotationBAL.SelectServiceByQoutationID(QuotationID);
        }
        public int DeleteServiceByID(int pkiQuotationSelectionID)
        {
            return QuotationBAL.DeleteServiceByID(pkiQuotationSelectionID);
        }

        public QuotationModel GetQuotationNumberByID2(Guid ParlourId)
        {
            return QuotationBAL.GetQuotationNumberByID2(ParlourId);
        }

        public int UpdateAllData(int QuotationID, string Notes, string QuotationNumber)
        {
            return QuotationBAL.UpdateAllData(QuotationID, Notes, QuotationNumber);
        }

        public int SaveDiscountAndAmount(QuotationDiscountModel model)
        {
            return QuotationBAL.SaveDiscountAndAmount(model);
        }
        public QuotationDiscountModel GetAllQuotationDiscounts(int QuotationID)
        {
            return QuotationBAL.GetAllQuotationDiscounts(QuotationID);
        }
        public QuotationServicesModel SelectServiceByQouAndID(int QuotationID, int pkiQuotationSelectionID)
        {
            return QuotationBAL.SelectServiceByQouAndID(QuotationID, pkiQuotationSelectionID);
        }
        public int SaveQuotationMessage(QuotationMessageModel model)
        {
            return QuotationBAL.SaveQuotationMessage(model);
        }
        public QuotationMessageModel SelectQuotationMessageByID(int QuotationID)
        {
            return QuotationBAL.SelectQuotationMessageByID(QuotationID);
        }
        public int SaveQuotationTaxAndDiscount(int QuotationID, Decimal Tax, Decimal Discount)
        {
            return QuotationBAL.SaveQuotationTaxAndDiscount(QuotationID, Tax, Discount);
        }
        public int QuotationStatus(int QuotationID, Guid parlourid, string status)
        {
            return QuotationBAL.QuotationStatus(QuotationID, parlourid, status);
        }

        #endregion
        #endregion

        #region Funeral
        public List<FuneralModel> SelectAllFuneralByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            return FuneralBAL.SelectAllFuneralByParlourId(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder,null,null,null);
        }
        public int FuneralDelete(int ID, string UserName)
        {
            return FuneralBAL.FuneralDelete(ID, UserName);
        }

        public int SaveFuneral(FuneralModel model)
        {
            return FuneralBAL.SaveFuneral(model);
        }
        public FuneralModel SelectFuneralBypkid(int ID, Guid ParlourId)
        {
            return FuneralBAL.SelectFuneralBypkid(ID, ParlourId);
        }
        public FuneralModel SelectFuneralByMemberNo(string MemberNo)
        {
            return FuneralBAL.SelectFuneralByMemberNo(MemberNo);
        }
        #region FuneralService
        public int SaveFuneralService(FuneralServiceSelectModel model)
        {
            return FuneralBAL.SaveFuneralService(model);
        }
        public List<FuneralServiceSelectModel> SelectServiceByFuneralID(int fkiFuneralID)
        {
            return FuneralBAL.SelectServiceByFuneralID(fkiFuneralID);
        }

        public FuneralServiceSelectModel SelectServiceByFunAndID(int fkiFuneralID, int pkiFuneralServiceSelectionID)
        {
            return FuneralBAL.SelectServiceByFunAndID(fkiFuneralID, pkiFuneralServiceSelectionID);
        }
        public int DeleteFuneralServiceByID(int pkiFuneralServiceSelectionID)
        {
            return FuneralBAL.DeleteFuneralServiceByID(pkiFuneralServiceSelectionID);
        }
        public FuneralModel GetInvoiceNumberByID(Guid ParlourId)
        {
            return FuneralBAL.GetInvoiceNumberByID(ParlourId);
        }
        public int UpdateAllFuneralData(int pkiFuneralID, string Notes, Decimal DisCount, Decimal Tax)
        {
            return FuneralBAL.UpdateAllFuneralData(pkiFuneralID, Notes, DisCount, Tax);
        }
        #endregion

        #region Funeral Doc
        public int SaveFuneralSupportedDocument(FuneralDocumentModel model)
        {
            return FuneralBAL.SaveFuneralSupportedDocument(model);
        }
        public List<FuneralDocumentModel> SelectFuneralDocumentsByMemberId(int fkiFuneralID)
        {
            return FuneralBAL.SelectFuneralDocumentsByMemberId(fkiFuneralID);
        }
        public int DeleteFuneraldocumentById(int pkiFuneralPictureID)
        {
            return FuneralBAL.DeleteFuneraldocumentById(pkiFuneralPictureID);
        }
        public FuneralDocumentModel SelectFuneralDocumentsByPKId(int DocumentId)
        {
            return FuneralBAL.SelectFuneralDocumentsByPKId(DocumentId);
        }

        #endregion
        #endregion

        #region TombStone
        public List<TombStoneModel> SelectAllTombStoneByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            return TombStoneBAL.SelectAllTombStoneByParlourId(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
        }
        public int TombStoneDelete(int ID, string UserName)
        {
            return TombStoneBAL.TombStoneDelete(ID, UserName);
        }
        public int SaveTombStone(TombStoneModel model)
        {
            return TombStoneBAL.SaveTombStone(model);
        }
        public TombStoneModel SelectTombStoneByParlAndPki(int pkiTombstoneID, Guid ParlourId)
        {
            return TombStoneBAL.SelectTombStoneByParlAndPki(pkiTombstoneID, ParlourId);
        }
        public int UploadTombImage(string ImageName, string ImagePath, int pkiTombstoneID, Guid parlourid)
        {
            return TombStoneBAL.UploadTombImage(ImageName, ImagePath, pkiTombstoneID, parlourid);
        }
        public TombStoneModel GetInvoiceNumOfTombByParlID(Guid ParlourId)
        {
            return TombStoneBAL.GetInvoiceNumOfTombByParlID(ParlourId);
        }
        public List<TombStoneServiceSelectModel> SelectServiceByTombStoneID(int fkiTombstoneID)
        {
            return TombStoneBAL.SelectServiceByTombStoneID(fkiTombstoneID);
        }
        public int DeleteTombstoneServiceByID(int pkiTombStoneSelectionID)
        {
            return TombStoneBAL.DeleteTombstoneServiceByID(pkiTombStoneSelectionID);
        }
        public TombStoneServiceSelectModel SelectServiceByTombAndID(int fkiTombstoneID, int pkiTombStoneSelectionID)
        {
            return TombStoneBAL.SelectServiceByTombAndID(fkiTombstoneID, pkiTombStoneSelectionID);
        }
        public int SaveTombStoneService(TombStoneServiceSelectModel model)
        {
            return TombStoneBAL.SaveTombStoneService(model);
        }
        public int UpdateAllTombStoneData(int pkiTombstoneID, Decimal DisCount, Decimal Tax, string InvoiceNumber)
        {
            return TombStoneBAL.UpdateAllTombStoneData(pkiTombstoneID, DisCount, Tax, InvoiceNumber);
        }

        #endregion

        #region Funeral Service Manage
        public List<FuneralServiceManageModel> SelectFuneralManageServiceByParlID(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            return ToolsSetingBAL.SelectFuneralManageServiceByParlID(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
        }
        public int SaveFuneralManageService(FuneralServiceManageModel Model1)
        {
            return ToolsSetingBAL.SaveFuneralManageService(Model1);
        }
        public FuneralServiceManageModel SelectFuneralManageServiceByParlANdID(int pkiServiceID, Guid ParlourId)
        {
            return ToolsSetingBAL.SelectFuneralManageServiceByParlANdID(pkiServiceID, ParlourId);
        }
        public int DeleteFuneralManageServiceById(int pkiServiceID)
        {
            return ToolsSetingBAL.DeleteFuneralManageServiceById(pkiServiceID);
        }
        public List<ApplicationSettingsModel> GetAllApplicationList(Guid parlourid, int param, int AppID)
        {
            return ToolsSetingBAL.GetAllApplicationList(parlourid, param, AppID);
        }
        public ApplicationSettingsModel GetAllApplicationList2(Guid parlourid, int param, int AppID)
        {
            return ToolsSetingBAL.GetAllApplicationList2(parlourid, param, AppID);
        }

        public List<SecureUserGroupsModel> GetSuperUserAccessByID(int ID, Guid ParlourId)
        {
            return ToolsSetingBAL.GetSuperUserAccessByID(ID, ParlourId);
        }
        #endregion

        #region Claims
        public int SaveClaims(ClaimsModel model)
        {
            return ClaimsBAL.SaveClaims(model);
        }
        public List<ClaimsModel> SelectAllClaimsByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, DateTime DateFrom, DateTime DateTo, string status)
        {
            return ClaimsBAL.SelectAllClaimsByParlourId(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder, DateFrom, DateTo, status);
        }
        public int ClaimsDelete(int ID, string UserName)
        {
            return ClaimsBAL.ClaimsDelete(ID, UserName);
        }
        public ClaimsViewModel SelectAllClaimsBySearch(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool ClaimingForMember, bool ApplyWaitingPeriod)
        {
            return ClaimsBAL.SelectAllClaimsBySearch(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder, ClaimingForMember, ApplyWaitingPeriod);
        }
        public List<ClaimsModel> GetClaimsbyMemeberNumber(int MemeberNo)
        {
            return ClaimsBAL.GetClaimsbyMemeberNumber(MemeberNo);
        }
        public ClaimsModel SelectClaimsBypkid(int ID, Guid ParlourId)
        {
            return ClaimsBAL.SelectClaimsBypkid(ID, ParlourId);
        }
        public List<MembersModel> SelectMembersAndDependencies1(Guid ParlourId, bool MainMem, string Keyword)
        {
            return ClaimsBAL.SelectMembersAndDependencies1(ParlourId, MainMem, Keyword);
        }
        public List<FamilyDependencyModel> SelectMembersAndDependencies2(Guid ParlourId, bool MainMem, string Keyword)
        {
            return ClaimsBAL.SelectMembersAndDependencies2(ParlourId, MainMem, Keyword);
        }
        public MembersModel selectMemberByPkidAndParlor(Guid ParlourId, int MemId)
        {
            return ClaimsBAL.selectMemberByPkidAndParlor(ParlourId, MemId);
        }
        public PlanModel GetPlanDetailsByPlanId(int planid)
        {
            return ClaimsBAL.GetPlanDetailsByPlanId(planid);
        }
        public int UpdateMemberNumber(int ID, string MemberNumber, string Claimnumber)
        {
            return ClaimsBAL.UpdateMemberNumber(ID, MemberNumber, Claimnumber);
        }
        public void ClaimStatusChangeReason(ChangeStatusReason model)
        {
            ClaimsBAL.ClaimStatusChangeReason(model);
        }
        public int UpdateClaimStatus(int ID, string status)
        {
            return ClaimsBAL.UpdateClaimStatus(ID, status);
        }
        #endregion

        #region Claim Doc
        public int SaveClaimSupportedDocument(ClaimDocumentModel model)
        {
            return ClaimsBAL.SaveClaimSupportedDocument(model);
        }
        public List<ClaimDocumentModel> SelectClaimDocumentsByClaimId(int fkiClaimID)
        {
            return ClaimsBAL.SelectClaimDocumentsByClaimId(fkiClaimID);
        }
        public int DeleteClaimsdocumentById(int pkiClaimPictureID)
        {
            return ClaimsBAL.DeleteClaimsdocumentById(pkiClaimPictureID);
        }
        public ClaimDocumentModel SelectClaimsDocumentsByPKId(int DocumentId)
        {
            return ClaimsBAL.SelectClaimsDocumentsByPKId(DocumentId);
        }
        #endregion

        #region Invoice payment Print
        public NewMemberInvoiceModel GetInvoiceByid(int InvoiceId)
        {
            return MembersBAL.GetInvoiceByid(InvoiceId);
        }
        #endregion
        #region Chart
        public DataTable PolicyStatusPieChart(Guid parlourid)
        {
            return MembersBAL.PolicyStatusPieChart(parlourid);
        }
        public List<PolicyPremiumDashboardModel> SelectPolicyPremiumByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool isAdmin, bool isSuperUser, string UserName)
        {
            return MembersBAL.SelectPolicyPremiumByParlourId(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder, isAdmin, isSuperUser, UserName);
        }
        #endregion

        #region Tomstone Payment & Invoice
        public int TombStonesPaymentSave(TombStonesPaymentModel model)
        {
            return TombStonesPaymentBAL.AddInvoice(model);
        }

        public TombStonesPaymentModel TombStonesPaymentSelect(Guid ParlourId, int InvoiceId)
        {
            return TombStonesPaymentBAL.TombStonesPaymentSelect(ParlourId, InvoiceId);
        }

        public List<TombStonesPaymentModel> TombStonesPaymentSelectByTombstoneId(Guid ParlourId, int TombstoneId)
        {
            return TombStonesPaymentBAL.TombStonesPaymentSelectByTombstoneID(ParlourId, TombstoneId);
        }

        public int GetPrintCounter(int invoiceId, Guid parlourId)
        {
            return MemberPaymentBAL.GetPrintCounter(invoiceId, parlourId);
        }
        #endregion

        #region Package Service
        public List<PackageServiceModel> GetAllPackage(Guid parlourId)
        {
            return FuneralPackageBAL.SelectPackage(parlourId);
        }

        public List<PackageServicesSelectionModel> GetPackageService(Guid parlourId, string PackageName)
        {
            return FuneralPackageBAL.SelectPackageService(parlourId, PackageName);
        }

        public int SavePackage(PackageServiceModel model)
        {
            return FuneralPackageBAL.SavePackage(model);
        }

        public int SavePackageService(PackageServiceModel model)
        {
            return FuneralPackageBAL.SavePackageService(model);
        }

        public void DeletePackageService(int Id)
        {
            FuneralPackageBAL.DeletePackageService(Id);
        }

        public List<PackageServicesSelectionModel> GetPackageServiceByPackgeId(Guid ParlourId, int PackgeId)
        {
            return FuneralPackageBAL.SelectPackageServiceByPackgeId(ParlourId, PackgeId);
        }
        #endregion

        #region Tomstone Package Service
        public List<TombstonePackageModel> GetTombstoneAllPackage(Guid ParlourId)
        {
            return TombstonePackageBAL.SelectAllPackage(ParlourId);
        }

        public int SaveTombstonePackage(TombstonePackageModel model)
        {
            return TombstonePackageBAL.SavePackage(model);
        }

        public int SaveTombstonePackageService(TombstonePackageModel model)
        {
            return TombstonePackageBAL.SavePackageService(model);
        }

        public void DeleteTombstonePackageService(int Id)
        {
            TombstonePackageBAL.DeletePackageService(Id);
        }

        public List<TombstonePackageModel> GetTombstonePackageServiceByPackgeId(Guid ParlourId, int PackgeId)
        {
            return TombstonePackageBAL.SelectPackageServiceByPackgeId(ParlourId, PackgeId);
        }

        public void DeletePackage(int Id, Guid parlourId)
        {
            FuneralPackageBAL.DeletePackage(Id, parlourId);
        }
        #endregion

        #region Check for joining fees
        public JoiningFeeModel JoiningFees(int memberId, Guid parlourId)
        {
            return MemberPaymentBAL.JoiningFees(memberId, parlourId);
        }
        #endregion

        #region Custom detail save information
        public int SaveCustomDetails(CustomDetails model)
        {
            return CustomDetailsBAL.CustomDetailsSave(model);
        }

        public void UpdateCustomDetails(CustomDetails model)
        {
            CustomDetailsBAL.CustomDetailsUpdate(model);
        }


        public CustomDetails GetCustomDetails(Guid ParlourId, int Id, int CustomType)
        {
            return CustomDetailsBAL.GetCustomDetails(Id, ParlourId, CustomType);
        }


        public List<CustomDetails> GetAllCustomDetailsByParlourId(Guid ParlourId, int CustomType)
        {
            return CustomDetailsBAL.GetAllCustomDetailsByParlourId(ParlourId, CustomType);
        }

        public void DeleteCustomDetails(CustomDetails model)
        {
            CustomDetailsBAL.CustomDetailsDelete(model);
        }
        #endregion

        #region Rights
        public List<RightsModel> tblRightGetAll()
        {
            return RightsBAL.tblRightGetAll();
        }
        public int SavetblRight(RightsModel model)
        {
            return RightsBAL.SavetblRight(model);
        }
        public RightsModel SelecttblRightById(int ID)
        {
            return RightsBAL.SelecttblRightById(ID);
        }
        public List<NewRightsModel> GetRightsByGroupId(Guid ParlourId, int GroupId)
        {
            return RightsBAL.GetRightsByGroupId(ParlourId, GroupId);
        }
        public List<tblPageModel> GetAllActiveTblPages()
        {
            return RightsBAL.GetAllActiveTblPages();
        }
        public int SaveTblRights(NewRightsModel model)
        {
            return RightsBAL.SaveTblRights(model);
        }
        public List<tblPageModel> LoadSideMenu(Guid ParlourId, int UserId)
        {
            return RightsBAL.LoadSideMenu(ParlourId, UserId);
        }

        #endregion

        #region OtherPayment
        public int OtherPaymentsSave(OtherPaymentModel model)
        {
            return OtherPaymentBAL.OtherPaymentDetailsSave(model);
        }

        public List<OtherPaymentModel> OtherPaymentSelectByMemberId(int MemberId, Guid Parlourid)
        {
            return OtherPaymentBAL.OtherPaymentSelectByMemberId(MemberId, Parlourid);
        }

        public OtherPaymentModel GetOtherPayment(int InvoiceId, Guid Parlourid)
        {
            return OtherPaymentBAL.OtherPaymentSelect(InvoiceId, Parlourid);
        }
        #endregion

        #region Underwriter

        public string GetPlanUnderwriterByPlanId(int pkiPlanID)
        {
            return CommonBAL.GetPlanUnderwriterByPlanId(pkiPlanID);
        }
        public List<UnderwriterModel> SelectAllUnderwriterByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            return UnderwriterBAL.SelectAllUnderwriterByParlourId(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
        }
        public int SaveUnderwriter(UnderwriterModel model)
        {
            return UnderwriterBAL.SaveUnderwriter(model);
        }
        public UnderwriterModel SelectUnderwriterBypkid(int ID, Guid ParlourId)
        {
            return UnderwriterBAL.SelectUnderwriterBypkid(ID, ParlourId);
        }
        public int DeleteUnderwriterByID(int PkiUnderwriterId, string UserName)
        {
            return UnderwriterBAL.DeleteUnderwriterByID(PkiUnderwriterId, UserName);
        }
        public UnderwriterModel SelectUnderwriterByName(string UnderwriterName, Guid ParlourId)
        {
            return UnderwriterBAL.SelectUnderwriterByName(UnderwriterName, ParlourId);
        }
        public List<UnderwriterModel> SelectUnderwriterNotDeleted()
        {
            return UnderwriterBAL.SelectUnderwriterNotDeleted();
        }
        #endregion
        #region UnderwriterPremium

        public int DeleteUnderwriterPremium(int ID, string UserName)
        {
            return UnderwriterPremiumBAL.DeleteUnderwriterPremium(ID, UserName);
        }

        public int DeleteUnderwriterSetupByID(int ID, string UserName)
        {
            return UnderwriterSetupBAL.DeleteUnderwriterSetupByID(ID, UserName);
        }




        public List<UnderwriterPremiumModel> GetAllUnderwriterPlansByParlourID(Guid ParlourID, int UnderwriterPlanSelection)
        {
            return ToolsSetingBAL.GetAllUnderwriterPlansByParlourID(ParlourID, UnderwriterPlanSelection);
        }
        public List<PlanModel> GetAllPlansList(Guid ParlourId)
        {
            return ToolsSetingBAL.GetAllPlansList(ParlourId);
        }
        public List<UnderwriterPremiumModel> SelectAllUnderwriterPremiumByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            return UnderwriterPremiumBAL.SelectAllUnderwriterPremiumByParlourId(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
        }
        public UnderwriterPremiumModel SelectUnderwriterPremiumByName(int FkiUnderwriterId, Guid ParlourId)
        {
            return UnderwriterPremiumBAL.SelectUnderwriterPremiumByName(FkiUnderwriterId, ParlourId);
        }
        public int SaveUnderwriterPremium(UnderwriterPremiumModel model)
        {
            return UnderwriterPremiumBAL.SaveUnderwriterPremium(model);
        }

        public UnderwriterPremiumModel EditUnderwriterPremiumbyID(int ID, Guid ParlourId)
        {
            return UnderwriterPremiumBAL.EditUnderwriterPremiumbyID(ID, ParlourId);
        }
        #endregion

        #region "get status detail list"
        /// <summary>
        /// Get status list.
        /// </summary>
        /// <param name="associatedTable"></param>
        /// <returns></returns>
        public List<StatusModel> GetStatus(string associatedTable)
        {
            return CommonBAL.GetStatus(associatedTable);
        }
        #endregion

        #region Update member status changes
        /// <summary>
        /// Update member status
        /// </summary>
        /// <param name="parlourId"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public void UpdateMemberStatus(Guid parlourId, int id, string status)
        {
            MembersBAL.UpdateMemberStatus(parlourId, id, status);
        }

        /// <summary>
        /// Made new entry every time status of member is changes.
        /// </summary>
        /// <param name="model"></param>
        public void MemberStatusChangeReason(ChangeStatusReason model)
        {
            MembersBAL.MemberStatusChangeReason(model);
        }
        #endregion
        #region RolePlayer
        public List<RolePlayerModel> GetAllRolePlayer(Guid ParlourId)
        {
            return ToolsSetingBAL.GetAllRolePlayer(ParlourId);
        }
        #endregion
        #region UnderWriterSetup
        public List<UnderwriterSetupModel> GetUnderwriterSetupNameByParlourId(Guid parlourid)
        {
            return CommonBAL.GetUnderwriterSetupNameByParlourId(parlourid);
        }
        public List<UnderwriterSetupModel> SelectAllUnderwriterSetupByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            return UnderwriterSetupBAL.SelectAllUnderwriterSetupByParlourId(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
        }
        public UnderwriterSetupModel SelectUnderwriterSetupByName(string UnderwriterName, Guid ParlourId)
        {
            return UnderwriterSetupBAL.SelectUnderwriterSetupByName(UnderwriterName, ParlourId);
        }
        public int SaveUnderwriterSetup(UnderwriterSetupModel model)
        {
            return UnderwriterSetupBAL.SaveUnderwriterSetup(model);
        }

        public UnderwriterSetupModel EditUnderwriterSetupbyID(int ID, Guid ParlourId)
        {
            return UnderwriterSetupBAL.EditUnderwriterSetupbyID(ID);
        }

        #endregion

        #region Tax Settings
        public List<TaxSetting> GetTaxSetting()
        {
            return TaxSettingBAL.GetAllTaxSettings();
        }
        public int InsertRecordForTax(TaxSetting model)
        {
            return TaxSettingBAL.InsertRecordForTax(model);
        }
        public TaxSetting GetTaxSettingById(int id)
        {
            return TaxSettingBAL.GetTaxSettingById(id);
        }
        public int DeleteTaxSettingById(int id)
        {
            return TaxSettingBAL.DeleteTaxSettingById(id);
        }

        public int UpdateRecordForTax(TaxSetting model)
        {
            return TaxSettingBAL.UpdateRecordForTax(model);
        }
        #endregion
        #region multiple pollicy for same member
        public List<MembersModel> GetPolicyByMemberIDNumber(string ID, Guid ParlourId)
        {
            return MembersBAL.GetPolicyByMemberIDNumber(ID, ParlourId);
        }
        #endregion

        public int SaveScheduleEmailReport(ScheduleEmailReportModel model)
        {
            return ScheduleEmailReportBAL.SaveScheduleEmailReport(model);

        }

        public List<ScheduleEmailReportModel> GetScheduleEmailReportByParlourId(Guid ParlourId)
        {
            return ScheduleEmailReportBAL.GetScheduleEmailReportByParlourId(ParlourId);
        }

        public ScheduleEmailReportModel GetScheduleById(int ID, Guid ParlourId)
        {
            return ScheduleEmailReportBAL.GetScheduleById(ID, ParlourId);
        }

        public int UpdateScheduleByScheduleId(ScheduleEmailReportModel model)
        {
            return ScheduleEmailReportBAL.UpdateScheduleByScheduleId(model);
        }

        public int DeleteScheduleEmailReport(int ID)
        {
            return ScheduleEmailReportBAL.DeleteScheduleEmailReport(ID);

        }
        public double SumPremiumByPlanId(int planId, int fkiMemberid, Guid ParlourId)
        {
            return MembersBAL.SumOfPremium(planId, fkiMemberid, ParlourId);
        }

        public int DeleteMemberNote(int ID)
        {
            return MembersBAL.DeleteMemberNote(ID);

        }
        public string GetCurrencyByParlourId(Guid ParlourId)
        {
            return MemberPaymentBAL.GetCurrencyByParlourId(ParlourId);
        }
        public void DeleteTombstonePackage(int Id)
        {
            TombstonePackageBAL.DeleteTombstonePackage(Id);
        }
        public ProgressStatus CheckProgressStatus(int ID, Guid ParlourId)
        {
            return ToolsSetingBAL.CheckProgressStatus(ID, ParlourId);
        }
        public List<VendorModel> GetVendorNameByParlourId(Guid parlourid)
        {
            return ToolsSetingBAL.GetVendorNameByParlourId(parlourid);
        }

        public void MemberRowImportToMember(string memberType, Guid importId)
        {
            MembersBAL.MemberRowImportToMember(memberType, importId);
            //return MembersBAL.MemberRowImportToMember(MemberType);
        }

        public List<PolicyModel> GetPlanSubscriptionByPlanIdNewMember(int pkiPlanID)
        {
            return CommonBAL.GetPlanSubscriptionByPlanIdNewMember(pkiPlanID);
        }

        public int GetLastCopiedMemberForDependency()
        {
            return MembersBAL.GetLastCopiedMemberForDependency();

        }
    }
}
