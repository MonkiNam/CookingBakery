using System;
using System.Security.Claims;
using EventManagementFPT.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagementFPT.Pages.Home
{
    public class LikeEvent : PageModel
    {
        private readonly IUserService _userService;

        public LikeEvent(IUserService userService)
        {
            _userService = userService;
        }
        
        public IActionResult OnGet(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            _userService.LikeEvent(userId, id);
            return RedirectToPage("/Home/Details", new {id});
        }
    }
}