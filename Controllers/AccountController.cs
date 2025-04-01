using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ActionBoard.Models;
using System.Security.Claims;

namespace ActionBoard.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string? returnUrl = null, bool forceLogin = false)
        {
            if (forceLogin)
            {
                // Clear any existing Google authentication
                Response.Cookies.Delete(".AspNetCore.Google", new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(-1),
                    Path = "/",
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax
                });
            }
            
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ShowProviders = false
            };
            
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ShowLoginOptions(string? returnUrl = null)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ShowProviders = true
            };
            
            return View("Login", model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string? returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            
            // Force Google to show the account selector
            if (provider == "Google")
            {
                properties.Items["prompt"] = "select_account";
            }
            
            return Challenge(properties, provider);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }

            // If the user does not have an account, create one
            var email = info.Principal.FindFirstValue(ClaimTypes.Email) ?? info.Principal.FindFirstValue(ClaimTypes.Name);
            if (email == null)
            {
                ModelState.AddModelError(string.Empty, "Error: Could not retrieve email from external provider");
                return RedirectToAction(nameof(Login));
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)
            };

            var createResult = await _userManager.CreateAsync(user);
            if (createResult.Succeeded)
            {
                createResult = await _userManager.AddLoginAsync(user, info);
                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToLocal(returnUrl);
                }
            }

            foreach (var error in createResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await _signInManager.SignOutAsync();
            
            // Clear all cookies
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(-1),
                    Path = "/",
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax
                });
            }
            
            // Clear specific authentication cookies
            var cookiesToDelete = new[]
            {
                "TodoApp.Auth",
                ".AspNetCore.Google",
                ".AspNetCore.Identity.Application",
                ".AspNetCore.Identity.External",
                "G_AUTHUSER_H",
                "G_AUTHUSER_H_*"
            };

            foreach (var cookieName in cookiesToDelete)
            {
                Response.Cookies.Delete(cookieName, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(-1),
                    Path = "/",
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax
                });
            }

            // Clear any Google-specific cookies
            foreach (var cookie in Request.Cookies.Keys.Where(k => k.StartsWith("G_")))
            {
                Response.Cookies.Delete(cookie, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(-1),
                    Path = "/",
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax
                });
            }

            // Redirect to login with parameters to force new login
            return RedirectToAction("Login", "Account", new { 
                forceLogin = true,
                prompt = "select_account" // This tells Google to show the account selector
            });
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
} 