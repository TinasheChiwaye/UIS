using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class DownloadSchedulesViewModel
    {
        public long FuneralId { get; set; }
        public string Description { get; set; }
        public DateTime FuneralStartDate { get; set; }
        public DateTime FuneralEndDate { get; set; }
        public string GraveNo { get; set; }
        public string DriverAndCars { get; set; }

    }
}
