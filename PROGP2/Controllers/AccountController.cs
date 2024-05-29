using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using PROGP2.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Sdk;
using Microsoft.AspNetCore.Http;

using User = PROGP2.Models.User;
using Notification;
using System.Net.Http;

namespace PROGP2.Controllers
{
    public class AccountController : Controller
    {
        AgriEnergyConnectContext context = new AgriEnergyConnectContext();
        HttpClient httpClient = new HttpClient();
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            // Check username and password
            var user = AuthenticateUser(username, password);


            if (user != null)
            {
                var roleName = context.Roles.FirstOrDefault(r => r.RoleId == user.RoleId)?.RoleName;
                if (roleName != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, roleName),
                };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Home");

                }
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View();
        }


        private User AuthenticateUser(string username, string password)
        {
            //var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            return context.Users
         .Include(u => u.Role)
         .FirstOrDefault(u => u.Username == username && u.Password == password);

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult RequestAccount()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RequestAccount(string username)
        {
            var notificationData = new NotificationData
            {
                RequestedBy = username,
                Message = $"Farmer '{username}' has requested an account.",
                IsRead = false
            };

            var response = await httpClient.PostAsJsonAsync("https://localhost:7242/api/Notifications", notificationData);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ConfirmAccount", "Account");
            }

            ModelState.AddModelError(string.Empty, "Unable to send request.");
            return View();
        }


        public IActionResult ConfirmAccount()
        {
            return View();
        }
        
        
    }
}

