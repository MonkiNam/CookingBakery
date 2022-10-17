using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookingBakery.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            
            return role switch
            {
                "Admin" or "Host" => RedirectToPage("/EventPage/Index"),
                "User" => RedirectToPage("/Home/Index"),
                _ => RedirectToPage("/Authentication/Index")
            };
        }
    }
}
