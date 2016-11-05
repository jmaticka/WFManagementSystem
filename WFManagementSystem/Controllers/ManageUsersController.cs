using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFManagementSystem.ViewModels;
using WFMDatabase;
using WFMDatabase.Entities;
using static WFManagementSystem.Controllers.ManageController;

namespace WFManagementSystem.Controllers
{
    public class ManageUsersController : Controller
    {
        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
        }


        private List<IdentityRole> UserRoles(List<IdentityUserRole> identityUserRoles)
        {
            using (var context = new DBContextWFManagementSystem())
            {
                var allRoles = context.Roles.ToList();
                var roles = allRoles.Where(role => identityUserRoles.Any(iur => iur.RoleId == role.Id)).ToList();
                return roles;
            }
        }


        // GET: ManageUsers
        public ActionResult Index()

        {
            var users = new List<UserWithRoleNamesViewModel>();
            foreach (var user in UserManager.Users.ToList())
            {
                users.Add(new UserWithRoleNamesViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = UserRoles(user.Roles.ToList())

                });
            }


            ViewBag.Users = users;
            return View();

        }

        public async System.Threading.Tasks.Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            ViewBag.User = user;

            List<IdentityRole> userRoles;
            using (var context = new DBContextWFManagementSystem())
            {
                userRoles = context.Roles.ToList();
            }
            ViewBag.UserRoles = new SelectList(userRoles, "Id", "Name", userRoles.FirstOrDefault(role => role.Id == user.Roles.First().RoleId).Id);
            return View();

        }


        // POST: ManageUsers/Edit/5
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Edit(string id, string oldPassword, ManageUsersViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = await UserManager.FindByIdAsync(id);
                IdentityResult result = null;


                if (!user.Roles.Any(role => role.RoleId == model.UserRole))
                {
                    var currentRole = UserManager.GetRoles(id).ToList().First();
                    
                    using (var context = new DBContextWFManagementSystem())
                    {
                        var roleToAdd = context.Roles.FirstOrDefault(x => x.Id == model.UserRole).Name;
                        result = await UserManager.AddToRoleAsync(id, roleToAdd);
                        

                    }
                    var resultRemoveRole = UserManager.RemoveFromRole(id, currentRole);
                    if (!result.Succeeded || !resultRemoveRole.Succeeded)
                    {
                      
                            return RedirectToAction("Index", new { Message = "Uživatel nebyl změnen: Problem s přiřazením role." });
                        
                    }


                }


                if (model.NewPassword != null)
                {
                    string resetToken = await UserManager.GeneratePasswordResetTokenAsync(id);
                    result = await UserManager.ResetPasswordAsync(id, resetToken, model.NewPassword);

                    if (result.Succeeded)
                    {

                        if (result.Succeeded)
                        {
                            user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                            if (user != null)
                            {
                                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            }

                        }
                        else
                        {
                            return RedirectToAction("Index", new { Message = "Uživatel nebyl změnen: Problém s přiřazením hesla." });
                        }
                    }
                }
                if(result == null || result.Succeeded)
                {
                    user = await UserManager.FindByIdAsync(id);
                    user.Email = model.Email;
                    result = await UserManager.UpdateAsync(user);

                }
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", new { Message = "Uživatel v pořádku změněn" });
                }
                else
                {
                    return RedirectToAction("Index", new { Message = "Uživatel nebyl změnen: Problém s aktualizací uživatele." });
                }


            }
            catch
            {
                return View(model);
            }
        }


        // POST: ManageUsers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
