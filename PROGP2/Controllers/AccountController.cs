using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using PROGP2.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Sdk;
using Microsoft.AspNetCore.Http;
using PROGP2.UserData;
using User = PROGP2.Models.User;

namespace PROGP2.Controllers
{
    public class AccountController : Controller
    {
        AgriEnergyConnectContext context = new AgriEnergyConnectContext();
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
                    new Claim("UserID", user.UserId.ToString()),
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
    }
}
