using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFManagementSystem.ViewModels
{
    public class ProcessStepViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime StartedDate { get; set; }
    }
}