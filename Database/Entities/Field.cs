using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Field
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public DateTime DateTimeEnded { get; set; }
        public Block Block { get; set; }
        public WorkflowInstance WorkflowInstance { get; set; }
        public IdentityUser Worker { get; set; }

    }
}
