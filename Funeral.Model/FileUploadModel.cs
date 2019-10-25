using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class FileUploadModel
    {
        public byte[] Data { get; set; }
        public string ThumbnailData { get; set; }
        public string FileName { get; set; }
    }
}
