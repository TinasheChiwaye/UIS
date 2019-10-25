using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Funeral.Model;

namespace Funeral.Web.Areas.Admin.Models.ViewModel
{
    public class MembersOtherPaymentDetailsVM
    {
        public MembersModel MembersModel { get; set; }
        public List<OtherPaymentModel> OtherPaymentModel { get; set; }
        public string ReceivedBy { get; set; }
        public string date { get; set; }
        public string Notes { get; set; }
        public Decimal amount { get; set; }
        public string methodOfPayment { get; set; }
    }
}