using System.Runtime.CompilerServices;
using EasyCheck.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyCheck.Controllers;

public class AuthController : Controller
{
    public AuthController()
    {

    }

    [HttpGet("/login")]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost("/login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if(ModelState.IsValid)
        {
            bool isAuthenticated = AuthenticateUser(model.Username, model.Password);
            if(isAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }
        return View();
    }

    private bool AuthenticateUser(string username, string password)
    {
        // Replace this with your actual authentication logic.
        // For example, check the username and password against a database.
        return username == "admin" && password == "password";
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}