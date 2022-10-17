using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CookingBakery.Modules.UserEventModule.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookingBakery.Pages.Home
{
    public class JoinEvent : PageModel
    {
        private readonly IUserEventService _userEventService;

        public JoinEvent(IUserEventService userEventService)
        {
            _userEventService = userEventService;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            await _userEventService.GoingAnEvent(userId,id, false);
            return RedirectToPage("/Home/Details", new {id});
        }
    }
}