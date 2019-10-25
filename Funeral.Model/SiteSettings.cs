using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class SiteSettings : ISiteSettings
    {
        public string SSRSUserName
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["SSRSUserName"]; }
        }

        public string SSRSPassword
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["SSRSPassword"]; }
        }

        public string SSRSDomain
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["SSRSDomain"]; }
        }

        public string SSRSUrl
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["SSRSUrl"]; }
        }
    }
}
