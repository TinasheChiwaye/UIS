using Funeral.DAL;
using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Funeral.BAL
{
    public class OtherPaymentBAL
    {
        public static int OtherPaymentDetailsSave(OtherPaymentModel model)
        {
            return OtherPaymentDAl.OthePaymentDetailsSave(model);
        }

        public static List<OtherPaymentModel> OtherPaymentSelectByMemberId(int MemberId, Guid Parlourid)
        {
            DataTable dr = OtherPaymentDAl.OtherPaymentSelectByMemberIddt(MemberId, Parlourid);
            return FuneralHelper.DataTableMapToList<OtherPaymentModel>(dr).ToList();
        }

        public static OtherPaymentModel OtherPaymentSelect(int InvoiceId, Guid Parlourid)
        {
            DataTable dr = OtherPaymentDAl.OtherPaymentSelectdt(InvoiceId, Parlourid);
            return FuneralHelper.DataTableMapToList<OtherPaymentModel>(dr)[0];
        }
        public static int AddEditGroupPayment(GroupPayment model)
        {
            return OtherPaymentDAl.AddEditGroupPayment(model);
        }
        public static List<SocietyModel> GetAllSocietyes(Guid ParlourId)
        {
            DataTable dr = ToolsSetingDAL.GetAllSocietyesdt(ParlourId);
            return FuneralHelper.DataTableMapToList<SocietyModel>(dr);
        }
        public static List<GroupPayment> GetAllGroupPaymentList(Guid ParlourId, int GroupId)
        {
            DataTable dr = OtherPaymentDAl.GetAllGroupPaymentList(ParlourId, GroupId);
            return FuneralHelper.DataTableMapToList<GroupPayment>(dr);
        }
        public static GroupPayment EditGroupPaymentByID(int ID, Guid ParlourId)
        {
            DataTable dr = OtherPaymentDAl.EditGroupPaymentByID(ID, ParlourId);
            return FuneralHelper.DataTableMapToList<GroupPayment>(dr).FirstOrDefault();
        }
        public static int DeleteGroupPayment(int ID)
        {
            return OtherPaymentDAl.DeleteGroupPayment(ID);
        }
    }
}
