using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFManagementSystem.ViewModels
{
    public class UserWithRoleNamesViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<IdentityRole> Roles { get; set; }

    }
}