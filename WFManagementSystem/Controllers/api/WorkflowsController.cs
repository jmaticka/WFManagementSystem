using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WFMDatabase.DML;
using WFMDatabase.Entities;

namespace WFManagementSystem.Controllers.api
{
    public class WorkflowsController : ApiController
    {
        IDMLWorkflow dbWorklfow = new DMLWorkflow();

        // POST api/<controller>
        [Route("api/workflows/create")]
        public IHttpActionResult Create([FromBody]Workflow value)
        {

            return Ok();
        }

    }
}
