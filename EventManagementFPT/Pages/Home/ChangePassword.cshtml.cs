using CookingBakery.Modules.UserModule;
using CookingBakery.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CookingBakery.Pages.Home
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IUserService _userService;

        public ChangePasswordModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string newPassword, string uid)
        {
            await _userService.ChangePassword(newPassword, uid);
            TempData["success"] = "Password has changed successfully";
            return RedirectToPage("./Index");
        }
    }
}
