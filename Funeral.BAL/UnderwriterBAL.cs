using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.BAL
{
    public class UnderwriterBAL
    {
        public static List<UnderwriterModel> SelectAllUnderwriterByParlourId(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DataTable dr = UnderwriterDAL.SelectAllUnderwriterByParlourIddt(ParlourId, PageSize, PageNum, Keyword, SortBy, SortOrder);
            return FuneralHelper.DataTableMapToList<UnderwriterModel>(dr);
        }
        public static int SaveUnderwriter(UnderwriterModel model)
        {
            return UnderwriterDAL.SaveUnderwriter(model);
        }
        public static UnderwriterModel SelectUnderwriterBypkid(int ID, Guid ParlourId)
        {
            DataTable dr = UnderwriterDAL.SelectUnderwriterBypkiddt(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<UnderwriterModel>(dr).FirstOrDefault();
        }
        public static int DeleteUnderwriterByID(int PkiUnderwriterId,string UserName)
        {
            return UnderwriterDAL.DeleteUnderwriterByID(PkiUnderwriterId, UserName);
        }
        public static UnderwriterModel SelectUnderwriterByName(string UnderwriterName, Guid ParlourId)
        {
            DataTable  dr = UnderwriterDAL.SelectUnderwriterByNamedt(UnderwriterName, ParlourId);
            return FuneralHelper.DataTableMapToList<UnderwriterModel>(dr).FirstOrDefault();
        }
        public static List<UnderwriterModel> SelectUnderwriterNotDeleted()
        {
            DataTable dr = UnderwriterDAL.SelectUnderwriterNotDeleteddt();
            return FuneralHelper.DataTableMapToList<UnderwriterModel>(dr);
        }
    }
}
