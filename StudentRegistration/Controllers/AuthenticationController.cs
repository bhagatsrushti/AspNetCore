using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Models;
using StudentRegistration.Services.Interfaces;
using System.Security.Claims;
using StudentRegistration.Data;
using Microsoft.AspNetCore.Identity;

namespace StudentRegistration.Controllers
{
    public class AuthenticationController : Controller
   {
        private readonly IAuthentication _authentication;
        private readonly ApplicationDbcontext _dbcontext;
        public AuthenticationController(ApplicationDbcontext dbcontext, IAuthentication authentication)
        {
                _authentication = authentication;
                _dbcontext = dbcontext;
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginCheck(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid) 
            {
                if(_authentication.AuthUser(loginViewModel.Username, loginViewModel.Password))
                {

                    var user =  _dbcontext.users.FirstOrDefault(u => u.username == loginViewModel.Username);

                    if (user != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.username),
                            new Claim(ClaimTypes.Email, user.email),
                            
                        };

                        
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        
                         HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                        return RedirectToAction("Index", "Student");
                    }

                    // Handle invalid login here
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View(loginViewModel); // Return the view with error

                }

            }
            return RedirectToAction("Login", "Authentication");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authentication");
        }

    }
}
