using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.DAL
{
    public class ClientPortalDAL
    {
        public static DataSet GetAllMembersdt(string IDNumber, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, string status)
        {
            DbParameter[] ObjParam = new DbParameter[7];
            try
            {
                ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
                ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
                ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, (Keyword == null) ? string.Empty : Keyword);
                ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
                ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
                ObjParam[5] = new DbParameter("@IDNumber", DbParameter.DbType.NVarChar, 0, IDNumber);
                ObjParam[6] = new DbParameter("@Status", DbParameter.DbType.VarChar, 0, status);
                return DbConnection.GetDataSet(CommandType.StoredProcedure, "getMyPoliceByIDnumber", ObjParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int SaveClientPayment(PayfastRequestModel model)
        {
            string query = "SaveClientPayment";

            DbParameter[] ObjParam = new DbParameter[12];
            ObjParam[0] = new DbParameter("@merchant_id", DbParameter.DbType.NVarChar, 0, model.merchant_id);
            ObjParam[1] = new DbParameter("@amount_fee", DbParameter.DbType.Decimal, 0, model.amount_fee);
            ObjParam[2] = new DbParameter("@amount_gross", DbParameter.DbType.Decimal, 0, model.amount_gross);
            ObjParam[3] = new DbParameter("@amount_net", DbParameter.DbType.Decimal, 0, model.amount_net);
            ObjParam[4] = new DbParameter("@email_address", DbParameter.DbType.NVarChar, 0, model.email_address);
            ObjParam[5] = new DbParameter("@item_name", DbParameter.DbType.NVarChar, 0, model.item_name);
            ObjParam[6] = new DbParameter("@item_description", DbParameter.DbType.NVarChar, 0, model.item_description);
            ObjParam[7] = new DbParameter("@m_payment_id", DbParameter.DbType.NVarChar, 0, model.m_payment_id);
            ObjParam[8] = new DbParameter("@payment_status", DbParameter.DbType.NVarChar, 0, model.payment_status);
            ObjParam[9] = new DbParameter("@token", DbParameter.DbType.NVarChar, 0, model.token);
            ObjParam[10] = new DbParameter("@MemberId", DbParameter.DbType.Int, 0, model.custom_int1);
            ObjParam[11] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0,Guid.Parse(model.custom_str1));
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static DataTable ReturnMemberPlanDetailsWithBalancedt(string strMemberNo, Guid pgParlourID)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@MemberNo", DbParameter.DbType.NVarChar, 0, strMemberNo);
            ObjParam[1] = new DbParameter("@ParlourID", DbParameter.DbType.UniqueIdentifier, 0, pgParlourID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberParlourIDAndMemberNoByID_New", ObjParam);//[MemberParlourIDAndMemberNoByID]
        }
    }
}
