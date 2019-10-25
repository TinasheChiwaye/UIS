using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.DAL
{
    public sealed class ScheduleEmailReportDAL
    {
        public static int SaveScheduleEmailReport(ScheduleEmailReportModel model)
        {
            string query = "SaveScheduleEmailReportDetails";
            DbParameter[] ObjParam = new DbParameter[9];
            ObjParam[0] = new DbParameter("@ReportType", DbParameter.DbType.VarChar, 0, model.ReportType);
            ObjParam[1] = new DbParameter("@Report", DbParameter.DbType.VarChar, 0, model.Report);
            ObjParam[2] = new DbParameter("@Frequency", DbParameter.DbType.VarChar, 0, model.Frequency);
            ObjParam[3] = new DbParameter("@DateFrom", DbParameter.DbType.DateTime, 0, model.DateFrom);
            ObjParam[4] = new DbParameter("@DateTo", DbParameter.DbType.DateTime, 0, model.DateTo);
            ObjParam[5] = new DbParameter("@Time", DbParameter.DbType.DateTime, 0, model.Time);
            ObjParam[6] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, model.ParlourId);
            ObjParam[7] = new DbParameter("@Email", DbParameter.DbType.NVarChar, 0, model.Email);
            ObjParam[8] = new DbParameter("@pkiScheduleId", DbParameter.DbType.Int, 0, model.pkiScheduleId);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }

        public static DataTable GetScheduleEmailReportByParlourId(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
             ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetScheduleEmailReportByParlourId", ObjParam) ;
        }

        public static DataTable GetScheduleById(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@pkiScheduleId", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetScheduleById", ObjParam);
        }

        public static int UpdateScheduleByScheduleId(ScheduleEmailReportModel model)
        {
            string query = "UpdateScheduleByScheduleId";
            DbParameter[] ObjParam = new DbParameter[9];
            ObjParam[0] = new DbParameter("@ReportType", DbParameter.DbType.VarChar, 0, model.ReportType);
            ObjParam[1] = new DbParameter("@Report", DbParameter.DbType.VarChar, 0, model.Report);
            ObjParam[2] = new DbParameter("@Frequency", DbParameter.DbType.VarChar, 0, model.Frequency);
            ObjParam[3] = new DbParameter("@DateFrom", DbParameter.DbType.DateTime, 0, model.DateFrom);
            ObjParam[4] = new DbParameter("@DateTo", DbParameter.DbType.DateTime, 0, model.DateTo);
            ObjParam[5] = new DbParameter("@Time", DbParameter.DbType.DateTime, 0, model.Time);
            ObjParam[6] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, model.ParlourId);
            ObjParam[7] = new DbParameter("@Email", DbParameter.DbType.NVarChar, 0, model.Email);
            ObjParam[8] = new DbParameter("@pkiScheduleId", DbParameter.DbType.Int, 0, model.pkiScheduleId);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }

        public static int DeleteScheduleEmailReport(int id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiScheduleId", DbParameter.DbType.Int, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteScheduleEmailReport", ObjParam));
        }


    }
}
