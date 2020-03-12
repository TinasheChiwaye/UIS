using System;
using System.ComponentModel.DataAnnotations;

namespace Funeral.Model
{
    public class ClaimAssigned
    {
        public int ClaimId { get; set; }
        public int CurrentAssigned { get; set; }
        [Required]
        public int NewAssigned { get; set; }

        public Guid ParlourId { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
