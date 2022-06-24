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
        public int CoffinSize { get; set; }
        public string TypeOfCoffin { get; set; }
        public string OtherServices { get; set; }
        public DateTime FuneralDatetime { get; set; }
        public string PolicyNumber { get; set; }
        public string BurialOrderNumber { get; set; }
        public string CarMakeRegistrantion { get; set; }
        public string DriverSurname { get; set; }
        public string GraveNumber { get; set; }
    }
}
