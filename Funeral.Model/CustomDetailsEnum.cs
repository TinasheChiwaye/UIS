using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class CustomDetailsEnums
    {
        public CustomDetailsEnums()
        {

        }
        public enum CustomDetailsType
        {
            EmploymentType = 1,
            PaymentType = 2,
            Source = 3,
            MaritalStatus = 4,
            DentalCondition = 5,
            GeneralCondition = 6,
            AgeGroup = 7,
            HeadHair = 8,
            ClientMatch = 9,
            ClientRelationship = 10,
            TransactionFrequency = 11,
            DeliveryChannel = 12
        }
    }
}
