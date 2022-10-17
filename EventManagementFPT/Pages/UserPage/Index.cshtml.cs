using System.Linq;
using CookingBakery.Model;
using CookingBakery.Modules.UserModule.Interface;
using CookingBakery.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookingBakery.Pages.UserPage
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public PaginatedList<User> User { get;set; }

        public void OnGet(int? pageIndex)
        {
            var users = _userService.GetAll().AsQueryable();
            User = PaginatedList<User>.Create(users, pageIndex ?? 1, 10);
        }
    }
}
