using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CookingBakery.Model;
using CookingBakery.Modules.ReportModule.Interface;
using System.Security.Claims;
using CookingBakery.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CookingBakery.Pages.ReportPage
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

        [BindProperty]
        public Report Report { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string reportName, string reportContent, Guid eventId)
        {
            var uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role is "Host" or "User")
            {
                if (uid != null)
                {
                    var userId = Guid.Parse(uid);
                    if (_userService.GetUserByUserID(userId) == null)
                    {
                        return RedirectToPage("../../Error");
                    }

                    Report.Name = reportName;
                    Report.Content = reportContent;
                    Report.EventId = eventId;
                    Report.Author = userId;
                }

                await _reportService.AddNewReport(Report);
                TempData["success"] = "A report has been created!";

                return RedirectToPage("/Home/Details", new { id = eventId });
            }

            TempData["error"] = "You are not allowed to perform this function";
            return RedirectToPage("/Index");
        }
    }
}