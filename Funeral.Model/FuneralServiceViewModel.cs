using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class FuneralServiceViewModel
    {
        public FuneralModel Funeral { get; set; }
        public ApplicationSettingsModel ApplicationSettings { get; set; }
        public List<FuneralServiceSelectModel> FuneralServiceSelect { get; set; }
        public List<FuneralPaymentsModel> FuneralPayments { get; set; }
        public ApplicationTnCModel ApplicationTnCModel { get; set; } 
    }
}
