using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WFMDatabase.Entities;

namespace WFManagementSystem.ViewModels
{
    public class ManageWorkflowsViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [DisplayName("Název")]
        public string WorkflowName { get; set; }

        #region block
        public string BlockName { get; set; }
        public string BlockDescription { get; set; }
        public string BlockValue { get; set; }
        [DisplayName("Typ bloku")]
        public virtual string BlockTypeId { get; set; }
        [DisplayName("Uživatel")]
        public string BlockWorkerId { get; set; }
        #endregion

        public bool WorkflowIsActual { get; set; } = true;
        public DateTime WorkflowDateTimeCreated { get; set; } = DateTime.Now;
    }
}