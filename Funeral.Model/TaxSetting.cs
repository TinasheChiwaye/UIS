using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class TaxSetting
    {
        public int Id { get; set; }
        public string TaxText { get; set; }
        public decimal TaxValue { get; set; }
    }
}
