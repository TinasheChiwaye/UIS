using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funeral.Model;
using System.Data.SqlClient;
using System.Data;
using Funeral.DAL;

namespace Funeral.BAL
{
    public class TombStoneBAL
    {
        public TombStoneBAL()
        { 
        }
        public static List<TombStoneModel> SelectAllTombStoneByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable  dr = TombStoneDAL.SelectAllTombStoneByParlourIddt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
            return FuneralHelper.DataTableMapToList<TombStoneModel>(dr);
        }
        public static int TombStoneDelete(int ID, string UserName)
        {
            return TombStoneDAL.TombStoneDelete(ID, UserName);
        }
        public static int SaveTombStone(TombStoneModel model)
        {
            return TombStoneDAL.SaveTombStone(model);
        }
        public static TombStoneModel SelectTombStoneByParlAndPki(int pkiTombstoneID, Guid ParlourId)
        {
            DataTable  dr = TombStoneDAL.SelectTombStoneByParlAndPkidt(pkiTombstoneID, ParlourId);
            return FuneralHelper.DataTableMapToList<TombStoneModel>(dr).FirstOrDefault();
        }
        public static int UploadTombImage(string ImageName, string ImagePath, int pkiTombstoneID, Guid parlourid)
        {
            return TombStoneDAL.UploadTombImage(ImageName, ImagePath, pkiTombstoneID, parlourid);
        }
        public static TombStoneModel GetInvoiceNumOfTombByParlID(Guid ParlourId) 
        {
            DataTable dr = TombStoneDAL.GetInvoiceNumOfTombByParlIDdt(ParlourId);
            return FuneralHelper.DataTableMapToList<TombStoneModel>(dr).FirstOrDefault();
        }
        public static List<TombStoneServiceSelectModel> SelectServiceByTombStoneID(int fkiTombstoneID)
        {
            DataTable  dr = TombStoneDAL.SelectServiceByTombStoneIDdt(fkiTombstoneID);
            return FuneralHelper.DataTableMapToList<TombStoneServiceSelectModel>(dr);
        }
        public static int DeleteTombstoneServiceByID(int pkiTombStoneSelectionID)
        {
            return TombStoneDAL.DeleteTombstoneServiceByID(pkiTombStoneSelectionID);
        }
        public static TombStoneServiceSelectModel SelectServiceByTombAndID(int fkiTombstoneID, int pkiTombStoneSelectionID)
        {
            DataTable  dr = TombStoneDAL.SelectServiceByTombAndIDdt(fkiTombstoneID, pkiTombStoneSelectionID);
            return FuneralHelper.DataTableMapToList<TombStoneServiceSelectModel>(dr).FirstOrDefault();
        }
        public static int SaveTombStoneService(TombStoneServiceSelectModel model)
        {
            return TombStoneDAL.SaveTombStoneService(model);
        }
        public static int UpdateAllTombStoneData(int pkiTombstoneID, Decimal DisCount, Decimal Tax, string InvoiceNumber)
        {
            return TombStoneDAL.UpdateAllTombStoneData(pkiTombstoneID, DisCount, Tax, InvoiceNumber);
        }

        public static TombStoneServiceSelectModel GetServiceByID(int ID)
        {
            DataTable dr = TombStoneDAL.GetServiceByIDdt(ID);
            return FuneralHelper.DataTableMapToList<TombStoneServiceSelectModel>(dr).FirstOrDefault();
        }

        #region TombStoneServices
        public static List<TombStoneServiceSelectModel> GetAllTombStoneServices(Guid ParlourId)
        {
            DataTable dr = TombStoneDAL.GetAllTombStoneServicesdt(ParlourId);
            return FuneralHelper.DataTableMapToList<TombStoneServiceSelectModel>(dr);
        }

        public static TombStoneModel SelectTombStoneByTombStoneId(int ID, Guid ParlourId)
        {
            DataTable dr = TombStoneDAL.SelectTombStoneByTombStoneIddt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<TombStoneModel>(dr).FirstOrDefault();
        }
        #endregion
    }
}
