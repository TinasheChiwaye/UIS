using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Funeral.BAL
{
    public class ScheduleEmailReportBAL
    {
        public static int SaveScheduleEmailReport(ScheduleEmailReportModel model)
        {
            return ScheduleEmailReportDAL.SaveScheduleEmailReport(model);
        }

        public static List<ScheduleEmailReportModel> GetScheduleEmailReportByParlourId(Guid ParlourId)
        {
            DataTable dr = ScheduleEmailReportDAL.GetScheduleEmailReportByParlourId(ParlourId);
            return FuneralHelper.DataTableMapToList<ScheduleEmailReportModel>(dr);
        }

        public static ScheduleEmailReportModel GetScheduleById(int ID, Guid ParlourId)
        {
            DataTable dr = ScheduleEmailReportDAL.GetScheduleById(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<ScheduleEmailReportModel>(dr).FirstOrDefault();

        }

        public static int UpdateScheduleByScheduleId(ScheduleEmailReportModel model)
        {
            return ScheduleEmailReportDAL.UpdateScheduleByScheduleId(model);
        }

        public static int DeleteScheduleEmailReport(int ID)
        {
            return ScheduleEmailReportDAL.DeleteScheduleEmailReport(ID);
           
        }

    }
}
