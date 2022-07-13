using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.ReportModule.Interface;
using System.Security.Claims;
using EventManagementFPT.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EventManagementFPT.Pages.ReportPage
{
    [Authorize(Roles = "User, Host")]
    public class CreateModel : PageModel
    {
        private readonly IReportService _reportService;
        private readonly IUserService _userService;
        public CreateModel(IReportService reportService, IUserService userService)
        {
            _reportService = reportService;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Report Report { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string reportName, string reportContent, Guid eventId)
        {
            Guid uid = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if(_userService.GetUserByUserID(uid) == null)
            {
                return RedirectToPage("../../Error");
            }
            Report.Name = reportName;
            Report.Content = reportContent;
            Report.EventId = eventId;
            Report.Author = uid;

            await _reportService.AddNewReport(Report);

            return RedirectToPage("./Index");
        }
    }
}
