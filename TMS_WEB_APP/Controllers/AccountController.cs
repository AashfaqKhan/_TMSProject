using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TMS_BAL.Service;
using TMS_BAL.Service.IService;
using TMS_DAL.DBContext;
using TMS_DAL.Entities;
using TMS_DTO.DTOs;


namespace TMS_WEB_APP.Controllers
{
    public class AccountController : Controller
    {

        private readonly PashwordHashService _passwordHashService;
        private readonly IUserService _userService;

        public AccountController( PashwordHashService passwordHashService, IUserService userService)
        {
          
            _passwordHashService = passwordHashService;
            _userService = userService;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {

                var user = _userService.GetUserByEmailAsync(model.Username).Result;
                if (user != null)
                {
                    string hashedPassword = _passwordHashService.HashPassword(model.Password);


                    bool isValid = (user.Email == model.Username && user.Password == hashedPassword);

                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.Username) }, CookieAuthenticationDefaults.AuthenticationScheme);
                        var princaple = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princaple);
                        HttpContext.Session.SetString("UserName", model.Username);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["errorpassword"] = "Invalid Password";
                        return View(model);
                    }

                }
                else
                {
                    TempData["errorusername"] = "Invalid UserName";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }


        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model.NewPassword != model.ConfirmNewPassword)
                {
                    TempData["errorpassword"] = "Both the password and confirm password fields value must be matched";
                    return View(model);
                }

                var username = HttpContext.User.Identity.Name;
                if (username == null)
                {
                    TempData["error"] = "Not logged in";
                    return View(model);
                }

                var user = await _userService.GetUserByEmailAsync(username);
                if (user == null)
                {
                    TempData["error"] = "User not found.";
                    return View(model);
                }
                var current = _passwordHashService.HashPassword(model.CurrentPassword);
                if (current != user.Password)
                {
                    TempData["errorpassword"] = "Invalid current password.Please enter a valid password.";
                    return View(model);
                }

                await _userService.ChangePasswordAsync(user.Id, user.Password,model.NewPassword);

                TempData["success"] = "Your Password changed successfully";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

    }
}

