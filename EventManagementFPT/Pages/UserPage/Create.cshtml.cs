using System.Threading.Tasks;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.UserModule.Interface;
using EventManagementFPT.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagementFPT.Pages.UserPage
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _env;

        public CreateModel(IUserService userService, IWebHostEnvironment env)
        {
            _userService = userService;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty] public new User User { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile customFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (customFile != null)
            {
                await UploadImage.UploadFile(customFile, _env);
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