using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
   public class ScheduleEmailReportModel
    {
        public ScheduleEmailReportModel()
            {
                 pkiScheduleId = 0;
                 Frequency = string.Empty;
                 DateFrom = System.DateTime.Now;
                 DateTo = System.DateTime.Now;
                Time = System.DateTime.Now;
                ParlourId = new Guid("00000000-0000-0000-0000-000000000000");
             Email = string.Empty;
            ReportType = string.Empty;
            Report = string.Empty;

             }

        public int pkiScheduleId { get; set; }

        public string Frequency { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public DateTime Time { get; set; }

        public Guid ParlourId { get; set; }

        public string Email { get; set; }
        public string ReportType { get; set; }
        public string Report { get; set; }

    }
}
