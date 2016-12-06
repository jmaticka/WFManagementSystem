using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFMDatabase.Entities;

namespace WFManagementSystem.ViewModels
{
    public class FieldBlockViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ApplicationUser Worker { get; set; }
        public string Action { get; set; }
        public Block Block { get; set; }
        public  BlockType BlockType { get; set; }
        public string Position { get; set; }


        //just for detail
        public bool IsActive { get; set; }
        public string Output { get; set; }
        public DateTime DateTimeEnded { get; set; }

    }
}
