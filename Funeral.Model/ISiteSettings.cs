using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
   public interface ISiteSettings
    {
        string SSRSUserName { get; }
        string SSRSPassword { get; }
        string SSRSDomain { get; }
        string SSRSUrl { get; }
        string SSRSFolderName { get; }
    }
}
