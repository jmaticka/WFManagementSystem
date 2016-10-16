using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Field : EntityBase
    {
        public string Value { get; set; }
        public DateTime DateTimeEnded { get; set; }
        public virtual Block Block { get; set; }
        public virtual WorkflowInstance WorkflowInstance { get; set; }
        public IdentityUser Worker { get; set; }

    }
}
