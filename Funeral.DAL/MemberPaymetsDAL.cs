using Funeral.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Funeral.DAL
{
    public class MemberPaymetsDAL
    {
        //public static SqlDataReader GetAllPayentMembers(Guid ParlourId)
        //{
        //    DbParameter[] ObjParam = new DbParameter[7];

        //    ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
        //    ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
        //    ObjParam[2] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
        //    ObjParam[3] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
        //    ObjParam[4] = new DbParameter("@PolicyNo", DbParameter.DbType.NVarChar, 0, PolicyNo);
        //    ObjParam[5] = new DbParameter("@IDNumber", DbParameter.DbType.NVarChar, 0, IDNumber);
        //    ObjParam[6] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
        //    return DbConnection.GetDataReader(CommandType.StoredProcedure, "MemberPaymentSelectAllByPage", ObjParam);
        //}
        public static SqlDataReader GetAllPayentMembers(Guid ParlourId, string PolicyNo, string IDNumber, int PageSize, int PageNum, string SortBy, string SortOrder, string PolicyStatus)
        {
            DbParameter[] ObjParam = new DbParameter[8];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[3] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[4] = new DbParameter("@PolicyNo", DbParameter.DbType.NVarChar, 0, PolicyNo);
            ObjParam[5] = new DbParameter("@IDNumber", DbParameter.DbType.NVarChar, 0, IDNumber);
            ObjParam[6] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[7] = new DbParameter("@PolicyStatus", DbParameter.DbType.NVarChar, 0, PolicyStatus);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "MemberPaymentSelectAllByPage", ObjParam);
        }

        public static DataSet GetAllPayentMembersdt(Guid ParlourId, string PolicyNo, string IDNumber, int PageSize, int PageNum, string SortBy, string SortOrder, string PolicyStatus)
        {
            DbParameter[] ObjParam = new DbParameter[8];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[3] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[4] = new DbParameter("@PolicyNo", DbParameter.DbType.NVarChar, 0, PolicyNo);
            ObjParam[5] = new DbParameter("@IDNumber", DbParameter.DbType.NVarChar, 0, IDNumber);
            ObjParam[6] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[7] = new DbParameter("@PolicyStatus", DbParameter.DbType.NVarChar, 0, PolicyStatus);
            if (ParlourId == Guid.Empty)
                return DbConnection.GetDataSet(CommandType.StoredProcedure, "MemberPaymentSelectAll_WithouthParlour", ObjParam);
            else
                return DbConnection.GetDataSet(CommandType.StoredProcedure, "MemberPaymentSelectAllByPage", ObjParam);
        }


        public static SqlDataReader GetMemberPlanDetails(int pkiPlanID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, pkiPlanID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetMemberPlanDetailsByPlanId", ObjParam);
        }

        public static DataTable GetMemberPlanDetailsdt(int pkiPlanID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, pkiPlanID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetMemberPlanDetailsByPlanId", ObjParam);
        }

        public static SqlDataReader GetMemberPaymnetDetailsByID(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "MemberPaymentSelect", ObjParam);
        }

        public static DataTable GetMemberPaymnetDetailsByIDdt(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberPaymentSelect", ObjParam);
        }

        public static SqlDataReader ReturnMemberPlanDetailsWithBalance(string strMemberNo, Guid pgParlourID)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@MemberNo", DbParameter.DbType.NVarChar, 0, strMemberNo);
            ObjParam[1] = new DbParameter("@ParlourID", DbParameter.DbType.UniqueIdentifier, 0, pgParlourID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "MemberParlourIDAndMemberNoByID", ObjParam);
        }

        public static DataTable ReturnMemberPlanDetailsWithBalancedt(string strMemberNo, Guid pgParlourID)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@MemberNo", DbParameter.DbType.NVarChar, 0, strMemberNo);
            ObjParam[1] = new DbParameter("@ParlourID", DbParameter.DbType.UniqueIdentifier, 0, pgParlourID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberParlourIDAndMemberNoByID_New", ObjParam);//[MemberParlourIDAndMemberNoByID_New]
        }

        public static SqlDataReader ReturnMemberPlanDetailsWithBalance(string strMemberNo)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@MemberNo", DbParameter.DbType.NVarChar, 0, strMemberNo);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "MemberPaymentDetailsByMemberNo", ObjParam);
        }

        public static DataTable ReturnMemberPlanDetailsWithBalancedt(string strMemberNo)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@MemberNo", DbParameter.DbType.NVarChar, 0, strMemberNo);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "MemberPaymentDetailsByMemberNo", ObjParam);
        }

        public static DataSet GetInvoiceNumberByParlourID(Guid pgParlourID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourID", DbParameter.DbType.UniqueIdentifier, 0, pgParlourID);
            return DbConnection.GetDataSet(CommandType.StoredProcedure, "GetInvoiceNumberByParlourID", ObjParam);
        }

        public static DataTable SearchMemberIDByMemberNo(string MemeberNumber, string strUserName, Guid pgParlourID)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@MemberNo", DbParameter.DbType.NVarChar, 0, MemeberNumber);
            ObjParam[1] = new DbParameter("@ParlourID", DbParameter.DbType.UniqueIdentifier, 0, pgParlourID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SearchMemberIDByMemberNo", ObjParam);

        }

        public static int AddPayments(MembersPaymentDetailsModel ModelPayment, bool IsJoiningFee)
        {
            int returnValue = 0;
            int invNumber = 0;
            DataSet dsData = GetInvoiceNumberByParlourID(ModelPayment.ParlourId);
            if (dsData.Tables[0].Rows.Count > 0)
            {
                invNumber = Convert.ToInt32(dsData.Tables[0].Rows[0][0]);
            }
            else
            {
                returnValue = 0;
                return returnValue;
            }
            DataTable dtMember = SearchMemberIDByMemberNo(ModelPayment.MemeberNumber, "", ModelPayment.ParlourId);
            if (dtMember.Rows.Count > 0 && ModelPayment.pkiMemberID != 0)
            {

                DbParameter[] ObjParam = new DbParameter[13];
                ObjParam[0] = new DbParameter("@MemberID", DbParameter.DbType.Int, 0, ModelPayment.pkiMemberID);
                //ObjParam[1] = new DbParameter("@AmountPaid", DbParameter.DbType.Decimal, 0, (ModelPayment.Amount / 100).ToString().Replace(",", "."));
                ObjParam[1] = new DbParameter("@AmountPaid", DbParameter.DbType.Decimal, 0, ModelPayment.Amount);
                ObjParam[2] = new DbParameter("@ReceivedBy", DbParameter.DbType.VarChar, 0, ModelPayment.ReceivedBy);
                ObjParam[3] = new DbParameter("@Notes", DbParameter.DbType.VarChar, 0, ModelPayment.Notes);
                ObjParam[4] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ModelPayment.ParlourId);
                ObjParam[5] = new DbParameter("@PaymentBranch", DbParameter.DbType.VarChar, 0, ModelPayment.Branch);
                ObjParam[6] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, ModelPayment.UserName);
                ObjParam[7] = new DbParameter("@PaidUntil", DbParameter.DbType.DateTime, 0, ModelPayment.NextPaymentDate.ToString("yyyy-MM-dd"));
                ObjParam[8] = new DbParameter("@InvNumber", DbParameter.DbType.Int, 0, invNumber);
                if (DateTime.Now.ToString("yyyy-MM-dd") == ModelPayment.PaymentDate.ToString("yyyy-MM-dd"))
                {
                    ObjParam[9] = new DbParameter("@DatePaid", DbParameter.DbType.DateTime, 0, DateTime.Now);
                }
                else
                {
                    ObjParam[9] = new DbParameter("@DatePaid", DbParameter.DbType.DateTime, 0, ModelPayment.PaymentDate.ToString("yyyy-MM-dd"));
                }
                ObjParam[10] = new DbParameter("@PaidBy", DbParameter.DbType.VarChar, 0, "");
                ObjParam[11] = new DbParameter("@MethodOfPayment", DbParameter.DbType.VarChar, 0, ModelPayment.MethodOfPayment);
                ObjParam[12] = new DbParameter("@IsJoiningFee", DbParameter.DbType.Bit, 0, IsJoiningFee);
                returnValue = Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "AddPayment", ObjParam));

                if (ModelPayment.LatePaymentPenalty > 0)
                {
                    DbParameter[] ObjLatePaymentParam = new DbParameter[3];
                    ObjLatePaymentParam[0] = new DbParameter("@pkiMemberid", DbParameter.DbType.Int, 0, ModelPayment.pkiMemberID);
                    ObjLatePaymentParam[1] = new DbParameter("@Amount", DbParameter.DbType.Decimal, 0, (ModelPayment.LatePaymentPenalty / 100).ToString().Replace(",", "."));
                    ObjLatePaymentParam[2] = new DbParameter("@Notes", DbParameter.DbType.VarChar, 0, "Penalty Payment :- RecievedBy" + ModelPayment.ReceivedBy);

                    returnValue = Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "MemberLatePaymentPenaltyPayment", ObjLatePaymentParam));
                }

                string PolicyStatus = string.Empty;
                if (ModelPayment.Notes.Contains("Reinstate Policy"))
                {
                    PolicyStatus = "Active";
                }
                else if (ModelPayment.Notes.Contains("Rejoin Policy"))
                {
                    PolicyStatus = "On Trial";
                }
                else
                {
                    PolicyStatus = "";
                }
                DbParameter[] ObjNotesParams = new DbParameter[2];
                ObjNotesParams[0] = new DbParameter("@pkiMemberid", DbParameter.DbType.Int, 0, ModelPayment.pkiMemberID);
                ObjNotesParams[1] = new DbParameter("@PolicyStatus", DbParameter.DbType.VarChar, 0, PolicyStatus);
                Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "UpdateMemberPolicy", ObjNotesParams));
                AddAudit(ModelPayment.ReceivedBy, ModelPayment.ParlourId, "AddPayments  MemberNumber=('" + ModelPayment.MemeberNumber + "')");
            }
            return returnValue;
        }

        public static int AddReversalPayments(int InvoiceId, string UserId, Guid Parlourid)
        {

            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@invoiceid", DbParameter.DbType.Int, 0, InvoiceId);
            ObjParam[1] = new DbParameter("@UserID", DbParameter.DbType.NVarChar, 0, UserId);
            ObjParam[2] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            int newInvoiceId = Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "InvoiceReversalPayment", ObjParam));
            AddAudit(UserId, Parlourid, "Reversal  MemberNumber=('" + InvoiceId + "')");
            return newInvoiceId;

        }

        public static int AddFuneralPayments(FuneralPaymentsModel model)
        {
            decimal amountPaid = model.AmountPaid.ToString().Contains(",") == true ? Convert.ToDecimal((model.AmountPaid / 100).ToString().Replace(",", ".")) : model.AmountPaid;
            DbParameter[] ObjParam = new DbParameter[7];
            ObjParam[0] = new DbParameter("@FuneralID", DbParameter.DbType.Int, 0, model.FuneralID);//pIntFnrlID
            ObjParam[1] = new DbParameter("@AmountPaid", DbParameter.DbType.Decimal, 0, amountPaid);
            ObjParam[2] = new DbParameter("@RecievedBy", DbParameter.DbType.VarChar, 0, model.RecievedBy);
            ObjParam[3] = new DbParameter("@PaidBy", DbParameter.DbType.VarChar, 0, model.PaidBy);
            ObjParam[4] = new DbParameter("@Notes", DbParameter.DbType.VarChar, 0, model.Notes);
            ObjParam[5] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.ParlourId);
            ObjParam[6] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, model.UserName);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "AddFuneralPayments", ObjParam));

        }

        public static SqlDataReader ReturnFuneralPayments(Guid parlourid, string pIntFnrlID)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@FuneralID", DbParameter.DbType.VarChar, 0, pIntFnrlID);//pIntFnrlID
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);//pIntFnrlID
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "ReturnFuneralPayments", ObjParam);
        }

        public static DataTable ReturnFuneralPaymentsdt(Guid parlourid, string pIntFnrlID)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@FuneralID", DbParameter.DbType.VarChar, 0, pIntFnrlID);//pIntFnrlID
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);//pIntFnrlID
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "ReturnFuneralPayments", ObjParam);
        }

        public static SqlDataReader ReturnMemberPayment(string intMemberID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@MemberID", DbParameter.DbType.VarChar, 0, intMemberID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "ReturnMemberPayment", ObjParam);
        }

        public static DataTable ReturnMemberPaymentdt(string intMemberID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@MemberID", DbParameter.DbType.VarChar, 0, intMemberID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "ReturnMemberPayment", ObjParam);
        }

        //public bool CheckUserAccess(string strSecurityGroupName, string strUserName)
        //{
        //    bool blnPassed = false;
        //    if (strUserName == "administrator")
        //    {
        //        blnPassed = true;
        //        return blnPassed;
        //    }

        //    DataRow[] result = pdtUserAccess.Select("sSecureGroupName = '" + strSecurityGroupName + "'");
        //    foreach (DataRow row in result)
        //    {
        //        blnPassed = true;

        //    }
        //    return blnPassed;
        //}


        public static int AddAudit(string strUserName, Guid pgParlourID, string actionDesc)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@strUserName", DbParameter.DbType.VarChar, 0, strUserName);
            ObjParam[1] = new DbParameter("@pgParlourID", DbParameter.DbType.UniqueIdentifier, 0, pgParlourID);
            ObjParam[2] = new DbParameter("@actionDesc", DbParameter.DbType.VarChar, 0, actionDesc);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "AddAudit", ObjParam));

        }

        public static int InsertSendReminder(SendReminderModel ModelPayment)
        {
            try
            {
                DbParameter[] ObjParam = new DbParameter[4];
                ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ModelPayment.parlourid);
                ObjParam[1] = new DbParameter("@UserID", DbParameter.DbType.VarChar, 0, ModelPayment.MemeberID);
                ObjParam[2] = new DbParameter("@Data", DbParameter.DbType.VarChar, 0, ModelPayment.MemberData);
                ObjParam[3] = new DbParameter("@ToNumber", DbParameter.DbType.VarChar, 0, ModelPayment.MemeberToNumber);
                DbConnection.GetScalarValue(CommandType.StoredProcedure, "SendSMS", ObjParam);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public static int SendingBulkSms(SendReminderModel ModelPayment)
        {
            try
            {
                DbParameter[] ObjParam = new DbParameter[3];
                ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ModelPayment.parlourid);
                ObjParam[1] = new DbParameter("@UserID", DbParameter.DbType.VarChar, 0, ModelPayment.MemeberID);
                ObjParam[2] = new DbParameter("@Data", DbParameter.DbType.VarChar, 0, ModelPayment.MemberData);

                DbConnection.GetScalarValue(CommandType.StoredProcedure, "SendBulkSMS", ObjParam);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public static void AddUpdateReceiptPrintCounter(int invoiceId, Guid parlourId)
        {
            try
            {
                DbParameter[] ObjParam = new DbParameter[2];
                ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourId);
                ObjParam[1] = new DbParameter("@InvoiceId", DbParameter.DbType.Int, 0, invoiceId);

                DbConnection.GetScalarValue(CommandType.StoredProcedure, "InvoicePrintCounterSave", ObjParam);

            }
            catch
            {

            }

        }

        public static int GetPrintCounter(int invoiceId, Guid parlourId)
        {
            try
            {
                int printcounter = 0;
                DbParameter[] ObjParam = new DbParameter[2];
                ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourId);
                ObjParam[1] = new DbParameter("@InvoiceId", DbParameter.DbType.Int, 0, invoiceId);
                DataTable dr = DbConnection.GetDataTable(CommandType.Text, "Select ISNULL(PrintCounter,0) AS PrintCounter From InvoicePrintCounter WHERE ParlourId=@ParlourId and InvoiceId=@InvoiceId", ObjParam);
                if (dr.Rows.Count > 0)
                {

                    printcounter = int.Parse(dr.Rows[0][0].ToString());
                }
                AddUpdateReceiptPrintCounter(invoiceId, parlourId);
                return printcounter;
            }
            catch
            {
                return 0;
            }

        }

        #region Check for joining fee
        public static SqlDataReader JoiningFees(int memberId, Guid parlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@MemberId", DbParameter.DbType.Int, 0, memberId);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "JoiningFeeSelectByMemberId", ObjParam);
        }
        public static DataTable JoiningFeesdt(int memberId, Guid parlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@MemberId", DbParameter.DbType.Int, 0, memberId);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "JoiningFeeSelectByMemberId", ObjParam);
        }
        #endregion
        public static string GetCurrencyByParlourId(Guid parlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourId);
            return DbConnection.GetScalarValue(CommandType.StoredProcedure, "GetCurrencyByParlourId", ObjParam).ToString();
        }
        public static int RecreateBillingMemberPayments(RegenerateBillingModal ModelPayment)
        {
            try
            {
                DbParameter[] ObjParam = new DbParameter[3];
                ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ModelPayment.parlourId);
                ObjParam[1] = new DbParameter("@Date", DbParameter.DbType.DateTime, 0, ModelPayment.PremiumDueDate);
                ObjParam[2] = new DbParameter("@ReferenceNumber", DbParameter.DbType.NVarChar, 0, ModelPayment.ReferenceNumber);
                DbConnection.GetScalarValue(CommandType.StoredProcedure, "sp_RecreateBillingMemberPayments", ObjParam);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}