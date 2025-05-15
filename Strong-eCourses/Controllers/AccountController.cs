using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Strong_eCourses.Models;

namespace Strong_eCourses.Controllers;

public class AccountController : Controller
{
    private UserManager<ApplicationUser> userManager;
    private SignInManager<ApplicationUser> signInManager;
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login([Required][EmailAddress] string email, [Required] string password, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            ApplicationUser appUser = await userManager.FindByEmailAsync(email);

            if (appUser != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await
                signInManager.PasswordSignInAsync(appUser, password, false, false);

                if (result.Succeeded)
                {
                    return Redirect(returnUrl ?? "/");
                }
            }
            ModelState.AddModelError(nameof(email), "Uneli ste nepostojecu email adresu ili pogresnu lozinku");
        }

        return View();
    }

    [Authorize]
    public async Task<ActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}
