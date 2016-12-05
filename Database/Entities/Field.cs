using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFMDatabase.Entities
{
    public class Field : EntityBase
    {
        public string Action { get; set; }
        public DateTime DateTimeEnded { get; set; }
        public bool IsActive { get; set; }
        public virtual Block Block { get; set; }
        public virtual WorkflowInstance WorkflowInstance { get; set; }
        public ApplicationUser Worker { get; set; }
        public string Output { get; set; }
    }
}
