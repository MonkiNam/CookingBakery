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
    public class DetailsModel : PageModel
    {
        private readonly IReportService _reportService;

        public DetailsModel(IReportService reportService)
        {
            _reportService = reportService;
        }

        public Report Report { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();

            Report = await _reportService.GetReportByID(id);

            if (Report == null) return NotFound();

            return Page();
        }
    }
}