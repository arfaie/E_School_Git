using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace E_School.Controllers.api.Management
{
    public class SiteController : ApiController
    {
        // GET: api/Site
        [HttpGet]
        public HttpResponseMessage Redirect()
        {
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri("http://localhost:12913");
            return response;

        }

        // GET: api/Site/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Site
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Site/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Site/5
        public void Delete(int id)
        {
        }
    }
}
