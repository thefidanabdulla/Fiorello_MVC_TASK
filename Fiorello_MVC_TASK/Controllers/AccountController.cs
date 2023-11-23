using Fiorello_MVC_TASK.Models;
using Fiorello_MVC_TASK.ViewModels.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC_TASK.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            User user = new User
            {
                Fullname = model.FullName,
                Email = model.Email,
                UserName = model.UserName,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            return RedirectToAction("login");

            return View();
        }
    
        public async Task<IActionResult> Login()
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
                ModelState.AddModelError(string.Empty, "Username or password is wrong");
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            
            if(!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username or password is wrong");
                return View(model);
            }

            if(!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("index", "home");
        }
    
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login");
        }
    }
}
