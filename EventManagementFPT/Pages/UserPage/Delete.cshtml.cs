using System;
using System.Threading.Tasks;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagementFPT.Pages.UserPage
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private IUserService _userService;

        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();

            User = _userService.GetUserByUserID(id);

            if (User == null) return NotFound();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null) return NotFound();

            await _userService.DeleteUser(id);

            return RedirectToPage("./Index");
        }
    }
}
