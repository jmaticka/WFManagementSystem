using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFMDatabase.Entities
{
    public class WorkflowInstance : EntityBase
    {
        public DateTime DateTimeStarted { get; set; }
        public DateTime DateTimeEnded { get; set; }
        public Workflow Workflow { get; set; }
        public IdentityUser UserStarted { get; set; }

    }
}
