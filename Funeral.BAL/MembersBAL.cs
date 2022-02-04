using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Funeral.BAL
{
    public class MembersBAL
    {

        public static int SaveMembers(MembersModel model)
        {
            return MembersDAL.SaveMember(model);
        }
        public static int DeleteMember(int ID)
        {
            return MembersDAL.DeleteMember(ID);
        }
        public static int MemberDeleteLogical(int id, string UserName)
        {
            return MembersDAL.MemberDeleteLogical(id, UserName);
        }
        public static MembersModel GetMemberByID(int ID, Guid ParlourId)
        {
            DataTable dr = MembersDAL.GetMemberByIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<MembersModel>(dr).FirstOrDefault();

        }
        public static MembersModel GetMemberByIDNum(string ID, Guid ParlourId)
        {
            DataTable dr = MembersDAL.GetMemberByIDNumdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<MembersModel>(dr).FirstOrDefault();
        }

        public static MembersModel GetMemberByIDNumber(string ID, Guid ParlourId, int PlanId)
        {
            DataTable dr = MembersDAL.GetMemberByIDNumber(ID, ParlourId, PlanId);
            return FuneralHelper.DataTableMapToList<MembersModel>(dr).FirstOrDefault();
        }
        public static FamilyDependencyModel GetDependencByIDNum(string ID, Guid ParlourId, int MemberID)
        {
            DataTable dr = MembersDAL.GetDependencByIDNumdt(ID, ParlourId, MemberID);
            return FuneralHelper.DataTableMapToList<FamilyDependencyModel>(dr).FirstOrDefault();
        }

        public static MembersViewModel GetAllMembers(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, string status, string BookName)
        {
            try
            {
                DataSet ds = MembersDAL.GetAllMembersdt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder, status);
                DataTable dr = ds.Tables[0];
                MembersViewModel objViewModel = new MembersViewModel();
                var membersList = FuneralHelper.DataTableMapToList<MembersModel>(dr, true);
                objViewModel.MemberList = !string.IsNullOrEmpty(BookName) && BookName != "0" ? membersList.Where(x => x.MemberSociety.Equals(BookName)).ToList() : membersList;
                //dr.NextResult();
                //dr.Read();
                objViewModel.TotalRecord = objViewModel.MemberList.Count;
                //dr.Close();
                //dr.Dispose();
                return objViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<string> GetDistinctPolicyStatusList()
        {
            DataTable dr = MembersDAL.GetDistinctPolicyStatusListdt();
            return FuneralHelper.DataTableMapToList<string>(dr);
        }

        //public static List<FamilyDependencyModel> GetFamilyDependencyByMemberID(Guid parlourid, int MemberId)
        //{
        //    SqlDataReader dr = MembersDAL.GetFamilyDependencyByMemberID(parlourid, MemberId);
        //    return FuneralHelper.DataReaderMapToList<FamilyDependencyModel>(dr);
        //}

        //public static List<VehiclesModel> GetVehicleByMemberID(int fkiMemberId)
        //{
        //    SqlDataReader dr = MembersDAL.GetVehicleByMemberID(fkiMemberId);
        //    return FuneralHelper.DataReaderMapToList<VehiclesModel>(dr);
        //}

        public static List<MemberInvoiceModel> GetInvoicesByMemberID(Guid parlourid, int MemberId)
        {
            DataTable dr = MembersDAL.GetInvoicesByMemberIDdt(parlourid, MemberId);
            return FuneralHelper.DataTableMapToList<MemberInvoiceModel>(dr);
        }

        public static List<MemberInvoiceModel> GetGroupInvoicesByReference(Guid parlourid)
        {
            DataTable dr = MembersDAL.GetGroupInvoiceByReference(parlourid);
            return FuneralHelper.DataTableMapToList<MemberInvoiceModel>(dr);
        }


        //note save
        public static int NotesSaveMember(MemberNotesModel Notes)
        {
            return MembersDAL.NotesSaveMember(Notes);
        }

        public static List<MemberNotesModel> GetMemberNoteByMemberID(int MemberId)
        {
            DataTable dr = MembersDAL.MemberNotesByMemberIDdt(MemberId);
            return FuneralHelper.DataTableMapToList<MemberNotesModel>(dr);
        }


        //note edit
        public static SqlDataReader MemberNotesBypkiNoteID(int PkInoteID)
        {
            return MembersDAL.MemberNotesBypkiNoteID(PkInoteID);
        }

        //note edit
        public static DataTable MemberNotesBypkiNoteIDdt(int PkInoteID)
        {
            return MembersDAL.MemberNotesBypkiNoteIDdt(PkInoteID);
        }

        public static int UpdateNotesMember(MemberNotesModel Notes)
        {
            return Convert.ToInt32(MembersDAL.NoteUpdateMember(Notes));
        }
        //public static List<SupportedDocument> SelectSupportDocumentsByMemberId(int MemberId)
        //{
        //    return FuneralHelper.DataReaderMapToList<SupportedDocument>(MembersDAL.SelectSupportDocumentsByMemberId(MemberId));
        //}
        //public static int SaveSupportDocument(SupportedDocument model)
        //{
        //    return MembersDAL.SaveSupportDocument(model);
        //}


        //product

        public static List<AddonProductsModal> SelectProductName(Guid Parlourid)
        {
            DataTable dr = MembersDAL.selectProductNamedt(Parlourid);
            return FuneralHelper.DataTableMapToList<AddonProductsModal>(dr);
        }
        public static int SaveAddonProducts(MemberAddonProductsModel Model)
        {
            return MembersDAL.SaveAddonProducts(Model);
        }
        public static int NewSaveAddonProducts(MemberAddonProductsModel Model)
        {
            return MembersDAL.NewSaveAddonProducts(Model);
        }

        public static List<MemberAddonProductsModel> SelectMemberAddonProducts(int fkiMemberid)
        {
            DataTable dr = MembersDAL.GetAddonProductsdt(fkiMemberid);
            return FuneralHelper.DataTableMapToList<MemberAddonProductsModel>(dr);
        }
        public static List<MemberAddonProductsModel> GetAddonProductsList(int fkiMemberid)
        {
            DataTable dr = MembersDAL.GetAddonProductsList(fkiMemberid);
            return FuneralHelper.DataTableMapToList<MemberAddonProductsModel>(dr);
        }
        public static void MemberAddonProductsRemove(Guid pkiMemberProductID)
        {
            MembersDAL.DeleteAddonProduct(pkiMemberProductID);
        }
        public static void MemberAddonProductsRemove(Guid pkiMemberProductID, string ModifiedUser)
        {
            MembersDAL.DeleteAddonProduct(pkiMemberProductID, ModifiedUser);
        }
        public static List<AddonProductsModal> MemberListBindAddonProduct(Guid pkiProductID)
        {
            DataTable dr = MembersDAL.MemberListBinddt(pkiProductID);
            return FuneralHelper.DataTableMapToList<AddonProductsModal>(dr);
        }

        public static List<MemberAddonProductsModel> SelectMembarAddonProductBypkiMemberProductID(Guid PkInoteID)
        {
            //   return MembersDAL.SelectMembarAddonProductBypkiMemberProductID(PkInoteID);
            DataTable dr = MembersDAL.SelectMembarAddonProductBypkiMemberProductIDdt(PkInoteID);
            return FuneralHelper.DataTableMapToList<MemberAddonProductsModel>(dr);
        }

        public static List<AuditTrail> GetAuditList(int PkiMemberId, Guid ParlourId)
        {
            DataTable dr = MembersDAL.GetAuditList(PkiMemberId, ParlourId);

            return FuneralHelper.DataTableMapToList<AuditTrail>(dr);
        }

        public static int AddonProductUpdateMember(MemberAddonProductsModel AddonProduct)
        {
            return MembersDAL.AddonProductUpdateMember(AddonProduct);
        }
        public static int NewAddonProductUpdateMember(MemberAddonProductsModel AddonProduct)
        {
            return MembersDAL.NewAddonProductUpdateMember(AddonProduct);
        }

        /// <summary>
        /// SupportedDocumentModel
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public static List<SupportedDocumentModel> SelectSupportDocumentsByMemberId(int MemberId)
        {
            return FuneralHelper.DataTableMapToList<SupportedDocumentModel>(MembersDAL.SelectSupportDocumentsByMemberIddt(MemberId));
        }
        public static SupportedDocumentModel SelectSupportDocumentsById(int DocumentId)
        {
            return FuneralHelper.DataTableMapToList<SupportedDocumentModel>(MembersDAL.SelectSupportDocumentsByIddt(DocumentId)).FirstOrDefault();
        }
        public static int SaveSupportDocument(SupportedDocumentModel model)
        {
            return MembersDAL.SaveSupportDocument(model);
        }
        public static double SumOfPremium(int fkiMemberid, Guid ParlourId)
        {
            return MembersDAL.SumOfPremium(fkiMemberid, ParlourId);

        }

        public static List<CountryModel> GetCountry()
        {
            DataTable dr = MembersDAL.GetCountrydt();
            return FuneralHelper.DataTableMapToList<CountryModel>(dr);
        }

        #region FamilyDependency
        public static List<FamilyDependencyTypeModel> GetAllFamilyDependencyTypes()
        {
            return FuneralHelper.DataTableMapToList<FamilyDependencyTypeModel>(MembersDAL.GetFamilyDependencyTypesdt());
        }
        public static List<RelationShipModel> SelectRelationship()
        {
            return FuneralHelper.DataTableMapToList<RelationShipModel>(MembersDAL.SelectRelationshipdt());
        }

        public static List<FamilyDependencyModel> GetFamilyDependencyByMemberID(Guid parlourid, int MemberId)
        {
            DataTable dr = MembersDAL.GetFamilyDependencyByMemberIDdt(parlourid, MemberId);
            return FuneralHelper.DataTableMapToList<FamilyDependencyModel>(dr);
        }

        public static int SaveFamilyDependency(FamilyDependencyModel model)
        {
            return MembersDAL.SaveFamilyDependency(model);
        }
        public static int CheckDependencyCount(FamilyDependencyModel model)
        {
            var dataset = MembersDAL.CheckFamilyDependency(model);
            if (dataset.Tables[0].Rows.Count > 0)
                return Convert.ToInt32(dataset.Tables[0].Rows[0]["GetCount"].ToString());
            else
                return 0;
        }
        //public static int SaveVehicle(VehiclesModel model)
        //{
        //    return MembersDAL.SaveVehicle(model);
        //}

        public static int UpdateFamilyDependency(FamilyDependencyModel model)
        {
            return MembersDAL.UpdateFamilyDependency(model);
        }

        //public static int UpdateVehicle(VehiclesModel model)
        //{
        //    return MembersDAL.UpdateVehicle(model);
        //}

        public static FamilyDependencyModel SelectFamilyDependencyById(int pkiDependentID)
        {
            return FuneralHelper.DataTableMapToList<FamilyDependencyModel>(MembersDAL.SelectFamilyDependencyByIddt(pkiDependentID)).FirstOrDefault();
        }

        //public static VehiclesModel SelectVehicleByVehicleId(int pkiVehicleID)
        //{
        //    return FuneralHelper.DataReaderMapToList<VehiclesModel>(MembersDAL.SelectVehicleByVehicleID(pkiVehicleID)).FirstOrDefault();
        //}

        public static SqlDataReader GetFamilyDependencyTypes()
        {
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetFamilyDependecyTypes");
        }

        public static bool GetFamilyDependencyTypes(int dependencyId)
        {
            return MembersDAL.DeleteDependentById(dependencyId);
        }
        public static bool DeleteSUpportdocumentById(int DocumentId)
        {
            return MembersDAL.DeleteSUpportdocumentById(DocumentId);
        }

        public static bool GetFamilyDependencyTypes(int dependencyId, string ModifiedUser)
        {
            return MembersDAL.DeleteDependentById(dependencyId, ModifiedUser);
        }
        public static bool DeleteSUpportdocumentById(int DocumentId, string ModifiedUser)
        {
            return MembersDAL.DeleteSUpportdocumentById(DocumentId, ModifiedUser);
        }

        #endregion
        public static List<AgentModel> SelectAllAgent(Guid ParlourId)
        {
            DataTable dr = MembersDAL.SelectAllAgentdt(ParlourId);
            return FuneralHelper.DataTableMapToList<AgentModel>(dr);
        }
        public static NewMemberInvoiceModel GetInvoiceByid(int InvoiceId)
        {
            DataTable dr = MembersDAL.GetInvoiceByiddt(InvoiceId);
            return FuneralHelper.DataTableMapToList<NewMemberInvoiceModel>(dr).FirstOrDefault();
        }


        public static DataTable PolicyStatusPieChart(Guid parlourid)
        {
            var dataTable = MembersDAL.PolicyStatusPieChartdt(parlourid);
            return dataTable;
        }
        public static List<PolicyPremiumDashboardModel> SelectPolicyPremiumByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, bool isAdmin, bool isSuperUser, string UserName)
        {
            DataTable dr = MembersDAL.SelectPolicyPremiumByParlourIddt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder, isAdmin, isSuperUser, UserName);
            return FuneralHelper.DataTableMapToList<PolicyPremiumDashboardModel>(dr);
        }
        public static void UpdateMemberStatus(Guid parlourid, int id, string status)
        {
            MembersDAL.UpdateMemberStatus(parlourid, id, status);
        }
        public static void MemberStatusChangeReason(ChangeStatusReason model)
        {
            MembersDAL.MemberStatusChangeReason(model);
        }
        public static List<MembersModel> GetPolicyBy_FKMemberId(string ID, Guid ParlourId)
        {
            DataTable dr = MembersDAL.GetPolicyBy_FKMemberId(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<MembersModel>(dr);
        }
        public static List<MembersModel> GetPolicyByMemberIDNumber(string ID, Guid ParlourId)
        {
            DataTable dr = MembersDAL.GetPolicyByMemberIDNumberdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<MembersModel>(dr);
        }
        public static List<MembersModel> GetPolicyByMemberPassportNumber(string ID, Guid ParlourId)
        {
            DataTable dr = MembersDAL.GetPolicyByMemberPassportNumber(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<MembersModel>(dr);
        }
        public static double SumOfPremium(int planId, int fkiMemberid, Guid ParlourId)
        {
            return MembersDAL.SumOfPremium(planId, fkiMemberid, ParlourId);

        }
        public static int DeleteMemberNote(int ID)
        {
            return MembersDAL.DeleteMemberNote(ID);
        }
        public static void MemberRowImportToMember(string memberType, Guid importId)
        {
            try
            {
                MembersDAL.MemberRowImportToMember(memberType, importId);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void SaveMemberStaging(string memberType, Guid importId)
        {
            MembersDAL.SaveMemberStaging(memberType, importId);
        }
        public static int GetLastCopiedMemberForDependency()
        {
            return MembersDAL.GetLastCopiedMemberForDependency();
        }
        public static void UpdateMemberPolicyStatus(string policyStatus, int memberId)
        {
            MembersDAL.UpdateMemberPolicyStatus(policyStatus, memberId);
        }

        public static int SaveBeneficiary(Beneficiary_model Model)
        {
            return MembersDAL.SaveBeneficiary(Model);
        }
        public static double SumOfBeneficiaryPercentage(int fkiMemberid)
        {
            DataTable dr = MembersDAL.SearchBeneficiaryData(fkiMemberid);
            return Convert.ToDouble(FuneralHelper.DataTableMapToList<Beneficiary_model>(dr).Sum(x => x.Percentages));
        }
        public static List<Beneficiary_model> SearchBeneficiaryData(int fkiMemberid)
        {
            DataTable dr = MembersDAL.SearchBeneficiaryData(fkiMemberid);
            return FuneralHelper.DataTableMapToList<Beneficiary_model>(dr);
        }
        public static void DeleteBeneficiary(int pkiBeneficiaryID)
        {
            MembersDAL.DeleteBeneficiary(pkiBeneficiaryID);
        }
        public static void DeleteBeneficiary(int pkiBeneficiaryID, string ModifiedUser)
        {
            MembersDAL.DeleteBeneficiary(pkiBeneficiaryID, ModifiedUser);
        }
        public static List<FamilyDependencyModel> GetExtendedFamilyList(Guid parlourid, int MemberId)
        {
            DataTable dr = MembersDAL.GetExtendedFamilyList(parlourid, MemberId);
            return FuneralHelper.DataTableMapToList<FamilyDependencyModel>(dr);
        }

        public static void MakeReadyForImportMember(Guid importId)
        {
            MembersDAL.ReadyToImportMember(importId);
        }
        public static List<UnderwriterSetupModel> SelectAllUnderwritters(Guid parlourid)
        {
            DataTable dr = MembersDAL.GetUnderwriterList(parlourid);
            return FuneralHelper.DataTableMapToList<UnderwriterSetupModel>(dr);
        }

        public static PlanCreator GetPlanCreatorByID(int ID, Guid ParlourId, int UserId)
        {
            DataTable dr = MembersDAL.GetPlanCreatorByIDdt(ID, ParlourId, UserId);
            return FuneralHelper.DataTableMapToList<PlanCreator>(dr).FirstOrDefault();
        }

        public static PlanModel GetPlanByID(int ID, Guid ParlourId)
        {
            DataTable dr = MembersDAL.GetPlanByIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<PlanModel>(dr).FirstOrDefault();
        }
    }
}
