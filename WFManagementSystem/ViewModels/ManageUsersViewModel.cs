using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WFManagementSystem.ViewModels
{
    public class ManageUsersViewModel
    {
        [Required]
        public string UserRole { get; set; }
        
        [StringLength(100, ErrorMessage = "Heslo musí mít alespoň {2} znaků.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nové heslo")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrzení hesla")]
        [Compare("NewPassword", ErrorMessage = "Nové heslo a potvrzení hesla nesouhlasí")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [Display(Name = "Změnit mail")]
        public string Email { get; set; }

    }
}