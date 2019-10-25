using Funeral.Model;
using Funeral.DAL;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Funeral.BAL
{
    public class QuotationBAL
    {
        public QuotationBAL()
        { 
        
        }

        public static int SaveQuotation(QuotationModel model)
        {
            return QuotationDAL.SaveQuotation(model);
        }
        public static int UpdateQuotation(QuotationModel model)
        {
            return QuotationDAL.UpdateQuotation(model);
        }
        public static int QuotationDelete(int ID, string UserName)
        {
            return QuotationDAL.QuotationDelete(ID, UserName);
        }

        public static QuotationModel SelectQuotationByQuotationId(int ID, Guid ParlourId)
        {
            DataTable  dr = QuotationDAL.SelectQuotationByQuotationIddt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<QuotationModel>(dr).FirstOrDefault();
        }

        public static List<QuotationModel> SelectQuotationByQuotationId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable  dr = QuotationDAL.SelectAllByParlourIddt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
            return FuneralHelper.DataTableMapToList<QuotationModel>(dr);
        }

        public static List<QuotationModel> GetQuotationNumberByID(Guid ParlourId)
        {
            DataTable  dr = QuotationDAL.GetQuotationNumberByIDdt(ParlourId);
            return FuneralHelper.DataTableMapToList<QuotationModel>(dr);
        }
        #region QuoataionServices
        public static List<QuotationServicesModel> GetAllQuotationServices(Guid ParlourId)
        {
            DataTable  dr = QuotationDAL.GetAllQuotationServicesdt(ParlourId);
            return FuneralHelper.DataTableMapToList<QuotationServicesModel>(dr);
        }
        public static QuotationServicesModel GetServiceByID(int ID)
        { 
           DataTable  dr = QuotationDAL.GetServiceByIDdt(ID);
           return FuneralHelper.DataTableMapToList<QuotationServicesModel>(dr).FirstOrDefault();
        }
        public static int SaveService(QuotationServicesModel model)
        {
            return QuotationDAL.SaveService(model);
        }
        public static List<QuotationServicesModel> SelectServiceByQoutationID(int QuotationID)
        {
            DataTable  dr = QuotationDAL.SelectServiceByQoutationIDdt(QuotationID);
            return FuneralHelper.DataTableMapToList<QuotationServicesModel>(dr);
        }
        public static int DeleteServiceByID(int pkiQuotationSelectionID)
        {
            return QuotationDAL.DeleteServiceByID(pkiQuotationSelectionID);
        }
        public static QuotationModel GetQuotationNumberByID2(Guid ParlourId)
        {
            DataTable  dr = QuotationDAL.GetQuotationNumberByID2dt(ParlourId);
            return FuneralHelper.DataTableMapToList<QuotationModel>(dr).FirstOrDefault();
        }

        public static int UpdateAllData(int QuotationID, string Notes, string QuotationNumber)
        {
            return QuotationDAL.UpdateAllData(QuotationID, Notes, QuotationNumber);
        }

        public static int SaveDiscountAndAmount(QuotationDiscountModel model)
        {
            return QuotationDAL.SaveDiscountAndAmount(model);
        }
         public static QuotationDiscountModel GetAllQuotationDiscounts(int QuotationID)
        {
            DataTable  dr = QuotationDAL.GetAllQuotationDiscountsdt(QuotationID);
            return FuneralHelper.DataTableMapToList<QuotationDiscountModel>(dr).FirstOrDefault();
        }
         public static QuotationServicesModel SelectServiceByQouAndID(int QuotationID, int pkiQuotationSelectionID)
         {
             DataTable dr = QuotationDAL.SelectServiceByQouAndIDdt(QuotationID, pkiQuotationSelectionID);
             return FuneralHelper.DataTableMapToList<QuotationServicesModel>(dr).FirstOrDefault();
         }
         public static int SaveQuotationMessage(QuotationMessageModel model)
         {
             return QuotationDAL.SaveQuotationMessage(model);
         }
         public static QuotationMessageModel SelectQuotationMessageByID(int QuotationID)
         {
             DataTable  dr = QuotationDAL.SelectQuotationMessageByIDdt(QuotationID);
             return FuneralHelper.DataTableMapToList<QuotationMessageModel>(dr).FirstOrDefault();
         }
         public static int SaveQuotationTaxAndDiscount(int QuotationID, Decimal Tax, Decimal Discount)
         {
             return QuotationDAL.SaveQuotationTaxAndDiscount(QuotationID, Tax, Discount);
         }
         public static int QuotationStatus(int QuotationID, Guid parlourid, string status)
         {
             return QuotationDAL.QuotationStatus(QuotationID, parlourid, status);
         }

        #endregion
    }
}
