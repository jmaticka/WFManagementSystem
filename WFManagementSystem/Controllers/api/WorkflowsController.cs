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
        IDMLWorkflow _dbWorklfow = new DMLWorkflow();

        // POST api/<controller>
        [Route("api/workflows/create")]
        public IHttpActionResult Create([FromBody]Workflow workflow)
        {
            if (ModelState.IsValid)
            {
                var reuslt =_dbWorklfow.Insert(workflow);
                return Created($"api/workflows/create/{workflow.Name}",workflow);
            }
            return BadRequest(ModelState);
        }

        [Route("api/workflows/update")]
        public IHttpActionResult Update([FromBody] Workflow workflow)
        {
            if (ModelState.IsValid)
            {
                var result = _dbWorklfow.Update(workflow);
                return Created($"api/workflows/update/{workflow.Name}", workflow);
            }
            return BadRequest(ModelState);
        }
        
    }
}
