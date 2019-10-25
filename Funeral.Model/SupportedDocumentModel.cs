
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class SupportedDocumentModel
    {
        public int pkiPictureID { get; set; }
        public string ImageName { get; set; }
        public Byte[] ImageFile { get; set; }
        public int fkiMemberID { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime LastModified { get; set; }

        public string ModifiedUser { get; set; }
        public string DocContentType { get; set; }
        public Guid parlourid { get; set; }
        public int DocType { get; set; }

    }
}

