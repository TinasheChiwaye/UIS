using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class ClaimDocumentModel
    {
        public ClaimDocumentModel()
        {
             InitVariables();
        }
        public void InitVariables()
        { 
            this.pkiClaimPictureID =0;
            this.ImageName = string.Empty;
            this.fkiClaimID =0;
            this.CreateDate= DateTime.MinValue;
            this.LastModified = DateTime.MinValue;
            this.ModifiedUser = string.Empty;
            this.DocType=0;
            this.DocContentType = string.Empty;
        }
        public int pkiClaimPictureID { get; set; }
        public string ImageName { get; set; }
        public Byte[] ImageFile { get; set; }
        public int fkiClaimID { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid Parlourid { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedUser { get; set; }
        public int DocType { get; set; }
        public string DocContentType { get; set; }

    }
}









