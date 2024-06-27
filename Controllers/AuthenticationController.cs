using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace EasyCheck.Controllers;

public class AuthenticationController : Controller
{
    public AuthenticationController()
    {

    }

    [HttpGet("/login")]
    public IActionResult Login(string returnUrl = "/")
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login(string username, string password, string returnUrl = "/")
    {
        if (username == "user" && password == "password")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            TempData["SuccessMessage"] = "Login successful!";
            return LocalRedirect(returnUrl);
        }
        TempData["ErrorMessage"] = "Invalid username or password.";
        return View();
    }

    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Authentication");
    }
}