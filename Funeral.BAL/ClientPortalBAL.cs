using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.BAL
{
    public class ClientPortalBAL
    {
        public static MembersViewModel GetAllMembers(string IdNumber, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder, string status)
        {
            try
            {
                DataSet ds = ClientPortalDAL.GetAllMembersdt(IdNumber, PageSize, PageNum, Keyword, SortBy, SortOrder, status);
                DataTable dr = ds.Tables[0];
                MembersViewModel objViewModel = new MembersViewModel();
                var membersList = FuneralHelper.DataTableMapToList<MembersModel>(dr, true);
                objViewModel.MemberList = membersList;
                //dr.NextResult();
                //dr.Read();
                objViewModel.TotalRecord = objViewModel.MemberList.Count;
                //dr.Close();
                //dr.Dispose();
                return objViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int SaveClientPayment(PayfastRequestModel payFastNotifyViewModel)
        {
            return ClientPortalDAL.SaveClientPayment(payFastNotifyViewModel);
        }
        public static MembersPaymentDetailsModel ReturnMemberPlanDetailsWithBalance(string strMemberNo, Guid pgParlourID)
        {
            DataTable dr = MemberPaymetsDAL.ReturnMemberPlanDetailsWithBalancedt(strMemberNo, pgParlourID);
            return FuneralHelper.DataTableMapToList<MembersPaymentDetailsModel>(dr).FirstOrDefault();
        }
    }


}
