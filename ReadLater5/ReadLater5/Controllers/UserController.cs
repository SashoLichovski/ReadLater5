using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReadLater5.Mappers;
using ReadLater5.Models;
using Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> RegisterUser(RegisterModel registerModel)
        {
            if (await userService.ValidateUsername(registerModel.Username))
                return RedirectToAction("Register", new RegisterModel { RegisterMessage = $"Username {registerModel.Username} is already taken" });

            await userService.RegisterUserAsync(Mapper.RegisterModelToUser(registerModel));

            return RedirectToAction("Login");
        }

        public IActionResult Register(RegisterModel registerModel)
        {
            return View(registerModel);
        }

        public async Task<IActionResult> Login()
        {
            var loginModel = new LoginModel() 
            {   
                ExternalLogins = (await userService.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await userService.LogoutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Authenticate(LoginModel loginModel)
        {
            if (ModelState.IsValid && await userService.Authenticate(loginModel.Username, loginModel.Password))
                 return RedirectToAction("Index", "Home");
            else
                return View("Login", loginModel);
        }

        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "User");
            var properties = userService.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> ExternalLoginCallback(string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login", new LoginModel
                {
                    ExternalLogins = (await userService.GetExternalAuthenticationSchemesAsync()).ToList()
                });
            }

            var isSuccess = await userService.HandleExternalLoginAndCallbackAsync();
            if (isSuccess)
                return RedirectToAction("Index", "Home");

            ViewBag.ErrorTitle = $"Something went wrong";
            ViewBag.ErrorMessage = "Please contact support on Pragim@PragimTech.com";

            return View("Error");
        }
    }
}
