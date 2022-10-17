using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CookingBakery.Model;
using CookingBakery.Modules.ReportModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CookingBakery.Pages.ReportPage
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IReportService _reportService;

        public EditModel(IReportService reportService)
        {
            _reportService = reportService;
        }

        [BindProperty]
        public Report Report { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id, int? status, string type)
        {
            if (type.Equals("edit"))
            {
                await _reportService.ChangeStatus(id, (int) status);
                TempData["success"] = "Status of the report has been updated!";
            }
            else if (type.Equals("delete"))
            {
                await _reportService.DeleteReport(id);
                TempData["success"] = "Report has been closed!";
            }

            return RedirectToPage("./Index");
        }
    }
}