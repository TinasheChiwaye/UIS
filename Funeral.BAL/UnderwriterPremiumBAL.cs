using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.BAL
{
  public  class UnderwriterPremiumBAL
    {
        public static List<UnderwriterPremiumModel> SelectAllUnderwriterPremiumByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable dr = UnderwriterPremiumDAL.SelectAllUnderwriterPremiumByParlourIddt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
            return FuneralHelper.DataTableMapToList<UnderwriterPremiumModel>(dr);
        }
        public static int SaveUnderwriterPremium(UnderwriterPremiumModel model)
        {
            return UnderwriterPremiumDAL.SaveUnderwriterPremium(model);
        }
        public static UnderwriterPremiumModel SelectUnderwriterPremiumBypkid(int ID, Guid ParlourId)
        {
            DataTable dr = UnderwriterPremiumDAL.SelectUnderwriterPremiumBypkiddt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<UnderwriterPremiumModel>(dr).FirstOrDefault();
        }
        //public static int DeleteUnderwriterPremiumByID(int PkiUnderwriterPremiumId, string UserName)
        //{
        //    return UnderwriterPremiumDAL.DeleteUnderwriterPremiumByID(PkiUnderwriterPremiumId, UserName);
        //}

        //public static int DeleteUnderwriterPremium(int PkiUnderwriterPremiumId, string UserName)
        //{
        //    return UnderwriterPremiumDAL.DeleteUnderwriterPremium(PkiUnderwriterPremiumId);
        //}
        public static UnderwriterPremiumModel SelectUnderwriterPremiumByName(int FkiUnderwriterId, Guid ParlourId)
        {
            DataTable dr = UnderwriterPremiumDAL.SelectUnderwriterPremiumByNamedt(FkiUnderwriterId, ParlourId);
            return FuneralHelper.DataTableMapToList<UnderwriterPremiumModel>(dr).FirstOrDefault();
        }
        public static List<UnderwriterPremiumModel> SelectUnderwriterPremiumNotDeleted()
        {
            DataTable dr = UnderwriterPremiumDAL.SelectUnderwriterPremiumNotDeleteddt();
            return FuneralHelper.DataTableMapToList<UnderwriterPremiumModel>(dr);
        }

        public static UnderwriterPremiumModel EditUnderwriterPremiumbyID(int ID, Guid ParlourId)
        {
            DataTable dr = UnderwriterPremiumDAL.EditUnderwriterPremiumbyIDdt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<UnderwriterPremiumModel>(dr).FirstOrDefault();
        }
        public static int DeleteUnderwriterPremium(int ID,string UserName)
        {
            return UnderwriterPremiumDAL.DeleteUnderwriterPremium(ID,UserName);
        }

        
        
    }
}
