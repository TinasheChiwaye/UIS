using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
   public class BankModel
    {
       public Guid BankID { get; set; }
       public string BankName { get; set; }
       public string BranchCode { get; set; }

    }
    public class Provinces
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
    }
    public class FuneralStatus
    {
        public int FuneralStatusID { get; set; }
        public string Status { get; set; }
    }
}
