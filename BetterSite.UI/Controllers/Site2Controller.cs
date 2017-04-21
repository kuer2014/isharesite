using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BetterSite.UI.Controllers
{
    public class Site2Controller : ApiController
    {
        // GET api/Site
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Site/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Site
        public string Post(BetterSite.Domain.M_Sites site)
        {
            return site.SiteCode +"|"+ site.SiteName;
        }

        // PUT api/Site/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Site/5
        public void Delete(int id)
        {
        }
    }
}