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
    public class DeleteModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;
        private IUserService _userService;

        public DeleteModel(EventManagementFPT.Model.EventManagementContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(id);

            return RedirectToPage("./Index");
        }
    }
}
