using System;

namespace Funeral.Model
{
    public class MappedDependents
    {
        public int ImportdependentId { get; set; }
        public string SystemTypeName { get; set; }
        public string ExcelTypeName { get; set; }
        public Guid parlourId { get; set; }
        public Guid ImportedId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
