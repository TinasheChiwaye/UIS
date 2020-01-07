using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Funeral.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFuneralServices" in both code and config file together.
    [ServiceContract]
    public interface IFuneralServices
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        AdminModel DoLogin(string UserName, string Password);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int SaveMember(MembersModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        ApplicationSettingsModel SaveApplication(ApplicationSettingsModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        AgentInfoSetupModel SaveAgentInfo(AgentInfoSetupModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        AdditionalApplicationSettingsModel SaveAdditionalApplication(AdditionalApplicationSettingsModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        BankingDetailSending SaveBankingDetail(BankingDetailSending Model);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveUserDetails(SecureUsersModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SavePlanDetails(PlanModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        Guid SaveBranchDetails(BranchModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveSocietyDetails(SocietyModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveVendorDetails(VendorModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int UpdatesmsTemplate(smsTempletModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveExpensesDetails(ExpensesModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveUserGroupDetails(SecureUserGroupsModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveSmsGroupDetails(smsSendingGroupModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        Guid SaveAddonProductDetails(AddonProductsModal model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteMember(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int MemberDeleteLogical(int id, string UserName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteUser(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeletePlan(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteCompany(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteAgent(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteBranch(Guid ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteSociety(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteVendor(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteExpenses(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteAddonProduct(Guid ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        SecureUsersModel EditUserbyID(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        PlanModel EditPlanbyID(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        BranchModel EditBranchbyID(Guid ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        SocietyModel EditSocietybyID(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        VendorModel EditVendorbyID(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        smsTempletModel GetEmailTemplateByID(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        ExpensesModel EditExpensesbyID(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<SecureUserGroupsModel> EditSecurUserbyID(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<smsSendingGroupModel> EditsmsGroupbyID(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        smsSendingGroupModel GetsmsGroupbyID(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        AddonProductsModal EditAddonProductbyID(Guid ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        MembersModel GetMemberByID(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        MembersModel GetMemberByIDNum(string ID, Guid ParlourId);        

         [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        FamilyDependencyModel GetDependencByIDNum(string ID, Guid ParlourId, int MemberID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        SecureUsersModel GetUserByID(string UserID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        SecureUsersModel GetUserByEmailID(string UserID);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        PlanModel GetPlanByID(string UserID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        SecureUserGroupsModel GetUserAccessByID(int UserID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        BranchModel GetBranchByID(string UserID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        AddonProductsModal GetAddonProductByID(string UserID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        SocietyModel GetSocietyByID(string UserID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        VendorModel GetVendorByID(string UserID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        ExpensesModel GetExpensesByID(string UserID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        ApplicationSettingsModel GetApplictionByParlourID(Guid ParlourId);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        ApplicationSettingsModel GetApplictionByID(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        BankingDetailSending GetBankingByID(Guid ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        AgentInfoSetupModel GetAgentByID(int ID);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<SecureGroupModel> GetSecureGrouList();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        MembersViewModel GetAllMembers(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder,string status);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<SecureUsersModel> GetAllUsers(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<PlanModel> GetAllPlans(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<ApplicationSettingsModel> GetAllCompanys(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<AgentInfoSetupModel> GetAllAgentInfo(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<BranchModel> GetAllBranches(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<SocietyModel> GetAllSocietyes(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<SocietyModel> GetAllSocietye(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<VendorModel> GetAllVendores(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<smsTempletModel> GetTemplateList(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<ExpensesModel> GetAllExpenseses(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<AddonProductsModal> GetAllAddonProductes(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<MembersModel> GetAllMemberCellphon(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<PolicyModel> GetPolicyByParlourId(Guid parlourid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<FamilyDependencyModel> FamilyDependency(Guid parlourid, int MemberId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<MemberInvoiceModel> GetInvoices(Guid parlourid, int MemberId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<SocietyModel> SocietyByparlourId(Guid parlourid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<SocietyModel> GetAllSociety(Guid parlourid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<BranchModel> BranchByparlourId(Guid parlourid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<BranchModel> GetAllBranch(Guid Parlourid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<PlanModel> GetAllPlanName(Guid Parlourid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<PolicyModel> GetPlanSubscriptionByPlanId(int pkiPlanID, Guid parlorId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int GetWaitingPeriodByPlanId(int pkiPlanID);




        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        string GetPlanUnderwriterByPlanId(int pkiPlanID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveMemberNote(MemberNotesModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<MemberNotesModel> GetMemberNoteByMemberID(int MemberID);

        //SupportedDocumentModel





        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int UpdateNotesMember(MemberNotesModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        SupportedDocumentModel SelectSupportDocumentsById(int DocumentId);


        //product

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<AddonProductsModal> SelectProductAddonProducts(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveAddonProducts(MemberAddonProductsModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<MemberAddonProductsModel> SelectMemberAddonProducts(int MemberID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        void MemberAddonProductsRemove(Guid MemberID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<AddonProductsModal> MemberListBindAddonProduct(Guid pkiProductID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<MemberAddonProductsModel> SelectMembarAddonProductBypkiMemberProductID(Guid MemberID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int AddonProductUpdateMember(MemberAddonProductsModel model);


        //


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveSupportDocument(SupportedDocumentModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<SupportedDocumentModel> SelectSupportDocumentsByMemberId(int MemberID);


        #region FamilyDependency
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<FamilyDependencyTypeModel> GetAllFamilyDependencyTypes();
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int SaveFamilyDependency(FamilyDependencyModel model);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int UpdateFamilyDependency(FamilyDependencyModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        FamilyDependencyModel SelectFamilyDependencyById(int pkiDependentID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<RelationShipModel> SelectRelationship();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        bool DeleteDependentById(int dependentId);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        bool DeleteSUpportdocumentById(int DocumentId);
        #endregion

        #region Bank Details
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<BankModel> GetAllBank();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        string GetMemberNumber(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<AccountTypeModel> AccountTypeSelectAll();

        #endregion

        #region Country Details
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<CountryModel> GetCountry();
        #endregion

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        double SumPremium(int fkiMemberid, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        MembersPaymentViewModel GetAllPayentMembers(Guid ParlourId, string PolicyNo, string IDNumber, int PageSize, int PageNum, string SortBy, string SortOrder, string PolicyStatus);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        MembersPaymentDetailsModel GetMemberPaymnetDetailsByID(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        MembersPaymentDetailsModel GetMemberPlanDetailsWithBalance(string MemberNo, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        MembersPaymentDetailsModel GetMemberPlanDetailsWithByMemberNo(string MemberNo);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int AddPayments(MembersPaymentDetailsModel model, bool IsJoiningFee);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int AddReversalPayments(int InvoiceId, string UserId, Guid Parlourid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int AddFuneralPayments(FuneralPaymentsModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<FuneralPaymentsModel> ReturnFuneralPayments(Guid parlourid, string pIntFnrlID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<FuneralPaymentsModel> ReturnMemberPayment(string intMemberID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<string> GetDistinctPolicyStatusList();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<AgentModel> GetAllAgent(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int InsertSendReminder(SendReminderModel model);



        #region Quotation Contract
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]

        int SaveQuotation(QuotationModel model);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int UpdateQuotation(QuotationModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteQuotation(int ID, string UserName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        QuotationModel SelectQuotationByQuotationId(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<QuotationModel> GetAllQuotationByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<QuotationModel> GetQuotationNumberByID(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<QuotationServicesModel> GetAllQuotationServices(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        QuotationServicesModel GetServiceByID(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveService(QuotationServicesModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<QuotationServicesModel> SelectServiceByQoutationID(int QuotationID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteServiceByID(int pkiQuotationSelectionID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        QuotationModel GetQuotationNumberByID2(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int UpdateAllData(int QuotationID, string Notes, string QuotationNumber);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveDiscountAndAmount(QuotationDiscountModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        QuotationDiscountModel GetAllQuotationDiscounts(int QuotationID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        QuotationServicesModel SelectServiceByQouAndID(int QuotationID, int pkiQuotationSelectionID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveQuotationMessage(QuotationMessageModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        QuotationMessageModel SelectQuotationMessageByID(int QuotationID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveQuotationTaxAndDiscount(int QuotationID, Decimal Tax, Decimal Discount);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int QuotationStatus(int QuotationID, Guid parlourid, string status);


        #endregion

        #region Funeral
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<FuneralModel> SelectAllFuneralByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int FuneralDelete(int ID, string UserName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveFuneral(FuneralModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        FuneralModel SelectFuneralBypkid(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        FuneralModel SelectFuneralByMemberNo(string MemberNo);

        #region FuneralSErvice
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveFuneralService(FuneralServiceSelectModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<FuneralServiceSelectModel> SelectServiceByFuneralID(int fkiFuneralID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        FuneralServiceSelectModel SelectServiceByFunAndID(int fkiFuneralID, int pkiFuneralServiceSelectionID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteFuneralServiceByID(int pkiFuneralServiceSelectionID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        FuneralModel GetInvoiceNumberByID(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int UpdateAllFuneralData(int pkiFuneralID, string Notes, Decimal DisCount, Decimal Tax);
        #endregion
        #region FuneralDoc
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveFuneralSupportedDocument(FuneralDocumentModel model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        List<FuneralDocumentModel> SelectFuneralDocumentsByMemberId(int fkiFuneralID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteFuneraldocumentById(int pkiFuneralPictureID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        FuneralDocumentModel SelectFuneralDocumentsByPKId(int DocumentId);
        #endregion
        #endregion

        #region AppcationTnc
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveTermsAndCondition(ApplicationTnCModel Model1);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        ApplicationTnCModel SelectApplicationTermsAndCondition(Guid parlourid);
        #endregion

        #region Tombstone
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<TombStoneModel> SelectAllTombStoneByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int TombStoneDelete(int ID, string UserName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveTombStone(TombStoneModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        TombStoneModel SelectTombStoneByParlAndPki(int pkiTombstoneID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int UploadTombImage(string ImageName, string ImagePath, int pkiTombstoneID, Guid parlourid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        TombStoneModel GetInvoiceNumOfTombByParlID(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<TombStoneServiceSelectModel> SelectServiceByTombStoneID(int fkiTombstoneID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteTombstoneServiceByID(int pkiTombStoneSelectionID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        TombStoneServiceSelectModel SelectServiceByTombAndID(int fkiTombstoneID, int pkiTombStoneSelectionID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveTombStoneService(TombStoneServiceSelectModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int UpdateAllTombStoneData(int pkiTombstoneID, Decimal DisCount, Decimal Tax, string InvoiceNumber);
        #endregion

        #region change in Tool Apllication
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int UpdateAutoPolicyNo(ApplicationSettingsModel Model1);
        #endregion

        #region Funeral service Manage

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<FuneralServiceManageModel> SelectFuneralManageServiceByParlID(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveFuneralManageService(FuneralServiceManageModel Model1);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        FuneralServiceManageModel SelectFuneralManageServiceByParlANdID(int pkiServiceID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteFuneralManageServiceById(int pkiServiceID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<ApplicationSettingsModel> GetAllApplicationList(Guid parlourid, int param, int AppID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        ApplicationSettingsModel GetAllApplicationList2(Guid parlourid, int param, int AppID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<SecureUserGroupsModel> GetSuperUserAccessByID(int ID, Guid ParlourId);
        #endregion

        #region Claims
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveClaims(ClaimsModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<ClaimsModel> SelectAllClaimsByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, DateTime DateFrom, DateTime DateTo,string status);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int ClaimsDelete(int ID, string UserName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        ClaimsViewModel SelectAllClaimsBySearch(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool ClaimingForMember, bool ApplyWaitingPeriod);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<ClaimsModel> GetClaimsbyMemeberNumber(int MemeberNo);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        ClaimsModel SelectClaimsBypkid(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<MembersModel> SelectMembersAndDependencies1(Guid ParlourId, bool MainMem, string Keyword);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        //List<FamilyDependencyModel> SelectMembersAndDependencies2(Guid ParlourId, bool MainMem, string Keyword);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        MembersModel selectMemberByPkidAndParlor(Guid ParlourId, int MemId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        PlanModel GetPlanDetailsByPlanId(int planid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int UpdateMemberNumber(int ID, string MemberNumber, string Claimnumber);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int UpdateClaimStatus(int ID, string status);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        void ClaimStatusChangeReason(ChangeStatusReason model);
        #endregion

        #region ClaimDoc
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int SaveClaimSupportedDocument(ClaimDocumentModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<ClaimDocumentModel> SelectClaimDocumentsByClaimId(int fkiClaimID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteClaimsdocumentById(int pkiClaimPictureID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        ClaimDocumentModel SelectClaimsDocumentsByPKId(int DocumentId);
        #endregion

        #region invoice Print
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        NewMemberInvoiceModel GetInvoiceByid(int InvoiceId);
        #endregion

        #region Chart
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        DataTable PolicyStatusPieChart(Guid parlourid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<PolicyPremiumDashboardModel> SelectPolicyPremiumByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool isAdmin, bool isSuperUser, string UserName);
        #endregion

        #region Tomstone Payment & Invoice
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int TombStonesPaymentSave(TombStonesPaymentModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        TombStonesPaymentModel TombStonesPaymentSelect(Guid ParlourId, int InvoiceId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<TombStonesPaymentModel> TombStonesPaymentSelectByTombstoneId(Guid ParlourId, int TombstoneId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int GetPrintCounter(int invoiceId, Guid parlourId);
        #endregion

        #region "Package Selection"
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<PackageServiceModel> GetAllPackage(Guid parlourId);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<PackageServicesSelectionModel> GetPackageService(Guid parlourId, string PackageName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int SavePackage(PackageServiceModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int SavePackageService(PackageServiceModel model);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        void DeletePackageService(int Id);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<PackageServicesSelectionModel> GetPackageServiceByPackgeId(Guid ParlourId, int PackgeId);
        #endregion

        #region Tombstone
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<TombstonePackageModel> GetTombstoneAllPackage(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int SaveTombstonePackage(TombstonePackageModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int SaveTombstonePackageService(TombstonePackageModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        void DeleteTombstonePackageService(int Id);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<TombstonePackageModel> GetTombstonePackageServiceByPackgeId(Guid ParlourId, int PackgeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        void DeletePackage(int Id, Guid parlourId);
        #endregion

        #region Check for joining fees
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        JoiningFeeModel JoiningFees(int memberId, Guid parlourId);

        #endregion

        #region Custom detail save information
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int SaveCustomDetails(CustomDetails model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        void UpdateCustomDetails(CustomDetails model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        CustomDetails GetCustomDetails(Guid ParlourId, int Id, int CustomType);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<CustomDetails> GetAllCustomDetailsByParlourId(Guid ParlourId, int CustomType);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        void DeleteCustomDetails(CustomDetails model);
        #endregion

        #region Rights
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<RightsModel> tblRightGetAll();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int SavetblRight(RightsModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        RightsModel SelecttblRightById(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<NewRightsModel> GetRightsByGroupId(Guid ParlourId, int GroupId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<tblPageModel> GetAllActiveTblPages();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int SaveTblRights(NewRightsModel model);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        //[FaultContract(typeof(FuneralServiceFault))]
        //List<NewRightsModel> GetPageRights(List<int> groupId, int pageId, Guid parlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<tblPageModel> LoadSideMenu(Guid ParlourId, int UserId);
        #endregion

        #region OtherPayment
        /// <summary>
        /// save other payment to  database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>return saved id</returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int OtherPaymentsSave(OtherPaymentModel model);

        /// <summary>
        /// get other payment detail by member id
        /// </summary>
        /// <param name="MemberId"></param>
        /// <param name="Parlourid"></param>
        /// <returns>List other OtherPaymentModel</returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<OtherPaymentModel> OtherPaymentSelectByMemberId(int MemberId, Guid Parlourid);

        /// <summary>
        /// Get other payment information by id
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <param name="Parlourid"></param>
        /// <returns>OtherPaymentModel</returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        OtherPaymentModel GetOtherPayment(int InvoiceId, Guid Parlourid);
        #endregion

        #region UnderWriter
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<UnderwriterModel> SelectAllUnderwriterByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int SaveUnderwriter(UnderwriterModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        UnderwriterModel SelectUnderwriterBypkid(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int DeleteUnderwriterByID(int PkiUnderwriterId, string UserName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        UnderwriterModel SelectUnderwriterByName(string UnderwriterName, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<UnderwriterModel> SelectUnderwriterNotDeleted();
        #endregion
        #region UnderwriterPremiums

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<UnderwriterPremiumModel> GetAllUnderwriterPlansByParlourID(Guid ParlourID, int CustomType);

        #endregion
        #region "get status detail list"
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<StatusModel> GetStatus(string associatedTable);
        #endregion

        #region Member change status and change reason
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        void UpdateMemberStatus(Guid parlourId, int id, string status);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        void MemberStatusChangeReason(ChangeStatusReason model);
        #endregion

        #region "Underwrite setup"
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        UnderwriterSetupModel SelectUnderwriterSetupByName(string UnderwriterName, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int SaveUnderwriterSetup(UnderwriterSetupModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<UnderwriterSetupModel> SelectAllUnderwriterSetupByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        UnderwriterSetupModel EditUnderwriterSetupbyID(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteUnderwriterSetupByID(int ID, string UserName);


        #endregion

        #region "Underwrite Premium"

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<UnderwriterPremiumModel> SelectAllUnderwriterPremiumByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        UnderwriterPremiumModel EditUnderwriterPremiumbyID(int ID, Guid ParlourId);
        
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<UnderwriterSetupModel> GetUnderwriterSetupNameByParlourId(Guid parlourid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        UnderwriterPremiumModel SelectUnderwriterPremiumByName(int FkiUnderwriterId, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        int SaveUnderwriterPremium(UnderwriterPremiumModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteUnderwriterPremium(int ID,string UserName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<PlanModel> GetAllPlansList(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<RolePlayerModel> GetAllRolePlayer(Guid ParlourId);
        #endregion

        #region Tax Settings
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        List<TaxSetting> GetTaxSetting();
      
        //For Insert
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int InsertRecordForTax(TaxSetting model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        TaxSetting GetTaxSettingById(int id);

        
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteTaxSettingById(int id);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int UpdateRecordForTax(TaxSetting model);
        #endregion
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<MembersModel> GetPolicyByMemberIDNumber(string ID, Guid ParlourId);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]

        int SaveScheduleEmailReport(ScheduleEmailReportModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<ScheduleEmailReportModel> GetScheduleEmailReportByParlourId(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        ScheduleEmailReportModel GetScheduleById(int ID, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int UpdateScheduleByScheduleId(ScheduleEmailReportModel model);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteScheduleEmailReport(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        double SumPremiumByPlanId(int planId,int fkiMemberid, Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int DeleteMemberNote(int ID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        string GetCurrencyByParlourId(Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [FaultContract(typeof(FuneralServiceFault))]
        void DeleteTombstonePackage(int Id);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        ProgressStatus CheckProgressStatus(int ID,Guid ParlourId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
       
        List<VendorModel> GetVendorNameByParlourId(Guid parlourid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]

        void MemberRowImportToMember(string memberType, Guid importId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<PolicyModel> GetPlanSubscriptionByPlanIdNewMember(int pkiPlanID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        int GetLastCopiedMemberForDependency();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        List<FamilyDependencyModel> SelectMembersAndDependencies2(Guid ParlourId, bool MainMem, string Keyword);
    }
}
