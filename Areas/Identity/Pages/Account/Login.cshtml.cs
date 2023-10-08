using System.ComponentModel.DataAnnotations;
using Admin_Panel_ITI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Admin_Panel_ITI.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<AppUser> _userManager;

        public LoginModel(SignInManager<AppUser> signInManager, ILogger<LoginModel> logger, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }


        [BindProperty]
        public InputModel Input { get; set; }


        public IList<AuthenticationScheme> ExternalLogins { get; set; }

 
        public string ReturnUrl { get; set; }


        [TempData]
        public string ErrorMessage { get; set; }



        public class InputModel
        {
            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

    
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

   
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            //clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(Input.UserName);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure:false);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");

                        //check the login user Role
                        if (await _userManager.IsInRoleAsync(user, "Admin"))
                            returnUrl = Url.Action("Index", "Home");
                        else if (await _userManager.IsInRoleAsync(user, "Instructor"))
                            returnUrl = Url.Action("Index", "Intake", new { area = "InstructorsArea" });
                        else
                            returnUrl = "~/ERROR404/Error.html"; 

                        return LocalRedirect(returnUrl);
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Credentials!");
            }

            //if we got this far, something failed, redisplay login form
            return Page();
        }
    }
}
