using Fiorello_MVC_TASK.Areas.ViewModels;
using Fiorello_MVC_TASK.Constants;
using Fiorello_MVC_TASK.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC_TASK.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Username or passsword is wrong");
                return View(model);
            }


            if (!await userManager.IsInRoleAsync(user, UserRoles.Admin.ToString()))
            {
                ModelState.AddModelError(string.Empty, "Username or passsword is wrong");
                return View(model);
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username or passsword is wrong");
                return View(model);
            }

            await signInManager.SignInAsync(user, false);

            return RedirectToAction("index", "products");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
    }
}
