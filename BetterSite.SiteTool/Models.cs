using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSite.SiteTool
{
 public   class SiteFile
    {
        private string siteUrl;
        private string siteFileName;

        public string SiteUrl
        {
            get
            {
                return siteUrl;
            }

            set
            {
                siteUrl = value;
            }
        }

        public string SiteFileName
        {
            get
            {
                return siteFileName;
            }

            set
            {
                siteFileName = value;
            }
        }
    }
}
