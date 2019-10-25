using Funeral.DAL;
using Funeral.Model;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
 

namespace Funeral.BAL
{
    public class RightsBAL
    {
        public static List<RightsModel> tblRightGetAll()
        {
            DataTable dr = RightsDAL.tblRightGetAlldt();
            return FuneralHelper.DataTableMapToList<RightsModel>(dr).ToList();
        }
        public static int SavetblRight(RightsModel model)
        {
            return RightsDAL.SavetblRight(model);
        }
        public static RightsModel SelecttblRightById(int ID)
        {
            DataTable dr = RightsDAL.SelecttblRightByIddt(ID);
            return FuneralHelper.DataTableMapToList<RightsModel>(dr).FirstOrDefault();
        }
        public static List<NewRightsModel> GetRightsByGroupId(Guid ParlourId, int GroupId)
        {
            DataTable dr = RightsDAL.GetRightsByGroupIddt(ParlourId, GroupId);
            return FuneralHelper.DataTableMapToList<NewRightsModel>(dr).ToList();
        }
        public static List<tblPageModel> GetAllActiveTblPages()
        {
            DataTable dr = RightsDAL.GetAllActiveTblPagesdt();
            return FuneralHelper.DataTableMapToList<tblPageModel>(dr).ToList();
        }
        public static int SaveTblRights(NewRightsModel model)
        {
            return RightsDAL.SaveTblRights(model);
        }
        public static List<tblPageModel> LoadSideMenu(Guid ParlourId, int UserId)
        {
            DataTable dr = RightsDAL.LoadSideMenudt(ParlourId, UserId);
            return FuneralHelper.DataTableMapToList<tblPageModel>(dr).ToList();
        }

       
    }
}
