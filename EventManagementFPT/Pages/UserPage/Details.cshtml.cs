using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.UserModule.Interface;

namespace EventManagementFPT.Pages.UserPage
{
    public class DetailsModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;
        private IUserService _userService;
        public DetailsModel(EventManagementFPT.Model.EventManagementContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User = _userService.GetUserByUserID(id);
            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
