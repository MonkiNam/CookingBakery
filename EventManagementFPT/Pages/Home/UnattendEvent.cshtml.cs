using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CookingBakery.Modules.UserEventModule.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookingBakery.Pages.Home
{
    public class UnattendEvent : PageModel
    {
        private readonly IUserEventService _userEventService;

        public UnattendEvent(IUserEventService userEventService)
        {
            _userEventService = userEventService;
        }
        
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _userEventService.NotGoingAnEvent(userId,id);
            return RedirectToPage("/Home/Details", new {id});
        }
    }
}