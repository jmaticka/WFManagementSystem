using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class WorkflowInstance
    {
        public int ID { get; set; }
        public DateTime DateTimeStarted { get; set; }
        public DateTime DateTimeEnded { get; set; }
        public Workflow Workflow { get; set; }
        public IdentityUser UserStarted { get; set; }

    }
}
