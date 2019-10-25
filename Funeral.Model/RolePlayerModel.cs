using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
  public  class RolePlayerModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Guid Parlourid { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreateddDate { get; set; }

    }
}
