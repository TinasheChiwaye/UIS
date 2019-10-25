using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
  public  class MemberNotesModel
    {
        public int pkiNoteID { get; set; }
        public int fkiMemberID { get; set; }
        [Required(ErrorMessage ="Note is required.")]
        public string Notes { get; set; }
        public DateTime NoteDate { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
    }




}
