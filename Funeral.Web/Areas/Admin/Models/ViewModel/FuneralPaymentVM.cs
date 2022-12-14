using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funeral.Web.Areas.Admin.Models.ViewModel
{
    public class FuneralPaymentVM
    {
        public List<FuneralPaymentsModel> FunerlaPaymentList { get; set; }
        public List<FuneralServiceSelectModel> FuneralServiceList { get; set; }
        public FuneralModel FuneralModel { get; set; }
        public string FuneralNumber { get; set; }
        public string ReceivedBy { get; set; }
        public string TotalAmount { get; set; }
        public int MonthPaid { get; set; }
        public string Notes { get; set; }

    }
}