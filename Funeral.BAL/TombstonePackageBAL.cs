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
    public class TombstonePackageBAL
    {

        public static List<TombstonePackageModel> SelectAllPackage(Guid ParlourId)
        {
            DataTable dr = TombstonePackageDAL.SelectAllPackagedt(ParlourId);
            return FuneralHelper.DataTableMapToList<TombstonePackageModel>(dr);
        }

        public static int SavePackage(TombstonePackageModel model)
        {
            return TombstonePackageDAL.SavePackage(model);
        }

        public static int SavePackageService(TombstonePackageModel model)
        {
            return TombstonePackageDAL.SavePackageService(model);
        }

        public static void DeletePackageService(int Id)
        {
            TombstonePackageDAL.DeletePackageService(Id);
        }

        public static List<TombstonePackageModel> SelectPackageServiceByPackgeId(Guid ParlourId, int PackgeId)
        {
            DataTable  dr = TombstonePackageDAL.SelectPackageServiceByPackgeIddt(ParlourId, PackgeId);
            return FuneralHelper.DataTableMapToList<TombstonePackageModel>(dr);
        }

        public static void DeleteTombstonePackage(int Id)
        {
            TombstonePackageDAL.DeleteTombstonePackage(Id);
        }

    }
}
