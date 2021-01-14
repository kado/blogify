using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

using BlogifyWebApp.Models;
using BlogifyWebApp.Models.Interfaces;


namespace BlogifyWebApp.Controllers
{
    public class AccountController : Controller
    {
        //2020-01-13 - Kadel D. Lacatt
        //public ActionResult Login()
        //Handles GET request to /Account/Login, shows the Login view where users
        //con start session.

        private readonly IAuthProvider _authProvider;

        public AccountController(IAuthProvider authProvider)
        {
            this._authProvider = authProvider;
        }
        public ActionResult Login()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginVM)
        {

            ResultViewModel result = new ResultViewModel();
            try
            {
                if (loginVM != null)
                {

                    if (!String.IsNullOrEmpty(loginVM.Username) && !String.IsNullOrEmpty(loginVM.Password))
                    {
                        //Is the user is in the db. Proceed to create auth cookie
                        if (_authProvider.VerifyUser(loginVM.Username, loginVM.Password))
                        {
                            //Get the user profile to fill in the claims
                            IUser userProfile = _authProvider.GetUser(loginVM.Username);

                            if (userProfile != null)
                            {
                                //Array of Claims on the User Identity
                                var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name, userProfile.Username),
                                    new Claim("FullName", userProfile.Name),
                                    new Claim(ClaimTypes.Role, userProfile.Role.Trim()),
                                    new Claim("role", userProfile.Role.Trim())
                                };

                                var claimsIdentity = new ClaimsIdentity(
                                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                                var authProperties = new AuthenticationProperties
                                {

                                    // The time at which the authentication ticket expires.
                                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120),
                                    // The time at which the authentication ticket was issued.
                                    IssuedUtc = System.DateTime.Now,

                                };

                                //Attach the authentication cookie to the Response
                                await HttpContext.SignInAsync(
                                    CookieAuthenticationDefaults.AuthenticationScheme,
                                    new ClaimsPrincipal(claimsIdentity),
                                    authProperties);

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                throw new Exception("Wrong user profile error. Please contact support.");
                            }
                            
                        }
                    }
                }

                result.setWarningMessage("Wrong username or password.");
                ViewBag.Result = result;
                return View();
                
            }
            catch (Exception ex)
            {
                result.setWarningMessage(ex.Message);
                ViewBag.Result = result;
                return View();
            }
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
            }
            catch 
            {
                return RedirectToAction("Index","Home");
            }
            
            
        }
    }
}
