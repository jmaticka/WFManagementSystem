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
        public HttpResponseMessage Create([FromBody]Workflow workflow)
        {
            workflow.DateTimeCreated = DateTime.Now;
            workflow.IsActual = true;
            //workflow.UserCreated = await UserManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                var result = _dbWorklfow.Insert(workflow, User.Identity.GetUserId());
                var newUrl = this.Url.Link("Default", new
                {
                    Controller = "ManageWorkflows",
                    Action = "Index"
                });
                return Request.CreateResponse(HttpStatusCode.OK,
                                                          new { Success = true, RedirectUrl = newUrl });
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPost, Route("api/workflows/get")]
        public IHttpActionResult Get([FromBody]int id)
        {
            var result = _dbWorklfow.GetById(id).Blocks.OrderByDescending(x => x.ID).ToList();
            result.RemoveAt(result.Count-1);
            result.Last().NextBlocks = new List<Block>();
            return Ok(result);
        }




        [Route("api/workflows/update")]
        public HttpResponseMessage Update([FromBody] Params postParams)
        {
            postParams.Workflow.DateTimeCreated = DateTime.Now;
            postParams.Workflow.IsActual = true;

            if (ModelState.IsValid)
            {
                var result = _dbWorklfow.Update(postParams.Workflow, postParams.Id, User.Identity.GetUserId());
                var newUrl = this.Url.Link("Default", new
                {
                    Controller = "ManageWorkflows",
                    Action = "Index"
                });
                return Request.CreateResponse(HttpStatusCode.OK,
                                                          new { Success = true, RedirectUrl = newUrl });
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

    }

    public class Params
    {
        public Workflow Workflow { get; set; }
        public int Id { get; set; }

    }
}
