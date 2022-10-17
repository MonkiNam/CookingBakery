using CookingBakery.Model;
using CookingBakery.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookingBakery.Pages.Home
{
    public class UserProfileModel : PageModel
    {
        private readonly IUserService _userService;

        private readonly IWebHostEnvironment _env;

        public UserProfileModel(IUserService userService, IWebHostEnvironment env)
        {
            _userService = userService;
            _env = env;
        }

        [BindProperty]
        public User user { get; set; }

        public void OnGet()
        {
            user = _userService.GetUserByUserID(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }

        public async Task<IActionResult> OnPostAsync(IFormFile customFile)
        {
            if(customFile != null)
            {
                string url = await Utils.UploadImage.UploadFile(customFile, _env);
                user.Avatar = url;
            }
            await _userService.UpdateUser(user);
            TempData["success"] = "Profile has been updated successfully";
            return Page();
        }
    }
}
