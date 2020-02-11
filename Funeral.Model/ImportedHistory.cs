using System;

namespace Funeral.Model
{
    public class ImportedHistory
    {
        //        ImportedId
        //FileName
        //ParlourId
        //MappingColumn
        //IsImported
        //ImportedBy
        //ImportedDate

        public int ImportedId { get; set; }
        public string ImportedFilename { get; set; }
        public Guid parlourId { get; set; }
        public string MappingColumn { get; set; }
        public bool IsImported { get; set; }
        public string ImportedBy { get; set; }
        public DateTime ImportedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid NewImportedId { get; set; }
        public string MemberType { get; set; }

    }
}
