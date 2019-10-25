using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class RightsModel
    {
        public long ID { get; set; }
        public string ParentRightId { get; set; }

        [Required(ErrorMessage = "Please Select Application")]
        public int ApplicationID { get; set; }
                
        public string Formkey { get; set; }
        public string RoleId { get; set; }

        [Required(ErrorMessage = "Please Enter MenuName")]
        public string MenuName { get; set; }

        public string IconClassName { get; set; }

        [Required(ErrorMessage = "Please Enter MenuURL")]
        public string MenuURL { get; set; }
                
        public int MenuLevel { get; set; }

        public int MenuOrder { get; set; }

        public bool InMenu { get; set; }

        [Required(ErrorMessage = "Please enter Form Name")]
        public string FormName { get; set; }
        public string Description { get; set; }
        public bool IsMenu { get; set; }
        public bool IsForm { get; set; }
        public bool IsCreate { get; set; }
        public bool IsRead { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsOthers { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
