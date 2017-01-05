using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFMDatabase.Entities;

namespace WFManagementSystem.ViewModels
{
    public class FieldViewModel
    {
        public string Action { get; set; }
        public DateTime DateTimeEnded { get; set; }
        public bool IsActive { get; set; }
        public virtual Block Block { get; set; }
        public virtual WorkflowInstance WorkflowInstance { get; set; }
        public ApplicationUser Worker { get; set; }
        public string Output { get; set; }
        public bool Confirm { get; set; }
        public int ID { get; set; }
        public bool StopWorkflow { get; set; }
    }
}