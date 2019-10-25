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
    public class FuneralPackageBAL
    {
        public static List<PackageServiceModel> SelectAllPackage(Guid ParlourId)
        {
            DataTable dr = FuneralPackageDAL.SelectAllPackagedt(ParlourId);
            return FuneralHelper.DataTableMapToList<PackageServiceModel>(dr);
        }
        public static List<PackageServicesSelectionModel> SelectPackageService(Guid ParlourId, string PackageName)
        {
            DataTable dr = FuneralPackageDAL.SelectPackageServicedt(ParlourId, PackageName);
            return FuneralHelper.DataTableMapToList<PackageServicesSelectionModel>(dr);
        }

        public static List<PackageServiceModel> SelectPackage(Guid ParlourId)
        {
            DataTable dr = FuneralPackageDAL.SelectPackagedt(ParlourId);
            return FuneralHelper.DataTableMapToList<PackageServiceModel>(dr);
        }

        public static int SavePackage(PackageServiceModel model)
        {
            return FuneralPackageDAL.SavePackage(model);
        }
        public static int SavePackageById(PackageServiceModel model)
        {
            return FuneralPackageDAL.SavePackageById(model);
        }

        public static int SavePackageService(PackageServiceModel model)
        {
            return FuneralPackageDAL.SavePackageService(model);
        }

        public static void DeletePackageService(int Id)
        {
            FuneralPackageDAL.DeletePackageService(Id);
        }

        public static void DeletePackageServiceByPackageAndService(int packageId, int serviceId)
        {
            FuneralPackageDAL.DeletePackageServiceByPackageAndService(packageId, serviceId);
        }

        public static List<PackageServicesSelectionModel> SelectPackageServiceByPackgeId(Guid ParlourId, int PackgeId)
        {
            DataTable dr = FuneralPackageDAL.SelectPackageServiceByPackgeIddt(ParlourId, PackgeId);
            return FuneralHelper.DataTableMapToList<PackageServicesSelectionModel>(dr);
        }

        public static List<FuneralServiceList> GetAllQuotationServicesdt(Guid ParlourId)
        {
            DataTable dr = FuneralPackageDAL.GetAllQuotationServicesdt(ParlourId);
            return FuneralHelper.DataTableMapToList<FuneralServiceList>(dr);
        }
        
        public static void DeletePackage(int Id, Guid parlourId)
        {
            FuneralPackageDAL.DeletePackage(Id, parlourId);
        }

        public static List<Service> SelectPackageServiceByPackgeAndService(Guid parlourId, int packageId)
        {
            DataTable dr = FuneralPackageDAL.SelectPackageServiceByPackgeAndServiceddt(parlourId, packageId);
            return FuneralHelper.DataTableMapToList<Service>(dr);
        }


    }
}
