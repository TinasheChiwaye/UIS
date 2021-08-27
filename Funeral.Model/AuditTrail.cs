using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class AuditTrail
    {
        public string User { get; set; }
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionDesc { get; set; }
    }
}
