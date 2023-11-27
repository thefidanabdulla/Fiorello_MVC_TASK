using Fiorello_MVC_TASK.Constants;
using Fiorello_MVC_TASK.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC_TASK.Controllers
{
    public class TempController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public TempController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Test()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles))) 
            {
                await roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
            }
            var user = new User
            {
                Fullname="admin",
                UserName = "admin",
                Email = "admin@gmail.com",
            }; 
            var result = await userManager.CreateAsync(user, "Admin123!");

            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
            }

            return Ok();
        }
    }
}
