using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventManagementFPT.Model;
using EventManagementFPT.Modules.ReportModule.Interface;
using Microsoft.AspNetCore.Authorization;

namespace EventManagementFPT.Pages.ReportPage
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IReportService _reportService;

        public DeleteModel(IReportService reportService)
        {
            _reportService = reportService;
        }

        [BindProperty]
        public Report Report { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report = await _reportService.GetReportByID(id);

            if (Report == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report = await _reportService.GetReportByID(id);

            if (Report != null)
            {
                await _reportService.DeleteReport(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
