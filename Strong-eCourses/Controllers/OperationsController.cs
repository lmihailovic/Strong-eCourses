using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Strong_eCourses.Controllers
{
    public class OperationsController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public OperationsController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public ViewResult Create()=>View();

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser appUser=new ApplicationUser
                {
                    UserName = user.Name,
                    Email = user.Email,
                };

                IdentityResult result=await userManager.CreateAsync(appUser,user.Password);
                if(result.Succeeded)
                {
                    ViewBag.Message="Korisnik je uspesno kreiran!";
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }
                return View(user);
            }
        }
    }
}