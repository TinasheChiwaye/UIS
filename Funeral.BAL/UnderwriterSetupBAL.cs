using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funeral.DAL;

namespace Funeral.BAL
{
  public  class UnderwriterSetupBAL
    {
        public static List<UnderwriterSetupModel> SelectAllUnderwriterSetupByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable dr = UnderwriterSetupDAL.SelectAllUnderwriterSetupByParlourIddt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
            return FuneralHelper.DataTableMapToList<UnderwriterSetupModel>(dr);
          
        }
        public static int SaveUnderwriterSetup(UnderwriterSetupModel model)
        {
            return UnderwriterSetupDAL.SaveUnderwriterSetup(model);
        }
        //public static UnderwriterModel SelectUnderwriterBypkid(int ID, Guid ParlourId)
        //{
        //    DataTable dr = UnderwriterDAL.SelectUnderwriterBypkiddt(ID, ParlourId);
        //    return FuneralHelper.DataTableMapToList<UnderwriterModel>(dr).FirstOrDefault();
        //}
        public static int DeleteUnderwriterSetupByID(int PkiUnderWriterSetupId, string UserName)
        {
            return UnderwriterSetupDAL.DeleteUnderwriterSetupByID(PkiUnderWriterSetupId, UserName);
        }
        public static UnderwriterSetupModel SelectUnderwriterSetupByName(string UnderwriterName, Guid ParlourId)
        {
            DataTable dr = UnderwriterSetupDAL.SelectUnderwriterSetupByNamedt(UnderwriterName, ParlourId);
            return FuneralHelper.DataTableMapToList<UnderwriterSetupModel>(dr).FirstOrDefault();
        }
        public static List<UnderwriterSetupModel> SelectUnderwriterSetupNotDeleted()
        {
            DataTable dr = UnderwriterSetupDAL.SelectUnderwriterSetupNotDeleteddt();
            return FuneralHelper.DataTableMapToList<UnderwriterSetupModel>(dr);
        }

        public static UnderwriterSetupModel EditUnderwriterSetupbyID(int ID, Guid ParlourId)
        {
            DataTable dr = UnderwriterSetupDAL.EditUnderwriterSetupbyIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<UnderwriterSetupModel>(dr).FirstOrDefault();
        }

    }
}
