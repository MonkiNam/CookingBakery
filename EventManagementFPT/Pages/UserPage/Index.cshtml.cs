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
    public class IndexModel : PageModel
    {
        private readonly EventManagementFPT.Model.EventManagementContext _context;
        private IUserService _userService;

        public IndexModel(EventManagementFPT.Model.EventManagementContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public IList<User> User { get;set; }

        public void OnGet()
        {
            User = (IList<User>) _userService.GetAll();
        }
    }
}
