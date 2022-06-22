using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class FuneralServicesModel
    {
        public string ClaimantName { get; set; }
        public string ClaimantSurname { get; set; }
        public string ContactDetails { get; set; }
        public string DeceasedAddress { get; set; }
        public string DriverID { get; set; }
        public string DriverName { get; set; }
        public string NumberPlate { get; set; }
        public string CarMake { get; set; }
        public DateTime DeceasedArrival { get; set; }
        public string FridgeNumber { get; set; }
        public string TagNumber { get; set; }
        public string ShelfNumber { get; set; }
    }
}
