using System;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagementFPT.Pages.UserPage
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private IUserService _userService;
        public DetailsModel(IUserService userService)
        {
            _userService = userService;
        }

        public User User { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null) return NotFound();
            
            User = _userService.GetUserByUserID(id);
            
            if (User == null) return NotFound();

            return Page();
        }
    }
}
