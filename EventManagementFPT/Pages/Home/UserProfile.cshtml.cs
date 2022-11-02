using BussinessObject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.BakeryModules.UserModule.Interface;
using Repositories.Utils;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookingBakery.Pages.Home
{
    public class UserProfile : PageModel
    {
        private readonly IUserService _userService;

        private readonly CookingBakeryContext _context;


        private readonly IWebHostEnvironment _env;

        public UserProfile(IUserService userService, CookingBakeryContext context)
        {
            _userService = userService;
            _context = context;
        }

        [BindProperty]
        public User user { get; set; }

        public void OnGet()
        {
            user = _userService.GetUserByUserID(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }

        public async Task<IActionResult> OnPostAsync(IFormFile customFile)
        {
            if (customFile != null)
            {
                string url = await UploadImage.UploadFile(customFile, _env);
                user.Avatar = url;
            }
            await _userService.UpdateUser(user);
            TempData["success"] = "Profile has been updated successfully";
            return Page();
        }
    }
}
