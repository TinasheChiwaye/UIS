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
    public class TombStonesPaymentBAL
    {
        public static TombStonesPaymentModel TombStonesPaymentSelect(Guid parlourid, int invoiceId)
        {
            DataTable dr = TombStonesPaymentDAL.TombStonesPaymentSelectdt(parlourid, invoiceId);
            return FuneralHelper.DataTableMapToList<TombStonesPaymentModel>(dr).FirstOrDefault();
        }

        public static List<TombStonesPaymentModel> TombStonesPaymentSelectByTombstoneID(Guid parlourid, int tombstoneId)
        {
            DataTable dr = TombStonesPaymentDAL.TombStonesPaymentSelectByTombstoneIDdt(parlourid, tombstoneId);
            return FuneralHelper.DataTableMapToList<TombStonesPaymentModel>(dr);
        }

        public static int AddInvoice(TombStonesPaymentModel model)
        {
            return TombStonesPaymentDAL.AddTombStonesPayment(model);
        }
    }
}
