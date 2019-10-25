using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    class MemberExcelData
    {
        public MemberExcelData()
        {
            PolicyNumber = string.Empty;
            Pol = string.Empty;
            Surname = string.Empty;
            Name = string.Empty;
            IDNo = string.Empty;
            Id2 = string.Empty;
            PlaneName = string.Empty;
            Primium = string.Empty;
            Cover = string.Empty;
            InceptionDate =string.Empty;
            CellNumber = string.Empty;
            Cell = string.Empty;


        }

        public string PolicyNumber { get; set; }
        public string Pol { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string IDNo { get; set; }
        public string Id2 { get; set; }
        public string PlaneName { get; set; }
        public string Primium { get; set; }
        public string Cover { get; set; }
        public string InceptionDate { get; set; }
        public string CellNumber { get; set; }
        public string Cell { get; set; }
        

    }
}
