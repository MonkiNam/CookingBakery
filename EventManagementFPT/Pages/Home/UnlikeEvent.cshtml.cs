using System;
using System.Security.Claims;
using CookingBakery.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookingBakery.Pages.Home
{
    public class UnlikeEvent : PageModel
    {
        private readonly IUserService _userService;

        public UnlikeEvent(IUserService userService)
        {
            _userService = userService;
        }
        
        public IActionResult OnGet(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            _userService.UnlikeEvent(userId, id);
            return RedirectToPage("/Home/Details", new {id});
        }
    }
}