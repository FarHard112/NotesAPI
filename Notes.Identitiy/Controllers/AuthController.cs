using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Identitiy.Models;
using System.Threading.Tasks;

namespace Notes.Identitiy.Controllers
{

    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IIdentityServerInteractionService _interactionServices;
        public AuthController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager, IIdentityServerInteractionService _interactionServices)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this._interactionServices = _interactionServices;
        }



        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel()
            { ReturnUrl = returnUrl };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var user = await userManager.FindByNameAsync(viewModel.UserName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(viewModel);
            }
            var result =
                await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction(viewModel.ReturnUrl);
            }
            ModelState.AddModelError(string.Empty, "Login Error");
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var viewModel = new RegisterViewModel()
            { ReturnUrl = returnUrl };
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new AppUser { UserName = model.UserName };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return RedirectToAction(model.ReturnUrl);
            }
            ModelState
                .AddModelError(string.Empty, "Error occured");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await signInManager.SignOutAsync();
            var logoutRequest = await _interactionServices.GetLogoutContextAsync(logoutId);
            return RedirectToAction(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
