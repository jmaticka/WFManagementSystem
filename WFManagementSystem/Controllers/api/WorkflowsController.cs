using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using WFMDatabase.DML;
using WFMDatabase.Entities;


namespace WFManagementSystem.Controllers.api
{
    public class WorkflowsController : ApiController
    {
        IDMLWorkflow _dbWorklfow = new DMLWorkflow();
        IDMLBlock _dbBlock = new DMLBlock();
        IDMLBlockType _dbBlockType = new DMLBlockType();

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        // POST api/<controller>
        [Route("api/workflows/create")]
        public async Task<IHttpActionResult> Create([FromBody]Workflow workflow)
        {
            workflow.DateTimeCreated = DateTime.Now;
            workflow.IsActual = true;
            //workflow.UserCreated = await UserManager.FindByNameAsync(User.Identity.Name);
           
            if (ModelState.IsValid)
            {
                var result = _dbWorklfow.Insert(workflow,User.Identity.GetUserId());
                return Created($"api/workflows/create/{workflow.Name}", workflow);
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
