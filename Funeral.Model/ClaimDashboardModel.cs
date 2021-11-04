using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Funeral.Model
{

    public class ClaimDashboardModel
    {
        public string TodayTotalPayment { get; set; }
        public string OutstandingPaymentsCount { get; set; }
        public string TotalSMSCreditsCount { get; set; }
        public ClaimDashboardChart DashboardChart { get; set; }
        public bool IsDisplayDashboarTable { get; set; }
    }
    public class ClaimDashboardChart
    {
        public string ChartLabels { get; set; }
        public string ChartData1 { get; set; }
        public string ChartData2 { get; set; }
        public string ChartTotalCount { get; set; }
        public string PolicyPieChartTotalCount { get; set; }
        public string PolicyPieChartLabels { get; set; }
        public string PolicyPieChartData1 { get; set; }
        public string PolicyPremiumPieChartTotalCount { get; set; }
        public string PolicyPremiumPieChartLabels { get; set; }
        public string PolicyPremiumPieChartData { get; set; }
        public string Currency { get; set; }
        public DataTable dataCollection { get; set; }
    }
}
