using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetInformatika.Views.Models;

namespace NetInformatika.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public UserController(ILogger<UserController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    /// <summary>
    /// POST login
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> AccountLogin(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View("Login", model);

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        // Failed
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt, try again!");

            _logger.LogWarning("User {0} tried to login!", model.Email);

            return View("Login", model);
        }

        // Success
        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// POST logout
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("logout")]
    public async Task<IActionResult> AccountLogout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("Login");
    }

    /// <summary>
    /// POST register
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> AccountRegister(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View("Register", model);

        var user = new IdentityUser { UserName = model.Email, Email = model.Email };

        var result = await _userManager.CreateAsync(user, model.Password);

        // Failed
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("Register", model);
        }

        // Success
        await _signInManager.SignInAsync(user, isPersistent: false);

        _logger.LogInformation("User {0} has been succesfully registered!", model.Email);

        return RedirectToAction("Index", "Home");
    }

    #region Pages
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }
    #endregion
}