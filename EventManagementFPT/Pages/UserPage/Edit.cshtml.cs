using System;
using System.Threading.Tasks;
using CookingBakery.Models;
using CookingBakery.BakeryModules.UserModule.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookingBakery.Pages.UserPage
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly CookingBakeryContext _context;
        private IUserService _userService;

        public EditModel(CookingBakeryContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [BindProperty]
        public new User User { get; set; }

        public IActionResult OnGet(Guid? id)
        {
            if (id == null) return NotFound();

            User = _userService.GetUserByUserID(id);

            if (User == null) return NotFound();
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _userService.UpdateUser(_context.Attach(User).Entity);

            return RedirectToPage("./Index");
        }

    }
}
