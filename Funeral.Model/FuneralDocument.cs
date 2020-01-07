using System;

namespace Funeral.Model
{
    public class FuneralDocument
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public bool IsChecked { get; set; }
        public string Status { get; set; }
        public string DocumentType { get; set; }
        public Guid ParlourId { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
