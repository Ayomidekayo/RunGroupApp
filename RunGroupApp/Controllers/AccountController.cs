using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroupApp.Data;
using RunGroupApp.Models;
using RunGroupApp.ViewModel.Acc;

namespace RunGroupApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContex _contex;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            AppDbContex contex)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._contex = contex;
        }
        public IActionResult Login()
        {
            var response=new LoginVM();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }
            //Found user
            var user=await _userManager.FindByEmailAsync(loginVm.EmailAddress);
            if (user != null)
            {
                //Check password
                var checkPassword = await _userManager.CheckPasswordAsync(user, loginVm.Password);
                if (checkPassword)
                {
                    // if Password correct sigin
                    var result=await _signInManager.PasswordSignInAsync(user, loginVm.Password,false,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                    
                }
                //If Password is incrrect
                TempData["Error"] = "Wrong credentials. Please try agaimn later";
                return View(loginVm);
            }
            //If User is not found
            TempData["Error"] = "Wrong credentials. Please try agaimn later";
            return View(loginVm);
        }
        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid)
            {
                return View(registerVM);
            }
            var user=await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "email already exist";
                return View(registerVM);
            }
            var newUser = new AppUser()
            {
                UserName = registerVM.EmailAddress,
                Email = registerVM.EmailAddress,
            };
            var newUserRespose=await _userManager.CreateAsync(newUser,registerVM.Password);
            if (newUserRespose.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser,UserRole.User);
            }
            return RedirectToAction("Index","Race");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Race");
        }
    }
}
