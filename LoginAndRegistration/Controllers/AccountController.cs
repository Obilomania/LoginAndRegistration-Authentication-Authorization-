using LoginAndRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginAndRegistration.Controllers
{ 
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        //REGISTER CONTROLLER
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //add role here
                    //await _userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("RegisterSuccess", "Account");
                }
            }
            ModelState.AddModelError("", "Invalid Register.");
            return View(model);
        }

        //LOGIN CONTROLLER
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    return RedirectToAction("LoginSuccess", "Account");
                    //user role list here
                    //var roles = await _userManager.GetRolesAsync(user);
                  
                    //string role = roles.FirstOrDefault();
                    //if (role.Equals("Admin"))
                    //{
                    //    return RedirectToAction("AdminActionName", "AdminControllerName");
                    //}
                    //else if (role.Equals("User"))
                    //{
                    //    return RedirectToAction("UserActionName", "UserControllerName");
                    //}
                    //else
                    //{
                    //    //do something here. put in your logic 
                    //}
                }
            }
            ModelState.AddModelError("", "Invalid ID or Password");
            return View(model);
        }

        public IActionResult RegisterSuccess()
        {
            return View();
        }

        public IActionResult LoginSuccess()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
