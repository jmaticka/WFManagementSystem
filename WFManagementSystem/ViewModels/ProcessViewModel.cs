using System.Collections.Generic;
using WFMDatabase.Entities;

namespace WFManagementSystem.ViewModels
{
    public class ProcessViewModel
    {
        public List<FieldBlockViewModel> Fields { get; set; }
        public int WorkflowId { get; set; }
        public string WorkflowName { get; set; }
    }
}