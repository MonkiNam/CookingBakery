using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventManagementFPT.Pages.Authentication
{
    public class IndexModel : PageModel
    {
        [Required(ErrorMessage = "This field is required")]
        [MinLength(1)]
        public string email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MinLength(1)]
        public string password { get; set; }
        public IActionResult OnGet()
        {
            if(User.Identity.IsAuthenticated)
            {
                TempData["noti"] = "You have already logged in";
                return RedirectToPage("../Index");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(string email, string password)
        {
            if(!ModelState.IsValid)
            {
                TempData["error"] = "Empty field(s) detected, please check your input!";
                return Page();
            }
            IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();
            string adminEmail = config["admin:email"];
            string adminPassword = config["admin:password"];
            if(email.Equals(adminEmail) && password.Equals(adminPassword))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(ClaimTypes.NameIdentifier, email)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                TempData["success"] = "Welcome admin";
                return RedirectToPage("../Index");
            }
            TempData["error"] = "Invalid credentials, please try again later!";
            return RedirectToPage("./Index");
        }
    }
}
