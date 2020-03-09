using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Funeral.BAL
{
    public class MemberPaymentBAL
    {
        public static MembersPaymentViewModel GetAllPayentMembers(Guid ParlourId, string PolicyNo, string IDNumber, int PageSize, int PageNum, string SortBy, string SortOrder, string PolicyStatus, string BookName = "")
        {
            DataSet ds = MemberPaymetsDAL.GetAllPayentMembersdt(ParlourId, PolicyNo, IDNumber, PageSize, PageNum, SortBy, SortOrder, PolicyStatus);
            DataTable dr = ds.Tables[0];
            MembersPaymentViewModel objViewModel = new MembersPaymentViewModel();
            var MemberList = FuneralHelper.DataTableMapToList<MembersPaymentModel>(dr, true);
            MemberList = !string.IsNullOrEmpty(BookName) ? MemberList.Where(x => x.ApplicationName.Contains(BookName)).ToList() : MemberList;
            objViewModel.MemberList = MemberList;
            objViewModel.TotalRecord = Convert.ToInt64(ds.Tables[1].Rows[0]["TotalRecord"].ToString());

            return objViewModel;
        }

        public static MembersPaymentDetailsModel GetMemberPaymnetDetailsByID(int ID)
        {
            DataTable dr = MemberPaymetsDAL.GetMemberPaymnetDetailsByIDdt(ID);
            return FuneralHelper.DataTableMapToList<MembersPaymentDetailsModel>(dr).FirstOrDefault();
        }

        public static MembersPaymentDetailsModel ReturnMemberPlanDetailsWithBalance(string strMemberNo, Guid pgParlourID)
        {
            DataTable dr = MemberPaymetsDAL.ReturnMemberPlanDetailsWithBalancedt(strMemberNo, pgParlourID);
            return FuneralHelper.DataTableMapToList<MembersPaymentDetailsModel>(dr).FirstOrDefault();
        }

        public static MembersPaymentDetailsModel ReturnMemberPlanDetailsWithBalance(string strMemberNo)
        {
            DataTable dr = MemberPaymetsDAL.ReturnMemberPlanDetailsWithBalancedt(strMemberNo);
            return FuneralHelper.DataTableMapToList<MembersPaymentDetailsModel>(dr).FirstOrDefault();
        }

        public static int AddPayments(MembersPaymentDetailsModel ModelPayment, bool IsJoiningFee)
        {
            return MemberPaymetsDAL.AddPayments(ModelPayment, IsJoiningFee);
        }

        public static int AddReversalPayments(int InvoiceId, string UserId, Guid Parlourid)
        {
            return MemberPaymetsDAL.AddReversalPayments(InvoiceId, UserId, Parlourid);
        }
        public static int InsertSendReminder(SendReminderModel ModelPayment)
        {
            return MemberPaymetsDAL.InsertSendReminder(ModelPayment);
        }
        public static int AddFuneralPayments(FuneralPaymentsModel model)
        {
            return MemberPaymetsDAL.AddFuneralPayments(model);
        }

        public static List<FuneralPaymentsModel> ReturnFuneralPayments(Guid parlourid, string pIntFnrlID)
        {
            DataTable dr = MemberPaymetsDAL.ReturnFuneralPaymentsdt(parlourid, pIntFnrlID);
            return FuneralHelper.DataTableMapToList<FuneralPaymentsModel>(dr);
        }
        public static List<FuneralPaymentsModel> ReturnMemberPayment(string intMemberID)
        {
            DataTable dr = MemberPaymetsDAL.ReturnMemberPaymentdt(intMemberID);
            return FuneralHelper.DataTableMapToList<FuneralPaymentsModel>(dr);
        }

        public static int GetPrintCounter(int invoiceId, Guid parlourId)
        {
            return MemberPaymetsDAL.GetPrintCounter(invoiceId, parlourId);
        }

        #region Check for joining fees
        public static JoiningFeeModel JoiningFees(int memberId, Guid parlourId)
        {
            DataTable dr = MemberPaymetsDAL.JoiningFeesdt(memberId, parlourId);
            return FuneralHelper.DataTableMapToList<JoiningFeeModel>(dr).FirstOrDefault();
        }
        #endregion
        public static string GetCurrencyByParlourId(Guid parlourId)
        {
            return MemberPaymetsDAL.GetCurrencyByParlourId(parlourId);
        }

        public static int RecreateBillingMemberPayments(RegenerateBillingModal ModelPayment)
        {
            return MemberPaymetsDAL.RecreateBillingMemberPayments(ModelPayment);
        }
    }
}
