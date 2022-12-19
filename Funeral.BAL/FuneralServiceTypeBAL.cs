using Funeral.DAL;
using Funeral.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Funeral.BAL
{
    public class FuneralServiceTypeBAL
    {
        public static int Save(FuneralServiceTypeVm model)
        {
            return FuneralServiceTypeDAL.Save(model);
        }
        public static List<FuneralServiceTypeVm> SelectAll()
        {
            DataTable dr = FuneralServiceTypeDAL.SelectAll();
            return FuneralHelper.DataTableMapToList<FuneralServiceTypeVm>(dr);
        }

        public static FuneralServiceTypeVm Select(int Id)
        {
            DataTable dt = FuneralServiceTypeDAL.SelectById(Id);
            return FuneralHelper.DataTableMapToList<FuneralServiceTypeVm>(dt).FirstOrDefault();
        }

    }
}
