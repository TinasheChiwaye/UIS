using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class ExpensesModel
    {
        public ExpensesModel()
        {

            pkiExpenseCategoryID = 0;
            Category = string.Empty;
            parlourid = new Guid("00000000-0000-0000-0000-000000000000");
            LastModified = System.DateTime.Now;
            ModifiedUser = string.Empty;
        }
        public int pkiExpenseCategoryID { get; set; }
        [Required(ErrorMessage ="Category is required.")]
        public string Category { get; set; }
        public Guid parlourid { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
    }
}
