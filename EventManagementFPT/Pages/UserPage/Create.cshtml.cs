using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace EventManagementFPT.Pages.UserPage
{
    [Authorize(Roles="admin")]
    public class CreateModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _env;

        public CreateModel(EventManagementFPT.Model.EventManagementContext context, IUserService userService, IWebHostEnvironment env)
        {
            _context = context;
            _userService = userService;
            _env = env; 
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile customFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(customFile != null)
            {
                await Utils.UploadImage.UploadFile(customFile, _env);
            }
            else
            {
                User.Avatar = "~/image/default.png";
            }

            await _userService.AddNewUser(User);

            return RedirectToPage("./Index");
        }
    }
}
