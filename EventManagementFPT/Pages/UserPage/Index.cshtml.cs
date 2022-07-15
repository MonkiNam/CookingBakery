using System.Collections.Generic;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventManagementFPT.Pages.UserPage
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public new IList<User> User { get;set; }

        public void OnGet()
        {
            User = (IList<User>) _userService.GetAll();
        }
    }
}
